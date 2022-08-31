using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Security.Principal;
using System.Data.Odbc;

/// <summary>
/// Summary description for ClassTools
/// </summary>
public class ClassTools
{
    // ------------------------------------------------------------

    public static int sysMode;  // 1=start, 2=preview, 3=upload, 4=import

    public static List<String> ListSpec = new List<string>();
    public static List<String> ListSpecStyle = new List<string>();
    public static List<String> ListSpecOptional = new List<string>();
    public static List<String> ListSpecNotInExcel = new List<string>();

    public static List<String> ListExcel = new List<string>();
    public static List<String> ListExcelMap = new List<string>();

    public static DataTable tableExcel = new System.Data.DataTable();

    public static string ExcelConStr = "";

    public static string TableName = "";
    public static string SpecExcelImage = "";
    public static string tableTemplateName = "";    //"Tool_Spending_Spec";
    public static string tableUploadName = "";      //"Tool_Spending_Upload"; 
    public static string specSelColName = "";

    public static string IdStr = "", bwID = "";        // IDs for error report.
    // --------------------------------------------------------------------
    public static List<String> ListExcelReal = new List<string>();
    public static List<String> ListExcelCaption = new List<string>();

    public static List<String> List_SpecExcelMap = new List<string>();
    public static List<String> List_SpecNotInExcel = new List<string>();
    public static DataTable excelTable = new System.Data.DataTable();

    // --------------------------------------------------------------------
    // public static string colorGrey   = "#FFEFD3"; 
    // public static string colorGreen  = "#FFEFD3"; 
    // public static string colorRed    = "#FFEFD3"; 
    // public static string colorYellow = "#FFEFD3"; 

    public ClassTools()
    {
        /*
        fValue = fValue.Replace("'", "&#39;");
        t = "<td style='width:" + fSize + "px;color:#CC3300;text-align:center;'>" + fValue + "</td>";
        */
    }

    public string CheckUser(string sql)
    {
        string userStatus = ""; // "" = OK




        return userStatus;
    }



    public static string CheckFile(string filePath)
    {
        string msg = "";

        if (File.Exists(filePath))
        {
            FileInfo f = new FileInfo(filePath);
            if (f.Length == 0)
            {
                msg = "File " + filePath + " is empty.";
            }
            else
            {
                // good
            }
        }
        else
        {
            msg = "File " + filePath + " not found. Abort.";
        }

        return msg;

    }

    protected void DoFileArchive(string fileName, string sourcePath, string targetPath)
    {

        string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
        string destFile = System.IO.Path.Combine(targetPath, fileName) + ".save";

        // -----------------------------------------------------------

        if (System.IO.File.Exists(@destFile))
        {
            try
            {
                System.IO.File.Delete(@destFile);
            }
            catch (System.IO.IOException e)
            {
                //Response.Write(e.Message);
                return;
            }
        }


    }

    public static string Mid(string str, int startIndex, int length)
    {
        string theStr = "";
        if (String.IsNullOrEmpty(str))
        {

        }
        else
        {
            str.Substring(startIndex, length);
        }
        return theStr;
    }


    public static string Left(string str, int length)
    {

        string theStr = "";

        if (String.IsNullOrEmpty(str) || length < 0)
        {
        }
        else
        {
            if (str.Length > length)
            {
                theStr = str.Substring(0, length);
            }
            else
            {
                theStr = str;
            }
        }

        return theStr;

    }

    public static string Right(string str, int length)
    {
        string theStr = "";

        if (String.IsNullOrEmpty(str) || length < 0)
        {

        }
        else
        {
            theStr = str.Substring(str.Length - length, length);
        }
        return theStr;
    }

    public static string xCleanData(string DataStr, string AsType)
    {
        /*
        AsType = text, date, number (int, money, float)
        */
        string theStr = "";

        if (String.IsNullOrEmpty(DataStr))
        {
            // do nothing
        }
        else
        {
            if (AsType == "text")
            {
                DataStr = DataStr.Replace("'", "~");
            }
        }
        return theStr;
    }

    public static string TimeStamp()
    {
        string timestamp = "", year = "", month = "", day = "", hour = "", minute = "", second = "";

        year = DateTime.Now.Year.ToString();
        month = Right("0" + DateTime.Now.Month.ToString(), 2);
        day = Right("0" + DateTime.Now.Day.ToString(), 2);
        hour = Right("0" + DateTime.Now.Hour.ToString(), 2);
        minute = Right("0" + DateTime.Now.Minute.ToString(), 2);
        second = Right("0" + DateTime.Now.Second.ToString(), 2);

        //dt = year + "-" + month + "-" + day + "@" + hour + "-" + minute + "-" + second ;
        timestamp = year + "." + month + "." + day + "@" + hour + ":" + minute + ":" + second;

        return timestamp;

    }

    public static Boolean IsNumeric(string s)
    {
        foreach (char c in s)
        {
            if (!char.IsDigit(c) && c != '.')
            {
                return false;
            }
        }

        return true;
    }

    public static Boolean IsDate(string sdate)
    {
        DateTime dt;
        bool isDate = true;
        try
        {
            dt = DateTime.Parse(sdate);
        }
        catch
        {
            isDate = false;
        }
        return isDate;
    }

    public static string MoneyFormat()
    {
          
        /*
         * fldLabor = Math.Round(Convert.ToDecimal(fldLabor + "0"),2).ToString() ;   
         * double value = 234.656;
          Console.WriteLine(value.ToString("C", CultureInfo.CurrentCulture)); // displays $
         * fldLabor =              string.Format(new CultureInfo("en-US"), "{0:c}", float.Parse(fldLabor) );
        */
        return "not-used";
    }

    public static string Chop(string str, int length)
    {

        string theStr = "";

        if (String.IsNullOrEmpty(str) || length < 0)
        {
        }
        else
        {
            if (str.Length > length)
            {
                theStr = str.Substring(0, length - 2) + "...";
            }
            else
            {
                theStr = str;
            }
        }

        return theStr;

    }



    public static string HtmlMsg(string Status, string Msg)
    {
        string Message;

        switch (Status)
        {
            case "NORMAL":
                Message = "<p style='text-align: center;color:#CC3300;'>" + Msg + "</p>";
                break;

            case "GOOD":
                Message = "<p style='text-align: center;color:#CC3300;'>" + Msg + "</p>";
                break;

            case "WARNING":
                Message = "<p style='text-align: center;color:#FF4500;'>" + Msg + "</p>";
                break;

            case "BAD":
                Message = "<p style='text-align: center;color:#0000FF;'>" + Msg + "</p>";
                break;

            default:
                return Msg;
        }

        return Message;

    }


    public static string TableToHtml(DataTable table)
    {

        string fValue = "", chopValue = "";
        DateTime mDate;
        string html = "<table border=1>";

        html = html + "<tr>";

        foreach (DataColumn column in table.Columns)
        {
            html = html + "<td class='colheader' style='color: #CC3300;' >" + column.ColumnName + "</td>";
        }
        html = html + "</tr>";


        foreach (DataRow row in table.Rows)
        {
            html = html + "<tr>";

            foreach (DataColumn column in table.Columns)
            {
                fValue = row[column].ToString();

                if (String.IsNullOrEmpty(fValue))
                {
                    html = html + "<td>&nbsp;</td>";
                }
                else
                {

                    //if (fValue.Length > 30)     fValue = ClassTools.Chop(fValue, 20);
                    
                    html = html + "<td nowrap >" + fValue + "</td>";

                }

            }
            html = html + "</tr>";

            // -------------------------------------------------
        }

        html = html + "</table>";

        table.Clear();

        return html;

    }



    public static Boolean ValidEmail(string email)
    {
        if ( String.IsNullOrEmpty(email) ) 
        {
            return false;
        }
        else
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email,
            @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
               
    }

    

    public static void xActionLog(string log)
    {
            string t;
            string path = @"\\usmdlsd849\p\Repts\monitor2.txt";
            string dt = DateTime.Now.ToString("yyyy.MM.dd. HH:mm:ss");
            try
            {

                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(dt + ". Created");
                        sw.WriteLine("------------------");
                    }
                }

                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(log + ". " + dt);
                    if (log.IndexOf("Done") != -1)
                    {
                        sw.WriteLine(" ");
                    }
                }

                /* Open the file to read from.
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
                */



            }
            catch (Exception e)
            {
                string localPath = @"C:\Projects\Visual_Studio\source\repos\PMT_Reporting_Service\TestService\bin\Release\log.txt";
                using (StreamWriter sw = File.CreateText(localPath))
                {
                    sw.WriteLine("Created on : " +  e.Message+ log);

                    t = e.Message;
                }
            }
            
        }

    private List<object> ConvertArrayToList(object[] array)
    {
        List<object> list = new List<object>();

        foreach (object obj in array)
            list.Add(obj);

        return list;
    }

    public static string DummyTable()
    {
        string t = "<table border='1' class='w3-table-all w3-small'>" +   
                    "<tr><th>Name</th><th>Phone</th><th>Country</th></tr>" +
                    "<tr><td>Tuyen</td><td>345.876.0987</td><td>Germany</td></tr>" +
                    "<tr><td>Thu</td><td>234.765.9876</td><td>USA</td></tr>" +
                    "<tr><td>Trang</td><td>765.324.5678</td><td>China</td></tr>" +
                    "</table>";
        return t;
    }

}