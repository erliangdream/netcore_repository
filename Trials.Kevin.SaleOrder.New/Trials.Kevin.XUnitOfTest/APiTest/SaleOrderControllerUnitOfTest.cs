using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using Trials.Kevin.Model.SaleOrderDB;
using Trials.Kevin.Contract.Response;
using Trials.Kevin.Model;
using Trials.Kevin.IService.SaleOrderService;
using Trials.Kevin.Service.SaleOrderService;
using Trials.Kevin.SaleOrder.New.Host.Controllers;
using System.Threading;
using Trials.Kevin.Contract.Request.SaleOrder;
using System.Linq;

namespace Trials.Kevin.XUnitOfTest
{
    [Collection("SaleOrderDataCollection")]
    public class SaleOrderControllerUnitOfTest
    {
        SaleOrderController _saleOrderController;
        SaleOrderDetailController _saleOrderDetailController;
        SaleOrderService _saleOrderService;
        SaleOrderDetailService _saleOrderDetailService;
        CancellationTokenSource _cancellationTokenSource;
        public SaleOrderControllerUnitOfTest(SaleOrderFixture saleOrderFixture)
        {
            _saleOrderController = saleOrderFixture.testServer.Services.GetService(typeof(SaleOrderController)) as SaleOrderController;
            _saleOrderDetailController = saleOrderFixture.testServer.Services.GetService(typeof(SaleOrderDetailController)) as SaleOrderDetailController;
            _saleOrderService = saleOrderFixture.testServer.Services.GetService(typeof(ISaleOrderService)) as SaleOrderService;
            _saleOrderDetailService = saleOrderFixture.testServer.Services.GetService(typeof(ISaleOrderDetailService)) as SaleOrderDetailService;
            _cancellationTokenSource = saleOrderFixture.testServer.Services.GetService(typeof(CancellationTokenSource)) as CancellationTokenSource;
        }

        [Fact]
        [Trait("SaleOrder", "Edit")]
        public async void PostSaleOrder()
        {
            var postResult = await _saleOrderController.Post(new SaleOrderDto
            {
                OrderNo = "SO10086",
                Customer = "test-Post",
                Remark = "test-remark-Post",
                SignDate = DateTime.Now
            }, _cancellationTokenSource.Token);
            long id = ((postResult as OkObjectResult).Value as BaseResponse<long>).Data;

            SaleOrderDto dto = await _saleOrderService.GetModelAsync(id, _cancellationTokenSource.Token);

            Assert.True(dto.OrderNo == "SO10086");
        }

        [Theory]
        [InlineData(1, 1)]
        [Trait("SaleOrder", "Get")]
        public async void GetSaleOrderList(int pageSize, int pageIndex)
        {
            var postResult1 = await _saleOrderService.AddModelAsync(new SaleOrderDto
            {
                OrderNo = "SO10087",
                Customer = "test-Post",
                Remark = "test-remark-Post",
                SignDate = DateTime.Now
            }, _cancellationTokenSource.Token);

            long id1 = postResult1.Data;


            await _saleOrderDetailService.AddModelAsync(new SaleOrderDetailDto
            {
                PId = id1,
                OrderNo = "SO10087",
                MaterialNo = "Mtr10086",
                Num = 1,
                ProjectNo = 1,
                SortNo = 1,
                Unit = "T",
                Remark = "remark"
            }, _cancellationTokenSource.Token);

            await _saleOrderDetailService.AddModelAsync(new SaleOrderDetailDto
            {
                PId = id1,
                OrderNo = "SO10087",
                MaterialNo = "Mtr10087",
                Num = 200,
                ProjectNo = 2,
                SortNo = 2,
                Unit = "KG",
                Remark = "remark-2"
            }, _cancellationTokenSource.Token);

            var postResult2 = await _saleOrderService.AddModelAsync(new SaleOrderDto
            {
                OrderNo = "SO10010",
                Customer = "test-Post2",
                Remark = "test-remark-Post2",
                SignDate = DateTime.Now,
                Status = 1
            }, _cancellationTokenSource.Token);

            long id2 = postResult2.Data;

            await _saleOrderDetailService.AddModelAsync(new SaleOrderDetailDto
            {
                PId = id2,
                OrderNo = "SO10010",
                MaterialNo = "Mtr10000",
                Num = 1,
                ProjectNo = 1,
                SortNo = 1,
                Unit = "T",
                Remark = "remark"
            }, _cancellationTokenSource.Token);


            var response = await _saleOrderController.Get(new SaleOrderQueryRequest
            {
                OrderNo = "SO10087",
                PageIndex = pageIndex,
                PageSize = pageSize
            }, _cancellationTokenSource.Token);

            var data = ((response as OkObjectResult).Value as BaseResponse<SaleOrderQueryDto>).Data;

            Assert.True(data.SaleOrders.Exists(p => p.SaleOrderDetailEntities.Any(t => t.MaterialNo == "Mtr10086")));
        }

        [Fact]
        [Trait("SaleOrder", "Edit")]
        public async void PutSaleOrder()
        {
            var postResult = await _saleOrderService.AddModelAsync(new SaleOrderDto
            {
                OrderNo = "SO10088",
                Customer = "test-Post",
                Remark = "test-remark-Post",
                SignDate = DateTime.Now
            }, _cancellationTokenSource.Token);

            long id = postResult.Data;

            var putResult = await _saleOrderController.Put(id, new SaleOrderDto
            {
                Customer = "test-Put",
                Remark = "test-remark-Put",
                SignDate = DateTime.Now.AddDays(1)
            }, _cancellationTokenSource.Token);

            SaleOrderDto dto = await _saleOrderService.GetModelAsync(id, _cancellationTokenSource.Token);

            Assert.True(dto.Customer == "test-Put" && dto.Remark == "test-remark-Put");
        }

        [Fact]
        [Trait("SaleOrder", "Edit")]
        public async void DeleteSaleOrder()
        {
            var postResult1 = await _saleOrderService.AddModelAsync(new SaleOrderDto
            {
                OrderNo = "SO10089",
                Customer = "test-Post",
                Remark = "test-remark-Post",
                SignDate = DateTime.Now
            }, _cancellationTokenSource.Token);

            long id = postResult1.Data;

            await _saleOrderDetailService.AddModelAsync(new SaleOrderDetailDto
            {
                PId = id,
                OrderNo = "SO10089",
                MaterialNo = "Mtr10086",
                Num = 1,
                ProjectNo = 1,
                SortNo = 1,
                Unit = "T",
                Remark = "remark"
            }, _cancellationTokenSource.Token);

            await _saleOrderDetailService.AddModelAsync(new SaleOrderDetailDto
            {
                PId = id,
                OrderNo = "SO10089",
                MaterialNo = "Mtr10087",
                Num = 200,
                ProjectNo = 2,
                SortNo = 2,
                Unit = "KG",
                Remark = "remark-2"
            }, _cancellationTokenSource.Token);

            SaleOrderDto saleOrderDtoBeforeDelete = await _saleOrderService.GetModelAsync(id, _cancellationTokenSource.Token);

            await _saleOrderController.Delete(id, _cancellationTokenSource.Token);

            SaleOrderDto saleOrderDtoAfterDelete = await _saleOrderService.GetModelAsync(id, _cancellationTokenSource.Token);

            SaleOrderDetailQueryDto saleOrderDetailQueryDto = await _saleOrderDetailService.GetEntitiesAsync(new SaleOrderDetailQueryRequest
            {
                OrderNo = "SO10089",
                PageIndex = 0
            }, _cancellationTokenSource.Token);

            Assert.True(saleOrderDtoBeforeDelete.OrderNo == "SO10089" && saleOrderDtoAfterDelete == null && saleOrderDetailQueryDto.TotalCount == 0);
        }
    }
}
