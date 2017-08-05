using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Life.BLL.LifeMan;
using Life.Model.LifeMan;

namespace Life.Web.ArticleManage
{
    public partial class WebForm1 : BasePage
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
        #endregion

        #region 方法
        /// <summary>
        /// 绑定文章
        /// </summary>
        private void BindArt(string artId)
        {
            Article art = articleService.Select(artId);
            if (art != null)
            {
                this.labTitle.Text=this.lTitle.Text = art.Title;
                this.labContent.Text = art.Content;
                this.hlEdit.NavigateUrl = "AddArticle.aspx?id=" + artId;
                this.labCreateTime.Text = art.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }

        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["artId"] != null)
                {
                    BindArt(Request["artId"]);
                }
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbDelete_Click(object sender, EventArgs e)
        {
            if (Request["artId"] != null)
            {
                if (articleService.Delete(Request["artId"]) > 0)
                {
                    this.RegisterClientScriptBlock("alert('删除成功');location.href='ArticleShow.aspx'");
                }
                else
                {
                    this.Alert("删除失败.");
                }
            }
            else
            {
                this.Alert("adfd");
            }
        }
    }
}