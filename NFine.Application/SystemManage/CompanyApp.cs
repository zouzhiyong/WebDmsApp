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

namespace NFine.Application.SystemManage
{
    public class CompanyApp
    {
        private ICompanyRepository service = new CompanyRepository();
        private ModuleApp moduleApp = new ModuleApp();
        private ModuleButtonApp moduleButtonApp = new ModuleButtonApp();

        public List<CompanyEntity> GetList()
        {
            var expression = ExtLinq.True<CompanyEntity>();
            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }

        public List<CompanyEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<CompanyEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
            }
            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }
            return service.FindList(expression, pagination);
        }
        public CompanyEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);            
        }
        public void SubmitForm(CompanyEntity companyEntity, string[] permissionIds, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                companyEntity.Modify(keyValue);
            }
            else
            {
                companyEntity.Create();
                companyEntity.F_CorpId = companyEntity.F_Id;
            }         

            var moduledata = moduleApp.GetList();
            var buttondata = moduleButtonApp.GetList();
            List<CompanyAuthorizeEntity> companyAuthorizeEntitys = new List<CompanyAuthorizeEntity>();
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
            service.SubmitForm(companyEntity, companyAuthorizeEntitys, keyValue);
        }
    }
}
