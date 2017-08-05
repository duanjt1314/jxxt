using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Life.BLL.LifeMan;
using Life.Model.LifeMan;
using Life.Common;

namespace Life.Web.ArticleManage
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        /// <summary>
        /// 文章类别的访问类
        /// </summary>
        BLArtCategory artCategoryService = new BLArtCategory();

        /// <summary>
        /// 文章的访问类
        /// </summary>
        BLArticle articleService = new BLArticle();

        #region 方法

        /// <summary>
        /// 绑定类别
        /// </summary>
        private void BindCategory()
        {
            List<ArtCategory> list = artCategoryService.Select(null);
            this.rp_cate_data.DataSource = list;
            this.rp_cate_data.DataBind();
        }

        /// <summary>
        /// 根据类别编号查询文章数量
        /// </summary>
        /// <param name="cateId">类别编号</param>
        /// <returns>文章数量</returns>
        public string getArtNum(string cateId)
        {
            return articleService.Select(new HashTableExp("CateId",cateId)).Count.ToString();
        }

        #endregion

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategory();
            }
        }

    }
}