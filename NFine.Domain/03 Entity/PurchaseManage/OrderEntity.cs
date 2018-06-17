/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;

namespace NFine.Domain.Entity.PurchaseManage
{
    public class OrderEntity : IEntity<OrderEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string F_CorpId { get; set; }
        public string F_EnCode { get; set; }
        public System.DateTime F_BillDate { get; set; }
        public int F_BillType { get; set; }
        public Nullable<System.DateTime> F_PostDate { get; set; }
        public string F_SupplierID { get; set; }
        public string F_TruckID { get; set; }
        public string F_DriverID { get; set; }
        public string F_PurchaserID { get; set; }
        public Nullable<long> F_Status { get; set; }
        public Nullable<int> F_IsStockFinished { get; set; }
        public Nullable<System.DateTime> F_ConfirmTime { get; set; }
        public string F_ConfirmUserID { get; set; }
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
