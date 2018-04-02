﻿/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System.Collections.Generic;
using NFine.Data;
using NFine.Domain.Entity.BaseManage;
using NFine.Domain.Entity.SystemManage;

namespace NFine.Domain.IRepository.SystemManage
{
    public interface ICompanyRepository : IRepositoryBase<CompanyEntity>
    {
        void DeleteForm(string keyValue);
        void SubmitForm(CompanyEntity companyEntity, CorporationEntity corporationEntity, List<CompanyAuthorizeEntity> companyAuthorizeEntitys, List<RoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue);
    }
}