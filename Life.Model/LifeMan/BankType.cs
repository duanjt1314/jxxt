/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 段江涛
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 银行卡类型表实体
 * *        Remark      : 2014-12-07 段江涛 创建
 * *
 * *  Copyright (c) SuHui Corporation.  All rights reserveBd. 
 * ***************************************************************************************/
using System;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Life.Model.LifeMan
{
    ///<summary>
    ///银行卡类型表实体
    ///</summary>
    ///<seealso cref="Object"/>
    ///<seealso cref="System"/>
    ///<remarks>
    ///<list type="bullet">
    ///<item><description>银行卡类型表实体</description></item>
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
    ///         BankType index = new BankType();         
    ///     }
    /// }
    /// </code>
    /// </example>
    [Table("BankType")]
    public class BankType
    {
        #region 数据库对应的属性
        /// <summary>
        /// 唯一标识
        /// </summary>        
        [Column("Id")]
        [Key]
        public String Id { get; set; }

        /// <summary>
        /// 银行卡名称
        /// </summary>        
        [Column("BankName")]
        public Decimal BankName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>        
        [Column("Remark")]
        public String Remark { get; set; }

        /// <summary>
        /// 当前余额
        /// </summary>        
        [Column("Balance")]
        public Double Balance { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>        
        [Column("CreateBy")]
        public String CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>        
        [Column("CreateTime")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>        
        [Column("UpdateBy")]
        public String UpdateBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>        
        [Column("UpdateTime")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>        
        [Column("IsUse")]
        public Boolean IsUse { get; set; }

        #endregion

        #region 自定义属性
        #endregion
    }
}

