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
    public class ModuleController : BaseController
    {
        #region 模块管理

        /// <summary>
        /// 模块管理初始界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询模块信息树，只查询父级
        /// </summary>
        /// <returns></returns>
        public String Select()
        {
            BLModule blm = new BLModule();
            List<Module> modules = blm.Select(new HashTableExp("ParentId", "0"));
            List<ExtTreeNode<Module>> nodes = new List<ExtTreeNode<Module>>();
            foreach (var item in modules)
            {
                nodes.Add(new ExtTreeNode<Module>()
                {
                    id = item.ModuleId,
                    text = item.ModuleName,
                    leaf = true,
                    Tobject = item,
                    icon = item.IconUrl,
                });
            }
            return JsonConvert.JavaScriptSerializer(nodes);
        }

        /// <summary>
        /// 根据父级编号分页查询
        /// </summary>
        /// <returns></returns>
        public String SelectByParentId(int limit, int start, String parentId)
        {
            int total;
            List<Module> modules = new BLModule().Select(limit, start, new HashTableExp("ParentId", parentId), out total);
            return JsonConvert.JavaScriptSerializer(new ExtGridRecord(modules, total));
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public String Save(Module module)
        {
            if (string.IsNullOrEmpty(module.ModuleId))
            {
                return Add(module);
            }
            else
            {
                return Update(module);
            }
        }

        /// <summary>
        /// 新增模块
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public String Add(Module module)
        {
            try
            {
                module.ModuleId = Guid.NewGuid().ToString();
                int result = new BLModule().Add(module);
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
        /// <param name="module"></param>
        /// <returns></returns>
        public String Update(Module module)
        {
            try
            {
                int result = new BLModule().Update(module);
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
            int num = new BLModule().Delete(ids);
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "成功删除" + num + "条数据" });
        }

        #endregion

        #region 外部方法(不用控制权限)
        /// <summary>
        /// 根据父级编号和用户信息查询可用(状态可用)模块信息
        /// </summary>
        /// <returns></returns>
        public String SelectAll(String node)
        {
            List<Module> modules = null;
            if (CurrentUser.Id == "-1")
                modules = new BLModule().Select(new HashTableExp("Status","1"));
            else
                modules = new BLModule().SelectByUserId(CurrentUser.Id,"1");
            List<ExtTreeNode<Module>> nodes = new List<ExtTreeNode<Module>>();
            foreach (var item in modules.Where(f => f.ParentId == node).ToList())
            {
                nodes.Add(new ExtTreeNode<Module>()
                {
                    id = item.ModuleId,
                    text = item.ModuleName,
                    leaf = modules.Where(j => j.ParentId == item.ModuleId).Count() == 0,
                    Tobject = item,
                    icon = item.IconUrl
                });
            }
            return JsonConvert.JavaScriptSerializer(nodes);
        }

        /// <summary>
        /// 获得手机模块信息
        /// </summary>
        /// <returns></returns>
        public String SelectPhoneModule()
        {
            List<Module> modules = new BLModule().Select(new HashTableExp());
            return JsonConvert.JavaScriptSerializer(modules);
        }

        #endregion
    }
}
