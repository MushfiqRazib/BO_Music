<%@ page language="C#" autoeventwireup="true" inherits="Admin_ImportExcelToDb, App_Web_import.aspx.fdf7a39c" title="Import / Update records" theme="ThemeOne" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../include/style.css" type="text/css" rel="stylesheet">
    <link href="media/style.css" type="text/css" rel="stylesheet">
    <link href="media/master.css" type="text/css" rel="stylesheet">

    <script language="JavaScript" src="../include/CommonFuctions.js"></script>

    <script language="JavaScript" src="../include/jscript.js"></script>

    <script language="JavaScript" src="../include/Datepicker.js"></script>

</head>
<body class="adminbody">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" height="450" width="882" border="0"
            class="contentArea">
             <tr height="76" valign="top">
                <td class="header">
                </td>
            </tr>
           <tr>
                <td align="left" valign="top" colspan="2">
                    <table style="width: 100%" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td align="left" class="contentHeader">
                                
                                    Import
                               
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 60%; background-color: white; height: 250px" valign="middle">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr style="background-color: #efefef">
                            <td style="height: 20px">
                                Select the table name
                            </td>
                            <td style="height: 20px">
                                <asp:DropDownList ID="DropDownList1" runat="server" Height="20px" Width="250px" AutoPostBack="True"
                                    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                    <asp:ListItem>article</asp:ListItem>
                                    <asp:ListItem>publisher</asp:ListItem>
                                    <asp:ListItem>composer</asp:ListItem>
                                    <asp:ListItem>customer</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 15px; vertical-align: top; text-align: left">
                                Select an Excel file
                            </td>
                            <td style="height: 15px;">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FileUpload1"
                                    Display="None" ErrorMessage="proper Excel file(.xls/.xlsx) should be given "
                                    ValidationExpression="^.+\.((xls)|(xlsx))$"></asp:RegularExpressionValidator>
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="250px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Have to select a Excel file"
                                    ControlToValidate="FileUpload1" Display="None"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="background-color: #efefef">
                            <td colspan="2" style="height: 20" align="right">
                                &nbsp;
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Import" Width="87px"
                                    Style="margin-left: 2px" />
                            </td>
                        </tr>
                    </table>
                    <table runat="server" id="tblEvents">
                        <tr style="background-color: #efefef">
                            <td style="height: 20px; width: 120px;">
                                Select an Event
                            </td>
                            <td style="height: 20px; width: 250px">
                                <asp:DropDownList ID="drpdlEvents" runat="server" Height="20px" Width="250px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="background-color: #efefef">
                            <td colspan="2" style="height: 20px" align="right">
                                <asp:Button ID="btnUpdateArticle" runat="server" OnClick="btnUpdateArticle_Click"
                                    Text="Update Events" Width="120px" Style="margin-left: 0px" />
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" />
                </td>
                <td style="background-color:white">
                    Select Column(s) to update<br />
                    <span style="color: Blue">No need to select PRIMARY KEY</span>
                    <asp:ListBox ID="lstFields" runat="server" Width="200px" Height="150px" SelectionMode="Multiple" />
                    <br />
                    <asp:Button ID="btnUpdateRecords" runat="server" Text="Update Records" Width="200px"
                        OnClick="btnUpdateRecords_Click" />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" class="loginFooter" valign="top">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
