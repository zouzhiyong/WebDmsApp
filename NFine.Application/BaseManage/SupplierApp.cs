/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.BaseManage;
using System.Collections.Generic;
using NFine.Domain.IRepository.Base;
using NFine.Repository.Base;

namespace NFine.Application.BaseManage
{
    public class SupplierApp
    {
        private IRepositoryEntity<SupplierEntity> service = new RepositoryEntity<SupplierEntity>();
        public List<SupplierEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<SupplierEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Name.Contains(keyword));
            }
            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }
            return service.FindList(expression, pagination);
        }
        public SupplierEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(SupplierEntity supplierEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                supplierEntity.Modify(keyValue);
                service.Update(supplierEntity);
            }
            else
            {
                supplierEntity.Create();
                service.Insert(supplierEntity);
            }
        }
    }
}
