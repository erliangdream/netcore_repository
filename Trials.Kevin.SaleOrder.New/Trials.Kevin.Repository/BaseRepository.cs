using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trials.Kevin.IRepository;
using Z.EntityFramework.Plus;

namespace Trials.Kevin.Repository
{
    public class BaseRepository<TSource> : IBaseRepository<TSource> where TSource : class, new()
    {
        private readonly DbContext _dbContext;
        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EntityEntry<TSource>> AddAsync(TSource entity, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TSource>().AddAsync(entity, cancellationToken);
        }

        public async ValueTask<int> DeleteAsync(Expression<Func<TSource, bool>> whereLambda, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TSource>().Where(whereLambda).DeleteAsync(cancellationToken);
        }

        public async Task<List<TSource>> GetListAsync(Expression<Func<TSource, bool>> whereLambda, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TSource>().Where(whereLambda).ToListAsync(cancellationToken);
        }

        public async Task<(List<TSource> entities, int totalCount)> GetListPageAsync(int pageSize, int pageIndex, Expression<Func<TSource, bool>> whereLambda, Dictionary<string, string> sortFields, CancellationToken cancellationToken)
        {
            var total = await _dbContext.Set<TSource>().Where(whereLambda).CountAsync();
            IQueryable<TSource> whereQuery = _dbContext.Set<TSource>().Where(whereLambda);
            System.Type dynamicSortTType = typeof(TSource);
            int index = 0;
            var parameter = Expression.Parameter(dynamicSortTType, "p");
            foreach (var item in sortFields)
            {
                string callSortMethodName = string.Empty;
                if (string.Compare(item.Value, "asc", true) == 0)
                {
                    if (index == 0)
                    {
                        callSortMethodName = "OrderBy";
                    }
                    else
                    {
                        callSortMethodName = "ThenBy";
                    }
                }
                else
                {
                    if (index == 0)
                    {
                        callSortMethodName = "OrderByDescending";
                    }
                    else
                    {
                        callSortMethodName = "ThenByDescending";
                    }
                }
                PropertyInfo propertyInfo = dynamicSortTType.GetProperty(item.Key);
                MemberExpression memberExpression = Expression.MakeMemberAccess(parameter, propertyInfo);
                var lambdaExpression = Expression.Lambda(memberExpression, parameter);

                MethodCallExpression methodCallExpression = Expression.Call(typeof(Queryable),
                    callSortMethodName,
                    new System.Type[] { dynamicSortTType, propertyInfo.PropertyType },
                    whereQuery.Expression, Expression.Quote(lambdaExpression));

                whereQuery = whereQuery.Provider.CreateQuery<TSource>(methodCallExpression);

                index++;
            }


            if (pageSize == 0 || pageIndex == 0)
            {
                var list = await whereQuery.ToListAsync(cancellationToken);

                return (entities: list, totalCount: total);
            }
            else
            {
                var list = await whereQuery.Skip(pageSize * (pageIndex - 1))
                         .Take(pageSize).ToListAsync(cancellationToken);

                return (entities: list, totalCount: total);
            }
        }



        public async Task<TSource> GetModelAsync(Expression<Func<TSource, bool>> whereLambda, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TSource>().AsNoTracking().FirstOrDefaultAsync(whereLambda, cancellationToken);
        }

        public async Task<EntityEntry<TSource>> UpdateAsync(Expression<Func<TSource, bool>> whereLambda, Expression<Func<TSource, TSource>> entity, CancellationToken cancellationToken)
        {
            //int executeCount = await _dbContext.Set<TSource>().Where(whereLambda).UpdateAsync(entity, cancellationToken);
            //_dbContext.Set<TSource>().Where(whereLambda).UpdateFromQueryAsync

            TSource sourceModel = await _dbContext.Set<TSource>().Where(whereLambda).FirstOrDefaultAsync();

            if (sourceModel == null)
            {
                return null;
            }

            _dbContext.Set<TSource>().Attach(sourceModel);

            var ee = (entity.Body as MemberInitExpression).Bindings;

            //获取表达式对象
            var lambdaResultObj = entity.Compile()(sourceModel);

            foreach (var item in ee)
            {
                //var bb = ((item as MemberAssignment).Expression as ConstantExpression).Value;
                //获取修改的属性名
                string propertyName = item.Member.Name;

                sourceModel.GetType().GetProperty(propertyName).SetValue(sourceModel,
                    lambdaResultObj.GetType().GetProperty(propertyName).GetValue(lambdaResultObj));

                _dbContext.Entry(sourceModel).Property(propertyName).IsModified = true;
            }
            var result = _dbContext.Set<TSource>().Update(sourceModel);
            return result;
            //return executeCount;
        }
    }
}
