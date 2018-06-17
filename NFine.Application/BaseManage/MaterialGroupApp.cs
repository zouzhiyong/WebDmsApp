/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.BaseManage;
using System.Collections.Generic;
using System.Linq;
using System;
using NFine.Repository.Base;
using NFine.Domain.IRepository.Base;

namespace NFine.Application.BaseManage
{
    public class MaterialGroupApp
    {
        private IRepositoryEntity<MaterialGroupEntity> service = new RepositoryEntity<MaterialGroupEntity>();
        private IRepositoryEntity<MaterialEntity> materialService = new RepositoryEntity<MaterialEntity>();

        public List<MaterialGroupEntity> GetList(string keyValue="")
        {
            string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
            var expression = ExtLinq.True<MaterialGroupEntity>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.F_ParentId == keyValue);
            }
            expression = expression.And(t => t.F_CorpId== CompanyId);
            return service.IQueryable(expression).ToList();
        }

        public MaterialGroupEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！包含有下级分类。");
            }
            else if (materialService.IQueryable().Count(t => t.F_ItemGroupID.Equals(keyValue) || t.F_ItemCategoryID.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！类别下面包含有商品");
            }
            else
            {
                service.Delete(t => t.F_Id == keyValue);
            }
        }
        public void SubmitForm(MaterialGroupEntity itemsEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                itemsEntity.Modify(keyValue);
                service.Update(itemsEntity);
            }
            else
            {
                itemsEntity.Create();
                service.Insert(itemsEntity);
            }
        }
    }
}
