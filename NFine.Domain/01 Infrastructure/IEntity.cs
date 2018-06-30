/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NFine.Data;
using System.Linq;
using System.Reflection;

namespace NFine.Domain
{
    public class IEntity<TEntity>
    {
        public void Create()
        {
            var entity = this as ICreationAudited;
            entity.F_Id = Common.GuId();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.F_CreatorUserId = LoginInfo.UserId;
                entity.F_CorpId = LoginInfo.CompanyId;
            }
            entity.F_CreatorTime = DateTime.Now;
        }
        public void Modify(string keyValue)
        {
            var entity = this as IModificationAudited;
            entity.F_Id = keyValue;
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.F_CorpId = LoginInfo.CompanyId;
                entity.F_LastModifyUserId= LoginInfo.UserId;
            }
            entity.F_LastModifyTime = DateTime.Now;
        }
        public void Remove()
        {
            //var entity = this as IDeleteAudited;
            //var LoginInfo = OperatorProvider.Provider.GetCurrent();
            //if (LoginInfo != null)
            //{
            //    entity.F_DeleteUserId = LoginInfo.UserId;                
            //}
            //entity.F_DeleteTime = DateTime.Now;
            //entity.F_DeleteMark = true;
        }

        public void Mapper(TEntity s)
        {
            //D d = Activator.CreateInstance<D>(); //构造新实例
            var d= this as ICreationAudited;
            try
            {
                var Types = s.GetType();//获得类型  
                var Typed = d.GetType();
                foreach (PropertyInfo sp in Types.GetProperties())//获得类型的属性字段  
                {
                    foreach (PropertyInfo dp in Typed.GetProperties())
                    {
                        if (dp.Name == sp.Name && dp.PropertyType == sp.PropertyType && dp.Name != "Error" && dp.Name != "Item")//判断属性名是否相同  
                        {
                            dp.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return d;
        }
    }
}
