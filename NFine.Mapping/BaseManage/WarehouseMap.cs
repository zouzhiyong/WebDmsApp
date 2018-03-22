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
    public class WarehouseMap : EntityTypeConfiguration<WarehouseEntity>
    {
        public WarehouseMap()
        {
            this.ToTable("Bas_Warehouse");
            this.HasKey(t => t.F_Id);
        }
    }    
}
