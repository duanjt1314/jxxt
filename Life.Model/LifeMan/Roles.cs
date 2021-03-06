﻿/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 段江涛
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 角色表实体
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
    ///角色表实体
    ///</summary>
    ///<seealso cref="Object"/>
    ///<seealso cref="System"/>
    ///<remarks>
    ///<list type="bullet">
    ///<item><description>角色表实体</description></item>
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
    ///         Roles index = new Roles();         
    ///     }
    /// }
    /// </code>
    /// </example>
    [Table("Roles")]
    public class Roles
    {
        #region 数据库对应的属性
        /// <summary>
        /// 角色编号
        /// </summary>        
        [Column("Role_Id")]
        [Key]
        public String RoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>        
        [Column("Role_Name")]
        public String RoleName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>        
        [Column("Notes")]
        public String Notes { get; set; }
        #endregion
        
        #region 自定义属性
        #endregion
    }
}

