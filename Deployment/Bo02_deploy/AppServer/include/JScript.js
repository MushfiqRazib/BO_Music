/*
  Javascript file used by shoppingcart.aspx
  Developer : Sajib Sarkar
  Date      : 10.06.2007
  version   :1.0.0.1
*/

// var bName = navigator.appName;// Browser Name
// var bVer = parseInt(navigator.appVersion);//Browser Version
 
 
 
 function getCulturalFloat(obj,culture)
 {
    if(culture=="nl-NL")
    {
        obj=obj.replace(",",".");
        obj=parseFloat(obj).toFixed(2);
        return obj;
    } 
    else
    {
        obj=parseFloat(obj).toFixed(2);
        return obj;

    }
}



	
function getCulturalString(obj,culture)
{       
    obj=obj.toString();
    if(culture=="nl-NL")
    {
        obj=obj.replace(".",",");
        return obj;
    } 
    else
    {
        return obj;
    }
}


    
function getGrandTotalObj(seachSpan)
{

    var spanArray = document.getElementsByTagName('span');

    for(var i=0;i<spanArray.length;i++)
    {
        var spanId=spanArray[i].id;
        var find=spanId.search(seachSpan);
        if(find!=-1)
        {
            return spanArray[i];
        }
    }


}



function getUnitPrice(row)
  {

          if(bName.search("Microsoft")!=-1)
            {
          
            return row.childNodes[2].childNodes[1].innerHTML;
            }
            else
            {
              return  row.childNodes[3].childNodes[1].innerHTML;
            }

  }
  
  
  
  
  
  function getRowTotal(row)
  {

          if(bName.search("Microsoft")!=-1)
            {

            return row.childNodes[4].childNodes[1].innerHTML;
            }
            else
            {
              return row.childNodes[5].childNodes[1].innerHTML;
            }

  }
  
  
  
  
 function setRowTotal(row,rowTotal)
  {

          if(bName.search("Microsoft")!=-1)
            {

           row.childNodes[4].childNodes[1].innerHTML =rowTotal;
            }
            else
            {
           row.childNodes[5].childNodes[1].innerHTML =rowTotal;
            }

  }
  
  
  
  
    function calculate(ob)                 
    { 
      var validChars = "0123456789";
      var length = ob.value.length;
      var default_qty =1;      
      for(i = 0 ;i<length; i++)
      {
       Char = ob.value.charAt(i);
       if(validChars.indexOf(Char) == -1)
       {         
         ob.value = default_qty;
         break;
       }
      }
      if(length==0)
      {
      ob.value = default_qty;
      }
      
        var lblGrandTotal= getGrandTotalObj("lblTotalPrice");
        var culture="en-US";
        var grandTotal=lblGrandTotal.innerHTML;
       
        var findGrand=grandTotal.search(",");

        if(findGrand!=-1)
        {
            culture="nl-NL";
        }
        grandTotal=getCulturalFloat(grandTotal,culture);
     
        var quantity = ob.value;
        var row = ob.parentNode.parentNode;
        var unitPrice = getUnitPrice(row);
        unitPrice=getCulturalFloat(unitPrice,culture);
        var rowTotal =getRowTotal(row);
        rowTotal=getCulturalFloat(rowTotal,culture);
        var total=unitPrice*quantity;
        grandTotal =(grandTotal -rowTotal)+ total ;
        grandTotal=grandTotal.toFixed(2);
        setRowTotal(row,getCulturalString(total.toFixed(2),culture)) ;
        lblGrandTotal.innerHTML=getCulturalString(grandTotal,culture);
}

function GetInvoiceAddress(val)
{
 if(val.checked)
   {
    if( (GetObject("txtDFName")!=null && GetObject("txtDFName").value=="")&&
        (GetObject("txtDMName")!=null && GetObject("txtDMName").value=="")&&
        (GetObject("txtDLName")!=null && GetObject("txtDLName").value=="")&&
        (GetObject("txtDResidence")!=null && GetObject("txtDResidence").value=="")&&
        (GetObject("txtDPostCode")!=null && GetObject("txtDPostCode").value=="")&&
        (GetObject("txtDHousenr")!=null && GetObject("txtDHousenr").value=="")&&
        (GetObject("txtDAddress")!=null && GetObject("txtDAddress").value=="")
      )
      {
       CopyValue("txtFName","txtDFName");
       CopyValue("txtMName","txtDMName");
       CopyValue("txtLName","txtDLName");
       CopyValue("txtResidence","txtDResidence");
       CopyValue("txtPostCode","txtDPostCode");
       CopyValue("txtHousenr","txtDHousenr");
       CopyValue("txtAddress","txtDAddress");
       CopyValue("ddlCountry","ddlDCountry");
       CopyValue("ddlInitialName","ddlDInitialName");
      }
   }
}
