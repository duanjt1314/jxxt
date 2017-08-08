using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Life.Web.HTML
{
    public partial class SearchImg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request["ImgUrl"]))
                    this.img.ImageUrl = Request["ImgUrl"];
            }
        }
    }
}