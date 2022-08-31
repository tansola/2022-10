function App_Hello()
{
    alert("App.js - App_Hello()");
}





function DoButton(theButton, url) {
    // alert("@DoButton - " + theButton.id);
    alert(url);
    openRightMenu();

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

function DoWarning(controlID, msg) {
    //alert(cID + ' - ' + msg);
    document.getElementById(controlID).style.backgroundColor = "#FF8F66";
    document.getElementById(controlID).title = msg;
}


function DummyTable() //
{
    var now = new Date();
        
    var t = "<table border=1 class='w3-table-all w3-small'>" +
                    "<tr><td>App.js</td><td>DummyTable()</td><td>A</td><td>A</td><td>A</td><td>A</td><td>A</td><td>A</td></tr>" +
                    "<tr><td>App.js</td><td>DummyTable()</td><td>A</td><td>A</td><td>A</td><td>A</td><td>A</td><td>A</td></tr>" +
                    "<tr><td>A</td><td>A</td><td>A</td><td>A</td><td>A</td><td>A</td><td>A</td><td>A</td></tr>" +
                    "<tr><td>A</td><td>A</td><td>A</td><td>A</td><td>A</td><td>A</td><td>A</td><td>A</td></tr>" +
                    "</table><br>" + now;

    return t;
}