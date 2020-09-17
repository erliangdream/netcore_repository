using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Trials.Kevin.Contract.Request.SaleOrder;
using Trials.Kevin.Model.SaleOrderDB;

namespace Trials.Kevin.Common.Profile
{
    public class SaleOrderProfile : AutoMapper.Profile
    {
        public SaleOrderProfile()
        {
            CreateMap<SaleOrderDto, SaleOrderEntity>();
            CreateMap<SaleOrderDetailDto, SaleOrderDetailEntity>();
        }
    }
}
