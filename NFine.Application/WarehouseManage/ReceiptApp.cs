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
        private IRepositoryEntity<PurOrderEntity> servicePurOrder = new RepositoryEntity<PurOrderEntity>();
        private IRepositoryEntity<PurOrderDetailEntity> servicePurDetail = new RepositoryEntity<PurOrderDetailEntity>();
        private IRepositoryEntity<EntryItemEntity> serviceEntryItem = new RepositoryEntity<EntryItemEntity>();
        /// <summary>
        /// 保存审核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReceiptEntity SubmitForm(ReceiptEntity model)
        {         
            using (var db = new RepositoryEntity().BeginTrans())
            {
                List<ReceiptDetailEntity> receiptDetailEntitys = model.details;
                List<PurOrderDetailEntity> purOrderDetailEntitys = new List<PurOrderDetailEntity>();

                //更新主表数据
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
                //插入主表数据
                else
                {
                    model.Create();
                    SerialNumberDetailApp.GetAutoIncrementCode<ReceiptEntity>(model, model.F_BillType);//获取单据编号
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

                    //获取来源单据明细
                    var purOrderDetail = servicePurDetail.FindEntity(t => t.F_Id == item.F_SourceId);
                    //不回写当前单据的待收发数量以及已收发数量
                    //item.F_BalanceQty = purOrderDetail.F_BalanceQty - item.F_BillQty;
                    //item.F_OperQty = purOrderDetail.F_BillQty - item.F_BalanceQty;
                    if (model.F_Status == 2)
                    {
                        //更新来源单据的已收发货数量与待收发货数量                        
                        purOrderDetail.F_BalanceQty = purOrderDetail.F_BalanceQty - item.F_BillQty;
                        purOrderDetail.F_OperQty = purOrderDetail.F_BillQty - purOrderDetail.F_BalanceQty;
                        db.Update(purOrderDetail);
                        purOrderDetailEntitys.Add(purOrderDetail);
                    }
                }

                //更新来源单据的状态以及插入至分录表
                if (model.F_Status == 2)
                {
                    //更新来源单据主表状态
                    var sourceOrderList = purOrderDetailEntitys.GroupBy(t => new { t.F_EnCode }).Select(g => new {
                        F_EnCode = g.Key.F_EnCode,
                        BillQty =g.Sum(t=>t.F_BillQty),
                        F_OperQty = g.Sum(t => t.F_OperQty),
                        F_BalanceQty = g.Sum(t => t.F_BalanceQty)
                    }).ToList();

                    foreach (var item in sourceOrderList)
                    {
                        int F_IsStockFinished = 0;
                        if (item.BillQty - item.F_OperQty == 0)
                        {
                            F_IsStockFinished = 2;
                        }

                        if (item.BillQty - item.F_OperQty > 0)
                        {
                            F_IsStockFinished = 1;
                        }

                        if((item.BillQty - item.F_OperQty) == item.BillQty)
                        {
                            F_IsStockFinished = 0;
                        }

                        PurOrderEntity purOrderEntity = servicePurOrder.FindEntity(t=>t.F_EnCode==item.F_EnCode);
                        purOrderEntity.F_IsStockFinished = F_IsStockFinished;
                        db.Update(purOrderEntity);                        
                    }

                    //写入物料分录表
                    db.Delete<EntryItemEntity>(t => t.F_BillId == model.F_Id);
                    List<EntryItemEntity> entryItemList = new List<EntryItemEntity>();
                    foreach (var item in receiptDetailEntitys)
                    {
                        EntryItemEntity entryItem = new EntryItemEntity();
                        entryItem.Create();
                        entryItem.F_BillId = model.F_Id;
                        entryItem.F_DetailId = item.F_Id;
                        entryItem.F_BillDate = model.F_BillDate;
                        entryItem.F_PostDate = model.F_PostDate;
                        entryItem.F_Status = model.F_Status;
                        entryItem.F_BillType = item.F_SourceType;
                        entryItem.F_WarehouseID = model.F_WarehouseID;
                        entryItem.F_ItemID = item.F_ItemID;
                        entryItem.F_ItemCode = item.F_ItemCode;
                        entryItem.F_ItemCodeName = item.F_ItemCodeName;
                        entryItem.F_BatchCode = item.F_BatchCode;
                        entryItem.F_ProduceDate = item.F_ProduceDate;
                        entryItem.F_ExpireDate = item.F_ExpireDate;
                        entryItem.F_UomID = item.F_UomID;
                        entryItem.F_BillQty = item.F_BillQty;
                        entryItem.F_OperQty = item.F_OperQty;
                        entryItem.F_BalanceQty = item.F_BalanceQty;
                        entryItem.F_UnitAmount = item.F_UnitAmount;
                        entryItem.F_Amount = item.F_Amount;
                        entryItem.F_UnitCost = item.F_UnitCost;
                        entryItem.F_DeleteMark = item.F_DeleteMark;
                        entryItem.F_EnabledMark = item.F_EnabledMark;
                        entryItem.F_Description = item.F_Description;
                        entryItem.F_LastModifyTime = entryItem.F_CreatorTime;
                        entryItem.F_LastModifyUserId = entryItem.F_LastModifyUserId;
                        entryItem.F_SortCode = entryItem.F_SortCode;
                        entryItemList.Add(entryItem);
                    }
                    db.Insert(entryItemList);
                }


                //List<PurOrderDetailEntity> _OrderDetail = new List<PurOrderDetailEntity>();

                //if (model.F_Status == 2)
                //{
                //    //更新采购订单状态
                //    var orderNoList = receiptDetailEntitys.GroupBy(t => t.F_SourceNo).Select(t => t.Key).ToList();
                //    foreach (var item in orderNoList)
                //    {
                //        var _Order = servicePurOrder.FindEntity(t => t.F_EnCode == item);
                //        int F_BillQty = 0;
                //        int F_OperQty = 0;
                //        int? F_BalanceQty = 0;
                //        foreach (var _item in _OrderDetail.FindAll(t => t.F_EnCode == item))
                //        {
                //            F_BillQty = F_BillQty + _item.F_BillQty;
                //            F_OperQty = F_OperQty + _item.F_OperQty;
                //            F_BalanceQty = F_BalanceQty + (_item.F_BalanceQty == null ? 0 : _item.F_BalanceQty);
                //        }
                //        if ((F_BillQty - F_OperQty) == 0)
                //        {
                //            _Order.F_IsStockFinished = 2;
                //        }
                //        if ((F_BillQty - F_OperQty) > 0)
                //        {
                //            _Order.F_IsStockFinished = 1;
                //        }

                //        if ((F_BillQty - F_OperQty) == F_BillQty)
                //        {
                //            _Order.F_IsStockFinished = 0;
                //        }
                //        db.Update(_Order);
                //    }

                //    //写入物料分录表
                //    db.Delete<EntryItemEntity>(t => t.F_BillId == model.F_Id);
                //    List<EntryItemEntity> entryItemList = new List<EntryItemEntity>();
                //    foreach (var item in receiptDetailEntitys)
                //    {
                //        EntryItemEntity entryItem = new EntryItemEntity();
                //        entryItem.Create();
                //        entryItem.F_BillId = model.F_Id;
                //        entryItem.F_DetailId = item.F_Id;
                //        entryItem.F_BillDate = model.F_BillDate;
                //        entryItem.F_PostDate = model.F_PostDate;
                //        entryItem.F_Status = model.F_Status;
                //        entryItem.F_BillType = item.F_SourceType;
                //        entryItem.F_WarehouseID = model.F_WarehouseID;
                //        entryItem.F_ItemID = item.F_ItemID;
                //        entryItem.F_ItemCode = item.F_ItemCode;
                //        entryItem.F_ItemCodeName = item.F_ItemCodeName;
                //        entryItem.F_BatchCode = item.F_BatchCode;
                //        entryItem.F_ProduceDate = item.F_ProduceDate;
                //        entryItem.F_ExpireDate = item.F_ExpireDate;
                //        entryItem.F_UomID = item.F_UomID;
                //        entryItem.F_BillQty = item.F_BillQty;
                //        entryItem.F_OperQty = item.F_OperQty;
                //        entryItem.F_BalanceQty = item.F_BalanceQty;
                //        entryItem.F_UnitAmount = item.F_UnitAmount;
                //        entryItem.F_Amount = item.F_Amount;
                //        entryItem.F_UnitCost = item.F_UnitCost;
                //        entryItem.F_DeleteMark = item.F_DeleteMark;
                //        entryItem.F_EnabledMark = item.F_EnabledMark;
                //        entryItem.F_Description = item.F_Description;                        
                //        entryItem.F_LastModifyTime = entryItem.F_CreatorTime;
                //        entryItem.F_LastModifyUserId = entryItem.F_LastModifyUserId;                        
                //        entryItem.F_SortCode = entryItem.F_SortCode;
                //        entryItemList.Add(entryItem);
                //    }
                //    db.Insert(entryItemList);
                //}

                //提交
                db.Commit();

                return GetReceiptData(model.F_Id);
            }
        }

        public List<ReceiptEntity> GetList(Pagination pagination, string keyword, string type)
        {
            var expression = ExtLinq.True<ReceiptEntity>();
            if (type == "SHD")
            {
                expression = expression.And(t => t.F_BillType == "CGRK" || t.F_BillType == "XSRK");
            }
            if (type == "FHD")
            {
                expression = expression.And(t => t.F_BillType == "CGCK" || t.F_BillType == "XSCK");
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_EnCode.Contains(keyword));
            }

            return service.FindList(expression, pagination);
        }

        /// <summary>
        /// 获取来源订单明细
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReceiptDetailEntity> GetDetail(List<PurOrderEntity> model)
        {
            int i = 0;
            PurOrderApp orderApp = new PurOrderApp();
            UnitOfMeasureApp unitOfMeasure = new UnitOfMeasureApp();
            var unit = unitOfMeasure.GetList();
            var data = orderApp.GetDetail(model);
            List<ReceiptDetailEntity> ReceiptDetailEntityList = new List<ReceiptDetailEntity>();
            foreach(var item in data)
            {
                i = i + 1;
                ReceiptDetailEntity receiptDetailEntity = new ReceiptDetailEntity();
                receiptDetailEntity.F_SourceId = item.F_Id;
                receiptDetailEntity.F_CorpId = item.F_CorpId;
                receiptDetailEntity.F_SourceNo = item.F_EnCode;
                receiptDetailEntity.F_SourceType = item.F_BillType;
                receiptDetailEntity.F_RowId = i;
                receiptDetailEntity.F_ItemID = item.F_ItemID;
                receiptDetailEntity.F_ItemCode = item.F_ItemCode;
                receiptDetailEntity.F_ItemCodeName = item.F_ItemCodeName;
                receiptDetailEntity.F_UomID = item.F_UomID;
                receiptDetailEntity.F_UomName = unit.Find(t => t.F_Id == item.F_UomID).F_Name;
                receiptDetailEntity.F_OperQty = item.F_OperQty;
                receiptDetailEntity.F_BalanceQty = item.F_BalanceQty;
                receiptDetailEntity.F_UnitAmount = item.F_UnitAmount;
                receiptDetailEntity.F_UnitCost = item.F_UnitCost;
                receiptDetailEntity.F_Amount = item.F_Amount;
                receiptDetailEntity.F_DiscountAmount = item.F_DiscountAmount;
                receiptDetailEntity.F_IsFree = item.F_IsFree;
                receiptDetailEntity.F_IsGift = item.F_IsGift;
                receiptDetailEntity.F_IsGiftName = item.F_IsGift==true ? "是" : "否";
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
        public ReceiptEntity GetForm(string keyValue, string type, int PreNextType)
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

        /// <summary>
        /// 获取订单明细
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        private ReceiptEntity GetReceiptData(string keyValue)
        {
            ReceiptEntity receiptItem = service.FindEntity(keyValue);
            List<ReceiptDetailEntity> details = serviceDetail.IQueryable(t => t.F_EnCode == receiptItem.F_EnCode).SortBy(t => t.F_RowId).ToList();
            MaterialApp marterialApp = new MaterialApp();

            UnitOfMeasureApp unitOfMeasure = new UnitOfMeasureApp();
            var unit = unitOfMeasure.GetList();

            foreach (var item in details)
            {
                item.F_IsGiftName = item.F_IsGift == true ? "是" : "否";
                item.F_UomName = unit.Find(t => t.F_Id == item.F_UomID).F_Name;
            }
            receiptItem.details = details;
            return receiptItem;
        }
    }
}
