using AjaxControlToolkit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // myl.InnerHtml = "";
       
        List<ListItem> list = new List<ListItem>();

        foreach (ListItem li in selectCtry.Items)
        {
            list.Add(li);
        }

        //sort list items alphabetically/ascending
        List<ListItem> sorted = list.OrderBy(b => b.Text).ToList();

        //empty dropdownlist
        selectCtry.Items.Clear();
        //repopulate dropdownlist with sorted items.
        foreach (ListItem li in sorted)
        {
            selectCtry.Items.Add(li);
        }

        StreamReader myFile = new StreamReader(HttpContext.Current.Server.MapPath("~/js/completedArray.js"));
        string myString = myFile.ReadToEnd();
        myFile.Close();
        //if (myString.Length == 0)
        //{
        //    StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArray.js"));
        //    file.WriteLine("var completedArray = [];");
        //    file.Close();
        //}
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
        string existing = "";
        if (STR.Length < 26)
        {
            existing = "var completedArray = [" + formatted + "];";
        }
        else
        {
            STR = STR.Substring(21);
            STR = STR.TrimEnd('\r', '\n');
            STR = STR.Remove(STR.Length - 1);
            STR = STR.Remove(STR.Length - 1);
            existing = "var completedArray = " + STR + ", " + formatted + "];";
        }
        StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArray.js"));
        file.WriteLine(existing);

        file.Close();

    }
    [System.Web.Services.WebMethod]
    public static void updateProblemJson(string pid,string problem,string status,string email)
    {
        System.IO.StreamReader myFile =
             new System.IO.StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/js/reportedProblems.js"));
        string myString = myFile.ReadToEnd();
        myFile.Close();
        Page page = (Page)HttpContext.Current.Handler;
        string STR = myString;
        STR = STR.Substring(23);
        STR = STR.TrimEnd('\r', '\n');
        STR = STR.Remove(STR.Length - 1);
        dynamic data = JArray.Parse(STR) as JArray;
        string tt = "";
        foreach (dynamic d in data)
        {
            if (d.PID == pid)
            {
                d.Problem = problem;
                d.Status = status;
                d.reportedBy = email;

            }

        }

        JToken jt = JToken.Parse(data.ToString());
        string formatted = jt.ToString(Formatting.Indented);

        StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/reportedProblems.js"));
        file.WriteLine("var reportedProblems = " + formatted + ";");

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
    public static void updateExistingData(string uid, string title, string mapyear, string org, int cls, string ds, string status, int release, string notes, string poc, string email, string phnum, string cite,string lub,string lut)
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
                d.POCEmail = email;
                d.POCPhoneNumber = phnum;
                d.HowToCite = cite;
                d.LastUpdatedBy = lub;
                d.LastUpdatedTime = lut;
            }

        }
        JToken jt = JToken.Parse(data.ToString());
        string formatted = jt.ToString(Formatting.Indented);

        StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArray.js"));
        file.WriteLine("var completedArray = " + formatted + ";");

        file.Close();

    }


    [System.Web.Services.WebMethod]
    public static void DeleteFromTemp(string uid)
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
            if ((d.UID).ToString() == uid)
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
    public static void DeleteFromnewlyAddedData(string uid)
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
            if ((d.UID).ToString() == uid)
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

    public static void DeleteFromOriginal(string uid)
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
    [System.Web.Services.WebMethod]
    public static void updateProblemsJson(string s)
    {
        System.IO.StreamReader myFile =
       new System.IO.StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/js/reportedProblems.js"));
        string myString = myFile.ReadToEnd();
        myFile.Close();
        Page page = (Page)HttpContext.Current.Handler;

        JToken jt = JToken.Parse(s);
        string formatted = jt.ToString(Formatting.Indented);
        string existing = "";

        //COMPLETED ARRAY JSON STRING
        string STR = myString;
        if (STR.Length <26)
        {
            existing = "var reportedProblems = [" + formatted + "];";
        }
        else
        {
            STR = STR.Substring(23);
            STR = STR.TrimEnd('\r', '\n');
            STR = STR.Remove(STR.Length - 1);
            STR = STR.Remove(STR.Length - 1);
            existing = "var reportedProblems = " + STR + ", " + formatted + "];";
        }
           

        StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/reportedProblems.js"));
       
        file.WriteLine(existing);

        file.Close();

    }
    protected void AjaxFileUpload1_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        //myl.Text = "started Imported Data!";
        string fileNametoupload = Server.MapPath("~/files/") + e.FileName.ToString();
 //       AjaxFileUpload afu=(AjaxFileUpload)(importDataForm.FindControl("AjaxFileUpload1"));
        //afu.SaveAs(fileNametoupload);
     //  getExcelFile(fileNametoupload, foruseremail.Value, forusertime.Value);
       
    }
   
    public void ExportData_Click(object sender, System.EventArgs e)
    {
        List<string> list = new List<string>();
        foreach (ListItem listItem in selectCtry.Items)
        {
            if (listItem.Selected)
                list.Add(listItem.Value);
        }
       // generateExcelfromJSON(list);
    }
    [System.Web.Services.WebMethod]
    public static string UpdateData(string path,string email,string time)
    {
        Page page = (Page)HttpContext.Current.Handler;


        //  FileUpload fupload=(FileUpload)  Page.FindControl("FileUpdateData")
        dynamic ndata = new JArray();
        StreamReader myFile = new System.IO.StreamReader(HttpContext.Current.Server.MapPath("~/js/completedArray.js"));
        string myString = myFile.ReadToEnd();
        myFile.Close();
        string STR = myString;
        STR = STR.Substring(21);
        STR = STR.TrimEnd('\r', '\n');
        STR = STR.Remove(STR.Length - 1);
        dynamic data = JArray.Parse(STR) as JArray;
      //  string fileName = Path.GetFileName(FileUpdateData.PostedFile.FileName);
        string folder = page.Server.MapPath("~/");
        Directory.CreateDirectory(folder);
      //  FileUpdateData.PostedFile.SaveAs(Path.Combine(folder, fileName));
      //  string path = Path.Combine(folder, fileName);
        using (StreamReader readFile = new StreamReader(Path.Combine(folder, path)))
        {
            string line;
            string[] rowlist;
            
            while ((line = readFile.ReadLine()) != null)
            {
                rowlist = line.Split(',');
                foreach (var d in data)
                {
                    //write the value to the console
                    if (rowlist[0] == (d.UID).ToString())
                    {


                        d.UID = rowlist[0];
                        d.Title = rowlist[1];
                        d.CategoryName = rowlist[2];
                        d.CategoryID[0]=(Convert.ToInt32(rowlist[3]));
                        d.MapYear = rowlist[4];
                        d.Organization = rowlist[5];

                        d.NumberOfClasses[0]=(Convert.ToInt32(rowlist[6]));
                        d.DataSource = rowlist[7];
                        d.Status = rowlist[8];
                        d.ReleasedYear = Convert.ToInt32(rowlist[9]);
                        d.Notes = rowlist[10];
                        d.PointOfContactName = rowlist[11];
                        d.POCEmail = rowlist[12];
                        d.POCPhoneNumber = rowlist[13];
                        d.HowToCite = rowlist[14];
                        d.LastUpdatedBy = email;
                        d.LastUpdatedTime = time;
                    }
                }
            }
        }


        JToken jt = JToken.Parse(data.ToString());
        string formatted = jt.ToString(Formatting.Indented);
        StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArray.js"));

        file.WriteLine("var completedArray = " + formatted + ";");
        string script = "completedArray = " + formatted + ";";
        file.Close();
        return script;
    }
    [System.Web.Services.WebMethod]
    public static string DeleteData(string path)
    {
        Page page = (Page)HttpContext.Current.Handler;

        dynamic ndata = new JArray();
        StreamReader myFile = new System.IO.StreamReader(HttpContext.Current.Server.MapPath("~/js/completedArray.js"));
        string myString = myFile.ReadToEnd();
        myFile.Close();
        string STR = myString;
        STR = STR.Substring(21);
        STR = STR.TrimEnd('\r', '\n');
        STR = STR.Remove(STR.Length - 1);
        dynamic data = JArray.Parse(STR) as JArray;
        List<string> uidList = new List<string>();
        string folder = page.Server.MapPath("~/");
        using (StreamReader readFile = new StreamReader(Path.Combine(folder, path)))
        {
            string line;
            string[] rowlist;

            while ((line = readFile.ReadLine()) != null)
            {

                rowlist = line.Split(',');
                uidList.Add(rowlist[0]);
            }
        }
        foreach (var ndd in data)
        {
            if (uidList.Contains((ndd.UID).ToString()))
            {

            }
            else
            {
                ndata.Add(ndd);
            }
            
            
        }


        JToken jt = JToken.Parse(ndata.ToString());
        string formatted = jt.ToString(Formatting.Indented);
        StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArray.js"));

        file.WriteLine("var completedArray = " + formatted + ";");
        string script = "completedArray = " + formatted + ";";
        file.Close();
        return script;
    }
    //gets excel from user and populates panels
    [System.Web.Services.WebMethod]
    public static object getExcelFile(string path,string email, string time)
    {

        Page page = (Page)HttpContext.Current.Handler;
        string folder = page.Server.MapPath("~/files/");
         
        dynamic ndata = new JArray();
        System.IO.StreamReader myFile = new System.IO.StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArray.js"));
        string myString = myFile.ReadToEnd();
        myFile.Close();
        string STR = myString;


        StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArray.js"));

        List<List<string>> excelrows = new List<List<string>>();

        StreamWriter efile = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/test.txt"));
        // List<string> rowlist = null;
        dynamic d = null;
        List<string> uidList = new List<string>();
        using (StreamReader readFile = new StreamReader(Path.Combine(folder,path)))
        {
            // string line;
            //   string[] rowlist;
            //  string[] rowlist;

            var contents = File.ReadAllText(Path.Combine(folder, path)).Split('\n');
            var rowlistb4 = from line in contents select line.Split(',').ToArray();
            foreach (var rowlist in rowlistb4.Skip(1).TakeWhile(r => r.Length > 1 && r.Last().Trim().Length > 0))
            {


                d = JsonConvert.DeserializeObject("{  'UID':0,'Title':'','CategoryName':'','CategoryID':[],'MapYear':'','Organization':'','NumberOfClasses':[],'DataSource':'','Status':'','ReleasedYear':0,'Notes':'','PointOfContactName':'','POCEmail':'','POCPhoneNumber':'','HowToCite':'' }");
                var now = DateTime.Now;
                var zeroDate = DateTime.MinValue.AddHours(now.Hour).AddMinutes(now.Minute).AddSeconds(now.Second).AddMilliseconds(now.Millisecond);
                int uniqueId = (int)(zeroDate.Ticks / 10000);
                d.UID = Guid.NewGuid().ToString("N").Substring(0, 19);
                d.Title = rowlist[0];
                d.CategoryName = rowlist[1];
                d.CategoryID.Add(Convert.ToInt32(rowlist[2]));
                d.MapYear = rowlist[3];
                d.Organization = rowlist[4];

                d.NumberOfClasses.Add(Convert.ToInt32(rowlist[5]));
                d.DataSource = rowlist[6];
                d.Status = rowlist[7];
                d.ReleasedYear = Convert.ToInt32(rowlist[8]);
                d.Notes = rowlist[9];
                d.PointOfContactName = rowlist[10];
                d.POCEmail = rowlist[11];
                d.POCPhoneNumber = rowlist[12];
                d.HowToCite = rowlist[13].Trim().Replace("\r", string.Empty); ;
                d.LastUpdatedBy = email;
                d.LastUpdatedTime = time;


                foreach (var nd in ndata)
                {
                    uidList.Add((nd.UID).ToString());
                }
                if (!uidList.Contains((d.UID).ToString()))
                    ndata.Add(d);
            }
        }

        JToken jt = JToken.Parse(ndata.ToString());
        string formatted = jt.ToString(Formatting.Indented);
        string formattednew = jt.ToString(Formatting.Indented).Remove(0, 1);
        string existing = "";
        string script = "";
        if (STR.Length < 26)
        {
            existing = "var completedArray = [" + formattednew + ";";
            script = "completedArray=[" + formattednew + ";";
        }
        else
        {
            STR = STR.Substring(21);
            STR = STR.TrimEnd('\r', '\n');
            STR = STR.Remove(STR.Length - 1);
            STR = STR.Remove(STR.Length - 1);
            existing = "var completedArray = " + STR + ", " + formattednew + ";";
            script = "completedArray=" + STR + ", " + formattednew + "; ";
        }
        file.WriteLine(existing);



        file.Close();
        efile.Close();

        return script;
    }

    [System.Web.Services.WebMethod]
    public static void generateExcelfromJSON(string[] listp)
    {
        List<string> list = listp.ToList();
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

        var sb = new StringBuilder();
        sb.AppendLine("UID,Title,CountryName,CountryID,MapYear,Organization,NumberOfClasses,DataSource,Status,ReleasedYear,Notes,PointOfContactName,POCEmail,POCPhoneNumber,HowTocite");
     
        foreach (var o in data)
        {
            if (list.Contains((o.CategoryName).ToString()))
            {
                // if(o.CategoryName=="Libya")
                if ((o.MapYear).ToString() == "")
                    o.MapYear = "0000";
                if (o.Organization.ToString() == "")
                    o.Organization = "Not specified";
                if (o.NumberOfClasses.ToString() == "")
                    o.NumberOfClasses = "0";
                if (o.DataSource.ToString() == "")
                    o.DataSource = "Not specified";
                if (o.Notes.ToString() == "")
                    o.Notes = "Not specified";
                if (o.POCPhoneNumber.ToString() == "")
                    o.POCPhoneNumber = "000-000-0000";
                if (o.HowToCite.ToString() == "")
                    o.HowToCite = "Not specified";
                if ((o.POCEmail).ToString() == "")
                    o.POCEmail = "test@test.com";
                if ((o.Status).ToString() == "")
                    o.Status = "Completed";
                if ((o.PointOfContactName).ToString() == "")
                    o.PointOfContactName = "Not specified";
                sb.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}", (o.UID).ToString(), (o.Title).ToString(), (o.CategoryName).ToString(), (o.CategoryID[0]).ToString(), (o.MapYear).ToString(), (o.Organization).ToString(), (o.NumberOfClasses[0]).ToString(), (o.DataSource).ToString(), (o.Status).ToString(), (o.ReleasedYear).ToString(), (o.Notes).ToString(), (o.PointOfContactName).ToString(), (o.POCEmail).ToString(), (o.POCPhoneNumber).ToString(), (o.HowToCite).ToString()));
            } }
       
        File.WriteAllText(page.Server.MapPath("~/files/GeneratedLULCData.csv"), sb.ToString());
    }


}