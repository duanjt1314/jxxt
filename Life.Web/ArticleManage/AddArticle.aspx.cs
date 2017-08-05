using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Life.Model.LifeMan;
using Life.BLL.LifeMan;

namespace Life.Web.ArticleManage
{
    public partial class AddArticle : BasePage
    {
        #region 方法
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        private void BindList()
        {
            List<ArtCategory> list = new BLArtCategory().Select(null);
            this.ddlCategory.DataSource = list;
            this.ddlCategory.DataTextField = "CatName";
            this.ddlCategory.DataValueField = "catId";
            this.ddlCategory.DataBind();
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();
                if (Request["id"] != null)
                {
                    //修改
                    Article index = new BLArticle().Select(Request["id"]);
                    if (index != null)
                    {
                        this.txtTitle.Text = index.Title;
                        this.txtContent.Text = index.Content;
                        SelectDropDownListByValue(this.ddlCategory, index.CateId);
                        this.btnAdd.Text = "确认修改";
                    }
                }
            }
        }
        /// <summary>
        /// 新增文章
        /// </summary>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Article index = new Article();
            index.Id = Guid.NewGuid().ToString();
            index.Title = this.txtTitle.Text;
            index.Content = this.txtContent.Text;
            index.CateId = this.ddlCategory.SelectedValue;
            if (Request["id"] == null)
            {
                index.CreateBy=index.UpdateBy = CurrentUser.Id;
                index.CreateTime=index.UpdateTime = DateTime.Now;
                if (new BLArticle().Add(index) > 0)
                {
                    Response.Redirect("ArticleDetail.aspx?artId=" + index.Id);
                }
                else
                {
                    Alert("新增失败");
                }
            }
            else
            {
                index.UpdateBy = CurrentUser.Id;
                index.UpdateTime = DateTime.Now;
                index.Id = Request["id"];
                Article article = new BLArticle().Select(index.Id);
                index.CreateTime = article.CreateTime;
                index.CreateBy = article.CreateBy;

                if (new BLArticle().Update(index) > 0)
                {
                    Response.Redirect("ArticleDetail.aspx?artId=" + index.Id);
                }
                else
                {
                    Alert("修改失败");
                }
            }
        }

    }
}