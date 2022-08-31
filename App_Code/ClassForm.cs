using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Configuration;

using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.Web.Caching;
using System.Web.SessionState;
using System.Web.Profile;

using System.Text;
using System.IO;
using System.Linq;


    public class ClassForm
    {
        DateTime TimeStart = DateTime.Now, TimeEnd;
        string SysMode;
        DataTable table;

        public ClassForm()
        {
            // TODO: Add constructor logic here

        }

        public static string DoMasterForm(string Mode, string UserID, string TableID, string RecordID, string UserRole)   // ADMIN or USER
        {

            
            string html, KeyColumn = "", fKey = "", fInfo, fSpecID, fStatus, fControlType, fCode, fCaption, fName = "", sql, t;
            string fType, fValidate, fSize, fValue = "", fSecurity, fCheck, fRuleNotes, RowID = "", RowUpdate = "", RowLog = "", RowLog_Display = "";
            int rowGroup;

            // get the column name of KEY
            sql =  "select * from[App_DataSpec] where[TableName] = '" + TableID + "' and [ControlType] = 'KEY'";
            DataTable table = ClassSQL.ExecSql_Table(sql, "SELECT");
            KeyColumn = table.Rows[0]["ColumnName"].ToString();
            table.Dispose();

            sql = "select * from " + TableID + " where [" + KeyColumn + "]=" + RecordID;   // ??? need to put it in sp
            
            DataTable tableData = ClassSQL.ExecSql_Table(sql, "SELECT");
            DataTable tableSpec = ClassSQL.ExecSp10P_Table("sp_App_FormLoad", Mode, UserID, TableID, RecordID );
            

            // So this is the way to check if there was an rror
            if (tableSpec.Columns.Contains("ExecStatus"))
            {
                // t = "Error handling. It works. 2021.09.18";
                html = ClassTools.TableToHtml(tableSpec);
                return html;
            }

            // Continue
            // ----------------------------------------

            html =  "<div class='w3-responsive'>" +
                    "<table  border='1' class='w3-table-all w3-small'>" +   
                    "<thead>" +  // just for maintain column width
                    "<tr class='w3-light-grey'>" +
                    "<th style='width:8%;'>" + UserID + "</th>" +
                    "<th style='width:17%;'>" + UserRole.Replace(".", "") + "</th>" +       // demo one role for now.
                    "<th style='width:8%;'>&nbsp</th>" +
                    "<th style='width:17%;'>&nbsp</th>" +
                    "<th style='width:8%;'>&nbsp</th>" +
                    "<th style='width:17%;'>&nbsp</th>" +
                    "<th style='width:8%;'>&nbsp</th>" +
                    // "<th style='width:17%;'>" + DateTime.Now.ToShortTimeString() + "</th>" +
                    "<th style='width:17%;'>" + DateTime.Now.ToString("HH:mm:ss") + "</th>" + 
                    // yourdate.ToString("yyyyMMddHHmmss")
                    "</tr>" +
                    "</thead>" ;

            rowGroup = 0;
            html = html + "<tr>";

            int r = 0;  // Data Row Index. Use to make ID unique on mutiple records view

            foreach (DataRow row in tableSpec.Rows)
            {

                r = r + 1;  // Not in used. In this case it will be = [App_DataSpec].[RowIndex]

                fControlType = row["ControlType"].ToString();
                fCode = row["ControlStyle"].ToString();  // will be use as CSS ClassName 

                
                // ---------------------------------------------------------------------------

                fSpecID = row["SpecID"].ToString();
                fStatus = row["Status"].ToString();
                fSecurity = row["Security"].ToString();

                fCaption = row["ControlLabel"].ToString();
                fName = row["ColumnName"].ToString();

                //fType = row.Field<string>("DbFieldType");    // not needed here yet !!!
                fValidate = row["ControlValidate"].ToString(); 
                fSize = row["ControlSize"].ToString();
                fRuleNotes = row["RuleNotes"].ToString();


                // --------------------------------------------------------------

                // ProjectID = tableData.Rows[0]["ProjectID"].ToString();

                RowID = tableData.Rows[0]["RowID"].ToString();   // just be there for now. it is ProjectID is the KEY

                RowUpdate = tableData.Rows[0]["RowUpdate"].ToString(); // for multi-user function
                
                // ---------------------------
                RowLog = tableData.Rows[0]["RowLog"].ToString();
                
                string[] separating = { "<br>" };
                string[] chop = RowLog.Split(separating, System.StringSplitOptions.RemoveEmptyEntries);
                RowLog_Display = "";
                for (int i = 0; i < chop.Length ; i++)
                {
                    RowLog_Display = RowLog_Display + chop[i] + "<br>";
                    if (i == 3) break; // just show 4 rows for now.
                }
                
                // -----------------------------

                if (String.IsNullOrEmpty(fName))    // in case of space holder
                {
                    fValue = "...";
                    //fControlType = "blank";         // should be
                }
                else
                {
                    fValue = tableData.Rows[0][fName].ToString();
                    fValue = fValue.Replace("'", "&#39;");
                }

                if (fControlType == "KEY")
                {
                    fKey = fName + "=" + fValue;    // CustomerID=12
                }

                // fKey must already assigned. 
                // fCheck is for form validattion. Narrow down to three type

                fCheck = "_";   // no need to do form validation. like list box
                if (fValidate == "TEXT") fCheck = "T";     // may need to clean
                if (fValidate == "DATE") fCheck = "D";
                if (fValidate == "NUMBER") fCheck = "N";

                fInfo = fKey + "~" + fSpecID + "~" + fCheck + fCode + fSecurity + r.ToString();

                t = "stop";
                // -------------------------------------------------------------

                if (fSecurity == "0") fControlType = "NOVIEW";
                if (fSecurity == "1") fControlType = "READONLY";

            // -------------------------------------------------------------

            //html = html + "<td  style='width:100px;font-family:Calibri;font-size:.8em;text-align:center'>" + row["ControlLabel"].ToString() + "</td>";
            
            // UserRole = "USERS";      //  "DBA";

                if (UserRole.IndexOf("DBA") > -1)   // if one of the role is DBA
                {
                    html = html + "<td ><input type='button' id='" + fSpecID + "' value='" + fCaption + "' style='width:100px;height:24px;width:100px;font-size:11px' " + 
                    "onclick=" + (char)34 + "openRightMenu('ShowSpec','" + fSpecID + "')" + (char)34 + "></td>";    // App_SideBar.js
                    t = "stop";
                
                }
                else
                {
                    html = html + "<td  style='width:100px;text-align:center'>" + fCaption + "</td>";
                }
                
                switch (fControlType)
                {
                    case "KEY":
                        html = html + DoReadOnly(fCaption, fName, fValue, fSize, fInfo, fControlType, fRuleNotes);
                        break;

                    case "TEXT":
                        html = html + DoText(fCaption, fName, fValue, fSize, fInfo, fRuleNotes);
                        break;

                    case "LIST":
                        html = html + DoList(fCaption, fName, fValue, fValidate, fSize, fInfo, fRuleNotes);
                        break;

                    case "READONLY":
                        html = html + DoReadOnly(fCaption, fName, fValue, fValidate, fSize, fInfo, fRuleNotes);
                        break;

                    case "DATE":
                        html = html + DoDate(fCaption, fName, fValidate, fValue, fInfo, fRuleNotes);
                        break;

                    case "BLANK":
                        html = html + DoBlank();
                        break;

                    case "NOVIEW":
                        html = html + DoNoView(fCaption, fName, fValue, fSize, fInfo, fRuleNotes);
                        break;

                    case "NOTES":
                        html = html + DoNotes(fCaption, fName, fValidate, fValue, fInfo, fRuleNotes);
                        rowGroup = 4;  // force end of row
                        break;

                    case "LOG":
                        
                        // RowLog = fValue; // could make it simpler. Just add to the end of the table.
                        // html = html + DoLog(fValue);  // 2021.0926. not in-used. Use option above.
                        rowGroup = 4;  // force end of row

                        break;

                    case "BUTTON":   // don't see any 0801
                        // html = html + DoButton(fCaption, fName, fValue, fSize, fInfo, fRuleNotes);
                        break;
                }

                rowGroup = rowGroup + 1;
                if (rowGroup >= 4)
                {
                    html = html + "</tr><tr>";
                    rowGroup = 0;
                }

            }

            html = html + "<tr><td><input id='buttonLog' type='button' style='width:100px;height:24px;width:100px;font-size:11px' value='Log' " +
            " onclick=" + (char)34 + "openRightMenu('ShowLog', '" + RecordID + "')" + (char)34 + " /></td>" + 

            "<td colspan=7 >" + RowLog_Display + "</td></tr>";
            // "onclick=" + (char)34 + "DoSpec('FormSpec.aspx?SpecID=" + fSpecID + "')" + (char)34 + ">" + fCaption + "</button></td>";

            html = html + "</table><input id='ButtonSave' type='button' class='w3-right' value='Submit' onclick='Action_Save()' />";

            // hidden Admin div
            t = "<div id='divAdmin' style='display:none' ><hr/>" +          
                "<table border=1 class='w3-table-all w3-small'>" +
                    "<tr><td style='width:10%;'>_RowLastUpdated</td><td style='width:10%;'><input    id='_RowLastUpdated'  value='" + RowUpdate + "'  style='width:100px;'></td>" +    // For multi-users feature
                        "<td style='width:10%;'>_UserID</td><td style='width:10%;'><input            id='_UserID'          value='" + UserID + "' style='width:100px;'></td>" +
                        "<td style='width:10%;'>_TableID</td><td style='width:10%;'><input           id='_TableID'         value='" + TableID + "' style='width:100px;'></td>" +
                        "<td style='width:10%;'>_RecordID</td><td style='width:10%;'><input          id='_RecordID'        value='" + RecordID + "' style='width:100px;'></td>" +
                        "<td style='width:10%;'>_UserRole</td><td style='width:10%;'><input          id='_UserRole'        value='" + UserRole + "' style='width:100px;'></td></tr>" +
                    "<tr><td>ToSave</td><td colspan=7><input id='ToSave' style='width:800px;' ></td>" +    // data to save. most important. 
                "<td><input  id='_RowLog' value='" + RowLog + "' style='width:100px;'> </td><td></td></tr></table></div><hr/>";
            
        //t = t + "<input id='ButtonPlay2' type='button' value='Play2' onclick='Action_Play2()' />";

        return html + t;

        }

        
        public static string DoLog(string fValue)
        {
            // all notes/log field start at column 1 and take the whole row
            // ??? not in use 0926
            string t = "";
            t = t + "<td colspan=7 >";
            t = t + "<textarea rows=3 cols=100  disabled ";
            t = t + fValue + "</textarea>";
            t = t + "</td><tr>";

            return t;
        }
        

        public static string DoBlank()
        {
            return ("<td>&nbsp</td>");
            
        }


    public static string DoText(string fCaption, string fID, string fValue, string fSize, string fInfo, string fRuleNotes)
    {
        // Attention. fID is not in used. the fInfo is. 
        // fSize is maxlength
        // -------------------------------------------
        string t = "";

        // fValue = fValue.Replace("'", "&#39;");
        t = "<td>";
        t = t + "<input id='" + fInfo + "' title='" + fValue + "' value='" + fValue + "' type='text' maxlength='" + fSize + "' ";
        t = t + "onChange='DoEdit(this)'>";
        t = t + "</td>";

        return t;

     }

    public static string DoCaption(string fCaption, string fID, string fValue, string fSize, string fInfo, string fRuleNotes)
    {
        string t = "";    // ??? 0727
        // fValue = fValue.Replace("'", "&#39;");
        t = "<td>" + fValue + "</td>";
        return t;

    }

    //                   DoMoney(       fCaption,        fName,      fValue,        fSize,        fInfo,       fRuleNotes);
    public static string DoMoney(string fCaption, string fID, string fValue, string fSize, string fInfo, string fRuleNotes)
        {
            // fSize not used. Money Width is in CSS

            string style = "std_money";

            if (!String.IsNullOrEmpty(fValue))
            {
                fValue = Convert.ToDecimal(fValue).ToString("n2");
                if (Convert.ToDecimal(fValue) < 0) style = "neg_money";
            }

            string t = "<td>";
            t = t + "<INPUT  class='" + style + "' id='" + fInfo + "' title='" + fValue + "' value='" + fValue + "' type='text' ";
            t = t + "onChange='DoEdit(this)'>";
            t = t + "</td>";

            return t;

        }

        //                   DoReadOnly(       fCaption,        fName,      fValue,        fValidate,           fSize,        fInfo,   fRuleNotes);
        public static string DoReadOnly(string fCaption, string fID, string fValue, string fControlType, string fSize, string fInfo, string fRuleNotes)
        {
            // 09.09.2021. need to fix the 100px below

            string t = "";

            if (fControlType == "key")
            {

                // t = "<td class='std_text' style='width:" + fSize + "px;color:#CC3300;text-align:center;'>" + fValue + "</td>";
                t = "<td style='width:" + fSize + "px;color:#CC3300;text-align:center;'>" + fValue + "</td>";
            }
            else
            {
                t = "<td>";
                t = t + "<input id='" + fInfo + "' title='" + fValue + "' value='" + fValue + "' type='text' ";
                t = t + "style='background-color: #CCCCCC;' readonly='readonly'>";
                // t = t + "maxlength=50  style='WIDTH:" + fSize + "px;background-color: #CCCCCC;' readonly='readonly' onChange='DoEdit(this)'>";
                t = t + "</td>";
            }
            return t;
        }

        public static string DoNoView(string fCaption, string fID, string fValue, string fSize, string fInfo, string fRuleNotes)
        {
            fValue = "********";

            string t = "";

            t = "<td>";
            t = t + "<input id='" + fInfo + "' title='View Access Denied' value='" + fValue + "' type='text' ";
            t = t + "style='background-color: #CCCCCC;' readonly='readonly' onChange='DoEdit(this)'>";
            t = t + "</td>";

            return t;
        }


        public static string DoList(string fCaption, string fName, string fValue, string fValidate, string fSize, string fInfo, string fRuleNotes)
        {
        
            string t = "", optionList;

            // Boolean IsWarning = false, bFound = false;


            if (fValue == null) fValue = "";
            //if (fDisplay == null || fDisplay == "") fDisplay = "&nbsp;";
            optionList = "<option value=''>&nbsp;</option>";

            t = t + "<td>";
            // t = t + "<select  id='" + fInfo + "' name='" + fName + "' class='myCbo' onChange='DoEdit(this)'> ";
            t = t + "<select  id='" + fInfo + "' name='" + fValidate + "' class='myCbo'  style='width:180px;height:20px;width:175px;font-size:12px' onChange='DoEdit(this)' > ";
            t = t + "<option value='" + fValue + "'>" + fValue + "</option>";   // ok
            t = t + "</select></td>";

            return t;

            /* Javascript will do this -------------------------------------------------------------------------------------  
            if (IsWarning)
            {
                t = t + "<td>";
                t = t + "<select  class='warning' id='" + fInfo + "' name='" + fInfo + "' title='Please select a valid selection from the list' style='WIDTH:" + fSize + "px;font-family:Calibri;font-size:.8em;' onChange='DoEdit(this)' ";
                t = t + optionList;
                t = t + "</select></td>";
            }
            else
            {
                t = t + "<td>";
                t = t + "<select  id='" + fInfo + "' name='" + fInfo + "' style='WIDTH:" + fSize + "px;font-family:Calibri;font-size:.8em;' onChange='DoEdit(this)' ";
                t = t + optionList;
                t = t + "</select></td>";
            }
            */

    }

    


        public static string DoDate(string fCaption, string fID, string fValidate, string fValue, string fInfo, string fRuleNotes)
        {

            string t = "";

            if (fValue.IndexOf("1/1/1900") > 0) fValue = "";    // SQL null date default.

            if (!String.IsNullOrEmpty(fValue))
            {
                DateTime dt = DateTime.Parse(fValue);
                fValue = String.Format("{0:MM/dd/yyyy}", dt);
            }

            t = t + "<td>";
            // t = t + "<input type='text' class='popdate' name='" + fInfo + "' id='" + fInfo + "' value='" + fValue + "' maxlength='15' size='10' ";
            t = t + "<input type='text' class='popdate' id='" + fInfo + "' value='" + fValue + "' maxlength='15' ";
            t = t + " onChange='DoEdit(this)' />";
            t = t + "</td>";

            return t;

        }
        //                   DoNotes(       fCaption,        fName,      fValidate,        fValue, fInfo, fRuleNotes);
        public static string DoNotes(string fCaption, string fID, string fValidate, string fValue, string fInfo, string fRuleNotes)
        {
            // setup : Notes must start on new whole row. And take the whole row
            string t = "";


            t = t + "<td colspan=7 >";
            t = t + "<input id='" + fInfo + "' class='notes' type='text'  maxlength='200' size='180' ";
            t = t + "onChange='DoEdit(this)'>";
            t = t + "</td>";

            return t;

           
            

        }


        public static string xCreateLookup(string Category)
        {
            // old technique not in use
            DataTable table;
            string cbo, t, sql;

            //ClassDAL dal = new ClassDAL();

            cbo = "<select id='" + Category + "' style='visibility:hidden;' >";
            try
            {
                sql = "select * from tblViewLookup where [Category]='" + Category + "'";
                table = ClassSQL.ExecSql_Table(sql, "");
                if (table.Rows.Count == 0)
                {

                }
                else
                {
                    foreach (DataRow row in table.Rows)
                    {
                        cbo = cbo + "<option value='" + row.Field<String>("CodeValue") + "'>" +
                              row.Field<String>("CodeDisplay") + "</option>";
                    }
                }
                cbo = cbo + "</select><br>";

            }
            catch (Exception error)
            {
                t = error.Message;
            }

            return cbo;

            /*
            <select id='AREA' style='visibility:hidden;'>
            <select id='AREA' >
                <option value="">&nbsp;</option>   
                <option value="CA">California</option>
            </select>
            */



        }


        public static string DoDetailForm(string sqlData, string sqlForm, string UserID)
        {

            /*  
                sqlForm = "select * from App_DataSpec where [TableName]='Project_Spending' order by [SpecIndex]";
                sqlData = "select * from Project_Spending where ProjectID=" + RecordID;
            */

            DateTime TimeStart = DateTime.Now, TimeEnd = DateTime.Now;
            string TimeDiff, t;

            string html = "", lookupCbo = "", ProjectID, IsOk, doneList = "";
            string  RowID = "",   // ??? 0718
                    fSpecID, fStatus, fSecurity, fValue, fDisplay, fCaption, fName, fType, fControlType, fCode,
                    fValidate, fSize, fInfo, fRuleNotes, fKey = "", fCheck;

            int iDataRow = 0, iDataCol = 0, fView, fUpdate, iYear;

            // -----------------------------------------------------

            DataTable dataRs, formRs;

            // -----------------------------------------------------
            // $$$ 2019
            formRs = ClassSQL.ExecSql_Table(sqlForm, "SELECT");
            dataRs = ClassSQL.ExecSql_Table(sqlData, "SELECT");
            // string t = ClassTools.TableToHtml(dataRs);

            //string dataMsg = ClassSQL.IsData(dataRs);
            //if (dataMsg.IndexOf("ERROR") > -1) return dataMsg;
            
            // ABORT

            // -------------------------------------------------------------

            html = "<form name='thisForm' >";
            // html = html + "<input id='Alert' class='alert'/><br />";
            // html = html + "<input id='TopSave' type='button' value='Save' onclick='DoSave()' />";
            html = html + "<table id='TableDetail'  border='1' class='w3-table-all w3-small'  >"; 

            string header = "";
            html = html + "<tr>";


            foreach (DataRow row in formRs.Rows)
            {
                header = row["ControlLabel"].ToString();
                // html = html + "<th title='" + header + "'>" + header + "</th>";
                html = html + "<th style='width:10%;' title='" + header + "'>" + header + "</th>";
            }
            html = html + "</tr>";


            int r = 0;   // data row index on detail.many.row form

            iDataRow = 0;

            foreach (DataRow dataRow in dataRs.Rows)
            {
                r = r + 1;

                iDataCol = 0;

                html = html + "<tr>";
            
                foreach (DataRow row in formRs.Rows)
                {
                    
                    // need to get the Key first. The Key record MUST be first

                    fSpecID = row["SpecID"].ToString();
                    fStatus = row["Status"].ToString();
                    

                    fCaption = row["ControlLabel"].ToString();
                    fName = row["ColumnName"].ToString();

                    fRuleNotes = row["RuleNotes"].ToString();
                    fValidate = row["ControlValidate"].ToString();
                    fCode = row["ControlType"].ToString();
                    fSize = row["ControlSize"].ToString();

                    // --------------------------------------------------------------
                    ProjectID = dataRs.Rows[iDataRow]["ProjectID"].ToString();
                    fValue = dataRs.Rows[iDataRow][fName].ToString();

                    // ??? 0718
                    fControlType = row["ControlType"].ToString(); 
                    if (fControlType == "KEY")
                    {
                        //fKey = fName + "=" + fValue;    // RowID=545
                        RowID = fValue;                                // 
                    }

                    // fKey must already assigned. The e + r is to make element unique, need for detail form
                    // fCheck is for form validattion. Narrow down to three type
                    fCheck = "_";   // not to form validate
                    if (fValidate == "TEXT") fCheck = "T";
                    if (fValidate == "DATE") fCheck = "D";
                    if (fValidate == "NUMBER") fCheck = "N";

                    // fInfo = fKey + "~" + fSpecID + "~" + fCheck + fCode + r.ToString();
                    fInfo = "Detail_" + RowID;

                // -------------------------------------------------------------

                if (String.IsNullOrEmpty(fName))
                    {
                        // need test
                        IsOk = DoBlank();
                        iDataCol++;
                        break;

                    }
                    else
                    {

                        // ----------------------------------------------------------
                        if (fControlType == "KEY")  // Detail Data
                        {
                            fControlType = "BUTTON";
                        }
                        else
                        {
                            fControlType = "CAPTION";   
                        }
                        // ----------------------------------------------------------

                        switch (fControlType)
                        {

                            case "KEY":
                                iDataCol++;
                                html = html + DoReadOnly(fCaption, fName, fValue, fSize, fInfo, fControlType, fRuleNotes);
                                break;

                            case "BUTTON":
                                iDataCol++;
                                // html = html + DoDetailButton(fCaption, fName, fValue, fSize, fInfo, fKey);   // fKey = fName + "=" + fValue;    // RowID=545
                                // html = html + DoDetailButton(fCaption, fName, fValue, fSize, fInfo, RowID);   // fKey = fName + "=" + fValue;    // RowID=545     
                                t = "";
                                t = t + "<td><input type='button' id='" + fInfo + "' title='" + fValue + "' value=''  ";
                                t = t + "style='width:" + fSize + "px;font-family:Calibri;font-size:.8em;' onClick=" + (char)34 + "openRightMenu('ShowDetail','" + RowID + "')" + (char)34 + ">";
                                t = t + "</td>";
                                html = html + t;

                            

                            // 
                            break;

                            

                            case "CAPTION":
                                iDataCol++;
                                html = html + DoCaption(fCaption, fName, fValue, fSize, fInfo, fRuleNotes);
                                break;

                            /*
                             * case "TEXT":
                                iDataCol++;
                                html = html + DoText(fCaption, fName, fValue, fSize, fInfo, fRuleNotes);
                                break;
                            case "money":
                                iDataCol++;
                                html = html + DoMoney(fCaption, fName, fValue, fSize, fInfo, fRuleNotes);
                                break;

                            case "LIST":
                                iDataCol++;
                                fDisplay = fValue; // for now  ??? 11/10/2014
                                html = html + DoList(fCaption, fName, fValue, fValidate, fSize, fInfo, fRuleNotes);
                                break;

                            case "date":
                                iDataCol++;
                                html = html + DoDate(fCaption, fName, fValidate, fValue, fInfo, fRuleNotes);
                                break;

                            case "notes":
                                iDataCol++;
                                html = html + DoNotes(fCaption, fName, fValidate, fValue, fInfo, fRuleNotes);
                                break;

                            case "readonly":
                                iDataCol++;
                                html = html + DoReadOnly(fCaption, fName, fValue, fSize, fInfo, fControlType, fRuleNotes);
                                break;

                            case "noview":
                                iDataCol++;
                                html = html + DoNoView(fCaption, fName, fValue, fSize, fInfo, fRuleNotes);
                                break;
                            */

                            default:
                                // should never get here
                                iDataCol++;
                                IsOk = DoBlank();
                                break;
                        }
                    }
                }

                
                html = html + "</tr>";

                iDataRow = iDataRow + 1;

            }

            html = html + "</table></form>";

            formRs.Dispose();
            dataRs.Dispose();

            TimeEnd = DateTime.Now;
            TimeDiff = "<p class='std_text'>" + TimeStart.ToLongTimeString() + " - " + TimeEnd.ToLongTimeString() + "<p>";

            return html; 
            

        }






    public static string VerticalForm(string CoID)
        {
            DataTable vTable;
            string sql;

            // sql = "exec [dbo].[sp_CO_Report] '', " + CoID;
            sql = "exec [dbo].[sp_CO_Report] '', " + CoID;
            vTable = ClassSQL.ExecSql_Table(sql, "SELECT");

            string html = "<table border=1 >";

            foreach (DataColumn column in vTable.Columns)
            {
                html = html + "<tr><td>" + column.ColumnName + "</td><td>" + vTable.Rows[0][column.ColumnName].ToString() + "</td></tr>";
            }
            html = html + "</table>";

            vTable.Dispose();

            return html;

        }

}

