/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 段江涛
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 生活费操作管理实体
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
    ///生活费操作管理实体
    ///</summary>
    ///<seealso cref="Object"/>
    ///<seealso cref="System"/>
    ///<remarks>
    ///<list type="bullet">
    ///<item><description>生活费操作管理实体</description></item>
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
    ///         LifingCost index = new LifingCost();         
    ///     }
    /// }
    /// </code>
    /// </example>
    [Table("Lifing_Cost")]
    public class LifingCost
    {
        #region 数据库对应的属性
        /// <summary>
        /// 编号
        /// </summary>        
        [Column("Id")]
        [Key]        
        public String Id { get; set; }
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
        /// 消费类型
        /// </summary>        
        [Column("Cost_Type_Id")]
        //[ForeignKey("diction")]
        public decimal CostTypeId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>        
        [Column("Notes")]
        public String Notes { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        [Column("Img_Url")]
        public String ImgUrl { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>        
        [Column("Create_By")]
        public String CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>        
        [Column("Create_Time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改者
        /// </summary>        
        [Column("UpDate_By")]
        public String UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>        
        [Column("UpDate_Time")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 特别标识
        /// </summary>        
        [Column("IsMark")]
        public Boolean IsMark { get; set; }

        /// <summary>
        /// 家庭内部支出,如把钱给家庭成员的某个人
        /// </summary>        
        [Column("FamilyPay")]
        public Boolean FamilyPay { get; set; }

        /// <summary>
        /// 自定义分组
        /// </summary>        
        [Column("CusGroup")]
        public String CusGroup { get; set; }
        #endregion

    }
}

