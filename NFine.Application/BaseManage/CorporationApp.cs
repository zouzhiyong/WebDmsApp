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
    public class CorporationApp
    {
        private IRepositoryEntity<CorporationEntity> service = new RepositoryEntity<CorporationEntity>();
        public List<CorporationEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<CorporationEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
            }
            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CorporationId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CorporationId);
            }
            return service.FindList(expression, pagination);
        }
        public CorporationEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        
        public void SubmitForm(CorporationEntity corporationEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                corporationEntity.Modify(keyValue);
                service.Update(corporationEntity);
            }
            else
            {
                corporationEntity.Create();
                service.Insert(corporationEntity);
            }
        }
    }
}
