/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 段江涛
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 系统配置实体
 * *        Remark      : 2013-12-29 段江涛 创建
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
    ///系统配置实体
    ///</summary>
    ///<seealso cref="Object"/>
    ///<seealso cref="System"/>
    ///<remarks>
    ///<list type="bullet">
    ///<item><description>系统配置实体</description></item>
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
    ///         SysConfig index = new SysConfig();         
    ///     }
    /// }
    /// </code>
    /// </example>
    [Table("Sys_Config")]
    public class SysConfig
    {
        #region 数据库对应的属性
        /// <summary>
        /// 唯一标识
        /// </summary>        
        [Column("Id")]
        [Key]
        public String Id { get; set; }
        /// <summary>
        /// 键
        /// </summary>        
        [Column("Sys_Key")]
        public String SysKey { get; set; }
        /// <summary>
        /// 名称
        /// </summary>        
        [Column("Name")]
        public String Name { get; set; }
        /// <summary>
        /// 值
        /// </summary>        
        [Column("Sys_Value")]
        public String SysValue { get; set; }
        /// <summary>
        /// 备注
        /// </summary>        
        [Column("Remark")]
        public String Remark { get; set; }
        /// <summary>
        /// 分组标识
        /// </summary>        
        [Column("Group_No")]
        public String GroupNo { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>        
        [Column("Is_Visible")]
        public bool IsVisible { get; set; }
        /// <summary>
        /// 排序序号
        /// </summary>        
        [Column("Order_Id")]
        public int OrderId { get; set; }
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
        /// 修改人编号
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

