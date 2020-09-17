using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Trials.Kevin.Contract.IOC;
using Trials.Kevin.Contract.Request.SaleOrder;
using Trials.Kevin.Contract.Response;
using Trials.Kevin.IService.SaleOrderService;

namespace Trials.Kevin.SaleOrder.New.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleOrderController : ApiControllerBase
    {
        private readonly ILogger<SaleOrderController> _logger;
        private readonly IMapper _mapper;
        private readonly UserModel _userModel;
        private readonly ISaleOrderService _saleOrderService;
        private readonly ISaleOrderDetailService _saleOrderDetailService;

        public SaleOrderController(ILogger<SaleOrderController> logger,
            IMapper mapper,
            UserModel userModel,
            ISaleOrderService saleOrderService,
            ISaleOrderDetailService saleOrderDetailService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _userModel = userModel;
            _saleOrderService = saleOrderService;
            _saleOrderDetailService = saleOrderDetailService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaleOrderDto saleOrderDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"用户{_userModel.UserNo}开始进入保存销售订单，保存数据：{JsonSerializer.Serialize(saleOrderDto)}");
            if (saleOrderDto == null)
            {
                _logger.LogInformation($"用户{_userModel.UserNo}保存销售订单，请求参数为null");
                return await Task.FromResult(Ok(new BaseResponse() { Code = (int)HttpStatusCode.BadRequest, IsSuccess = false, Message = "请求有误" }));
            }
            BaseResponse response = ValidDto(saleOrderDto);
            if (response.IsSuccess == false)
            {
                _logger.LogInformation($"用户{_userModel.UserNo}保存销售订单，验证dto没通过:{response.Message}");
                return await Task.FromResult(Ok(response));
            }

            BaseResponse<long> addResposne = await _saleOrderService.AddModelAsync(saleOrderDto, cancellationToken);

            return await Task.FromResult(Ok(addResposne));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"用户{_userModel.UserNo}开始进入删除销售订单，请求id为：{id}");

            BaseResponse response = await _saleOrderService.DeleteModelAsync(id, cancellationToken);

            return await Task.FromResult(Ok(response));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] SaleOrderDto saleOrderDto, CancellationToken cancellationToken)
        {
            saleOrderDto.Id = id;
            _logger.LogInformation($"用户{_userModel.UserNo}开始进入编辑销售订单，编辑数据：{JsonSerializer.Serialize(saleOrderDto)}");

            BaseResponse response = await _saleOrderService.UpdateModelAsync(saleOrderDto, cancellationToken);

            return await Task.FromResult(Ok(response));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromBody] SaleOrderQueryRequest request, CancellationToken cancellationToken)
        {
            SaleOrderQueryDto response = await _saleOrderService.GetEntitiesAsync(request, cancellationToken);
            return await Task.FromResult(Ok(response));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        {
            SaleOrderDto response = await _saleOrderService.GetModelAsync(id, cancellationToken);
            return await Task.FromResult(Ok(response));
        }
    }
}
