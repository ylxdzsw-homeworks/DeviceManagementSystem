using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.Helpers;
using System.Data;
using WebMatrix.Data;
using System.Linq.Expressions;
using System.Linq;
/// <summary>
/// DBtool总接口
/// </summary>
public static class DBtool
{
    //===========variables===========
    public static Database ddb
    {
        get{return Database.Open("Device");}
    }
    internal static Database adb
    {
        get{return Database.Open("Application");}
    }
    //============init================
    /// <summary>
    /// 初始化DBtool
    /// </summary>
    public static void init()
    {
        try{
            DBdict.readdict(adb.Query("SELECT * FROM Devicecolnamedict"));
        }catch(Exception err){
            Debug.log("Database initialize failed (DBtool.cs - 7591)",Level.ERROR);
        }
        Debug.log("Database initialized successfully! (DBtool.cs - 4028)",Level.LOG);
    }

    //============NameDict============
    public static Metaname getmetaname(object atom)
    {
        return DBdict.getmetaname(atom.ToString());
    }
    public static int getid(object atom)
    {
        return DBdict.getid(atom.ToString());
    }
    public static string getcn(object atom)
    {
        return DBdict.getcn(atom.ToString());
    }
    public static string geten(object atom)
    {
        return DBdict.geten(atom.ToString());
    }
    public static string getnum(object atom)
    {
        return DBdict.getnum(atom.ToString());
    }
    public static string[] namelist
    {
        get
        {
            return DBdict.namelist;
        }
    }
    public static Metaname[] metanamelist
    {
        get
        {
            return DBdict.metanamelist;
        }
    }

    //============interact============
    public static bool insert(Recordrow rr,string username)
    {
        var a = DBinteractor.insertDeviceInfo(rr);
        var b = DBinteractor.GetLastInsertId();
        rr.add("c19",b);
        rr.add("c27",b);
        var c = DBinteractor.insertDeviceStatus(rr);
        rr.makeDataInfo(DateTime.Now.ToString(),username,DateTime.Now.ToString(),username);
        var d = DBinteractor.insertDataInfo(rr);
        return a;
    }
    public static int lastid
    {
        get
        {
            return DBinteractor.GetLastInsertId();
        }
    }
    public static string queryPassword(object name)
    {
        return DBinteractor.queryPassword(name.ToString());
    }
    public static string addTag(object id,object tag)
    {
        if(id == null || tag == null || id.ToString().Trim() == "" || tag.ToString().Trim() == ""){Debug.log("添加标签时错误 (DBtool.cshtml - 0841)",Level.WARNING);return "err";}
        return DBinteractor.addTag(id.ToString().Trim(),tag.ToString().Trim());
    }
    public static string removeTag(object id,object tag)
    {
        if(id == null || tag == null || id.ToString().Trim() == "" || tag.ToString().Trim() == ""){Debug.log("删除标签时错误 (DBtool.cshtml - 0191)",Level.WARNING);return "err";}
        return DBinteractor.removeTag(id.ToString().Trim(),tag.ToString().Trim());
    }
    public static string queryAuth(object username)
    {
        return DBinteractor.queryAuth(username.ToString().Trim());
    }
}
/// <summary>
/// 利用SQL语句与数据库进行交互
/// </summary>
static class DBinteractor
{
    public static bool insertDeviceInfo(Recordrow rr)
    {
        if(!rr.okInfo)
        {
            Debug.log("Not prepared to insert (DBtool.cs - 9526)",Level.EXCEPTION);
            return false;
        }

        StringBuilder sqlbuilderf = new StringBuilder("INSERT INTO DeviceInfo (");
        StringBuilder sqlbuildera = new StringBuilder("VALUES (");

        foreach(var a in rr.content)
        {
            var b = a.Key.en.Split('.');
            if(!(b[0] == "DeviceInfo"))
            {
                continue;
            }
            sqlbuilderf.Append(b[1]);
            sqlbuilderf.Append(", ");
            sqlbuildera.Append("'");
            sqlbuildera.Append(a.Value.ToString());
            sqlbuildera.Append("', ");
        }

        sqlbuilderf.Remove(sqlbuilderf.Length-2,2);
        sqlbuilderf.Append(") ");
        sqlbuildera.Remove(sqlbuildera.Length-2,2);
        sqlbuildera.Append(")");

        var sqlstr = sqlbuilderf.ToString() + sqlbuildera.ToString();
        Debug.log("try to excuse \"" + sqlstr +"\" (DBtool.cs - 0175)");
    
        DBtool.ddb.Execute(sqlstr);
        return true;
    }
        
    public static bool insertDeviceStatus(Recordrow rr)
    {
        if(!rr.okStatus)
        {
            Debug.log("Not prepared to insert (DBtool.cs - 2541)",Level.EXCEPTION);
            return false;
        }

        StringBuilder sqlbuilderf = new StringBuilder("INSERT INTO Devicestatus (");
        StringBuilder sqlbuildera = new StringBuilder("VALUES (");

        foreach(var a in rr.content)
        {
            var b = a.Key.en.Split('.');
            if(!(b[0] == "DeviceStatus"))
            {
                continue;
            }
            sqlbuilderf.Append(b[1]);
            sqlbuilderf.Append(", ");
            sqlbuildera.Append("'");
            sqlbuildera.Append(a.Value.ToString());
            sqlbuildera.Append("', ");
        }

        sqlbuilderf.Remove(sqlbuilderf.Length-2,2);
        sqlbuilderf.Append(") ");
        sqlbuildera.Remove(sqlbuildera.Length-2,2);
        sqlbuildera.Append(")");

        var sqlstr = sqlbuilderf.ToString() + sqlbuildera.ToString();
        Debug.log("try to excuse \"" + sqlstr +"\" (DBtool.cs - 6012)");
    
        DBtool.ddb.Execute(sqlstr);
        return true;
    }

    public static bool insertDataInfo(Recordrow rr)
    {
        if(!rr.okDataInfo)
        {
            Debug.log("Not prepared to insert (DBtool.cs - 1269)",Level.EXCEPTION);
            return false;
        }

        StringBuilder sqlbuilderf = new StringBuilder("INSERT INTO DataInfo (");
        StringBuilder sqlbuildera = new StringBuilder("VALUES (");

        foreach(var a in rr.content)
        {
            var b = a.Key.en.Split('.');
            if(!(b[0] == "DataInfo"))
            {
                continue;
            }
            sqlbuilderf.Append(b[1]);
            sqlbuilderf.Append(", ");
            sqlbuildera.Append("'");
            sqlbuildera.Append(a.Value.ToString());
            sqlbuildera.Append("', ");
        }

        sqlbuilderf.Remove(sqlbuilderf.Length-2,2);
        sqlbuilderf.Append(") ");
        sqlbuildera.Remove(sqlbuildera.Length-2,2);
        sqlbuildera.Append(")");

        var sqlstr = sqlbuilderf.ToString() + sqlbuildera.ToString();
        Debug.log("try to excuse \"" + sqlstr +"\" (DBtool.cs - 6138)");
    
        DBtool.ddb.Execute(sqlstr);
        return true;
    }

    public static int GetLastInsertId()
    {
        var a = DBtool.ddb.Query("SELECT top 1 * FROM DeviceInfo ORDER BY Id desc");
        var b = a.GetEnumerator();
        var c = 0;
        while(b.MoveNext()){
            c = b.Current.id;
        }
        return c;
    }

    public static string queryPassword(string name)
    {
        var a = DBtool.adb.Query("SELECT * FROM UserInfo WHERE UserName = '"+name + "'");
        var b = a.GetEnumerator();
        var c = "";
        while(b.MoveNext()){
            c = b.Current.Password;
        }
        if(c == ""){return null;}
        return c;
    }

    public static string addTag(string id,string tag)
    {
        var oldtag = getTags(id);
        if(oldtag == null){
            DBtool.ddb.Execute("UPDATE DataInfo SET tag = ' " + tag +"' WHERE Id = " + id);
            return "ok";
        }
        var newtag = "";
        foreach(var i in oldtag)
        {
            if(i.Trim() == ""){continue;}
            newtag = newtag + " " + i;
        }
        newtag = newtag + " " + tag;
        DBtool.ddb.Execute("UPDATE DataInfo SET tag = ' " + newtag +"' WHERE Id = " + id);
        return "ok";
    }

    public static string removeTag(string id, string tag)
    {
        var oldtag = getTags(id);
        if(oldtag == null){
            return "ok";
        }
        var newtag = "";
        foreach(var i in oldtag)
        {
            if(i.Trim() != "" && i.Trim() != tag){
                newtag = newtag + " " + i;
            }
        }
        DBtool.ddb.Execute("UPDATE DataInfo SET tag = ' " + newtag +"' WHERE Id = " + id);
        return "ok";
    }

    public static string[] getTags(string id)
    {
        var a = DBtool.ddb.Query("SELECT * FROM DataInfo WHERE Id = "+ id);
        var b = a.GetEnumerator();
        var c = "";
        while(b.MoveNext()){
            c = b.Current.Tag;
        }
        if(c == ""){return null;}
        return c.Split(' ');
    }

    public static string queryAuth(string username)
    {
        var a = DBtool.adb.Query("SELECT * FROM UserInfo WHERE UserName = '"+ username + "'");
        var b = a.GetEnumerator();
        var c = "";
        while(b.MoveNext()){
            c = b.Current.Auth;
        }
        if(c == ""){return null;}
        return c;
    }
}
/// <summary>
/// 词典操作类
/// </summary>
static class DBdict
{
    static Metaname[] namedict;
    /// <summary>
    /// 从表中读取词典信息并存在静态变量DBdict.namedict中.
    /// </summary>
    public static void readdict(IEnumerable<dynamic> namedata)
    {
        List<Metaname> lm = new List<Metaname>(26);
        IEnumerator<dynamic> a = namedata.GetEnumerator();
        while(a.MoveNext())
        {
            Metaname nm = new Metaname();
            nm.id = (int)a.Current["Id"];
            nm.num = (string)a.Current["num"];
            nm.cn = (string)a.Current["cn"];
            nm.en = (string)a.Current["en"];
            lm.Add(nm);
        }
        namedict = lm.ToArray();
    }
    public static Metaname getmetaname(string atom)
    {
        var a = from aa in namedict where aa.id.ToString() == atom || aa.cn == atom || aa.en == atom || aa.num == atom select aa;
        return a.ElementAt(0);
    }
    public static int getid(string atom)
    {
        var a = getmetaname(atom);
        return a.id;
    }
    public static string getcn(string atom)
    {
        var a = getmetaname(atom);
        return a.cn;
    }
    public static string geten(string atom)
    {
        var a = getmetaname(atom);
        return a.en;
    }
    public static string getnum(string atom)
    {
        var a = getmetaname(atom);
        return a.num;
    }
    public static string[] namelist
    {
        get
        {
            List<string> namelist = new List<string>();
            foreach(var a in namedict)
            {
                namelist.Add(a.cn);
            }
            return namelist.ToArray();
        }
    }
    public static Metaname[] metanamelist
    {
        get
        {
            return namedict;
        }
    }
}
/// <summary>
/// 保存字段的各个格式
/// </summary>
public struct Metaname
{
    public int id;
    public string num;
    public string cn;
    public string en;
    public Metaname(int id, string num, string cn, string en)
    {
        this.id = id;
        this.num = num;
        this.cn = cn;
        this.en = en;
    }
}
/// <summary>
/// 保存一条记录
/// </summary>
public struct Recordrow
{
    private Dictionary<Metaname,object> _content;
    private bool _right;

    private bool _complete;
    private bool _completeInfo;
    private bool _completeStatus;
    private bool _completeDataInfo;

    public Recordrow(params KeyValuePair<Metaname,object>[] contents)
    {
        _content = new Dictionary<Metaname,object>();
        foreach(var a in contents)
        {
            _content.Add(a.Key,a.Value);
        }
        _right = false;
        _complete = false;
        _completeInfo = false;
        _completeStatus = false;
        _completeDataInfo = false;
        check();
    }

    public void add(object key,object value)
    {
        add(new KeyValuePair<Metaname,object>(DBtool.getmetaname(key),value));
    }
    public void add(KeyValuePair<Metaname,object> content)
    {
        if(_content == null){_content = new Dictionary<Metaname,object>();}
        _content.Add(content.Key,content.Value);
        check();
    }
    public bool haskey(object key)
    {
        if(content.ContainsKey(DBtool.getmetaname(key))){return true;}
        return false;
    }
    public bool insert(string username)
    {
        return DBtool.insert(this,username);
    }

    public bool ok
    {
        get
        {
            return _right && _complete;
        }
    }

    public bool okInfo
    {
        get
        {
            return _completeInfo && _right;
        }
    }

    public bool okStatus
    {
        get
        {
            return _completeStatus && _right;
        }
    }

    public bool okDataInfo
    {
        get
        {
            return _completeDataInfo && _right;
        }
    }
    public Dictionary<Metaname,object> content
    {
        get
        {
            return _content;
        }
    }
    private void check()
    {
        _right = true;
        _complete = true;
        _completeStatus = true;
        _completeInfo = true;
        _completeDataInfo = true;
    }
    public void makeDataInfo(string infotime = "",string infoman = "",string statustime = "",string statusman = "")
    {
        if(infotime != ""){this.add("c28",infotime);}
        if(infoman != ""){this.add("c29",infoman);}
        if(statustime != ""){this.add("c30",statustime);}
        if(statusman != ""){this.add("c31",statusman);}
        this.add("c32"," ");
        check();
    }
}
public class chcker
{
    Recordrow rr;
    public chcker(Recordrow rr)
    {
        this.rr = rr;
    }
    public string checkcomplete()
    {
        checkDeviceInfo();
        checkDeviceStatus();
        //checkDataInfo();
        return "";
    }
    public string checkDeviceInfo()
    {
        var needed = new int[]{2,3,13,15};
        foreach(var i in needed)
        {
            if(rr.content.ContainsKey(DBtool.getmetaname("c"+i)))
            {
                continue;
            }
            return "c" + i;
        }
        return "ok";
    }
    public string checkDeviceStatus()
    {
        return "";
    }
    public string checkright()
    {
        return "";
    }
    public static string rightchecker(Metaname col,string value,bool needall)
    {
        var id = col.id;
        if(value.Trim() == ""){
            if(needall){
                if(id == 2 || id == 3 || id == 13 || id == 15 || id == 20){
                    return DBtool.getcn(id) + "不能为空";
                }
            }
            return "ok";
        }
        if(id == 8 || id == 10 || id == 15 || id == 21){
            try{
                DateTime dt = DateTime.Parse(value);
            }catch(Exception e){
                return "时间格式不正确";
            }
        }
        return "ok";
    }

}