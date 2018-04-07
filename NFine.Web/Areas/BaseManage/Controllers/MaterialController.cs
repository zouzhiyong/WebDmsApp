﻿/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NFine.Domain.Entity.BaseManage;
using NFine.Application.BaseManage;

namespace NFine.Web.Areas.BaseManage.Controllers
{
    public class MaterialController : ControllerBase
    {
        private MaterialApp materialApp = new MaterialApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string itemId, string keyword)
        {
            var data = materialApp.GetList(itemId, keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string enCode)
        {
            var data = materialApp.GetItemList(enCode);
            List<object> list = new List<object>();
            foreach (MaterialEntity item in data)
            {
                list.Add(new { id = item.F_EnCode, text = item.F_FullName });
            }
            return Content(list.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = materialApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(MaterialEntity materialEntity, List<MaterialUomEntity>  materialuomEntitys, string keyValue)
        {
            materialApp.SubmitForm(materialEntity, materialuomEntitys, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            materialApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
