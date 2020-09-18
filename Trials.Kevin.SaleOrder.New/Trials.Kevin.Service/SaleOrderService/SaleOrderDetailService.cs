using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trials.Kevin.Contract.Enum;
using Trials.Kevin.Contract.Request.SaleOrder;
using Trials.Kevin.Contract.Response;
using Trials.Kevin.IRepository;
using Trials.Kevin.IService.SaleOrderService;
using Trials.Kevin.Model;
using Trials.Kevin.Model.SaleOrderDB;
using Trials.Kevin.Repository.UnitOfWork;

namespace Trials.Kevin.Service.SaleOrderService
{
    public class SaleOrderDetailService : ISaleOrderDetailService
    {
        private readonly IUnitOfWork<SaleOrderContext> _unitOfWork;
        private readonly IBaseRepository<SaleOrderEntity> _repository;
        private readonly IBaseRepository<SaleOrderDetailEntity> _repositoryDetail;
        private readonly IMapper _mapper;
        private readonly ILogger<SaleOrderDetailService> _logger;

        public SaleOrderDetailService(IUnitOfWork<SaleOrderContext> unitOfWork,
            IBaseRepository<SaleOrderEntity> repository,
            IBaseRepository<SaleOrderDetailEntity> repositoryDetail,
            IMapper mapper,
            ILogger<SaleOrderDetailService> logger)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _repositoryDetail = repositoryDetail;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BaseResponse<long>> AddModelAsync(SaleOrderDetailDto dto, CancellationToken cancellationToken)
        {
            //验证订单号是否存在
            SaleOrderEntity saleOrderEntity = await _repository.GetModelAsync(p => p.OrderNo == dto.OrderNo && p.Id == dto.PId && p.Status != (int)SaleOrderStatusEnum.Obsolete && p.Status != (int)SaleOrderStatusEnum.Finished, cancellationToken);
            if (saleOrderEntity?.Id > 0)
            {
                //验证行号
                SaleOrderDetailEntity saleOrderDetail = await _repositoryDetail.GetModelAsync(p => p.PId == dto.PId && p.ProjectNo == dto.ProjectNo, cancellationToken);
                if (saleOrderDetail?.Id > 0)
                {
                    _logger.LogInformation($"添加订单明细失败，销售订单号:{dto.OrderNo}中行号：{dto.ProjectNo}已经存在");
                    return new BaseResponse<long>() { Code = (int)HttpStatusCode.NoContent, IsSuccess = false, Message = "销售订单行号重复" };
                }

                SaleOrderDetailEntity addSaleOrderDetailEntity = _mapper.Map<SaleOrderDetailEntity>(dto);
                EntityEntry<SaleOrderDetailEntity> result = await _repositoryDetail.AddAsync(addSaleOrderDetailEntity, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                if (result.Entity?.Id > 0)
                {
                    return new BaseResponse<long> { Data = result.Entity.Id };
                }
                return new BaseResponse<long> { Code = (int)HttpStatusCode.InternalServerError, IsSuccess = false, Message = "新增失败" };

            }
            else
            {
                _logger.LogInformation($"添加订单明细失败，销售订单号:{dto.OrderNo}和pid:{dto.PId}不存在，或者状态不可添加");
                return new BaseResponse<long>() { Code = (int)HttpStatusCode.NoContent, IsSuccess = false, Message = "销售订单数据不存在" };
            }
        }

        public async Task<BaseResponse> DeleteModelAsync(long id, CancellationToken cancellationToken)
        {
            //验证id是否存在
            SaleOrderDetailEntity saleOrderDetailEntity = await _repositoryDetail.GetModelAsync(p => p.Id == id, cancellationToken);
            if (saleOrderDetailEntity == null || saleOrderDetailEntity.Id <= 0)
            {
                _logger.LogInformation($"删除订单明细失败，明细id为:{id}不存在");
                return new BaseResponse<long> { IsSuccess = false, Code = (int)HttpStatusCode.NoContent, Message = "数据不存在" };
            }
            //验证销售订单号是否存在
            SaleOrderEntity saleOrderEntity = await _repository.GetModelAsync(p => p.Id == saleOrderDetailEntity.PId && p.Status != (int)SaleOrderStatusEnum.Obsolete && p.Status != (int)SaleOrderStatusEnum.Finished, cancellationToken);
            if (saleOrderEntity == null || saleOrderEntity.Id <= 0)
            {
                _logger.LogInformation($"删除订单明细失败，销售订单号:{saleOrderDetailEntity.OrderNo}不存在，或者状态不可删除");
                return new BaseResponse<long>() { Code = (int)HttpStatusCode.NoContent, IsSuccess = false, Message = "销售订单数据不存在" };
            }

            await _repositoryDetail.UpdateAsync(p => p.Id == id, p => new SaleOrderDetailEntity { IsDeleted = (int)LogicDeleteEnum.Yes }, cancellationToken);
            int executeCount = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponse();
        }

        public async Task<SaleOrderDetailQueryDto> GetEntitiesAsync(SaleOrderDetailQueryRequest request, CancellationToken cancellationToken)
        {
            Expression<Func<SaleOrderDetailEntity, bool>> expressionLambda = p => string.IsNullOrEmpty(request.OrderNo) || p.OrderNo.Contains(request.OrderNo);

            (List<SaleOrderDetailEntity> entities, int totalCount) tuple = await _repositoryDetail.GetListPageAsync(request.PageSize, request.PageIndex, expressionLambda, request.SortFields, cancellationToken);

            return new SaleOrderDetailQueryDto { SaleOrderDetails = _mapper.Map<List<SaleOrderDetailEntity>, List<SaleOrderDetailDto>>(tuple.entities), TotalCount = tuple.totalCount };
        }

        public async Task<SaleOrderDetailDto> GetModelAsync(long id, CancellationToken cancellationToken)
        {
            SaleOrderDetailEntity saleOrderDetailEntity = await _repositoryDetail.GetModelAsync(p => p.Id == id, cancellationToken);
            return _mapper.Map<SaleOrderDetailEntity, SaleOrderDetailDto>(saleOrderDetailEntity);
        }

        public async Task<BaseResponse> UpdateModelAsync(SaleOrderDetailDto dto, CancellationToken cancellationToken)
        {
            //验证id是否存在
            SaleOrderDetailEntity saleOrderDetailEntity = await _repositoryDetail.GetModelAsync(p => p.Id == dto.Id, cancellationToken);

            if (saleOrderDetailEntity == null || saleOrderDetailEntity.Id <= 0)
            {
                _logger.LogInformation($"删除订单明细失败，明细id为:{dto.Id}不存在");
                return new BaseResponse { IsSuccess = false, Code = (int)HttpStatusCode.NoContent, Message = "数据不存在" };
            }

            //验证销售订单号是否存在
            SaleOrderEntity saleOrderEntity = await _repository.GetModelAsync(p => p.Id == saleOrderDetailEntity.PId && p.Status != (int)SaleOrderStatusEnum.Obsolete && p.Status != (int)SaleOrderStatusEnum.Finished, cancellationToken);
            if (saleOrderEntity == null || saleOrderEntity.Id <= 0)
            {
                _logger.LogInformation($"删除订单明细失败，销售订单号:{saleOrderDetailEntity.OrderNo}不存在，或者状态不可删除");
                return new BaseResponse() { Code = (int)HttpStatusCode.NoContent, IsSuccess = false, Message = "销售订单数据不存在" };
            }
            var result = await _repositoryDetail.UpdateAsync(p => p.Id == dto.Id, p => new SaleOrderDetailEntity
            {
                MaterialNo = dto.MaterialNo,
                Num = dto.Num,
                Remark = dto.Remark,
                SortNo = dto.SortNo,
                Unit = dto.Unit
            }, cancellationToken);
            if (result != null)
            {
                int executeCount = await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return new BaseResponse();
        }
    }
}
