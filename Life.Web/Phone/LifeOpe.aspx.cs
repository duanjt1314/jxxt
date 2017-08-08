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
    public partial class LifeOpe : BasePage
    {
        #region 属性和字段
        BLDiction blDiction = new BLDiction();
        BLLifingCost bLLifingCost = new BLLifingCost();

        #endregion

        #region 方法

        private void BindList()
        {
            //绑定生活费消费类型下拉框
            List<Diction> code = blDiction.Select(new HashTableExp("ParentId", "1000300000")).ToList();
            this.ddlCostType.DataValueField = "Id";
            this.ddlCostType.DataTextField = "Name";
            this.ddlCostType.DataSource = code;
            this.ddlCostType.DataBind();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();
                if (Request["id"] != null)
                {
                    //表示修改
                    LifingCost life = bLLifingCost.Select(Request["id"]);
                    this.txtReason.Text = life.Reason;
                    this.txtPrice.Text = life.Price.ToString();
                    this.txtDate.Text = life.Time.ToString();
                    this.txtNotes.Text = life.Notes;
                    SelectDropDownListByValue(this.ddlCostType, life.CostTypeId+"");
                    
                    this.labTitle.Text = "修改生活费信息";
                }
                this.txtReason.Focus();
            }
        }
    }
}