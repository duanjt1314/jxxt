using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Data;
using System.Web;
using NPOI.XSSF.UserModel;

namespace Life.Common
{
    /// <summary>
    /// Excel操作类 
    /// </summary>
    public class ExcelOperations
    {
        #region 导入

        /// <summary>
        /// 根据不同Excel格式得到IWorkbook
        /// </summary>
        /// <param name="excelFileStream">文件流</param>
        /// <param name="excelType">Excel文件类型</param>
        /// <returns>IWorkbook 对象</returns>
        public static IWorkbook GetIWorkbook(Stream excelFileStream, ExcelType excelType)
        {
            IWorkbook workbook = null;
            switch (excelType)
            {
                //2003格式
                case ExcelType.xls:
                    {
                        workbook = new HSSFWorkbook(excelFileStream);
                    }
                    break;
                //2007以上格式
                case ExcelType.xlsx:
                    {
                        workbook = new XSSFWorkbook(excelFileStream);
                    }
                    break;
            }
            return workbook;
        }

        /// <summary>
        /// sheet 转换为DataTable
        /// </summary>
        /// <param name="sheet">ISheet对象</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <returns>DataTable</returns>
        private static DataTable ImportDataTableFromExcel(ISheet sheet, Int32 headerRowIndex)
        {
            DataTable table = new DataTable();

            IRow headerRow = sheet.GetRow(headerRowIndex);
            Int32 cellCount = headerRow.LastCellNum;
            for (Int32 i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                if (headerRow.GetCell(i) == null || headerRow.GetCell(i).StringCellValue.Trim() == "")
                {
                    // 如果遇到第一个空列，则不再继续向后读取
                    cellCount = i + 1;
                    break;
                }

                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            for (Int32 i = (headerRowIndex + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null || row.GetCell(0) == null || row.GetCell(0).ToString().Trim() == "")
                {
                    // 如果遇到第一个空行，则不再继续向后读取
                    continue;
                }

                DataRow dataRow = table.NewRow();
                for (Int32 j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        dataRow[j] = row.GetCell(j).ToString();
                    }
                }
                table.Rows.Add(dataRow);
            }

            return table;
        }

        /// <summary>
        /// 由Excel导入DataTable
        /// </summary>
        /// <param name="excelFileStream">Excel文件流</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <param name="excelType">Excel类型</param>
        /// <returns>DataTable</returns>
        public static DataTable ImportDataTableFromExcel(Stream excelFileStream, ExcelType excelType, String sheetName, Int32 headerRowIndex)
        {
            IWorkbook workbook = GetIWorkbook(excelFileStream, excelType);

            ISheet sheet = workbook.GetSheet(sheetName);

            DataTable table = ImportDataTableFromExcel(sheet, headerRowIndex);

            excelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>
        /// 由Excel导入DataTable
        /// </summary>
        /// <param name="excelFilePath">Excel文件路径，为物理路径。</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <returns>DataTable</returns>
        public static DataTable ImportDataTableFromExcel(String excelFilePath, String sheetName, Int32 headerRowIndex)
        {
            using (FileStream stream = System.IO.File.OpenRead(excelFilePath))
            {
                return ImportDataTableFromExcel(stream, ExcelVerifyTheForm(excelFilePath), sheetName, headerRowIndex);
            }
        }

        /// <summary>
        /// 由Excel导入DataTable
        /// </summary>
        /// <param name="excelFileStream">Excel文件流</param>
        /// <param name="sheetIndex">Excel工作表索引</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <param name="excelType">Excel类型</param>
        /// <returns>DataTable</returns>
        public static DataTable ImportDataTableFromExcel(Stream excelFileStream, ExcelType excelType, Int32 sheetIndex, Int32 headerRowIndex)
        {
            IWorkbook workbook = GetIWorkbook(excelFileStream, excelType);

            ISheet sheet = workbook.GetSheetAt(sheetIndex);

            DataTable table = ImportDataTableFromExcel(sheet, headerRowIndex);

            excelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }
        /// <summary>
        /// 由Excel导入DataTable
        /// </summary>
        /// <param name="excelFilePath">Excel文件路径，为物理路径。</param>
        /// <param name="sheetIndex">Excel工作表索引</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <returns>DataTable</returns>
        public static DataTable ImportDataTableFromExcel(String excelFilePath, Int32 sheetIndex, Int32 headerRowIndex)
        {
            using (FileStream stream = System.IO.File.OpenRead(excelFilePath))
            {
                return ImportDataTableFromExcel(stream, ExcelVerifyTheForm(excelFilePath), sheetIndex, headerRowIndex);
            }
        }

        /// <summary>
        /// 由Excel导入DataSet，如果有多个工作表，则导入多个DataTable
        /// </summary>
        /// <param name="excelFileStream">Excel文件流</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <param name="excelType">Excel类型</param>
        /// <returns>DataSet</returns>
        public static DataSet ImportDataSetFromExcel(Stream excelFileStream, ExcelType excelType, Int32 headerRowIndex)
        {
            DataSet ds = new DataSet();

            IWorkbook workbook = GetIWorkbook(excelFileStream, excelType);

            for (Int32 a = 0, b = workbook.NumberOfSheets; a < b; a++)
            {
                ISheet sheet = workbook.GetSheetAt(a);

                DataTable table = ImportDataTableFromExcel(sheet, headerRowIndex);

                ds.Tables.Add(table);
            }

            excelFileStream.Close();
            workbook = null;

            return ds;
        }

        /// <summary>
        /// 由Excel导入DataSet，如果有多个工作表，则导入多个DataTable
        /// </summary>
        /// <param name="excelFilePath">Excel文件路径，为物理路径。</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <returns>DataSet</returns>
        public static DataSet ImportDataSetFromExcel(String excelFilePath, Int32 headerRowIndex)
        {
            using (FileStream stream = System.IO.File.OpenRead(excelFilePath))
            {
                return ImportDataSetFromExcel(stream, ExcelVerifyTheForm(excelFilePath), headerRowIndex);
            }
        }
        #endregion

        #region 导出

        /// <summary>
        /// IWorkbook 工厂
        /// </summary>
        /// <param name="excelType">ExcelType对象</param>
        /// <returns>IWorkbook</returns>
        private static IWorkbook GetIWorkbook(ExcelType excelType)
        {
            IWorkbook workbook = null;
            switch (excelType)
            {
                case ExcelType.xls:
                    {
                        workbook = new HSSFWorkbook();
                    }
                    break;
                case ExcelType.xlsx:
                    {
                        workbook = new XSSFWorkbook();
                    }
                    break;
            }
            return workbook;
        }

        /// <summary>
        /// 由DataSet导出Excel
        /// </summary>
        /// <param name="sourceDs">要导出数据的DataSet</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="excelType">工资表类型</param>
        /// <returns>Excel工作表</returns>
        private static Stream ExportDataSetToExcel(DataSet sourceDs, ExcelType excelType, String sheetName)
        {
            IWorkbook workbook = GetIWorkbook(excelType);

            MemoryStream ms = new MemoryStream();

            String[] sheetNames = sheetName.Split(',');

            for (Int32 i = 0; i < sheetNames.Length; i++)
            {
                ISheet sheet = workbook.CreateSheet(sheetNames[i]);
                IRow headerRow = sheet.CreateRow(0);
                // handling header.
                foreach (DataColumn column in sourceDs.Tables[i].Columns)
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

                // handling value.
                Int32 rowIndex = 1;

                foreach (DataRow row in sourceDs.Tables[i].Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);

                    foreach (DataColumn column in sourceDs.Tables[i].Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }

                    rowIndex++;
                }
            }
            workbook.Write(ms);
            ms.Flush();
            //ms.Position = 0;

            workbook = null;
            return ms;
        }

        /// <summary>
        /// 由DataSet导出Excel
        /// </summary>
        /// <param name="sourceDs">要导出数据的DataSet</param>
        /// <param name="fileName">指定Excel工作表名称</param>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns>Excel工作表</returns>
        public static void ExportDataSetToExcel(DataSet sourceDs, String fileName, String sheetName)
        {
            MemoryStream ms = ExportDataSetToExcel(sourceDs, ExcelVerifyTheForm(fileName), sheetName) as MemoryStream;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            HttpContext.Current.Response.End();
            ms.Close();
            ms = null;
        }
        /// <summary>
        /// 由DataTable导出Excel
        /// </summary>
        /// <param name="sourceTable">要导出数据的DataTable</param>
        /// <param name="sheetName">Sheet名称</param>
        /// <param name="excelType">工资表类型</param>
        /// <returns>Excel工作表</returns>
        private static Stream ExportDataTableToExcel(DataTable sourceTable, ExcelType excelType, String sheetName)
        {
            IWorkbook workbook = GetIWorkbook(excelType);

            MemoryStream ms = new MemoryStream();

            ISheet sheet = workbook.CreateSheet(sheetName);

            IRow headerRow = sheet.CreateRow(0);
            // handling header.
            foreach (DataColumn column in sourceTable.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // handling value.
            Int32 rowIndex = 1;

            foreach (DataRow row in sourceTable.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);

                foreach (DataColumn column in sourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }

                rowIndex++;
            }

            workbook.Write(ms);
            ms.Flush();
            //ms.Position = 0;

            sheet = null;
            headerRow = null;
            workbook = null;

            return ms;
        }
        /// <summary>
        /// 由DataTable导出Excel
        /// </summary>
        /// <param name="sourceTable">要导出数据的DataTable</param>
        /// <param name="fileName">指定Excel工作表名称</param>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns>Excel工作表</returns>
        public static void ExportDataTableToExcel(DataTable sourceTable, String fileName, String sheetName)
        {
            MemoryStream ms = ExportDataTableToExcel(sourceTable, ExcelVerifyTheForm(fileName), sheetName) as MemoryStream;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            HttpContext.Current.Response.End();
            ms.Close();
            ms = null;
        }
        #endregion

        #region 公用方法

        /// <summary>
        /// 检查 Excel中是否有数据
        /// </summary>
        /// <param name="excelFileStream"></param>
        /// <returns></returns>
        public static Boolean HasData(Stream excelFileStream)
        {
            using (excelFileStream)
            {
                IWorkbook workbook = new HSSFWorkbook(excelFileStream);
                if (workbook.NumberOfSheets > 0)
                {
                    ISheet sheet = workbook.GetSheetAt(0);
                    return sheet.PhysicalNumberOfRows > 0;
                }
            }
            return false;
        }

        /// <summary>
        /// 将Excel的列索引转换为列名，列索引从0开始，列名从A开始。如第0列为A，第1列为B...
        /// </summary>
        /// <param name="index">列索引</param>
        /// <returns>列名，如第0列为A，第1列为B...</returns>
        public static String ConvertColumnIndexToColumnName(Int32 index)
        {
            index = index + 1;
            Int32 system = 26;
            char[] digArray = new char[100];
            Int32 i = 0;
            while (index > 0)
            {
                Int32 mod = index % system;
                if (mod == 0) mod = system;
                digArray[i++] = (char)(mod - 1 + 'A');
                index = (index - 1) / 26;
            }
            StringBuilder sb = new StringBuilder(i);
            for (Int32 j = i - 1; j >= 0; j--)
            {
                sb.Append(digArray[j]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 转化日期
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static DateTime ConvertDate(String date)
        {
            DateTime dt = new DateTime();
            String[] time = date.Split('-');
            Int32 year = Convert.ToInt32(time[2]);
            Int32 month = Convert.ToInt32(time[0]);
            Int32 day = Convert.ToInt32(time[1]);
            String years = Convert.ToString(year);
            String months = Convert.ToString(month);
            String days = Convert.ToString(day);
            if (months.Length == 4)
            {
                dt = Convert.ToDateTime(date);
            }
            else
            {
                String rq = "";
                if (years.Length == 1)
                {
                    years = "0" + years;
                }
                if (months.Length == 1)
                {
                    months = "0" + months;
                }
                if (days.Length == 1)
                {
                    days = "0" + days;
                }
                rq = "20" + years + "-" + months + "-" + days;
                dt = Convert.ToDateTime(rq);
            }
            return dt;
        }

        /// <summary>
        /// Excel格式验证
        /// </summary>
        /// <param name="fileName">Excel文件名称</param>
        /// <returns>格式类型</returns>
        public static ExcelType ExcelVerifyTheForm(String fileName)
        {
            if (!String.IsNullOrEmpty(fileName))
            {
                if (fileName.Substring(fileName.LastIndexOf(".") + 1) == "xlsx")
                    return ExcelType.xlsx;
            }
            return ExcelType.xls;
        }

        #endregion
    }

    /// <summary>
    /// Excel类型
    /// </summary>
    public enum ExcelType
    {
        /// <summary>
        /// Excel2003格式
        /// </summary>
        xls = 2003,

        /// <summary>
        /// Excel2007以上格式 兼容2010，2013
        /// </summary>
        xlsx = 2007
    }
}
