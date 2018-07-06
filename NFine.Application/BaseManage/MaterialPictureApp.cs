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
    public class MaterialPictureApp
    {
        private IRepositoryEntity<MaterialPictureEntity> service = new RepositoryEntity<MaterialPictureEntity>();
        private MaterialApp materialApp = new MaterialApp();

        public List<MaterialPictureEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<MaterialPictureEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_MaterialId == keyword);
            }
            return service.IQueryable(expression).OrderBy(t=>t.F_SortCode).ToList();
        }      
        
    }
}
