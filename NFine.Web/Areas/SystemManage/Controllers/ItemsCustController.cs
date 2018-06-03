/*******************************************************************************
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
    public class ItemsCustController : ControllerBase
    {
        private ItemsCustDetailApp itemsCustDetailApp = new ItemsCustDetailApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string itemId, string keyword)
        {
            var data = itemsCustDetailApp.GetList(itemId, keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string enCode)
        {
            string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;            
            var data = itemsCustDetailApp.GetItemList(enCode, CompanyId);
            List<object> list = new List<object>();
            foreach (ItemsCustDetailEntity item in data)
            {
                list.Add(new { id = item.F_ItemCode, text = item.F_ItemName });
            }
            return Content(list.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = itemsCustDetailApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [MyValidateAntiForgeryToken]
        public ActionResult SubmitForm(ItemsCustDetailEntity itemsCustDetailEntity, string keyValue)
        {
            itemsCustDetailApp.SubmitForm(itemsCustDetailEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [MyValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            itemsCustDetailApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
