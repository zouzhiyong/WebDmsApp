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
    public class WarehouseController : ControllerBase
    {
        private WarehouseApp warehouseApp = new WarehouseApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string keyValue="")
        {
            var data = warehouseApp.GetList(keyValue);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = warehouseApp.GetList(pagination, keyword),
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
            var data = warehouseApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [MyValidateAntiForgeryToken]
        public ActionResult SubmitForm(WarehouseEntity warehouseEntity, string userIds, string keyValue)
        {
            string[] arr = userIds.Split(',').Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();
            warehouseApp.SubmitForm(warehouseEntity, arr, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [MyValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            warehouseApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}