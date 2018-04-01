/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFine.Application.SystemManage
{
    public class ModuleButtonApp
    {
        private IModuleButtonRepository service = new ModuleButtonRepository();
        private ICompanyAuthorizeRepository companyauthorize = new CompanyAuthorizeRepository();
        string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;

        public List<ModuleButtonEntity> GetList(string moduleId = "")
        {
            var expression = ExtLinq.True<ModuleButtonEntity>();
            if (!string.IsNullOrEmpty(moduleId))
            {
                expression = expression.And(t => t.F_ModuleId == moduleId);
            }
            var modulebuttonDataList =  service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();

            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                return modulebuttonDataList;
            }
            else
            {
                var data = new List<ModuleButtonEntity>();
                var companyauthorizedata = companyauthorize.IQueryable(t => t.F_CorpId == CompanyId && t.F_ModuleType == 2);
                foreach (var item in companyauthorizedata)
                {
                    ModuleButtonEntity modulebuttonEntity = modulebuttonDataList.Find(t => t.F_Id == item.F_ModuleId);
                    if (modulebuttonEntity != null)
                    {
                        data.Add(modulebuttonEntity);
                    }
                }
                return data.OrderBy(t => t.F_SortCode).ToList();
            }
        }
        public ModuleButtonEntity GetForm(string keyValue)
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
        public void SubmitForm(ModuleButtonEntity moduleButtonEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                moduleButtonEntity.Modify(keyValue);
                service.Update(moduleButtonEntity);
            }
            else
            {
                moduleButtonEntity.Create();
                service.Insert(moduleButtonEntity);
            }
        }
        public void SubmitCloneButton(string moduleId, string Ids)
        {
            string[] ArrayId = Ids.Split(',');
            var data = this.GetList();
            List<ModuleButtonEntity> entitys = new List<ModuleButtonEntity>();
            foreach (string item in ArrayId)
            {
                ModuleButtonEntity moduleButtonEntity = data.Find(t => t.F_Id == item);
                moduleButtonEntity.F_Id = Common.GuId();
                moduleButtonEntity.F_ModuleId = moduleId;
                entitys.Add(moduleButtonEntity);
            }
            service.SubmitCloneButton(entitys);
        }
    }
}
