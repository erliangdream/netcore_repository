using Trials.Kevin.IRepository;
using Trials.Kevin.Model;
using Trials.Kevin.Model.SaleOrderDB;

namespace Trials.Kevin.Repository.SaleOrderRepository
{
    public class SaleOrderRepository : BaseRepository<SaleOrderEntity>, ISaleOrderRepository
    {
        public SaleOrderRepository(SaleOrderContext dbContext) : base(dbContext)
        {

        }
    }
}
