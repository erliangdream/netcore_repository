using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Trials.Kevin.Contract.Enum
{
    /// <summary>
    /// 销售订单处理状态
    /// </summary>
    public enum SaleOrderStatusEnum
    {
        //0：待处理 1：处理中  2：作废  3：完成
        /// <summary>
        /// 待处理
        /// </summary>
        [Description("待处理")]
        Pending = 0,

        /// <summary>
        /// 处理中
        /// </summary>
        [Description("处理中")]
        Processing = 1,

        // <summary>
        /// 作废
        /// </summary>
        [Description("作废")]
        Obsolete = 2,

        /// <summary>
        /// 处理中
        /// </summary>
        [Description("完成")]
        Finished = 3
    }
}
