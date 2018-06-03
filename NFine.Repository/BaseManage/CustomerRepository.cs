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
using NFine.Domain.IRepository.BaseManage;
using NFine.Repository.BaseManage;

namespace NFine.Repository.BaseManage
{
    public class CustomerRepository : RepositoryBase<CustomerEntity>, ICustomerRepository
    {
        public List<CustomerEntity> FindList(Expression<Func<CustomerEntity, bool>> predicate)
        {
            var expression = ExtLinq.True<CustomerEntity>();
            expression.And(predicate);

            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }

            return IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }

        public List<CustomerEntity> FindList(Expression<Func<CustomerEntity, bool>> predicate, Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<CustomerEntity>();
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
