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
using System.Text;
using System.Web.Mvc;
using NFine.Application.BaseManage;
using NFine.Domain.Entity.BaseManage;
using NFine.Data;

namespace NFine.Web.Controllers
{
    [HandlerLogin]
    public class ClientsDataController : Controller
    {
        private string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
        private IRepositoryBase service = new RepositoryBase();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetClientsDataJson()
        {
            var data = new
            {
                dataItems = this.GetDataItemList(),
                company = this.GetDataCompanyList(),
                department = this.GetDataDepartmentList(),
                unit = this.GetDataUnitList(),
                //organize = this.GetOrganizeList(),
                role = this.GetRoleList(),
                duty = this.GetDutyList(),
                user = this.GetUserList(),
                authorizeMenu = this.GetMenuList(),
                authorizeButton = this.GetMenuButtonList()
            };
            return Content(data.ToJson());
        }
        private object GetDataItemList()
        {
            Dictionary<string, object> dictionaryItem = new Dictionary<string, object>();

            List<ItemsEntity> itemList = service.IQueryable<ItemsEntity>().ToList();
            List<ItemsDetailEntity> itemData = service.IQueryable<ItemsDetailEntity>().ToList();
            List<ItemsCustDetailEntity> itemCustData = service.IQueryable<ItemsCustDetailEntity>(t => t.F_CorpId == CompanyId).ToList();
            string GeneralId = itemList.First(t => t.F_EnCode == "Sys_Items").F_Id;

            foreach (var item in itemList)
            {
                if (OperatorProvider.Provider.GetCurrent().IsSystem)
                {
                    List<ItemsDetailEntity> itemdata = new ItemsDetailApp().GetList();
                    var dataItemList = itemdata.FindAll(t => t.F_ItemId == item.F_Id);
                    Dictionary<string, string> dictionaryItemList = new Dictionary<string, string>();
                    foreach (var _item in dataItemList)
                    {
                        dictionaryItemList.Add(_item.F_ItemCode, _item.F_ItemName);
                    }
                    dictionaryItem.Add(item.F_EnCode, dictionaryItemList);
                }
                else
                {
                    Dictionary<string, string> dictionaryItemList = new Dictionary<string, string>();                    
                    var dataItemList = itemData.FindAll(t => t.F_ItemId == item.F_Id && item.F_ParentId == GeneralId);
                    foreach (var _item in dataItemList)
                    {
                        dictionaryItemList.Add(_item.F_ItemCode, _item.F_ItemName);
                    }
                    var dataItemCustList = itemCustData.FindAll(t => t.F_ItemId == item.F_Id);
                    foreach (var _item in dataItemCustList)
                    {
                        dictionaryItemList.Add(_item.F_ItemCode, _item.F_ItemName);
                    }
                    //List<ItemsDetailEntity> itemData = new ItemsDetailApp().GetList();
                    //var dataItemList = itemData.FindAll(t => t.F_ItemId == item.F_Id && item.F_EnCode == "Sys_Items");
                    //List<ItemsCustDetailEntity> itemCustData = new ItemsCustDetailApp().GetList();
                    //var dataItemCustList = itemCustData.FindAll(t => t.F_ItemId == item.F_Id);
                    //Dictionary<string, string> dictionaryItemList = new Dictionary<string, string>();
                    //foreach (var itemList in dataItemList)
                    //{
                    //    dictionaryItemList.Add(itemList.F_ItemCode, itemList.F_ItemName);
                    //}
                    //foreach (var itemList in dataItemCustList)
                    //{
                    //    dictionaryItemList.Add(itemList.F_ItemCode, itemList.F_ItemName);
                    //}
                    dictionaryItem.Add(item.F_EnCode, dictionaryItemList);
                }
            }
            return dictionaryItem;
        }
        //private object GetOrganizeList()
        //{
        //    OrganizeApp organizeApp = new OrganizeApp();
        //    var data = organizeApp.GetList();
        //    Dictionary<string, object> dictionary = new Dictionary<string, object>();
        //    foreach (OrganizeEntity item in data)
        //    {
        //        var fieldItem = new
        //        {
        //            encode = item.F_EnCode,
        //            fullname = item.F_FullName
        //        };
        //        dictionary.Add(item.F_Id, fieldItem);
        //    }
        //    return dictionary;
        //}
        private object GetRoleList()
        {
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                return null;
            }
            RoleApp roleApp = new RoleApp();
            var data = roleApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (RoleEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_EnCode,
                    fullname = item.F_FullName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }
        private object GetDataCompanyList()
        {
            CompanyApp companyApp = new CompanyApp();
            var data = companyApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (CompanyEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_CorpId,
                    fullname = item.F_FullName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }
        private object GetDataDepartmentList()
        {
            DepartmentApp departmentApp = new DepartmentApp();
            var data = departmentApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (DepartmentEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_Id,
                    fullname = item.F_FullName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }
        private object GetUserList()
        {
            UserApp userApp = new UserApp();
            var data = userApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (UserEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_Account,
                    fullname = item.F_RealName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }
        private object GetDataUnitList()
        {
            UnitOfMeasureApp unitofmeasureApp = new UnitOfMeasureApp();
            var data = unitofmeasureApp.GetList().Where(t => t.F_EnabledMark == true);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (UnitOfMeasureEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_Id,
                    fullname = item.F_Name
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }
        private object GetDutyList()
        {
            DutyApp dutyApp = new DutyApp();
            var data = dutyApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (RoleEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_EnCode,
                    fullname = item.F_FullName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }
        private object GetMenuList()
        {
            var roleId = OperatorProvider.Provider.GetCurrent().RoleId;

            List<ModuleEntity> entitys = null;
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                entitys = new RoleAuthorizeApp().GetMenuList(roleId).FindAll(t => t.F_IsAdmin == true);
            }
            else
            {
                entitys = new RoleAuthorizeApp().GetMenuList(roleId);
            }

            return ToMenuJson(entitys, "0");
        }
        private string ToMenuJson(List<ModuleEntity> data, string parentId)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("[");
            List<ModuleEntity> entitys = data.FindAll(t => t.F_ParentId == parentId);
            if (entitys.Count > 0)
            {
                foreach (var item in entitys)
                {
                    if (item.F_IsMenu == true)
                    {
                        string strJson = item.ToJson();
                        strJson = strJson.Insert(strJson.Length - 1, ",\"ChildNodes\":" + ToMenuJson(data, item.F_Id) + "");
                        sbJson.Append(strJson + ",");
                    }
                }
                sbJson = sbJson.Remove(sbJson.Length - 1, 1);
            }
            sbJson.Append("]");
            return sbJson.ToString();
        }
        private object GetMenuButtonList()
        {
            var roleId = OperatorProvider.Provider.GetCurrent().RoleId;
            var data = new RoleAuthorizeApp().GetButtonList(roleId);
            var dataModuleId = data.Distinct(new ExtList<ModuleButtonEntity>("F_ModuleId"));
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (ModuleButtonEntity item in dataModuleId)
            {
                var buttonList = data.Where(t => t.F_ModuleId.Equals(item.F_ModuleId) && t.F_EnabledMark == true);
                dictionary.Add(item.F_ModuleId, buttonList);
            }
            return dictionary;
        }
    }
    
}
