<%@ page language="C#" autoeventwireup="true" inherits="Admin_Report, Bo02" title="Reports" theme="ThemeOne" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../include/style.css" type="text/css" rel="stylesheet">
    <link href="media/style.css" type="text/css" rel="stylesheet">
    <link href="media/master.css" type="text/css" rel="stylesheet">
</head>
<body class="adminbody">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" width="882" border="0" 
            class="contentArea">
            <tr height="76" valign="top">
                <td class="header">
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    <table style="width: 100%" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td align="left" class="pageHeader">
                                <h4 class="contentHeader">
                                    Reports</h4>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 60%; background-color: white; height: 130px" valign="middle">
                    <table width="650" border="0" align="center" style="background-color: white;">
                        <tr style="background-color: #efefef">
                            <td style="text-align: left; width: 30px; height: 25px;">
                                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Admin/DateRange.aspx?analysis=vat">Go to</asp:LinkButton>
                            </td>
                            <td align="left" style="width: 100px; height: 25px;">
                                <asp:Label ID="Label1" runat="server" Text="VAT Analysis"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 30px; height: 25px;">
                                <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Admin/salesanalysis.aspx">Go to</asp:LinkButton>
                            </td>
                            <td align="left" style="width: 100px; height: 25px;">
                                <asp:Label ID="Label2" runat="server" Text="Sales Analysis"></asp:Label>
                            </td>
                        </tr>
                        <tr style="background-color: #efefef">
                            <td style="text-align: left; width: 30px; height: 25px;">
                                <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/Admin/DateRange.aspx?analysis=sales">Go to</asp:LinkButton>
                            </td>
                            <td align="left" style="width: 100px; height: 25px;">
                                <asp:Label ID="Label3" runat="server" Text="Sales Statement"></asp:Label>
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
