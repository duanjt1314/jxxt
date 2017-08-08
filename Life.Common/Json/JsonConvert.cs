using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace Life.Common
{
    public class JsonConvert
    {
        /// <summary>
        /// 利用微软的JavaScriptSerializer类序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string JavaScriptSerializer(object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);

        }

        public static string GetJsonString(object obj)
        {
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None, timeConverter);
        }

        #region Method
        /// <summary>
        /// 生成Json格式
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="obj">集合对象</param>
        /// <returns>转换的Json格式字符串</returns>
        public static String GetJson<T>(T obj)
        {
            if (obj == null) return "[]";
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());

            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, obj);
                String szJson = Encoding.UTF8.GetString(stream.ToArray());
                return szJson;
            }
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="obj">集合对象</param>
        /// <returns>转换的Json格式字符串</returns>
        public static String JSONString<T>(T obj)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            return Serializer.Serialize(obj);
        }

        private static String ConvertJsonDateToDateString(Match m)
        {
            String result = String.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        /// <summary>
        /// 获取Json的Model
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="szJson">Json字符串</param>
        /// <returns>实体</returns>
        public static T ParseFromJson<T>(String szJson)
        {
            T obj = Activator.CreateInstance<T>();

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());

                return (T)serializer.ReadObject(ms);
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="JsonStr">Json格式字符串</param>
        /// <returns>T集合</returns>
        public static List<T> JSONStringToList<T>(String JsonStr)
        {
            if (String.IsNullOrEmpty(JsonStr))
                return null;
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            List<T> objs = Serializer.Deserialize<List<T>>(JsonStr);
            return objs;
        }
        /// <summary>
        /// 获取Json的List集合（此方法需要约定：每个对象json字符串用♀分割）
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="strJson">Json字符串</param>
        /// <returns>实体集合</returns>
        public static List<T> GetObjectList<T>(String strJson)
        {
            List<T> oList = new List<T>();
            if (String.IsNullOrEmpty(strJson)) return oList;
            if (strJson.IndexOf('♀') == -1)
            {
                oList.Add(ParseFromJson<T>(strJson));
            }
            else
            {
                String[] arrs = strJson.Split('♀');
                foreach (String str in arrs)
                {
                    oList.Add(ParseFromJson<T>(str));
                }
            }
            return oList;
        }
        /// <summary>
        /// 获取Json的List集合
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="strJson">Json字符串</param>
        /// <param name="divide">Json字符串分隔符</param>
        /// <returns>实体集合</returns>
        public static List<T> GetObjectList<T>(String strJson, char divide)
        {
            List<T> oList = new List<T>();
            if (String.IsNullOrEmpty(strJson)) return oList;
            if (strJson.IndexOf(divide) == -1)
            {
                oList.Add(ParseFromJson<T>(strJson));
            }
            else
            {
                String[] arrs = strJson.Split(divide);
                foreach (String str in arrs)
                {
                    oList.Add(ParseFromJson<T>(str));
                }
            }
            return oList;
        }
        /// <summary>
        /// 得到实体类所有字段，用于model字段
        /// </summary>
        /// <param name="jsonObject">Object对象</param>
        /// <returns>处理后的字符串</returns>
        public static String GetFields(Object jsonObject)
        {
            StringBuilder jsonStr = new StringBuilder();
            PropertyInfo[] propertyInfo = jsonObject.GetType().GetProperties();
            for (Int32 i = 0; i < propertyInfo.Length; i++)
            {
                jsonStr.Append("\"");
                jsonStr.Append(propertyInfo[i].Name);
                jsonStr.Append("\",");
            }
            return DeleteLast(jsonStr.ToString());
        }
        /// <summary>
        /// 对象转换为Json字符串
        /// </summary>
        /// <param name="jsonObject">对象</param>
        /// <returns>Json字符串</returns>
        public static String ToJson(Object jsonObject)
        {
            if (jsonObject == null) return "";
            String jsonString = "{";
            PropertyInfo[] propertyInfo = jsonObject.GetType().GetProperties();
            for (Int32 i = 0; i < propertyInfo.Length; i++)
            {
                Object objectValue = propertyInfo[i].GetGetMethod().Invoke(jsonObject, null);
                String value = String.Empty;
                if (objectValue is DateTime || objectValue is Guid || objectValue is TimeSpan)
                {
                    value = "'" + objectValue + "'";
                }
                else if (objectValue is String)
                {
                    value = "'" + ToJson(objectValue.ToString()) + "'";
                }
                else if (objectValue is IEnumerable)
                {
                    value = ToJson((IEnumerable)objectValue);
                }
                else if (objectValue == null)
                {
                    value = "'" + ToJson(objectValue) + "'";
                }
                else
                {
                    value = ToJson(objectValue.ToString());
                }
                jsonString += "\"" + ToJson(propertyInfo[i].Name) + "\":" + value + ",";
            }
            return DeleteLast(jsonString) + "}";
        }
        /// <summary>
        /// 对象集合转换Json
        /// </summary>
        /// <param name="array">集合对象</param>
        /// <returns>Json字符串</returns>
        public static String ToJson(IEnumerable array)
        {
            String jsonString = "[";
            foreach (Object item in array)
            {
                jsonString += ToJson(item) + ",";
            }
            return DeleteLast(jsonString) + "]";
        }
        /// <summary>
        /// 类对像转换成json格式
        /// </summary>
        /// <param name="t"></param>
        /// <param name="HasNullIgnore">是否忽略NULL值</param>
        /// <returns></returns>
        public static string ToJson(object t, bool HasNullIgnore)
        {
            if (HasNullIgnore)
                return Newtonsoft.Json.JsonConvert.SerializeObject(t, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            else
                return ToJson(t);
        }
        /// <summary>
        /// 普通集合转换Json
        /// </summary>
        /// <param name="array">集合对象</param>
        /// <returns>Json字符串</returns>
        public static String ToArrayString(IEnumerable array)
        {
            String jsonString = "[";
            foreach (Object item in array)
            {
                jsonString = ToJson(item.ToString()) + ",";
            }
            return DeleteLast(jsonString) + "]";
        }
        /// <summary>
        /// 删除结尾字符
        /// </summary>
        /// <param name="str">需要删除的字符</param>
        /// <returns>完成后的字符串</returns>
        public static String DeleteLast(String str)
        {
            if (str.Length > 1)
            {
                return str.Substring(0, str.Length - 1);
            }
            return str;
        }
        /// <summary>
        /// Datatable转换为Json
        /// </summary>
        /// <param name="table">Datatable对象</param>
        /// <returns>Json字符串</returns>
        public static String ToJson(DataTable table)
        {
            String jsonString = "[";
            DataRowCollection drc = table.Rows;
            for (Int32 i = 0; i < drc.Count; i++)
            {
                jsonString += "{";
                foreach (DataColumn column in table.Columns)
                {
                    jsonString += "\"" + ToJson(column.ColumnName) + "\":";
                    if (column.DataType == typeof(DateTime))
                    {
                        jsonString += "\"" + Convert.ToDateTime(drc[i][column.ColumnName]).ToString("yyyy-MM-dd hh:mm:ss") + "\",";//ToJson(drc[i][column.ColumnName].ToString())
                    }
                    else if (column.DataType == typeof(String))
                    {
                        jsonString += "\"" + ToJson(drc[i][column.ColumnName].ToString()) + "\",";
                    }
                    else
                    {
                        jsonString += ToJson(drc[i][column.ColumnName].ToString()) + ",";
                    }
                }
                jsonString = DeleteLast(jsonString) + "},";
            }
            return DeleteLast(jsonString) + "]";
        }
        /// <summary>
        /// DataReader转换为Json
        /// </summary>
        /// <param name="dataReader">DataReader对象</param>
        /// <returns>Json字符串</returns>
        public static String ToJson(IDataReader dataReader)
        {
            String jsonString = "[";
            while (dataReader.Read())
            {
                jsonString += "{";

                for (Int32 i = 0; i < dataReader.FieldCount; i++)
                {
                    if ((String.IsNullOrEmpty(dataReader[i].ToString()) && dataReader.GetFieldType(i) == typeof(DateTime)) == false)
                    {
                        jsonString += @"""" + ToJson(dataReader.GetName(i)) + @""":";
                        if (dataReader.GetFieldType(i) == typeof(String))
                            jsonString += @"""" + ToJson(dataReader[i].ToString()) + @""",";
                        else if (dataReader.GetFieldType(i) == typeof(DateTime))
                            jsonString += @"""" + ((DateTime)dataReader[i]).ToString("yyyy-MM-dd HH:mm:ss") + @""",";
                        else if (dataReader.GetFieldType(i) == typeof(Boolean))
                            jsonString += Convert.ToString(dataReader[i]).ToLower() + ",";
                        else
                            jsonString += ToJson(dataReader[i].ToString()) + ",";
                    }
                }
                jsonString += "},";
            }
            dataReader.Close();
            jsonString = jsonString.TrimEnd(',');
            return jsonString += "]";
        }
        /// <summary>
        /// DataSet转换为Json
        /// </summary>
        /// <param name="dataSet">DataSet对象</param>
        /// <returns>Json字符串</returns>
        public static String ToJson(DataSet dataSet)
        {
            if (dataSet != null)
            {
                String jsonString = "{";
                foreach (DataTable table in dataSet.Tables)
                {
                    jsonString += "\"" + ToJson(table.TableName) + "\":" + ToJson(table) + ",";
                }
                return jsonString = DeleteLast(jsonString) + "}";
            }
            return "{\"Table\":{}}";
        }
        /// <summary>
        /// String转换为Json
        /// </summary>
        /// <param name="value">String对象</param>
        /// <returns>Json字符串</returns>
        public static String ToJson(String value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return String.Empty;
            }

            String temstr;
            temstr = value;
            temstr = temstr.Replace("{", "｛").Replace("}", "｝").Replace(":", "：").Replace(",", "，").Replace("[", "【").Replace("]", "】").Replace(";", "；").Replace("\n", "<br/>").Replace("\r", "");

            temstr = temstr.Replace("\t", "   ");
            temstr = temstr.Replace("'", "\'");
            temstr = temstr.Replace(@"\", @"\\");
            temstr = temstr.Replace("\"", "\"\"");
            return temstr;
        }
        /// <summary>
        /// 字符串过滤：空格,双引号和换行符
        /// </summary>
        /// <param name="str">传入String</param>
        /// <returns>新String</returns>
        public static String StringDispose(String str)
        {
            if (str == null || str.Length < 1) { return ""; }
            String strs = str.Replace(" ", "");
            strs = strs.Replace("'", "");
            strs = strs.Replace("\"", "");
            strs = strs.Replace("\n", "");
            strs = strs.Replace("\t", "");
            strs = strs.Replace("\r", "");
            strs = strs.Replace("\r\n", "");
            strs = strs.Replace(Environment.NewLine.ToString(), "");
            return strs;
        }
        /// <summary>
        /// 解析字符串,Json格式:[{"key":"value","key":"value"}]
        /// </summary>
        /// <param name="JsonStr">String字符串</param>
        /// <returns>返回Hashtable</returns>
        public static Hashtable GetHashTableByJosn(String JsonStr)
        {
            StringBuilder sb = new StringBuilder(StringDispose(JsonStr.Trim()));
            sb.Remove(0, 2);
            sb.Remove(sb.Length - 2, 2);
            String strs = sb.ToString();

            String[] strArrA = strs.Split(new char[] { ',' });

            if (strArrA == null || strArrA.Length < 1) { return null; }

            Hashtable hs = new Hashtable();
            foreach (String str in strArrA)
            {
                String[] strArrB = str.Split(new char[] { ':' });
                hs.Add(strArrB[0], strArrB[1]);
            }
            return hs;
        }
        /// <summary>
        /// 解析字符串,Json格式:[{"key":"value","key":"value"}]或[{"key":"value","key":"value"},{"key":"value","key":"value""}]
        /// </summary>
        /// <param name="JsonStr">传入Json字符串</param>
        /// <returns>返回DataTable</returns>
        public static DataTable GetDataTableByJosn(String JsonStr)
        {
            StringBuilder sb = new StringBuilder(StringDispose(JsonStr.Trim()));
            sb.Remove(0, 1);
            sb.Remove(sb.Length - 1, 1);
            String strs = sb.ToString();

            String[] strArrA = strs.Replace("},{", "}{").Split(new char[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

            if (strArrA == null || strArrA.Length < 1) { return new DataTable(); }

            DataTable dt = new DataTable("TableA");
            Boolean ckA = true;
            foreach (String strA in strArrA)
            {
                String[] strArrB = strA.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (ckA)
                {
                    foreach (String strB in strArrB)
                    {
                        String[] strArrC = strB.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        dt.Columns.Add(strArrC[0], typeof(String));
                    }
                    ckA = false;
                }
            }
            foreach (String strA in strArrA)
            {
                DataRow drAll = dt.NewRow();
                String[] strArrB = strA.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (String strB in strArrB)
                {
                    String[] strArrC = strB.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    drAll[strArrC[0]] += strArrC[1];
                }
                dt.Rows.Add(drAll);
            }

            return dt;
        }
        /// <summary>
        /// Json字符串转化成DataSet,传入格式:{"TableName1":"[{"key":"value","key":"value"}]","TableName2":"[{"key":"value","key":"value"}]"}
        /// </summary>
        /// <param name="JsonString"></param>
        /// <returns>返回DataSet</returns>
        public static DataSet GetDataSetByJson(String JsonString)
        {
            StringBuilder sb = new StringBuilder(StringDispose(JsonString).Trim());
            sb.Remove(0, 1);
            sb.Remove(sb.Length - 1, 1);
            String strs = sb.ToString();
            String[] strArrA = strs.Split(new String[] { "]," }, StringSplitOptions.RemoveEmptyEntries);
            if (strArrA == null || strArrA.Length < 1) { return null; }
            DataSet ds = new DataSet("DataSetA");
            foreach (String strA in strArrA)
            {
                String[] strArrB = strA.Split(new String[] { ":[" }, StringSplitOptions.RemoveEmptyEntries);
                Int32 a = 0;
                DataTable dt = new DataTable();
                foreach (String strB in strArrB)
                {
                    if (a == 0)
                    {
                        dt.TableName = strB.ToString();
                    }
                    else
                    {
                        String[] strArrC = strB.Replace("]", "").Replace("},{", "}{").Split(new char[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

                        Boolean ckA = true;
                        foreach (String strC in strArrC)
                        {
                            String[] strArrD = strC.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            if (ckA)
                            {
                                foreach (String strD in strArrD)
                                {
                                    String[] strArrE = strD.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                                    dt.Columns.Add(strArrE[0], typeof(String));
                                }
                                ckA = false;
                            }
                        }
                        foreach (String strC in strArrC)
                        {
                            DataRow drAll = dt.NewRow();
                            String[] strArrD = strC.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (String strD in strArrD)
                            {
                                String[] strArrE = strD.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                                drAll[strArrE[0]] += strArrE[1];
                            }
                            dt.Rows.Add(drAll);
                        }
                    }
                    a += 1;
                }
                ds.Tables.Add(dt);
            }
            return ds;
        }
        /// <summary>
        /// DataSet转化成Json格式的字符串
        /// </summary>
        /// <param name="ds">传入DataSet</param>
        /// <returns>Json字符串</returns>
        public static String GetJsonByDataSet(DataSet ds)
        {
            if (ds == null) { return ""; }
            StringBuilder strA = new StringBuilder();
            strA.Append("{");
            foreach (DataTable dt in ds.Tables)
            {
                strA.Append("\"" + dt.TableName.ToString() + "\":\"[");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i < dt.Rows.Count; i++)
                    {
                        strA.Append("{");
                        for (Int32 j = 0; j < dt.Columns.Count; j++)
                        {
                            if (j < dt.Columns.Count - 1)
                            {
                                strA.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":"
                                    + "\"" + dt.Rows[i][j].ToString() + "\",");
                            }
                            else if (j == dt.Columns.Count - 1)
                            {
                                strA.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":"
                                    + "\"" + dt.Rows[i][j].ToString() + "\"");
                            }
                        }
                        if (i == dt.Rows.Count - 1)
                        {
                            strA.Append("}");
                        }
                        else
                        {
                            strA.Append("},");
                        }
                    }
                }
                else
                {
                    return null;
                }
                strA.Append("]\",");
            }
            strA.Remove(strA.Length - 1, 1);
            strA.Append("}");
            return strA.ToString();
        }

        /// <summary>
        /// json格式转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T FromJson<T>(string strJson) where T : class
        {
            if (!String.IsNullOrEmpty(strJson))
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(strJson);
            return null;
        }
        #endregion
    }
}
