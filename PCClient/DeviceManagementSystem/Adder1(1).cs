using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeviceManagementSystem
{
    /// <summary>
    /// 向设备信息表中添加记录的窗口
    /// </summary>
    public partial class Adder1 : Form
    {
        DataSet ds;
        /// <summary>
        /// 
        /// </summary>
        public Adder1(DataSet d)
        {
            InitializeComponent();
            ds = d;
        }

        private void Adder1_Load(object sender, EventArgs e)
        {

        }
        #region "Check"
        //public void D_check(string[] args);

        public bool D_check(string fenleihao, string mingcheng, string xinghao, string guige, string danjia, string changjia, string chuchangriqi, string chuchanghao, string gouzhiriqi, string jingfeikemu, string shiyongfangxiang, string jingshouren, string lingyongren, string zichanleibie, string rukushijian, string xianzhuang, string beizhu = "")
        {
            bool Pass = true;
            StringBuilder errtxt = new StringBuilder(1024);
            Pass = fenleihaocheck(fenleihao, errtxt, false)                &&  Pass;
            Pass = mingchengcheck(mingcheng, errtxt, false)               &&  Pass;
            Pass = xinghaocheck(xinghao, errtxt, false)                    &&  Pass;
            Pass = guigecheck(guige, errtxt, false)                        &&  Pass;
            Pass = danjiacheck(danjia, errtxt, false)                      &&  Pass;
            Pass = changjiacheck(changjia, errtxt, false)                  &&  Pass;
            Pass = chuchangriqicheck(chuchangriqi, errtxt, false )         &&  Pass;
            Pass = chuchanghaocheck(chuchanghao, errtxt, false)            &&  Pass;
            Pass = gouzhiriqicheck(gouzhiriqi, errtxt, false)              &&  Pass;
            Pass = jingfeikemucheck(jingfeikemu, errtxt, false)           &&  Pass;
            Pass = shiyongfangxiangcheck(shiyongfangxiang, errtxt, false)  &&  Pass;
            Pass = jingshourencheck(jingshouren, errtxt, false)            &&  Pass;
            Pass = lingyongrencheck(lingyongren, errtxt, false)            &&  Pass;
            Pass = zichanleibiecheck(zichanleibie, errtxt, false)          &&  Pass;
            Pass = rukushijiancheck(rukushijian, errtxt, false)           &&  Pass;
            Pass = xianzhuangcheck(xianzhuang, errtxt, false)              &&  Pass;
            Pass = beizhucheck(beizhu, errtxt, false) && Pass;
            if (!Pass)
            {
                MessageBox.Show(errtxt.ToString());
            }
            return Pass;
        }

        public static bool fenleihaocheck(string fenleihao, StringBuilder errtxt, bool prompt)
        {
            int fenleihaoint;

            if (fenleihao == "")
            {
                errtxt.Append("分类号不能为空！\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }
            try
            {
                fenleihaoint = Convert.ToInt32(fenleihao);
            }
            catch (Exception e)
            {
                errtxt.Append("分类号不是整数。分类号应当是一个正整数\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }

            if (fenleihaoint < 0)
            {
                errtxt.Append("分类号不是正数。分类号应当是一个正整数\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }
            return true;
        }
        public static bool mingchengcheck(string mingcheng, StringBuilder errtxt, bool prompt)
        {
            if (mingcheng == "")
            {
                errtxt.Append("名称不能为空！\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }

            if (mingcheng.Length >= 32)
            {
                errtxt.Append("名称太长。名称应当是一个小于32字符的字符串\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }
            return true;
        }
        public static bool xinghaocheck(string xinghao, StringBuilder errtxt, bool prompt)
        {
            if (xinghao.Length >= 32)
            {
                errtxt.Append("型号太长。型号应当是一个小于32字符的字符串\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            }
            return true;
        }
        public static bool guigecheck(string guige, StringBuilder errtxt, bool prompt)
        {
            if (guige.Length >= 32)
            {
                errtxt.Append("规格太长。规格应当是一个小于32字符的字符串\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            }
            return true;
        }
        public static bool danjiacheck(string danjia, StringBuilder errtxt, bool prompt)
        {
            float danjiaf;
            if (danjia == "")
            {
                errtxt.Append("单价不能为空！\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }
            try
            {
                danjiaf = Convert.ToSingle(danjia);
            }
            catch (Exception e)
            {
                errtxt.Append("单价格式不对。单价应当是一个正小数(不多于两位)\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            }
            if (danjiaf <= 0)
            {
                errtxt.Append("单价不是正数。分类号应当是一个正小数(不多于两位)\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            }
            return true;
        }
        public static bool changjiacheck(string changjia, StringBuilder errtxt, bool prompt)
        {
            if (changjia.Length >= 64)
            {
                errtxt.Append("厂家太长。厂家应当是一个小于64字符的字符串\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            }
            return true;
        }
        public static bool chuchangriqicheck(string chuchangriqi, StringBuilder errtxt, bool prompt)
        {
            DateTime chuchangriqidt = new DateTime();
            if (chuchangriqi == "") { return true; }
            try
            {
                chuchangriqidt = Convert.ToDateTime(chuchangriqi);
            }
            catch (Exception e)
            {
                errtxt.Append("出厂日期格式不对。参考格式：2014-3-24\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            }
            if (chuchangriqidt > DateTime.Now)
            {
                errtxt.Append("出厂日期不正确。出厂日期应当不晚于当前系统时间\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            }
            if (chuchangriqidt < Convert.ToDateTime("1920-10-17"))
            {
                errtxt.Append("出厂日期不正确。出厂日期应当不早于哈工大建校时间\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            }
            return true;
        }
        public static bool chuchanghaocheck(string chuchanghao, StringBuilder errtxt, bool prompt)
        {
            int chuchanghaoint;
            if (chuchanghao == "") { return true; }
            try
            {
                chuchanghaoint = Convert.ToInt32(chuchanghao);
            }
            catch (Exception e)
            {
                errtxt.Append("出厂号不是整数。出厂号应当是一个正整数\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            }
            if (chuchanghaoint < 0)
            {
                errtxt.Append("分类号不是正数。分类号应当是一个正整数\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            }
            return true;
        }
        public static bool gouzhiriqicheck(string gouzhiriqi, StringBuilder errtxt, bool prompt)
        {
            DateTime gouzhiriqidt = new DateTime();
            if (gouzhiriqi == "")
            {
                errtxt.Append("购置日期不能为空！\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }
            try
            {
                gouzhiriqidt = Convert.ToDateTime(gouzhiriqi);
            }
            catch (Exception e)
            {
                errtxt.Append("日期格式不对。参考格式：2014-3-24\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            }
            if (gouzhiriqidt > DateTime.Now)
            {
                errtxt.Append("购置日期不正确。购置日期应当不晚于当前系统时间\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            }
            if (gouzhiriqidt < Convert.ToDateTime("1920-10-17"))
            {
                errtxt.Append("购置日期不正确。购置日期应当不早于哈工大建校时间\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            }
            return true;
        }
        public static bool jingfeikemucheck(string jingfeikemu, StringBuilder errtxt, bool prompt)
        {
            if (jingfeikemu == "")
            {
                errtxt.Append("经费科目不能为空！\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }

            if (jingfeikemu.Length >= 32)
            {
                errtxt.Append("经费科目太长。经费科目应当是一个小于32字符的字符串\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            }
            return true;
        }
        public static bool shiyongfangxiangcheck(string shiyongfangxiang, StringBuilder errtxt, bool prompt)
        {
            if (shiyongfangxiang == "")
            {
                errtxt.Append("使用方向不能为空！\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }

            if (shiyongfangxiang.Length >= 64)
            {
                errtxt.Append("使用方向太长。使用方向应当是一个小于64字符的字符串\n\n");
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
                errtxt.Append("经手人太长。经手人应当是一个小于16字符的字符串\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return false;
            } return true;
        }
        public static bool lingyongrencheck(string lingyongren, StringBuilder errtxt, bool prompt)
        {
            if (lingyongren.Length >= 16)
            {
                errtxt.Append("领用人太长。领用人应当是一个小于16字符的字符串\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                } return false;
            }
            return true;
        }
        public static bool zichanleibiecheck(string zichanleibie, StringBuilder errtxt, bool prompt)
        {
            if (zichanleibie == "")
            {
                errtxt.Append("资产类别不能为空！\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }

            if (zichanleibie.Length >= 32)
            {
                errtxt.Append("资产类别太长。资产类别应当是一个小于32字符的字符串\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                } return false;
            } return true;
        }
        public static bool rukushijiancheck(string rukushijian, StringBuilder errtxt, bool prompt)
        {
            DateTime rukushijiandt = new DateTime();
            if (rukushijian == "")
            {
                errtxt.Append("入库时间不能为空！\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                }

                return false;
            }

            try
            {
                rukushijiandt = Convert.ToDateTime(rukushijian);
            }
            catch (Exception e)
            {
                errtxt.Append("日期格式不对。参考格式：2014-3-24\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                } return false;
            }
            if (rukushijiandt > DateTime.Now)
            {
                errtxt.Append("入库时间不正确。入库时间应当不晚于当前系统时间\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                } return false;
            }
            if (rukushijiandt < Convert.ToDateTime("1920-10-17"))
            {
                errtxt.Append("入库时间不正确。入库时间应当不早于哈工大建校时间\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                } return false;
            } return true;
        }
        public static bool xianzhuangcheck(string xianzhuang, StringBuilder errtxt, bool prompt)
        {
            if (xianzhuang.Length >= 64)
            {
                errtxt.Append("现状太长。现状应当是一个小于64字符的字符串\n\n");
                if (prompt)
                {
                    MessageBox.Show(errtxt.ToString());
                } return false;
            } return true;
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
        /// <summary>
        /// 向设备信息中添加记录
        /// </summary>
        /// <param name="fenleihao"></param>
        /// <param name="mingcheng"></param>
        /// <param name="xinghao"></param>
        /// <param name="guige"></param>
        /// <param name="danjia"></param>
        /// <param name="changjia"></param>
        /// <param name="chuchangriqi"></param>
        /// <param name="chuchanghao"></param>
        /// <param name="gouzhiriqi"></param>
        /// <param name="jingfeikemu"></param>
        /// <param name="shiyongfangxiang"></param>
        /// <param name="jingshouren"></param>
        /// <param name="lingyongren"></param>
        /// <param name="zichanleibie"></param>
        /// <param name="rukushijian"></param>
        /// <param name="xianzhuang"></param>
        /// <param name="beizhu"></param>
        public void D_add(string fenleihao, string mingcheng, string xinghao, string guige, string danjia, string changjia, string chuchangriqi, string chuchanghao, string gouzhiriqi, string jingfeikemu, string shiyongfangxiang, string jingshouren, string lingyongren, string zichanleibie, string rukushijian, string xianzhuang, string beizhu = "")
        {
            DataRow row = ds.Tables[Form1.bm1].NewRow();
            row["fenleihao"] = Convert.ToInt32(fenleihao);
            row["mingcheng"] = mingcheng;
            row["xinghao"] = xinghao;
            row["guige"] = guige;
            row["danjia"] = Convert.ToSingle(danjia);
            row["changjia"] = changjia;
            if (chuchangriqi != "")
            {
                row["chuchangriqi"] = Convert.ToDateTime(chuchangriqi);
            }
            if (chuchanghao != "")
            {
                row["chuchanghao"] = chuchanghao;
            }
            row["gouzhiriqi"] = Convert.ToDateTime(gouzhiriqi);
            row["jingfeikemu"] = jingfeikemu;
            row["shiyongfangxiang"] = shiyongfangxiang;
            row["jingshouren"] = jingshouren;
            row["lingyongren"] = lingyongren;
            row["zichanleibie"] = zichanleibie;
            row["rukushijian"] = Convert.ToDateTime(rukushijian);
            row["xianzhuang"] = xianzhuang;
            row["beizhu"] = beizhu;
            ds.Tables[Form1.bm1].Rows.Add(row);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool pass = D_check(textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim(), textBox4.Text.Trim(), textBox5.Text.Trim(), textBox6.Text.Trim(), textBox7.Text.Trim(), textBox8.Text.Trim(), textBox9.Text.Trim(), textBox10.Text.Trim(), textBox11.Text.Trim(), textBox12.Text.Trim(), textBox13.Text.Trim(), textBox14.Text.Trim(), textBox15.Text.Trim(), textBox16.Text.Trim(), textBox17.Text.Trim());
            if (pass)
            {
                D_add(textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim(), textBox4.Text.Trim(), textBox5.Text.Trim(), textBox6.Text.Trim(), textBox7.Text.Trim(), textBox8.Text.Trim(), textBox9.Text.Trim(), textBox10.Text.Trim(), textBox11.Text.Trim(), textBox12.Text.Trim(), textBox13.Text.Trim(), textBox14.Text.Trim(), textBox15.Text.Trim(), textBox16.Text.Trim(), textBox17.Text.Trim());
            }

        }
        /*    public bool D_check(string fenleihao, string mingcheng, string xinghao, string guige, string danjia, string changjia, string chuchangriqi, string chuchanghao, string gouzhiriqi, string jingfeikemu, string shiyongfangxiang, string jingshouren, string lingyongren, string zichanleibie, string rukushijian, string xianzhuang, string beizhu = "")
            {
                bool Pass = true;
                StringBuilder errtxt = new StringBuilder(1024);

            fenleihaocheck:
                int fenleihaoint;
                try
                {
                    fenleihaoint = Convert.ToInt32(fenleihao);
                }
                catch (Exception e)
                {
                    errtxt.Append("分类号不是整数。分类号应当是一个正整数\n\n");
                    Pass = false;
                    goto mingchengcheck;
                }
                if (fenleihaoint < 0)
                {
                    errtxt.Append("分类号不是正数。分类号应当是一个正整数\n\n");
                    Pass = false;
                    goto mingchengcheck;
                }

            mingchengcheck:
                if (mingcheng.Length >= 32)
                {
                    errtxt.Append("名称太长。名称应当是一个小于32字符的字符串\n\n");
                    Pass = false;
                    goto xinghaocheck;
                }

            xinghaocheck:
                if (xinghao.Length >= 32)
                {
                    errtxt.Append("型号太长。型号应当是一个小于32字符的字符串\n\n");
                    Pass = false;
                    goto guigecheck;
                }

            guigecheck:
                if (guige.Length >= 32)
                {
                    errtxt.Append("规格太长。规格应当是一个小于32字符的字符串\n\n");
                    Pass = false;
                    goto danjiacheck;
                }

            danjiacheck:
                float danjiaf;
                try
                {
                    danjiaf = Convert.ToSingle(danjia);
                }
                catch (Exception e)
                {
                    errtxt.Append("单价格式不对。单价应当是一个正小数(不多于两位)\n\n");
                    Pass = false;
                    goto changjiacheck;
                }
                if (danjiaf <= 0)
                {
                    errtxt.Append("单价不是正数。分类号应当是一个正小数(不多于两位)\n\n");
                    Pass = false;
                    goto changjiacheck;
                }

            changjiacheck:
                if (changjia.Length >= 64)
                {
                    errtxt.Append("厂家太长。厂家应当是一个小于64字符的字符串\n\n");
                    Pass = false;
                    goto chuchangriqicheck;
                }

            chuchangriqicheck:
                DateTime chuchangriqidt = new DateTime();
                try
                {
                    chuchangriqidt = Convert.ToDateTime(chuchangriqi);
                }
                catch (Exception e)
                {
                    errtxt.Append("出厂日期格式不对。参考格式：2014-3-24\n\n");
                    Pass = false;
                    goto chuchanghaocheck;
                }
                if (chuchangriqidt > DateTime.Now)
                {
                    errtxt.Append("出厂日期不正确。出厂日期应当不晚于当前系统时间\n\n");
                    Pass = false;
                    goto chuchanghaocheck;
                }
                if (chuchangriqidt < Convert.ToDateTime("1920-10-17"))
                {
                    errtxt.Append("出厂日期不正确。出厂日期应当不早于哈工大建校时间\n\n");
                    Pass = false;
                    goto chuchanghaocheck;
                }

            chuchanghaocheck:
                int chuchanghaoint;
                try
                {
                    chuchanghaoint = Convert.ToInt32(chuchanghao);
                }
                catch (Exception e)
                {
                    errtxt.Append("出厂号不是整数。出厂号应当是一个正整数\n\n");
                    Pass = false;
                    goto gouzhiriqicheck;
                }
                if (chuchanghaoint < 0)
                {
                    errtxt.Append("分类号不是正数。分类号应当是一个正整数\n\n");
                    Pass = false;
                    goto gouzhiriqicheck;
                }

            gouzhiriqicheck:
                DateTime gouzhiriqidt = new DateTime();
                try
                {
                    gouzhiriqidt = Convert.ToDateTime(gouzhiriqi);
                }
                catch (Exception e)
                {
                    errtxt.Append("日期格式不对。参考格式：2014-3-24\n\n");
                    Pass = false;
                    goto jingfeikemucheck;
                }
                if (gouzhiriqidt > DateTime.Now)
                {
                    errtxt.Append("购置日期不正确。购置日期应当不晚于当前系统时间\n\n");
                    Pass = false;
                    goto jingfeikemucheck;
                }
                if (gouzhiriqidt < Convert.ToDateTime("1920-10-17"))
                {
                    errtxt.Append("购置日期不正确。购置日期应当不早于哈工大建校时间\n\n");
                    Pass = false;
                    goto jingfeikemucheck;
                }
            jingfeikemucheck:
                if (jingfeikemu.Length >= 32)
                {
                    errtxt.Append("经费科目太长。经费科目应当是一个小于32字符的字符串\n\n");
                    Pass = false;
                    goto shiyongfangxiangcheck;
                }
            shiyongfangxiangcheck:
                if (shiyongfangxiang.Length >= 64)
                {
                    errtxt.Append("使用方向太长。使用方向应当是一个小于64字符的字符串\n\n");
                    Pass = false;
                    goto jingshourencheck;
                }

            jingshourencheck:
                if (jingshouren.Length >= 16)
                {
                    errtxt.Append("经手人太长。经手人应当是一个小于16字符的字符串\n\n");
                    Pass = false;
                    goto lingyongrencheck;
                }

            lingyongrencheck:
                if (lingyongren.Length >= 16)
                {
                    errtxt.Append("领用人太长。领用人应当是一个小于16字符的字符串\n\n");
                    Pass = false;
                    goto zichanleibiecheck;
                }

            zichanleibiecheck:
                if (zichanleibie.Length >= 32)
                {
                    errtxt.Append("资产类别太长。资产类别应当是一个小于32字符的字符串\n\n");
                    Pass = false;
                    goto rukushijiancheck;
                }

            rukushijiancheck:
                DateTime rukushijiandt = new DateTime();
                try
                {
                    rukushijiandt = Convert.ToDateTime(rukushijian);
                }
                catch (Exception e)
                {
                    errtxt.Append("日期格式不对。参考格式：2014-3-24\n\n");
                    Pass = false;
                    goto xianzhuangcheck;
                }
                if (rukushijiandt > DateTime.Now)
                {
                    errtxt.Append("入库时间不正确。入库时间应当不晚于当前系统时间\n\n");
                    Pass = false;
                    goto xianzhuangcheck;
                }
                if (rukushijiandt < Convert.ToDateTime("1920-10-17"))
                {
                    errtxt.Append("入库时间不正确。入库时间应当不早于哈工大建校时间\n\n");
                    Pass = false;
                    goto xianzhuangcheck;
                }

            xianzhuangcheck:
                if (xianzhuang.Length >= 64)
                {
                    errtxt.Append("现状太长。现状应当是一个小于64字符的字符串\n\n");
                    Pass = false;
                    goto beizhucheck;
                }

            beizhucheck:
                if (beizhu.Length >= 256)
                {
                    errtxt.Append("备注太长。备注应当是一个小于256字符的字符串\n\n");
                    Pass = false;
                }
                if (!Pass)
                {
                    MessageBox.Show(errtxt.ToString());
                }
                return Pass;
            }
            */
    }
}
