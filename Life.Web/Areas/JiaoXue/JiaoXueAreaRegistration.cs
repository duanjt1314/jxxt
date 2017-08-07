using System.Web.Mvc;

namespace Life.Web.Areas.JiaoXue
{
    public class JiaoXueAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "JiaoXue";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "JiaoXue_default",
                "JiaoXue/{controller}/{action}/{id}",
                new {controller="Manage", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
