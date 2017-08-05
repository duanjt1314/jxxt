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
    public class SysConfigController : BaseController
    {
        #region 系统配置管理

        /// <summary>
        /// 系统配置初始界面
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
            List<SysConfig> sysConfigs = new BLSysConfig().Select(limit, start, null, out total,"");
            return JsonConvert.JavaScriptSerializer(new ExtGridRecord(sysConfigs, total));
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sysConfig"></param>
        /// <returns></returns>
        public String Save(SysConfig sysConfig)
        {
            if (string.IsNullOrEmpty(sysConfig.Id))
            {
                return Add(sysConfig);
            }
            else
            {
                return Update(sysConfig);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sysConfig"></param>
        /// <returns></returns>
        public String Add(SysConfig sysConfig)
        {
            try
            {
                sysConfig.Id = Guid.NewGuid().ToString();
                sysConfig.CreateBy = CurrentUser.Id;
                sysConfig.CreateTime = DateTime.Now;
                int result = new BLSysConfig().Add(sysConfig);
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
        /// <param name="sysConfig"></param>
        /// <returns></returns>
        public String Update(SysConfig sysConfig)
        {
            try
            {
                sysConfig.UpdateBy = CurrentUser.Id;
                sysConfig.UpdateTime = DateTime.Now;
                int result = new BLSysConfig().Update(sysConfig);
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
            int num = new BLSysConfig().Delete(ids);
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "成功删除" + num + "条数据" });
        }

        #endregion


    }
}
