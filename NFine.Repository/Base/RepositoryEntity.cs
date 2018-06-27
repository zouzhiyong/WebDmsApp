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
using NFine.Domain.IRepository.Base;

namespace NFine.Repository.Base
{
    public class RepositoryEntity : RepositoryBase, IRepositoryEntity
    {
        private IRepositoryBase repositoryBase = new RepositoryBase();
        public new IRepositoryEntity BeginTrans()
        {
            repositoryBase.BeginTrans();
            return this;
        }
        public new int Commit()
        {
            return repositoryBase.Commit();
        }
        public new void Dispose()
        {
            repositoryBase.Dispose();
        }

        public new int Insert<TEntity>(TEntity entity) where TEntity : class
        {
            return repositoryBase.Insert(entity);
        }
        public new int Insert<TEntity>(List<TEntity> entitys) where TEntity : class
        {
            return repositoryBase.Insert(entitys);
        }
        public new int Update<TEntity>(TEntity entity) where TEntity : class
        {
            return repositoryBase.Update(entity);
        }
        public new int Delete<TEntity>(TEntity entity) where TEntity : class
        {
            entity = deleteEntity(entity);            
            return repositoryBase.Delete(entity);
        }
        public new int Delete<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            predicate = expression(predicate);
            return repositoryBase.Delete(predicate);
        }
        public new TEntity FindEntity<TEntity>(object keyValue) where TEntity : class
        {
            return repositoryBase.FindEntity<TEntity>(keyValue);
        }
        public new TEntity FindEntity<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            predicate = expression<TEntity>(predicate);
            return repositoryBase.FindEntity(predicate);
        }
        public new IQueryable<TEntity> IQueryable<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            predicate = expression(predicate);
            return repositoryBase.IQueryable(predicate);
        }
        public new List<TEntity> FindList<TEntity>(string strSql) where TEntity : class
        {
            return repositoryBase.FindList<TEntity>(strSql);
        }
        public new List<TEntity> FindList<TEntity>(string strSql, DbParameter[] dbParameter) where TEntity : class
        {
            return repositoryBase.FindList<TEntity>(strSql, dbParameter);
        }
        public new List<TEntity> FindList<TEntity>(Pagination pagination) where TEntity : class, new()
        {            
            return repositoryBase.FindList<TEntity>(pagination);
        }
        public List<TEntity> FindList<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return IQueryable(predicate).ToList();
        }
        public new List<TEntity> FindList<TEntity>(Expression<Func<TEntity, bool>> predicate, Pagination pagination) where TEntity : class, new()
        {
            predicate = expression(predicate);            
            return repositoryBase.FindList(predicate, pagination);
        }
        public List<TEntity> FindList<TEntity>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>> sortPredicate) where TEntity : class, new()
        {
            return ExtLinq.SortBy(IQueryable(predicate), sortPredicate).ToList();
        }


        private Expression<Func<TEntity, bool>> expression<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            if (OperatorProvider.Provider.GetCurrent() != null)
            {
                //if (!OperatorProvider.Provider.GetCurrent().IsSystem)
                //{
                    ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "t");//创建参数
                    if (parameter.Type.Name != "ItemsEntity" && parameter.Type.Name != "ModuleButtonEntity" && parameter.Type.Name!= "SerialNumberEntity")//除类型主表和模板对应按钮外，其它表查询都需要加上公司ID条件
                    {
                        MemberExpression member = Expression.PropertyOrField(parameter, "F_CorpId");
                        ConstantExpression constant = Expression.Constant(OperatorProvider.Provider.GetCurrent().CompanyId);//创建常数
                        var lambda = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(member, constant), parameter);
                        predicate = predicate.And(lambda);
                    }
                //}
            }
            return predicate;
        }
        private TEntity deleteEntity<TEntity>(TEntity entity) where TEntity : class
        {
            foreach (var pro in entity.GetType().GetProperties())
            {
                var LoginInfo = OperatorProvider.Provider.GetCurrent();
                if (LoginInfo != null)
                {
                    if (pro.Name.Equals("F_CorpId"))
                    {
                        pro.SetValue(entity, LoginInfo.CompanyId, null);
                    }
                }
            }
            return entity;
        }
    }
}
