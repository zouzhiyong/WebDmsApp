/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;

namespace NFine.Domain.Entity.BaseManage
{
    public class MaterialPictureEntity : IEntity<MaterialPictureEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string F_CorpId { get; set; }
        public string F_MaterialId { get; set; }
        public string F_Picture { get; set; }
        public Nullable<int> F_PictureType { get; set; }
        public Nullable<bool> F_IsTitle { get; set; }
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
