using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeviceManagementSystem
{
    /// <summary>
    /// 用于标题名称映射的结构体
    /// </summary>
    public struct headtxt
    {
        /// <summary>
        /// p是原名称,n是目标名称
        /// </summary>
        public readonly string p,n;
        public headtxt(string pp, string nn)
        {
            p = pp;
            n = nn;
        }
    }

    public partial class Form1 : Form
    {
        OleDbConnection conn;
        OleDbDataAdapter da;
        OleDbCommandBuilder cmdb;
        DataSet ds;

        #region "Constants"
        /// <summary>
        /// 两个表的名称
        /// </summary>
        public const string bm1 = "shebeixinxi", bm2 = "shiyongqingkuang",bm3 = "result";
        /// <summary>
        /// 保存列标题的对应
        /// </summary>
        public static readonly  headtxt[] headtxtlist ={  
                                                new headtxt("yiqibianhao","仪器编号"),
                                                new headtxt("fenleihao","分类号"),
                                                new headtxt("mingcheng","名称"),
                                                new headtxt("xinghao","型号"),
                                                new headtxt("guige","规格"),
                                                new headtxt("danjia","单价"),
                                                new headtxt("changjia","厂家"),
                                                new headtxt("chuchangriqi","出厂日期"),
                                                new headtxt("chuchanghao","出厂号"),
                                                new headtxt("gouzhiriqi","购置日期"),
                                                new headtxt("jingfeikemu","经费科目"),
                                                new headtxt("shiyongfangxiang","使用方向"),
                                                new headtxt("jingshouren","经手人"),
                                                new headtxt("lingyongren","领用人"),
                                                new headtxt("zichanleibie","资产类别"),
                                                new headtxt("rukushijian","入库时间"),
                                                new headtxt("xianzhuang","现状"),
                                                new headtxt("beizhu","备注"),
                                                new headtxt("cunfangdi","存放地"),
                                                new headtxt("dangqianshiyongren","当前使用人"),
                                                new headtxt("shiyongshijian","使用时间"),
                                                new headtxt("shebeizhaopian","设备照片"),
                                                new headtxt("suoshubumen","所属部门"),
                                                new headtxt("shebeixinxi.yiqibianhao","仪器编号"),
                                                new headtxt("shebeixinxi.jingshouren","设备经手人"),
                                                new headtxt("shebeixinxi.beizhu","设备备注"),
                                                new headtxt("shiyongqingkuang.yiqibianhao","仪器编号"),
                                                new headtxt("shiyongqingkuang.jingshouren","经手人"),
                                                new headtxt("shiyongqingkuang.beizhu","备注")
                                            };

        #endregion

        /// <summary>
        /// 主窗口的构造函数
        /// </summary>
        /// <history>
        /// 01-zsw
        /// </history>
        public Form1()
        {
            InitializeComponent();
            app_Load();
        }
        /// <summary>
        /// 程序的初始化工作
        /// </summary>
        /// <history>
        /// 04-zsw
        /// </history>
        private void app_Load()
        {
            Program.global_Directory = System.AppDomain.CurrentDomain.BaseDirectory.Substring(0, System.AppDomain.CurrentDomain.BaseDirectory.LastIndexOf('\\'));
            Program.global_Directory = Program.global_Directory.Substring (0,Program.global_Directory.LastIndexOf('\\'));
        }
        /// <summary>
        /// 主窗口初始化函数
        /// </summary>
        /// <history>
        /// 01-zsw
        /// </history>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

            datatoolsinitialize();
            Location = new Point( Convert.ToInt32(Mytools.ContentValue("config", "X")),Convert.ToInt32(Mytools.ContentValue("config", "Y")));
            Size = new System.Drawing.Size(Convert.ToInt32(Mytools.ContentValue("config", "Width")), Convert.ToInt32(Mytools.ContentValue("config", "Height")));
            D_Load();
            D_Print(bm1);
            D_Print(bm2);
        }
        private void datatoolsinitialize()
        {
            string conntxt = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+Program.global_Directory +"\\Reslibrary\\db1.mdb";
            conn = new OleDbConnection(conntxt);
            da = new OleDbDataAdapter("",conn);
            ds = new DataSet();
            cmdb = new OleDbCommandBuilder();
        }
        /// <summary>
        ///dataset加载数据库
        /// </summary>
        /// <history>
        /// 01-zsw
        /// </history>
        /// <returns>bool D_Load ->加载是否成功</returns>
        private bool D_Load()
        {
            try
            {
                string commandtxt;
                commandtxt = "SELECT * FROM shebeixinxi";
                da.SelectCommand.CommandText = commandtxt;
                da.Fill(ds, bm1);
                commandtxt = "SELECT * FROM shiyongqingkuang";
                da.SelectCommand.CommandText = commandtxt;
                da.Fill(ds, bm2);
                ds.Tables[bm1].PrimaryKey = new DataColumn[1]{ ds.Tables[0].Columns["yiqibianhao"]};
                ds.Tables[bm2].PrimaryKey = new DataColumn[1]{ ds.Tables[1].Columns["yiqibianhao"]};
                ForeignKeyConstraint fc = new ForeignKeyConstraint("bond", ds.Tables[bm1].Columns["yiqibianhao"], ds.Tables[bm2].Columns["yiqibianhao"]);
                ds.Tables[bm2].Constraints.Add(fc);
                ds.Tables.Add("result");
                ds.Tables[bm1].Columns["yiqibianhao"].AllowDBNull = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("数据库加载失败,可能的原因：\n"+ e.ToString());
                return false;
            }

            return true;
        }
        /// <summary>
        /// 从屏幕输出ds中的数据
        /// </summary>
        /// <history>
        /// 01-zsw
        /// </history>
        /// <param name="table">string table -> 表的名称</param>
        public void D_Print(string table = bm1)
        {
            if (table == bm1)
            {
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = table;
                uint i = 0;
                foreach (DataGridViewColumn acol in dataGridView1.Columns)
                {
                    acol.Width = Convert.ToInt32(Mytools.ContentValue(bm1, "width" + i.ToString("00")));
                    acol.HeaderText = Mytools.namemapping(acol.Name);
                    i++;
                }
            }
            else if (table == bm2)
            {
                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = table;
                uint i = 0;
                foreach (DataGridViewColumn acol in dataGridView2.Columns)
                {
                    acol.Width = Convert.ToInt32(Mytools.ContentValue(bm2, "width" + i.ToString("00")));
                    acol.HeaderText = Mytools.namemapping(acol.Name);
                    i++;
                }
            }
            else if (table == bm3)
            {
                dataGridView3.DataSource = ds;
                dataGridView3.DataMember = table;
                uint i = 0;
                foreach (DataGridViewColumn acol in dataGridView3.Columns)
                {
                    acol.Width = Convert.ToInt32(Mytools.ContentValue(bm3, "width" + i.ToString("00")));
                    if (acol.Name == "shiyongqingkuang.yiqibianhao")
                    {
                        acol.Visible = false;
                    }
                    acol.HeaderText = Mytools.namemapping(acol.Name);
                    i++;
                    
                }
            }
            else
            {
                MessageBox.Show("BUG");
            }
        }
        

        private void saveconfig()
        {
            uint i = 0;
            foreach (DataGridViewColumn acol in dataGridView1.Columns)
            {
                Mytools.WritePrivateProfileString(bm1, "width" + i.ToString("00"), acol.Width.ToString());
                i++;
            }
            i = 0;
            foreach (DataGridViewColumn acol in dataGridView2.Columns)
            {
                Mytools.WritePrivateProfileString(bm2, "width" + i.ToString("00"), acol.Width.ToString());
                i++;
            }
            i = 0;
            foreach (DataGridViewColumn acol in dataGridView3.Columns)
            {
                Mytools.WritePrivateProfileString(bm3, "width" + i.ToString("00"), acol.Width.ToString());
                i++;
            }
            Mytools.WritePrivateProfileString("config", "X", Location.X.ToString());
            Mytools.WritePrivateProfileString("config", "Y", Location.Y.ToString());
            Mytools.WritePrivateProfileString("config", "Height", Size.Height.ToString());
            Mytools.WritePrivateProfileString("config", "Width", Size.Width.ToString());
        }
        private void savechange()
        {
            da.SelectCommand.CommandText = "SELECT * FROM shebeixinxi";
            cmdb.DataAdapter = da;
            da.Update(ds.Tables[bm1]);
            da.SelectCommand.CommandText = "SELECT * FROM shiyongqingkuang";
            cmdb.DataAdapter = da;
            da.Update(ds.Tables[bm2]);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ds.HasChanges())
            {

                DialogResult result = MessageBox.Show("是否保存更改？", "确认退出", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Yes:
                        savechange();
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    default:
                        MessageBox.Show("BUG");
                        Application.Exit();
                        break;
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            saveconfig();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                Adder2 adder = new Adder2(ds);
                adder.Show();
            }
            else
            {
                Adder1 adder = new Adder1(ds);
                adder.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Searcher sear = new Searcher(ds,dataGridView3,conn);
            sear.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Mytools.CreateEx("result", "result", saveFileDialog1, ds.Tables[bm3],dataGridView3 );
        }

    }
}
