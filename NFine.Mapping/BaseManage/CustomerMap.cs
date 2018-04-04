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
    public class CustomerMap : EntityTypeConfiguration<CustomerEntity>
    {
        public CustomerMap()
        {
            this.ToTable("Bas_Customer");
            this.HasKey(t => t.F_Id);
        }
    }    
}
