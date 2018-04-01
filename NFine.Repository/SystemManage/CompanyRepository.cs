/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System.Collections.Generic;
using NFine.Data;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;

namespace NFine.Repository.SystemManage
{
    public class CompanyRepository : RepositoryBase<CompanyEntity>, ICompanyRepository
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Delete<CompanyEntity>(t => t.F_Id == keyValue);
                db.Delete<CompanyAuthorizeEntity>(t => t.F_CorpId == keyValue);
                db.Commit();
            }
        }
        public void SubmitForm(CompanyEntity companyEntity, List<CompanyAuthorizeEntity> companyAuthorizeEntitys, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(companyEntity);
                }
                else
                {
                    //companyEntity.F_Category = 1;
                    db.Insert(companyEntity);
                }
                db.Delete<CompanyAuthorizeEntity>(t => t.F_CorpId == companyEntity.F_Id);
                db.Insert(companyAuthorizeEntitys);
                db.Commit();
            }
        }
    }
}
