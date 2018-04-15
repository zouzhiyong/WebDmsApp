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
    public class MaterialPictureApp
    {
        private IMaterialPictureRepository service = new MaterialPictureRepository();
        private MaterialApp materialApp = new MaterialApp();
        string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;

        public List<MaterialPictureEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<MaterialPictureEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_MaterialId == keyword);
            }
            expression = expression.And(t => t.F_CorpId == CompanyId);
            return service.IQueryable(expression).OrderBy(t=>t.F_SortCode).ToList();
        }      
        
    }
}
