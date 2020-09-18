using System;
using System.Collections.Generic;
using System.Text;

namespace Trials.Kevin.Contract.Request.SaleOrder
{
    public class SaleOrderDetailQueryRequest : BaseQuery
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
    }
}
