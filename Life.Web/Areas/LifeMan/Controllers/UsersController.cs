using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Life.Common;
using Life.BLL.LifeMan;
using Life.Model.LifeMan;
using System.Data;

namespace Life.Web.Areas.LifeMan.Controllers
{
    public class UsersController : BaseController
    {
        //
        // GET: /User/
        #region 用户表管理

        /// <summary>
        /// 用户表初始界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
        public String Select()
        {
            List<Users> userss = new BLUsers().Select();
            List<ExtTreeNode<Users>> nodes = new List<ExtTreeNode<Users>>();
            foreach (var item in userss)
            {
                nodes.Add(new ExtTreeNode<Users>()
                {
                    id = item.Id,
                    text = item.Name,
                    leaf = true,
                    Tobject = item
                });
            }
            return JsonConvert.JavaScriptSerializer(nodes);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        public String SelectByPage(int limit, int start)
        {
            int total;
            List<Users> userss = new BLUsers().Select(limit, start, new HashTableExp(), out total," and id<>'-1'");
            return JsonConvert.JavaScriptSerializer(new ExtGridRecord(userss, total));
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public String Save(Users users)
        {
            if (string.IsNullOrEmpty(users.Id))
            {
                return Add(users);
            }
            else
            {
                return Update(users);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public String Add(Users users)
        {
            try
            {
                users.Id = Guid.NewGuid().ToString();
                users.LoginPwd = MD5Encry.Encry("123456");//默认密码123456
                int result = new BLUsers().Add(users);
                if (result > 0)
                {
                    var roles =new String []{};                    
                    if (!String.IsNullOrEmpty(Request["Roles"]))
                    {
                        roles=Request["Roles"].Split(',');
                    }
                    new BLUserToRole().Save(users.Id,roles);
                    return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "新增成功,初始密码:123456" });
                }
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
        /// <param name="users"></param>
        /// <returns></returns>
        public String Update(Users users)
        {
            try
            {
                int result = new BLUsers().Update(users);
                if (result > 0)
                {
                    var roles = new String[] { };
                    if (!String.IsNullOrEmpty(Request["Roles"]))
                    {
                        roles = Request["Roles"].Split(',');
                    }
                    new BLUserToRole().Save(users.Id, roles);
                    return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "修改成功" });
                }
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
            int num = new BLUsers().Delete(ids);
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "成功删除" + num + "条数据" });
        }

        #endregion

        /// <summary>
        /// 获得所有角色信息
        /// </summary>
        /// <returns></returns>
        public String GetAllRoles()
        {
            List<Roles> roles = new BLRoles().Select(new HashTableExp(),"and Role_Id<>'-1'");
            return JsonConvert.JavaScriptSerializer(roles);
        }

        /// <summary>
        /// 根据用户编号查询用户角色信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public String GetUserRole(String userId)
        {
            List<UserToRole> utr = new BLUserToRole().Select(new HashTableExp("UserId",userId));
            return JsonConvert.JavaScriptSerializer(utr);
        }

        public ActionResult Login()
        {
            return View();
        }

        //获得所有用户信息
        public String GetAll()
        {
            List<Users> userss = new BLUsers().Select();
            userss = userss.Where(f => f.Id != "-1").ToList();
            return JsonConvert.JavaScriptSerializer(userss);
        }

    }
}
