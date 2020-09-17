using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Trials.Kevin.IRepository
{
    /// <summary>
    /// DB工作单元接口
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        /// <returns></returns>
        DbContext GetDbContext();

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <returns></returns>
        ValueTask<int> SaveChangesAsync();

    }
}
