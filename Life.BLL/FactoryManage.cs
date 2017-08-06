using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Life.Factory.LifeMan;
using System.Configuration;
using System.Reflection;
using Life.Factory.Flow;
using Life.Factory;
using Life.Factory.JiaoXue;

namespace Life.BLL
{
    public class FactoryManage
    {
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["DAL"];

        //使用缓存
        private static object CreateObject(string AssemblyPath, string classNamespace)
        {
            object objType = DataCache.GetCache(classNamespace);
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                    DataCache.SetCache(classNamespace, objType);// 写入缓存
                }
                catch//(System.Exception ex)
                {
                    //string str=ex.Message;// 记录错误日志
                }
            }
            return objType;
        }
        
        #region 获得数据访问层操作对象
        #region 扫帚相关
            
        /// <summary>
        /// 扫帚采购信息
        /// </summary>
        /// <returns></returns>
        public static FPurchase GetPurchase()
        {
            string ClassNamespace = AssemblyPath + ".DLPurchase";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FPurchase)objType;
        }

        /// <summary>
        /// 扫帚销售信息
        /// </summary>
        /// <returns></returns>
        public static FSalesVol GetSalesVol()
        {
            string ClassNamespace = AssemblyPath + ".DLSalesVol";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FSalesVol)objType;
        }
        #endregion

        #region Flow

        /// <summary>
        /// 获得流程类型表
        /// </summary>
        /// <returns></returns>
        public static FFlowType GetFlowType()
        {
            string ClassNamespace = AssemblyPath + ".Flow.DLFlowType";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FFlowType)objType;
        }

        /// <summary>
        /// 获得流程表
        /// </summary>
        /// <returns></returns>
        public static FWorkFlow GetWorkFlow()
        {
            string ClassNamespace = AssemblyPath + ".Flow.DLWorkFlow";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FWorkFlow)objType;
        }
        #endregion

        #region LifeMan

        /// <summary>
        /// 获得文章类型表
        /// </summary>
        /// <returns></returns>
        public static FArtCategory GetArtCategory()
        {
            string ClassNamespace = AssemblyPath + ".LifeMan.DLArtCategory";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FArtCategory)objType;
        }

        /// <summary>
        /// 获得文章表
        /// </summary>
        /// <returns></returns>
        public static FArticle GetArticle()
        {
            string ClassNamespace = AssemblyPath + ".LifeMan.DLArticle";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FArticle)objType;
        }

        /// <summary>
        /// 获得银行卡操作记录表
        /// </summary>
        /// <returns></returns>
        public static FBankCard GetBankCard()
        {
            string ClassNamespace = AssemblyPath + ".LifeMan.DLBankCard";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FBankCard)objType;
        }

        /// <summary>
        /// 获得银行卡日志记录表
        /// </summary>
        /// <returns></returns>
        public static FBankCardLog GetBankCardLog()
        {
            string ClassNamespace = AssemblyPath + ".LifeMan.DLBankCardLog";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FBankCardLog)objType;
        }

        /// <summary>
        /// 获得字典表
        /// </summary>
        /// <returns></returns>
        public static FDiction GetDiction()
        {
            string ClassNamespace = AssemblyPath + ".LifeMan.DLDiction";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FDiction)objType;
        }

        /// <summary>
        /// 获得收入记录表
        /// </summary>
        /// <returns></returns>
        public static FIncome GetIncome()
        {
            string ClassNamespace = AssemblyPath + ".LifeMan.DLIncome";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FIncome)objType;
        }

        /// <summary>
        /// 获得生活费操作管理
        /// </summary>
        /// <returns></returns>
        public static FLifingCost GetLifingCost()
        {
            string ClassNamespace = AssemblyPath + ".LifeMan.DLLifingCost";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FLifingCost)objType;
        }

        /// <summary>
        /// 获得生活费日志记录表
        /// </summary>
        /// <returns></returns>
        public static FLifingCostLog GetLifingCostLog()
        {
            string ClassNamespace = AssemblyPath + ".LifeMan.DLLifingCostLog";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FLifingCostLog)objType;
        }

        /// <summary>
        /// 获得模块表
        /// </summary>
        /// <returns></returns>
        public static FModule GetModule()
        {
            string ClassNamespace = AssemblyPath + ".LifeMan.DLModule";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FModule)objType;
        }

        /// <summary>
        /// 获得角色模块对应表
        /// </summary>
        /// <returns></returns>
        public static FRoleToModule GetRoleToModule()
        {
            string ClassNamespace = AssemblyPath + ".LifeMan.DLRoleToModule";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FRoleToModule)objType;
        }

        /// <summary>
        /// 获得角色表
        /// </summary>
        /// <returns></returns>
        public static FRoles GetRoles()
        {
            string ClassNamespace = AssemblyPath + ".LifeMan.DLRoles";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FRoles)objType;
        }

        /// <summary>
        /// 获得系统配置
        /// </summary>
        /// <returns></returns>
        public static FSysConfig GetSysConfig()
        {
            string ClassNamespace = AssemblyPath + ".LifeMan.DLSysConfig";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FSysConfig)objType;
        }

        /// <summary>
        /// 获得临时信息存储表
        /// </summary>
        /// <returns></returns>
        public static FTempData GetTempData()
        {
            string ClassNamespace = AssemblyPath + ".LifeMan.DLTempData";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FTempData)objType;
        }

        /// <summary>
        /// 获得用户角色对应表
        /// </summary>
        /// <returns></returns>
        public static FUserToRole GetUserToRole()
        {
            string ClassNamespace = AssemblyPath + ".LifeMan.DLUserToRole";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FUserToRole)objType;
        }

        /// <summary>
        /// 获得用户表
        /// </summary>
        /// <returns></returns>
        public static FUsers GetUsers()
        {
            string ClassNamespace = AssemblyPath + ".LifeMan.DLUsers";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FUsers)objType;
        }

        /// <summary>
        /// 获得银行卡类型
        /// </summary>
        /// <returns></returns>
        public static FBankType GetBankType()
        {
            string ClassNamespace = AssemblyPath + ".LifeMan.DLBankType";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FBankType)objType;
        }
        #endregion

        #region MyRegion

        /// <summary>
        /// 获得收入记录表
        /// </summary>
        /// <returns></returns>
        public static FStudent GetStudent()
        {
            string ClassNamespace = AssemblyPath + ".JiaoXue.DLStudent";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FStudent)objType;
        }
        #endregion

        #endregion

    }
}
