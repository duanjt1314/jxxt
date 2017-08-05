using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Life.Model.LifeMan;
using Life.BLL.LifeMan;
using Life.Common;

namespace Life.Web.Areas.LifeMan.Controllers
{
    public class DictionController : BaseController
    {
        #region 字典表管理

        /// <summary>
        /// 字典表初始界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询顶级（父级编号为0）的模块
        /// </summary>
        /// <returns></returns>
        public String Select()
        {
            List<Diction> dictions = new BLDiction().Select(new HashTableExp("ParentId", "0"));
            List<ExtTreeNode<Diction>> nodes = new List<ExtTreeNode<Diction>>();
            foreach (var item in dictions)
            {
                nodes.Add(new ExtTreeNode<Diction>()
                {
                    id = item.Id.ToString(),
                    text = item.Name,
                    leaf = true,
                    Tobject = item
                });
            }
            return JsonConvert.JavaScriptSerializer(nodes);
        }

        /// <summary>
        /// 根据父级编号分页查询
        /// </summary>
        /// <returns></returns>
        public String SelectByPage(int limit, int start, decimal parentId)
        {
            int total;
            List<Diction> dictions = new BLDiction().Select(limit, start, new HashTableExp("ParentId", parentId.GetString()), out total);
            return JsonConvert.JavaScriptSerializer(new ExtGridRecord(dictions, total));
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="diction"></param>
        /// <returns></returns>
        public String Save(Diction diction)
        {
            Users user = Session["user"] as Users;
            if (diction.Id == 0)
            {
                diction.CreateTime = diction.UpdateTime = DateTime.Now;
                diction.CreateBy = diction.UpdateBy = user.Name;
                return Add(diction);
            }
            else
            {
                diction.UpdateTime = DateTime.Now;
                diction.UpdateBy = user.Name;
                return Update(diction);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="diction"></param>
        /// <returns></returns>
        public String Add(Diction diction)
        {
            try
            {
                decimal parentId = Request["parentId"].GetDecimal();
                List<Diction> list = new BLDiction().Select(new HashTableExp("ParentId", parentId.GetString()));
                if (list.Count > 0)
                    diction.Id = list.Max(f => f.Id) + 1;
                else
                    diction.Id = parentId + 1;

                int result = new BLDiction().Add(diction);
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
        /// <param name="diction"></param>
        /// <returns></returns>
        public String Update(Diction diction)
        {
            try
            {
                int result = new BLDiction().Update(diction);
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
            int num = new BLDiction().Delete(ids);
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "成功删除" + num + "条数据" });
        }

        #endregion

        #region private function

        #endregion

        #region 不需要权限控制的公共方法

        /// <summary>
        /// 根据父级编号查询所有的数据信息，不分页
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public String SelectByParentId(decimal parentId)
        {
            List<Diction> list = new BLDiction().Select(new HashTableExp("ParentId", parentId.GetString()));
            return JsonConvert.JavaScriptSerializer(list);
        }

        #endregion



    }
}
