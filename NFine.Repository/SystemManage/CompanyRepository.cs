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
using NFine.Domain.Entity.BaseManage;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;

namespace NFine.Repository.SystemManage
{
    public class CompanyRepository : RepositoryBase<CompanyEntity>, ICompanyRepository
    {
        public List<CompanyEntity> FindList(Expression<Func<CompanyEntity, bool>> predicate)
        {
            var expression = ExtLinq.True<CompanyEntity>();
            expression.And(predicate);

            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }

            return IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }

        public List<CompanyEntity> FindList(Expression<Func<CompanyEntity, bool>> predicate, Pagination pagination,string keyword)
        {
            var expression = ExtLinq.True<CompanyEntity>();
            expression.And(predicate);

            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
            }

            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }

            return FindList(expression, pagination);
        }

        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Delete<CompanyEntity>(t => t.F_Id == keyValue);
                db.Delete<CompanyAuthorizeEntity>(t => t.F_CorpId == keyValue);//删除用户权限表     
                db.Delete<CorporationEntity>(t => t.F_CorpId == keyValue);//删除用户级销商表
                db.Commit();
            }
        }
        public void SubmitForm(CompanyEntity companyEntity, CorporationEntity corporationEntity, List<CompanyAuthorizeEntity> companyAuthorizeEntitys, List<RoleAuthorizeEntity> roleAuthorizeEntitys, List<ItemsCustDetailEntity> itemsCustDetailEntitys, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(companyEntity);
                }
                else
                {
                    db.Insert(companyEntity);
                    db.Insert(corporationEntity);
                }
                db.Delete<CompanyAuthorizeEntity>(t => t.F_CorpId == companyEntity.F_Id);//删除用户权限表                

                foreach (var item in roleAuthorizeEntitys)
                {
                    db.Delete<RoleAuthorizeEntity>(t => t.F_Id == item.F_Id);                    
                }            
                
                db.Insert(companyAuthorizeEntitys);

                db.Insert(itemsCustDetailEntitys);

                db.Commit();
            }
        }
    }
}
