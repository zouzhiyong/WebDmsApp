/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using NFine.Code;
using NFine.Domain.IRepository.Base;
using NFine.Repository.Base;

namespace NFine.Application.SystemManage
{
    public class ItemsApp
    {
        private IRepositoryEntity<ItemsEntity> service = new RepositoryEntity<ItemsEntity>();

        public List<ItemsEntity> GetList()
        {
            //如果是经销商，只能看到父级为自定义类型的数据            
            var expression = ExtLinq.True<ItemsEntity>();
            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {

                string F_Id = service.IQueryable(t => t.F_ParentId == "0" && t.F_EnCode == "CustType").FirstOrDefault().F_Id;
                expression = expression.And(t => t.F_ParentId == F_Id);
                expression = expression.Or(t => t.F_ParentId == "0" && t.F_EnCode == "CustType");
            }
            return service.IQueryable(expression).ToList();
        }
        public ItemsEntity GetForm(string keyValue)
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
        public void SubmitForm(ItemsEntity itemsEntity, string keyValue)
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
