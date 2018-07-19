/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System.Data.Entity.ModelConfiguration;
using NFine.Domain.Entity.WarehouseManage;

namespace NFine.Mapping.WarehouseManage
{
    public class ReceiptMap : EntityTypeConfiguration<ReceiptEntity>
    {
        public ReceiptMap()
        {
            this.ToTable("Ware_Receipt");
            this.HasKey(t => t.F_Id);
        }
    }    
}
