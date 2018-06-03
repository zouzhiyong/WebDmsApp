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
using NFine.Domain.Entity.SystemManage;

namespace NFine.Domain.IRepository.SystemManage
{
    public interface IModuleRepository : IRepositoryBase<ModuleEntity>
    {
        List<ModuleEntity> FindList(Expression<Func<ModuleEntity, bool>> predicate);
        List<ModuleEntity> FindList(Expression<Func<ModuleEntity, bool>> predicate, Pagination pagination, string keyword);
    }
}
