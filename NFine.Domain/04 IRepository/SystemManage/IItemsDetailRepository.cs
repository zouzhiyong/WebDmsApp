/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Data;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using NFine.Code;
using System.Linq.Expressions;
using System;

namespace NFine.Domain.IRepository.SystemManage
{
    public interface IItemsDetailRepository : IRepositoryBase<ItemsDetailEntity>
    {
        List<ItemsDetailEntity> GetItemList(string enCode);

        List<ItemsDetailEntity> FindList(Expression<Func<ItemsDetailEntity, bool>> predicate);
        List<ItemsDetailEntity> FindList(Expression<Func<ItemsDetailEntity, bool>> predicate, Pagination pagination, string keyword);

    }
}
