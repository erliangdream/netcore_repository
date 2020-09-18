using System;
using System.Collections.Generic;
using System.Text;

namespace Trials.Kevin.Contract.Request
{
    public class BaseQuery
    {
        public int PageSize => 10;

        public int PageIndex => 1;

        /// <summary>
        /// 排序字段
        /// </summary>
        public Dictionary<string, string> SortFields { get; set; } = new Dictionary<string, string> { { "CreateTime", "desc" } };
    }
}
