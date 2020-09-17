using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
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
        private readonly IBaseRepository<SaleOrderDetailEntity> _repositoryDetial;
        private readonly IMapper _mapper;
        private readonly ILogger<SaleOrderDetailService> _logger;

        public SaleOrderDetailService(IUnitOfWork<SaleOrderContext> unitOfWork,
            IBaseRepository<SaleOrderEntity> repository,
            IBaseRepository<SaleOrderDetailEntity> repositoryDetial,
            IMapper mapper,
            ILogger<SaleOrderDetailService> logger)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _repositoryDetial = repositoryDetial;
            _mapper = mapper;
            _logger = logger;
        }
    }
}
