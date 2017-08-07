/***************************************************************************************
 * *
 * *        File Name          : BiddingInfo.cs
 * *        Creator            : 樊小平
 * *        Create Time        : 2013-06-19
 * *        Functional Description  : 招标信息实体
 * *        Remark      : 2013-06-19 樊小平 创建
 * *
 * *  Copyright (c) SuHui Corporation.  All rights reserved. 
 * ***************************************************************************************/
using System;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Life.Model.JiaoXue
{
    ///<summary>
    ///实体
    ///</summary>
    ///<seealso cref="Object"/>
    ///<seealso cref="System"/>
    ///<remarks>
    ///<list type="bullet">
    ///<item><description>实体</description></item>
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
    ///         Student index = new Student();         
    ///     }
    /// }
    /// </code>
    /// </example>
    [Table("Student")]
    public class Student
    {
        /// <summary>
        /// 编号
        /// </summary>        
        [Column("id")]
        public String Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>        
        [Column("name")]
        public String Name { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>        
        [Column("card_no")]
        public String CardNo { get; set; }
        /// <summary>
        /// 生日
        /// </summary>        
        [Column("birth_day")]
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// 性别
        /// </summary>        
        [Column("sex")]
        public int Sex { get; set; }
        /// <summary>
        /// 地址
        /// </summary>        
        [Column("addr")]
        public String Addr { get; set; }
        /// <summary>
        /// 备注
        /// </summary>        
        [Column("remark")]
        public String Remark { get; set; }
    }
}

