using System;
using System.Collections.Generic;
using System.Text;

namespace Trials.Kevin.Contract.Request
{
    public interface IBaseUserDto
    {
        string CreateUserNo { get; set; }

        DateTime CreateTime { get; set; }

        string UpdateUserNo { get; set; }

        DateTime UpdateTime { get; set; }
    }
}
