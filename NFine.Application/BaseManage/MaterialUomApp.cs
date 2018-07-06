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
using NFine.Domain.IRepository.Base;
using NFine.Repository.Base;

namespace NFine.Application.BaseManage
{
    public class MaterialUomApp
    {
        private IRepositoryEntity<MaterialUomEntity> service = new RepositoryEntity<MaterialUomEntity>();
        private MaterialApp materialApp = new MaterialApp();

        public List<MaterialUomEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<MaterialUomEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_MaterialId == keyword);
            }
            return service.IQueryable(expression).OrderBy(t=>t.F_SortCode).ToList();
        }
       
        //public List<MaterialEntity> GetUserList(string materialId)
        //{
        //    var data = new List<MaterialEntity>();
        //    string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
        //    var userdata = materialApp.GetList();
        //    var materialuomdata = service.IQueryable(t => t.F_MaterialId == materialId && t.F_CorpId == CompanyId).ToList();
        //    foreach (var item in materialuomdata)
        //    {
        //        MaterialEntity materialEntity = userdata.Find(t => t.F_Id == item.F_UomId);
        //        if (materialEntity != null)
        //        {
        //            data.Add(materialEntity);
        //        }
        //    }
        //    return data.OrderBy(t => t.F_SortCode).ToList();
        //}
        
    }
}
