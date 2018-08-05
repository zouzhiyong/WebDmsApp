/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Domain.Entity.WarehouseManage;
using System.Data.Entity.ModelConfiguration;

namespace NFine.Mapping.WarehouseManage
{
    public class WareEntryItemMap : EntityTypeConfiguration<WareEntryItemEntity>
    {
        public WareEntryItemMap()
        {
            this.ToTable("Dms_WareEntryItem");
            this.HasKey(t => t.F_Id);
        }
    }    
}
