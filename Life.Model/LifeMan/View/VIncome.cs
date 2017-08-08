using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Life.Model.LifeMan
{
    /// <summary>
    /// 纯收入管理视图
    /// </summary>
    [Table("V_Income")]
    public class VIncome
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
        /// 创建时间 创建时间 创建时间 系统当前默认时间
        /// </summary>        
        [Column("Create_Time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改者
        /// </summary>        
        [Column("UpDate_By")]
        public String UpdateBy { get; set; }
        /// <summary>
        /// 修改时间 修改时间 修改时间 修改时候的系统默认时间
        /// </summary>        
        [Column("UpDate_Time")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 特殊标识
        /// </summary>        
        [Column("IsMark")]
        public Boolean IsMark { get; set; }

        /// <summary>
        /// 家庭内部收入，如家庭成员的某个人把钱给自己
        /// </summary>
        [Column("FamilyIncome")]
        public Boolean? FamilyIncome { get; set; }
        #endregion

        #region 自定义属性
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

        /// <summary>
        /// 自定义分组
        /// </summary>
        [Column("CusGroup")]
        public String CusGroup { get; set; }
        #endregion
    }
}
