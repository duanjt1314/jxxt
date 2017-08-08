using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Life.BLL;
using Life.Common;
using Life.Model;
using Life.Model.LifeMan;
using Life.BLL.LifeMan;

namespace Life.Web.Phone
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(Request.Browser.Browser+" "+Request.Browser.Version);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            String loginId = this.txtLoginId.Text;
            String pwd = this.txtPwd.Text;

            pwd = MD5Encry.Encry(pwd);
            Users user = new BLUsers().Login(loginId, pwd);
            if (user!=null)
            {
                Session["user"] = user;
                //成功
                Response.Redirect("Menu.aspx");
            }
            else
            {
                //失败
                Alert("用户名或密码错误");
            }
        }
    }
}