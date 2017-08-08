using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Life.Common.Email;
using System.Text;
using Life.Model.LifeMan;
using Life.BLL.LifeMan;
using Life.Common;

namespace Life.Web.BackPwd
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String subKey = Request["subKey"];
                String email = Request["email"];

                HashTableExp hash = new HashTableExp("Id", subKey);
                hash.Add("Email",email);
                TempData data = new BLTempData().Select(hash, String.Format(" and Expires>'{0}'", DateTime.Now.AddSeconds(1).ToString("yyyy-MM-dd HH:mm:ss"))).SingleOrDefault();
                //删除过期数据
                new BLTempData().Delete(null,String.Format( " and Expires<'{0}'",DateTime.Now.AddSeconds(1).ToString("yyyy-MM-dd HH:mm:ss")));

                if (data == null)
                    Response.Redirect("error.htm");
                else
                {
                    new BLTempData().Delete(subKey);
                    Users user = new BLUsers().Select(new HashTableExp("Mail", email)).SingleOrDefault();
                    if (user == null)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), DateTime.Now.ToString(), "alert('邮件地址不存在');location.href='error.htm'", true);
                    }
                    else
                    {
                        this.LitUserName.Text = user.LoginId;
                        Session["TempUser"] = user;
                    }
                }
            }
        }

        protected void btnBackPwd_Click(object sender, EventArgs e)
        {
            Users user = Session["TempUser"] as Users;
            user.LoginPwd = Common.MD5Encry.Encry(this.txtPwd.Text);
            if (new BLUsers().Update(user) > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), DateTime.Now.ToString(), "alert('密码修改成功');location.href='/'", true);
            }
            else
                Response.Redirect("error.htm");
            
        }
    }
}