using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Life.Model.LifeMan
{
    [Table("V_BANK_CARD")]
    public class VBankCard
    {
        #region 数据库对应的属性
        /// <summary>
        /// 编号
        /// </summary>        
        [Column("Id")]
        [Key]
        public String Id { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>        
        [Column("TIME")]
        public DateTime Time { get; set; }
        /// <summary>
        /// 操作金额
        /// </summary>        
        [Column("Price")]
        public double Price { get; set; }
        /// <summary>
        /// 操作类型 操作类型 表示存入还是取出，从字典表中获取
        /// </summary>        
        [Column("Save_Type")]
        public decimal SaveType { get; set; }
        /// <summary>
        /// 余额
        /// </summary>        
        [Column("Balance")]
        public double Balance { get; set; }
        /// <summary>
        /// 银行卡名称 银行卡名称 表示操作的银行卡的名称，从字典表中获得
        /// </summary>        
        [Column("Bank_Type")]
        public decimal BankType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>        
        [Column("Note")]
        public String Note { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>        
        [Column("Create_By")]
        public String CreateBy { get; set; }
        /// <summary>
        /// 创建时间 创建时间 系统当前默认时间
        /// </summary>        
        [Column("Create_Time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改者
        /// </summary>        
        [Column("UpDate_By")]
        public String UpdateBy { get; set; }
        /// <summary>
        /// 修改时间 修改时间 修改时候的系统默认时间
        /// </summary>        
        [Column("UpDate_Time")]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 图标地址
        /// </summary>        
        [Column("ImgUrl")]
        public String ImgUrl { get; set; }
        #endregion

        #region 自定义属性
        /// <summary>
        /// 银行卡名称
        /// </summary>
        [Column("Bank_Type_Name")]
        public String BankTypeName { get; set; }

        /// <summary>
        /// 操作名称
        /// </summary>
        [Column("Save_Name")]
        public String SaveName { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [Column("Create_Name")]
        public String CreateName { get; set; }

        /// <summary>
        /// 修改人姓名
        /// </summary>
        [Column("Update_Name")]
        public String UpdateName { get; set; }

        #endregion
    }
}
