using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trials.Kevin.Model.SaleOrderDB
{
    [Table("Kevin.SaleOrder.Order")]
    public class SaleOrderEntity : IDBDelete, IUserInfo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// 销售订单号
        /// </summary>
        [Required]
        [DefaultValue("")]
        [Column(TypeName = "nvarchar(10)")]
        public string OrderNo { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        [Required]
        [DefaultValue("")]
        [Column(TypeName = "nvarchar(10)")]
        public string Customer { get; set; }

        /// <summary>
        /// 签订日期
        /// </summary>
        [Required]
        [DefaultValue(typeof(DateTime), "1900-01-01 00:00:00")]
        public DateTime SignDate { get; set; }

        /// <summary>
        /// 状态 0：待处理 1：处理中  2：作废  3：完成
        /// </summary>
        public int Status { get; set; }

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
        public int IsDeleted { get; set; }


        public virtual ICollection<SaleOrderDetailEntity> SaleOrderDetailEntities { get; set; }
    }
}
