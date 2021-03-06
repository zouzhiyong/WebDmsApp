﻿using NFine.Application.SystemManage;
using NFine.Code;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web
{
    public class HandlerAuthorizeAttribute : ActionFilterAttribute
    {
        public bool Ignore { get; set; }
        public HandlerAuthorizeAttribute(bool ignore = true)
        {
            Ignore = ignore;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (OperatorProvider.Provider.GetCurrent() == null)
            {
                WebHelper.WriteCookie("nfine_login_error", "overdue");
                string sysVirDir = "/" + Configs.GetValue("SystemVirtualDirectory").ToLower() + "/";
                filterContext.HttpContext.Response.Write("<script>top.location.href = '"+ sysVirDir + "Login/Index';</script>");
                return;
            }

            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                return;
            }
            if (Ignore == false)
            {
                return;
            }
            if (!this.ActionAuthorize(filterContext))
            {
                StringBuilder sbScript = new StringBuilder();
                sbScript.Append("<script type='text/javascript'>alert('很抱歉！您的权限不足，访问被拒绝！');</script>");

                JsonResult json = new JsonResult();
                json.Data = new { state = "error", message = "很抱歉！您的权限不足，访问被拒绝！" };
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = json;// new ContentResult() { Content = sbScript.ToString() };
                return;
            }
        }
        private bool ActionAuthorize(ActionExecutingContext filterContext)
        {
            var operatorProvider = OperatorProvider.Provider.GetCurrent();
            var roleId = operatorProvider.RoleId;
            var moduleId = WebHelper.GetCookie("nfine_currentmoduleid");
            var action = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();
            return new RoleAuthorizeApp().ActionValidate(roleId, moduleId, action);
        }
    }
}