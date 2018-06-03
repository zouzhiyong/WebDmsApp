﻿/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NFine.Code;
using NFine.Data;
using NFine.Domain.Entity.BaseManage;

namespace NFine.Domain.IRepository.BaseManage
{
    public interface IWarehouseRepository : IRepositoryBase<WarehouseEntity>
    {
        void DeleteForm(string keyValue);
        void SubmitForm(WarehouseEntity warehouseEntity, List<WarehouseUserEntity> warehouseUserEntitys, string keyValue);

        List<WarehouseEntity> FindList(Expression<Func<WarehouseEntity, bool>> predicate);
        List<WarehouseEntity> FindList(Expression<Func<WarehouseEntity, bool>> predicate, Pagination pagination, string keyword);
    }
}
