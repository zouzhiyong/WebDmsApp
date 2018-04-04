/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;

namespace NFine.Domain.Entity.BaseManage
{
    public class CustomerEntity : IEntity<CustomerEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string F_CorpId { get; set; }
        public string F_EnCode { get; set; }
        public string F_FullName { get; set; }
        public string F_ShortName { get; set; }
        public string F_HelperCode { get; set; }
        public string F_CustCategoryID { get; set; }
        public string F_PaymentID { get; set; }
        public string F_RegionID { get; set; }
        public string F_TelePhone { get; set; }
        public string F_MobilePhone { get; set; }
        public string F_Fax { get; set; }
        public string F_Email { get; set; }
        public string F_Address { get; set; }
        public string F_PostCode { get; set; }
        public string F_City { get; set; }
        public string F_Contact { get; set; }
        public string F_EmployeeID { get; set; }
        public string F_Photo { get; set; }
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
