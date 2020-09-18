using System;
using System.Collections.Generic;
using System.Text;

namespace Trials.Kevin.Model
{
    public interface IUserInfo
    {
        string CreateUserNo { get; set; }

        DateTime CreateTime { get; set; }

        string UpdateUserNo { get; set; }

        DateTime UpdateTime { get; set; }
    }
}
