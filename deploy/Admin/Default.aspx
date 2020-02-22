<%@ page title="" language="C#" autoeventwireup="true" inherits="Admin_Default, App_Web_default.aspx.fdf7a39c" theme="ThemeOne" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="media/style.css" type="text/css" rel="stylesheet">
    <link href="media/master.css" type="text/css" rel="stylesheet">

    <script language="JavaScript" src="../include/CommonFuctions.js"></script>

    <script language="JavaScript" src="../include/jscript.js"></script>

    <script language="JavaScript" src="../include/Datepicker.js"></script>

</head>
<body class="adminbody">
    <form id="form1" runat="server">
    <div class="menu">
        <table border="0px" cellpadding="0px" cellspacing="0px" Style="display: block; border: 2px outset #98BF21">
            <tr height="76px" valign="top">
                <td class="header" colspan="2">
                </td>
            </tr>
            <tr>
                <td>
                    <a href="../admin/OB/default.aspx">
                        <img src="Media/ObjectBrowser.gif" alt="Object Browser" title="Object Browser" class="menuImage" /></a>
                </td>
                <td>
                    <ul>
                        <li>Order Management</li>
                        <li>Invoice Management</li>
                        <li>Stock Management</li>
                        <li>Article</li>
                        <li>Necessary Tables</li>
                    </ul>
                </td>
            </tr>
            <tr>
                <td>
                    <ul>
                        <li>Batch Import</li>
                        <li>Batch Update</li>
                    </ul>
                </td>
                <td>
                    <a href="../admin/Import.aspx">
                        <img src="Media/Import.gif" alt="Import" title="Import" class="menuImage" /></a>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="../admin/Report.aspx">
                        <img src="Media/Report.gif" alt="Report" title="Report" class="menuImage" /></a>
                </td>
                <td>
                    <ul>
                        <li>Vat Analysis Report</li>
                        <li>Sales Analysis Report</li>
                        <li>Sales Statement</li>
                    </ul>
                </td>
            </tr>
        </table>
    </div>
    <%--    <div class="menu">
        <a href="../admin/OB/default.aspx">
            <div class="obrowser" style="float: left">
            </div>
            <div style="height: 136px">
                Object Browser
            </div>
        </a><a href="../admin/Import.aspx">
            <div class="import" style="float: left">
            </div>
            <div style="height: 136px">
                Import
            </div>
        </a><a href="../admin/Report.aspx">
            <div class="report" style="float: left">
            </div>
            <div style="height: 136px">
                Report
            </div>
        </a>
    </div>
--%>
    </form>
</body>
</html>
