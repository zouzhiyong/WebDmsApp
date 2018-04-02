using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Application.BaseManage;
using NFine.Code;
using NFine.Domain.Entity.BaseManage;

namespace NFine.Web.Areas.BaseManage.Controllers
{
    public class CorporationController : ControllerBase
    {
        private CorporationApp corporationApp = new CorporationApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = corporationApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = corporationApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(CorporationEntity corporationEntity, string permissionIds, string keyValue)
        {
            corporationApp.SubmitForm(corporationEntity, keyValue);
            return Success("操作成功。");
        }       
    }
}