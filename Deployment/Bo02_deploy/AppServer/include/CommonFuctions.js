// JScript File
var player = 1;
	bName = navigator.appName;
	bVer = parseInt(navigator.appVersion);

function clickButton(e, buttonid)
{ 
    var bt = document.getElementById(buttonid); 
    if (e.keyCode == 13)
    { 
        bt.click(); 
        return false; 
    } 
}

function GetObject(id) {
    return document.getElementById(id);
//	var elm	= document.aspnetForm.elements;
//	for(i=0; i<elm.length; i++)
//	{
//		if(elm[i].id.toLowerCase().indexOf(id.toLowerCase())>-1)
//		{
//		    return elm[i];
//		}
//        
//	}
}
function GetObjectID(objId){
    return document.getElementById(id);

//	var elm	= document.aspnetForm.elements;
//	for(i=0; i<elm.length; i++)
//	{
//		if(elm[i].id.indexOf(objId)>-1)
//		{
//		    return elm[i].id;
//		}
//       
//	}
}
function CopyValue(fromObj, toObj){
    objFrom= GetObject(fromObj);
    objTo =  GetObject(toObj);
    objTo.value = objFrom.value;
}
/*
    Provas Kumar Saha
    Date : June 30, 2007
    Email: provasks@hotmail.com
    
    This function will select or deselect all checkboxes
    taking the groupname of a checklist.
    if group name is undefined then it will select/deselect all checkboxes exist 
    in the page otherwise it will select/deselect all checkboxes under a group
*/
function SelectDeselectAll(group){
	var elm	= document.aspnetForm.elements;
 	if(group==undefined){                   //Select / Deselect all checkboxes
	    for(i=0; i<elm.length; i++){
		    if(elm[i].type=="checkbox"){
		        elm[i].checked=!elm[i].checked;
		    }
	    }
        
 	}
 	else{
	    for(i=0; i<elm.length; i++){        //Select / Deselect all checkboxes under a group
		    if(elm[i].id.indexOf(group)>-1){
		        elm[i].checked=!elm[i].checked;
		    }
	    }
	}
}
function SelectDeselectCheck(val)
{
   
	var elm	= document.aspnetForm.elements;
	    for(i=0; i<elm.length; i++)
        {
		    if(elm[i].type=="checkbox" && elm[i].disabled==false)
            {
		        elm[i].checked=val;
		    }
        }

}

/*
Test functions
*/
function PadZero(val, minLength) {
    var MANY_ZEROS = "000000000000000000";
    if (typeof(val) != "string")
        val = String(val);
    return (MANY_ZEROS.substring(0, minLength - val.length)) + val;
}

function ChangeTitle(title) { 
    document.title = title; 
}

/*
Zebra styled table
*/
function init () {
	var tables = document.getElementsByTagName("table");
	for (var i = 0; i < tables.length; i++) {
	  if (tables[i].className.match(/zebra/)) {
	    zebra(tables[i]);
	  }
	}
}

function zebra (table) {
	var current = "oddRow";
	var trs = table.getElementsByTagName("tr");
	for (var i = 0; i < trs.length; i++) {
	  trs[i].className += " " + current;
	  current = current == "evenRow" ? "oddRow" : "evenRow";
	}
}

function pause(milliseconds)
{
    var now = new Date();
    var exitTime = now.getTime() + milliseconds;

    while(true)
    {
        now = new Date();
        if(now.getTime() > exitTime) return;
    }
}


//checks valid input characters when onkeypress event
function CheckNumericKeyStroke(e, val, validChars) {
        var key = window.event ? e.keyCode : e.which;
	    var keychar = String.fromCharCode(key);

	    return (IsValidKey(key) || IsNumeric(val += keychar, validChars));

	}
	function IsValidKey(key) {
	    var validKeys = [0, 8];
	    for (vKey in validKeys) {
	        if (validKeys[vKey] == key)
	            return true;
	    }
	    return false;
	}
    //checks to see valid characters
    function IsNumeric(sText,ValidChars)
    {
       //var ValidChars = "0123456789,";
       var IsNumber=true;
       var decimalCount=0;
       var Char;       

       for (i = 0; i < sText.length && IsNumber == true; i++) 
       { 
          Char = sText.charAt(i); 
          if(Char==',')
          {
            decimalCount++;
          }
          if (ValidChars.indexOf(Char) == -1) 
          {
             IsNumber = false;
          }
       }
       if(decimalCount>1)
          IsNumber = false;
       return IsNumber;
    }
    
   //checks valid input characters when onchange event
    function CheckNumeric(control,defaultValue,validChars)
    {      
      var length = control.value.length;      
      var status = IsNumeric(control.value,validChars);           
      var qty =GetObject(defaultValue);      
      //var qty = document.getElementById("<%=hiddenQty.ClientID %>");
       if(status == false || length==0)
         {
           control.value = qty.value;
         }      
      
      return;
    }
    
    function CheckCorrectValue(control,validChars)
    {
      var length = control.value.length;      
      var status = IsNumeric(control.value,validChars);
      if(status == false || length==0)
         {

              control.value = prev;
         }      
    }
    function SetLatestValue(val){       
        prev=val;        
    }

    function showalert()
    {
        alert("OK");
    }
function OpenPlayer(code)
{
 if(false == player.closed)                 
    player.close ();                 
    
                 
    if(code)
    {
        player=window.open("MusicPlayer.aspx?code="+code+"", "", "width=420, height=295, toolbar=no, scrollbars=no, left=350, top=300");
    }
    else
    {
        player=window.open("MusicPlayer.aspx","","width=420, height=295, toolbar=no, scrollbars=no, left=350, top=300");    
    }
    return false;
}


function SwitchPlaylist(id,code)
{
    var Controlobj = document.getElementById(id);
    var curleftPos =  0;        
    var curtopPos = 0;	
    if (Controlobj.offsetParent)        
        {         
            do {        	
                    curleftPos += Controlobj.offsetLeft;		
                    curtopPos += Controlobj.offsetTop;     	    
                } while (Controlobj = Controlobj.offsetParent);        
         }
//  alert(curleftPos );
//  alert(curtopPos );
   //debugger
   var divPlaylist = document.getElementById("divPlaylist");
   var height = divPlaylist.offsetHeight;
   var width = divPlaylist.offsetWidth;
  
   divPlaylist.style.left = curleftPos+"px";
   divPlaylist.style.top = curtopPos+23+"px";
   divPlaylist.style.zIndex = 1000;   
   divPlaylist.style.visibility = divPlaylist.style.visibility=="hidden"?"visible":"hidden";
   if(divPlaylist.style.visibility =="hidden")
   {
    document.getElementById("UcPlaylist").style.display= "none";
   }
   else
   {
   document.getElementById("UcPlaylist").style.display= "block";
   document.getElementById("UcPlaylist").style.position="fixed";
   
   }
   
   //divPlaylist
    //OpenPlayer(code);
    return false;
}
 function ShowPlayListItems(enablediv, disablediv, current)
 {
 //debugger
    enablediv = document.getElementById(enablediv);
    disablediv = document.getElementById(disablediv);
    document.getElementById("_ctl0_playlist_lblCurrentPlaylist").innerHTML = current;
//    highlight = document.getElementById(highlight);
//    retrieve = document.getElementById(retrieve);
//            
//    highlight.style.backgroundColor="red";
//    retrieve.style.backgroundColor="#800728";
    
    enablediv.style.height = "350px";
    disablediv.style.height ="0px";
    enablediv.style.visibility="visible";
    disablediv.style.visibility="hidden";    
    return false;
 }
 function ClosePlayList()
 {
     var divPlaylist = document.getElementById("divPlaylist");
     divPlaylist.style.visibility="hidden";
     document.getElementById("UcPlaylist").style.display = "none";
 }
 
 function getQuerystring(key, default_)
 {
  if (default_==null) default_="";
  key = key.replace(/[\[]/,"\\\[").replace(/[\]]/,"\\\]");
  var regex = new RegExp("[\\?&]"+key+"=([^&#]*)");
  var qs = regex.exec(window.location.href);
  if(qs == null)
    return default_;
  else
    return qs[1];
 }
     