﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Trials.Kevin.IService.SaleOrderService;
using Trials.Kevin.Service.SaleOrderService;
using Xunit;
namespace Trials.Kevin.XUnitOfTest.APiTest
{
    [Collection("SaleOrderDataCollection")]
    public class Class1
    {
        private readonly SaleOrderService _saleOrderService;
        private readonly CancellationTokenSource _cancellationTokenSource;
        public Class1(SaleOrderFixture saleOrderFixture)
        {
            _saleOrderService = saleOrderFixture.testServer.Services.GetService(typeof(ISaleOrderService)) as SaleOrderService;
            _cancellationTokenSource = saleOrderFixture.testServer.Services.GetService(typeof(CancellationTokenSource)) as CancellationTokenSource;
        }

        [Fact]
        public async void aa()
        {
            //var cc = await _saleOrderService.AddAsync(new Model.SaleOrderDB.SaleOrderEntity
            //{
            //    Customer = "10086",
            //    Remark = "123",
            //    SignDate = DateTime.Now,
            //    CreateTime = DateTime.Now,
            //    CreateUserNo = "110",
            //    IsDeleted = 0,
            //    Status = 1,
            //    OrderNo = "001",
            //    UpdateTime = DateTime.Now,
            //    UpdateUserNo = "110"
            //}, _cancellationTokenSource.Token);

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
