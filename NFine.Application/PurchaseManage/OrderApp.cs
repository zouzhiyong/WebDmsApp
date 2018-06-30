/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/

using NFine.Domain.IRepository.Base;
using NFine.Repository.Base;
using NFine.Domain.Entity.PurchaseManage;
using NFine.Application.SystemManage;
using System.Collections.Generic;
using System.Reflection;
using NFine.Code;
using System;

namespace NFine.Application.PurManage
{
    public class OrderApp
    {
        private IRepositoryEntity<OrderEntity> service = new RepositoryEntity<OrderEntity>();
        private IRepositoryEntity<OrderDetailEntity> serviceDetail = new RepositoryEntity<OrderDetailEntity>();

        public OrderEntity SubmitForm(OrderEntity model)
        {
            List<OrderDetailEntity> orderDetailEntitys = model.details;
            using (var db = new RepositoryEntity().BeginTrans())
            {
                //插入主表数据
                if (!string.IsNullOrEmpty(model.F_Id))
                {
                    model.Modify(model.F_Id);
                    if (model.F_Status == 2)
                    {
                        var LoginInfo = OperatorProvider.Provider.GetCurrent();
                        model.F_ConfirmUserId = LoginInfo.UserId;
                        model.F_ConfirmTime = DateTime.Now;
                    }
                    model.details = null;
                    db.Update(model);                    
                }
                //更新主表数据
                else
                {
                    model.Create();
                    SerialNumberDetailApp.GetAutoIncrementCode<OrderEntity>(model);//获取编号                    
                    db.Insert(model);
                }

                //删除从表数据
                db.Delete<OrderDetailEntity>(t => t.F_POId == model.F_EnCode);
                //插入从表数据
                foreach (var item in orderDetailEntitys)
                {
                    item.Create();
                    item.F_POId = model.F_EnCode;
                    db.Insert(item);
                }

                //提交
                db.Commit();
                model.details = serviceDetail.FindList(t => t.F_POId == model.F_EnCode);

                return model;
            }
        }
    }
}
