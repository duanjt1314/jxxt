using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Life.Model.LifeMan;
using System.Linq.Expressions;
using Life.Common;
using System.Data;

namespace Life.Factory.LifeMan
{
    public interface FLifingCost
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns>影响的行数</returns>
        int Add(LifingCost index);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns>影响的行数</returns>
        int Add(List<LifingCost> list);

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>影响的行数</returns>
        int Update(LifingCost users);
        
        /// <summary>
        ///  批量修改
        /// </summary>
        /// <returns>影响的行数</returns>
        int Update(List<LifingCost> list);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">编号的集合，如：1,2,3...</param>
        /// <returns>影响的行数</returns>
        int Delete(string ids);

        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns></returns>
        int Delete();
        
        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <returns></returns>
        int Delete(HashTableExp hash,String sqlWhere);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="filter">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        List<VLifingCost> Select(int pageSize, int start, HashTableExp hash, out int total,String sqlWhere);

        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>数据集合</returns>
        LifingCost Select(string Id);

        /// <summary>
        /// 按条件查询数据
        /// </summary>
        /// <returns></returns>
        List<LifingCost> Select(HashTableExp hash,String sqlWhere);
        
        /// <summary>
        /// 汇总月份生活费消费信息
        /// </summary>
        /// <param name="beginTime">开始时间,格式2014-11-01</param>
        /// <param name="endTime">结束时间,格式2014-11-11</param>
        /// <param name="isMark">特殊标识</param>
        /// <param name="family">家庭内消费</param>
        /// <param name="costType">消费类型</param>
        /// <returns></returns>
        List<CostMonthCol> GetCollectionByMonth(String beginTime, String endTime, Boolean? isMark, Boolean? family, String costTypeIds,String userId);

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
        DataTable GetCollectionByDay(String time, String beginTime, String endTime, Boolean? isMark, Boolean? family, String costTypeIds, String userId);

        /// <summary>
        /// 根据月份汇总生活费类型
        /// </summary>
        /// <param name="time">时间，如:2013-1</param>
        /// <returns></returns>
        DataTable GetCollectionByType(String time,String sqlWhere);

        /// <summary>
        /// 查询生活费的所有消费名称
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        List<LifingCost> GetReasons(String key);

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
        DataTable GetCollectionType(String time, String beginTime, String endTime, Boolean? isMark, Boolean? family, String costTypeIds, String userId);

        /// <summary>
        /// 汇总月份生活费消费信息--图形汇总
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="beginTime">格式：2013-01</param>
        /// <param name="endTime">格式：2013-01</param>
        /// <returns></returns>
        DataTable GetCollection(String sqlWhere, String beginTime, String endTime);

        /// <summary>
        /// 根据条件查询消费总金额
        /// </summary>
        /// <returns></returns>
        Double GetTotalPrice(HashTableExp hash, String sqlWhere);

        /// <summary>
        /// 批量修改自定义分组信息
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        int ModifyCusGroup(String ids, String value);

        /// <summary>
        /// 查询所有的自定义分组名称
        /// </summary>
        /// <returns></returns>
        List<String> GetAllCusGroup();

        /// <summary>
        /// 查询所有的消费名称汇总,如:李子、桃子等
        /// </summary>
        /// <returns></returns>
        List<String> GetAllReasons();

        /// <summary>
        /// 保存,存在就修改,否则就新增
        /// </summary>
        /// <param name="lifes"></param>
        /// <returns></returns>
        bool Save(List<LifingCost> lifes);
    }
}

