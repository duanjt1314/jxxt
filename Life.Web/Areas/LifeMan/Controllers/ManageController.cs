using System;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Life.Common;
using System.Collections.Generic;
using Life.Model;
using Life.BLL;
using System.Net;
using Life.Common.Email;
using Life.Model.LifeMan;
using Life.BLL.LifeMan;
using System.Configuration;
using ThoughtWorks.QRCode.Codec;
using System.Drawing;

namespace Life.Web.Areas.LifeMan.Controllers
{
    /// <summary>
    /// 不涉及到权限的调用
    /// </summary>
    public class ManageController : Controller
    {
        public ActionResult Index()
        {
            //检查Cookie是不为空就自动登录
            var cookie = Request.Cookies["LifeLogin"];
            if (cookie != null)
            {
                var data = cookie.Value.Replace("%2C",",").Split(',');
                if (data.Length == 2)
                {
                    var loginUserName = data[0];
                    var loginPassword = MD5Encry.Encry(data[1]);
                    //直接登录
                    Users user = new BLUsers().Login(loginUserName, loginPassword);
                    if (user!=null)
                    {
                        Session["user"] = user;
                        return Redirect("/Default/Index");
                    }
                }
            }

            List<SysConfig> list= new BLSysConfig().Select(new HashTableExp("SysKey", "SysVersion"));
            ViewData["version"] = list[0].SysValue;
            ViewData["dataBase"] = ConfigurationManager.AppSettings["DAL"];
            return View();
        }

        public ActionResult GetImgVerifyChars()
        {
            //在此处放置用户代码以初始化页面
            HttpContext.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache); //不缓存
            Common.YZMHelper yz = new Common.YZMHelper();
            yz.CreateImage();
            Session["CheckCode"] = yz.Text; //将验证字符写入Session，供前台调用
            MemoryStream ms = new MemoryStream();
            yz.Image.Save(ms, ImageFormat.Png);
            yz.Image.Dispose();
            return File(ms.ToArray(), @"image/jpeg");
        }

        /// <summary>
        /// 获得所有的图标
        /// </summary>
        /// <returns></returns>
        public String GetIcons()
        {
            String[] files=System.IO.Directory.GetFiles(Server.MapPath("/Content/Images/icon16"));
            List<Icons> icons = new List<Icons>();
            foreach (var item in files)
            {
                string fileName = item.Substring(item.LastIndexOf('\\') + 1);
                icons.Add(new Icons() { url = "/Content/Images/icon16/"+fileName });
            }
            return JsonConvert.JavaScriptSerializer(icons);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginusername">登录名称</param>
        /// <param name="loginpassword">登录密码</param>
        /// <param name="checkCode">验证码</param>
        /// <returns></returns>
        public String Login(String loginUserName, String loginPassword, String checkCode)
        {
            try
            {
                if (Request["actionkeys"] != ConfigurationManager.AppSettings["actionkeys"])
                {
                    //if (Session["CheckCode"].ToString().ToLower() != checkCode.ToLower())
                    //    return JsonConvert.JavaScriptSerializer(new ExtResult() { success = false, msg = "验证码错误" });
                }                

                loginPassword = MD5Encry.Encry(loginPassword);
                Users user = new BLUsers().Login(loginUserName, loginPassword);
                if (user!=null)
                {
                    Session["user"] = user;
                    return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "登录成功",data=user });
                }
                else
                {
                    return JsonConvert.JavaScriptSerializer(new ExtResult() { success = false, msg = "用户名或密码错误" });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = false, msg = "登录失败，失败原因:"+ex.Message });
            }          
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public String ForgotPwd(String email)
        {
            String msg = "";
            //查询邮件是否有效
            List<Users> users = new BLUsers().Select(new HashTableExp("Mail", email));
            if (users.Count == 0)
            {
                msg = "邮箱信息无效";
            }
            else
            {
                Users user = users[0];
                TempData data = new TempData()
                {
                    Id=Guid.NewGuid().GetString(),
                    Email=email,
                    CreateTime=DateTime.Now,
                    Expires=DateTime.Now.AddMinutes(3)                    
                };
                new BLTempData().Add(data);

                String html = "尊敬的客户您好： <p style=\"padding-left:40px\"> 感谢您使用生活费管理系统，请<a href=\"{url}\" target=\"_blank\">点击这里</a>找回密码</p>";
                String url = String.Format("?subKey={0}&email={1}", data.Id, data.Email);
                html = html.Replace("{url}", "http://"+Request.UrlReferrer.Authority + "/BackPwd/index.aspx" + url);
                SOCNetSendMail.AsyncSender(EmailType.QQ, new EmailUser() { UserName = "lifemanager@foxmail.com", UserPwd = "duanjiangtao" }, email,html, "找回密码");
                msg = "邮件已发送到你的邮箱，请注意查收";
            }
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = msg});
        }

        /// <summary>
        /// 退出当前登录
        /// </summary>
        /// <returns></returns>
        public String ExitLogin()
        {
            try
            {
                Session["user"] = null; //清空session
                //清空cookie的方法就是将cookie的失效期设置为当前日期之前            
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "退出成功" });
            }
            catch (Exception ex)
            {
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = false, msg = "退出失败，原因:"+ex.Message });
            }
        }

        /// <summary>
        /// 获得当前登录用户信息
        /// </summary>
        /// <returns></returns>
        public String GetCurrentUser()
        {
            Users user = Session["user"] as Users;
            if (user == null)
                user = new Users();
            return JsonConvert.JavaScriptSerializer(user);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public String UpdatePass(String oldPass,String newPass)
        {
            Users user = Session["user"] as Users;
            if (MD5Encry.Encry(oldPass) != user.LoginPwd)
            {
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = false, msg = "原始密码错误" });
            }
            else
            {
                user.LoginPwd =MD5Encry.Encry(newPass);
                new BLUsers().Update(user);
                Session["user"] = user;
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "密码修改成功" });
            }
        }

        /// <summary>
        /// 获得重要信息
        /// </summary>
        /// <returns></returns>
        public String GetMessage()
        {
            String msg = String.Empty;
            DateTime beginTime = Convert.ToDateTime("2010-12-24");
            DateTime endTime = DateTime.Now;
            TimeSpan total = endTime - beginTime;
            msg = "和小猪猪相爱<span style='color:red'>" + total.Days + "</span>天了";

            beginTime = Convert.ToDateTime("2014-02-07");
            total = endTime - beginTime;
            msg += "<br/>和小猪猪领证<span style='color:red'>" + total.Days + "</span>天了";
            return msg;
        }

        /// <summary>
        /// 获得手机二维码下载地址
        /// </summary>
        public void GetCodeImage()
        {
            String url = Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, "");
            url = url + "/File/Duanjt.Life.apk";

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //设置编码方式
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //设置规模和版本
            qrCodeEncoder.QRCodeScale = 2;
            qrCodeEncoder.QRCodeVersion = 7;
            //这里设置规模为：4，版本为：7，其余值读者可以自行试验，这两个值基本只是改变了二维码的大小，读者设置的值都是现在普遍使用的值。

            //设置错误校验（错误更正）的级别：这里设置为中等，一共有四个级别，读者可以自行试验。
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;

            Bitmap bmp = qrCodeEncoder.Encode(url);
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            
            Response.ClearContent();
            Response.ContentType = "image/Gif";
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }
    }

    class Icons
    {
        public String url{get;set;}
    }
}
