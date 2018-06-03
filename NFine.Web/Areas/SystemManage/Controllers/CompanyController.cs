using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Web.App_Start._01_Handler;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class CompanyController : ControllerBase
    {
        private CompanyApp companyApp = new CompanyApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson()
        {
            var data = companyApp.GetList();
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = companyApp.GetList(pagination, keyword),
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
            var data = companyApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [MyValidateAntiForgeryToken]
        public ActionResult SubmitForm(CompanyEntity companyEntity, string permissionIds, string keyValue)
        {
            string[] arr = permissionIds.Split(',').Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();
            companyApp.SubmitForm(companyEntity, arr, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [MyValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            companyApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [MyValidateAntiForgeryToken]
        public ActionResult DisabledAccount(string keyValue)
        {
            CompanyEntity companyEntity = new CompanyEntity();
            companyEntity.F_Id = keyValue;
            companyEntity.F_EnabledMark = false;
            companyApp.UpdateForm(companyEntity);
            return Success("公司禁用成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [MyValidateAntiForgeryToken]
        public ActionResult EnabledAccount(string keyValue)
        {
            CompanyEntity companyEntity = new CompanyEntity();
            companyEntity.F_Id = keyValue;
            companyEntity.F_EnabledMark = true;
            companyApp.UpdateForm(companyEntity);
            return Success("公司启用成功。");
        }
    }
}