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
    public class RepositoryEntity<TEntity> : RepositoryBase<TEntity>, IRepositoryEntity<TEntity> where TEntity : class, new()
    {
        private IRepositoryBase<TEntity> repositoryBase = new RepositoryBase<TEntity>();

        public new int Insert(TEntity entity)
        {
            return repositoryBase.Insert(entity);
        }
        public new int Insert(List<TEntity> entitys)
        {
            return repositoryBase.Insert(entitys);
        }
        public new int Update(TEntity entity)
        {
            return repositoryBase.Update(entity);
        }
        public new int Delete(TEntity entity)
        {
            entity = deleteEntity(entity);            
            return repositoryBase.Delete(entity);
        }
        public new int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            predicate = expression(predicate);
            return repositoryBase.Delete(predicate);
        }
        public new TEntity FindEntity(object keyValue)
        {
            return repositoryBase.FindEntity(keyValue);
        }
        public new TEntity FindEntity(Expression<Func<TEntity, bool>> predicate)
        {
            predicate = expression(predicate);
            return repositoryBase.FindEntity(predicate);
        }
        public new IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            predicate = expression(predicate);
            return repositoryBase.IQueryable(predicate);
        }
        public new List<TEntity> FindList(string strSql)
        {
            return repositoryBase.FindList(strSql);
        }
        public new List<TEntity> FindList(string strSql, DbParameter[] dbParameter)
        {
            return repositoryBase.FindList(strSql, dbParameter);
        }
        public new List<TEntity> FindList(Pagination pagination)
        {            
            return FindList(pagination);
        }
        public List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate)
        {
            return IQueryable(predicate).ToList();
        }
        public new List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Pagination pagination)
        {
            predicate = expression(predicate);            
            return repositoryBase.FindList(predicate, pagination);
        }
        public List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>> sortPredicate)
        {
            return ExtLinq.SortBy(IQueryable(predicate), sortPredicate).ToList();
        }


        private Expression<Func<TEntity, bool>> expression(Expression<Func<TEntity, bool>> predicate)
        {
            if (OperatorProvider.Provider.GetCurrent() != null)
            {
                if (!OperatorProvider.Provider.GetCurrent().IsSystem)
                {
                    ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "t");//创建参数
                    if (parameter.Type.Name != "ItemsEntity" && parameter.Type.Name != "ModuleButtonEntity" && parameter.Type.Name != "SerialNumberEntity")//除类型主表和模板对应按钮外，其它表查询都需要加上公司ID条件
                    {
                        MemberExpression member = Expression.PropertyOrField(parameter, "F_CorpId");
                        ConstantExpression constant = Expression.Constant(OperatorProvider.Provider.GetCurrent().CompanyId);//创建常数
                        var lambda = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(member, constant), parameter);
                        predicate = predicate.And(lambda);
                    }
                }
            }
            return predicate;
        }
               
        private TEntity deleteEntity(TEntity entity)
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
