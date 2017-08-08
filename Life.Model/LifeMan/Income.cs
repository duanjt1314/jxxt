/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 段江涛
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 收入记录表实体
 * *        Remark      : 2013-12-17 段江涛 创建
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
    ///收入记录表实体
    ///</summary>
    ///<seealso cref="Object"/>
    ///<seealso cref="System"/>
    ///<remarks>
    ///<list type="bullet">
    ///<item><description>收入记录表实体</description></item>
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
    ///         Income index = new Income();         
    ///     }
    /// }
    /// </code>
    /// </example>
    [Table("Income")]
    public class Income
    {
        #region 数据库对应的属性
        /// <summary>
        /// 编号
        /// </summary>        
        [Column("Id")]
        [Key]
        public String Id { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>        
        [Column("TIME")]
        public DateTime Time { get; set; }
        /// <summary>
        /// 操作金额
        /// </summary>        
        [Column("Price")]
        public double Price { get; set; }
        /// <summary>
        /// 备注
        /// </summary>        
        [Column("Note")]
        public String Note { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>        
        [Column("Create_By")]
        public String CreateBy { get; set; }
        /// <summary>
        /// 创建时间 创建时间 创建时间 系统当前默认时间
        /// </summary>        
        [Column("Create_Time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改者
        /// </summary>        
        [Column("UpDate_By")]
        public String UpdateBy { get; set; }
        /// <summary>
        /// 修改时间 修改时间 修改时间 修改时候的系统默认时间
        /// </summary>        
        [Column("UpDate_Time")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 家庭内部收入，如家庭成员的某个人把钱给自己
        /// </summary>
        [Column("FamilyIncome")]
        public Boolean FamilyIncome { get; set; }

        /// <summary>
        /// 特殊标识
        /// </summary>        
        [Column("IsMark")]
        public Boolean IsMark { get; set; }

        /// <summary>
        /// 自定义分组
        /// </summary>        
        [Column("CusGroup")]
        public String CusGroup { get; set; }
        #endregion

        #region 自定义属性
        #endregion
    }
}

