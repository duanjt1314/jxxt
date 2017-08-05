using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Life.Model.LifeMan
{
    public class VPurchase
    {
        #region 数据库对应的属性
        /// <summary>
        /// 编号
        /// </summary>        
        [Column("Id")]
        [Key]
        public String Id { get; set; }
        /// <summary>
        /// 购买时间
        /// </summary>        
        [Column("Pur_Time")]
        public DateTime PurTime { get; set; }
        /// <summary>
        /// 数量(斤) 购买数量
        /// </summary>        
        [Column("Number")]
        public double Number { get; set; }
        /// <summary>
        /// 单价
        /// </summary>        
        [Column("Unit_Price")]
        public double UnitPrice { get; set; }
        /// <summary>
        /// 总价
        /// </summary>        
        [Column("Total_Price")]
        public double TotalPrice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>        
        [Column("Note")]
        public String Note { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>        
        [Column("Create_By")]
        public String CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>        
        [Column("Create_Time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>        
        [Column("UpDate_By")]
        public String UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>        
        [Column("UpDate_Time")]
        public DateTime? UpdateTime { get; set; }
        #endregion

        #region 自定义属性
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
