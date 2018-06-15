/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NFine.Code;
using NFine.Data;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;

namespace NFine.Repository.SystemManage
{
    public class DepartmentRepository<TEntity> : RepositoryBase<TEntity>, IDepartmentRepository<TEntity> where TEntity : class, new()
    {
        public List<TEntity> FindList1(Expression<Func<TEntity, bool>> predicate)
        {
            return IQueryable(predicate).ToList();
        }

        public List<TEntity> FindList1(Expression<Func<TEntity, bool>> predicate, Pagination pagination)
        {
            return FindList(predicate, pagination);
        }
    }
}
