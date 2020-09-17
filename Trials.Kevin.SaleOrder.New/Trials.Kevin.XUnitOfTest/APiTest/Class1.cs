using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Trials.Kevin.IRepository;
using Trials.Kevin.IService.SaleOrderService;
using Trials.Kevin.Repository.SaleOrderRepository;
using Trials.Kevin.Service.SaleOrderService;
using Xunit;
namespace Trials.Kevin.XUnitOfTest.APiTest
{
    [Collection("SaleOrderDataCollection")]
    public class Class1
    {
        private readonly SaleOrderService _saleOrderService;
        private readonly SaleOrderDetailRepository _saleOrderDetailRepository;
        private readonly CancellationTokenSource _cancellationTokenSource;
        public Class1(SaleOrderFixture saleOrderFixture)
        {
            _saleOrderService = saleOrderFixture.testServer.Services.GetService(typeof(ISaleOrderService)) as SaleOrderService;
            _saleOrderDetailRepository = saleOrderFixture.testServer.Services.GetService(typeof(ISaleOrderDetailRepository)) as SaleOrderDetailRepository;
            _cancellationTokenSource = saleOrderFixture.testServer.Services.GetService(typeof(CancellationTokenSource)) as CancellationTokenSource;
        }

        [Fact]
        public async void aa()
        {
            var cc = await _saleOrderService.AddModelAsync(new Contract.Request.SaleOrder.SaleOrderDto
            {
                Customer = "10086",
                Remark = "123",
                Status = 1,
                OrderNo = "001",
            }, _cancellationTokenSource.Token);

            var bb = await _saleOrderDetailRepository.AddAsync(new Model.SaleOrderDB.SaleOrderDetailEntity
            {
                IsDeleted = 0,
                MaterialNo = "111",
                PId = cc.Data
            }, _cancellationTokenSource.Token);

            var aa = await _saleOrderService.UpdateModelAsync(new Contract.Request.SaleOrder.SaleOrderDto
            {
                Id = cc.Data,
                Remark = "update",
                Customer = "aaaaa"
            }, _cancellationTokenSource.Token);

            var dd = await _saleOrderService.GetModelAsync(cc.Data, _cancellationTokenSource.Token);

            var ff = await _saleOrderService.GetEntitiesAsync(new Contract.Request.SaleOrder.SaleOrderQueryRequest { 
                OrderNo= "001"
            } , _cancellationTokenSource.Token);
            //var dd = await _saleOrderService.AddAsync(new Model.SaleOrderDB.SaleOrderEntity
            //{
            //    Customer = "10086",
            //    Remark = "123456",
            //    SignDate = DateTime.Now,
            //    CreateTime = DateTime.Now,
            //    CreateUserNo = "119",
            //    IsDeleted = 1,
            //    Status = 1,
            //    OrderNo = "002",
            //    UpdateTime = DateTime.Now,
            //    UpdateUserNo = "119"
            //}, _cancellationTokenSource.Token);

            //var ee = await _saleOrderService.GetListAsync(p => p.Customer == "10086", _cancellationTokenSource.Token);
        }
    }
}
