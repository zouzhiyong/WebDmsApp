/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Data;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System.Collections.Generic;
using System.Linq.Expressions;
using NFine.Code;
using System;
using System.Linq;

namespace NFine.Repository.SystemManage
{
    public class ModuleButtonRepository : RepositoryBase<ModuleButtonEntity>, IModuleButtonRepository
    {
        public void SubmitCloneButton(List<ModuleButtonEntity> entitys)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                foreach (var item in entitys)
                {
                    db.Insert(item);
                }
                db.Commit();
            }
        }

        public List<ModuleButtonEntity> FindList(Expression<Func<ModuleButtonEntity, bool>> predicate)
        {
            var expression = ExtLinq.True<ModuleButtonEntity>();
            expression.And(predicate);

            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }

            return IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }

        public List<ModuleButtonEntity> FindList(Expression<Func<ModuleButtonEntity, bool>> predicate, Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ModuleButtonEntity>();
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
    }
}
