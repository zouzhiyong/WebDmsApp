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
using System.Linq;
using NFine.Application.BaseManage;
using NFine.Domain.Entity.BaseManage;
using System.Text;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace NFine.Application.PurManage
{
    public class OrderApp
    {
        private IRepositoryEntity<OrderEntity> service = new RepositoryEntity<OrderEntity>();
        private IRepositoryEntity<OrderDetailEntity> serviceDetail = new RepositoryEntity<OrderDetailEntity>();        
        private IRepositoryEntity<WarehouseEntity> serviceWarehouse = new RepositoryEntity<WarehouseEntity>();

        public OrderEntity SubmitForm(OrderEntity model)
        {
            if(model.F_BillType!=1 && model.F_BillType != -1)
            {
                throw new Exception("此单据为非法单据!");
            }

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
                    if (model.F_BillType == 1)
                    {
                        SerialNumberDetailApp.GetAutoIncrementCode<OrderEntity>(model, "PurchaseOrder");//获取编号  
                    }else
                    {
                        SerialNumberDetailApp.GetAutoIncrementCode<OrderEntity>(model, "PurchaseReturnOrder");//获取编号   
                    }                    
                                      
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

                return GetOrderData(model.F_Id);
            }
        }

        public List<OrderEntity> GetList(Pagination pagination, string keyword,int type)
        {
            var expression = ExtLinq.True<OrderEntity>();
            expression = expression.And(t => t.F_BillType == type);
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_EnCode.Contains(keyword));
            }

            return service.FindList(expression, pagination);
        }

        /// <summary>
        /// 获取单据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public OrderEntity GetForm(string keyValue)
        {
            return GetOrderData(keyValue);
        }
        /// <summary>
        /// 复制单据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public OrderEntity GetCopyForm(string keyValue)
        {
            var orderItem = GetOrderData(keyValue);

            orderItem.F_Id = "";
            orderItem.F_Status = 0;
            orderItem.F_CreatorTime = null;
            orderItem.F_CreatorUserId = "";
            orderItem.F_ConfirmTime = null;
            orderItem.F_ConfirmUserId = "";
            orderItem.F_LastModifyTime = null;
            orderItem.F_LastModifyUserId = "";
            orderItem.F_PrintNums = 0;
            orderItem.F_EnCode = "";

            foreach (var item in orderItem.details)
            {
                item.F_Id = "";
                item.F_POId = "";
                item.F_LastModifyTime = null;
                item.F_LastModifyUserId = "";
                item.F_CreatorTime = null;
                item.F_CreatorUserId = "";
            }           

            return orderItem;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            OrderEntity orderEntity = service.FindEntity(t => t.F_Id == keyValue);
            if (orderEntity.F_Status >1)
            {
                throw new Exception("此单据状态无法删除!");
            }
            else
            {
                using (var db = new RepositoryEntity().BeginTrans())
                {
                    //删除主表数据
                    service.Delete(t => t.F_Id == keyValue && t.F_Status == 1);
                    //删除明细表数据
                    serviceDetail.Delete(t => t.F_POId == orderEntity.F_EnCode);
                    //提交
                    db.Commit();
                }
            }
        }

        private OrderEntity GetOrderData(string keyValue)
        {
            OrderEntity orderItem = service.FindEntity(keyValue);
            List<OrderDetailEntity> details = serviceDetail.IQueryable(t => t.F_POId == orderItem.F_EnCode).SortBy(t => t.F_RowId).ToList();
            MaterialApp marterialApp = new MaterialApp();

            foreach (var item in details)
            {
                item.F_UomIDList = marterialApp.getUOM(item.F_ItemID);
                item.F_WarehouseIDList = serviceWarehouse.FindList(t => t.F_EnabledMark == true);
            }
            orderItem.details = details;
            return orderItem;
        }
    }
}
