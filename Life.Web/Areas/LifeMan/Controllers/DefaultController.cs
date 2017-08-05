using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Life.Web.Areas.LifeMan.Controllers
{
    /// <summary>
    /// 默认控制器,主界面
    /// </summary>
    public class DefaultController : BaseController
    {
        //
        // GET: /Default/

        public ActionResult Index()
        {
            return View();
        }

    }
}
