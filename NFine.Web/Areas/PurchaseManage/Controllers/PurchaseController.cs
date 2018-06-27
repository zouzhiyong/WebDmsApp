using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Application.BaseManage;
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.BaseManage;
using NFine.Web.App_Start._01_Handler;

namespace NFine.Web.Areas.PurchaseManage.Controllers
{
    public class PurchaseController : ControllerBase
    {
        private SerialNumberDetailApp serialNumberDetailApp = new SerialNumberDetailApp();
        
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson()
        {
            string data = serialNumberDetailApp.GetAutoIncrementCode("PurchaseOrder");
            //var data = new
            //{
            //    rows = purchaseApp.GetList(pagination, keyword),
            //    total = pagination.total,
            //    page = pagination.page,
            //    records = pagination.records
            //};
            return Content(data);
        }
        //[HttpGet]
        //[HandlerAjaxOnly]
        //public ActionResult GetFormJson(string keyValue)
        //{
        //    var data = purchaseApp.GetForm(keyValue);
        //    return Content(data.ToJson());
        //}
        //[HttpPost]
        //[HandlerAjaxOnly]
        //[MyValidateAntiForgeryToken]
        //public ActionResult SubmitForm(PurchaseEntity supplierEntity, string permissionIds, string keyValue)
        //{
        //    purchaseApp.SubmitForm(supplierEntity, keyValue);
        //    return Success("操作成功。");
        //}
        //[HttpPost]
        //[HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[MyValidateAntiForgeryToken]
        //public ActionResult DeleteForm(string keyValue)
        //{
        //    purchaseApp.DeleteForm(keyValue);
        //    return Success("删除成功。");
        //}
    }
}