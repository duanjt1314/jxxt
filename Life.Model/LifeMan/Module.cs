/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 段江涛
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 模块表实体
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
    ///模块表实体
    ///</summary>
    ///<seealso cref="Object"/>
    ///<seealso cref="System"/>
    ///<remarks>
    ///<list type="bullet">
    ///<item><description>模块表实体</description></item>
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
    ///         Module index = new Module();         
    ///     }
    /// }
    /// </code>
    /// </example>
    [Table("Module")]
    public class Module
    {
        #region 数据库对应的属性
        /// <summary>
        /// 模块编号
        /// </summary>        
        [Column("Module_ID")]
        [Key]
        public String ModuleId { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>        
        [Column("Module_Name")]
        public String ModuleName { get; set; }
        /// <summary>
        /// 模块路径
        /// </summary>        
        [Column("Module_URL")]
        public String ModuleUrl { get; set; }
        /// <summary>
        /// 图标路径
        /// </summary>        
        [Column("Icon_Url")]
        public String IconUrl { get; set; }
        /// <summary>
        /// 父级模块
        /// </summary>        
        [Column("Parent_Id")]
        public String ParentId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>        
        [Column("Order_Id")]
        public decimal OrderId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>        
        [Column("Notes")]
        public String Notes { get; set; }
        /// <summary>
        /// 状态 状态 1可用，0不可用
        /// </summary>        
        [Column("STATUS")]
        public decimal Status { get; set; }
        #endregion
        
        #region 自定义属性
        #endregion
    }
}

