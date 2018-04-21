﻿/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.BaseManage;
using NFine.Domain.IRepository.BaseManage;
using System.Collections.Generic;
using System.Linq;
using NFine.Repository.BaseManage;
using NFine.Data;
using System.IO;
using System;
using System.Drawing;
using System.Web;

namespace NFine.Application.BaseManage
{
    public class MaterialApp
    {
        private IMaterialRepository service = new MaterialRepository();

        public List<MaterialEntity> GetList(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<MaterialEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.F_ItemGroupID == itemId || t.F_ItemCategoryID==itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }

            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
                expression = expression.And(t => t.F_CorpId == CompanyId);
            }
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }
        public List<MaterialEntity> GetItemList(string enCode)
        {
            return service.GetItemList(enCode);
        }
        public MaterialEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Delete<MaterialEntity>(t => t.F_Id == keyValue);
                    db.Delete<MaterialUomEntity>(t => t.F_MaterialId == keyValue);
                }
                db.Commit();
            }
        }
        public void SubmitForm(MaterialEntity materialEntity, MaterialUomEntity[] materialuomEntitys,MaterialPictureEntity materialpictureEntity, string keyValue)
        { 
            string CompanyId = OperatorProvider.Provider.GetCurrent().CompanyId;
            if (!string.IsNullOrEmpty(keyValue))
            {
                materialEntity.Modify(keyValue);
                materialEntity.F_BaseUOM = "";
                materialEntity.F_SalesUOM = "";
                materialEntity.F_PurchaseUOM = "";
                materialEntity.F_SalesPrice = 0;
                materialEntity.F_PurchasePrice = 0;
            }
            else
            {
                materialEntity.Create();
                materialEntity.F_DeleteMark = false;                
            }

            //封面图片
            materialpictureEntity.Create();
            materialpictureEntity.F_MaterialId = materialEntity.F_Id;
            materialpictureEntity.F_CorpId = materialEntity.F_CorpId;
            materialpictureEntity.F_DeleteMark = false;
            materialpictureEntity.F_PictureType = 0;

            string base64Data = materialpictureEntity.F_Picture;
            //获取文件储存路径            
            string suffix = base64Data.Split(new char[] { ';' })[0].Substring(base64Data.IndexOf('/') + 1);//获取后缀名
            string newFileName = "Material_" + materialEntity.F_Id + ".png";// + suffix;
            string strPath = HttpContext.Current.Server.MapPath("~/UploadImgPath/" + materialEntity.F_CorpId + "/"); //获取当前项目所在目录 
            //获取图片并保存
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(base64Data.Split(',')[1]));
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
            new Bitmap(stream).Save(strPath+ newFileName);
            materialpictureEntity.F_Picture = newFileName;


            //对应单位
            List<MaterialUomEntity> materialuomEntitysTemp = new List<MaterialUomEntity>();
            foreach (var items in materialuomEntitys)
            {
                items.Create();
                items.F_MaterialId = materialEntity.F_Id;
                items.F_CorpId = CompanyId;
                items.F_RateQty = (items.F_RateQty <= 0 ? 1 : items.F_RateQty);
                if(items.F_UomId!=null && items.F_UomId != "")
                {                  
                    if(items.F_UomType==1)
                    {
                        materialEntity.F_BaseUOM = items.F_UomId;
                        materialEntity.F_SalesUOM = (items.F_IsSalesUOM == true ? items.F_UomId : "");
                        materialEntity.F_PurchaseUOM = (items.F_IsPurchaseUOM == true ? items.F_UomId : "");
                        materialEntity.F_SalesPrice = (items.F_IsSalesUOM == true ? items.F_SalesPrice : 0);
                        materialEntity.F_PurchasePrice = (items.F_IsPurchaseUOM == true ? items.F_PurchasePrice : 0);
                    }
                    items.F_DeleteMark = false;
                    materialuomEntitysTemp.Add(items);
                }                
            }

            service.SubmitForm(materialEntity, materialuomEntitysTemp, materialpictureEntity, keyValue);
        }
    }
}