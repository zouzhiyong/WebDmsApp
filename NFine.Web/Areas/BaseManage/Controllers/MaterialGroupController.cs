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

namespace NFine.Web.Areas.BaseManage.Controllers
{
    public class MaterialGroupController : ControllerBase
    {
        private MaterialGroupApp materialGroupApp = new MaterialGroupApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string keyValue)
        {
            var data = materialGroupApp.GetList(keyValue);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = materialGroupApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (MaterialGroupEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.F_FullName;
                treeModel.parentId = item.F_ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeJson()
        {
            var data = materialGroupApp.GetList();
            var treeList = new List<TreeViewModel>();
            foreach (MaterialGroupEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
                tree.id = item.F_Id;
                tree.text = item.F_FullName;
                tree.value = item.F_EnCode;
                tree.parentId = item.F_ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeGridJson()
        {
            var data = materialGroupApp.GetList();
            var treeList = new List<TreeGridModel>();
            foreach (MaterialGroupEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
                treeModel.id = item.F_Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.F_ParentId;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = materialGroupApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(MaterialGroupEntity materialGroupEntity, string keyValue)
        {
            materialGroupApp.SubmitForm(materialGroupEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            materialGroupApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
