﻿/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Domain.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace NFine.Mapping.SystemManage
{
    public class SerialNumberDetailMap : EntityTypeConfiguration<SerialNumberDetailEntity>
    {
        public SerialNumberDetailMap()
        {
            this.ToTable("Sys_SerialNumberDetail");
            this.HasKey(t => t.F_Id);
        }
    }    
}
