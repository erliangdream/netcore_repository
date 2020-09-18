using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Trials.Kevin.IRepository
{
    /// <summary>
    /// DB工作单元接口
    /// </summary>
    public interface IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        /// <summary>
        /// 保存操作
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
