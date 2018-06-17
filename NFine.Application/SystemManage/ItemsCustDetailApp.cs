﻿/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using NFine.Repository.Base;
using NFine.Domain.IRepository.Base;
using System.Text;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace NFine.Application.SystemManage
{
    public class ItemsCustDetailApp
    {
        private IRepositoryEntity<ItemsCustDetailEntity> service = new RepositoryEntity<ItemsCustDetailEntity>();
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  d.*
                            FROM    Sys_ItemsCustDetail d
                                    INNER  JOIN Sys_Items i ON i.F_Id = d.F_ItemId AND d.F_CorpId=@F_CorpId
                            WHERE   1 = 1
                                    AND i.F_EnCode = @enCode
                                    AND d.F_EnabledMark = 1
                                    AND d.F_DeleteMark = 0
                            ORDER BY d.F_SortCode ASC");
            DbParameter[] parameter =
            {
                 new MySqlParameter("@enCode",enCode),
                 new MySqlParameter("@F_CorpId", F_CorpId)
            };

            return service.FindList(strSql.ToString(), parameter);
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
