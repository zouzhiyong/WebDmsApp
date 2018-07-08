using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Application.PurManage;
using NFine.Code;
using NFine.Domain.Entity.PurchaseManage;
using NFine.Web.App_Start._01_Handler;

namespace NFine.Web.Areas.PurchaseManage.Controllers
{
    public class PurchaseController : ControllerBase
    {
        private OrderApp orderApp = new OrderApp();


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = orderApp.GetList(pagination, keyword),
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
            var data = orderApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [MyValidateAntiForgeryToken]
        public ActionResult GetCopyFormJson(string keyValue)
        {
            var data = orderApp.GetCopyForm(keyValue);
            return Content(data.ToJson());
        }


        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [MyValidateAntiForgeryToken]
        public ActionResult SubmitForm(OrderEntity model)
        {
            var result=orderApp.SubmitForm(model);
            return Success("操作成功。", result);
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [MyValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            orderApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        //SubmitForm(OrderEntity orderEntity, OrderDetailEntity[] orderDetailEntitys, string keyValue)
        //[HttpGet]
        //[HandlerAjaxOnly]
        //public ActionResult GetGridJson()
        //{
        //string data = serialNumberDetailApp.GetAutoIncrementCode("PurchaseOrder");
        //var data = new
        //{
        //    rows = purchaseApp.GetList(pagination, keyword),
        //    total = pagination.total,
        //    page = pagination.page,
        //    records = pagination.records
        //};
        //return Content(data);
        // }
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