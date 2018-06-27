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
using NFine.Domain.IRepository.Base;
using NFine.Repository.Base;

namespace NFine.Application.SystemManage
{
    public class SerialNumberDetailApp
    {
        private IRepositoryEntity<SerialNumberEntity> service = new RepositoryEntity<SerialNumberEntity>();
        private IRepositoryEntity<SerialNumberDetailEntity> serviceDetail = new RepositoryEntity<SerialNumberDetailEntity>();

        public string GetAutoIncrementCode(string F_EnCode)
        {
            DateTime dt = DateTime.Now;
            var CodeResult = "";
            var item= service.FindList(t=>t.F_EnCode== F_EnCode).FirstOrDefault();
            int length = item.F_EndingNumber.ToString().Length;
            string Id = item.F_Id;
            var MaintainMethod = item.F_MaintainMethod;

            var itemDetail = serviceDetail.FindList(t => t.F_SerialId == Id).FirstOrDefault();
            if (itemDetail == null)
            {
                itemDetail = new SerialNumberDetailEntity();
                itemDetail.Create();
                itemDetail.F_SerialId = Id;
                itemDetail.F_EnCode = item.F_EnCode;
                itemDetail.F_FirstNumber = item.F_StartingNumber;
                itemDetail.F_IncrementByNumber = item.F_IncrementByNumber;
                itemDetail.F_LastNumber = item.F_EndingNumber;
                itemDetail.F_NumberDate = dt;
                itemDetail.F_WarningNumber = item.F_WarningNumber;
                itemDetail.F_LastNumberUsed = item.F_IncrementByNumber;
                itemDetail.F_NumberLength = item.F_EndingNumber.ToString().Length;
                serviceDetail.Insert(itemDetail);

            }
            else
            {
                itemDetail.Modify(itemDetail.F_Id);
                itemDetail.F_EnCode = item.F_EnCode;
                itemDetail.F_FirstNumber = item.F_StartingNumber;
                itemDetail.F_IncrementByNumber = item.F_IncrementByNumber;
                itemDetail.F_LastNumber = item.F_EndingNumber;
                itemDetail.F_NumberDate = dt;
                itemDetail.F_WarningNumber = item.F_WarningNumber;
                itemDetail.F_LastNumberUsed = itemDetail.F_LastNumberUsed + item.F_IncrementByNumber;
                itemDetail.F_NumberLength = item.F_EndingNumber.ToString().Length;
                serviceDetail.Update(itemDetail);
            }

            switch (MaintainMethod)
            {
                case "0":
                    CodeResult = item.F_Prefix + itemDetail.F_LastNumberUsed.ToString().PadLeft(length, '0');
                    break;
                case "1":
                    CodeResult = item.F_Prefix + dt.ToString("yyMM") + itemDetail.F_LastNumberUsed.ToString().PadLeft(length, '0');
                    break;
                case "2":
                    CodeResult = item.F_Prefix + dt.ToString("yyMM") + itemDetail.F_LastNumberUsed.ToString().PadLeft(length, '0');
                    break;
                case "3":
                    CodeResult = item.F_Prefix + dt.ToString("yyMMdd") + itemDetail.F_LastNumberUsed.ToString().PadLeft(length, '0');
                    break;
                default:
                    CodeResult = item.F_Prefix + itemDetail.F_LastNumberUsed.ToString().PadLeft(length, '0');
                    break;
            }
            return CodeResult;
        }
        
    }
}
