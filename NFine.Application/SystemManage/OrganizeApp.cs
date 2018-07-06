/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using NFine.Code;
using NFine.Domain.IRepository.Base;
using NFine.Repository.Base;

namespace NFine.Application.SystemManage
{
    public class OrganizeApp
    {
        private IRepositoryEntity<OrganizeEntity> service = new RepositoryEntity<OrganizeEntity>();

        public List<OrganizeEntity> GetList(Pagination pagination, string F_CategoryId, string keyword)
        {
            var expression = ExtLinq.True<OrganizeEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
            }
            expression = expression.And(t => t.F_CategoryId == F_CategoryId);
            return service.FindList(expression, pagination);
        }

        public List<OrganizeEntity> GetSelet(string F_CategoryId, string F_ParentId)
        {
            if (F_CategoryId.ToLower()== "company")
            {
                F_ParentId = "0";
            }
            var expression = ExtLinq.True<OrganizeEntity>();            
            expression = expression.And(t => t.F_CategoryId == F_CategoryId);
            
            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }else
            {
                expression = expression.And(t => t.F_ParentId == F_ParentId);
            }
            return service.IQueryable(expression).OrderBy(t => t.F_CreatorTime).ToList();
        }

        public List<OrganizeEntity> GetList(string F_CategoryId)
        {
            string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                if (F_CategoryId == null)
                {
                    return service.IQueryable().OrderBy(t => t.F_CreatorTime).ToList();
                }
                else
                {
                    return service.IQueryable(t => t.F_CategoryId == F_CategoryId).OrderBy(t => t.F_CreatorTime).ToList();
                }
            }
            else
            {
                if (F_CategoryId == null)
                {
                    return service.IQueryable(t => t.F_Id == CompanyId || t.F_ParentId == CompanyId).OrderBy(t => t.F_CreatorTime).ToList();
                }
                else
                {
                    return service.IQueryable(t => t.F_CategoryId == F_CategoryId && (t.F_Id == CompanyId || t.F_ParentId == CompanyId)).OrderBy(t => t.F_CreatorTime).ToList();
                }                
            }
        }

        public List<OrganizeEntity> GetList()
        {
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                return service.IQueryable().OrderBy(t => t.F_CreatorTime).ToList();
            }
            else
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                return service.IQueryable(t => t.F_Id == CompanyId || t.F_ParentId == CompanyId).OrderBy(t => t.F_CreatorTime).ToList();
            }
        }

        public OrganizeEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.F_Id == keyValue);
            }
        }
        public void SubmitForm(OrganizeEntity organizeEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                organizeEntity.Modify(keyValue);
                if (OperatorProvider.Provider.GetCurrent().IsSystem)
                {
                    if (organizeEntity.F_CategoryId.ToLower() == "company")
                    {
                        organizeEntity.F_CorpId = organizeEntity.F_Id;
                    }
                }
                service.Update(organizeEntity);
            }
            else
            {
                organizeEntity.Create();
                if (OperatorProvider.Provider.GetCurrent().IsSystem)
                {
                    if (organizeEntity.F_CategoryId.ToLower() == "company")
                    {
                        organizeEntity.F_CorpId = organizeEntity.F_Id;
                    }
                }
                service.Insert(organizeEntity);
            }
        }
    }
}
