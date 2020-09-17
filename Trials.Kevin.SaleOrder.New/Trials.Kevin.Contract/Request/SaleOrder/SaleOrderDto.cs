using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Trials.Kevin.Contract.Attr;

namespace Trials.Kevin.Contract.Request.SaleOrder
{
    public class SaleOrderDto : IBaseUserDto
    {
        public long Id { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [DtoEmpty("订单号必填")]
        public string OrderNo { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        [DtoEmpty("客户编号必填")]
        public string Customer { get; set; }

        /// <summary>
        /// 签订日期
        /// </summary>
        public DateTime SignDate { get; set; }

        /// <summary>
        /// 状态 0：待处理 1：处理中  2：作废  3：完成
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        public string CreateUserNo { get; set; }
        public DateTime CreateTime { get; set; }
        public string UpdateUserNo { get; set; }
        public DateTime UpdateTime { get; set; }

        //public ICollection<SaleOrderDetailEntity> SaleOrderDetailEntities { get; set; }
    }
}
