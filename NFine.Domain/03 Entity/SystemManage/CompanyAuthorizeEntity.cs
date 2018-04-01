/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;

namespace NFine.Domain.Entity.SystemManage
{
    public class CompanyAuthorizeEntity : IEntity<CompanyAuthorizeEntity>, ICreationAudited
    {
        public string F_Id { get; set; }
        public Nullable<int> F_ModuleType { get; set; }
        public string F_ModuleId { get; set; }
        public Nullable<int> F_SortCode { get; set; }
        public Nullable<System.DateTime> F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public string F_CorpId { get; set; }
    }
}
