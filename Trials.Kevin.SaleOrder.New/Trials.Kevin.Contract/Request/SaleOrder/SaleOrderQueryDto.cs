using System;
using System.Collections.Generic;
using System.Text;

namespace Trials.Kevin.Contract.Request.SaleOrder
{
    public class SaleOrderQueryDto
    {
        public int TotalCount { get; set; }

        public List<SaleOrderDto> SaleOrders { get; set; }
    }
}
