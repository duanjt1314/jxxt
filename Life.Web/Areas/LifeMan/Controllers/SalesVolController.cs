using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Life.BLL.LifeMan;
using Life.Model.LifeMan;
using Life.Common;

namespace Life.Web.Areas.LifeMan.Controllers
{
    public class SalesVolController : BaseController
    {
        #region 扫帚销售表管理

        /// <summary>
        /// 扫帚销售表初始界面
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
            int total;
            List<SalesVol> salesVols = new BLSalesVol().Select(limit, start, null, out total);
            return JsonConvert.JavaScriptSerializer(new ExtGridRecord(salesVols, total));
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="salesVol"></param>
        /// <returns></returns>
        public String Save(SalesVol salesVol)
        {
            if (string.IsNullOrEmpty(salesVol.Id))
            {
                return Add(salesVol);
            }
            else
            {
                return Update(salesVol);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="salesVol"></param>
        /// <returns></returns>
        public String Add(SalesVol salesVol)
        {
            try
            {
                salesVol.Id = Guid.NewGuid().ToString();
                salesVol.CreateBy = CurrentUser.Id;
                salesVol.CreateTime = DateTime.Now;
                int result = new BLSalesVol().Add(salesVol);
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
        /// <param name="salesVol"></param>
        /// <returns></returns>
        public String Update(SalesVol salesVol)
        {
            try
            {
                salesVol.UpdateBy = CurrentUser.Id;
                salesVol.UpdateTime = DateTime.Now;
                int result = new BLSalesVol().Update(salesVol);
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
            int num = new BLSalesVol().Delete(ids);
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "成功删除" + num + "条数据" });
        }

        #endregion


    }
}
