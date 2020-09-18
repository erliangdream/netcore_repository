using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trials.Kevin.Contract.IOC
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public static class UserService
    {

        public static IServiceCollection AddUserService(this IServiceCollection service)
        {
            return service.AddScoped(factory => new UserModel());
        }
    }
}
