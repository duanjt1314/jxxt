/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 段江涛
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 流程类型表实体
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
    ///流程类型表实体
    ///</summary>
    ///<seealso cref="Object"/>
    ///<seealso cref="System"/>
    ///<remarks>
    ///<list type="bullet">
    ///<item><description>流程类型表实体</description></item>
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
    ///         FlowType index = new FlowType();         
    ///     }
    /// }
    /// </code>
    /// </example>
    [Table("FlowType")]
    public class FlowType
    {
        #region 数据库对应的属性
        /// <summary>
        /// 编号
        /// </summary>        
        [Column("Id")]
        [Key]
        public String Id { get; set; }
        
        /// <summary>
        /// 流程名称
        /// </summary>        
        [Column("Name")]
        public String Name { get; set; }
        
        /// <summary>
        /// 描述
        /// </summary>        
        [Column("Description")]
        public String Description { get; set; }
        
        #endregion
        
        #region 自定义属性
        #endregion
    }
}

