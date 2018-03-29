/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.BaseManage;
using NFine.Domain.IRepository.BaseManage;
using System.Collections.Generic;
using System.Linq;
using NFine.Repository.BaseManage;

namespace NFine.Application.BaseManage
{
    public class UnitOfMeasureApp
    {
        private IUnitOfMeasureRepository service = new UnitOfMeasureRepository();

        public List<UnitOfMeasureEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<UnitOfMeasureEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Name.Contains(keyword));
            }
            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_OrganizeId == CompanyId);
            }
            return service.FindList(expression, pagination);
        }
        public UnitOfMeasureEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(UnitOfMeasureEntity unitofmeasureEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                unitofmeasureEntity.Modify(keyValue);
                service.Update(unitofmeasureEntity);
            }
            else
            {
                unitofmeasureEntity.Create();
                service.Insert(unitofmeasureEntity);
            }
        }
    }
}
