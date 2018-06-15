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
    public interface IDepartmentRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class, new()
    {
        List<TEntity> FindList1(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> FindList1(Expression<Func<TEntity, bool>> predicate, Pagination pagination);
    }
}
