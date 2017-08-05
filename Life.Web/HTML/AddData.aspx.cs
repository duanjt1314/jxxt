using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Life.Model.LifeMan;

namespace Life.Web.HTML
{
    public partial class AddData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //用户
        protected void Button1_Click(object sender, EventArgs e)
        {
            new Life.SQLiteDAL.LifeMan.DLUsers().Delete();
            List<Users> list= new Life.DAL.LifeMan.DLUsers().Select(null, null);
            int result= new Life.SQLiteDAL.LifeMan.DLUsers().Add(list);
            Response.Write("成功导入"+result+"条数据");
        }

        //文章
        protected void Button3_Click(object sender, EventArgs e)
        {
            new Life.SQLiteDAL.LifeMan.DLArticle().Delete();
            List<Article> list = new Life.DAL.LifeMan.DLArticle().Select(null, null);
            int result = new Life.SQLiteDAL.LifeMan.DLArticle().Add(list);
            Response.Write("成功导入" + result + "条数据");
        }

        //文章类型
        protected void Button2_Click(object sender, EventArgs e)
        {
            new Life.SQLiteDAL.LifeMan.DLArtCategory().Delete();
            List<ArtCategory> list = new Life.DAL.LifeMan.DLArtCategory().Select(null, null);
            int result = new Life.SQLiteDAL.LifeMan.DLArtCategory().Add(list);
            Response.Write("成功导入" + result + "条数据");
        }

        //字典
        protected void Button4_Click(object sender, EventArgs e)
        {
            new Life.SQLiteDAL.LifeMan.DLDiction().Delete();
            List<Diction> list = new Life.DAL.LifeMan.DLDiction().Select(null, null);
            int result = new Life.SQLiteDAL.LifeMan.DLDiction().Add(list);
            Response.Write("成功导入" + result + "条数据");
        }

        //生活费
        protected void Button5_Click(object sender, EventArgs e)
        {
            new Life.SQLiteDAL.LifeMan.DLLifingCost().Delete();
            List<LifingCost> list = new Life.DAL.LifeMan.DLLifingCost().Select(null, null);
            int result = new Life.SQLiteDAL.LifeMan.DLLifingCost().Add(list);
            Response.Write("成功导入" + result + "条数据");
        }

        //银行卡
        protected void Button6_Click(object sender, EventArgs e)
        {
            new Life.SQLiteDAL.LifeMan.DLBankCard().Delete();
            List<BankCard> list = new Life.DAL.LifeMan.DLBankCard().Select(null, null);
            int result = new Life.SQLiteDAL.LifeMan.DLBankCard().Add(list);
            Response.Write("成功导入" + result + "条数据");
        }

        //纯收入
        protected void Button7_Click(object sender, EventArgs e)
        {
            new Life.SQLiteDAL.LifeMan.DLIncome().Delete();
            List<Income> list = new Life.DAL.LifeMan.DLIncome().Select(null, null);
            int result = new Life.SQLiteDAL.LifeMan.DLIncome().Add(list);
            Response.Write("成功导入" + result + "条数据");
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            new Life.SQLiteDAL.LifeMan.DLArticle().Delete();
            new Life.SQLiteDAL.LifeMan.DLIncome().Delete();
            new Life.SQLiteDAL.LifeMan.DLLifingCost().Delete();
            new Life.SQLiteDAL.LifeMan.DLBankCard().Delete();
            Response.Write("成功清空");
        }
    }
}