/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 段江涛
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 用户表实体
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
    ///用户表实体
    ///</summary>
    ///<seealso cref="Object"/>
    ///<seealso cref="System"/>
    ///<remarks>
    ///<list type="bullet">
    ///<item><description>用户表实体</description></item>
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
    ///         Users index = new Users();         
    ///     }
    /// }
    /// </code>
    /// </example>
    [Table("Users")]
    public class Users
    {
        #region 数据库对应的属性
        /// <summary>
        /// 编号
        /// </summary>        
        [Column("Id")]
        [Key]
        public String Id { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>        
        [Column("Login_Id")]
        public String LoginId { get; set; }
        /// <summary>
        /// 密码
        /// </summary>        
        [Column("Login_Pwd")]
        public String LoginPwd { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>        
        [Column("Name")]
        public String Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>        
        [Column("Phone")]
        public String Phone { get; set; }
        /// <summary>
        /// 邮件
        /// </summary>        
        [Column("Mail")]
        public String Mail { get; set; }
        /// <summary>
        /// 地址
        /// </summary>        
        [Column("Address")]
        public String Address { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>        
        [Column("Age")]
        public decimal Age { get; set; }
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

