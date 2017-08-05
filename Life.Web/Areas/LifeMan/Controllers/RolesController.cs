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
    public class RolesController : BaseController
    {
        #region 角色表管理

        /// <summary>
        /// 角色表初始界面
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
            List<Roles> roless = new BLRoles().Select(limit, start,new HashTableExp(), out total,"and Role_Id<>'-1'");
            return JsonConvert.JavaScriptSerializer(new ExtGridRecord(roless, total));
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public String Save(Roles roles)
        {
            if (string.IsNullOrEmpty(roles.RoleId))
            {
                return Add(roles);
            }
            else
            {
                return Update(roles);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public String Add(Roles roles)
        {
            try
            {
                roles.RoleId = Guid.NewGuid().ToString();
                int result = new BLRoles().Add(roles);
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
        /// <param name="roles"></param>
        /// <returns></returns>
        public String Update(Roles roles)
        {
            try
            {
                int result = new BLRoles().Update(roles);
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
            int num = new BLRoles().Delete(ids);
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "成功删除" + num + "条数据" });
        }

        #endregion


    }
}
