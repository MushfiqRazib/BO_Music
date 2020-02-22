<%@ page language="C#" autoeventwireup="true" inherits="errorpage, Bo02" title="Error Page" theme="ThemeOne" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3c.org/TR/1999/REC-html401-19991224/loose.dtd">
<html>
<head id="Head1" runat="server">
    <title>Error Page</title>
 	<META http-equiv=Content-Type content="text/html; charset=UTF-8">       
 	<LINK href="include/style.css"  media="screen" type="text/css" rel="stylesheet">
   	<script language="JavaScript" src="include/CommonFuctions.js"></script>
	<script language="JavaScript" src="include/jscript.js"></script>
    
    
    <META content="MSHTML 6.00.6000.16481" name=GENERATOR>
    
</head>
<body onload="init()" bgcolor="#A7A6A6">
    <form id="form1" runat="server">
    <div align=center>
 
        <DIV id=header>
        <DIV class=pad1></DIV>
         <table id="tableHeader" cellpadding=0 cellspacing=0 border=0 style="background-color:White;" width=916px>
         <tr align=left>
            <td align=left  valign=top class="errorPageStyle1"></td>
            <td align=left valign=top  >
                <table border=0 cellpadding="0" cellspacing="0">
                <tr>
                    <td align=left  valign=top class="errorPageStyle2"></td>
                </tr>
                 <tr>
                     <td align="left" >
                           
                                <table id="table2" align="center"  cellpadding="0" cellspacing="0"   border=0 width=100%>
                                    <tr height=76 valign=top>
                                        <td  class="errorPageStyle3">
                                            <table cellpadding="0" cellspacing="0" border=0px  style=width:100%>
                                                <tr >
                                                    <td align="right" style="height: 36px;width:882px;" valign=top >
                                                            <span>
                                                             
                                                              
                                                            </span>
                                                    </td>
                                                </tr>
                                            </table>                    
                                        </td>
                                    </tr>
                                    <tr style="height:19px; width:880px;" valign=top >
                                        <td height=19px valign="top">
                                            <asp:PlaceHolder  ID="phMenu" runat="server"></asp:PlaceHolder>
                                        </td>
                                    </tr>
                                    <tr><td width=80% height=8px></td></tr>                   
                               </table>
                        
                           
                          </td>
                </tr>
                </table>    
            </td>
            <td  align=left  valign =top  class="errorPageStyle4"></td>
        </tr>
      
    </table>
    </DIV>
    <DIV id=content align=center>
     <DIV class=pad2></DIV><div style="height:20px;"></div>
    <table id="tableContent" cellpadding=0 cellspacing=0 border=0 width=916px align=center style="background-color:White;"  >
        <tr >
            <td align=left valign=top class="errorPageStyle5"></td>
            <td valign ="top" style="padding-right:0px;height:450px"  align=center>
                <!-- Start of Content -->

                <table class="errorPageStyle6">
                <tr>
                    <td>
                        <table class="errorPageStyle7">
                        <tr>
                            <td align="center"  valign=top colspan="3" style="word-wrap:break-word; width:850px;">
                                <asp:Label ID="lblErrorMessage" runat="server"  ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                            </td>
                            <td style="width: 100px">
                            </td>
                            <td style="width: 100px">
                            </td>
                        </tr>
                        </table>
                    </td>
                </tr>
                </table>


                <!-- End of Content -->
            </td>
            <td align=left  valign=top  class="errorPageStyle8"></td>
        </tr>
        <tr>
             <td align=left valign=top  class="errorPageStyle9"></td>
             <td  valign=top>
                <table cellpadding="0" cellspacing="0" width=100%>
                    <tr style="background-color:#800728; height:19px;">
                        <td align="right" style="padding-right:9px; color:White;"><a href="#" style="text-decoration:none; color:white;">Disclaimer</a>&nbsp;&nbsp;&nbsp;<a href="#" style="text-decoration:none; color:white;">General terms</a></td>
                    </tr>
                </table>
             </td>
            <td align=left  valign=top  class="errorPageStyle8"></td>
           
        </tr>
        <tr>
             <td align=left valign=top  class="errorPageStyle10"></td>
             <td class="errorPageStyle11"></td>
            <td align=left  valign=top class="errorPageStyle12"></td>
           
        </tr>
        
    </table>
    <DIV class=pad2> </DIV>
    </div>
    
  
    </div>

    </form>
</body>
</html>