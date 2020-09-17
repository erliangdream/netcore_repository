using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Trials.Kevin.Contract.Request.SaleOrder;
using Trials.Kevin.Contract.Response;
using Trials.Kevin.Model.SaleOrderDB;

namespace Trials.Kevin.IService.SaleOrderService
{
    public interface ISaleOrderService : IBaseService
    {
        Task<SaleOrderDto> GetModelAsync(long id, CancellationToken cancellationToken);

        Task<SaleOrderQueryDto> GetEntitiesAsync(SaleOrderQueryRequest request, CancellationToken cancellationToken);

        Task<BaseResponse<long>> AddModelAsync(SaleOrderDto dto, CancellationToken cancellationToken);

        Task<BaseResponse> UpdateModelAsync(SaleOrderDto dto, CancellationToken cancellationToken);

        Task<BaseResponse> DeleteModelAsync(long id, CancellationToken cancellationToken);
    }
}
