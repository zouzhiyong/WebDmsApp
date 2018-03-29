﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Application.BaseManage;
using NFine.Code;
using NFine.Domain.Entity.BaseManage;

namespace NFine.Web.Areas.BaseManage.Controllers
{
    public class UnitOfMeasureController : ControllerBase
    {
        private UnitOfMeasureApp unitofmeasureApp = new UnitOfMeasureApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = unitofmeasureApp.GetList(pagination, keyword),
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
            var data = unitofmeasureApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(UnitOfMeasureEntity unitofmeasureEntity, string permissionIds, string keyValue)
        {
            unitofmeasureApp.SubmitForm(unitofmeasureEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            unitofmeasureApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}