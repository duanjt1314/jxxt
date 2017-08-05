using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Life.Model.LifeMan;
using Life.BLL.LifeMan;
using Life.Common;

namespace Life.Web.Phone
{
    public partial class LifeList : BasePage
    {
        #region 属性和字段
        /// <summary>
        /// 生活费
        /// </summary>
        BLLifingCost bllLifingCost = new BLLifingCost();
        

        /// <summary>
        /// 表示每页显示多少条数据
        /// </summary>
        public int pageSize = 10;

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
        private void BindLife()
        {
            int total;
            List<VLifingCost> list = new BLLifingCost().Select(pageSize, (PageIndex-1)*pageSize, null, out total,"");
            this.rp_data.DataSource = list;
            this.rp_data.DataBind();

            //绑定分页控件
            this.Count = total;
            BindPage();
        }

        /// <summary>
        /// 控制分页控件
        /// </summary>
        private void BindPage()
        {
            if (this.Count == 0)
            {
                this.divPage.Visible = false;
                return;
            }

            //计算总页数
            int pageCount = this.Count % this.pageSize == 0 ? this.Count / this.pageSize : this.Count / this.pageSize + 1;

            this.btnPrex.Enabled = true;
            this.btnNext.Enabled = true;
            //设置上一页
            if (this.PageIndex == 1)
                this.btnPrex.Enabled = false;

            //设置下一页
            if (this.PageIndex == pageCount)
                this.btnNext.Enabled = false;

            //下拉页赋值
            this.ddp_page.Items.Clear();
            for (int i = 0; i < pageCount; i++)
            {
                this.ddp_page.Items.Add(new ListItem((i+1)+"/"+pageCount,i+1+""));
            }

            SelectDropDownListByValue(this.ddp_page, this.PageIndex + "");
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageIndex = 1;
                BindLife();
            }
        }

        #region 分页事件
        protected void btnPrex_Click(object sender, EventArgs e)
        {
            this.PageIndex--;
            BindLife();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            this.PageIndex++;
            BindLife();
        }

        protected void ddp_page_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PageIndex=this.ddp_page.SelectedValue.GetInt32();
            BindLife();
        }
        #endregion
    }
}