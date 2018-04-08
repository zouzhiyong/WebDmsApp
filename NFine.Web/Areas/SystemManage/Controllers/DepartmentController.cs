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
    public class DepartmentController : ControllerBase
    {
        private DepartmentApp departmentApp = new DepartmentApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson()
        {
            var data = departmentApp.GetList();
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = departmentApp.GetList(pagination, keyword),
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
            var data = departmentApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [MyValidateAntiForgeryToken]
        public ActionResult SubmitForm(DepartmentEntity departmentEntity, string permissionIds, string keyValue)
        {
            departmentApp.SubmitForm(departmentEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [MyValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            departmentApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}