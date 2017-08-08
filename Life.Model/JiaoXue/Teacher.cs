using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Life.Model.JiaoXue
{
    [Table("Teacher")]
    public class Teacher
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
        /// 性别
        /// </summary>        
        [Column("Sex")]
        public int Sex { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>        
        [Column("native_place")]
        public String NativePlace { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>        
        [Column("phone")]
        public String Phone { get; set; }
        /// <summary>
        /// QQ
        /// </summary>        
        [Column("qq")]
        public String Qq { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>        
        [Column("email")]
        public String Email { get; set; }
        /// <summary>
        /// 毕业院校
        /// </summary>        
        [Column("graduate_school")]
        public String GraduateSchool { get; set; }
        /// <summary>
        /// 所读专业
        /// </summary>        
        [Column("professional")]
        public String Professional { get; set; }
        /// <summary>
        /// 擅长科目
        /// </summary>        
        [Column("good_subjects")]
        public String GoodSubjects { get; set; }
        /// <summary>
        /// 自我评价
        /// </summary>        
        [Column("self_assessment")]
        public String SelfAssessment { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>        
        [Column("create_by")]
        public String CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>        
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>        
        [Column("update_by")]
        public String UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>        
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}
