/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using NFine.Domain.IRepository.Base;
using NFine.Repository.Base;

namespace NFine.Application.SystemManage
{
    public class ModuleApp
    {
        private IRepositoryEntity<ModuleEntity> service = new RepositoryEntity<ModuleEntity>();
        private IRepositoryEntity<CompanyAuthorizeEntity> companyauthorize = new RepositoryEntity<CompanyAuthorizeEntity>();
        string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;

        public List<ModuleEntity> GetList()
        {
            var modulDataList = service.IQueryable().OrderBy(t => t.F_SortCode).ToList();
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                return modulDataList;
            }else
            {
                var data = new List<ModuleEntity>();
                var companyauthorizedata = companyauthorize.IQueryable(t => t.F_CorpId == CompanyId && t.F_ModuleType==1);
                foreach (var item in companyauthorizedata)
                {
                    ModuleEntity moduleEntity = modulDataList.Find(t => t.F_Id == item.F_ModuleId);
                    if (moduleEntity != null)
                    {
                        data.Add(moduleEntity);
                    }
                }
                return data.OrderBy(t => t.F_SortCode).ToList();
            }
                
        }
        public ModuleEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.F_Id == keyValue);
            }
        }
        public void SubmitForm(ModuleEntity moduleEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                moduleEntity.Modify(keyValue);
                service.Update(moduleEntity);
            }
            else
            {
                moduleEntity.Create();
                service.Insert(moduleEntity);
            }
        }
    }
}
