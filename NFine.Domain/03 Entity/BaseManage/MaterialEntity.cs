/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;

namespace NFine.Domain.Entity.BaseManage
{
    public class MaterialEntity : IEntity<MaterialEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string F_CorpId { get; set; }
        public string F_EnCode { get; set; }
        public string F_FullName { get; set; }
        public string F_ShortName { get; set; }
        public string F_HelperCode { get; set; }        
        public string F_ItemGroupID { get; set; }
        public string F_ItemCategoryID { get; set; }
        public string F_WarehouseID { get; set; }
        public Nullable<float> F_SaveInventory { get; set; }
        public string F_BaseUOM { get; set; }
        public string F_SalesUOM { get; set; }
        public Nullable<decimal> F_SalesPrice { get; set; }
        public string F_PurchaseUOM { get; set; }
        public Nullable<decimal> F_PurchasePrice { get; set; }
        public Nullable<float> F_Length { get; set; }
        public Nullable<float> F_Width { get; set; }
        public Nullable<float> F_Height { get; set; }
        public Nullable<float> F_CrossWeigth { get; set; }
        public Nullable<float> F_Size { get; set; }
        public float F_LastCost { get; set; }
        public bool F_IsBatch { get; set; }
        public Nullable<int> F_Period { get; set; }
        public Nullable<bool> F_IsForSale { get; set; }
        public bool F_IsZeroValue { get; set; }
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
