using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Life.Common;
using Life.BLL.Flow;
using Life.Model.Flow;

namespace Life.Web.Areas.Flow.Controllers
{
    public class FlowTypeController : BaseController
    {
        #region 基本操作

        /// <summary>
        /// 流程类型表初始界面
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
            List<FlowType> flowtypes = new BLFlowType().Select(limit, start, null, out total,"");
            return JsonConvert.JavaScriptSerializer(new ExtGridRecord(flowtypes, total));
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="flowtype"></param>
        /// <returns></returns>
        public String Save(FlowType flowtype)
        {
            if (string.IsNullOrEmpty(flowtype.Id))
            {
                return Add(flowtype);
            }
            else
            {
                return Update(flowtype);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="flowtype"></param>
        /// <returns></returns>
        public String Add(FlowType flowtype)
        {
            try
            {
                flowtype.Id = Guid.NewGuid().ToString();
                int result = new BLFlowType().Add(flowtype);
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
        /// <param name="flowtype"></param>
        /// <returns></returns>
        public String Update(FlowType flowtype)
        {
            try
            {
                int result = new BLFlowType().Update(flowtype);
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
            int num = new BLFlowType().Delete(ids);
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "成功删除" + num + "条数据" });
        }
        #endregion

        #region private function

        #endregion

        #region 不需要权限控制的公共方法

        #endregion

    }
}
