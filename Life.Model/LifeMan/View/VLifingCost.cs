using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Life.Model.LifeMan
{
    [Table("V_Lifing_Cost")]
    public class VLifingCost
    {
        #region 数据库对应的属性
        /// <summary>
        /// 编号
        /// </summary>        
        [Column("Id")]
        [Key]
        public String Id { get; set; }
        /// <summary>
        /// 消费时间
        /// </summary>        
        [Column("TIME")]
        public DateTime Time { get; set; }
        /// <summary>
        /// 消费名称
        /// </summary>        
        [Column("Reason")]
        public String Reason { get; set; }
        /// <summary>
        /// 消费金额
        /// </summary>        
        [Column("Price")]
        public double Price { get; set; }
        /// <summary>
        /// 消费类型
        /// </summary>        
        [Column("Cost_Type_Id")]
        //[ForeignKey("diction")]
        public decimal CostTypeId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>        
        [Column("Notes")]
        public String Notes { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        [Column("Img_Url")]
        public String ImgUrl { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>        
        [Column("Create_By")]
        public String CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>        
        [Column("Create_Time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改者
        /// </summary>        
        [Column("UpDate_By")]
        public String UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>        
        [Column("UpDate_Time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 特殊标识
        /// </summary>        
        [Column("IsMark")]
        public Boolean IsMark { get; set; }

        /// <summary>
        /// 家庭内部支出,如把钱给家庭成员的某个人
        /// </summary>        
        [Column("FamilyPay")]
        public Boolean FamilyPay { get; set; }

        /// <summary>
        /// 自定义分组
        /// </summary>        
        [Column("CusGroup")]
        public String CusGroup { get; set; }

        #endregion

        #region 自定义属性
        /// <summary>
        /// 消费类型名称
        /// </summary>
        [Column("Cost_Type_Name")]
        public String CostTypeName { get; set; }

        /// <summary>
        /// 创建者名称
        /// </summary>
       [Column("Create_Name")]
        public String CreateName { get; set; }

        /// <summary>
        /// 修改者名称
        /// </summary>
        [Column("Update_Name")]
        public String UpdateName { get; set; }
        #endregion
    }
}
