using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Life.Common;
using Life.BLL.LifeMan;
using Life.Model.LifeMan;

namespace Life.Web.Areas.LifeMan.Controllers
{
    public class BankTypeController : BaseController
    {
        #region 基本操作

        /// <summary>
        /// 银行卡类型表初始界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        public String SelectByPage(int limit, int start)
        {
            int total;
            List<BankType> banktypes = new BLBankType().Select(limit, start, null, out total,null);
            return JsonConvert.JavaScriptSerializer(new ExtGridRecord(banktypes, total));
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="banktype"></param>
        /// <returns></returns>
        public String Save(BankType banktype)
        {
            if (string.IsNullOrEmpty(banktype.Id))
            {
                return Add(banktype);
            }
            else
            {
                return Update(banktype);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="banktype"></param>
        /// <returns></returns>
        public String Add(BankType banktype)
        {
            try
            {
                banktype.Id = Guid.NewGuid().ToString();
                int result = new BLBankType().Add(banktype);
                if (result > 0)
                    return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "新增成功" });
                else
                    return JsonConvert.JavaScriptSerializer(new ExtResult() { success = false, msg = "新增失败" });
            }
            catch (Exception ex)
            {
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = false, msg = "新增失败,失败原因:" + ex.Message });
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="banktype"></param>
        /// <returns></returns>
        public String Update(BankType banktype)
        {
            try
            {
                int result = new BLBankType().Update(banktype);
                if (result > 0)
                    return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "修改成功" });
                else
                    return JsonConvert.JavaScriptSerializer(new ExtResult() { success = false, msg = "修改失败" });
            }
            catch (Exception ex)
            {
                return JsonConvert.JavaScriptSerializer(new ExtResult() { success = false, msg = "修改失败,失败原因:" + ex.Message });
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">Id的集合，如1，2，3</param>
        /// <returns></returns>
        public String Delete(String ids)
        {
            int num = new BLBankType().Delete(ids);
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "成功删除" + num + "条数据" });
        }
        #endregion
        
    }
}
