/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 段江涛
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 扫帚销售表实体
 * *        Remark      : 2014-02-04 段江涛 创建
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
    ///扫帚销售表实体
    ///</summary>
    ///<seealso cref="Object"/>
    ///<seealso cref="System"/>
    ///<remarks>
    ///<list type="bullet">
    ///<item><description>扫帚销售表实体</description></item>
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
    ///         SalesVol index = new SalesVol();         
    ///     }
    /// }
    /// </code>
    /// </example>
    [Table("Sales_Vol")]
    public class SalesVol
    {
        #region 数据库对应的属性
        /// <summary>
        /// 编号
        /// </summary>        
        [Column("Id")]
        [Key]
        public String Id { get; set; }
        /// <summary>
        /// 销售时间
        /// </summary>        
        [Column("Pur_Time")]
        public DateTime PurTime { get; set; }
        /// <summary>
        /// 数量(个) 销售数量
        /// </summary>        
        [Column("Number")]
        public double Number { get; set; }
        /// <summary>
        /// 单价
        /// </summary>        
        [Column("Unit_Price")]
        public double UnitPrice { get; set; }
        /// <summary>
        /// 总价
        /// </summary>        
        [Column("Total_Price")]
        public double TotalPrice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>        
        [Column("Note")]
        public String Note { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>        
        [Column("Create_By")]
        public String CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>        
        [Column("Create_Time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>        
        [Column("UpDate_By")]
        public String UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>        
        [Column("UpDate_Time")]
        public DateTime? UpdateTime { get; set; }
        #endregion

        #region 自定义属性
        #endregion
    }
}

