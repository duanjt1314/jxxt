using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Life.Model.LifeMan;

/// <summary>
///BasePage 的摘要说明
/// </summary>
public class BasePage : System.Web.UI.Page
{
	public BasePage()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// Session中的当前用户信息
    /// </summary>
    protected Users CurrentUser
    {
        get
        {
            return Session["user"] as Users;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (Session["user"] == null)
        {
            //Response.Write("<script>window.top.location.href='../login.aspx'</script>");
            //Response.Redirect("~/login.htm");
        }
    }

    /// <summary>
    /// 弹出提示信息
    /// </summary>
    /// <param name="msg"></param>
    public void Alert(string msg)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), DateTime.Now.ToString(), "alert('"+msg+"')", true);
    }

    /// <summary>
    /// 注册脚本
    /// </summary>
    /// <param name="script"></param>
    public void RegisterClientScriptBlock(string script)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), DateTime.Now.ToString(), ""+script+"", true);
    }

    /// <summary>
    /// 让下拉框指定的值处于选中状态
    /// </summary>
    /// <param name="value"></param>
    public void SelectDropDownListByValue(DropDownList dropDownList,string value)
    {
        ListItem item = dropDownList.Items.FindByValue(value);
        if (item != null)
        {
            item.Selected = true;
        }
    }


}