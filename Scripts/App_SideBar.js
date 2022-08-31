function App_SideBar() //
{
    
    /*
    var x = document.getElementById("sForm");
    var txt = "";
    var i;
      for (i = 0; i < x.length; i++)
    {
        txt = txt + x.elements[i].id + " = " + x.elements[i].value + "<br>";
    }
    // alert("App_SideBar()\n" + txt);
    */

}

function NewAjax(Mode, Sql, UserID, TableID, RecordID)  //("notused", "SaveSpec", "DivRight", "_CATHY", "_TableID", RowID);     // [SpecID]
{
    //alert("1. START NewAjax()\n" + Mode); //+ Mode + " - " + RecordID);
        
    Url = "FormAction.aspx?Mode=" + Mode + "&Sql=" + Sql + "&UserID=" + UserID + "&TableID=" + TableID + "&RecordID=" + RecordID;

    //alert("2. NewAjax.\n" + Url);

    $.ajax({
        type: 'POST',
        url: Url,
        dataType: 'text',
        async: false,    // wait. one call at a time
        data:
        {

        },
        success: function (ajaxData) //
        {
            //alert("2. NewAjax - return:\n" + ajaxData);    // Including the form ?! // While Action_AjaxLoad() is working very nice!!!
            let eod = ajaxData.indexOf("[END]");             //  end of data                                 
            ajaxData = ajaxData.substring(0, eod);
            //alert("3. NewAjax.\n" + ajaxData);

            
            if (ajaxData.indexOf('***') > -1) //    SaveSpec(). a flag from ClassSQL.ExecSql_Table() Action Query
            {
                var e = document.getElementById("DivTemp");
                e.innerHTML = ajaxData;
                //document.getElementById("buttonSaveSpec").value = ajaxData;
                document.getElementById("buttonSaveSpec").style.visibility = "hidden";
                //document.getElementById("buttonSaveSpec").style.width = "300px";
                //document.getElementById("buttonSaveSpec").disabled = true;
                

            }
            else // 
            {
                var e = document.getElementById("DivRight");   // 
                e.innerHTML = ajaxData;
            }

            
            

            if (Mode == 'ShowSpec') //
            {
                // alert("here. " + Mode);
                Action_SideBarCbo();
            }
            //return (ajaxData);

        },

        error: function (xhr, ajaxOptions, error) //
        {
            alert("AJAX ERROR: " + xhr.status);
        }

    });


}


function Action_SaveSpec() // Mode, SpecID)   
{
    //alert("*-- Action_SaveSpec(). ");
    
    var UserID = document.getElementById("_UserID").value; // alert(UserID);
    var SpecID = document.getElementById("sForm").elements.namedItem("sb_SpecID").value;
    var ControlType = document.getElementById("sForm").elements.namedItem("sb_ControlType").value;

    var sql = SpecID;
    sql = sql + "~" + document.getElementById("sForm").elements.namedItem("sb_TableName").value;   // ^ to split later
    sql = sql + "~" + document.getElementById("sForm").elements.namedItem("sb_ColumnName").value;
    sql = sql + "~" + document.getElementById("sForm").elements.namedItem("sb_ControlLabel").value;
    sql = sql + "~" + ControlType;
    // if (ControlType == "LIST") //
    sql = sql + "~" + document.getElementById("sForm").elements.namedItem("sb_ControlValidate").value;


    //alert("Action_SaveSpec(). Now Call NewAjax\n" + sql);

    // NewAjax(Mode, Sql, UserID, TableID, RecordID)
    
    NewAjax("UpdateSpec", sql, UserID, "notused_TableID", SpecID); // [SpecID]
    


}



function openRightMenu(theMode, RowID) //
{
    //alert("START. openRightMenu()\n" + theMode + " - " + RowID);
    
    var rm = document.getElementById("rightMenu");
    rm.style.display = "block";
    
    switch (theMode) //
    {

        case "ShowSpec":

            rm.style.width = "500px";
            rm.style.backgroundColor = "#e6d6c0";
                       //  NewAjax( Mode,      Sql,    UserID, TableID, RecordID)
            NewAjax("ShowSpec", "_Sql", "CATHY", "_TableID", RowID); // [SpecID]
            //document.getElementById("DivRight").innerHTML = ajaxReturn;

            break;

        case "ShowDetail":

            rm.style.width = "800px";
            rm.style.backgroundColor = "#ddebdd";

            //alert("Now Call Ajax.\n. " + theMode + " - " + RowID);
            //             NewAjax( Mode,         Sql,          UserID,   TableID,  RecordID)
            NewAjax("ShowDetail", "_Sql", "CATHY", "_TableID", RowID); //   [RowID]

            Action_SideBarCbo();
            
            break;


        case "ShowLog":

            // in used ??
            rm.style.width = "1200px";
            // rm.style.height = "500px";
            rm.style.backgroundColor = "#bfbfbf";
            t = document.getElementById("_RowLog").value;   // already there

            t = "<p style='font-family:Consolas;font-size:14px' />" + t + "</p>";

            document.getElementById("DivRight").innerHTML = t;   

            break;


        default:

            rm.style.width = "800px";
            rm.style.backgroundColor = "#e6d6c0";  
          
    }

    //document.getElementById("DivRight").innerHTML = "<p>Hello</p>";

}

function closeRightMenu() //
{
    var rm = document.getElementById("rightMenu");
    rm.style.display = "none";
}

function Action_SideBarCbo() //
{
    // alert("START. Action_SideBarCbo()");

    // Populate cbo on the sidebar
    var cboCategory, cboID, cboValue;
    var cbo, x, i, t = "";
    x = document.querySelectorAll(".sidebarCbo");
    for (i = 0; i < x.length; i++) //
    {
        // alert(x[i].id + ' - ' + x[i].name);
        cboID = x[i].id;
        cboCategory = x[i].name;   // ex: Status array -> element Status ->
        cbo = document.getElementById(cboID);
        cboValue = cbo.options[cbo.selectedIndex].text;
        Cbo_Populate(cboCategory, cboID, cboValue);    // in Lookup.js

    }


}

