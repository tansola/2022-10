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
using System.Web.UI.WebControls.WebParts;
using System.Web.Caching;
using System.Web.SessionState;
using System.Web.Profile;

using System.Text;
using System.IO;
using System.Linq;


    public partial class FormSave : System.Web.UI.Page
    {
        string UserID="", TableID="", RecordID="", ToSave="";
        string fID, fSpec, fType, fValue, controlID = "", controlsToUpdate = "", ExecStatus, ExecMessage, ExecInfo;
        string fSaveToTable = "", fSaveToField = "", fSaveSpecial = "", fSaveSpecialInfo = "", sql, sqlCheck, t;
        DataTable tblSpec = null, table = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            
            // Url = "FormSave.aspx?&UserID=" + UserID + "&TableID=" + TableID + "&RecordID=" + RecordID + "&ToSave=" + ToSave;

            UserID = Request.QueryString["UserID"];
            TableID = Request.QueryString["TableID"];
            RecordID = Request.QueryString["RecordID"];

            ToSave = Request.QueryString["ToSave"];

            ToSave = ToSave.Replace("'", "''");      // Save Working. Saved to database. 0803.
            // AlertInfo.Text = toSave;

            // |ProjectID=23~9~T9~Hello|ProjectID=23~11~_11~TX|ProjectID=23~16~D16~12/01/2015
            // |ProjectID=23~10~T210~77479|RowID=546~24~_2~BUDGET
            // SaveBySql_One(toSave);
            // ToSave = "|ProjectID=23~10~T_210~00099|ProjectID=23~14~T_214~TUYEN-80";


            if (!String.IsNullOrEmpty(ToSave))
            {
                ToSave = ToSave.Replace("⌘⌘", "&");    // Undo. alert("@100. FormLoad.htm. DoEdit() #,&")
                ToSave = ToSave.Replace("⌘", "#");      // alert("@100. FormLoad.htm. DoEdit()\nHandle #,&")

                try
                {
                    table = ClassSQL.ExecSp10P_Table("sp_App_FormSave", "PRODUCTION", TableID, RecordID, UserID, ToSave);
                    
                    if (table.Rows.Count == 0)
                    {
                        // should never be here. SP always return one or more records. See ClassSQL for instruction.
                    }
                    else
                    {

                        ExecStatus = table.Rows[0][0].ToString();
                        ExecMessage = table.Rows[0][1].ToString();
                        ExecInfo = table.Rows[0][2].ToString();

                        if (ExecStatus == "ERROR")  // This column return from SP
                        {
                            ExecMessage = "Error. " + table.Rows[0][1].ToString();   // :Error is the flag for UI. error message for user

                            // clean up to pass to UI
                            //ExecMessage = ExecMessage.Replace(" ", "_");
                            //ExecMessage = ExecMessage.Replace("'", "_");
                            controlsToUpdate = ExecInfo;   // controlsToUpdate + controlID + ExecMessage + "|";
                        }
                        else
                        {
                            // sucessfule. Add control ID to update back to the screen (to change back-ground to green)
                            controlsToUpdate = ExecInfo;   // controlsToUpdate + controlID + "|";

                        }

                    }

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    table.Dispose();
                }

                // ProjectID=23~10~T_210|ProjectID=23~14~T_214|
                controlsToUpdate = controlsToUpdate.Replace("'", "");  // in case of error there will be some
                
                // ProjectID=23~10~T_210|ProjectID=23~14~T_214|
                Response.Write(controlsToUpdate);



            }
        }




        public void SaveBySql_One(string toSave)
        {

            if (!String.IsNullOrEmpty(toSave))
            {

                // Split and update ONE column/sql at a time ---------------------------------------------

                string[] fInfo = toSave.Split('|');         // into each update
                foreach (string fToSave in fInfo)
                {
                    if (fToSave != "")
                    {

                        string[] f = fToSave.Split('~');     // into column data
                        fID = f[0];
                        fSpec = f[1];
                        fType = ClassTools.Left(f[2], 1);
                        fValue = f[3].Replace("|", "");


                        controlID = f[0] + "~" + f[1] + "~" + f[2];  // used to update color back to the form - just remove the value

                        // ----------------------------------------------------------------------------

                        sql = "select * from App_DataSpec where [SpecId]=" + fSpec;
                        tblSpec = ClassSQL.ExecSql_Table(sql, "SELECT");

                        if (tblSpec.Rows.Count > 0)
                        {
                            fSaveToField = tblSpec.Rows[0]["SaveToField"].ToString();
                            fSaveToTable = tblSpec.Rows[0]["SaveToTable"].ToString();

                            //fSaveSpecial = tblSpec.Rows[0]["SaveSpecial"].ToString();
                            //fSaveSpecialInfo = tblSpec.Rows[0]["SaveSpecialInfo"].ToString();      // special instruction
                        }
                        // ----------------------------------------------------------------------------

                        if (fType == "N")
                        {
                            fValue = fValue.Replace(",", "");       // SQL don't handle ','  -12,345,678.90' will crash
                            fValue = fValue.Replace("$", "");
                        }

                        if (fType == "T")
                        {
                            fValue = fValue.Replace("'", "''");     // ~ and all reserse characters
                            // ... more
                        }


                        if (String.IsNullOrEmpty(fValue))  // if user delete the existing value 
                        {
                            if (fType == "N" || fType == "D")
                            {
                                sql = "update " + fSaveToTable + " set [" + fSaveToField + "] = null where  " + fID;
                            }
                            else // text
                            {
                                sql = "update " + fSaveToTable + " set [" + fSaveToField + "] = '' where = " + fID;
                            }
                        }
                        else
                        {
                            if (fType == "N")
                            {
                                sql = "update " + fSaveToTable + " set [" + fSaveToField + "] = " + fValue + " where " + fID;
                            }
                            else // text & date
                            {
                                sql = "update " + fSaveToTable + " set [" + fSaveToField + "] = '" + fValue + "' where " + fID;
                            }
                        }


                        try
                        {
                            // TEST: To make it fail ----------------------------------------------------
                            // sql = "update Tool_Project set [RegionX] = 'Hello2' where ProjectID=23";
                            // --------------------------------------------------------------------------
                            table = ClassSQL.ExecSql_Table(sql, "ACTION");

                            // table = ClassSQL.ExecSp10P_Table("sp_Tool_FormSave", "RUN", "Project", "23", "NB58050", toSave);

                            // --------------------------------------------------------------------------

                            if (table.Rows.Count == 0)
                            {
                                // should never be here. DAL always return one or more records. [Status]='ERROR' - even when error
                            }
                            else
                            {

                                ExecStatus = table.Rows[0][0].ToString();   // "ExecStatus"

                                if (ExecStatus == "ERROR")  // error - This column return from error log
                                {
                                    ExecMessage = table.Rows[0][1].ToString();      // error message for user
                                    ExecMessage = ":Error:" + table.Rows[0][2].ToString();   // :Error is the flag for UI
                                    // clean up to pass to UI
                                    ExecMessage = ExecMessage.Replace(" ", "_");
                                    ExecMessage = ExecMessage.Replace("'", "_");
                                    controlsToUpdate = controlsToUpdate + controlID + ExecMessage + "|";
                                }
                                else
                                {
                                    // sucessfule. Add control ID to update back to the screen (change back-ground to green)
                                    controlsToUpdate = controlsToUpdate + controlID + "|";

                                }

                            }

                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                        finally
                        {
                            table.Dispose();
                        }

                    }

                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "UpdateControl('" + controlsToUpdate + "');", true);

            }

        }












    }
