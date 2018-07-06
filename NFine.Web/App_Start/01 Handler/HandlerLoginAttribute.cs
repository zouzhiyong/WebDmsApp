using NFine.Code;
using System.Web.Mvc;

namespace NFine.Web
{
    public class HandlerLoginAttribute : AuthorizeAttribute
    {
        public bool Ignore = true;
        public HandlerLoginAttribute(bool ignore = true)
        {
            Ignore = ignore;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (Ignore == false)
            {
                return;
            }
            if (OperatorProvider.Provider.GetCurrent() == null)
            {
                WebHelper.WriteCookie("nfine_login_error", "overdue");
                string sysVirDir = "/" + Configs.GetValue("SystemVirtualDirectory").ToLower() + "/";
                filterContext.HttpContext.Response.Write("<script>top.location.href = '" + sysVirDir + "Login/Index';</script>");
                return;
            }
        }
    }
}