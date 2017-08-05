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
    public partial class ArticleShow : System.Web.UI.Page
    {
        #region 属性和字段
        /// <summary>
        /// 文章类别的访问类
        /// </summary>
        BLArtCategory artCategoryService = new BLArtCategory();
        /// <summary>
        /// 文章访问类 
        /// </summary>
        BLArticle articleService = new BLArticle();

        /// <summary>
        /// 表示每页显示多少条数据
        /// </summary>
        public int pageSize = 20;

        /// <summary>
        /// 表示当前第几页
        /// </summary>
        public int PageIndex
        {
            get { return Convert.ToInt32(ViewState["CurrentPage"]); }
            set { ViewState["CurrentPage"] = value; }
        }

        /// <summary>
        /// 总共多少条数据
        /// </summary>
        public int Count
        {
            get { return Convert.ToInt32(ViewState["Count"]); }
            set { ViewState["Count"] = value; }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 绑定文章
        /// </summary>
        private void BindArt()
        {
            HashTableExp hash = new HashTableExp();
            String sqlWhere = String.Empty;
            if (!String.IsNullOrEmpty(Request["cateId"]))
            {
                hash.Add("CateId", Request["cateId"]);
            }
            if (!String.IsNullOrEmpty(this.txtKey.Text))
            {
                sqlWhere += String.Format(" and (Title like '%{0}%' or Content like '%{0}%')",this.txtKey.Text);
            }
            
            int total;
            List<Article> list = articleService.Select(this.pageSize, pageSize * (PageIndex-1), hash, out total,sqlWhere);
            this.rp_art_data.DataSource = list;
            this.rp_art_data.DataBind();

            //绑定分页控件
            this.AspNetPager1.PageSize = this.pageSize;
            this.AspNetPager1.RecordCount = total;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageIndex = 1;
                BindArt();
            }

        }
        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            this.PageIndex = this.AspNetPager1.CurrentPageIndex;
            BindArt();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindArt();
        }
    }
}