using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Trials.Kevin.IRepository;
using Trials.Kevin.Model;

namespace Trials.Kevin.Repository.UnitOfWork
{
    /// <summary>
    /// 销售订单模块工作单元
    /// </summary>
    public class SaleOrderUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// 销售订单上下文
        /// </summary>
        private readonly SaleOrderContext _saleOrderContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="saleOrderContext"></param>
        public SaleOrderUnitOfWork(SaleOrderContext saleOrderContext)
        {
            _saleOrderContext = saleOrderContext;
        }

        /// <summary>
        /// 获取DB上下文
        /// </summary>
        /// <returns></returns>
        public DbContext GetDbContext()
        {
            return _saleOrderContext;
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <returns></returns>
        public async ValueTask<int> SaveChangesAsync()
        {
            return await _saleOrderContext.SaveChangesAsync();
        }
    }
}
