/*******************************************************************************
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
using System.Web.Script.Serialization;
using System.Runtime.Serialization;
using NFine.Web.App_Start._01_Handler;

namespace NFine.Web.Areas.BaseManage.Controllers
{
    public class MaterialController : ControllerBase
    {
        private MaterialApp materialApp = new MaterialApp();
        private MaterialUomApp materialUomApp = new MaterialUomApp();

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
            var materia_data = materialApp.GetForm(keyValue);
            var materiaUom_data = materialUomApp.GetList(keyValue);
            return Content(new { data1= materia_data ,data2= materiaUom_data }.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [MyValidateAntiForgeryToken]
        public ActionResult SubmitForm(MaterialEntitys model, string keyValue)
        {
            materialApp.SubmitForm(model.F_MaterialEntity, model.F_MaterialUomEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [MyValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            materialApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }

    public class MaterialEntitys
    {
        public MaterialEntity F_MaterialEntity { get; set; }
        public MaterialUomEntity[] F_MaterialUomEntity { get; set; }
    }
}
