using Trials.Kevin.IRepository;
using Trials.Kevin.Model;
using Trials.Kevin.Model.SaleOrderDB;

namespace Trials.Kevin.Repository.SaleOrderRepository
{
    public class SaleOrderDetailRepository : BaseRepository<SaleOrderDetailEntity>, ISaleOrderDetailRepository
    {
        public SaleOrderDetailRepository(SaleOrderContext dbContext) : base(dbContext)
        {
        }
    }
}
