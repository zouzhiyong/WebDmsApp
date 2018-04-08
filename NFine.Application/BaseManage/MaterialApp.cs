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
    public class MaterialApp
    {
        private IMaterialRepository service = new MaterialRepository();

        public List<MaterialEntity> GetList(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<MaterialEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.F_ItemGroupID == itemId || t.F_ItemCategoryID==itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }

            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }
        public List<MaterialEntity> GetItemList(string enCode)
        {
            return service.GetItemList(enCode);
        }
        public MaterialEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(MaterialEntity materialEntity, MaterialUomEntity[] materialuomEntitys, string keyValue)
        {
            string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
            if (!string.IsNullOrEmpty(keyValue))
            {
                materialEntity.F_Id = keyValue;
            }
            else
            {
                materialEntity.F_Id = Common.GuId();
            }

            //if (!string.IsNullOrEmpty(keyValue))
            //{
            //    materialEntity.Modify(keyValue);
            //    service.Update(materialEntity);
            //}
            //else
            //{
            //    materialEntity.Create();
            //    materialEntity.F_DeleteMark = false;
            //    service.Insert(materialEntity);
            //}
            List<MaterialUomEntity> materialuomEntitysTemp = new List<MaterialUomEntity>();
            foreach (var items in materialuomEntitys)
            {
                items.F_Id = Common.GuId();
                items.F_MaterialId = materialEntity.F_Id;
                items.F_CorpId = CompanyId;
                items.F_RateQty = (items.F_RateQty <= 0 ? 1 : items.F_RateQty);
                if(items.F_UomId!=null && items.F_UomId != "")
                {
                    materialuomEntitysTemp.Add(items);
                }                
            }

            service.SubmitForm(materialEntity, materialuomEntitysTemp, keyValue);

            //string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
            //if (!string.IsNullOrEmpty(keyValue))
            //{
            //    warehouseEntity.F_Id = keyValue;
            //}
            //else
            //{
            //    warehouseEntity.F_Id = Common.GuId();
            //}
            //var userdata = userApp.GetList();
            //List<WarehouseUserEntity> warehouseUserEntitys = new List<WarehouseUserEntity>();
            //foreach (var itemId in userIds)
            //{
            //    WarehouseUserEntity warehouseUserEntity = new WarehouseUserEntity();
            //    warehouseUserEntity.F_Id = Common.GuId();
            //    warehouseUserEntity.F_WarehouseId = warehouseEntity.F_Id;
            //    warehouseUserEntity.F_UserId = itemId;
            //    warehouseUserEntity.F_CorpId = CompanyId;
            //    warehouseUserEntitys.Add(warehouseUserEntity);
            //}
            //service.SubmitForm(warehouseEntity, warehouseUserEntitys, keyValue);
        }
    }
}
