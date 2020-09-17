using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;

namespace Trials.Kevin.Model.SaleOrderDB
{
    [Table("Kevin.SaleOrder.OrderDetail")]
    public class SaleOrderDetailEntity : IDBDelete, IUserInfo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long PId { get; set; }

        [ForeignKey("PId")]
        public virtual SaleOrderEntity OrderEntity { get; set; }

        /// <summary>
        /// 销售订单号
        /// </summary>
        [Required]
        [DefaultValue("")]
        [Column(TypeName = "nvarchar(10)")]
        public string OrderNo { get; set; }

        /// <summary>
        /// 行项目号
        /// </summary>
        public int ProjectNo { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        [Required]
        [DefaultValue("")]
        [Column(TypeName = "nvarchar(10)")]
        public string MaterialNo { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public double Num { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Column(TypeName = "nvarchar(10)")]
        public string Unit { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNo { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        [Column(TypeName = "nvarchar(2000)")]
        public string Remark { get; set; }

        /// <summary>
        /// 创建者编号
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string CreateUserNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者编号
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string UpdateUserNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime UpdateTime { get; set; }


        /// <summary>
        /// 是否逻辑删除  0：否  1：是
        /// </summary>
        public int IsDeleted { get; set; } = 0;
    }
}
