using System;
using System.Collections.Generic;
using System.Text;
using Trials.Kevin.IRepository;
using Trials.Kevin.IService.SaleOrderService;
using Trials.Kevin.Model.SaleOrderDB;

namespace Trials.Kevin.Service.SaleOrderService
{
    public class SaleOrderService : BaseService<SaleOrderEntity>, ISaleOrderService
    {
        public SaleOrderService(IUnitOfWork unitOfWork, IBaseRepository<SaleOrderEntity> repository) : base(unitOfWork, repository)
        {

        }
    }
}
