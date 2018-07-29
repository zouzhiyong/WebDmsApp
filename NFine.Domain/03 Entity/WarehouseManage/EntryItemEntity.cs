/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using NFine.Domain.Entity.BaseManage;

namespace NFine.Domain.Entity.WarehouseManage
{
    public class EntryItemEntity : IEntity<EntryItemEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string F_BillId { get; set; }
        public string F_CorpId { get; set; }
        public string F_EnCode { get; set; }
        public string F_DetailId { get; set; }
        public System.DateTime F_BillDate { get; set; }
        public Nullable<System.DateTime> F_PostDate { get; set; }
        public Nullable<long> F_Status { get; set; }
        public string F_BillType { get; set; }
        public string F_WarehouseID { get; set; }
        public string F_ItemID { get; set; }
        public string F_ItemCode { get; set; }
        public string F_ItemCodeName { get; set; }
        public string F_BatchCode { get; set; }
        public Nullable<System.DateTime> F_ProduceDate { get; set; }
        public Nullable<System.DateTime> F_ExpireDate { get; set; }
        public string F_UomID { get; set; }
        public int F_BillQty { get; set; }
        public Nullable<int> F_OperQty { get; set; }
        public Nullable<int> F_BalanceQty { get; set; }
        public decimal F_UnitAmount { get; set; }
        public decimal F_Amount { get; set; }
        public decimal F_UnitCost { get; set; }
        public Nullable<bool> F_DeleteMark { get; set; }
        public Nullable<bool> F_EnabledMark { get; set; }
        public string F_Description { get; set; }
        public Nullable<System.DateTime> F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public Nullable<System.DateTime> F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public Nullable<System.DateTime> F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }
        public Nullable<int> F_SortCode { get; set; }
    }
    
}
