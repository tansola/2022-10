<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormLoad.aspx.cs" Inherits="FormLoad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />
    <link href="Css/App.css" rel="stylesheet" />

    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>

    <title></title>
    <script type="text/javascript">

        $(document).ready(function () {
            $('.popdate').each(function () {
                $(this).datepicker({ dateFormat: 'mm/dd/yy' });
            });

            $("input:text").focus(function () { $(this).select(); });

        });

    </script>


    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            var ele = document.getElementById("divLog");
            ele.style.display = "none";
        });

        /*
        function DoSpec(specUrl) {
            alert(specUrl);
        }

        function DoButton(theButton, url) {
            // alert("@DoButton - " + theButton.id);
            alert(url);

        }

        function DoLog() //
        {
            alert("DoLog() - In used ?")
            var ele = document.getElementById("divLog");
            //var text = document.getElementById(switchTextDiv);
            if (ele.style.display == "block") {
                ele.style.display = "none";
                //text.innerHTML = "restore";
            }
            else {
                ele.style.display = "block";
                //text.innerHTML = "collapse";
            }
        }
        */

        function DoEdit(theControl) //
        {
            alert("FormLoad.aspx. DoEdit(). In used ?")

            var fValue, cStatus, controlInfo;

            controlInfo = theControl.id + "~" + theControl.value;

            document.getElementById("Alert").value = controlInfo;  // reset for each control display

            cStatus = ValidateControl(theControl.id, theControl.value)      // App.js
            if (cStatus == "") //
            {
                fValue = theControl.value;
                fValue = fValue.replace("~", " ");   // ~ is reserved delim, not allow as data.
                controlInfo = theControl.id + "~" + fValue;  // add to pass the Value too

                document.getElementById("ToSave").value = document.getElementById("ToSave").value + "|" + controlInfo;
                theControl.style.backgroundColor = "bisque";
                document.getElementById(theControl.id).title = theControl.value;
                document.getElementById("ButtonSave").disabled = false;

            }
            else //
            {
                document.getElementById("Alert").value = controlInfo + " - " + cStatus;
                theControl.style.backgroundColor = "#FF8F66";            // ignore the controlID
                document.getElementById(theControl.id).title = cStatus;
                document.getElementById("ButtonSave").disabled = true;

            }


        }

        function DoWarning(controlID, msg) {
            //alert(cID + ' - ' + msg);
            document.getElementById(controlID).style.backgroundColor = "#FF8F66";
            document.getElementById(controlID).title = msg;
        }

        function DoBlur(obj) {
            //alert("@DoBlur - " + obj.id);

        }
        function DoFocus(obj) {
            //alert("@DoFocus - " + obj.id);

        }

        function isNumber(n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
            // function IsNumeric(num) { return (num >= 0 || num < 0);        }

        }


        function isDate(dateStr) {

            var datePat = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
            var matchArray = dateStr.match(datePat); // is the format ok?
            var validate = "";

            if (matchArray == null) {
                //validate = "Please enter date as either mm/dd/yyyy or mm-dd-yyyy.";
                validate = "Please enter date in format mm/dd/yyyy.";
                //alert(validate);
                return validate;
            }

            month = matchArray[1]; // p@rse date into variables
            day = matchArray[3];
            year = matchArray[5];

            if (month < 1 || month > 12) { // check month range
                validate = "Month must be between 1 and 12.";
                //alert(validate);
                return validate;
            }

            if (day < 1 || day > 31) {
                validate = "Day must be between 1 and 31.";
                //alert(validate);
                return validate;
            }

            if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {

                validate = "Month " + month + " does not has 31 days!";
                //alert(validate);
                return validate;
            }

            if (month == 2) { // check for february 29th
                var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
                if (day > 29 || (day == 29 && !isleap)) {
                    validate = "February " + year + " does not has " + day + " days!";
                    //alert(validate);
                    return validate;
                }
            }

            return validate; // "" = ok. 
        }

    </script>

    <script type="text/javascript">
        function openLeftMenu() {
            document.getElementById("leftMenu").style.display = "block";
        }

        function closeLeftMenu() {
            document.getElementById("leftMenu").style.display = "none";
        }

        function openRightMenu() {
            document.getElementById("rightMenu").style.display = "block";
        }

        function closeRightMenu() {
            document.getElementById("rightMenu").style.display = "none";
        }
    </script>


  
</head>

<body bgcolor="#DCDCDC">


    <form id="ThisForm" >
        

    
    </form>

</body>

</html>

