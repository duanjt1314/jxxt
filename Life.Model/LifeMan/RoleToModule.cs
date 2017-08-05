/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 段江涛
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 角色模块对应表实体
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
    ///角色模块对应表实体
    ///</summary>
    ///<seealso cref="Object"/>
    ///<seealso cref="System"/>
    ///<remarks>
    ///<list type="bullet">
    ///<item><description>角色模块对应表实体</description></item>
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
    ///         RoleToModule index = new RoleToModule();         
    ///     }
    /// }
    /// </code>
    /// </example>
    [Table("Role_To_Module")]
    public class RoleToModule
    {
        #region 数据库对应的属性
        /// <summary>
        /// 角色模块编号
        /// </summary>        
        [Column("Id")]
        [Key]
        public String Id { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>        
        [Column("Role_Id")]
        public String RoleId { get; set; }
        /// <summary>
        /// 模块编号
        /// </summary>        
        [Column("Module_Id")]
        public String ModuleId { get; set; }
        #endregion
        
        #region 自定义属性
        #endregion
    }
}

