using System;
using System.Collections.Generic;
using System.Text;

namespace Trials.Kevin.Contract.Request.SaleOrder
{
    public class SaleOrderQueryRequest : BaseQuery
    {
        // <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 签订日期开始
        /// </summary>
        public DateTime? SignDateStart { get; set; }

        /// <summary>
        /// 签订日期结束
        /// </summary>
        public DateTime? SignDateEnd { get; set; }

        /// <summary>
        /// 创建时间开始
        /// </summary>
        public DateTime? CreateTimeStart { get; set; }

        /// <summary>
        /// 创建时间结束
        /// </summary>
        public DateTime? CreateTimeEnd { get; set; }

        /// <summary>
        /// 更新时间开始
        /// </summary>
        public DateTime? UpdateTimeStart { get; set; }

        /// <summary>
        /// 更新时间结束
        /// </summary>
        public DateTime? UpdateTimeEnd { get; set; }

        /// <summary>
        /// 状态 0：待处理 1：处理中  2：作废  3：完成
        /// </summary>
        public int[] Status { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public Dictionary<string, string> SortFields { get; set; } = new Dictionary<string, string> { { "CreateTime", "desc" } };
    }
}
