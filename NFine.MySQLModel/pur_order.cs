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
    
    public partial class pur_order
    {
        public string F_Id { get; set; }
        public string F_CorpId { get; set; }
        public string F_EnCode { get; set; }
        public System.DateTime F_BillDate { get; set; }
        public int F_BillType { get; set; }
        public Nullable<System.DateTime> F_PostDate { get; set; }
        public string F_SupplierID { get; set; }
        public string F_SupplierName { get; set; }
        public string F_TruckID { get; set; }
        public string F_DriverID { get; set; }
        public string F_PurchaserID { get; set; }
        public Nullable<long> F_Status { get; set; }
        public Nullable<int> F_IsStockFinished { get; set; }
        public Nullable<System.DateTime> F_ConfirmTime { get; set; }
        public string F_ConfirmUserId { get; set; }
        public Nullable<decimal> F_TaxRate { get; set; }
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
    }
}
