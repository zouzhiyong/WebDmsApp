using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Application.WarehouseManage;
using NFine.Code;
using NFine.Domain.Entity.PurchaseManage;
using NFine.Domain.Entity.WarehouseManage;
using NFine.Web.App_Start._01_Handler;

namespace NFine.Web.Areas.WarehouseManage.Controllers
{
    public class ReceiptController : ControllerBase
    {
        private ReceiptApp receiptApp = new ReceiptApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword, string type)
        {
            var data = new
            {
                rows = receiptApp.GetList(pagination, keyword, type),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult GetOrderDetailForm(List<PurOrderEntity> model)
        {
            var data = receiptApp.GetDetail(model);
            return Content(data.ToJson());
        }


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue, string type, int prenexttype)
        {
            var data = receiptApp.GetForm(keyValue, type, prenexttype);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [MyValidateAntiForgeryToken]
        public ActionResult GetCopyFormJson(string keyValue)
        {
            var data = receiptApp.GetCopyForm(keyValue);
            return Content(data.ToJson());
        }


        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [MyValidateAntiForgeryToken]
        public ActionResult SubmitForm(WareReceiptEntity model)
        {
            var result = receiptApp.SubmitForm(model);
            return Success("操作成功。", result);
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [MyValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            receiptApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}