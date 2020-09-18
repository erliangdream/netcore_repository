using System;
using System.Collections.Generic;
using System.Text;

namespace Trials.Kevin.Contract.Request.SaleOrder
{
    public class SaleOrderDetailDto : IBaseUserDto
    {
        public long Id { get; set; }

        public long PId { get; set; }

        /// <summary>
        /// 销售订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 行项目号
        /// </summary>
        public int ProjectNo { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        public string MaterialNo { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double Num { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNo { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        public string CreateUserNo { get; set; }
        public DateTime CreateTime { get; set; }
        public string UpdateUserNo { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
