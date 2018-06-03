/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Data;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System.Collections.Generic;
using System.Data.Common;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Text;
using System.Linq.Expressions;
using NFine.Code;
using System;
using System.Linq;

namespace NFine.Repository.SystemManage
{
    public class ItemsDetailRepository : RepositoryBase<ItemsDetailEntity>, IItemsDetailRepository
    {
        public List<ItemsDetailEntity> GetItemList(string enCode)
        {
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append(@"SELECT  d.*
            //                FROM    Sys_ItemsDetail d
            //                        INNER  JOIN Sys_Items i ON i.F_Id = d.F_ItemId
            //                WHERE   1 = 1
            //                        AND i.F_EnCode = @enCode
            //                        AND d.F_EnabledMark = 1
            //                        AND d.F_DeleteMark = 0
            //                ORDER BY d.F_SortCode ASC");
            //DbParameter[] parameter = 
            //{
            //     new SqlParameter("@enCode",enCode)
            //};
            //return this.FindList(strSql.ToString(), parameter);

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  d.*
                            FROM    Sys_ItemsDetail d
                                    INNER  JOIN Sys_Items i ON i.F_Id = d.F_ItemId
                            WHERE   1 = 1
                                    AND i.F_EnCode = @enCode
                                    AND d.F_EnabledMark = 1
                                    AND d.F_DeleteMark = 0
                            ORDER BY d.F_SortCode ASC");
            DbParameter[] parameter =
            {
                 new MySqlParameter("@enCode",enCode)
            };
            return this.FindList(strSql.ToString(), parameter);
        }

        public List<ItemsDetailEntity> FindList(Expression<Func<ItemsDetailEntity, bool>> predicate)
        {
            var expression = ExtLinq.True<ItemsDetailEntity>();
            expression.And(predicate);

            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }

            return IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }

        public List<ItemsDetailEntity> FindList(Expression<Func<ItemsDetailEntity, bool>> predicate, Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ItemsDetailEntity>();
            expression.And(predicate);

            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    expression = expression.And(t => t.F_FullName.Contains(keyword));
            //}

            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }

            return FindList(expression, pagination);
        }
    }
}
