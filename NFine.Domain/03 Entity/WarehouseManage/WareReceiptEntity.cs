﻿/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NFine.Code;

namespace NFine.Domain.Entity.WarehouseManage
{
    public class WareReceiptEntity : IEntity<WareReceiptEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {        
        public string F_Id { get; set; }
        public string F_CorpId { get; set; }
        public string F_EnCode { get; set; }
        public System.DateTime F_BillDate { get; set; }
        public string F_WarehouseID { get; set; }
        public string F_BillType { get; set; }
        public Nullable<System.DateTime> F_PostDate { get; set; }
        public Nullable<long> F_Status { get; set; }
        public string F_TruckID { get; set; }
        public string F_DriverID { get; set; }
        public Nullable<int> F_IsStockFinished { get; set; }
        public Nullable<System.DateTime> F_ConfirmTime { get; set; }
        public string F_ConfirmUserId { get; set; }
        public Nullable<int> F_PrintNums { get; set; }
        public Nullable<int> F_SortCode { get; set; }
        public Nullable<bool> F_DeleteMark { get; set; }
        public Nullable<bool> F_EnabledMark { get; set; }
        public string F_Description { get; set; }
        public Nullable<System.DateTime> F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public Nullable<System.DateTime> F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public Nullable<System.DateTime> F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }
        [NotMapped]
        public List<WareReceiptDetailEntity> details { get; set; }
    }
}
