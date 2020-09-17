using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Trials.Kevin.Contract.Enum
{
    /// <summary>
    /// 逻辑删除
    /// </summary>
    public enum LogicDeleteEnum
    {
        /// <summary>
        /// 否
        /// </summary>
        [Description("否")]
        No = 0,

        /// <summary>
        /// 是
        /// </summary>
        [Description("是")]
        Yes = 1
    }
}
