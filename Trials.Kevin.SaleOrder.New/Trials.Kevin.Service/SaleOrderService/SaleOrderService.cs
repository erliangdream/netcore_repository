using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SaleOrderService : ISaleOrderService
    {
        private readonly IUnitOfWork<SaleOrderContext> _unitOfWork;
        private readonly IBaseRepository<SaleOrderEntity> _repository;
        private readonly IBaseRepository<SaleOrderDetailEntity> _repositoryDetial;
        private readonly IMapper _mapper;
        private readonly ILogger<SaleOrderService> _logger;

        public SaleOrderService(IUnitOfWork<SaleOrderContext> unitOfWork,
            IBaseRepository<SaleOrderEntity> repository,
            IBaseRepository<SaleOrderDetailEntity> repositoryDetial,
            IMapper mapper,
            ILogger<SaleOrderService> logger)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _repositoryDetial = repositoryDetial;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BaseResponse<long>> AddModelAsync(SaleOrderDto dto, CancellationToken cancellationToken)
        {
            //验证订单号是否存在
            SaleOrderEntity saleOrderEntity = await _repository.GetModelAsync(p => p.OrderNo == dto.OrderNo, cancellationToken);
            if (saleOrderEntity?.Id > 0)
            {
                _logger.LogInformation($"新增订单时,订单号{dto.OrderNo}已经存在");
                return new BaseResponse<long> { IsSuccess = false, Code = (int)HttpStatusCode.Conflict, Message = "订单号已存在" };
            }
            SaleOrderEntity addSaleOrderEntity = _mapper.Map<SaleOrderEntity>(dto);

            EntityEntry<SaleOrderEntity> result = await _repository.AddAsync(addSaleOrderEntity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (result.Entity?.Id > 0)
            {
                return new BaseResponse<long> { Data = result.Entity.Id };
            }
            return new BaseResponse<long> { Code = (int)HttpStatusCode.InternalServerError, IsSuccess = false, Message = "新增失败" };
        }

        public async Task<BaseResponse> DeleteModelAsync(long id, CancellationToken cancellationToken)
        {
            //验证id是否存在
            SaleOrderEntity saleOrderEntity = await _repository.GetModelAsync(p => p.Id == id && p.Status == (int)SaleOrderStatusEnum.Pending, cancellationToken);
            if (saleOrderEntity == null || saleOrderEntity.Id <= 0)
            {
                _logger.LogInformation($"删除订单时,id:{id}不存在或者状态不可删除");
                return new BaseResponse<long> { IsSuccess = false, Code = (int)HttpStatusCode.NoContent, Message = "数据不存在" };
            }
            await _repository.UpdateAsync(p => p.Id == id && p.Status == (int)SaleOrderStatusEnum.Pending, p => new SaleOrderEntity { IsDeleted = (int)LogicDeleteEnum.Yes }, cancellationToken);
            await _repositoryDetial.UpdateAsync(p => p.PId == id, p => new SaleOrderDetailEntity { IsDeleted = (int)LogicDeleteEnum.Yes }, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponse();
        }

        public async Task<SaleOrderQueryDto> GetEntitiesAsync(SaleOrderQueryRequest request, CancellationToken cancellationToken)
        {
            Expression<Func<SaleOrderEntity, bool>> expressionLambda = p => (string.IsNullOrEmpty(request.OrderNo) || p.OrderNo.Contains(request.OrderNo))
                   && (string.IsNullOrEmpty(request.Customer) || p.Customer.Contains(request.Customer))
                   && (string.IsNullOrEmpty(request.Remark) || p.Remark.Contains(request.Remark))
                   && (request.CreateTimeStart == null || p.CreateTime >= request.CreateTimeStart)
                   && (request.CreateTimeEnd == null || p.CreateTime <= request.CreateTimeEnd)
                   && (request.SignDateStart == null || p.SignDate >= request.SignDateStart)
                   && (request.SignDateEnd == null || p.SignDate <= request.SignDateEnd)
                   && (request.UpdateTimeStart == null || p.UpdateTime >= request.UpdateTimeStart)
                   && (request.UpdateTimeEnd == null || p.UpdateTime <= request.UpdateTimeEnd)
                   && (request.Status == null || request.Status.Length == 0 || request.Status.Contains(p.Status));

            (List<SaleOrderEntity> entities, int totalCount) tuple = await _repository.GetListPageAsync(request.PageSize, request.PageIndex, expressionLambda, request.SortFields, cancellationToken);

            return new SaleOrderQueryDto { SaleOrders = _mapper.Map<List<SaleOrderEntity>, List<SaleOrderDto>>(tuple.entities), TotalCount = tuple.totalCount };

        }

        public async Task<SaleOrderDto> GetModelAsync(long id, CancellationToken cancellationToken)
        {
            SaleOrderEntity saleOrderEntity = await _repository.GetModelAsync(p => p.Id == id, cancellationToken);
            if (saleOrderEntity?.Id > 0)
            {
                List<SaleOrderDetailEntity> saleOrderDetailEntities = await _repositoryDetial.GetListAsync(p => p.PId == id, cancellationToken);
                saleOrderEntity.SaleOrderDetailEntities = saleOrderDetailEntities;
            }
            return _mapper.Map<SaleOrderEntity, SaleOrderDto>(saleOrderEntity);
        }


        public async Task<BaseResponse> UpdateModelAsync(SaleOrderDto dto, CancellationToken cancellationToken)
        {
            SaleOrderEntity saleOrderEntity = await _repository.GetModelAsync(p => p.Id == dto.Id && p.Status != (int)SaleOrderStatusEnum.Obsolete && p.Status != (int)SaleOrderStatusEnum.Finished, cancellationToken);

            if (saleOrderEntity == null || saleOrderEntity.Id <= 0)
            {
                return new BaseResponse() { Code = (int)HttpStatusCode.NoContent, IsSuccess = false, Message = "数据不存在" };
            }

            int executeCount = await _repository.UpdateAsync(p => p.Id == dto.Id, p => new SaleOrderEntity
            {
                Status = dto.Status,
                Remark = dto.Remark,
                SignDate = dto.SignDate
            }, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponse();

        }
    }
}
