using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Trials.Kevin.IRepository;
using Trials.Kevin.IService;

namespace Trials.Kevin.Service
{
    public class BaseService<TSource> : IBaseService<TSource> where TSource : class, new()
    {
        protected IUnitOfWork _unitOfWork;
        protected IBaseRepository<TSource> _repository;

        public BaseService(IUnitOfWork unitOfWork, IBaseRepository<TSource> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<EntityEntry<TSource>> AddAsync(TSource entity)
        {
            EntityEntry<TSource> entityEntry = await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entityEntry;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async ValueTask<int> DeleteAsync(Expression<Func<TSource, bool>> whereLambda)
        {
            await _repository.DeleteAsync(whereLambda);
            return await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<List<TSource>> GetListAsync(Expression<Func<TSource, bool>> whereLambda)
        {
            return await _repository.GetListAsync(whereLambda);
        }


        public async Task<Tuple<List<TSource>, int>> GetListPageAsync(int pageSize, int pageIndex, Expression<Func<TSource, bool>> whereLambda, Dictionary<string, string> sortFields)
        {
            return await _repository.GetListPageAsync(pageSize, pageIndex, whereLambda, sortFields);
        }

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<TSource> GetModelAsync(Expression<Func<TSource, bool>> whereLambda)
        {
            return await _repository.GetModelAsync(whereLambda);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async ValueTask<int> UpdateAsync(Expression<Func<TSource, bool>> whereLambda, Expression<Func<TSource, TSource>> entity)
        {
            await _repository.UpdateAsync(whereLambda, entity);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
