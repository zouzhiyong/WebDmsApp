/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Application.BaseManage;
using NFine.Code;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NFine.Application.SystemManage;
using NFine.Domain.Entity.BaseManage;
using NFine.Domain.Entity.SystemManage;

namespace NFine.Web.Areas.BaseManage.Controllers
{
    public class MaterialUomController : ControllerBase
    {
        private MaterialUomApp materialuomApp = new MaterialUomApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string keyword)
        {
            var data = materialuomApp.GetList(keyword);
            return Content(data.ToJson());
        }
    }
}
