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
    public class WarehouseUserController : ControllerBase
    {
        private WarehouseUserApp warehouseuserApp = new WarehouseUserApp();
        private UserApp userApp = new UserApp();
        //public ActionResult GetGridJson(string warehouseId)
        //{
        //    var userdata = userApp.GetList();
        //    var warehouseuserdata = new List<WarehouseUserEntity>();
        //    if (!string.IsNullOrEmpty(warehouseId))
        //    {
        //        warehouseuserdata = warehouseuserApp.GetList(warehouseId);
        //    }

        //    foreach (UserEntity item in userdata)
        //    {
        //        //TreeViewModel tree = new TreeViewModel();
        //        //bool hasChildren = moduledata.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
        //        //tree.id = item.F_Id;
        //        //tree.text = item.F_FullName;
        //        //tree.value = item.F_EnCode;
        //        //tree.parentId = item.F_ParentId;
        //        //tree.isexpand = true;
        //        //tree.complete = true;
        //        //tree.showcheck = true;
        //        //tree.checkstate = authorizedata.Count(t => t.F_ItemId == item.F_Id);
        //        //tree.hasChildren = true;
        //        //tree.img = item.F_Icon == "" ? "" : item.F_Icon;
        //        //treeList.Add(tree);
        //    }
            
        //    return Content(treeList.TreeViewJson());
        //}
    }
}
