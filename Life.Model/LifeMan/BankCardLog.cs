/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 段江涛
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 银行卡日志记录表实体
 * *        Remark      : 2013-09-12 段江涛 创建
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
    ///银行卡日志记录表实体
    ///</summary>
    ///<seealso cref="Object"/>
    ///<seealso cref="System"/>
    ///<remarks>
    ///<list type="bullet">
    ///<item><description>银行卡日志记录表实体</description></item>
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
    ///         BankCardLog index = new BankCardLog();         
    ///     }
    /// }
    /// </code>
    /// </example>
    [Table("Bank_Card_log")]
    public class BankCardLog
    {
        public BankCardLog()
        {

        }

        public BankCardLog(VBankCard bankCard,String opeType)
        {
            this.Id = Guid.NewGuid().ToString();
            this.BankId = bankCard.Id;
            this.Time = bankCard.Time;
            this.Price = bankCard.Price;
            this.SaveType = bankCard.SaveName;
            this.Balance = bankCard.Balance;
            this.BankType = bankCard.BankTypeName;
            this.Note = bankCard.Note;
            this.CreateBy = bankCard.UpdateBy;
            this.CreateTime = bankCard.UpdateTime;
            this.OpeType = opeType;
        }

        #region 数据库对应的属性
        /// <summary>
        /// 编号
        /// </summary>        
        [Column("Id")]
        public String Id { get; set; }
        /// <summary>
        /// 银行卡编号
        /// </summary>        
        [Column("Bank_Id")]
        public String BankId { get; set; }
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
        /// 操作类型 存入/取出
        /// </summary>        
        [Column("Save_Type")]
        public String SaveType { get; set; }
        /// <summary>
        /// 余额
        /// </summary>        
        [Column("Balance")]
        public double Balance { get; set; }
        /// <summary>
        /// 银行卡名称
        /// </summary>        
        [Column("Bank_Type")]
        public String BankType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>        
        [Column("Note")]
        public String Note { get; set; }
        /// <summary>
        /// 创建人编号
        /// </summary>        
        [Column("Create_By")]
        public String CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>        
        [Column("Create_Time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 操作类型 修改/删除
        /// </summary>        
        [Column("Ope_Type")]
        public String OpeType { get; set; }
        #endregion

        #region 自定义属性
        #endregion
    }
}

