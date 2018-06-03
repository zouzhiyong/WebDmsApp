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

namespace NFine.Domain.IRepository.BaseManage
{
    public interface ICorporationRepository : IRepositoryBase<CorporationEntity>
    {
        List<CorporationEntity> FindList(Expression<Func<CorporationEntity, bool>> predicate);
        List<CorporationEntity> FindList(Expression<Func<CorporationEntity, bool>> predicate, Pagination pagination, string keyword);
    }
}
