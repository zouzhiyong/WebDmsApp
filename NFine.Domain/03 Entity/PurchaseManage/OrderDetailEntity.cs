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

namespace NFine.Domain.Entity.PurchaseManage
{
    public class OrderDetailEntity : IEntity<OrderDetailEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string F_POId { get; set; }
        public string F_CorpId { get; set; }
        public int F_RowId { get; set; }
        public string F_ItemID { get; set; }
        public string F_ItemCode { get; set; }
        public string F_ItemCodeName { get; set; }
        public string F_UomID { get; set; }
        public string F_WarehouseID { get; set; }
        public string F_BinID { get; set; }
        public int F_BillQty { get; set; }
        public int F_OperQty { get; set; }
        public Nullable<int> F_BalanceQty { get; set; }
        public decimal F_UnitAmount { get; set; }
        public decimal F_UnitCost { get; set; }
        public decimal F_Amount { get; set; }
        public Nullable<decimal> F_DiscountAmount { get; set; }
        public string F_ReturnReasonID { get; set; }
        public Nullable<bool> F_IsFree { get; set; }
        public Nullable<bool> F_IsGift { get; set; }
        public Nullable<int> F_ReturnQuantity { get; set; }
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
        [NotMapped]
        public List<WarehouseEntity> F_WarehouseIDList { get; set; }
    }
}
