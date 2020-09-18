using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Trials.Kevin.IRepository
{
    /// <summary>
    /// 基础仓储接口
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    public interface IBaseRepository<TSource> where TSource : class, new()
    {
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<EntityEntry<TSource>> AddAsync(TSource entity, CancellationToken cancellationToken);


        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        ValueTask<int> DeleteAsync(Expression<Func<TSource, bool>> whereLambda, CancellationToken cancellationToken);

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<List<EntityEntry<TSource>>> UpdateAsync(Expression<Func<TSource, bool>> whereLambda, Expression<Func<TSource, TSource>> entity, CancellationToken cancellationToken);


        /// <summary>
        /// 获取单实体对象
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<TSource> GetModelAsync(Expression<Func<TSource, bool>> whereLambda, CancellationToken cancellationToken);

        /// <summary>
        /// 获取查询实体集合
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<List<TSource>> GetListAsync(Expression<Func<TSource, bool>> whereLambda, CancellationToken cancellationToken);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="whereLambda"></param>
        /// <param name="sortFields"></param>
        /// <returns></returns>
        Task<(List<TSource> entities, int totalCount)> GetListPageAsync(int pageSize, int pageIndex, Expression<Func<TSource, bool>> whereLambda, Dictionary<string, string> sortFields, CancellationToken cancellationToken);
    }
}
