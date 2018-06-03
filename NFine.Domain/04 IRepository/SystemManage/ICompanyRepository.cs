/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NFine.Code;
using NFine.Data;
using NFine.Domain.Entity.BaseManage;
using NFine.Domain.Entity.SystemManage;

namespace NFine.Domain.IRepository.SystemManage
{
    public interface ICompanyRepository : IRepositoryBase<CompanyEntity>
    {
        List<CompanyEntity> FindList(Expression<Func<CompanyEntity, bool>> predicate);
        List<CompanyEntity> FindList(Expression<Func<CompanyEntity, bool>> predicate, Pagination pagination,string keyword);
        void DeleteForm(string keyValue);
        void SubmitForm(CompanyEntity companyEntity, CorporationEntity corporationEntity, List<CompanyAuthorizeEntity> companyAuthorizeEntitys, List<RoleAuthorizeEntity> roleAuthorizeEntitys, List<ItemsCustDetailEntity> itemsCustDetailEntitys, string keyValue);
    }
}
