using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Data;
using System.Reflection;
using System.Windows.Forms;


namespace DeviceManagementSystem
{
    /// <summary>
    /// 工具函数
    /// </summary>
    /// <remarks>
    /// 使用全局变量：global_Directory
    /// </remarks>
    static class Mytools
    {
        #region "ini"
        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">节点名称[如[TypeName]]</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern int WritePrivateProfileString(string section, string key, string val, string filepath);
        public static int WritePrivateProfileString(string section, string key, string val)
        {
            return WritePrivateProfileString(section, key, val, strFilePath);
        }

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="def">值</param>
        /// <param name="retval">stringbulider对象</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        /// 所使用全局变量：golbal_Directory
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        private static string strFilePath = Program.global_Directory + @"\Reslibrary\Config.ini";//获取INI文件路径
        /// <summary>
        /// 自定义读取INI文件中的内容方法
        /// </summary>
        /// <param name="Section">键</param>
        /// <param name="key">值</param>
        /// <returns></returns>
        public static string ContentValue(string Section, string key)
        {

            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }

        #endregion
        #region "namemapping"
        public static string namemapping(string str)
        {
            foreach (headtxt head in Form1.headtxtlist)
            {
                if (head.p == str.Trim())
                {
                    return head.n;
                }
            }
            return str;
        }
        #endregion
        #region "excel"
        /// <summary>
        /// 创建excel表格
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="sheetNames">sheet名</param>
        /// 
        public static void CreateEx(string title, string sheetNames, System.Windows.Forms.SaveFileDialog saveFileDialog1, System.Data.DataTable dt, DataGridView dv)
        {
            string fileName = saveFileDialog1.FileName;
            string FileName;
            saveFileDialog1.FileName = sheetNames ;
            saveFileDialog1.DefaultExt = ".xlsx";
            saveFileDialog1.Filter = "excel文件(*.xlsx)|*.xlsx|所有文件(*.*)|*.*";
            if (saveFileDialog1.ShowDialog(Program.mainform) == DialogResult.OK)
            {
                fileName = saveFileDialog1.FileName;
                FileName = fileName;
            }

            string FilePath = saveFileDialog1.FileName;
            //string fileName = "报表";//报表名称
            //string FilePath = @"E:\报表";//生成文件在E盘目录下
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
            if (fileName == "")
            {
                return;
            }
            if (sheetNames != null && sheetNames != "")
            {
                Excel.Application m_Excel = new Excel.Application();
                m_Excel.SheetsInNewWorkbook = 1;
                Excel._Workbook m_Book = (Excel._Workbook)(m_Excel.Workbooks.Add(Missing.Value));//添加新工作簿
                Excel._Worksheet m_Sheet = (Excel._Worksheet)(m_Excel.Worksheets.Add(Missing.Value));//添加新工作表

                m_Sheet.Name = sheetNames ;


                ////创建表格查询
                int rowCount1 = dt.Rows.Count;
                int columnCount1 = dt.Columns.Count;
                DataToSheet(title, dt, m_Sheet, m_Book,dv);//创建表格


                m_Book.SaveAs(FilePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                //保存文件
                m_Book.Close(false, Missing.Value, Missing.Value);
                //关闭工作簿
                m_Excel.Quit();
                //退出进程
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_Book);//释放资源
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_Excel);//释放资源
                m_Book = null;
                m_Sheet = null;
                m_Excel = null;
                GC.Collect();
            }
        }

        public static void DataToSheet(string title, System.Data.DataTable dt, Excel._Worksheet m_Sheet, Excel._Workbook m_Book, DataGridView dv)
        {
            Excel.Range r;//声明Range对象
            int rownum = dt.Rows.Count;//获取数据行数
            int columnnum = dt.Columns.Count;//获取数据列数

            r = m_Sheet.Range[m_Sheet.Cells[1,1], m_Sheet.Cells[1, columnnum]];
            r.Font.Bold = true;
            r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            r.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            
            //OperatorExcel op = new OperatorExcel();//声明op对象以设置单元格的样式
            //op.SetColumnWidth(m_Sheet, m_Sheet.Cells[3, 1], m_Sheet.Cells[3 + rownum + 1, columnnum], 15);//设置列宽
            //op.SetRowHeight(m_Sheet, m_Sheet.Cells[4, 1], m_Sheet.Cells[3 + rownum + 1, columnnum], 20);//设置行高
            //op.SetHAlignCenter(m_Sheet, m_Sheet.Cells[3, 1], m_Sheet.Cells[3 + rownum + 1, columnnum]);//字体居中
            //op.SetRowHeight(m_Sheet, m_Sheet.Cells[3, 1], m_Sheet.Cells[3, columnnum], 80);//第三行高度
            //op.SetBorderAll(m_Sheet, m_Sheet.Cells[3, 1], m_Sheet.Cells[3 + rownum + 1, columnnum]);//设置边框

            //m_Sheet.Cells[num + 2, 1] = "Regional";//追加行
            //op.SetBgColor(m_Sheet, m_Sheet.Cells[num + 2, 1], m_Sheet.Cells[num + 2, columnnum], Color.Gray);//追加行样式
            //将字段名写入文档（字段名就是列名）
            for (int j = 0; j < columnnum; j++)
            {
                foreach (headtxt htt in Form1.headtxtlist)
                {
                    if (dt.Columns[j].ColumnName == htt.p)
                    {
                        m_Sheet.Cells[1, 1 + j] = htt.n;
                    }
                }

                //foreach (DataGridViewColumn acol in dv.Columns)
                //{
                //    Excel.Range rr;
                //    rr = m_Sheet.Range[m_Sheet.Cells[1, j+1], m_Sheet.Cells[1, j+1]];
                //    rr.ColumnWidth = dv.Columns[j].Width;
                    
                //}

                //op.SetBgColor(m_Sheet, m_Sheet.Cells[q, 1 + j], m_Sheet.Cells[q, 1 + j], Color.Red);

            }
            //逐行写入数据
            for (int i = 0; i < rownum; i++)
            {
                for (int j = 0; j < columnnum; j++)
                {
                    m_Sheet.Cells[2 + i, 1 + j] = dt.Rows[i][j].ToString();
                    //if (j > 0 && dt.Rows[i][j].ToString() != "")
                    //{
                    //    //拆分数据以拿出比较与100%小于100%的红色标识
                    //    string b = dt.Rows[i][j].ToString();
                    //    string[] V = b.Split('%');
                    //    string d = V[0];
                    //    double c = Convert.ToDouble(d);
                    //    double f = c / 100;
                    //    int A = (int)Math.Ceiling(f);
                    //    if (A <= 1)
                    //    {
                    //        op.SetColor(m_Sheet, m_Sheet.Cells[p + 2 + i, 1 + j], m_Sheet.Cells[p + 2 + i, 1 + j], Color.Red);
                    //    }
                    //}
                }

                //m_Sheet.Cells[p + 2 + i, 1 + columnnum] = "By VAR";//追加列值
                //if (i == rownum - 1)
                //    op.AddHyperLink(m_Sheet, m_Sheet.Cells[p + 2 + i, 1 + columnnum], "E:\\AAA.xlsx", "By VAR", "By VAR");

            }
            ((Excel.Range)m_Sheet.Cells[1, 19]).Select();
            ((Excel.Range)m_Sheet.Cells[1, 19]).EntireColumn.Delete(0); 
            r.EntireColumn.AutoFit(); 
            // op.Merge(m_Sheet, m_Sheet.Cells[2, 2], m_Sheet.Cells[3, 2]);
            //op.WriteAfterMerge(m_Sheet, m_Sheet.Cells[2, 3], m_Sheet.Cells[2, 5], "SW");//合并单元格
            // op.Merge(m_Sheet, m_Sheet.Cells[2, 3], m_Sheet.Cells[2, 5]);
            //op.SetHAlignCenter(m_Sheet, m_Sheet.Cells[2, 3], m_Sheet.Cells[2, 5]);//居中设置
            //op.WriteRange(m_Sheet, m_Sheet.Cells[2, 3], m_Sheet.Cells[2, 5],"SW");
            //op.WriteCell(m_Sheet, m_Sheet.Cells[2, 3], "SW");
            //for (int m = 1; m < columnnum;m++ )
            //{
            //    op.SetFormula(m_Sheet,m_Sheet.Cells[num + 2, m + 1],"");
            //}
            //设置连接单元格

            //for (int i = 0; i < rownum; i++)
            //{

            //    op.AddHyperLink(m_Sheet, m_Sheet.Cells[p + 2 + i, 1 + columnnum], "E:\\AAA.xlsx", "By VAR", "By VAR");
            //}
            //计算
            //for (int i = 1; i < columnnum - 3; i++)
            //{
            //    double d = 0; double s = 0; int a = rownum; double f = 0;
            //    for (int j = 0; j < rownum; j++)
            //    {
            //        if (dt.Rows[i][j].ToString() != "")
            //        {
            //            string b = dt.Rows[4 + j][1 + i].ToString();
            //            string[] V = b.Split('%');
            //            string g = V[0];
            //            double c = Convert.ToDouble(d);
            //            double h = c / 100;

            //            d = h;
            //            s += d;
            //        }
            //    }
            //    f = s / a;
            //    f = f / 100;
            //    string k = Convert.ToString(f);
            //    k += "%";
            //    op.WriteCell(m_Sheet, m_Sheet.Cells[num + 2, 1 + columnnum], k);
            //}
        }
        
        
        
        #endregion
        #region "pic"
        /// <summary>
        /// 判断文件是否是图片
        /// </summary>
        /// <param name="sText"></param>
        /// <returns></returns> 
        public static bool IsPicture(string fileName)
        {
            string strFilter = ".jpeg|.gif|.jpg|.png|.bmp|.pic|.tiff|.ico|.iff|.lbm|.mag|.mac|.mpt|.opt|";
            char[] separtor = { '|' };
            string[] tempFileds = StringSplit(strFilter, separtor);
            foreach (string str in tempFileds)
            {
                if (str.ToUpper() == fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf(".")).ToUpper())
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 通过字符串，分隔符返回string[]数组
        /// </summary>
        /// <param name="sText"></param>
        /// <returns></returns> 
        public static string[] StringSplit(string s, char[] separtor)
        {
            string[] tempFileds = s.Trim().Split(separtor);
            return tempFileds;
        }
        #endregion
    }
}
