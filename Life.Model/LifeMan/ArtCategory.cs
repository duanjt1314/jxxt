/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 段江涛
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 文章类型表实体
 * *        Remark      : 2013-07-30 段江涛 创建
 * *
 * *  Copyright (c) SuHui Corporation.  All rights reserved. 
 * ***************************************************************************************/
using System;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Life.Model.LifeMan
{
    ///<summary>
    ///文章类型表实体
    ///</summary>
    ///<seealso cref="Object"/>
    ///<seealso cref="System"/>
    ///<remarks>
    ///<list type="bullet">
    ///<item><description>文章类型表实体</description></item>
    ///<item><description>直接访问数据库相关数据，不作业务处理</description></item> 
    ///</list>
    /// </remarks>
    /// <example>
    /// <para>演示如何创建类的实例</para>
    /// <code>
    /// using System;
    /// using Netatomy.Learning;
    ///
    /// public class Program
    /// {
    ///     public static void Main(String[] args)
    ///     {
    ///         ArtCategory index = new ArtCategory();         
    ///     }
    /// }
    /// </code>
    /// </example>
    [Table("Art_Category")]
    public class ArtCategory
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
        
        #region 自定义属性
        #endregion
    }
}

