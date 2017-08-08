using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Life.Model.LifeMan
{
    /// <summary>
    /// 文章类别查询视图
    /// </summary>
    [Table("V_Art_Category")]
    public class VArtCategory
    {
        #region 数据库对应的属性
        /// <summary>
        /// 类型编号
        /// </summary>        
        [Column("Cat_Id")]
        [Key]
        public String CatId { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>        
        [Column("Cat_Name")]
        public String CatName { get; set; }
        /// <summary>
        /// 类型备注
        /// </summary>        
        [Column("Cat_Remark")]
        public String CatRemark { get; set; }
        /// <summary>
        /// 排序
        /// </summary>        
        [Column("Cat_Order")]
        public int CatOrder { get; set; }
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
        public DateTime UpdateTime { get; set; }
        #endregion

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
    }
}
