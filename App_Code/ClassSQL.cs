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




public class ClassSQL
    {

        public static string gUserLogin = "", gAppInfo = "";
        public static SqlConnection gDb = null;
        public static SqlCommand gCmd = null;
        public static SqlDataReader gReader = null, gRs = null;

        public ClassSQL()
        {
            
        }

    public static string Hello()
        {
            return "Hello - ClassSQL";
        }



        public static string GetConStr()
        {
            string ConStr;
            ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            return ConStr;
        }

    public static string IsData(DataTable table)
    {
        string sqlMsg = "";     // "" is ok
        string t = ClassTools.TableToHtml(table);

        DataColumnCollection columns = table.Columns;
        if (columns.Contains("ExecStatus"))
        {
            DataRow row = table.Rows[0];
            sqlMsg = row["ExecStatus"] + ". " + row["ExecMessage"];
        }
        return sqlMsg + "<br>" + t;

    }
    public static DataTable ExecSql_Table(string sql, string queryType)
        {

        /*
            Always return the column "ExecStatus" to simplify UI coding. UI will check [ExecStatus].
            
                [ExecStatus] = '' is OK.    Anything else other than '' is error
                [ExecStatus] = 'ERROR'.     Error

        */

        /*
        Reminder. Store procedure using this function MUST ALWAY return a table. Either data table or the ERROR table below.
        Could be create by SP or C#
        [ExecStatus],            // '' for OK, 'ERROR' = error
        [ExecMessage]            // error message
        [ExecInfo]               // Additional info.
                                    ProjectID=23~10~T_210:ERROR|1000. Invalid column name 'Zip'.

        If there is a column [ExecStatus] with ERROR, then there was error.

        */

        DataTable table = new DataTable();
        DataTable SpTable = new DataTable();
        SpTable.Columns.Add("ExecStatus", typeof(string));   // Required column for caller
        SpTable.Columns.Add("ExecMessage", typeof(string));  // error message
        SpTable.Columns.Add("ExecInfo", typeof(string));     // SQL + other info : user + pc + browser + etc..
                                                                 // -----------------------------------------------------

            gDb = new SqlConnection(GetConStr());
            gDb.Open();

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, gDb);
                da.Fill(table);

                if (queryType == "SELECT")
                {
                    if (table.Rows.Count == 0)
                    {
                        // no record found is an error.
                        SpTable.Rows.Add("ERROR", "No records found.", sql);
                        return SpTable;
                    }
                    else
                    {
                        return table;
                    }
                }

                if (queryType == "ACTION")
                {
                    // OK. Return a record for consistency . "" = ok
                    SpTable.Rows.Add("", "*** Update is complete.", sql);
                    return SpTable;
                }



            }
            catch (Exception ex)
            {
                // Could be cases like network & server is down. If any error, return the error message.
                string NoteToIT = sql;
                SpTable.Rows.Add("ERROR", ex.Message + ". @1200", NoteToIT);

                return SpTable;

            }

            finally
            {
                table.Dispose();
                SpTable.Dispose();
                CleanUp();
            }

            // the function requires this block ????!!!
            SpTable.Rows.Add("ERROR", "Should not get here. @ExecSql_Table.", sql);
            return SpTable;

        }



        public static DataTable ExecSp10P_Table(string spName,
                                                string @P1, string @P2 = "", string @P3 = "", string @P4 = "", string @P5 = "",
                                                string @P6 = "", string @P7 = "", string @P8 = "", string @P9 = "", string @P10 = "")
        {

            /* 
                Reminder. Store procedure using this function MUST return a table
                If there is no record found, it is also considered an ERROR. No data found.

                In case of ERROR. Return below table
                [ExecStatus] = 'ERROR'                  This is what the UI will check to see if it's an error.
                [ExecMessage]                           error message to display.
                [ExecInfo]                              More details. 
                                                        ProjectID=23~10~T_210:ERROR|1000. Invalid column name 'Zip'.
                                                        the web control id + :ERROR| + message to add to the control title 
            */


            string t = ""; 
            string sqlCon = GetConStr();

            DataTable table = new DataTable();
            /*
            DataTable SpTable = new DataTable();
            SpTable.Columns.Add("ExecStatus", typeof(string));   // Required column for caller
            SpTable.Columns.Add("ExecMessage", typeof(string));  // error message
            SpTable.Columns.Add("ExecInfo", typeof(string));     // SQL + other info : user + pc + browser + etc..
            */                                                     // -----------------------------------------------------

            gCmd = new SqlCommand(sqlCon);

            try
            {

                gDb = new SqlConnection(sqlCon);
                gDb.Open();

                gCmd.Connection = gDb;
                gCmd.CommandType = CommandType.StoredProcedure;

                gCmd.CommandText = spName;

                gCmd.Parameters.Add(new SqlParameter("@P1", @P1));
                gCmd.Parameters.Add(new SqlParameter("@P2", @P2));
                gCmd.Parameters.Add(new SqlParameter("@P3", @P3));
                gCmd.Parameters.Add(new SqlParameter("@P4", @P4));
                gCmd.Parameters.Add(new SqlParameter("@P5", @P5));
                gCmd.Parameters.Add(new SqlParameter("@P6", @P6));
                gCmd.Parameters.Add(new SqlParameter("@P7", @P7));
                gCmd.Parameters.Add(new SqlParameter("@P8", @P8));
                gCmd.Parameters.Add(new SqlParameter("@P9", @P9));
                gCmd.Parameters.Add(new SqlParameter("@P10", @P10));

                gReader = gCmd.ExecuteReader();

                // ----------------------------------------
                table.Load(gReader);
                // ----------------------------------------

                return table;

            }
            catch (Exception ex)
            {
                // ??? Could be cases like network & server is down. If any error, return the error message.

                DataTable SpTable = new DataTable();
                SpTable.Columns.Add("ExecStatus", typeof(string));   // Status. Required column for caller
                SpTable.Columns.Add("ExecMessage", typeof(string));  // error message
                SpTable.Columns.Add("ExecInfo", typeof(string));     // SQL + other info : user + pc + browser + etc..

                SpTable.Rows.Add("[ERROR]", ex.Message, "@1201 - " + spName);
                return SpTable;
                // ----------------------------------------
            }

            finally
            {
                // SpTable.Dispose();
                CleanUp();
            }

        }


        public static void CleanUp()
        {
            if (gReader != null) gReader.Close();
            if (gCmd != null) gCmd.Dispose();
            if (gDb != null) gDb.Close();

        }



        /*
        using (SqlConnection con = new SqlConnection(CNN_STRING))
        {
            using (SqlCommand cmd = new SqlCommand("Select ID,Name From Person", con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);           
                return ds;
            }
        }
        -------------------------------------
        string connectionString = ConsoleApplication1.Properties.Settings.Default.ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            //
            // Open the SqlConnection.
            //
            con.Open();
            //
            // This code uses an SqlCommand based on the SqlConnection.
            //
            using (SqlCommand command = new SqlCommand("SELECT TOP 2 * FROM Dogs1", con))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine("{0} {1} {2}",
                        reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                }
            }
        } 
         
        */




    }
