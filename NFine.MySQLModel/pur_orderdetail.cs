//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NFine.MySQLModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class pur_orderdetail
    {
        public string F_Id { get; set; }
        public string F_POId { get; set; }
        public string F_CorpId { get; set; }
        public string F_RowId { get; set; }
        public string F_ItemID { get; set; }
        public string F_ItemName { get; set; }
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
        public string F_ItemCode { get; set; }
        public string F_ItemCodeName { get; set; }
    }
}
