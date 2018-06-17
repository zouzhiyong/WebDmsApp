/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Domain.Entity.PurchaseManage;
using System.Data.Entity.ModelConfiguration;

namespace NFine.Mapping.PurchaseManage
{
    public class OrderDetailMap : EntityTypeConfiguration<OrderDetailEntity>
    {
        public OrderDetailMap()
        {
            this.ToTable("Pur_OrderDetail");
            this.HasKey(t => t.F_Id);
        }
    }    
}
