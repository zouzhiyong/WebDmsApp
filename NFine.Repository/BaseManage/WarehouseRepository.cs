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

namespace NFine.Repository.BaseManage
{
    public class WarehouseRepository : RepositoryBase<WarehouseEntity>, IWarehouseRepository
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Delete<WarehouseEntity>(t => t.F_Id == keyValue);
                db.Delete<WarehouseUserEntity>(t => t.F_WarehouseId == keyValue);
                db.Commit();
            }
        }

        public void SubmitForm(WarehouseEntity warehouseEntity, List<WarehouseUserEntity> warehouseUserEntitys, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(warehouseEntity);
                }
                else
                {
                    db.Insert(warehouseEntity);
                }
                db.Delete<WarehouseUserEntity>(t => t.F_WarehouseId == warehouseEntity.F_Id);
                db.Insert(warehouseUserEntitys);
                db.Commit();
            }
        }

        public List<WarehouseEntity> FindList(Expression<Func<WarehouseEntity, bool>> predicate)
        {
            var expression = ExtLinq.True<WarehouseEntity>();
            expression.And(predicate);

            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }

            return IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }

        public List<WarehouseEntity> FindList(Expression<Func<WarehouseEntity, bool>> predicate, Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<WarehouseEntity>();
            expression.And(predicate);

            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    expression = expression.And(t => t.F_FullName.Contains(keyword));
            //}

            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }

            return FindList(expression, pagination);
        }
    }
}
