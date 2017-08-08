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
    public class TeacherController : BaseController
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
            List<Teacher> incomes = new BLTeacher().Select(limit, start, tuple.Item1, out total, tuple.Item2);
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
            if (!String.IsNullOrEmpty(Request["phone"]))
            {
                sqlWhere += string.Format(" and phone like '%{0}%'", Request["phone"]);
            }
            #endregion

            Tuple<HashTableExp, String> result = new Tuple<HashTableExp, string>(hash, sqlWhere);
            return result;
        }
        
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="Teacher"></param>
        /// <returns></returns>
        public String Save(Teacher teacher)
        {
            if (string.IsNullOrEmpty(teacher.Id))
            {
                return Add(teacher);
            }
            else
            {
                return Update(teacher);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="Teacher"></param>
        /// <returns></returns>
        public String Add(Teacher teacher)
        {
            try
            {
                teacher.Id = Guid.NewGuid().ToString();
                int result = new BLTeacher().Add(teacher);
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
        /// <param name="Teacher"></param>
        /// <returns></returns>
        public String Update(Teacher teacher)
        {
            try
            {
                int result = new BLTeacher().Update(teacher);
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
            int num = new BLTeacher().Delete(ids);
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "成功删除" + num + "条数据" });
        }
                
        #endregion

        #region private function

        #endregion

        #region 不需要权限控制的公共方法


        #endregion



    }
}
