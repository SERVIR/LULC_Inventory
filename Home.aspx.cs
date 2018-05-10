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
using Excel = Microsoft.Office.Interop.Excel;
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
    public static void updateProblemJson(string pid,string problem,string status)
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
                d.HowToCite = cite;

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


        //COMPLETED ARRAY JSON STRING
        string STR = myString;
        STR = STR.Substring(23);
        STR = STR.TrimEnd('\r', '\n');
        STR = STR.Remove(STR.Length - 1);
        STR = STR.Remove(STR.Length - 1);

        StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/reportedProblems.js"));
        file.WriteLine("var reportedProblems = " + STR + ", " + formatted + "];");

        file.Close();

    }
    public void ImportData_Click(object sender, System.EventArgs e)
    {
        string path = ""; 
        if (FileImportData.PostedFile != null && FileImportData.PostedFile.ContentLength > 0)
        {

            string fileName = Path.GetFileName(FileImportData.PostedFile.FileName);
            string folder = Server.MapPath("~/files/");
            Directory.CreateDirectory(folder);
            FileImportData.PostedFile.SaveAs(Path.Combine(folder, fileName));
            path = Path.Combine(folder, fileName);
            try
            {
                myl.InnerHtml = "Success,images saved";
                Response.Write("Uploaded: " + fileName);

            }
            catch
            {
                myl.InnerHtml = "Operation Failed!!!";
            }
        }
        getExcelFile(path);


    }
    public void ExportData_Click(object sender, System.EventArgs e)
    {
        List<string> list = new List<string>();
        foreach (ListItem listItem in selectCtry.Items)
        {
            if (listItem.Selected)
                list.Add(listItem.Value);
        }
        generateExcelfromJSON(list);
    }
    public void UpdateData_Click(object sender, System.EventArgs e)
    {

       

        dynamic ndata = new JArray();
        StreamReader myFile = new System.IO.StreamReader(HttpContext.Current.Server.MapPath("~/js/completedArray.js"));
        string myString = myFile.ReadToEnd();
        myFile.Close();
        string STR = myString;
        STR = STR.Substring(21);
        STR = STR.TrimEnd('\r', '\n');
        STR = STR.Remove(STR.Length - 1);
        dynamic data = JArray.Parse(STR) as JArray;
        string fileName = Path.GetFileName(FileUpdateData.PostedFile.FileName);
        string folder = Server.MapPath("~/files/");
        Directory.CreateDirectory(folder);
        FileUpdateData.PostedFile.SaveAs(Path.Combine(folder, fileName));
        string path = Path.Combine(folder, fileName);
        using (StreamReader readFile = new StreamReader(path))
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
                        d.Email = rowlist[12];
                        d.PhoneNumber = rowlist[13];
                        d.HowToCite = rowlist[14];
                    }
                }
            }
        }


        JToken jt = JToken.Parse(data.ToString());
        string formatted = jt.ToString(Formatting.Indented);
        StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArray.js"));

        file.WriteLine("var completedArray = " + formatted + ";");

        file.Close();

    }
    public void DeleteData_Click(object sender, System.EventArgs e)
    {
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

        using (StreamReader readFile = new StreamReader(@"D:\test.csv"))
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

        file.Close();
    }
    //gets excel from user and populates panels
    public void getExcelFile(string path)
    {
        dynamic ndata = new JArray();
        System.IO.StreamReader myFile = new System.IO.StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArray.js"));
        string myString = myFile.ReadToEnd();
        myFile.Close();
        string STR = myString;
        STR = STR.Substring(21);
        STR = STR.TrimEnd('\r', '\n');
        STR = STR.Remove(STR.Length - 1);
        STR = STR.Remove(STR.Length - 1);

        StreamWriter file = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/js/completedArray.js"));
       
        List<List<string>> excelrows = new List<List<string>>();
        //Create COM Objects. Create a COM object for everything that is referenced
        Excel.Application xlApp = new Excel.Application();
        Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
        Excel._Worksheet xlWorksheet = (Excel._Worksheet)xlWorkbook.Sheets[1];
        Excel.Range xlRange = xlWorksheet.UsedRange;

        int rowCount = xlRange.Rows.Count;
        int colCount = xlRange.Columns.Count;
        StreamWriter efile = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/test.txt"));
        List<string> rowlist = null;
        dynamic d = null;
        List<string> uidList = new List<string>();
        
        //iterate over the rows and columns and print to the console as it appears in the file
        //excel is not zero based!!
        for (int i = 2; i <= rowCount; i++)
        {
            d = JsonConvert.DeserializeObject("{  'UID':0,'Title':'','CategoryName':'','CategoryID':[],'MapYear':'','Organization':'','NumberOfClasses':[],'DataSource':'','Status':'','ReleasedYear':0,'Notes':'','PointOfContactName':'','Email':'','PhoneNumber':'','HowToCite':'' }"); 
            rowlist = new List<string>();
            rowlist.Add(Guid.NewGuid().ToString("N").Substring(0, 19));
            for (int j = 1; j <= colCount; j++)
            {
                //new line
                if (j == 1)
                    efile.WriteLine("\r\n");

                //write the value to the console
                if (xlRange.Cells[i, j] != null ) {
                    rowlist.Add(((Excel.Range)xlRange.Cells[i, j]).Value2.ToString());
                }
            }
         
            excelrows.Add(rowlist);
            var now = DateTime.Now;
            var zeroDate = DateTime.MinValue.AddHours(now.Hour).AddMinutes(now.Minute).AddSeconds(now.Second).AddMilliseconds(now.Millisecond);
            int uniqueId = (int)(zeroDate.Ticks / 10000);
            d.UID = uniqueId;
            d.Title = rowlist[1];
            d.CategoryName = rowlist[2];
                d.CategoryID.Add(Convert.ToInt32(rowlist[3])); 
            d.MapYear = rowlist[4];
            d.Organization = rowlist[5];

            d.NumberOfClasses.Add(Convert.ToInt32(rowlist[6]) );
            d.DataSource = rowlist[7];
            d.Status = rowlist[8];
            d.ReleasedYear = Convert.ToInt32(rowlist[9]);
            d.Notes = rowlist[10];
            d.PointOfContactName = rowlist[11];
            d.Email = rowlist[12];
            d.PhoneNumber = rowlist[13];
            d.HowToCite = rowlist[14];
            foreach (var nd in ndata)
            {
                uidList.Add((nd.UID).ToString());
            }
            if(!uidList.Contains((d.UID).ToString()))
            ndata.Add(d);
        }

        for (int i = 0; i < excelrows.Count; i++)
        {
            for (int j = 0; j < excelrows[i].Count; j++)
            {
                efile.WriteLine(excelrows[i][j] + "\t");
            }
        }
        JToken jt = JToken.Parse(ndata.ToString());
        string formatted = jt.ToString(Formatting.Indented);
        string formattednew = jt.ToString(Formatting.Indented).Remove(0, 1);
        file.WriteLine("var completedArray = " + STR + ", " + formattednew + ";");

        file.Close();
        efile.Close();
        GC.Collect();
        GC.WaitForPendingFinalizers();

        //rule of thumb for releasing com objects:
        //  never use two dots, all COM objects must be referenced and released individually
        //  ex: [somthing].[something].[something] is bad

        //release com objects to fully kill excel process from running in the background
        Marshal.ReleaseComObject(xlRange);
        Marshal.ReleaseComObject(xlWorksheet);

        //close and release
        xlWorkbook.Close();
        Marshal.ReleaseComObject(xlWorkbook);

        //quit and release
        xlApp.Quit();
        Marshal.ReleaseComObject(xlApp);
    }

    public void generateExcelfromJSON(List<string> list)
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

       // var data = JsonConvert.DeserializeObject<Hashtable>(STR)["value"];
        var sb = new StringBuilder();
        sb.AppendLine("UID,Title,CategoryName,CategoryID,MapYear,Organization,NumberOfClasses,DataSource,Status,ReleasedYear,Notes,PointOfContactName,Email,PhoneNumber,HowTocite");

        foreach (var o in data)
        {
            if (list.Contains((o.CategoryName).ToString()))
                // if(o.CategoryName=="Libya")
                sb.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}",(o.UID).ToString(),(o.Title).ToString(),(o.CategoryName).ToString(),(o.CategoryID[0]).ToString(),(o.MapYear).ToString(), (o.Organization).ToString(),(o.NumberOfClasses[0]).ToString(),(o.DataSource).ToString(),(o.Status).ToString(),(o.ReleasedYear).ToString(),(o.Notes).ToString(),(o.PointOfContactName).ToString(),(o.Email).ToString(),(o.PhoneNumber).ToString(),(o.HowToCite).ToString()));
        }
        File.WriteAllText(@"D:\\test.csv", sb.ToString());
    }


}