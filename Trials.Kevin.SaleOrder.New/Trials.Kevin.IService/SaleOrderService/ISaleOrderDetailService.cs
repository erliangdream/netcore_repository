using System.Threading;
using System.Threading.Tasks;
using Trials.Kevin.Contract.Request.SaleOrder;
using Trials.Kevin.Contract.Response;
using Trials.Kevin.Model.SaleOrderDB;

namespace Trials.Kevin.IService.SaleOrderService
{
    public interface ISaleOrderDetailService : IBaseService
    {
        Task<SaleOrderDetailDto> GetModelAsync(long id, CancellationToken cancellationToken);

        Task<SaleOrderDetailQueryDto> GetEntitiesAsync(SaleOrderDetailQueryRequest request, CancellationToken cancellationToken);

        Task<BaseResponse<long>> AddModelAsync(SaleOrderDetailDto dto, CancellationToken cancellationToken);

        Task<BaseResponse> UpdateModelAsync(SaleOrderDetailDto dto, CancellationToken cancellationToken);

        Task<BaseResponse> DeleteModelAsync(long id, CancellationToken cancellationToken);
    }
}
