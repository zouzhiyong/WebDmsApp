﻿/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Domain.Entity.PurchaseManage;
using System.Data.Entity.ModelConfiguration;

namespace NFine.Mapping.PurchaseManage
{
    public class PurOrderDetailMap : EntityTypeConfiguration<PurOrderDetailEntity>
    {
        public PurOrderDetailMap()
        {
            this.ToTable("Dms_PurOrderDetail");
            this.HasKey(t => t.F_Id);
        }
    }    
}
