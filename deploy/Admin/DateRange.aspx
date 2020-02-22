<%@ page language="c#" autoeventwireup="true" enableeventvalidation="false" inherits="Admin_DateRange, App_Web_daterange.aspx.fdf7a39c" title="Date Range" theme="ThemeOne" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  	<LINK href="../include/style.css" type="text/css" rel="stylesheet">
    <LINK href="media/style.css" type="text/css" rel="stylesheet">
    <LINK href="media/master.css" type="text/css" rel="stylesheet">
   	<script language="JavaScript" src="../include/CommonFuctions.js"></script>
	<script language="JavaScript" src="../include/jscript.js"></script>
    <script language="JavaScript" src="../include/Datepicker.js"></script>
</head>
<body class="adminbody">
    <form id="form1" runat="server">
    <div>
       
        <table cellpadding="0" cellspacing="0" height="250px" width="882px" border="0"
            class="contentArea">
            <tr height="76px" valign="top">
                <td class="header">
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    <table style="width: 100%" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td align="left" class="contentHeader">
                               
                                    <asp:Label ID="lblAnalysis" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 60%; background-color: white;" valign="middle">
                    <table width="650px" height="100px" border="0" align="center" style="background-color: white;">
                        <tr style="background-color: #EFEFEF">
                            <td align="left" style="height: 38px">
                                <asp:Label ID="lblFilter" runat="server" Text="From"></asp:Label>
                            <td align="left" style="height: 38px;">
                                <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                <img width="20" height="20" style="vertical-align: bottom" onclick="displayDatePicker('<%=txtFromDate.ClientID%>', false, 'dmy', '-')"
                                    src="media/fTable.jpg" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 38px">
                                <asp:Label ID="Label1" runat="server" Text="To"></asp:Label>
                            </td>
                            <td align="left" style="height: 38px">
                                <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                <img width="20" height="20" style="vertical-align: bottom" onclick="displayDatePicker('<%=txtToDate.ClientID%>', false, 'dmy', '-');"
                                    src="media/fTable.jpg" />
                            </td>
                        </tr>
                        <tr style="background-color: #EFEFEF">
                            <td>
                            </td>
                            <td align="left" colspan="2" style="height: 38px; vertical-align: bottom">
                                <asp:Button ID="btnPdf" runat="server" Text="Print" OnClick="btnPdf_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="loginFooter" valign="top">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
