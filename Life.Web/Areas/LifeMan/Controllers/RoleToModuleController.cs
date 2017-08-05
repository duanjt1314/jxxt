using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Life.Common;
using Life.Model.LifeMan;
using Life.BLL.LifeMan;

namespace Life.Web.Areas.LifeMan.Controllers
{
    /// <summary>
    /// 角色模块对应信息
    /// </summary>
    public class RoleToModuleController : Controller
    {
        /// <summary>
        /// 角色模块对应信息主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询模块信息树信息，加载所有(包括子集)
        /// </summary>
        /// <returns></returns>
        public String Select()
        {
            List<ExtTreeNode<Module>> nodes = LoadModule("0");
            return JsonConvert.JavaScriptSerializer(nodes).Replace("Checked", "checked");
        }

        /// <summary>
        /// 递归加载模块树
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<ExtTreeNode<Module>> LoadModule(String parentId)
        {
            BLModule blm = new BLModule();
            List<Module> modules = blm.Select(new HashTableExp("ParentId",parentId));
            List<ExtTreeNode<Module>> nodes = new List<ExtTreeNode<Module>>();
            foreach (var item in modules)
            {
                var children = blm.Select(new HashTableExp("ParentId", item.ModuleId));
                nodes.Add(new ExtTreeNode<Module>()
                {
                    id = item.ModuleId,
                    text = item.ModuleName,
                    leaf = children.Count == 0,
                    Tobject = item,
                    icon = item.IconUrl,
                    Checked = false,
                    expanded=true,
                    children = LoadModule(item.ModuleId)
                });
            }
            return nodes;
        }

        /// <summary>
        /// 查询角色树信息
        /// </summary>
        /// <returns></returns>
        public String Search()
        {
            List<Roles> roless = new BLRoles().Select(new HashTableExp(), "and Role_Id<>'-1'");
            List<ExtTreeNode<Roles>> nodes = new List<ExtTreeNode<Roles>>();
            foreach (var item in roless)
            {
                nodes.Add(new ExtTreeNode<Roles>()
                {
                    id = item.RoleId,
                    text = item.RoleName,
                    leaf = true,
                    Tobject = item
                });
            }
            return JsonConvert.JavaScriptSerializer(nodes);
        }

        /// <summary>
        /// 根据角色编号查询角色模块信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public String SearchModule(String roleId)
        {
            List<RoleToModule> list = new BLRoleToModule().Select(new HashTableExp("RoleId", roleId));
            return JsonConvert.JavaScriptSerializer(list);
        }

        /// <summary>
        /// 保存角色的模块信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moduleIds"></param>
        /// <returns></returns>
        public String Save(String roleId,String moduleIds)
        {
            String[] ids=new String[]{};
            if(!String.IsNullOrEmpty(moduleIds))
                ids=moduleIds.Split(',');
            if(new BLRoleToModule().Save(roleId, ids)>0)
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "操作成功" });
            else
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "操作失败" });
        }
    }
}
