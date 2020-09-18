using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
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
    public class SaleOrderDetailController : ApiControllerBase
    {
        private readonly ILogger<SaleOrderDetailController> _logger;
        private readonly UserModel _userModel;
        private readonly ISaleOrderDetailService _saleOrderDetailService;

        public SaleOrderDetailController(ILogger<SaleOrderDetailController> logger,
            UserModel userModel,
            ISaleOrderDetailService saleOrderDetailService
            )
        {
            _logger = logger;
            _userModel = userModel;
            _saleOrderDetailService = saleOrderDetailService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaleOrderDetailDto saleOrderDetailDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"用户{_userModel.UserNo}开始进入保存销售订单，保存数据：{JsonSerializer.Serialize(saleOrderDetailDto)}");
            if (saleOrderDetailDto == null)
            {
                _logger.LogInformation($"用户{_userModel.UserNo}保存销售订单，请求参数为null");
                return await Task.FromResult(Ok(new BaseResponse() { Code = (int)HttpStatusCode.BadRequest, IsSuccess = false, Message = "请求有误" }));
            }
            BaseResponse response = ValidDto(saleOrderDetailDto);
            if (response.IsSuccess == false)
            {
                _logger.LogInformation($"用户{_userModel.UserNo}保存销售订单明细，验证dto没通过:{response.Message}");
                return await Task.FromResult(Ok(response));
            }

            BaseResponse<long> addResposne = await _saleOrderDetailService.AddModelAsync(saleOrderDetailDto, cancellationToken);

            return await Task.FromResult(Ok(addResposne));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"用户{_userModel.UserNo}开始进入删除销售订单明细，请求id为：{id}");

            BaseResponse response = await _saleOrderDetailService.DeleteModelAsync(id, cancellationToken);

            return await Task.FromResult(Ok(response));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] SaleOrderDetailDto saleOrderDetailDto, CancellationToken cancellationToken)
        {
            saleOrderDetailDto.Id = id;
            _logger.LogInformation($"用户{_userModel.UserNo}开始进入编辑销售订单明细，编辑数据：{JsonSerializer.Serialize(saleOrderDetailDto)}");

            BaseResponse response = await _saleOrderDetailService.UpdateModelAsync(saleOrderDetailDto, cancellationToken);

            return await Task.FromResult(Ok(response));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromBody] SaleOrderDetailQueryRequest request, CancellationToken cancellationToken)
        {
            SaleOrderDetailQueryDto response = await _saleOrderDetailService.GetEntitiesAsync(request, cancellationToken);
            return await Task.FromResult(Ok(response));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        {
            SaleOrderDetailDto response = await _saleOrderDetailService.GetModelAsync(id, cancellationToken);
            return await Task.FromResult(Ok(response));
        }
    }
}
