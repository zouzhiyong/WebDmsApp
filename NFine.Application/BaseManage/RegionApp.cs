/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Domain.Entity.BaseManage;
using System;
using System.Collections.Generic;
using System.Linq;
using NFine.Code;
using NFine.Domain.IRepository.Base;
using NFine.Repository.Base;

namespace NFine.Application.BaseManage
{
    public class RegionApp
    {
        private IRepositoryEntity<RegionEntity> service = new RepositoryEntity<RegionEntity>();

        public List<RegionEntity> GetList()
        {
            var expression = ExtLinq.True<RegionEntity>();
            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }
            return service.IQueryable(expression).ToList();
        }
        public RegionEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.F_Id == keyValue);
            }
        }
        public void SubmitForm(RegionEntity regionEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                regionEntity.Modify(keyValue);
                service.Update(regionEntity);
            }
            else
            {
                regionEntity.Create();
                service.Insert(regionEntity);
            }
        }
    }
}
