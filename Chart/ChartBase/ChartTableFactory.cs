using System;

using System.Collections.Generic;
using System.Text;
using System.Data;
using Excel;
using System.IO;

namespace Chart.ChartBase
{
    public class ChartTableFactory
    {
        public static DataTable CreateChartTable()
        {
            DataTable table = new DataTable();
            InitColumns(table);
            return table;
        }
        public static DataTable CreateChartTable(string fileName)
        {
            DataTable table = null;
            if (getExt(fileName).Equals("txt"))
            {
                table = new DataTable("table");
                InitColumns(table);
                StreamReader sr = File.OpenText(fileName);
                String input=sr.ReadLine();//第一行不读
                while ((input = sr.ReadLine()) != null)
                {
                    string[] str = input.Split(new char[] { ' ', ',', '\t',';'});
                    table.Rows.Add(str);
                }
                sr.Close();
            }
            if (getExt(fileName).Equals("xls"))
            {
                FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                excelReader.IsFirstRowAsColumnNames = true;
                DataSet data = excelReader.AsDataSet(true);
                excelReader.Close();
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    double days = Convert.ToDouble(row[1].ToString());
                    DateTime dt = DateTime.Parse("1899-12-30");
                    dt = dt.AddDays(days);
                    row[1] = dt.ToString("yyyy-MM-dd");
                }
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    double days = Convert.ToDouble(row[2].ToString());
                    DateTime dt = DateTime.Parse("1899-12-30");
                    dt = dt.AddDays(days);
                    row[2] = dt.ToString("HH:mm:ss");
                }
                if (data.Tables.Count > 0)
                {
                    table = data.Tables[0];
                    int count = table.Columns.Count;
                    for (int i = 0; i < 10 - count; i++)
                    {
                        table.Columns.Add(getColumnName(count+i));
                    }
                }
            }
            if (getExt(fileName).Equals("xlsx"))
            {
                FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                excelReader.IsFirstRowAsColumnNames = true;
                DataSet result = excelReader.AsDataSet(true);
                excelReader.Close();
                if (result.Tables.Count > 0)
                {
                    table = result.Tables[0];
                    InitColumns(table);
                }
            }
            return table;
        }
        private static void InitColumns(DataTable table)
        {
            if (table != null)
            {
                int count = table.Columns.Count;
                for (int i = 0; i < 10-count; i++)
                {
                    table.Columns.Add(getColumnName(count+i));
                }
            }
        }
        public static string getColumnName(int i)
        {
            switch (i)
            {
                case 0: return "Number";
                case 1: return "Date";
                case 2: return "Time";
                case 3: return "SYS";
                case 4: return "MAP";
                case 5: return "DIA";
                case 6: return "HeartRate";
                case 7: return "PulsePressure";
                case 8: return "RPP";
                case 9: return "Comment";
                default: return "Col"+i;
            }
        }
        public static string getExt(string filename)
        {
            string ext = filename.Substring(filename.LastIndexOf(".") + 1);
            return ext.ToLower();
        }
    }
}
