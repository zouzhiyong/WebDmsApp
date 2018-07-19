using System.Web.Mvc;

namespace NFine.Web.Areas.PurchaseManage
{
    public class PurchaseManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PurchaseManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                this.AreaName + "_Default",
                this.AreaName + "/{controller}/{action}/{id}",
                new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "NFine.Web.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}
