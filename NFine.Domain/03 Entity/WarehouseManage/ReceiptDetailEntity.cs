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
    public class ReceiptDetailEntity : IEntity<ReceiptDetailEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string F_CorpId { get; set; }
        public string F_EnCode { get; set; }
        public string F_SourceNo { get; set; }
        public string F_SourceType { get; set; }
        public Nullable<int> F_RowId { get; set; }
        public string F_ItemID { get; set; }
        public string F_ItemCode { get; set; }
        public string F_ItemCodeName { get; set; }
        public string F_UomID { get; set; }
        public int F_BillQty { get; set; }
        public Nullable<int> F_OperQty { get; set; }
        public Nullable<int> F_BalanceQty { get; set; }
        public decimal F_UnitAmount { get; set; }
        public decimal F_UnitCost { get; set; }
        public decimal F_Amount { get; set; }
        public Nullable<decimal> F_DiscountAmount { get; set; }
        public Nullable<bool> F_IsFree { get; set; }
        public Nullable<bool> F_IsGift { get; set; }
        public Nullable<System.DateTime> F_ProduceDate { get; set; }
        public Nullable<System.DateTime> F_ExpireDate { get; set; }
        public string F_BatchCode { get; set; }
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
        public List<UnitOfMeasureEntity> F_UomIDList { get; set; }        
    }
}
