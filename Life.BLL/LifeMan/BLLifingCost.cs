using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Life.Model.LifeMan;
using System.Linq.Expressions;
using System.Data;
using Life.Common;
using Life.Factory.LifeMan;

namespace Life.BLL.LifeMan
{
    public class BLLifingCost
    {
        FLifingCost dLLifingCost = FactoryManage.GetLifingCost();

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Add(LifingCost lifingCost)
        {
            return dLLifingCost.Add(lifingCost);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns>影响的行数</returns>
        public int Add(List<LifingCost> list)
        {
            return dLLifingCost.Add(list);
        }

        public bool Save(List<LifingCost> list)
        {
            return dLLifingCost.Save(list);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Update(LifingCost lifingCost)
        {
            return dLLifingCost.Update(lifingCost);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Delete(string ids)
        {
            return dLLifingCost.Delete(ids);
        }

        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            return dLLifingCost.Delete();
        }
        
        /// <summary>
        /// 按条件查询所有符合条件的数据(不分页)
        /// </summary>
        /// <param name="filter">筛选条件</param>
        /// <returns>信息集合</returns>
        public List<LifingCost> Select(HashTableExp hash,String sqlWhere="")
        {
            return dLLifingCost.Select(hash,sqlWhere);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="filter">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<VLifingCost> Select(int pageSize, int start, HashTableExp hash, out int total, String sqlWhere)
        {
            return dLLifingCost.Select(pageSize, start, hash, out total, sqlWhere);
        }

        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>数据集合</returns>
        public LifingCost Select(string id)
        {
            return dLLifingCost.Select(id);
        }

        /// <summary>
        /// 汇总月份生活费消费信息
        /// </summary>
        /// <param name="beginTime">开始时间,格式2014-11-01</param>
        /// <param name="endTime">结束时间,格式2014-11-11</param>
        /// <param name="isMark">特殊标识</param>
        /// <param name="family">家庭内消费</param>
        /// <param name="costType">消费类型</param>
        /// <returns></returns>
        public List<CostMonthCol> GetCollectionByMonth(String beginTime, String endTime, Boolean? isMark, Boolean? family, String costTypeIds,String userId)
        {
            return dLLifingCost.GetCollectionByMonth(beginTime,endTime,isMark,family,costTypeIds,userId);
        }

        /// <summary>
        /// 根据月份汇总生活费
        /// </summary>
        /// <param name="time">时间,如:'2014-01','2014-12'</param>
        /// <param name="beginTime">开始统计的时间</param>
        /// <param name="endTime">结束统计的时间</param>
        /// <param name="isMark">特殊标识</param>
        /// <param name="family">家庭内收支</param>
        /// <param name="costTypeId">消费类型编号</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public DataTable GetCollectionByDay(String time, String beginTime, String endTime, Boolean? isMark, Boolean? family, String costTypeIds, String userId)
        {
            return dLLifingCost.GetCollectionByDay(time,beginTime,endTime,isMark,family,costTypeIds,userId);
        }

        /// <summary>
        /// 根据月份汇总生活费类型
        /// </summary>
        /// <param name="time">时间，如:2013-01</param>
        /// <returns></returns>
        public DataTable GetCollectionByType(String time,String sqlWhere)
        {
            return dLLifingCost.GetCollectionByType(time,sqlWhere);
        }

        /// <summary>
        /// 汇总生活费类型
        /// </summary>
        /// <param name="time">时间,如:'2014-01','2014-12'</param>
        /// <param name="beginTime">开始统计的时间</param>
        /// <param name="endTime">结束统计的时间</param>
        /// <param name="isMark">特殊标识</param>
        /// <param name="family">家庭内收支</param>
        /// <param name="costTypeId">消费类型编号</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public DataTable GetCollectionType(string time, string beginTime, string endTime, bool? isMark, bool? family, string costTypeIds, string userId)
        {
            return dLLifingCost.GetCollectionType(time, beginTime, endTime, isMark, family, costTypeIds, userId);
        }
        
        /// <summary>
        /// 将dataTable保存到数据库
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public String SaveData(DataTable dt,String userId)
        {
            List<LifingCost> list = new List<LifingCost>();
            String msg = "";
            foreach (DataRow row in dt.Rows)
            {
                LifingCost index = new LifingCost();
                String type = row["消费类型"].ToString();
                //判断类型是否存在
                HashTableExp hash = new HashTableExp("Name", type);
                hash.Add("ParentId", "1000300000");
                List<Diction> dictions = new BLDiction().Select(hash);
                if (dictions.Count <= 0)
                {
                    msg += "<br/>" + type + "不存在";
                    continue;
                }

                DateTime time;
                //判断时间是否正确
                if (!DateTime.TryParse(row["消费时间"].ToString(), out time))
                {
                    msg += "<br/>" + row["消费时间"] + "不是时间类型";
                    continue;
                }
                else
                    index.Time = time;

                double price;
                     
                //判断金额是否正确
                if (!double.TryParse(row["消费金额"].ToString(), out price))
                {
                    msg += "<br/>" + row["消费金额"] + "不是数字类型";
                    continue;
                }
                else
                    index.Price = price;

                index.Id = Guid.NewGuid().ToString();
                index.Reason = row["消费名称"].ToString();
                index.CostTypeId = dictions[0].Id;
                index.Notes = row["备注"].ToString();
                index.CreateBy = userId;
                index.CreateTime = DateTime.Now;
                list.Add(index);
            }

            int result=dLLifingCost.Add(list);
            if (result > 0)
                return "成功保存" + result + "条数据." + msg;
            else
                return msg;
        }

        /// <summary>
        /// 查询生活费的所有消费名称
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<LifingCost> GetReasons(String key)
        {
            return dLLifingCost.GetReasons(key);
        }

        /// <summary>
        /// 根据月份汇总生活费消费信息-图形汇总
        /// </summary>
        /// <returns></returns>
        public DataTable GetCollection(String sqlWhere, String beginTime, String endTime)
        {
            return dLLifingCost.GetCollection(sqlWhere, beginTime, endTime);
        }

        /// <summary>
        /// 根据条件查询消费总金额
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public double GetTotalPrice(HashTableExp hash, string sqlWhere)
        {
            return dLLifingCost.GetTotalPrice(hash, sqlWhere);
        }

        /// <summary>
        /// 批量修改自定义分组信息
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int ModifyCusGroup(String ids, String value)
        {
            return dLLifingCost.ModifyCusGroup(ids, value);
        }

        /// <summary>
        /// 查询所有的自定义分组名称
        /// </summary>
        /// <returns></returns>  
        public List<string> GetAllCusGroup()
        {
            return dLLifingCost.GetAllCusGroup();
        }

        /// <summary>
        /// 返回所有的消费名称,分组后
        /// </summary>
        /// <returns></returns>
        public List<String> GetAllReasons()
        {
            return dLLifingCost.GetAllReasons();
        }
    }
}


