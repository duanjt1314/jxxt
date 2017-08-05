using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Life.BLL.LifeMan;
using Life.Model.LifeMan;
using Life.Common;

namespace Life.Web.Areas.LifeMan.Controllers
{
    public class PurchaseController : BaseController
    {
        #region 扫帚采购表管理

        /// <summary>
        /// 扫帚采购表初始界面
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
            List<Purchase> purchases = new BLPurchase().Select(limit, start,null, out total,"");
            return JsonConvert.JavaScriptSerializer(new ExtGridRecord(purchases, total));
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="purchase"></param>
        /// <returns></returns>
        public String Save(Purchase purchase)
        {
            if (string.IsNullOrEmpty(purchase.Id))
            {
                return Add(purchase);
            }
            else
            {
                return Update(purchase);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="purchase"></param>
        /// <returns></returns>
        public String Add(Purchase purchase)
        {
            try
            {
                purchase.CreateTime = DateTime.Now;
                purchase.CreateBy = CurrentUser.Id;
                purchase.Id = Guid.NewGuid().ToString();
                int result = new BLPurchase().Add(purchase);
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
        /// <param name="purchase"></param>
        /// <returns></returns>
        public String Update(Purchase purchase)
        {
            try
            {
                purchase.UpdateBy = CurrentUser.Id;
                purchase.UpdateTime = DateTime.Now;
                int result = new BLPurchase().Update(purchase);
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
            int num = new BLPurchase().Delete(ids);
            return JsonConvert.JavaScriptSerializer(new ExtResult() { success = true, msg = "成功删除" + num + "条数据" });
        }

        #endregion


    }
}
