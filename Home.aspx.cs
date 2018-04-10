using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Guid changer = Guid.NewGuid();
        string jsfile = "js/completedArray.js?c=" + changer.ToString();

        HtmlGenericControl child = new HtmlGenericControl("script");
        child.Attributes.Add("type", "text/javascript");
        child.Attributes.Add("src", jsfile);
        Page.Header.Controls.Add(child);
        Guid changer1 = Guid.NewGuid();

        string jsfile1 = "js/completedArrayTemp.js?c=" + changer1.ToString();

        HtmlGenericControl child1 = new HtmlGenericControl("script");
        child1.Attributes.Add("type", "text/javascript");
        child1.Attributes.Add("src", jsfile1);
        Page.Header.Controls.Add(child1);
        Guid changer2 = Guid.NewGuid();

        string jsfile2 = "js/newlyAddedData.js?c=" + changer2.ToString();

        HtmlGenericControl child2 = new HtmlGenericControl("script");
        child2.Attributes.Add("type", "text/javascript");
        child2.Attributes.Add("src", jsfile2);
        Page.Header.Controls.Add(child2);
    }

    [System.Web.Services.WebMethod]
    public static void updateJson(string s)
    {
        System.IO.StreamReader myFile =
        new System.IO.StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArray.js"));
        string myString = myFile.ReadToEnd();
        myFile.Close();
        Page page = (Page)HttpContext.Current.Handler;

        JToken jt = JToken.Parse(s);
        string formatted = jt.ToString(Formatting.Indented);


        //COMPLETED ARRAY JSON STRING
        string STR = myString;
        STR = STR.Substring(21);
        STR = STR.TrimEnd('\r', '\n');
        STR = STR.Remove(STR.Length - 1);
        STR = STR.Remove(STR.Length - 1);

        StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArray.js"));
        file.WriteLine("var completedArray = " + STR + ", " + formatted + "];");

        file.Close();

    }

    [System.Web.Services.WebMethod]
    public static void updateJsonTemp(string s)
    {
        System.IO.StreamReader myFile =
        new System.IO.StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArrayTemp.js"));
        string myString = myFile.ReadToEnd();
        myFile.Close();
        Page page = (Page)HttpContext.Current.Handler;

        JToken jt = JToken.Parse(s);
        string formatted = jt.ToString(Formatting.Indented);


        //COMPLETED ARRAY JSON STRING
        string STR = myString;
        STR = STR.Substring(25);
        STR = STR.TrimEnd('\r', '\n');
        STR = STR.Remove(STR.Length - 1);
        STR = STR.Remove(STR.Length - 1);

        StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArrayTemp.js"));
        file.WriteLine("var completedArrayTemp = " + STR + ", " + formatted + "];");

        file.Close();

    }
    [System.Web.Services.WebMethod]
    public static void updateUserJson(string s)
    {
        System.IO.StreamReader myFile =
        new System.IO.StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/js/users.js"));
        string myString = myFile.ReadToEnd();
        myFile.Close();
        Page page = (Page)HttpContext.Current.Handler;

        JToken jt = JToken.Parse(s);
        string formatted = jt.ToString(Formatting.Indented);


        //COMPLETED ARRAY JSON STRING
        string STR = myString;
        STR = STR.Substring(12);
        STR = STR.TrimEnd('\r', '\n');
        STR = STR.Remove(STR.Length - 1);
        STR = STR.Remove(STR.Length - 1);
        StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/users.js"));
        file.WriteLine("var emails = " + STR + ", " + formatted + "];");

        file.Close();

    }

    [System.Web.Services.WebMethod]
    public static void updateNewlyAddedData(int flag, string s)
    {
        System.IO.StreamReader myFile =
        new System.IO.StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/js/newlyAddedData.js"));
        string myString = myFile.ReadToEnd();
        myFile.Close();
        Page page = (Page)HttpContext.Current.Handler;

        JToken jt = JToken.Parse(s);
        string formatted = jt.ToString(Formatting.Indented);


        //COMPLETED ARRAY JSON STRING
        string STR = myString;
        STR = STR.Substring(16);
        STR = STR.TrimEnd('\r', '\n');
        STR = STR.Remove(STR.Length - 1);
        STR = STR.Remove(STR.Length - 1);
  
        StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/newlyAddedData.js"));
        if (flag == 1)
            file.WriteLine("var newlyAdded = " + STR + " " + formatted + "];");
        else
            file.WriteLine("var newlyAdded = " + STR + ", " + formatted + "];");

        file.Close();


    }

    [System.Web.Services.WebMethod]
    public static void updateExistingData(int uid, string title, string mapyear, string org, int cls, string ds, string status, int release, string notes, string poc, string email, string phnum, string cite)
    {
        System.IO.StreamReader myFile =
             new System.IO.StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArray.js"));
        string myString = myFile.ReadToEnd();
        myFile.Close();
        Page page = (Page)HttpContext.Current.Handler;
        string STR = myString;
        STR = STR.Substring(21);
        STR = STR.TrimEnd('\r', '\n');
        STR = STR.Remove(STR.Length - 1);
        dynamic data = JArray.Parse(STR) as JArray;
        string tt = "";
        foreach (dynamic d in data)
        {
            if (d.UID == uid)
            {
                d.Title = title;
                d.MapYear = mapyear;
                d.Organization = org;
                d.NumberOfClasses = cls;
                d.DataSource = ds;
                d.Status = status;
                d.ReleasedYear = release;
                d.Notes = notes;
                d.PointOfContactName = poc;
                d.Email = email;
                d.PhoneNumber = phnum;
                d.HowTocite = cite;

            }

        }
        JToken jt = JToken.Parse(data.ToString());
        string formatted = jt.ToString(Formatting.Indented);

        StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArray.js"));
        file.WriteLine("var completedArray = " + formatted + ";");

        file.Close();

    }


    [System.Web.Services.WebMethod]
    public static void DeleteFromTemp(int uid)
    {
        System.IO.StreamReader myFile =
             new System.IO.StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArrayTemp.js"));
        string myString = myFile.ReadToEnd();
        myFile.Close();
        Page page = (Page)HttpContext.Current.Handler;
        string STR = myString;
        STR = STR.Substring(25);
        STR = STR.TrimEnd('\r', '\n');
        STR = STR.Remove(STR.Length - 1);
        dynamic data = JArray.Parse(STR) as JArray;
        dynamic ndata=new JArray();
        foreach (var d in data)
        {
            if (d.UID == uid)
            {
            }
            else
                ndata.Add(d);

        }
        JToken jt = JToken.Parse(ndata.ToString());
        string formatted = jt.ToString(Formatting.Indented);

        StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArrayTemp.js"));
        file.WriteLine("var completedArrayTemp = " + formatted + ";");

        file.Close();

    }
    [System.Web.Services.WebMethod]
    public static void DeleteFromnewlyAddedData(int uid)
    {
        System.IO.StreamReader myFile =
             new System.IO.StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/js/newlyAddedData.js"));
        string myString = myFile.ReadToEnd();
        myFile.Close();
        Page page = (Page)HttpContext.Current.Handler;
        string STR = myString;
        STR = STR.Substring(16);
        STR = STR.TrimEnd('\r', '\n');
        STR = STR.Remove(STR.Length - 1);
        dynamic data = JArray.Parse(STR) as JArray;
        dynamic ndata = new JArray();
        foreach (var d in data)
        {
            if (d.UID == uid)
            {
            }
            else
                ndata.Add(d);

        }
        JToken jt = JToken.Parse(ndata.ToString());
        string formatted = jt.ToString(Formatting.Indented);


        StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/newlyAddedData.js"));
        file.WriteLine("var newlyAdded = " + formatted + ";");

        file.Close();

    }
    [System.Web.Services.WebMethod]

    public static void DeleteFromOriginal(int uid)
    {
        System.IO.StreamReader myFile =
             new System.IO.StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArray.js"));
        string myString = myFile.ReadToEnd();
        myFile.Close();
        Page page = (Page)HttpContext.Current.Handler;
        string STR = myString;
        STR = STR.Substring(21);
        STR = STR.TrimEnd('\r', '\n');
        STR = STR.Remove(STR.Length - 1);
        dynamic data = JArray.Parse(STR) as JArray;
        dynamic ndata = new JArray();
        foreach (var d in data)
        {
            if (d.UID == uid)
            {
            }
            else
                ndata.Add(d);

        }
        JToken jt = JToken.Parse(ndata.ToString());
        string formatted = jt.ToString(Formatting.Indented);
        //StreamWriter file1 = new StreamWriter(@"D:\what.txt");
        //file1.WriteLine("var completedArray = " + formatted + ";");
        //file1.Close();

        StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArray.js"));
        file.WriteLine("var completedArray = " + formatted + ";");

        file.Close();

    }
}