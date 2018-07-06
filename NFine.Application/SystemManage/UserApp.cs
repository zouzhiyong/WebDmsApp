/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using NFine.Repository.Base;
using NFine.Domain.IRepository.Base;
using NFine.Data;

namespace NFine.Application.SystemManage
{
    public class UserApp
    {
        private IRepositoryBase<UserEntity> serviceLogin = new RepositoryBase<UserEntity>();
        private IRepositoryEntity<UserEntity> service = new RepositoryEntity<UserEntity>();
        private IRepositoryBase<CompanyEntity> serviceCompany = new RepositoryBase<CompanyEntity>();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();

        public List<UserEntity> GetList(string custType="",string keyword = "")
        {
            var expression = ExtLinq.True<UserEntity>();
            if (!string.IsNullOrEmpty(custType)) {
                expression = expression.And(t => t.F_UserCategoryID== custType);
            }
                if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Account.Contains(keyword));
                expression = expression.Or(t => t.F_RealName.Contains(keyword));
                expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
            }

            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }

        public List<UserEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<UserEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Account.Contains(keyword));
                expression = expression.Or(t => t.F_RealName.Contains(keyword));
                expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
            }
            expression = expression.And(t => t.F_Account != "admin");
          
            return service.FindList(expression, pagination);
        }
        public UserEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            //service.DeleteForm(keyValue);
            using (var db = new RepositoryEntity().BeginTrans())
            {
                db.Delete<UserEntity>(t => t.F_Id == keyValue);
                db.Delete<UserLogOnEntity>(t => t.F_UserId == keyValue);
                db.Commit();
            }
        }
        public void SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                userEntity.Modify(keyValue);
            }
            else
            {
                userEntity.Create();
            }
            //service.SubmitForm(userEntity, userLogOnEntity, keyValue);
            using (var db = new RepositoryEntity().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(userEntity);
                }
                else
                {
                    userLogOnEntity.F_Id = userEntity.F_Id;
                    userLogOnEntity.F_UserId = userEntity.F_Id;
                    userLogOnEntity.F_UserSecretkey = Md5.md5(Common.CreateNo(), 16).ToLower();
                    userLogOnEntity.F_UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userLogOnEntity.F_UserPassword, 32).ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
                    userLogOnEntity.F_CorpId = userEntity.F_CorpId;
                    db.Insert(userEntity);
                    db.Insert(userLogOnEntity);
                }
                db.Commit();
            }
        }
        public void UpdateForm(UserEntity userEntity)
        {
            service.Update(userEntity);
        }
        public UserEntity CheckLogin(string username, string password)
        {
            UserEntity userEntity = serviceLogin.FindEntity(t => t.F_Account == username);
            if (userEntity != null)
            {
                if (userEntity.F_EnabledMark == true)
                {
                    UserLogOnEntity userLogOnEntity = userLogOnApp.GetForm(userEntity.F_Id);
                    string dbPassword = Md5.md5(DESEncrypt.Encrypt(password.ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
                    if (dbPassword == userLogOnEntity.F_UserPassword)
                    {
                        string F_CorpId = userEntity.F_CorpId;
                        CompanyEntity companyEntity = serviceCompany.FindEntity(t => t.F_CorpId == F_CorpId);
                        if (companyEntity.F_EnabledMark == true)
                        {
                            DateTime lastVisitTime = DateTime.Now;
                            int LogOnCount = (userLogOnEntity.F_LogOnCount).ToInt() + 1;
                            if (userLogOnEntity.F_LastVisitTime != null)
                            {
                                userLogOnEntity.F_PreviousVisitTime = userLogOnEntity.F_LastVisitTime.ToDate();
                            }
                            userLogOnEntity.F_LastVisitTime = lastVisitTime;
                            userLogOnEntity.F_LogOnCount = LogOnCount;
                            userLogOnEntity.F_CorpId = userEntity.F_CorpId;
                            userLogOnApp.UpdateForm(userLogOnEntity);
                            return userEntity;
                        }else
                        {
                            throw new Exception("账号不属于有效公司，请联系管理员");
                        }                        
                    }
                    else
                    {
                        throw new Exception("密码不正确，请重新输入");
                    }
                }
                else
                {
                    throw new Exception("账户被系统锁定,请联系管理员");
                }
            }
            else
            {
                throw new Exception("账户不存在，请重新输入");
            }
        }
    }
}
