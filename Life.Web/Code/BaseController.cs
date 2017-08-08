using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Life.Model.LifeMan;
using System.Configuration;

namespace Life.Web
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Session中的当前用户信息
        /// </summary>
        protected Users CurrentUser
        {
            get
            {
                return HttpContext.Session["user"] as Users;
            }
        }

        protected String ActionKeys{
            get{
                return ConfigurationManager.AppSettings["actionkeys"];
            }
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (requestContext.HttpContext.Request["actionkeys"] != ActionKeys)
            {
                if (requestContext.HttpContext.Session["user"] == null)
                {
                    requestContext.HttpContext.Response.Redirect("~/Manage/Index", true);
                }
            }
            else
            {
               requestContext.HttpContext.Session["user"] = new Users() { Id = requestContext.HttpContext.Request["UserId"] };
            }
            base.Initialize(requestContext);
        }

        /// <summary>
        /// 上传数据
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="filePath">文件路径,如:/UpLoadFile/CorporateInformation/</param>
        /// <returns>返回上传的路径,网站的虚拟路径</returns>
        public String Upload(HttpPostedFileBase file,String filePath,String uploadName="")
        {
            //获取用户上传文件的后缀名
            string Extension = file.FileName.Substring(file.FileName.LastIndexOf("."));

            //物理完整路径                    
            //String filePath = "/UpLoadFile/CorporateInformation/";
            String fileFullPath = Server.MapPath(filePath);

            //检查是否有该路径  没有就创建
            if (!System.IO.Directory.Exists(fileFullPath))
            {
                Directory.CreateDirectory(fileFullPath);
            }
            
            //重新命名文件
            String fileName = DateTime.Now.ToFileTime().ToString() + Extension;
            if (uploadName != "")
                fileName = uploadName;
            
            file.SaveAs(fileFullPath + fileName);
            return filePath + fileName;
        }
    }
}