﻿/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NFine.Web.App_Start._01_Handler;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class DutyController : ControllerBase
    {
        private DutyApp dutyApp = new DutyApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string F_CorpId)
        {
            var data = dutyApp.GetSelect(F_CorpId);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string keyword)
        {
            var data = dutyApp.GetList(keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = dutyApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [MyValidateAntiForgeryToken]
        public ActionResult SubmitForm(RoleEntity roleEntity, string keyValue)
        {
            dutyApp.SubmitForm(roleEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [MyValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            dutyApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
