function Cbo_Populate(cboCategory, cboID, cboValue) //
{
    //alert("Cbo_Populate().\n" + cboCategory + ' - ' + cboID + ' - ' + cboValue);

    /* ---------------------------------------------------
    In some case you need to point to an existing array.
    Example: Shipping_Status, Customer_Status should use the Status array.
    Or you can point to a total different array. Ex. HR Status is diffreent from Inventory Status
    */

    // Specify the Array
    var cboArray;
    cboArray = LookupArrays;    // in LookupArray.js  

    // -------------------------------------------
    var myCbo = document.getElementById(cboID);
    var myCboValue = document.getElementById(cboID).value;
    myCbo.options.length = 0;

    // Add a blank option
    myOption = document.createElement("option");
    myOption.value = '';
    myOption.text = '';
    myCbo.add(myOption);
    // ---------------------------------------------------

    var cbo = "", Category, Value, Display, myOption;
    var chop = [];
    var found = "no", t = "";
    
    for (var i = 0; i < cboArray.length; i++) //   (var i = 0; i < LookupArrays.length; i++) //
    {

        Category = cboArray[i][0];
        Value = cboArray[i][1];
        Display = cboArray[i][2];
        
        // alert(Category + " - " + Value);

        if (Category.toUpperCase() == cboCategory.toUpperCase()) //
        {

            myOption = document.createElement("option");
            myOption.value = cboArray[i][1];
            myOption.text = cboArray[i][2];
            myOption.style.color = "blue";
            
            myCbo.add(myOption);

            //NOT WORKING if (myOption.value.toUpperCase == myCboValue.toUpperCase) // real value. Need to convert to UPPER Case 
            if (myOption.value == myCboValue) // real value.  
            {
                myCbo.remove(0);  // now we found it. remove the original first option
                myCbo.value = myOption.value; // make it selected
                found = "yes";
            }

        }

    }

    if (found == "no")     // the current value is not found in the list. 
    {
        // alert("not found. " + cboValue);
        myOption = document.createElement("option");
        myOption.value = cboValue;
        myOption.text = cboValue;
         
        myOption.style.color = "red";
        myOption.style.backgroundColor = "yellow";
        myCbo.add(myOption);

        myCbo.style.backgroundColor = "yellow";
        myCbo.value = cboValue; // selecte the original value

    }

    // document.getElementById(cboID).value = cboValue;
    // var myCboValue = myCbo.options[myCbo.selectedIndex].value;
    // alert('hereee .' + myCbo + ' - ' +  myCboValue);



}



function YYYYMMDD(aDate) //
{
    //alert("YYYYMMDD. " + aDate);

    var mm, dd, yyyy, myDate = "";
    var today = new Date(aDate);
    mm = "0" + (today.getMonth() + 1);
    mm = mm.substring(mm.length - 2, mm.length);
    dd = "0" + today.getDate();
    dd = dd.substring(dd.length - 2, dd.length);

    yyyy = today.getFullYear();
    myDate = yyyy + '-' + mm + '-' + dd;

    return myDate;

}





