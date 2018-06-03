/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Data;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using NFine.Code;
using System.Linq.Expressions;
using System;

namespace NFine.Domain.IRepository.SystemManage
{
    public interface IModuleButtonRepository : IRepositoryBase<ModuleButtonEntity>
    {
        void SubmitCloneButton(List<ModuleButtonEntity> entitys);

        List<ModuleButtonEntity> FindList(Expression<Func<ModuleButtonEntity, bool>> predicate);
        List<ModuleButtonEntity> FindList(Expression<Func<ModuleButtonEntity, bool>> predicate, Pagination pagination, string keyword);
    }
}
