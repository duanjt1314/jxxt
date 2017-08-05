using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Life.Common.Sms
{
    /// <summary>
    /// 短信发送对象
    /// </summary>
    public class SOCSms
    {
        /// <summary>
        /// 发送短信的登录帐号
        /// </summary>
        public String loginId { get; set; }

        /// <summary>
        /// 发送短信的密码
        /// </summary>
        public String Pwd { get; set; }

        /// <summary>
        /// 接收短信的电话号码
        /// </summary>
        public String ToNumber { get; set; }

        /// <summary>
        /// 短信内容
        /// </summary>
        public String SmsContent { get; set; }

        /// <summary>
        /// 发送短息
        /// </summary>
        /// <returns></returns>
        public String Send()
        {
            //发送短信的URl
            String url = "http://quanapi.sinaapp.com/fetion.php";

            //要发送的内容
            string strSend = "u=" + this.loginId + "&p=" + this.Pwd + "&to=" + this.ToNumber +
                             "&m=" + this.SmsContent;

            WebClient web = new WebClient();
            byte[] by = web.DownloadData(url + "?" + strSend);            
            string html = Encoding.UTF8.GetString(by);

            //web.UploadData("","post",)
            return html;
        }
    }
}
