using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Life.Common;
using Life.Model.LifeMan;
using Life.BLL.LifeMan;
using System.Linq.Expressions;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;

namespace Life.Web.Areas.LifeMan.Controllers
{
    public class LifingCostController : BaseController
    {
        #region 生活费操作管理管理

        /// <summary>
        /// 生活费操作管理初始界面
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
        public String SelectByPage(int limit, int start, String startTime, String endTime, String key, String CostTypeId, Boolean? isMark, Boolean? family)
        {
            var tuple = GetParam(startTime, endTime, key, CostTypeId, isMark, family);
            int total;
            List<VLifingCost> lifingCosts = new BLLifingCost().Select(limit, start, tuple.Item1, out total, tuple.Item2);
            return JsonConvert.JavaScriptSerializer(new ExtGridRecord(lifingCosts, total));
        }

        /// <summary>
        /// 查询总金额
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="key"></param>
        /// <param name="CostTypeId"></param>
        /// <param name="isMark"></param>
        /// <param name="family"></param>
        /// <returns></returns>
        public String GetTotalPrice(String startTime, String endTime, String key, String CostTypeId, Boolean? isMark, Boolean? family)
        {
            try
            {
                var tuple = GetParam(startTime, endTime, key, CostTypeId, isMark, family);
                var total = new BLLifingCost().GetTotalPrice(tuple.Item1, tuple.Item2);
                return JsonConvert.JavaScriptSerializer(new ExtResult()
                {
                    data = total,
                    success = true,
                    msg = "查询成功"
                });
            }
            catch
            {
                return JsonConvert.JavaScriptSerializer(new ExtResult()
                {
                    data = null,
                    success = false,
                    msg = "查询失败"
                });
            }
        }

        /// <summary>
        /// 获得参数
        /// </summary>
        /// <returns></returns>
        public Tuple<HashTableExp, String> GetParam(String startTime, String endTime, String key, String CostTypeId, Boolean? isMark, Boolean? family)
        {
            #region 封装查询方法
            String sqlWhere = String.Format(" and (Reason like '%{0}%' or Create_Name like '%{0}%' or Notes like '%{0}%')", key);
            if (!String.IsNullOrEmpty(startTime))
            {
                sqlWhere += String.Format(" and time >='{0}'", startTime.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!String.IsNullOrEmpty(endTime))
            {
                sqlWhere += String.Format(" and time <= '{0}'", endTime.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!String.IsNullOrEmpty(CostTypeId))
            {
                sqlWhere += String.Format(" and Cost_Type_Id in({0})", CostTypeId.GetIds());
            }

            HashTableExp hash = new HashTableExp();
            String yearMonth = Request["yearMonth"];//年月,格式2013-1
            if (!String.IsNullOrEmpty(yearMonth))
            {
                hash.Add("YearMonth", yearMonth.GetIds());
            }

            String costTypeName = Request["CostTypeName"];//消费类型
            if (!String.IsNullOrEmpty(costTypeName))
            {
                sqlWhere += String.Format(" and Cost_Type_Name ='{0}'", costTypeName);
            }

            if (!String.IsNullOrEmpty(Request["minPrice"]))
            {
                sqlWhere += String.Format(" and Price>='{0}'",Request["minPrice"]);
            }
            if (!String.IsNullOrEmpty(Request["maxPrice"]))
            {
                sqlWhere += String.Format(" and Price<='{0}'", Request["maxPrice"]);
            }

            if (isMark == false)
                sqlWhere += String.Format(" and IsMark ='{0}'", 0);
            if (family == false)
                sqlWhere += String.Format(" and FamilyPay ='{0}'", 0);

            if (!String.IsNullOrEmpty(Request["CusGroup"]))
            {
                hash.Add("CusGroupMore", Request["CusGroup"]);
            }
            if (!String.IsNullOrEmpty(Request["CusGroupNo"]))
            {
                hash.Add("CusGroupNo", Request["CusGroupNo"]);
            }
            if (!String.IsNullOrEmpty(Request["searchMark"]))
            {
                hash.Add("IsMark", Request["searchMark"]);
            }
            if (!String.IsNullOrEmpty(Request["searchFamily"]))
            {
                hash.Add("FamilyPay", Request["searchFamily"]);
            }
            //查询当前用户信息
            if (CurrentUser.Id != "-1")
            {
                hash.Add("CreateBy", CurrentUser.Id);
            }
            #endregion

            Tuple<HashTableExp, String> result = new Tuple<HashTableExp, string>(hash, sqlWhere);
            return result;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="lifingCost"></param>
        /// <returns></returns>
        public String Save(LifingCost lifingCost)
        {
            //判断Request中是否有接收Files文件
            if (Request.Files.Count != 0 && !String.IsNullOrEmpty(lifingCost.ImgUrl))
            {
                //HttpPostedFileBase类，提供对用户上载的单独文件的访问
                //获取到用户上传的文件
                HttpPostedFileBase file = Request.Files[0];
                String fileName = this.Upload(file, "/File/Life/");
                lifingCost.ImgUrl = fileName;
            }

            //创建人和修改者赋值
            Users user = Session["user"] as Users;
            if (string.IsNullOrEmpty(lifingCost.Id))
            {
                lifingCost.CreateTime = DateTime.Now;
                lifingCost.CreateBy = user.Id;
                return Add(lifingCost);
            }
            else
            {
                lifingCost.UpdateTime = DateTime.Now;
                lifingCost.UpdateBy = user.Id;
                if (String.IsNullOrEmpty(lifingCost.ImgUrl))
                    lifingCost.ImgUrl = new BLLifingCost().Select(lifingCost.Id).ImgUrl;
                else
                {
                    var imgUrl = new BLLifingCost().Select(lifingCost.Id).ImgUrl;
                    if (!String.IsNullOrEmpty(imgUrl))
                    {
                        System.IO.File.Delete(Server.MapPath(imgUrl));
                    }
                }
                return Update(lifingCost);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="lifingCost"></param>
        /// <returns></returns>
        public String Add(LifingCost lifingCost)
        {
            try
            {
                lifingCost.Id = Guid.NewGuid().ToString();
                int result = new BLLifingCost().Add(lifingCost);
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
        /// <param name="lifingCost"></param>
        /// <returns></returns>
        public String Update(LifingCost lifingCost)
        {
            try
            {
                int result = new BLLifingCost().Update(lifingCost);
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
            int num = new BLLifingCost().Delete(ids);
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "成功删除" + num + "条数据" });
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

                String fileName = Server.MapPath(this.Upload(file, "/File/Import/", "imp.xls"));
                DataTable dt = Life.Common.ExcelOperations.ImportDataTableFromExcel(fileName, 0, 0);

                String msg = new BLLifingCost().SaveData(dt, CurrentUser.Id);
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
            String fileName = Server.MapPath("/File/exp.xls");
            FileStream stream = System.IO.File.OpenRead(fileName);
            IWorkbook excel = ExcelOperations.GetIWorkbook(stream, ExcelType.xls);
            ISheet sheet = excel.GetSheet("Life");

            List<Diction> dictions = new BLL.LifeMan.BLDiction().Select(new HashTableExp("ParentId", "1000300000"), String.Empty);
            for (int i = 0; i < dictions.Count; i++)
            {
                sheet.CreateRow(i + 2).CreateCell(8).SetCellValue(dictions[i].Name);
            }

            sheet = null;

            MemoryStream ms = new MemoryStream();
            excel.Write(ms);
            Response.AppendHeader("Content-Disposition", "attachment;filename=生活费模版.xls");
            Response.BinaryWrite(ms.ToArray());
            Response.End();
            ms.Close();
            ms = null;
        }

        public String GetReasons(String key)
        {
            List<LifingCost> list = new BLLifingCost().GetReasons(key);
            return JsonConvert.JavaScriptSerializer(new ExtGridRecord(list, list.Count));
            //return JsonConvert.JavaScriptSerializer(list);
        }

        /// <summary>
        /// 批量修改自定义分组信息
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public String ModifyCusGroup(String ids, String value)
        {
            int num = new BLLifingCost().ModifyCusGroup(ids, value);
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "成功保存" + num + "条数据" });
        }

        /// <summary>
        /// 查询所有的自定义分组名称
        /// </summary>
        /// <returns></returns>  
        public String GetAllCusGroup()
        {
            var list = new BLLifingCost().GetAllCusGroup();
            return JsonConvert.JavaScriptSerializer(list);
        }

        /// <summary>
        /// 同时向支出表和收入表中写入数据
        /// </summary>
        /// <param name="outUser">支出用户</param>
        /// <param name="outPrice">支出金额</param>
        /// <param name="inUser">收入用户</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public String OutIn(string time,string outUser, string outPrice, string inUser, string remark)
        {
            if (outUser == inUser)
            {
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = false, msg = "操作失败,支出和收入用户不能一样" });
            }

            //组装数据
            LifingCost life = new LifingCost();
            life.Id = Guid.NewGuid().ToString();
            life.Time = time.GetDateTime();
            life.Reason = remark;
            life.Price = outPrice.GetDouble();
            life.CostTypeId = 1000300010;
            life.FamilyPay = true;
            life.Notes = remark+" 快速新增";
            life.CreateBy = outUser;
            life.UpdateBy = CurrentUser.Id;
            life.CreateTime = DateTime.Now;
            life.UpdateTime = DateTime.Now;

            Income income = new Income();
            income.Id = Guid.NewGuid().ToString();
            income.Time = time.GetDateTime();
            income.Price = outPrice.GetDouble();
            income.Note = remark;
            income.FamilyIncome = true;
            income.CreateBy = inUser;
            income.CreateTime = DateTime.Now;
            income.UpdateBy = CurrentUser.Id;
            income.UpdateTime = DateTime.Now;

            //写入数据库
            try
            {
                new BLLifingCost().Add(life);
                new BLIncome().Add(income);
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "操作成功" });

            }
            catch (Exception ex)
            {
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "操作失败,失败原因:"+ex.Message });
            }
        }
        #endregion

        #region 按月份汇总

        public ActionResult CollectionByMonth()
        {
            return View();
        }

        /// <summary>
        /// 生活费汇总，列表汇总
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="isMark"></param>
        /// <param name="family"></param>
        /// <param name="costTypeId"></param>
        /// <returns></returns>
        public string GetCollectionByMonth(String beginTime, String endTime, Boolean? isMark, Boolean? family, String costTypeId)
        {
            String costTypeIds = String.Empty;
            if (isMark == true)
                isMark = null;
            if (family == true)
                family = null;
            if (costTypeId != null)
                costTypeIds = costTypeId.GetIds();

            List<CostMonthCol> list = new BLLifingCost().GetCollectionByMonth(beginTime, endTime, isMark, family, costTypeIds, CurrentUser.Id);
            return JsonConvert.JavaScriptSerializer(list);
        }

        /// <summary>
        /// 图形汇总信息
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public string GetCollection(String beginTime, String endTime)
        {
            String sqlWhere = CurrentUser.Id == "-1" ? "" : String.Format(" and create_by='{0}'", CurrentUser.Id);

            DataTable dt = new BLLifingCost().GetCollection(sqlWhere, beginTime, endTime);
            return JsonConvert.ToJson(dt);
        }
        #endregion

        #region 按日期汇总

        public string GetCollectionByDay(String time, String beginTime, String endTime, Boolean? isMark, Boolean? family, String costTypeId)
        {
            String costTypeIds = String.Empty;
            if (isMark == true)
                isMark = null;
            if (family == true)
                family = null;
            if (costTypeId != null)
                costTypeIds = costTypeId.GetIds();
            if (!String.IsNullOrEmpty(time))
                time = time.GetIds();
            else
                time = "''";

            DataTable dt = new BLLifingCost().GetCollectionByDay(time, beginTime, endTime, isMark, family, costTypeIds, CurrentUser.Id);
            return JsonConvert.ToJson(dt);
        }
        #endregion

        #region 按类型汇总生活费

        public String GetCollectionByType(String time)
        {
            String sqlWhere = CurrentUser.Id == "-1" ? "" : String.Format(" and create_by='{0}'", CurrentUser.Id);
            DataTable dt = new BLLifingCost().GetCollectionByType(time, sqlWhere);
            return JsonConvert.ToJson(dt);
        }

        public String GetCollectionType(string time, string beginTime, string endTime, bool? isMark, bool? family, string costTypeId)
        {
            String costTypeIds = String.Empty;
            if (isMark == true)
                isMark = null;
            if (family == true)
                family = null;
            if (costTypeId != null)
                costTypeIds = costTypeId.GetIds();
            if (!String.IsNullOrEmpty(time))
                time = time.GetIds();
            else
                time = "''";

            DataTable dt = new BLLifingCost().GetCollectionType(time, beginTime, endTime, isMark, family, costTypeIds, CurrentUser.Id);
            return JsonConvert.ToJson(dt);
        }
        #endregion

        #region 生活费查询
        /// <summary>
        /// 查询生活费界面，不区分权限
        /// </summary>
        /// <returns></returns>
        public ActionResult Search()
        {
            return View();
        }

        #endregion

    }
}
