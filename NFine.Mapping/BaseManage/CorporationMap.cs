/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Domain.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace NFine.Mapping.BaseManage
{
    public class CorporationMap : EntityTypeConfiguration<CorporationEntity>
    {
        public CorporationMap()
        {
            this.ToTable("Bas_Corporation");
            this.HasKey(t => t.F_Id);
        }
    }    
}
