using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Life.Model.LifeMan;
using Life.BLL.LifeMan;
using Life.Common;
using System.IO;
using System.Globalization;
using System.Collections;

namespace Life.Web.Areas.LifeMan.Controllers
{
    public class ArticleController : BaseController
    {
        #region 文章表管理

        /// <summary>
        /// 文章表初始界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateView()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }
        
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        public String SelectByPage(int limit, int start,String query)
        {
            String sqlWhere = String.Format(" and (Title like '%{0}%' or Content like '%{0}%')", query);
            
            if (!String.IsNullOrEmpty(Request["cateId"]))
            {
                sqlWhere += String.Format(" and cate_Id='{0}'", Request["cateId"]);
            }

            int total;
            List<Article> articles = new BLArticle().Select(limit, start, null, out total, sqlWhere);
            return JsonConvert.JavaScriptSerializer(new ExtGridRecord(articles, total));
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public String Save(Article article)
        {
            if (string.IsNullOrEmpty(article.Id))
            {
                article.CreateBy = article.UpdateBy = CurrentUser.Id;
                article.CreateTime = article.UpdateTime = DateTime.Now;
                return Add(article);
            }
            else
            {
                Article index = new BLArticle().Select(article.Id);
                index.Title = article.Title;
                index.Content = article.Content;
                index.UpdateBy = CurrentUser.Id;
                index.UpdateTime = DateTime.Now;
                return Update(index);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public String Add(Article article)
        {
            try
            {
                article.Id = Guid.NewGuid().ToString();
                int result = new BLArticle().Add(article);
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
        /// <param name="article"></param>
        /// <returns></returns>
        public String Update(Article article)
        {
            try
            {
                int result = new BLArticle().Update(article);
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
            int num = new BLArticle().Delete(ids);
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "成功删除" + num + "条数据" });
        }

        public String Select(String id)
        {
            Article article = new BLArticle().Select(id);
            return JsonConvert.JavaScriptSerializer(article);
        }
        #endregion

    }
}
