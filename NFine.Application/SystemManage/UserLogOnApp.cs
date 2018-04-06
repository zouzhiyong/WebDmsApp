/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;

namespace NFine.Application.SystemManage
{
    public class UserLogOnApp
    {
        private IUserLogOnRepository service = new UserLogOnRepository();

        public UserLogOnEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void UpdateForm(UserLogOnEntity userLogOnEntity)
        {
            service.Update(userLogOnEntity);
        }
        public void RevisePassword(string userPassword,string keyValue)
        {
            UserLogOnEntity userLogOnEntity = new UserLogOnEntity();
            userLogOnEntity.F_Id = keyValue;
            userLogOnEntity.F_UserSecretkey = Md5.md5(Common.CreateNo(), 16).ToLower();
            userLogOnEntity.F_UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userPassword, 32).ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
            userLogOnEntity.F_ChangePasswordDate = DateTime.Now;
            service.Update(userLogOnEntity);
        }

        public void ChangePassword(string userPasswordOld,string userPasswordNew, string keyValue)
        {
            UserLogOnEntity userLogOnEntity = service.FindEntity(keyValue);
            string dbOldPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userPasswordOld, 32).ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
            if (dbOldPassword == userLogOnEntity.F_UserPassword)
            {
                userLogOnEntity.F_Id = keyValue;
                userLogOnEntity.F_UserSecretkey = Md5.md5(Common.CreateNo(), 16).ToLower();
                userLogOnEntity.F_UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userPasswordNew, 32).ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
                userLogOnEntity.F_ChangePasswordDate = DateTime.Now;
                service.Update(userLogOnEntity);
            }
            else
            {
                throw new Exception("原密码不正确，请重新输入");
            }


            
        }
    }
}
