using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Life.Common;
using Life.BLL.LifeMan;
using Life.Model.LifeMan;

namespace Life.Web.Areas.LifeMan.Controllers
{
    public class IncomeController : BaseController
    {
        #region 收入记录表管理

        /// <summary>
        /// 收入记录表初始界面
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
        public String SelectByPage(int limit, int start, Boolean? isMark, Boolean? family)
        {
            var tuple = GetParam(isMark, family);
            int total;
            List<VIncome> incomes = new BLIncome().Select(limit, start, tuple.Item1, out total, tuple.Item2);
            return JsonConvert.JavaScriptSerializer(new ExtGridRecord(incomes, total));
        }

        /// <summary>
        /// 获得收入总金额
        /// </summary>
        /// <returns></returns>
        public String GetTotalPrice(Boolean? isMark, Boolean? family)
        {
            try
            {
                var tuple = GetParam(isMark, family);
                var total = new BLIncome().GetTotalPrice(tuple.Item1, tuple.Item2);
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
        public Tuple<HashTableExp, String> GetParam(Boolean? isMark, Boolean? family)
        {
            #region 封装查询方法
            String sqlWhere = "";
            HashTableExp hash = new HashTableExp();

            if (!String.IsNullOrEmpty(Request["key"]))
            {
                String.Format(" and (Note like '%{0}%')", Request["key"]);
            }

            String yearMonth = Request["yearMonth"];//年月,格式2013-01
            if (!String.IsNullOrEmpty(yearMonth))
            {
                String[] times = yearMonth.Split('-');
                hash.Add("YearMonth", yearMonth);
            }
            if (!String.IsNullOrEmpty(Request["startTime"]))
            {
                sqlWhere += String.Format(" and time >='{0}'", Request["startTime"].GetDateTime().ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!String.IsNullOrEmpty(Request["endTime"]))
            {
                sqlWhere += String.Format(" and time <= '{0}'", Request["endTime"].GetDateTime().ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!String.IsNullOrEmpty(Request["minPrice"]))
            {
                sqlWhere += String.Format(" and Price>='{0}'", Request["minPrice"]);
            }
            if (!String.IsNullOrEmpty(Request["maxPrice"]))
            {
                sqlWhere += String.Format(" and Price<='{0}'", Request["maxPrice"]);
            }

            if (!String.IsNullOrEmpty(Request["CusGroup"]))
            {
                hash.Add("CusGroupMore", Request["CusGroup"]);
            }
            if (!String.IsNullOrEmpty(Request["CusGroupNo"]))
            {
                hash.Add("CusGroupNo", Request["CusGroupNo"]);
            }

            if (CurrentUser.Id != "-1")
            {
                hash.Add("CreateBy", CurrentUser.Id);
            }

            if (isMark == false)
                sqlWhere += String.Format(" and IsMark ='{0}'", 0);
            if (family == false)
                sqlWhere += String.Format(" and FamilyIncome ='{0}'", 0);

            if (!String.IsNullOrEmpty(Request["searchMark"]))
            {
                hash.Add("IsMark", Request["searchMark"]);
            }
            if (!String.IsNullOrEmpty(Request["searchFamily"]))
            {
                hash.Add("FamilyIncome", Request["searchFamily"]);
            }
            #endregion

            Tuple<HashTableExp, String> result = new Tuple<HashTableExp, string>(hash, sqlWhere);
            return result;
        }
        
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="income"></param>
        /// <returns></returns>
        public String Save(Income income)
        {
            if (string.IsNullOrEmpty(income.Id))
            {
                return Add(income);
            }
            else
            {
                return Update(income);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="income"></param>
        /// <returns></returns>
        public String Add(Income income)
        {
            try
            {
                income.Id = Guid.NewGuid().ToString();
                income.CreateBy = CurrentUser.Id;
                income.CreateTime = DateTime.Now;
                int result = new BLIncome().Add(income);
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
        /// <param name="income"></param>
        /// <returns></returns>
        public String Update(Income income)
        {
            try
            {
                income.UpdateBy = CurrentUser.Id;
                income.UpdateTime = DateTime.Now;
                int result = new BLIncome().Update(income);
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
            int num = new BLIncome().Delete(ids);
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "成功删除" + num + "条数据" });
        }

        /// <summary>
        /// 批量修改自定义分组信息
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public String ModifyCusGroup(String ids, String value)
        {
            int num = new BLIncome().ModifyCusGroup(ids, value);
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "成功保存" + num + "条数据" });
        }
        
        #endregion

        #region private function

        #endregion

        #region 不需要权限控制的公共方法
        /// <summary>
        /// 查询所有的自定义分组名称
        /// </summary>
        /// <returns></returns>  
        public String GetAllCusGroup()
        {
            var list=new BLIncome().GetAllCusGroup();
            return JsonConvert.JavaScriptSerializer(list);
        }
        #endregion



    }
}
