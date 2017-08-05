/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 段江涛
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 用户角色对应表实体
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
    ///用户角色对应表实体
    ///</summary>
    ///<seealso cref="Object"/>
    ///<seealso cref="System"/>
    ///<remarks>
    ///<list type="bullet">
    ///<item><description>用户角色对应表实体</description></item>
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
    ///         UserToRole index = new UserToRole();         
    ///     }
    /// }
    /// </code>
    /// </example>
    [Table("User_To_Role")]
    public class UserToRole
    {
        #region 数据库对应的属性
        /// <summary>
        /// 用户角色编号
        /// </summary>        
        [Column("Id")]
        [Key]
        public String Id { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>        
        [Column("User_Id")]
        public String UserId { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>        
        [Column("Role_Id")]
        public String RoleId { get; set; }
        #endregion
        
        #region 自定义属性
        #endregion
    }
}

