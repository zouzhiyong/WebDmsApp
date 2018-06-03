/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System.Collections.Generic;
using System.Linq;

namespace NFine.Application.SystemManage
{
    public class ItemsCustDetailApp
    {
        private IItemsCustDetailRepository service = new ItemsCustDetailRepository();
        private string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;

        public List<ItemsCustDetailEntity> GetList(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<ItemsCustDetailEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.F_ItemId == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_ItemName.Contains(keyword) && t.F_CorpId== CompanyId);
                expression = expression.Or(t => t.F_ItemCode.Contains(keyword) && t.F_CorpId == CompanyId);
            }

            expression = expression.And(t => t.F_CorpId == CompanyId);

            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }
        public List<ItemsCustDetailEntity> GetItemList(string enCode, string F_CorpId)
        {
            return service.GetItemList(enCode, F_CorpId);
        }
        public ItemsCustDetailEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(ItemsCustDetailEntity ItemsCustDetailEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                ItemsCustDetailEntity.Modify(keyValue);
                service.Update(ItemsCustDetailEntity);
            }
            else
            {
                ItemsCustDetailEntity.Create();
                ItemsCustDetailEntity.F_DeleteMark = false;
                service.Insert(ItemsCustDetailEntity);
            }
        }
    }
}
