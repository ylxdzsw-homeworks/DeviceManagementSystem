using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

public enum Level{LOG,ERROR,WARNING,EXCEPTION}

/// <summary>
/// 用于Debug的一些静态函数所在的类
/// </summary>
public static class Debug
{
    private static string defaultlogfile = AppDomain.CurrentDomain.BaseDirectory + "Log/Debuglogs.txt";
    /// <summary>
    /// 向记录文件输出Debug信息
    /// </summary>
    public static void log(string content, string logfile, Level level = Level.LOG)
    {
        string prefix;
        switch(level)
        {
            case Level.LOG:
                prefix = "log - ";
                break;
            case Level.ERROR:
                prefix = "error - ";
                break;
            case Level.WARNING:
                prefix = "warning - ";
                break;
            case Level.EXCEPTION:
                prefix = "exception - ";
                break;
            default:
                prefix = "other - ";
                break;
        }
        StreamWriter sw = new StreamWriter(logfile,true);
        sw.WriteLine(prefix + content + " - " + DateTime.Now.ToString());
        sw.Flush();
        sw.Close();
    }
    public static void log(string content, Level level = Level.LOG)
    {
        log(content,defaultlogfile,level);
    }
    public static void setdefaultlogfile(string newlogfile)
    {
        if(File.Exists(newlogfile))
            defaultlogfile = newlogfile;
        else
            log("File not exist! (Debug.cs - 3741)",Level.ERROR);
    }
}