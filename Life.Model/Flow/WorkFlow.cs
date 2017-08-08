/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 段江涛
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 流程表实体
 * *        Remark      : 2014-03-22 段江涛 创建
 * *
 * *  Copyright (c) SuHui Corporation.  All rights reserved. 
 * ***************************************************************************************/
using System;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Life.Model.Flow
{
    ///<summary>
    ///流程表实体
    ///</summary>
    ///<seealso cref="Object"/>
    ///<seealso cref="System"/>
    ///<remarks>
    ///<list type="bullet">
    ///<item><description>流程表实体</description></item>
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
    ///         WorkFlow index = new WorkFlow();         
    ///     }
    /// }
    /// </code>
    /// </example>
    [Table("WorkFlow")]
    public class WorkFlow
    {
        #region 数据库对应的属性
        /// <summary>
        /// 编号
        /// </summary>        
        [Column("Id")]
        [Key]
        public String Id { get; set; }
        
        /// <summary>
        /// 流程类型编号
        /// </summary>        
        [Column("TypeId")]
        public String TypeId { get; set; }
        
        /// <summary>
        /// 步骤顺序
        /// </summary>        
        [Column("OrderId")]
        public Int32 OrderId { get; set; }
        
        /// <summary>
        /// 步骤名称
        /// </summary>        
        [Column("Name")]
        public String Name { get; set; }
        
        /// <summary>
        /// 审核角色编号
        /// </summary>        
        [Column("RoleId")]
        public String RoleId { get; set; }
        
        /// <summary>
        /// 回退步骤 表示回退到哪一步
        /// </summary>        
        [Column("BackStep")]
        public Int32 BackStep { get; set; }
        
        /// <summary>
        /// 是否能够回退
        /// </summary>        
        [Column("IsBack")]
        public Boolean IsBack { get; set; }
        
        #endregion
        
        #region 自定义属性
        #endregion
    }
}

