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
using NFine.Application.SystemManage;

namespace NFine.Application.BaseManage
{
    public class WarehouseApp
    {
        private IWarehouseRepository service = new WarehouseRepository();
        private UserApp userApp = new UserApp();

        public List<WarehouseEntity> GetList(string keyword="")
        {
            var expression = ExtLinq.True<WarehouseEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Name.Contains(keyword));
            }
            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }
            return service.IQueryable(expression).ToList();
        }

        public List<WarehouseEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<WarehouseEntity>();
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
        public WarehouseEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.F_Id == keyValue);
        }

        public void SubmitForm(WarehouseEntity warehouseEntity, string[] userIds, string keyValue)
        {
            string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
            if (!string.IsNullOrEmpty(keyValue))
            {
                warehouseEntity.F_Id = keyValue;
            }
            else
            {
                warehouseEntity.F_Id = Common.GuId();
            }
            var userdata = userApp.GetList();
            List<WarehouseUserEntity> warehouseUserEntitys = new List<WarehouseUserEntity>();
            foreach (var itemId in userIds)
            {
                WarehouseUserEntity warehouseUserEntity = new WarehouseUserEntity();
                warehouseUserEntity.F_Id = Common.GuId();
                warehouseUserEntity.F_WarehouseId = warehouseEntity.F_Id;
                warehouseUserEntity.F_UserId = itemId;
                warehouseUserEntity.F_CorpId = CompanyId;
                warehouseUserEntitys.Add(warehouseUserEntity);
            }
            service.SubmitForm(warehouseEntity, warehouseUserEntitys, keyValue);
        }
        //public void SubmitForm(WarehouseEntity WarehouseEntity, string keyValue)
        //{
        //    if (!string.IsNullOrEmpty(keyValue))
        //    {
        //        WarehouseEntity.Modify(keyValue);
        //        service.Update(WarehouseEntity);
        //    }
        //    else
        //    {
        //        WarehouseEntity.Create();
        //        service.Insert(WarehouseEntity);
        //    }
    }
}
