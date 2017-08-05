using System.Web.Mvc;

namespace Life.Web.Areas.LifeMan
{
    public class LifeManAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "LifeMan";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "LifeMan_default",
                "LifeMan/{controller}/{action}/{id}",
                new {controller="Manage", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
