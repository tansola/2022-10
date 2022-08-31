using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
using System.IO;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class FormLoad : System.Web.UI.Page
{
    DataTable table;

    string Mode = "", UserID = "", TableID = "", RecordID = "", RowID = "";
    string UserRole = "", Sql = "";
    string HtmlMain = "", HtmlDetail = "", HtmlSpec = "", t = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        // Response.Write( ClassTools.Hello() );

        Mode = Request.QueryString["Mode"];              // Main or Detail
        UserID = Request.QueryString["UserID"];          // UserID = "CATHY";   
        TableID = Request.QueryString["TableID"];        // TableID = "PROJECT";
        RecordID = Request.QueryString["RecordID"];      // The KEY 

        // User Permission ------------------------------------------------------------------------------------
        Sql = "select * from [App_UserRole] where [UserID] = '" + UserID + "'";

        table = ClassSQL.ExecSql_Table(Sql, "SELECT");

        foreach (DataRow dr in table.Rows)
        {
            UserRole = UserRole + dr["RoleCode"].ToString() + ".";     // Multiple roles designed. ADMIN to have the ADMIN button
        }
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        t = "";

        switch (Mode) // there will be more Mode
        {


            default:

                // exec sp_App_FormLoad 'CATHY', 'Project', '10'
                HtmlMain = ClassForm.DoMasterForm("Master", UserID, TableID, RecordID, UserRole);    // (string Mode, string UserID, string TableID, string RecordID, string UserRole)  

                HtmlDetail = "";
                if (TableID.ToUpper() == "PROJECT")   // then do the Detail Form
                { 
                    string sqlForm = "select * from App_DataSpec where [TableName]='Project_Spending' order by [SpecIndex]";
                    string sqlData = "select [ProjectID], [Month], [Year], [Amount], [SpendType], convert(varchar(20), [SpendDate], 101) as [SpendDate], [UserID], [Notes], [RowID] " +
                                     "from [Project_Spending] where [ProjectID] = " + RecordID;

                    HtmlDetail = ClassForm.DoDetailForm(sqlData, sqlForm, UserID);
                }

                Response.Write(HtmlMain + HtmlDetail);

                break;
        }




    }




}