using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Life.Model.LifeMan;
using System.Linq.Expressions;
using System.Data;
using Life.Factory.LifeMan;
using Life.Common;

namespace Life.BLL.LifeMan
{
    public class BLBankCard
    {
        FBankCard dLBankCard = FactoryManage.GetBankCard();

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Add(BankCard bankcard)
        {
            return dLBankCard.Add(bankcard);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns>影响的行数</returns>
        public int Add(List<BankCard> list)
        {
            return dLBankCard.Add(list);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Update(BankCard bankcard)
        {
            dLBankCard.Delete(bankcard.Id.ToString());
            return dLBankCard.Add(bankcard);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Delete(string ids)
        {
            return dLBankCard.Delete(ids);
        }

        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            return dLBankCard.Delete();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="filter">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<VBankCard> Select(int pageSize, int start, HashTableExp hash, out int total,String sqlWhere="")
        {
            return dLBankCard.Select(pageSize, start, hash, out total,sqlWhere);

        }
        
        /// <summary>
        /// 按条件查询所有符合条件的数据(不分页)
        /// </summary>
        /// <param name="filter">筛选条件</param>
        /// <returns>信息集合</returns>
        public List<BankCard> Select(HashTableExp hash,String sqlWhere="")
        {
            return dLBankCard.Select(hash,sqlWhere);
        }


        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>数据集合</returns>
        public BankCard Select(string id)
        {
            return dLBankCard.Select(id);
        }

        /// <summary>
        /// 根据月份汇总银行卡信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetCollectionByMonth()
        {
            return dLBankCard.GetCollectionByMonth();
        }

        /// <summary>
        /// 自动计算所有数据的余额
        /// </summary>
        /// <returns></returns>
        public bool CalcAllBalance()
        {
            return dLBankCard.CalcAllBalance();
        }

        /// <summary>
        /// 将dataTable保存到数据库
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public String SaveData(DataTable dt, String userId)
        {
            List<BankCard> list = new List<BankCard>();
            String msg = "";
            foreach (DataRow row in dt.Rows)
            {
                BankCard index = new BankCard();
                String type = row["操作类型"].ToString();
                //判断操作类型是否存在
                HashTableExp hash = new HashTableExp("Name", type);
                hash.Add("ParentId", "1000200000");
                List<Diction> dictions = new BLDiction().Select(hash);
                if (dictions.Count <= 0)
                {
                    msg += "<br/>" + type + "不存在";
                    continue;
                }
                else
                {
                    index.SaveType = dictions[0].Id;
                }

                //判断银行卡是否正确
                type = row["银行卡名称"].ToString();
                hash.Clear();
                hash.Add("Name",type);
                hash.Add("ParentId", "1000100000");
                dictions = new BLDiction().Select(hash);
                if (dictions.Count <= 0)
                {
                    msg += "<br/>" + type + "不存在";
                    continue;
                }
                else
                {
                    index.BankType = dictions[0].Id;
                }

                DateTime time;
                //判断时间是否正确
                if (!DateTime.TryParse(row["时间"].ToString(), out time))
                {
                    msg += "<br/>" + row["时间"] + "不是时间类型";
                    continue;
                }
                else
                    index.Time = time;

                double price;

                //判断金额是否正确
                if (!double.TryParse(row["金额"].ToString(), out price))
                {
                    msg += "<br/>" + row["金额"] + "不是数字类型";
                    continue;
                }
                else
                    index.Price = price;

                index.Id = Guid.NewGuid().ToString();
                index.Note = row["备注"].ToString();
                index.CreateBy = index.UpdateBy = userId;
                index.CreateTime = index.UpdateTime = DateTime.Now;
                list.Add(index);
            }

            int result = dLBankCard.Add(list);
            if (result > 0)
                return "成功保存" + result + "条数据." + msg;
            else
                return msg;
        }


        /// <summary>
        /// 查询所有银行卡的最后一条数据 time格式：2015-01-02
        /// </summary>
        /// <returns></returns>
        public List<VBankCard> SelectCalc(String time)
        {
            return dLBankCard.SelectCalc(time);
        }
    }
}


