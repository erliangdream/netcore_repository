using System;
using System.Collections.Generic;
using System.Text;

namespace Trials.Kevin.Contract.Request.SaleOrder
{
    public class SaleOrderDetailQueryDto
    {
        public int TotalCount { get; set; }

        public List<SaleOrderDetailDto> SaleOrderDetails { get; set; }
    }
}
