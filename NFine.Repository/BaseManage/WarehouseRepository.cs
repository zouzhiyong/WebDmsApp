/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System.Collections.Generic;
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
    }
}
