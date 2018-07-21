/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/

using NFine.Domain.IRepository.Base;
using NFine.Repository.Base;
using NFine.Application.SystemManage;
using System.Collections.Generic;
using NFine.Code;
using System;
using System.Linq;
using NFine.Application.BaseManage;
using NFine.Domain.Entity.BaseManage;
using NFine.Domain.Entity.WarehouseManage;
using NFine.Domain.Entity.PurchaseManage;
using NFine.Application.PurchaseManage;

namespace NFine.Application.WarehouseManage
{
    public class ReceiptApp
    {
        private IRepositoryEntity<ReceiptEntity> service = new RepositoryEntity<ReceiptEntity>();
        private IRepositoryEntity<ReceiptDetailEntity> serviceDetail = new RepositoryEntity<ReceiptDetailEntity>();
        private IRepositoryEntity<WarehouseEntity> serviceWarehouse = new RepositoryEntity<WarehouseEntity>();

        public ReceiptEntity SubmitForm(ReceiptEntity model)
        {
            if (model.F_BillType != 1 && model.F_BillType != -1)
            {
                throw new Exception("此单据为非法单据!");
            }

            List<ReceiptDetailEntity> receiptDetailEntitys = model.details;
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
                        SerialNumberDetailApp.GetAutoIncrementCode<ReceiptEntity>(model, "PurchaseReceipt");//获取收货单编号  
                    }
                    else
                    {
                        SerialNumberDetailApp.GetAutoIncrementCode<ReceiptEntity>(model, "SalesShipment");//获取发货单编号   
                    }

                    db.Insert(model);
                }

                //删除从表数据
                db.Delete<ReceiptDetailEntity>(t => t.F_EnCode == model.F_EnCode);
                //插入从表数据
                foreach (var item in receiptDetailEntitys)
                {
                    item.Create();
                    item.F_EnCode = model.F_EnCode;
                    db.Insert(item);
                }

                //提交
                db.Commit();

                return GetReceiptData(model.F_Id);
            }
        }

        public List<ReceiptEntity> GetList(Pagination pagination, string keyword, int type)
        {
            var expression = ExtLinq.True<ReceiptEntity>();
            expression = expression.And(t => t.F_BillType == type);
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_EnCode.Contains(keyword));
            }

            return service.FindList(expression, pagination);
        }

        public List<ReceiptDetailEntity> GetDetail(List<OrderEntity> model)
        {
            int i = 0;
            OrderApp orderApp = new OrderApp();
            var data = orderApp.GetDetail(model);
            List<ReceiptDetailEntity> ReceiptDetailEntityList = new List<ReceiptDetailEntity>();
            foreach(var item in data)
            {
                i = i + 1;
                ReceiptDetailEntity receiptDetailEntity = new ReceiptDetailEntity();
                receiptDetailEntity.F_CorpId = item.F_CorpId;
                receiptDetailEntity.F_SourceNo = item.F_EnCode;
                receiptDetailEntity.F_SourceType = "purorder";
                receiptDetailEntity.F_RowId = i;
                receiptDetailEntity.F_ItemID = item.F_ItemID;
                receiptDetailEntity.F_ItemCode = item.F_ItemCode;
                receiptDetailEntity.F_ItemCodeName = item.F_ItemCodeName;
                receiptDetailEntity.F_UomID = item.F_UomID;
                receiptDetailEntity.F_BillQty = item.F_BillQty;
                receiptDetailEntity.F_OperQty = item.F_OperQty;
                receiptDetailEntity.F_BalanceQty = item.F_BalanceQty;
                receiptDetailEntity.F_UnitAmount = item.F_UnitAmount;
                receiptDetailEntity.F_UnitCost = item.F_UnitCost;
                receiptDetailEntity.F_Amount = item.F_Amount;
                receiptDetailEntity.F_DiscountAmount = item.F_DiscountAmount;
                receiptDetailEntity.F_IsFree = item.F_IsFree;
                receiptDetailEntity.F_IsGift = item.F_IsGift;
                receiptDetailEntity.F_BatchCode = item.F_ItemID;
                receiptDetailEntity.F_SortCode = item.F_SortCode;
                ReceiptDetailEntityList.Add(receiptDetailEntity);
            }
            return ReceiptDetailEntityList;
        }
        /// <summary>
        /// 获取单据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ReceiptEntity GetForm(string keyValue, int type, int PreNextType)
        {
            //下一页
            if (PreNextType == 1)
            {
                var data = service.IQueryable(t => t.F_BillType == type).AsEnumerable().OrderByDescending(t => t.F_CreatorTime).SkipWhile(t => t.F_Id != keyValue).Skip(1).FirstOrDefault();
                if (data == null)
                {
                    throw new Exception("已经是最后一条！");
                }
                else
                {
                    return GetReceiptData(data.F_Id);
                }

            }
            //上一页
            else if (PreNextType == -1)
            {
                var data = service.IQueryable(t => t.F_BillType == type).AsEnumerable().OrderBy(t => t.F_CreatorTime).SkipWhile(t => t.F_Id != keyValue).Skip(1).FirstOrDefault();
                if (data == null)
                {
                    throw new Exception("已经是第一条！");
                }
                else
                {
                    return GetReceiptData(data.F_Id);
                }
            }
            else
            {
                return GetReceiptData(keyValue);
            }
        }
        /// <summary>
        /// 复制单据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ReceiptEntity GetCopyForm(string keyValue)
        {
            var receiptItem = GetReceiptData(keyValue);

            receiptItem.F_Id = "";
            receiptItem.F_Status = 0;
            receiptItem.F_CreatorTime = null;
            receiptItem.F_CreatorUserId = "";
            receiptItem.F_ConfirmTime = null;
            receiptItem.F_ConfirmUserId = "";
            receiptItem.F_LastModifyTime = null;
            receiptItem.F_LastModifyUserId = "";
            receiptItem.F_PrintNums = 0;
            receiptItem.F_EnCode = "";

            foreach (var item in receiptItem.details)
            {
                item.F_Id = "";
                item.F_EnCode = "";
                item.F_LastModifyTime = null;
                item.F_LastModifyUserId = "";
                item.F_CreatorTime = null;
                item.F_CreatorUserId = "";
            }

            return receiptItem;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            ReceiptEntity receiptEntity = service.FindEntity(t => t.F_Id == keyValue);
            if (receiptEntity.F_Status > 1)
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
                    serviceDetail.Delete(t => t.F_EnCode == receiptEntity.F_EnCode);
                    //提交
                    db.Commit();
                }
            }
        }

        private ReceiptEntity GetReceiptData(string keyValue)
        {
            ReceiptEntity receiptItem = service.FindEntity(keyValue);
            List<ReceiptDetailEntity> details = serviceDetail.IQueryable(t => t.F_EnCode == receiptItem.F_EnCode).SortBy(t => t.F_RowId).ToList();
            MaterialApp marterialApp = new MaterialApp();

            foreach (var item in details)
            {
                item.F_UomIDList = marterialApp.getUOM(item.F_ItemID);
                //item.F_WarehouseIDList = serviceWarehouse.FindList(t => t.F_EnabledMark == true);
            }
            receiptItem.details = details;
            return receiptItem;
        }
    }
}
