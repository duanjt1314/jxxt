/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 段江涛
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 模块表实体
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
    /// <summary>
    /// 消费月份汇总
    /// </summary>
    [Table("CostMonthCol")]
    public class CostMonthCol
    {
        #region 数据库对应的属性
        /// <summary>
        /// 月份
        /// </summary>        
        [Column("Time")]
        public String Time { get; set; }
        /// <summary>
        /// 消费金额
        /// </summary>        
        [Column("Pay")]
        public double Pay { get; set; }
        /// <summary>
        /// 收入金额
        /// </summary>        
        [Column("Income")]
        public double Income { get; set; }
        
        /// <summary>
        /// 收入金额
        /// </summary>        
        [Column("Balance")]
        public double Balance { get; set; }
        
        #endregion
        
        #region 自定义属性
        #endregion
    }
}

