/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using System.Collections.Generic;
using System.Linq;
using NFine.Repository.SystemManage;
using NFine.Domain.Entity.BaseManage;

namespace NFine.Application.SystemManage
{
    public class CompanyApp
    {
        private ICompanyRepository service = new CompanyRepository();
        private IRoleAuthorizeRepository roleauthorize = new RoleAuthorizeRepository();        
        private IItemsCustDetailRepository itemsCustDetail = new ItemsCustDetailRepository();
        private ModuleApp moduleApp = new ModuleApp();
        private ModuleButtonApp moduleButtonApp = new ModuleButtonApp();
        string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;

        public List<CompanyEntity> GetList()
        {
            var expression = ExtLinq.True<CompanyEntity>();

            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }

            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
            //return service.FindList(t=>true);
        }

        public List<CompanyEntity> GetList(Pagination pagination, string keyword)
        {
            return service.FindList(t => true, pagination, keyword);
        }
        public CompanyEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);            
        }

        public void UpdateForm(CompanyEntity companyEntity)
        {
            service.Update(companyEntity);
        }
        public void SubmitForm(CompanyEntity companyEntity, string[] permissionIds, string keyValue)
        {
            CorporationEntity corporationEntity = new CorporationEntity();

            if (!string.IsNullOrEmpty(keyValue))
            {
                companyEntity.Modify(keyValue);
            }
            else
            {
                companyEntity.Create();                
                companyEntity.F_CorpId = companyEntity.F_Id;

                //创建用户级经销商表
                corporationEntity.Create();
                corporationEntity.F_Id = companyEntity.F_Id;
                corporationEntity.F_CorpId = companyEntity.F_Id;
                corporationEntity.F_FullName = companyEntity.F_FullName;
                corporationEntity.F_EnabledMark = companyEntity.F_EnabledMark;
                corporationEntity.F_FullName = companyEntity.F_FullName;
                corporationEntity.F_Email = companyEntity.F_Email;
                corporationEntity.F_InvAccountPeriod = companyEntity.F_InvAccountPeriod;
                corporationEntity.F_ManagerId = companyEntity.F_ManagerId;
                corporationEntity.F_MobilePhone = companyEntity.F_MobilePhone;
                corporationEntity.F_WeChat = companyEntity.F_WeChat;
                corporationEntity.F_Address = companyEntity.F_Address;
                corporationEntity.F_Fax = companyEntity.F_Fax;
            }         

            var moduledata = moduleApp.GetList();
            var buttondata = moduleButtonApp.GetList();
            
            List<CompanyAuthorizeEntity> companyAuthorizeEntitys = new List<CompanyAuthorizeEntity>();
            List<RoleAuthorizeEntity> roleauthorizeEntitys = new List<RoleAuthorizeEntity>();
            foreach (var itemId in permissionIds)
            {
                CompanyAuthorizeEntity companyAuthorizeEntity = new CompanyAuthorizeEntity();
                companyAuthorizeEntity.Create();
                companyAuthorizeEntity.F_CorpId = companyEntity.F_Id;
                companyAuthorizeEntity.F_ModuleId = itemId;
  
                if (moduledata.Find(t => t.F_Id == itemId) != null)
                {
                    companyAuthorizeEntity.F_ModuleType = 1;
                }
                if (buttondata.Find(t => t.F_Id == itemId) != null)
                {
                    companyAuthorizeEntity.F_ModuleType = 2;
                }
                companyAuthorizeEntitys.Add(companyAuthorizeEntity);                
            }

            var expression = ExtLinq.True<RoleAuthorizeEntity>();
            expression = expression.And(t => !permissionIds.Contains(t.F_ItemId) && t.F_CorpId== companyEntity.F_CorpId);
            roleauthorizeEntitys = roleauthorize.IQueryable(expression).ToList();

            List<ItemsCustDetailEntity> itemsCustDetailEntitys = itemsCustDetail.GetItemNotCustList(companyEntity.F_Id);
            List<ItemsCustDetailEntity> itemsCustDetailEntityList = new List<ItemsCustDetailEntity>();
            foreach (var item in itemsCustDetailEntitys)
            {
                ItemsCustDetailEntity itemsCustDetailEntity = new ItemsCustDetailEntity();
                itemsCustDetailEntity.Create();
                itemsCustDetailEntity.F_Id = item.F_Id;
                itemsCustDetailEntity.F_ItemId = item.F_ItemId;
                itemsCustDetailEntity.F_ParentId = item.F_ParentId;
                itemsCustDetailEntity.F_ItemCode = item.F_ItemCode;
                itemsCustDetailEntity.F_ItemName = item.F_ItemName;
                itemsCustDetailEntity.F_SimpleSpelling = item.F_SimpleSpelling;
                itemsCustDetailEntity.F_IsDefault = item.F_IsDefault;
                itemsCustDetailEntity.F_Layers = item.F_Layers;
                itemsCustDetailEntity.F_SortCode = item.F_SortCode;
                itemsCustDetailEntity.F_DeleteMark = item.F_DeleteMark;
                itemsCustDetailEntity.F_EnabledMark = item.F_EnabledMark;
                itemsCustDetailEntity.F_Description = item.F_Description;
                itemsCustDetailEntity.F_LastModifyTime = item.F_LastModifyTime;
                itemsCustDetailEntity.F_LastModifyUserId = item.F_LastModifyUserId;
                itemsCustDetailEntity.F_DeleteTime = item.F_DeleteTime;
                itemsCustDetailEntity.F_DeleteUserId = item.F_DeleteUserId;
                itemsCustDetailEntity.F_CorpId = companyEntity.F_Id;
                itemsCustDetailEntityList.Add(itemsCustDetailEntity);
            }

            //
            service.SubmitForm(companyEntity, corporationEntity, companyAuthorizeEntitys, roleauthorizeEntitys, itemsCustDetailEntityList, keyValue);
        }
    }
}
