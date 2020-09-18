using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using Trials.Kevin.Model.SaleOrderDB;
using Trials.Kevin.Contract.Enum;
using Microsoft.EntityFrameworkCore.Metadata;
using Trials.Kevin.Contract.IOC;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.Configuration.Annotations;
using System.Threading.Tasks;
using System.Threading;

namespace Trials.Kevin.Model
{
    /// <summary>
    /// 销售订单模块Mysql上下文
    /// </summary>
    public class SaleOrderContext : DbContext
    {
        private readonly UserModel _userModel;

        public SaleOrderContext(DbContextOptions<SaleOrderContext> options, UserModel userModel) : base(options) {
            _userModel = userModel;
        }

        public DbSet<SaleOrderEntity> SaleOrders { get; set; }

        public DbSet<SaleOrderDetailEntity> SaleOrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<SaleOrderEntity>().HasQueryFilter(p => p.IsDeleted == (int)LogicDeleteEnum.No);

            //modelBuilder.Entity<SaleOrderDetailEntity>().HasQueryFilter(p => p.IsDeleted == (int)LogicDeleteEnum.No);

            //var tt = modelBuilder.Model.GetEntityTypes();
            //var ttt = modelBuilder.Model.GetEntityTypes().Where(p => typeof(IDBDelete).IsAssignableFrom(p.ClrType));

            foreach (var item in modelBuilder.Model.GetEntityTypes().Where(p => typeof(IDBDelete).IsAssignableFrom(p.ClrType)))
            {
                //申明一个dbModel的变量
                ParameterExpression pe = Expression.Parameter(item.ClrType, "p");
                //获取IsDeleted属性
                MemberExpression me = Expression.Property(pe, nameof(IDBDelete.IsDeleted));
                //创建一个常量
                ConstantExpression ce = Expression.Constant((int)LogicDeleteEnum.No, typeof(int));
                //生成一个p.IsDeleted == (int)LogicDeleteEnum.No
                BinaryExpression be = Expression.Equal(me, ce);
                var lambda = Expression.Lambda(be, pe);
                modelBuilder.Entity(item.ClrType).HasQueryFilter(lambda);
            }
            base.OnModelCreating(modelBuilder);

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Added && typeof(IUserInfo).IsAssignableFrom(e.Entity.GetType())))
            {
                ((IUserInfo)item.Entity).CreateTime = DateTime.Now;
                ((IUserInfo)item.Entity).UpdateTime = DateTime.Now;
                ((IUserInfo)item.Entity).CreateUserNo = _userModel.UserName;
                ((IUserInfo)item.Entity).UpdateUserNo = _userModel.UserName;
            }

            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && typeof(IUserInfo).IsAssignableFrom(e.Entity.GetType())))
            {
                ((IUserInfo)item.Entity).UpdateTime = DateTime.Now;
                ((IUserInfo)item.Entity).UpdateUserNo = _userModel.UserName;
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
