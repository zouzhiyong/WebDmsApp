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

namespace NFine.Application.BaseManage
{
    public class CompanyApp
    {
        private ICompanyRepository service = new CompanyRepository();

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
            service.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(CompanyEntity companyEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                companyEntity.Modify(keyValue);
                companyEntity.F_CorpId = companyEntity.F_Id;
                service.Update(companyEntity);
            }
            //else
            //{
            //    companyEntity.Create();
            //    service.Insert(companyEntity);
            //}
        }
    }
}
