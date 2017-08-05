using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Life.BLL.LifeMan;
using Life.Common;
using Life.Model.LifeMan;

namespace Life.Web.Areas.LifeMan.Controllers
{
    public class ArtCategoryController : BaseController
    {
        #region 文章类型表管理

        /// <summary>
        /// 文章类型表初始界面
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
            List<ArtCategory> artCategorys = new BLArtCategory().Select(null);
            List<ExtTreeNode<ArtCategory>> nodes = new List<ExtTreeNode<ArtCategory>>();
            foreach (var item in artCategorys)
            {
                nodes.Add(new ExtTreeNode<ArtCategory>()
                {
                    id = item.CatId,
                    text = item.CatName,
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
            List<VArtCategory> artCategorys = new BLArtCategory().Select(limit, start, new HashTableExp(), out total,"");
            return JsonConvert.JavaScriptSerializer(new ExtGridRecord(artCategorys, total));
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="artCategory"></param>
        /// <returns></returns>
        public String Save(ArtCategory artCategory)
        {
            if (string.IsNullOrEmpty(artCategory.CatId))
            {
                return Add(artCategory);
            }
            else
            {
                return Update(artCategory);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="artCategory"></param>
        /// <returns></returns>
        public String Add(ArtCategory artCategory)
        {
            try
            {
                artCategory.CatId = Guid.NewGuid().ToString();
                artCategory.UpdateBy = artCategory.CreateBy = CurrentUser.Id;
                artCategory.UpdateTime = artCategory.CreateTime = DateTime.Now;
                int result = new BLArtCategory().Add(artCategory);
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
        /// <param name="artCategory"></param>
        /// <returns></returns>
        public String Update(ArtCategory artCategory)
        {
            try
            {
                int result = new BLArtCategory().Update(artCategory);
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
            int num = new BLArtCategory().Delete(ids);
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "成功删除" + num + "条数据" });
        }

        /// <summary>
        /// 查询所有的文章类别信息
        /// </summary>
        /// <returns></returns>
        public String SelectAll()
        {
            List<ArtCategory> artCategorys = new BLArtCategory().Select(null);
            return JsonConvert.JavaScriptSerializer(artCategorys);
        }

        #endregion

        /// <summary>
        /// 修改类别的排序
        /// </summary>
        /// <param name="dataStr"></param>
        /// <returns></returns>
        public String UpdateOrder(String dataStr)
        {
            List<ArtCategory> list= JsonConvert.JSONStringToList<ArtCategory>(dataStr);
            int result=new BLArtCategory().Update(list);
            if (result > 0)
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "修改成功" });
            else
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = false, msg = "修改失败" });
        }

    }
}
