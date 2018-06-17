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
    public interface IRepositoryEntity : IRepositoryBase
    {
        new IRepositoryEntity BeginTrans();
        new int Commit();
        new int Insert<TEntity>(TEntity entity) where TEntity : class;
        new int Insert<TEntity>(List<TEntity> entitys) where TEntity : class;
        new int Update<TEntity>(TEntity entity) where TEntity : class;
        new int Delete<TEntity>(TEntity entity) where TEntity : class;
        new int Delete<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        new TEntity FindEntity<TEntity>(object keyValue) where TEntity : class;
        new TEntity FindEntity<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        new IQueryable<TEntity> IQueryable<TEntity>() where TEntity : class;
        new IQueryable<TEntity> IQueryable<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        new List<TEntity> FindList<TEntity>(string strSql) where TEntity : class;
        new List<TEntity> FindList<TEntity>(string strSql, DbParameter[] dbParameter) where TEntity : class;
        new List<TEntity> FindList<TEntity>(Pagination pagination) where TEntity : class, new();
        List<TEntity> FindList<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        new List<TEntity> FindList<TEntity>(Expression<Func<TEntity, bool>> predicate, Pagination pagination) where TEntity : class, new();
        List<TEntity> FindList<TEntity>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>> sortPredicate) where TEntity : class, new();
    }
}
