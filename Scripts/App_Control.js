var colorReason = "#E9967A";              // dark red
var colorReminder = "#ffffbf"               // light yellow
var colorWarning = "#ffff00"               // bright yellow

function Action_Control() //
{

    // alert("Action_Control()\n. To run after Action_Init() for special cases.");  

    /*
        if ("NEW") // 

            var x = document.getElementById("formMain").elements;
            var t = "";
            for (i = 0; i < x.length; i++) //
            {
                x[i].disabled = true;
                x[i].style.visibility = "hidden";'
                //.style.backgroundColor = colorReason;
            }

    */


}




function Action_Validate() //
{

    // document.getElementById("divHeader").innerHTML = "";

    var sql, data, DataString, pageAlert = "Please provide required information<br>";
    var coID, userID, mmddyyyy;
    var today = new Date();

    mmddyyyy = myDate('aDate', 'aFormat'); // (today.getMonth() + 1) + "/" + today.getDate() + "/" + today.getFullYear();
    coID = document.getElementById("coID").value;
    userID = document.getElementById("userID").value;

    //alert(mmddyyyy + " - " + coID + " - " + userID);
    //return "ok";

    var toSave = "";    //  "|Name~Value|Name~Value|Name~Value|Name~Value|Name~Value|";
    var id, value, type, nameValue, t;


    // -------------------------------------------------
    document.getElementById("PageValidate").value = "";    // blank = ok = no error = all fields ready to save
    // -------------------------------------------------

    var x = document.getElementsByClassName("myMust");
    var t = "", tOne;
    var i;
    for (i = 0; i < x.length; i++) //
    {   // #1

        id = x[i].id;
        type = x[i].type;
        value = document.getElementById(id).value;  // should work for both text and select-one

        x[i].style.backgroundColor = "#fcefeb";

        t = t + "~" + x[i].id + " - " + x[i].type + " - " + x[i].value;

        if (value == "") //
        {
            //alert(x[i].id + " - " +  x[i].value);
            x[i].style.backgroundColor = "#ffffbf";    // light yellow = #ffffe0
            document.getElementById("PageValidate").value = document.getElementById("PageValidate").value + "~" + id;

        }
        else //
        {
            //x[i].style.backgroundColor = "#eff5ef";   // light green
        }

    }  // end of #1



    // ===========================================================================
    if (document.getElementById("PageValidate").value != "")  // required (class=myMust) field with no data
    {
        document.getElementById("divHeader").innerHTML =
        "<div class='w3-panel w3-pale-yellow w3-center w3-display-container'>" +
        "<span onclick=\"this.parentElement.style.display='none'\"" +
        "class='w3-button w3-large w3-display-topright'>&times;</span>" +
        "<p>" + pageAlert + "</p>" +
        "</div>";

        return false;
    }
    else {
        // no error and some data changed
        if (document.getElementById("PageValidate").value == "" && document.getElementById("PageChanged").value != "") //
        {
            return true;
        }

    }

}

function Action_Rules() // 
{
    //alert("Action_Rules - START");   ValidEmail("ddo@yaho.com")

    var sql, data, pageAlert = "", t = "";
    var coID, mmddyyyy;
    var today = new Date();

    mmddyyyy = myDate('aDate', 'aFormat');
    coID = document.getElementById("coID").value;

    if (coID == "NEW") // 
    {   // #2

        // ------------------------------------------------------------
        var fldPONeedDate = new Date(document.getElementById("fldPONeedDate").value);
        var fldWorkBeginDate = new Date(document.getElementById("fldWorkBeginDate").value);
        var fldWorkEndDate = new Date(document.getElementById("fldWorkEndDate").value);

        //fldPONeedDate       = YYYYMMDD(fldPONeedDate);
        //fldWorkBeginDate    = YYYYMMDD(fldWorkBeginDate);
        //fldWorkEndDate      = YYYYMMDD(fldWorkEndDate);

        today = YYYYMMDD(today);

        // alert( YYYYMMDD(fldPONeedDate) + " ------ " + today);
        // ------------------------------------------------------------

        if (fldPONeedDate == "Invalid Date" || YYYYMMDD(fldPONeedDate) < today) //
        {
            t = "Date entries are required and must be >= today";
            document.getElementById("fldPONeedDate").style.backgroundColor = "#ffffe0"; //  "#ffff00";  // bright yellow
        }

        if (fldWorkBeginDate == "Invalid Date" || YYYYMMDD(fldWorkBeginDate) < today) //
        {
            t = "Date entries are required and must be >= today";
            document.getElementById("fldWorkBeginDate").style.backgroundColor = "#ffffe0";  // bright yellow
        }
        if (fldWorkEndDate == "Invalid Date" || YYYYMMDD(fldWorkEndDate) < today) //
        {
            t = "Date entries are required and must be >= today";
            document.getElementById("fldWorkEndDate").style.backgroundColor = "#ffffe0";  // bright yellow
        }

        //alert("check date - " + t);
        if (t != "") //
        {
            PopAlert(t, 'idstring');
            return "ERROR";
        }

        //alert(document.getElementById("fldWTC").value);







    }   // end of #2 NEW

    // ----------------------------------------------------


    var cost = document.getElementById("fldEstimateTotal").value;

    if (cost == "0" || cost == "0.00" || cost == "$0.00")  //
    {
        // alert("fldEstimateTotal");
        document.getElementById("fldEstimateTotal").style.backgroundColor = "#ffffbf";
        document.getElementById("PageValidate").value = document.getElementById("PageValidate").value + "~fldEstimateTotal";
        t = "Please complete cost section";
        PopAlert(t, 'idstring');
        return "ERROR";

    }

    i = parseInt(document.getElementById("fldOtherCost").value);
    if (i > 0) //
    {
        if (document.getElementById("fldOtherCostDesc").value == "") //
        {
            document.getElementById("fldOtherCost").style.backgroundColor = "#E9967A";
            document.getElementById("fldOtherCostDesc").style.backgroundColor = "#ffffbf";
            return "ERROR";
        }
    }
    // ---------------------------------------------------------------
    t = document.getElementById("fldSupplierContactEmail").value;
    if (ValidEmail(t) != true) {
        document.getElementById("fldSupplierContactEmail").style.backgroundColor = "#ffff00";  // bright yellow
        return "ERROR";
    }

    //alert(document.getElementById("fldWTC").value) ;
    if (document.getElementById("fldWTC").value == "800.9") //
    {
        t = "MOC is required";
        if (document.getElementById("fldMOCNumber").value == "") //
        {
            document.getElementById("fldWTC").style.backgroundColor = "#E9967A";
            document.getElementById("fldMOC").style.backgroundColor = "#ffffe0";  // bright yellow
            document.getElementById("fldMOCNumber").style.backgroundColor = "#ffffe0";  // bright yellow
            return "ERROR";
        }

    }

    // alert("check Supplier");
    var i = 0;

    if (document.getElementById("fldAMSLSupplier").value != "") i = i + 1;
    if (document.getElementById("fldNonAMSLSupplier").value != "") i = i + 1;
    if (document.getElementById("Other_Supplier").value != "") i = i + 1;

    if (i != 1) //
    {
        t = "You must select ONE type of supplier";
        document.getElementById("fldAMSLSupplier").style.backgroundColor = "#ffff00";  // bright yellow
        document.getElementById("fldNonAMSLSupplier").style.backgroundColor = "#ffff00";  // bright yellow
        document.getElementById("Other_Supplier").style.backgroundColor = "#ffff00";  // bright yellow

        if (t != "") //
        {
            PopAlert(t, 'idstring');
            return "ERROR";
        }
    }

    // ------------------------------------------------------
    // alert("checking Supplier - step 2");
    t = "";
    if (document.getElementById("Other_Supplier").value != "") //
    {
        if (document.getElementById("fldEHSDeliveryLeader").value == "")  //
        {
            t = "\nRequirements for Other Supplier";
            document.getElementById("fldEHSDeliveryLeader").style.backgroundColor = "#ffff00";  // bright yellow
        }
        if (document.getElementById("fldHoldHarmlessWaiver").value == "") //
        {
            t = "\nRequirements for Other Supplier";
            document.getElementById("fldHoldHarmlessWaiver").style.backgroundColor = "#ffff00";
        }
        if (document.getElementById("fldSafetyWaiver").value == "") //
        {
            t = "\nRequirements for Other Supplier";
            document.getElementById("fldSafetyWaiver").style.backgroundColor = "#ffff00";
        }
    }

    if (t != "") //
    {
        document.getElementById("Other_Supplier").style.backgroundColor = "#E9967A";
        PopAlert(t, 'idstring');
        return "ERROR";
    }

    if (document.getElementById("fldMOC").value == "True" && document.getElementById("fldMOCNumber").value == "") //
    {
        t = "MOC Number required";
        document.getElementById("fldMOC").style.backgroundColor = "#E9967A";
        document.getElementById("fldMOCNumber").style.backgroundColor = "#ffff00";
        return "ERROR";
    }

    if (document.getElementById("fldTA").value == "True" && document.getElementById("fldTAID").value == "") //
    {
        t = "Turn Around number required";
        document.getElementById("fldTA").style.backgroundColor = "#E9967A";
        document.getElementById("fldTAID").style.backgroundColor = "#ffff00";
        return "ERROR";
    }

    // --------------------------------------
    //alert("check Hazard 1");
    t = "";

    if (document.getElementById("Hazard").value == "True") //
    {   // hazard
        // Only C or A are allowed
        if (document.getElementById("fldBadgeType").value == "B" || document.getElementById("fldBadgeType").value == "D") //
        {
            document.getElementById("Hazard").style.backgroundColor = "#E9967A";
            document.getElementById("fldBadgeType").style.backgroundColor = "#ffff00";
            t = "Badge type not correct. Only C or A allowed.";
            if (t != "") //
            {
                document.getElementById("Hazard").style.backgroundColor = "#E9967A";
                PopAlert(t, 'idstring');
                return "ERROR";
            }
        }

        // --------------------------------------------
        // Badge Type A needs additional info

        t = ""
        if (document.getElementById("fldBadgeType").value == "A") // 
        {
            if (document.getElementById("fldEHSDeliveryLeader").value == "")  //
            {
                t = "Missing info for type A";
                document.getElementById("fldEHSDeliveryLeader").style.backgroundColor = "#ffff00";  // bright yellow
            }

            if (document.getElementById("fldHoldHarmlessWaiver").value == "") //
            {
                t = "Missing info for type A";
                document.getElementById("fldHoldHarmlessWaiver").style.backgroundColor = "#ffff00";
            }

            if (document.getElementById("fldSafetyWaiver").value == "") //
            {
                t = "Missing info for type A";
                document.getElementById("fldSafetyWaiver").style.backgroundColor = "#ffff00";
            }
        }

        if (t != "") //
        {
            document.getElementById("fldBadgeType").style.backgroundColor = "#E9967A";
            PopAlert(t, 'idstring');
            return "ERROR";
        }




        //}  // H2
    } // hazard

    // Finally
    return "DONE";

    // * * * * * * * * * * * * * * * * * * * * * * * * * 

}


function Action_Alert() // 
{
    document.getElementById("DivHeader").style.backgroundColor = "#ffff00";
}