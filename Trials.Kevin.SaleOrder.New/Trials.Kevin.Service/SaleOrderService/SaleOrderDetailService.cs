using System;
using System.Collections.Generic;
using System.Text;
using Trials.Kevin.IRepository;
using Trials.Kevin.IService.SaleOrderService;
using Trials.Kevin.Model.SaleOrderDB;

namespace Trials.Kevin.Service.SaleOrderService
{
    public class SaleOrderDetailService : BaseService<SaleOrderDetailEntity>, ISaleOrderDetailService
    {
        public SaleOrderDetailService(IUnitOfWork unitOfWork, IBaseRepository<SaleOrderDetailEntity> repository) : base(unitOfWork, repository)
        {

        }
    }
}
