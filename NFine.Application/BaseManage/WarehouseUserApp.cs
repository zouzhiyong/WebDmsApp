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
using NFine.Application.SystemManage;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.Base;
using NFine.Repository.Base;

namespace NFine.Application.BaseManage
{
    public class WarehouseUserApp
    {
        private IRepositoryEntity<WarehouseUserEntity> service = new RepositoryEntity<WarehouseUserEntity>();
        private UserApp userApp = new UserApp();
        string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;

        public List<WarehouseUserEntity> GetList(string keyword = "")
        {
            return service.IQueryable(t => t.F_WarehouseId == keyword && t.F_CorpId== CompanyId).ToList();
        }
        public List<UserEntity> GetUserList(string warehouseId)
        {
            var data = new List<UserEntity>();
            
            var userdata = userApp.GetList();
            var warehouseuserdata = service.IQueryable(t => t.F_WarehouseId == warehouseId && t.F_CorpId == CompanyId).ToList();
            foreach (var item in warehouseuserdata)
            {
                UserEntity userEntity = userdata.Find(t => t.F_Id == item.F_UserId);
                if (userEntity != null)
                {
                    data.Add(userEntity);
                }
            }
            return data.OrderBy(t => t.F_SortCode).ToList();
        }
        
    }
}
