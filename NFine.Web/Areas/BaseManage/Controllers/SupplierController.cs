using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Application.BaseManage;
using NFine.Code;
using NFine.Domain.Entity.BaseManage;
using NFine.Web.App_Start._01_Handler;

namespace NFine.Web.Areas.BaseManage.Controllers
{
    public class SupplierController : ControllerBase
    {
        private SupplierApp supplierApp = new SupplierApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = supplierApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelcetJson()
        {
            var data = supplierApp.GetSelect();
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = supplierApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [MyValidateAntiForgeryToken]
        public ActionResult SubmitForm(SupplierEntity supplierEntity, string permissionIds, string keyValue)
        {
            supplierApp.SubmitForm(supplierEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [MyValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            supplierApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}