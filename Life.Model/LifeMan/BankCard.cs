/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 段江涛
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 银行卡操作记录表实体
 * *        Remark      : 2013-08-04 段江涛 创建
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
    ///银行卡操作记录表实体
    ///</summary>
    ///<seealso cref="Object"/>
    ///<seealso cref="System"/>
    ///<remarks>
    ///<list type="bullet">
    ///<item><description>银行卡操作记录表实体</description></item>
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
    ///         BankCard index = new BankCard();         
    ///     }
    /// }
    /// </code>
    /// </example>
    [Table("Bank_Card")]    
    public class BankCard
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
        /// 操作类型 操作类型 表示存入还是取出，从字典表中获取
        /// </summary>        
        [Column("Save_Type")]
        public decimal SaveType { get; set; }
        /// <summary>
        /// 余额
        /// </summary>        
        [Column("Balance")]
        public double Balance { get; set; }
        /// <summary>
        /// 银行卡名称 银行卡名称 表示操作的银行卡的名称，从字典表中获得
        /// </summary>        
        [Column("Bank_Type")]
        public decimal BankType { get; set; }
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
        /// 创建时间 创建时间 系统当前默认时间
        /// </summary>        
        [Column("Create_Time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改者
        /// </summary>        
        [Column("UpDate_By")]
        public String UpdateBy { get; set; }
        /// <summary>
        /// 修改时间 修改时间 修改时候的系统默认时间
        /// </summary>        
        [Column("UpDate_Time")]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>        
        [Column("ImgUrl")]
        public String ImgUrl { get; set; }

        #endregion

        #region 自定义属性        

        #endregion
    }
}

