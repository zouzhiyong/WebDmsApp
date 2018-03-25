/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.BaseManage;
using NFine.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using NFine.Application.SystemManage;
using NFine.Domain.Entity.SystemManage;
using NFine.Repository.BaseManage;
using NFine.Domain.IRepository.BaseManage;

namespace NFine.Application.BaseManage
{
    public class WarehouseUserApp
    {
        private IWarehouseUserRepository service = new WarehouseUserRepository();
        private UserApp userApp = new UserApp();

        public List<WarehouseUserEntity> GetList(string ObjectId)
        {
            return service.IQueryable(t => t.F_WarehouseId == ObjectId).ToList();
        }
        public List<UserEntity> GetUserList(string warehouseId)
        {
            var data = new List<UserEntity>();
            string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                data = userApp.GetList();
            }
            else
            {
                var userdata = userApp.GetList();
                var warehouseuserdata = service.IQueryable(t => t.F_WarehouseId == warehouseId && t.F_OrganizeId == CompanyId).ToList();
                foreach (var item in warehouseuserdata)
                {
                    UserEntity userEntity = userdata.Find(t => t.F_Id == item.F_UserId);
                    if (userEntity != null)
                    {
                        data.Add(userEntity);
                    }
                }
            }
            return data.OrderBy(t => t.F_SortCode).ToList();
        }
        
    }
}
