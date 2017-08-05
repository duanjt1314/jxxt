using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Life.Common.Sms;
using System.Net;
using System.Text;
using System.Security.Cryptography;

namespace Life.Web.ArticleManage
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SOCSms sms = new SOCSms()
            {
                loginId="13628497041",
                Pwd="jianglovezhu",
                ToNumber=this.TextBox1.Text,
                SmsContent=this.TextBox2.Text
            };

            Response.Write(sms.Send());

            /*WebClient _client = new WebClient();
            string postValues = "VER=4.0&CMD=Login&SEQ=123&UIN=479224006&PS="+MD5("jiangtao.123")+"&M5=1&LC=9326B87B234E7235";
            Byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(postValues);
            Byte[] pageData = _client.UploadData("http://tqq.tencent.com:8000", "POST", byteArray);

            Response.Write(Encoding.UTF8.GetString(pageData));*/
        }

        public static string MD5(string toCryString)
        {
            MD5CryptoServiceProvider hashmd5;
            hashmd5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(toCryString))).Replace("-", "").ToLower();//asp是小写,把所有字符变小写
        } 
    }
}