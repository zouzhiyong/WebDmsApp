/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using NFine.Code;
using NFine.Data;
using NFine.Domain.Entity.SystemManage;

namespace NFine.Domain.IRepository.Base
{
    public interface IRepositoryEntity<TEntity> : IRepositoryBase<TEntity> where TEntity : class, new()
    {
        new int Insert(TEntity entity);
        new int Insert(List<TEntity> entitys);
        new int Update(TEntity entity);
        new int Delete(TEntity entity);
        new int Delete(Expression<Func<TEntity, bool>> predicate);
        new TEntity FindEntity(object keyValue);
        new TEntity FindEntity(Expression<Func<TEntity, bool>> predicate);
        new IQueryable<TEntity> IQueryable();
        new IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate);
        new List<TEntity> FindList(string strSql);
        new List<TEntity> FindList(string strSql, DbParameter[] dbParameter);
        new List<TEntity> FindList(Pagination pagination);
        List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate);
        new List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Pagination pagination);
        List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>> sortPredicate);
    }
}
