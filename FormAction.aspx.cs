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

using System.Web.UI.WebControls;

using System.Web.Profile;

using System.Text;
using System.IO;
using System.Linq;

public partial class FormAction : System.Web.UI.Page
{

    DataTable table;

    string Mode = "", Sql = "", UserID = "", TableID = "", RecordID = "";
    //string UserLogin = "", UserPassword = "", UserEmail = "";
    //string UserRole = "", RoleID = "", 


    protected void Page_Load(object sender, EventArgs e)
    {
        // Response.Write( ClassTools.Hello() );
        string f;

        Mode = Request.QueryString["Mode"];                      // Main or Detail
        Sql = Request.QueryString["Sql"];
        UserID = Request.QueryString["UserID"];
        TableID = Request.QueryString["TableID"];
        RecordID = Request.QueryString["RecordID"];

        //UserLogin = Request.QueryString["UserLogin"];            // UserID = "CATHY";   
        //UserPassword = Request.QueryString["UserPassword"];      // TableID = "PROJECT";
        //UserEmail = Request.QueryString["UserEmail"];            // RecordID = "23";

        // --------------------------------------------------------------------------------------------

        switch (Mode)
        {

            case "ShowSpec":

                // Response.Write("<p>ShowSpec. Coming soon on next tutorial</p>" + ClassTools.DummyTable() + "[END]");
                f = ShowSpecForm(RecordID);         // SpecID)
                Response.Write(f + "[END]");

                break;

            case "UpdateSpec":

                f = UpdateSpec(Sql);
                Response.Write(f + "[END]");

                break;

            case "ShowDetail":

                f = ShowSideBarDetailForm(RecordID);
                Response.Write("<p>ShowDetail. Coming soon on next tutorial</p>" + f + "[END]");
                // Response.Write("<p>ShowDetail. Coming soon on next tutorial</p>" + ClassTools.DummyTable() + "[END]");

                break;

            case "Register":

                Response.ClearContent();  // not working
                Response.Write("Registration is complete." + "[END]");

                break;

            case "Login":
                
                break;

            default:
                // code block
                break;
        }


    }

    public static string UpdateSpec(string Sql)
    {
        string msg = "";
        string[] chop = Sql.Split('~');
        /*
        SpecID;
        TableName
        ColumnName
        ControlLabel
        ControlType
        ControlValidate
        */
        msg = chop[0] + ", " + chop[1] + ", " + chop[2] + ", " + chop[3] + ", " + chop[4] + ", " + chop[5];

        string aSql =
              "update [App_DataSpec] set " +

                "[TableName] = '"        + chop[1] + "'" +
               ",[ColumnName] = '"     + chop[2] + "'" +
               ",[ControlLabel] = '"    + chop[3] + "'" +
               ",[ControlType] = '"     + chop[4] + "'" +
               ",[ControlValidate] = '" + chop[5] + "'" +
                   " where [SpecID] = " + chop[0] ;
        

        DataTable table = ClassSQL.ExecSql_Table(aSql, "ACTION");
        DataRow row = table.Rows[0];
        return row["ExecMessage"].ToString();


    }

    public static string ShowSpecForm(string SpecID)    // string sqlData, string sqlForm, string UserID)
    {

        DataTable specTable;
        string sql, html = "", t;
        string TableName = "", ColumnName = "", ControlLabel = "", ControlType = "", ControlValidate = "";

        specTable = ClassSQL.ExecSp10P_Table("sp_App_SideBar", "ShowSpec", "(notused)", SpecID);
        // ProjectID = tableData.Rows[0]["ProjectID"].ToString();

        TableName = specTable.Rows[0]["TableName"].ToString();
        ColumnName = specTable.Rows[0]["ColumnName"].ToString();
        ControlLabel = specTable.Rows[0]["ControlLabel"].ToString();
        ControlType = specTable.Rows[0]["ControlType"].ToString();
        if (ControlType == "BLANK") ControlType = "";

        ControlValidate = specTable.Rows[0]["ControlValidate"].ToString();
       
        html = "<form id='sForm'><table border=1 class='w3-table-all w3-small'>" +

        "<tr>" + "<td>Specification</td>" + DoTextStd("ReadOnly", "sb_SpecID", SpecID, "100") + "</tr>" +    // sb = on SideBar
        "<tr>" + "<td>Table Name</td>" + DoTextStd("ReadOnly", "sb_TableName", TableName, "100") + "</tr>";
        
        if (ColumnName == "")
        {
            html += "<tr>" + "<td>Column Name</td>" + DoTextStd("", "sb_ColumnName", ColumnName, "100") + "</tr>";
        }
        else
        {   // can not change
            html += "<tr>" + "<td>Column Name</td>" + DoTextStd("ReadOnly", "sb_ColumnName", ColumnName, "100") + "</tr>";
        }
        html += "<tr>" + "<td>Column Label</td>" + DoTextStd("", "sb_ControlLabel", ControlLabel, "100") + "</tr>" +

        "<tr>" + "<td>Column Type</td>" + DoListStd("", "sb_ControlType", ControlType, "ControlType", "100") + "</tr>";

        if (ControlType == "LIST" ) 
        {
            html = html + "<tr>" + "<td>Column Validate</td>" + DoListStd("", "sb_ControlValidate", ControlValidate, "ControlValidate", "100") + "</tr>";
        }
        else
        {
            html = html + "<tr>" + "<td></td><td><input type='hidden' id='sb_ControlValidate' value='" + ControlType + "'></td></tr>";     // for consistency
        }


        html = html + "</table><hr>" + 
                        "<div id='DivTemp'></div>" +
                        "<input id='buttonClose' type='button' style='width:100px;height:24px;width:100px;font-size:11px' value='Close' onclick='closeRightMenu()' />&nbsp;&nbsp;&nbsp;" +
                        "<input id='buttonSaveSpec' type='button' style='width:100px;height:24px;width:100px;font-size:11px' value='Save' onclick='Action_SaveSpec()' />" +
                        "</form>"; 



        specTable.Dispose();

        return html;


        

    }



    
public string ShowSideBarDetailForm(string RowID)     //public string Action_DetailForm(string RowID)
    {

        DataTable vTable;
        string sql, html;
        /*
        sql = "  select * from [Project_Spending] where [RowID] = " + RowID;
        vTable = ClassSQL.ExecSql_Table(sql, "SELECT");
        */
        vTable = ClassSQL.ExecSp10P_Table("sp_App_SideBar", "ShowDetail", "(notused)", RowID);

        html = "<table border=1  class='w3-table-all w3-small' >";
        foreach (DataColumn column in vTable.Columns)
        {
            html = html + "<tr><td>" + column.ColumnName + "</td><td>" + vTable.Rows[0][column.ColumnName].ToString() + "</td></tr>";
        }
        html = html + "</table>";

        vTable.Dispose();

        return html;

    }
    


    public static string DoTextStd(string fMode, string fID, string fValue, string fSize)
    {

        string t = "";
        if (fMode == "ReadOnly") // 
        {
            t = "<td>";
            t = t + "<input id='" + fID + "' value='" + fValue + "' type='text' disabled readonly='readonly'  >";
            t = t + "</td>";
        }
        else
        {
            t = "<td>";
            t = t + "<input id='" + fID + "' value='" + fValue + "' type='text' >";
            t = t + "</td>";
        }

        return t;

        // fValue = fValue.Replace("'", "&#39;");
        // t = "<td style='width:" + fSize + "px;color:#CC3300;text-align:center;'>" + fValue + "</td>";

    }

    public static string DoListStd(string fMode, string fID, string fValue, string fValidate, string fSize)
    {
        // IMPORTANT.
        // In most case. fValidate is normall the same as fID. And it is also the Category in array lines
        // Will be use for Validate Category later. as cboCategory. In LookupArray.js
        // function Cbo_Populate(cboCategory, cboID, cboValue), In Lookup.js

        string t = ""; 

        if (fValue == null) fValue = "";
        
        t = t + "<td>";
        t = t + "<select  id='" + fID + "' name='" + fValidate + "' class='sidebarCbo'  style='width:200px;height:20px;width:175px;font-size:12px' > ";
        t = t + "<option value='" + fValue + "'>" + fValue + "</option>";   // ok
        t = t + "</select></td>";

        return t;

    }


    // Keep for now ==========================================================================
    /*
        // sql = "  SELECT * FROM [App_DataSpec] where [SpecID] = " + SpecID;
        // vTable = ClassSQL.ExecSql_Table(sql, "SELECT");

        html = "<table border=1  class='w3-table-all w3-small' >";
        foreach (DataColumn column in vTable.Columns)
        {
            html = html + "<tr><td>" + column.ColumnName + "</td><td>" + vTable.Rows[0][column.ColumnName].ToString() + "</td></tr>";
        }
        html = html + "</table>";


        */

}