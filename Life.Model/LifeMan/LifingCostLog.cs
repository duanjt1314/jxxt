/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 段江涛
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 生活费日志记录表实体
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
    ///生活费日志记录表实体
    ///</summary>
    ///<seealso cref="Object"/>
    ///<seealso cref="System"/>
    ///<remarks>
    ///<list type="bullet">
    ///<item><description>生活费日志记录表实体</description></item>
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
    ///         LifingcostLog index = new LifingcostLog();         
    ///     }
    /// }
    /// </code>
    /// </example>
    [Table("Lifing_Cost_log")]
    public class LifingCostLog
    {
        public LifingCostLog()
        {

        }

        public LifingCostLog(VLifingCost life,string opeType)
        {
            this.Id = Guid.NewGuid().ToString();
            this.CostId =life.Id;
            this.Time = life.Time;
            this.Reason = life.Reason;
            this.Price = life.Price;
            this.Notes = life.Notes;
            this.CostTypeName = life.CostTypeName;
            this.CreateBy = life.UpdateBy;
            this.OpeType = opeType;
        }

        #region 数据库对应的属性
        /// <summary>
        /// 编号
        /// </summary>        
        [Column("Id")]
        [Key]
        public String Id { get; set; }
        /// <summary>
        /// 消费编号
        /// </summary>        
        [Column("Cost_Id")]
        public String CostId { get; set; }
        /// <summary>
        /// 消费时间
        /// </summary>        
        [Column("TIME")]
        public DateTime Time { get; set; }
        /// <summary>
        /// 消费名称
        /// </summary>        
        [Column("Reason")]
        public String Reason { get; set; }
        /// <summary>
        /// 消费金额
        /// </summary>        
        [Column("Price")]
        public double Price { get; set; }
        /// <summary>
        /// 备注
        /// </summary>        
        [Column("Notes")]
        public String Notes { get; set; }
        /// <summary>
        /// 消费类型名称
        /// </summary>        
        [Column("Cost_Type_Name")]
        public String CostTypeName { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>        
        [Column("Create_By")]
        public String CreateBy { get; set; }
        /// <summary>
        /// 操作时间
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

