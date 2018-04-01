/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System.Collections.Generic;
using System.Linq;

namespace NFine.Application.SystemManage
{
    public class DepartmentApp
    {
        private IDepartmentRepository service = new DepartmentRepository();

        public List<DepartmentEntity> GetSelect(string F_CorpId)
        {            
            var expression = ExtLinq.True<DepartmentEntity>();            

            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }else
            {
                expression = expression.And(t => t.F_CorpId == F_CorpId);
            }
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }

        public List<DepartmentEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<DepartmentEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
            }
            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }
            return service.FindList(expression, pagination);
        }

        public List<DepartmentEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<DepartmentEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
            }

            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }
        public DepartmentEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(DepartmentEntity departmentEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                departmentEntity.Modify(keyValue);
                service.Update(departmentEntity);
            }
            else
            {
                departmentEntity.Create();
                service.Insert(departmentEntity);
            }
        }
    }
}
