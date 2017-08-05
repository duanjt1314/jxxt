/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 段江涛
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 字典表实体
 * *        Remark      : 2013-07-30 段江涛 创建
 * *
 * *  Copyright (c) SuHui Corporation.  All rights reserved. 
 * ***************************************************************************************/
using System;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Life.Model.LifeMan
{
    ///<summary>
    ///字典表实体
    ///</summary>
    ///<seealso cref="Object"/>
    ///<seealso cref="System"/>
    ///<remarks>
    ///<list type="bullet">
    ///<item><description>字典表实体</description></item>
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
    ///         Diction index = new Diction();         
    ///     }
    /// }
    /// </code>
    /// </example>
    [Table("Diction")]
    public partial class Diction
    {
        #region 数据库对应的属性
        /// <summary>
        /// 编号 编号 10位数的编号
        /// </summary>        
        [Column("Id")]
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>        
        [Column("Name")]
        public String Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>        
        [Column("Note")]
        public String Note { get; set; }
        /// <summary>
        /// 父级编号 父级编号 引用主键
        /// </summary>        
        [Column("Parent_Id")]
        public decimal ParentId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>        
        [Column("Order_Id")]
        public decimal OrderId { get; set; }
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
        [Column("Update_By")]
        public String UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>        
        [Column("Update_Time")]
        public DateTime UpdateTime { get; set; }
        #endregion
        
        
        #region 自定义属性
        #endregion
    }
}

