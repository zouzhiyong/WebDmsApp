/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Data;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using NFine.Code;

namespace NFine.Domain.IRepository.SystemManage
{
    public interface IItemsCustDetailRepository : IRepositoryBase<ItemsCustDetailEntity>
    {
        List<ItemsCustDetailEntity> FindList(Expression<Func<ItemsCustDetailEntity, bool>> predicate);
        List<ItemsCustDetailEntity> FindList(Expression<Func<ItemsCustDetailEntity, bool>> predicate, Pagination pagination, string keyword);
        List<ItemsCustDetailEntity> GetItemList(string enCode, string F_CorpId);

        List<ItemsCustDetailEntity> GetItemNotCustList(string F_CorpId);
    }
}
