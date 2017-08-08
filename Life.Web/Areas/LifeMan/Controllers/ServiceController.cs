using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Life.Model.LifeMan;
using Life.Common;
using Life.BLL.LifeMan;

namespace Life.Web.Areas.LifeMan.Controllers
{
    /// <summary>
    /// Android手机访问接口
    /// </summary>
    public class ServiceController : Controller
    {
        #region 查询
        /// <summary>
        /// 获得所有用户
        /// </summary>
        /// <returns></returns>
        public String GetAllUsers()
        {
            List<Users> list = new BLUsers().Select();
            String result = JsonConvert.JavaScriptSerializer(new ExtResult()
            {
                data = list,
                success = true
            });
            return result;
        }

        /// <summary>
        /// 查询所有字典信息
        /// </summary>
        /// <returns></returns>
        public String GetDictions()
        {
            List<Diction> list = new BLDiction().Select(null, null);
            String result = JsonConvert.JavaScriptSerializer(new ExtResult()
            {
                data = list,
                success = true
            });
            return result;
        }

        /// <summary>
        /// 查询所有消费名称
        /// </summary>
        /// <returns></returns>
        public String GetLifeNames()
        {
            List<String> list = new BLLifingCost().GetAllReasons();
            String result = JsonConvert.JavaScriptSerializer(new ExtResult()
            {
                data = list,
                success = true
            });
            return result;
        }

        /// <summary>
        /// 分页查询生活费信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public String GetLifingCost(int pageSize, int start,String createBy)
        {
            try
            {
                int total;
                var list = new BLLifingCost().Select(pageSize, start, new HashTableExp("CreateBy",createBy), out total, string.Empty);
                String result = JsonConvert.GetJsonString(new ExtResult()
                {
                    data = new ExtGridRecord(list, total),
                    success = true
                });
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("获取生活费信息错误pageSize=" + pageSize + ",start=" + start, ex);
                return JsonConvert.GetJsonString(new ExtResult()
                {
                    msg = ex.Message,
                    success = false,
                    data = null
                });
            }
        }

        /// <summary>
        /// 分页查询存收入信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public String GetIncome(int pageSize, int start, String createBy)
        {
            try
            {
                int total;
                var list = new BLIncome().Select(pageSize, start, new HashTableExp("CreateBy", createBy), out total, string.Empty);
                String result = JsonConvert.GetJsonString(new ExtResult()
                {
                    data = new ExtGridRecord(list, total),
                    success = true
                });
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("获取纯收入信息错误pageSize=" + pageSize + ",start=" + start, ex);
                return JsonConvert.GetJsonString(new ExtResult()
                {
                    msg = ex.Message,
                    success = false,
                    data = null
                });
            }
        }

        /// <summary>
        /// 分页查询银行卡信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="start"></param>
        /// <param name="createBy"></param>
        /// <returns></returns>
        public String GetBankCard(int pageSize, int start, String createBy)
        {
            try
            {
                int total;
                var list = new BLBankCard().Select(pageSize, start, new HashTableExp("CreateBy", createBy), out total, string.Empty);
                String result = JsonConvert.GetJsonString(new ExtResult()
                {
                    data = new ExtGridRecord(list, total),
                    success = true
                });
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("获取银行卡信息错误pageSize=" + pageSize + ",start=" + start, ex);
                return JsonConvert.GetJsonString(new ExtResult()
                {
                    msg = ex.Message,
                    success = false,
                    data = null
                });
            }
        }
        #endregion

        #region 上传
        /// <summary>
        /// 将JSON数据上传到服务器并保存
        /// </summary>
        /// <param name="lifings"></param>
        /// <returns></returns>
        public String InsertLifingCost(String lifings)
        {
            try
            {
                LogHelper.Log.Info("收到请求:" + lifings);
                List<LifingCost> list = JsonConvert.JSONStringToList<LifingCost>(lifings);
                LogHelper.Log.Info("转换后的数据:" + list.GetJsonString());
                new BLLifingCost().Save(list);
                return JsonConvert.JavaScriptSerializer(new ExtResult()
                {
                    msg = "成功保存" + list.Count + "条数据",
                    success = true,
                    data = null
                });
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("上传生活费信息错误", ex);
                return JsonConvert.JavaScriptSerializer(new ExtResult()
                {
                    msg = ex.Message,
                    success = false,
                    data = null
                });
            }
        }
        /// <summary>
        /// 将手机端的银行卡信息上传并保存到服务器
        /// </summary>
        /// <param name="bank"></param>
        /// <returns></returns>
        public String InsertBankCard(String bank)
        {
            try
            {
                List<BankCard> list = JsonConvert.JSONStringToList<BankCard>(bank);
                int result = new BLBankCard().Add(list);
                return JsonConvert.JavaScriptSerializer(new ExtResult()
                {
                    msg = "成功保存" + result + "条数据",
                    success = true,
                    data = result
                });
            }
            catch (Exception ex)
            {
                return JsonConvert.JavaScriptSerializer(new ExtResult()
                {
                    msg = ex.Message,
                    success = false,
                    data = null
                });
            }
        }

        /// <summary>
        /// 将手机端的纯收入信息上传并保存到服务器
        /// </summary>
        /// <param name="bank"></param>
        /// <returns></returns>
        public String InsertIncome(String income)
        {
            try
            {
                List<Income> list = JsonConvert.JSONStringToList<Income>(income);
                bool result = new BLIncome().Save(list);
                return JsonConvert.JavaScriptSerializer(new ExtResult()
                {
                    msg = "成功保存" + list.Count + "条数据",
                    success = true,
                    data = result
                });
            }
            catch (Exception ex)
            {
                return JsonConvert.JavaScriptSerializer(new ExtResult()
                {
                    msg = ex.Message,
                    success = false,
                    data = null
                });
            }
        }

        #endregion
    }
}
