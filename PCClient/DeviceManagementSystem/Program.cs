using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DeviceManagementSystem
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>

        //全局变量
        public static string global_Directory;
        public static Form1 mainform;
        public static readonly  DateTime standardday = new DateTime(1900, 1, 1);
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainform = new Form1();
            Application.Run(mainform );
        }
    }
}
