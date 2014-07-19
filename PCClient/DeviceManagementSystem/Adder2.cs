using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DeviceManagementSystem
{
    public partial class Adder2 : Form
    {
        DataSet ds;
        int addingnum = -1;
        public Adder2(DataSet ds1)
        {
            InitializeComponent();
            ds = ds1;
        }

        #region "Check"
        public bool D_check(string yiqibianhao, string cunfangdi, string dangqianshiyongren, string shiyongshijian, string jingshouren, string beizhu, string shebeizhaopian, string suoshubumen)
        {
            bool Pass = true;
            StringBuilder errtxt = new StringBuilder(1024);
            try
            {
                yiqibianhaocheck(yiqibianhao, errtxt, false);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            Pass = cunfangdicheck(cunfangdi, errtxt, false) && Pass;
            Pass = dangqianshiyongrencheck(dangqianshiyongren, errtxt, false) && Pass;
            Pass = shiyongshijiancheck(shiyongshijian, errtxt, false, Convert.ToDateTime(ds.Tables[0].Rows[addingnum]["rukushijian"])) && Pass;
            Pass = jingshourencheck(jingshouren, errtxt, false) && Pass;
            Pass = suoshubumencheck(suoshubumen, errtxt, false) && Pass;
            Pass = shebeizhaopiancheck(shebeizhaopian, errtxt, false) && Pass;
            Pass = beizhucheck(beizhu, errtxt, false) && Pass;
            if (!Pass)
            {
                MessageBox.Show(errtxt.ToString());
            }
            return Pass;

        }
        public string yiqibianhaocheck(string yiqibianhao, StringBuilder errtxt, bool prompt)
        {
            int yiqibianhaoint;
            addingnum = -1;
            if (yiqibianhao == "")
            {
                errtxt.Append("仪器编号不能为空！\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                throw new Exception("仪器编号为空");
            }
            try
            {
                yiqibianhaoint = Convert.ToInt32(yiqibianhao);
            }
            catch (Exception e)
            {
                errtxt.Append("仪器编号不是整数。仪器编号应当是一个正整数\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                throw new Exception("仪器编号不是整数");
            }

            if (yiqibianhaoint < 0)
            {
                errtxt.Append("仪器编号不是正数。仪器编号应当是一个正整数\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                throw new Exception("仪器编号不是正数");
            }
            for (int i = ds.Tables[Form1.bm2].Rows.Count - 1; i >= 0; i--)
            {
                if (yiqibianhaoint == Convert.ToInt32(ds.Tables[Form1.bm2].Rows[i]["yiqibianhao"]))
                {
                    throw new Exception("该记录已存在");
                }
            }

            for(int i = ds.Tables[Form1.bm1].Rows.Count -1;i >= 0;i--)
            {
                if (yiqibianhaoint == Convert.ToInt32(ds.Tables[Form1.bm1].Rows[i]["yiqibianhao"]))
                {
                    addingnum = i;
                    return ds.Tables[0].Rows[i]["mingcheng"].ToString() + ds.Tables[0].Rows[i]["rukushijian"].ToString();
                }
            }
            throw new Exception("不存在这条记录");
        }
        public static bool cunfangdicheck(string cunfangdi, StringBuilder errtxt, bool prompt)
        {
            if (cunfangdi == "")
            {
                errtxt.Append("存放地不能为空！\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }

            if (cunfangdi.Length >= 64)
            {
                errtxt.Append("存放地太长。名称应当是一个小于64字符的字符串\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }
            return true;
        }
        public static bool dangqianshiyongrencheck(string dangqianshiyongren, StringBuilder errtxt, bool prompt)
        {
            if (dangqianshiyongren == "")
            {
                errtxt.Append("当前使用人不能为空！\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }

            if (dangqianshiyongren.Length >= 16)
            {
                errtxt.Append("当前使用人太长。名称应当是一个小于16字符的字符串\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }
            return true;
        }
        public static bool shiyongshijiancheck(string shiyongshijian, StringBuilder errtxt, bool prompt, DateTime dt)
        {
            DateTime shiyongshijiandt = new DateTime();
            if (shiyongshijian == "")
            {
                errtxt.Append("使用时间不能为空！\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }
            try
            {
                shiyongshijiandt = Convert.ToDateTime(shiyongshijian);
            }
            catch (Exception e)
            {
                errtxt.Append("使用时间格式不对。参考格式：2014-3-24\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            }
            if (shiyongshijiandt > DateTime.Now)
            {
                errtxt.Append("使用时间不正确。使用时间应当不晚于当前系统时间\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            }
            if (shiyongshijiandt < Convert.ToDateTime("1920-10-17".ToString()))
            {
                errtxt.Append("使用时间不正确。使用时间应当不早于哈工大建校时间\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            }
            if (dt != null && shiyongshijiandt < dt)
            {
                errtxt.Append("使用时间不正确。使用时间应当不早于入库时间\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            }
                return true;
        }
        public static bool jingshourencheck(string jingshouren, StringBuilder errtxt, bool prompt)
        {
            if (jingshouren == "")
            {
                errtxt.Append("经手人不能为空！\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }

            if (jingshouren.Length >= 16)
            {
                errtxt.Append("经手人太长。名称应当是一个小于16字符的字符串\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }
            return true;
        }
        public static bool suoshubumencheck(string suoshubumen, StringBuilder errtxt, bool prompt)
        {
            if (suoshubumen == "")
            {
                errtxt.Append("所属部门不能为空！\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }

            if (suoshubumen.Length >= 32)
            {
                errtxt.Append("所属部门太长。名称应当是一个小于32字符的字符串\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }
            return true;
        }
        public static bool shebeizhaopiancheck(string shebeizhaopian, StringBuilder errtxt, bool prompt)
        {
            if (shebeizhaopian == "")
            {
                return true;
            }

            if (shebeizhaopian.Length >= 256)
            {
                errtxt.Append("设备照片太长。名称应当是一个小于256字符的字符串\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }
            if (!File.Exists(shebeizhaopian))
            {
                errtxt.Append("该文件不存在\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;

            }
            if (!Mytools.IsPicture(shebeizhaopian))
            {
                errtxt.Append("该文件可能不是图片，请使用普通一点的图片格式^_^\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;

            }
            return true;
        }
        public static bool beizhucheck(string beizhu, StringBuilder errtxt, bool prompt)
        {
            if (beizhu.Length >= 256)
            {
                errtxt.Append("备注太长。备注应当是一个小于256字符的字符串\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                } return false;
            } return true;
        }

        #endregion

        public void D_add(string yiqibianhao, string cunfangdi, string dangqianshiyongren, string shiyongshijian, string jingshouren, string beizhu, string shebeizhaopian, string suoshubumen)
        {
            DataRow row = ds.Tables[Form1.bm2].NewRow();
            row["yiqibianhao"] = Convert.ToInt32(yiqibianhao);
            row["cunfangdi"] = cunfangdi;
            row["dangqianshiyongren"] = dangqianshiyongren;
            row["shiyongshijian"] = shiyongshijian;
            row["jingshouren"] = jingshouren;
            row["suoshubumen"] = suoshubumen;
            row["shebeizhaopian"] = shebeizhaopian;
            row["beizhu"] = beizhu;
            ds.Tables[Form1.bm2].Rows.Add(row);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            bool pass = D_check(comboBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim(), textBox4.Text.Trim(), textBox5.Text.Trim(), textBox6.Text.Trim(), textBox7.Text.Trim(), textBox8.Text.Trim());
            if (pass)
            {
                D_add(comboBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim(), textBox4.Text.Trim(), textBox5.Text.Trim(), textBox6.Text.Trim(), textBox7.Text.Trim(), textBox8.Text.Trim());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
