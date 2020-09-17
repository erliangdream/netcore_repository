using System;
using System.Collections.Generic;
using System.Text;

namespace Trials.Kevin.Contract.Request
{
    public class BaseQuery
    {
        public int PageSize => 10;

        public int PageIndex => 1;
    }
}
