using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Life.Common;
using Life.BLL.LifeMan;
using Life.Model.LifeMan;
using Life.Model.JiaoXue;

namespace Life.Web.Areas.JiaoXue.Controllers
{
    public class StudentController : BaseController
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
        public String SelectByPage(int limit, int start)
        {
            var tuple = GetParam();
            int total;
            List<Student> incomes = new BLStudent().Select(limit, start, tuple.Item1, out total, tuple.Item2);
            return JsonConvert.JavaScriptSerializer(new ExtGridRecord(incomes, total));
        }
        
        /// <summary>
        /// 获得参数
        /// </summary>
        /// <returns></returns>
        public Tuple<HashTableExp, String> GetParam()
        {
            #region 封装查询方法
            String sqlWhere = "";
            HashTableExp hash = new HashTableExp();
            
            if (!String.IsNullOrEmpty(Request["name"]))
            {
                sqlWhere += string.Format(" and name like '%{0}%'",Request["name"]);
            }
            if (!String.IsNullOrEmpty(Request["cardno"]))
            {
                sqlWhere += string.Format(" and card_no like '%{0}%'", Request["cardno"]);
            }
            #endregion

            Tuple<HashTableExp, String> result = new Tuple<HashTableExp, string>(hash, sqlWhere);
            return result;
        }
        
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public String Save(Student student)
        {
            if (string.IsNullOrEmpty(student.Id))
            {
                return Add(student);
            }
            else
            {
                return Update(student);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public String Add(Student student)
        {
            try
            {
                student.Id = Guid.NewGuid().ToString();
                int result = new BLStudent().Add(student);
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
        /// <param name="student"></param>
        /// <returns></returns>
        public String Update(Student student)
        {
            try
            {
                int result = new BLStudent().Update(student);
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
            int num = new BLStudent().Delete(ids);
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "成功删除" + num + "条数据" });
        }
                
        #endregion

        #region private function

        #endregion

        #region 不需要权限控制的公共方法


        #endregion



    }
}
