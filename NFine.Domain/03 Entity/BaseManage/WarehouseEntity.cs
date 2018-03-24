/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;

namespace NFine.Domain.Entity.BaseManage
{
    public class WarehouseEntity : IEntity<WarehouseEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string F_OrganizeId { get; set; }
        public string F_Name { get; set; }
        public string F_Address { get; set; }
        public string F_Contact { get; set; }
        public string F_Tel { get; set; }
        public string F_MobilePhone { get; set; }
        public bool? F_IsBin { get; set; }
        public bool? F_IsRequireReceive { get; set; }
        public bool? F_IsRequireShipment { get; set; }
        public bool? F_DeleteMark { get; set; }
        public bool? F_EnabledMark { get; set; }
        public string F_Description { get; set; }
        public System.DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public System.DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public System.DateTime? F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }
        public int? F_SortCode { get; set; }
    }
}
