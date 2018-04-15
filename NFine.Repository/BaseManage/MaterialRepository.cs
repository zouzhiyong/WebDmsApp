/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using MySql.Data.MySqlClient;
using NFine.Data;
using NFine.Domain.Entity.BaseManage;
using NFine.Domain.IRepository.BaseManage;

namespace NFine.Repository.BaseManage
{
    public class MaterialRepository : RepositoryBase<MaterialEntity>, IMaterialRepository
    {
        public List<MaterialEntity> GetItemList(string enCode)
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
                            FROM    Bas_Material d
                                    INNER  JOIN Bas_MaterialGroup i ON i.F_Id = d.F_ItemGroupID or i.F_Id = d.F_ItemCategoryID 
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

        public void SubmitForm(MaterialEntity materialEntity, List<MaterialUomEntity> materialuomEntitys, MaterialPictureEntity materialpictureEntity, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(materialEntity);
                }
                else
                {
                    db.Insert(materialEntity);
                }
                db.Delete<MaterialPictureEntity>(t => t.F_MaterialId == materialEntity.F_Id && t.F_PictureType==0 && t.F_CorpId==materialEntity.F_CorpId);
                db.Insert(materialpictureEntity);

                db.Delete<MaterialUomEntity>(t => t.F_MaterialId == materialEntity.F_Id && t.F_CorpId == materialEntity.F_CorpId);
                db.Insert(materialuomEntitys);
                db.Commit();
            }
        }
    }
}
