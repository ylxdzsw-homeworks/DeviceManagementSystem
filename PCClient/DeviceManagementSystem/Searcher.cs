using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace DeviceManagementSystem
{
    /// <summary>
    /// 模糊查询对话框
    /// </summary>
    public partial class Searcher : Form
    {
        DataSet ds;
        DataGridView dv;
        OleDbConnection conn;
        OleDbDataAdapter da;
        
        public Searcher(DataSet ds1,DataGridView dv1,OleDbConnection conn1)
        {
            InitializeComponent();
            ds = ds1;
            dv = dv1;
            conn = conn1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder errtxt = new StringBuilder();
            if (textBox24.Text.Trim() != "")
            {
                int yiqibianhaoint;
                try
                {
                    yiqibianhaoint = Convert.ToInt32(textBox24.Text.Trim());
                }
                catch (Exception ex)
                {
                    errtxt.Append("仪器编号不是整数。仪器编号应当是一个正整数\n\n");
                    MessageBox.Show(errtxt.ToString());
                    return;
                }

                if (yiqibianhaoint < 0)
                {
                    errtxt.Append("仪器编号不是正数。仪器编号应当是一个正整数\n\n");
                    MessageBox.Show(errtxt.ToString());
                    return;
                }
                Search(yiqibianhaoint);
            }
            else
            if (D_Check(textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim(),
                textBox4.Text.Trim(), textBox5.Text.Trim(), textBox6.Text.Trim(),
                textBox7.Text.Trim(), textBox8.Text.Trim(), textBox9.Text.Trim(),
                textBox10.Text.Trim(), textBox11.Text.Trim(), textBox12.Text.Trim(),
                textBox13.Text.Trim(), textBox14.Text.Trim(), textBox15.Text.Trim(),
                textBox16.Text.Trim(), textBox17.Text.Trim(), textBox18.Text.Trim(),
                textBox19.Text.Trim(), textBox20.Text.Trim(), textBox21.Text.Trim(),
                textBox22.Text.Trim(), textBox23.Text.Trim()))
            {
                Search(textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim(),
                textBox4.Text.Trim(), textBox5.Text.Trim(), textBox6.Text.Trim(),
                textBox7.Text.Trim(), textBox8.Text.Trim(), textBox9.Text.Trim(),
                textBox10.Text.Trim(), textBox11.Text.Trim(), textBox12.Text.Trim(),
                textBox13.Text.Trim(), textBox14.Text.Trim(), textBox15.Text.Trim(),
                textBox16.Text.Trim(), textBox17.Text.Trim(), textBox18.Text.Trim(),
                textBox19.Text.Trim(), textBox20.Text.Trim(), textBox21.Text.Trim(),
                textBox22.Text.Trim(), textBox23.Text.Trim());
            }
            else
            {
                return;
            }

            this.Close();
        }
    

        private void Search(int yiqibianhao)
        {
            //DataRow[] drs = new DataRow[256];
            //DataTable dta1 = new DataTable("temp");
            //DataTable dta2 = new DataTable("temp");
            //dta1 = ds.Tables[0].Clone();
            //dta2 = ds.Tables[1].Clone();
            

            //DataRelation ddr = new DataRelation("shit", ds.Tables[0].Columns["yiqibianhao"], ds.Tables[1].Columns["yiqibianhao"]);

            //drs = ds.Tables[0].Select("yiqibianhao = " + yiqibianhao.ToString());
            //foreach (DataRow dr in drs)
            //{
            //    dta1.Rows.Add(dr.ItemArray );
            //}
            //ds.Tables["result"].Merge(dta1, true, MissingSchemaAction.Add);
            
            //drs = ds.Tables[1].Select("yiqibianhao = " + yiqibianhao.ToString());
            //foreach (DataRow dr in drs)
            //{
            //    dta2.Rows.Add(dr.ItemArray);
            //}
            //ds.Tables["result"].Merge(dta2, true, MissingSchemaAction.Add);
            StringBuilder commandtxt = new StringBuilder(256);
            commandtxt.Append("select * from shebeixinxi ");
            commandtxt.Append("left JOIN shiyongqingkuang ON (shebeixinxi.yiqibianhao = shiyongqingkuang.yiqibianhao) ");
            commandtxt.Append("WHERE shebeixinxi.yiqibianhao = ");
            commandtxt.Append(yiqibianhao.ToString());
            da = new OleDbDataAdapter(commandtxt.ToString(), conn);
            da.Fill(ds, Form1.bm3);
            Program.mainform.D_Print(Form1.bm3);

        }

        private void Search(string fenleihao, string mingcheng, string xinghao, string guige, string danjia, string changjia, string chuchangriqi, string chuchanghao, string gouzhiriqi, string jingfeikemu, string shiyongfangxiang, string shebeijingshouren, string lingyongren, string zichanleibie, string rukushijian, string xianzhuang, string shebeibeizhu, string cunfangdi, string dangqianshiyongren, string shiyongshijian, string jingshouren, string beizhu, string suoshubumen)
        {
            StringBuilder commandtxt = new StringBuilder(256);
            commandtxt.Append("select * from shebeixinxi ");
            commandtxt.Append("left JOIN shiyongqingkuang ON (shebeixinxi.yiqibianhao = shiyongqingkuang.yiqibianhao) WHERE 1 = 1");

            if (fenleihao.Trim() != "")
            {
                commandtxt.Append(" AND shebeixinxi.fenleihao = ");
                commandtxt.Append(fenleihao.Trim());
            }

            if (mingcheng.Trim() != "")
            {
                commandtxt.Append(" AND shebeixinxi.mingcheng = '");
                commandtxt.Append(mingcheng.Trim() + "'");
            }

            if (xinghao.Trim() != "")
            {
                commandtxt.Append(" AND shebeixinxi.xinghao = '");
                commandtxt.Append(xinghao.Trim() + "'");
            }

            if (guige.Trim() != "")
            {
                commandtxt.Append(" AND shebeixinxi.guige = '");
                commandtxt.Append(guige.Trim() + "'");
            }

            if (danjia.Trim() != "")
            {
                commandtxt.Append(" AND shebeixinxi.danjia = ");
                commandtxt.Append(danjia.Trim());
            }

            if (changjia.Trim() != "")
            {
                commandtxt.Append(" AND shebeixinxi.changjia = '");
                commandtxt.Append(changjia.Trim() + "'");
            }

            if (chuchangriqi.Trim() != "")
            {
                commandtxt.Append(" AND shebeixinxi.chuchangriqi = ");
                TimeSpan ts = Convert.ToDateTime(chuchangriqi.Trim()) - Program.standardday;
                commandtxt.Append(ts.Days);
            }

            if (chuchanghao.Trim() != "")
            {
                commandtxt.Append(" AND shebeixinxi.chuchanghao = '");
                commandtxt.Append("2013/7/4" + "'");
            }

            if (gouzhiriqi.Trim() != "")
            {
                commandtxt.Append(" AND shebeixinxi.gouzhiriqi = ");
                commandtxt.Append(gouzhiriqi.Trim());
            }

            if (jingfeikemu.Trim() != "")
            {
                commandtxt.Append(" AND shebeixinxi.jingfeikemu = '");
                commandtxt.Append(jingfeikemu.Trim() + "'");
            }

            if (shiyongfangxiang.Trim() != "")
            {
                commandtxt.Append(" AND shebeixinxi.shiyongfangxiang = '");
                commandtxt.Append(shiyongfangxiang.Trim() + "'");
            }

            if (shebeijingshouren.Trim() != "")
            {
                commandtxt.Append(" AND shebeixinxi.jingshouren = '");
                commandtxt.Append(shebeijingshouren.Trim() + "'");
            }

            if (lingyongren.Trim() != "")
            {
                commandtxt.Append(" AND shebeixinxi.lingyongren = '");
                commandtxt.Append(lingyongren.Trim() + "'");
            }

            if (zichanleibie.Trim() != "")
            {
                commandtxt.Append(" AND shebeixinxi.zichanleibie = '");
                commandtxt.Append(zichanleibie.Trim() + "'");
            }

            if (rukushijian.Trim() != "")
            {
                commandtxt.Append(" AND shebeixinxi.rukushijian = ");
                commandtxt.Append("41493");
            }

            if (xianzhuang.Trim() != "")
            {
                commandtxt.Append(" AND shebeixinxi.xianzhuang = '");
                commandtxt.Append(xianzhuang.Trim() + "'");
            }

            if (shebeibeizhu.Trim() != "")
            {
                commandtxt.Append(" AND shebeixinxi.beizhu = '");
                commandtxt.Append(shebeibeizhu.Trim() + "'");
            }

            if (cunfangdi.Trim() != "")
            {
                commandtxt.Append(" AND shiyongqingkuang.cunfangdi = '");
                commandtxt.Append(cunfangdi.Trim() + "'");
            }

            if (dangqianshiyongren.Trim() != "")
            {
                commandtxt.Append(" AND shiyongqingkuang.dangqianshiyongren = '");
                commandtxt.Append(dangqianshiyongren.Trim() + "'");
            }

            if (shiyongshijian.Trim() != "")
            {
                commandtxt.Append(" AND shiyongqingkuang.shiyongshijian = ");
                commandtxt.Append(shiyongshijian.Trim());
            }

            if (jingshouren.Trim() != "")
            {
                commandtxt.Append(" AND shiyongqingkuang.jingshouren = '");
                commandtxt.Append(jingshouren.Trim() + "'");
            }

            if (beizhu.Trim() != "")
            {
                commandtxt.Append(" AND shiyongqingkuang.beizhu = '");
                commandtxt.Append(beizhu.Trim() + "'");
            }

            if (suoshubumen.Trim() != "")
            {
                commandtxt.Append(" AND shiyongqingkuang.suoshubumen = '");
                commandtxt.Append(  suoshubumen.Trim() + "'");
            }

            da = new OleDbDataAdapter(commandtxt.ToString(), conn);
            da.Fill(ds, Form1.bm3);
            Program.mainform.D_Print(Form1.bm3);

        }

        private bool D_Check(string fenleihao, string mingcheng, string xinghao, string guige, string danjia, string changjia, string chuchangriqi, string chuchanghao, string gouzhiriqi, string jingfeikemu, string shiyongfangxiang, string shebeijingshouren, string lingyongren, string zichanleibie, string rukushijian, string xianzhuang, string shebeibeizhu,string cunfangdi,string dangqianshiyongren,string shiyongshijian, string jingshouren, string beizhu, string suoshubumen)
        {            
            if(fenleihao.Trim() == "" && mingcheng.Trim() == "" && xinghao.Trim() == "" &&
                guige.Trim() == "" && danjia.Trim() == "" && changjia.Trim() == "" &&
                chuchangriqi.Trim() == "" && chuchanghao.Trim() == "" &&gouzhiriqi.Trim() == ""  &&
                jingfeikemu.Trim() == "" && shiyongfangxiang.Trim() == "" && shebeijingshouren.Trim() == "" &&
                lingyongren.Trim() == "" && zichanleibie.Trim() == "" &&rukushijian.Trim() == "" &&
                xianzhuang.Trim() == "" && shebeibeizhu.Trim() == "" &&cunfangdi.Trim() == "" &&
                dangqianshiyongren.Trim() == "" && shiyongshijian.Trim() == "" && jingshouren.Trim() == "" &&
                suoshubumen.Trim() == "" && beizhu.Trim() == "")
            {
                MessageBox.Show("没有输入条件");
                return false;
            }
            bool Pass = true;
            StringBuilder errtxt = new StringBuilder(1024);
            Pass = (fenleihao.Trim() == "" || Adder1.fenleihaocheck(fenleihao, errtxt, false)) && Pass;
            Pass = (mingcheng.Trim() == "" || Adder1.mingchengcheck(mingcheng, errtxt, false)) && Pass;
            Pass = (xinghao.Trim() == "" || Adder1.xinghaocheck(xinghao, errtxt, false)) && Pass;
            Pass = (guige.Trim() == "" || Adder1.guigecheck(guige, errtxt, false)) && Pass;
            Pass = (danjia.Trim() == "" || Adder1.danjiacheck(danjia, errtxt, false)) && Pass;
            Pass = (changjia.Trim() == "" || Adder1.changjiacheck(changjia, errtxt, false)) && Pass;
            Pass = (chuchangriqi.Trim() == "" || Adder1.chuchangriqicheck(chuchangriqi, errtxt, false)) && Pass;
            Pass = (chuchanghao.Trim() == "" || Adder1.chuchanghaocheck(chuchanghao, errtxt, false)) && Pass;
            Pass = (gouzhiriqi.Trim() == "" || Adder1.gouzhiriqicheck(gouzhiriqi, errtxt, false)) && Pass;
            Pass = (jingfeikemu.Trim() == "" || Adder1.jingfeikemucheck(jingfeikemu, errtxt, false)) && Pass;
            Pass = (shiyongfangxiang.Trim() == "" || Adder1.shiyongfangxiangcheck(shiyongfangxiang, errtxt, false)) && Pass;
            Pass = (shebeijingshouren.Trim() == "" || Adder1.jingshourencheck(shebeijingshouren, errtxt, false)) && Pass;
            Pass = (lingyongren.Trim() == "" || Adder1.lingyongrencheck(lingyongren, errtxt, false)) && Pass;
            Pass = (zichanleibie.Trim() == "" || Adder1.zichanleibiecheck(zichanleibie, errtxt, false)) && Pass;
            Pass = (rukushijian.Trim() == "" || Adder1.rukushijiancheck(rukushijian, errtxt, false)) && Pass;
            Pass = (xianzhuang.Trim() == "" || Adder1.xianzhuangcheck(xianzhuang, errtxt, false)) && Pass;
            Pass = (shebeibeizhu.Trim() == "" || Adder1.beizhucheck(shebeibeizhu, errtxt, false)) && Pass;
            Pass = (cunfangdi.Trim() == "" || Adder2.cunfangdicheck(cunfangdi, errtxt, false)) && Pass;
            Pass = (dangqianshiyongren.Trim() == "" || Adder2.dangqianshiyongrencheck(dangqianshiyongren, errtxt, false)) && Pass;
            Pass = (shiyongshijian.Trim() == "" || Adder2.shiyongshijiancheck(shiyongshijian, errtxt, false, Convert.ToDateTime("1920-1-1")) )&& Pass;
            Pass = (jingshouren.Trim() == "" || Adder2.jingshourencheck(jingshouren, errtxt, false)) && Pass;
            Pass = (suoshubumen.Trim() == "" || Adder2.suoshubumencheck(suoshubumen, errtxt, false)) && Pass;
            Pass = (beizhu.Trim() == "" || Adder2.beizhucheck(beizhu, errtxt, false)) && Pass;

            if (!Pass)
            {
                MessageBox.Show(errtxt.ToString());
            }
            return Pass;

        }

        private void Searcher_Load(object sender, EventArgs e)
        {

        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            StringBuilder strb = new StringBuilder();
            int bh;
            if (textBox24.Text.Trim().Trim() == "")
            {
                label49.Text = "";
                return;
            }
            try
            {
                bh = Convert.ToInt32(textBox24.Text.Trim());
            }
            catch (Exception ex)
            {
                label49.Text = "仪器编号不是整数。仪器编号应当是一个正整数\n\n";
                return;
            }

            if (bh < 0)
            {
                label49.Text ="仪器编号不是正数。仪器编号应当是一个正整数\n\n";
                return;
            }

            for(int i = ds.Tables[Form1.bm1].Rows.Count -1;i >= 0;i--)
            {
                if (bh == Convert.ToInt32(ds.Tables[Form1.bm1].Rows[i]["yiqibianhao"]))
                {
                    label49.Text = ds.Tables[0].Rows[i]["mingcheng"].ToString() + ds.Tables[0].Rows[i]["rukushijian"].ToString();
                    return;
                }
            }
            label49.Text ="不存在这条记录";
            return;
        }
    }
}
