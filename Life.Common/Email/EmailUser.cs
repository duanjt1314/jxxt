using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Life.Common.Email
{
    /// <summary>
    /// Email账户
    /// </summary>
    public class EmailUser
    {
        private String userName;

        /// <summary>
        /// 邮箱号
        /// </summary>
        public String UserName
        {
            get { return this.userName; }
            set
            {
                if (HasEmail(value))
                    this.userName = value;
                else
                    throw new Exception("邮箱账号格式错误");
            }
        }

        /// <summary>
        /// 邮箱密码
        /// </summary>
        public String UserPwd { get; set; }

        /// <summary>
        /// 验证字符串是否是邮箱账号
        /// </summary>
        /// <param name="strEmail">需要验证的字符串</param>
        /// <returns>验证结果</returns>
        private Boolean HasEmail(String strEmail)
        {
            return Regex.IsMatch(strEmail, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", RegexOptions.IgnoreCase);
        }
    }
}
