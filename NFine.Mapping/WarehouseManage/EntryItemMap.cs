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
    public class EntryItemMap : EntityTypeConfiguration<EntryItemEntity>
    {
        public EntryItemMap()
        {
            this.ToTable("Ware_EntryItem");
            this.HasKey(t => t.F_Id);
        }
    }    
}
