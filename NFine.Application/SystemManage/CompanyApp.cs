/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using NFine.Domain.Entity.BaseManage;
using NFine.Repository.Base;
using NFine.Domain.IRepository.Base;
using System.Text;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace NFine.Application.SystemManage
{
    public class CompanyApp
    {
        private IRepositoryEntity<CompanyEntity> service = new RepositoryEntity<CompanyEntity>();
        private IRepositoryEntity<RoleAuthorizeEntity> roleauthorize = new RepositoryEntity<RoleAuthorizeEntity>();
        private IRepositoryEntity<ItemsCustDetailEntity> itemsCustDetail = new RepositoryEntity<ItemsCustDetailEntity>();

        private ModuleApp moduleApp = new ModuleApp();
        private ModuleButtonApp moduleButtonApp = new ModuleButtonApp();

        public List<CompanyEntity> GetList()
        {
            var expression = ExtLinq.True<CompanyEntity>();
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }

        public List<CompanyEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<CompanyEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }
        public CompanyEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            //service.DeleteForm(keyValue);    
            using (var db = new RepositoryEntity().BeginTrans())
            {
                db.Delete<CompanyEntity>(t => t.F_Id == keyValue);
                db.Delete<CompanyAuthorizeEntity>(t => t.F_CorpId == keyValue);//删除用户权限表     
                db.Delete<CorporationEntity>(t => t.F_CorpId == keyValue);//删除用户级销商表
                db.Commit();
            }
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
                companyEntity.F_CorpId = companyEntity.F_Id;
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

            List<ItemsCustDetailEntity> itemsCustDetailEntitys = GetItemNotCustList(companyEntity.F_Id);
            List<ItemsCustDetailEntity> itemsCustDetailEntityList = new List<ItemsCustDetailEntity>();
            foreach (var item in itemsCustDetailEntitys)
            {
                var F_Id = item.F_Id;
                item.Create();
                item.F_Id = F_Id;
                item.F_CorpId = companyEntity.F_Id;
                itemsCustDetailEntityList.Add(item);
            }

            //
            //service.SubmitForm(companyEntity, corporationEntity, companyAuthorizeEntitys, roleauthorizeEntitys, itemsCustDetailEntityList, keyValue);

            using (var db = new RepositoryEntity().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(companyEntity);
                }
                else
                {
                    db.Insert(companyEntity);
                    db.Insert(corporationEntity);
                }
                db.Delete<CompanyAuthorizeEntity>(t => t.F_CorpId == companyEntity.F_Id);//删除用户权限表                

                foreach (var item in roleauthorizeEntitys)
                {
                    db.Delete<RoleAuthorizeEntity>(t => t.F_Id == item.F_Id);
                }

                db.Insert(companyAuthorizeEntitys);

                db.Insert(itemsCustDetailEntityList);

                db.Commit();
            }
        }


        private List<ItemsCustDetailEntity> GetItemNotCustList(string F_CorpId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  d.*
                            FROM    Sys_ItemsDetail d
                                    Left  JOIN Sys_ItemsCustDetail i ON i.F_Id = d.F_Id AND i.F_CorpId=@F_CorpId
                            WHERE   1 = 1
                                    AND d.F_IsDefault = 1
                                    AND i.F_Id is null
                            ORDER BY d.F_SortCode ASC");
            DbParameter[] parameter =
            {
                 new MySqlParameter("@F_CorpId",F_CorpId)
            };
            return itemsCustDetail.FindList(strSql.ToString(), parameter);
        }
    }
}
