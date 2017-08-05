using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Life.BLL.LifeMan;
using Life.Common;
using Life.Model.LifeMan;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;

namespace Life.Web.Areas.LifeMan.Controllers
{
    public class BankCardController : BaseController
    {
        #region 银行卡操作记录表管理

        /// <summary>
        /// 银行卡操作记录表初始界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        public String SelectByPage(int limit, int start,String key,String startTime)
        {
            #region 封装查询方法
            String sqlWhere =string.Format( " and (Bank_Type_Name like '%{0}%' or Create_Name like '%{0}%' or Save_Name like '%{0}%' or Note like '%{0}%')",key);
            if (!String.IsNullOrEmpty(startTime))
            {
                sqlWhere += String.Format(" and time >='{0}'", startTime.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!String.IsNullOrEmpty(Request["endTime"]))
            {
                sqlWhere += String.Format(" and time <= '{0}'", Request["endTime"].GetDateTime().ToString("yyyy-MM-dd HH:mm:ss"));
            }

            if (!String.IsNullOrEmpty(Request["BankType"]))
            {
                sqlWhere += String.Format(" and Bank_Type_Name='{0}'", Request["BankType"]);
            }

            if (!String.IsNullOrEmpty(Request["SaveType"]))
            {
                sqlWhere += String.Format(" and Save_Name='{0}'", Request["SaveType"]);
                
            }

            String yearMonth = Request["yearMonth"];//年月,格式2013-1
            HashTableExp hash = new HashTableExp();
            if (!String.IsNullOrEmpty(yearMonth))
            {
                hash.Add("YearMonth",yearMonth);
            }
            #endregion

            int total;
            List<VBankCard> bankCards = new BLBankCard().Select(limit, start, hash, out total, sqlWhere);
            return JsonConvert.JavaScriptSerializer(new ExtGridRecord(bankCards, total));
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="bankCard"></param>
        /// <returns></returns>
        public String Save(BankCard bankCard)
        {
            //判断Request中是否有接收Files文件
            if (Request.Files.Count != 0 && !String.IsNullOrEmpty(bankCard.ImgUrl))
            {
                //HttpPostedFileBase类，提供对用户上载的单独文件的访问
                //获取到用户上传的文件
                HttpPostedFileBase file = Request.Files[0];
                String fileName = this.Upload(file, "/File/Life/");
                bankCard.ImgUrl = fileName;
            }

            Users user = Session["user"] as Users;
            if (string.IsNullOrEmpty(bankCard.Id))
            {
                bankCard.CreateTime = bankCard.UpdateTime = DateTime.Now;
                bankCard.CreateBy = bankCard.UpdateBy = user.Id;
                return Add(bankCard);
            }
            else
            {
                bankCard.UpdateTime = DateTime.Now;
                bankCard.UpdateBy = user.Id;
                if (String.IsNullOrEmpty(bankCard.ImgUrl))
                    bankCard.ImgUrl = new BLBankCard().Select(bankCard.Id).ImgUrl;
                else
                {
                    var imgUrl = new BLBankCard().Select(bankCard.Id).ImgUrl;
                    if (!String.IsNullOrEmpty(imgUrl))
                    {
                        System.IO.File.Delete(Server.MapPath(imgUrl));
                    }
                }
                return Update(bankCard);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="bankCard"></param>
        /// <returns></returns>
        public String Add(BankCard bankCard)
        {
            try
            {
                bankCard.Id = Guid.NewGuid().ToString();
                int result = new BLBankCard().Add(bankCard);
                if (bankCard.SaveType == 1000200001 && Request["Income"] == "true")
                {
                    //纯收入
                    Income income = new Income()
                    {
                        Id=Guid.NewGuid().ToString(),
                        Time=bankCard.Time,
                        Note=bankCard.Note,
                        Price=bankCard.Price,
                        CreateBy=bankCard.CreateBy,
                        CreateTime=bankCard.CreateTime,
                        IsMark = Request["IsMark"].GetBoolean(),
                        FamilyIncome = Request["FamilyPay"].GetBoolean()
                    };
                    new BLIncome().Add(income);
                }
                if (bankCard.SaveType == 1000200002 && Request["Cost"] == "true")
                {
                    //消费信息
                    LifingCost cost = new LifingCost()
                    {
                        Id=Guid.NewGuid().ToString(),
                        Time=bankCard.Time,
                        Reason=bankCard.Note,
                        Price=bankCard.Price,
                        CostTypeId = Request["CostTypeId"].GetDecimal(),
                        Notes=bankCard.Note,
                        CreateBy = bankCard.CreateBy,
                        CreateTime = bankCard.CreateTime,
                        IsMark = Request["IsMark"].GetBoolean(),
                        FamilyPay = Request["FamilyPay"].GetBoolean()
                    };
                    new BLLifingCost().Add(cost);
                }
                if (result > 0)
                    return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "新增成功" });
                else
                    return JsonConvert.JavaScriptSerializer(new ExtResult() { success = false, msg = "新增失败" });
            }
            catch (Exception ex)
            {
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = false, msg = "新增失败,失败原因:" + ex.Message });
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="bankCard"></param>
        /// <returns></returns>
        public String Update(BankCard bankCard)
        {
            try
            {
                int result = new BLBankCard().Update(bankCard);
                if (result > 0)
                    return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "修改成功" });
                else
                    return JsonConvert.JavaScriptSerializer(new ExtResult() { success = false, msg = "修改失败" });
            }
            catch (Exception ex)
            {
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = false, msg = "修改失败,失败原因:" + ex.Message });
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">Id的集合，如1，2，3</param>
        /// <returns></returns>
        public String Delete(String ids)
        {
            int num = new BLBankCard().Delete(ids);
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "成功删除" + num + "条数据" });
        }

        /// <summary>
        /// 银行卡转账
        /// </summary>
        /// <param name="bankCard">转出的银行卡信息</param>
        /// <param name="outBankCard">转入的银行卡编号</param>
        /// <returns></returns>
        public String Transfer(BankCard bankCard,decimal inBankCard)
        {
            try
            {
                bankCard.Id = Guid.NewGuid().ToString();
                bankCard.SaveType = 1000200002;//取出
                bankCard.CreateTime = bankCard.UpdateTime = DateTime.Now;
                bankCard.CreateBy = bankCard.UpdateBy = CurrentUser.Id;
                new BLBankCard().Add(bankCard);
                bankCard.Id = Guid.NewGuid().ToString();
                bankCard.BankType = inBankCard;
                bankCard.SaveType = 1000200001;//存入
                new BLBankCard().Add(bankCard);
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "操作成功" });
            }
            catch (Exception ex)
            {
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = false, msg = "操作失败,"+ex.Message });
            
            }
        }

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="impExcel"></param>
        /// <returns></returns>
        public String ImportExcel(String impExcel)
        {
            //判断Request中是否有接收Files文件
            if (Request.Files.Count != 0 && !String.IsNullOrEmpty(impExcel))
            {
                //HttpPostedFileBase类，提供对用户上载的单独文件的访问
                //获取到用户上传的文件
                HttpPostedFileBase file = Request.Files[0];

                String fileName = Server.MapPath(this.Upload(file, "/File/Import/", "impBank.xls"));
                DataTable dt = Life.Common.ExcelOperations.ImportDataTableFromExcel(fileName, 0, 0);

                String msg = new BLBankCard().SaveData(dt, CurrentUser.Id);
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = msg });
            }

            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = false, msg = "请选择文件." });

        }

        /// <summary>
        /// 获得模版
        /// </summary>
        /// <returns></returns>
        public void GetTemp()
        {
            String fileName = Server.MapPath("/File/expBank.xls");
            FileStream stream = System.IO.File.OpenRead(fileName);
            IWorkbook excel = ExcelOperations.GetIWorkbook(stream, ExcelType.xls);
            ISheet sheet = excel.GetSheet("Life");

            List<Diction> dictions = new BLL.LifeMan.BLDiction().Select(new HashTableExp("ParentId", "1000200000"), String.Empty);
            for (int i = 0; i < dictions.Count; i++)
            {
                sheet.CreateRow(i + 2).CreateCell(8).SetCellValue(dictions[i].Name);
            }

            dictions = new BLL.LifeMan.BLDiction().Select(new HashTableExp("ParentId", "1000100000"), String.Empty);
            for (int i = 0; i < dictions.Count; i++)
            {
                sheet.CreateRow(i + 5).CreateCell(6).SetCellValue(dictions[i].Name);
            }

            sheet = null;

            MemoryStream ms = new MemoryStream();
            excel.Write(ms);
            Response.AppendHeader("Content-Disposition", "attachment;filename=银行信息模版.xls");
            Response.BinaryWrite(ms.ToArray());
            Response.End();
            ms.Close();
            ms = null;
        }
        #endregion

        #region 银行卡按月汇总
        public ActionResult CollectionByMonth()
        {
            return View();
        }

        /// <summary>
        /// 根据月份汇总银行卡信息
        /// </summary>
        /// <returns></returns>
        public String GetCollectionByMonth()
        {
            DataTable dt = new BLBankCard().GetCollectionByMonth();
            return JsonConvert.ToJson(dt);
        }
        #endregion

        #region 计算所有余额
        public String CalcAllBalance()
        {
            bool con = new BLBankCard().CalcAllBalance();
            if(con)
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "操作成功" });
            else
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = false, msg = "操作失败" });
        }
        #endregion


        #region 查询所有银行卡的最后一条数据
        /// <summary>
        /// 显示余额界面
        /// </summary>
        /// <returns></returns>
        public ActionResult CalcShow()
        {
            return View();
        }

        /// <summary>
        /// 查询所有银行卡的最后一条数据
        /// </summary>
        /// <returns></returns>
        public String SelectCalc(String time)
        {
            List<VBankCard> list = new BLBankCard().SelectCalc(time);
            return JsonConvert.JavaScriptSerializer(new ExtGridRecord(list, 0));
        } 
        #endregion
    }
}
