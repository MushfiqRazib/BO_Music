<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Boeijenga Music - Object Browser</title>
    <!-- ***************  CSS Links ***************** -->
    <link href="ext/resources/css/ext-all.css" rel="stylesheet" type="text/css" />
    <link href="styles/obrowser.css" rel="stylesheet" type="text/css" />
    <link href="styles/extOverrideStyles.css" rel="stylesheet" type="text/css" />
    <!--[if IE 6]>
        <link href="styles/ie6.css" rel="stylesheet" type="text/css" />
    <![endif]-->

    <script type="text/javascript">
    
    window.onunload = CookieUpdate;
    
    function CookieUpdate()
    {
        SaveUserSettingsInCookie();        
    }


    function GetOBServiceUrl() {
        return '<%= Page.ResolveUrl("~/admin/OB/OBServices.asmx") %>';
    }

    function GetWrapperServiceUrl() 
    {
        return '<%= Page.ResolveUrl("~/admin/OB/WrapperServices.asmx") %>';
    }    
               
    </script>

    <!-- ***************  Javascript Links ***************** -->

    <script src="ext/adapter/ext/ext-base.js" type="text/javascript"></script>

    <script src="ext/ext-all-debug.js" type="text/javascript"></script>

    <script src="Scripts/Common.js" type="text/javascript"></script>

    <script src="Scripts/Core/OBrowser.js" type="text/javascript"></script>

    <script src="Scripts/Core/Navigations.js" type="text/javascript"></script>

    <script src="Scripts/OBController.js" type="text/javascript"></script>

    <script src="Scripts/Core/ext-override.js" type="text/javascript"></script>

    <script src="Scripts/Wrapper/SettingsProcessor.js" type="text/javascript"></script>

    <script src="Scripts/Wrapper/Wrapper.js" type="text/javascript"></script>

    <script src="Scripts/Wrapper/Toolbar.js" type="text/javascript"></script>

    <script src="Scripts/Wrapper/WrapperServiceProxy.js" type="text/javascript"></script>

    <script src="Scripts/Wrapper/ActionControl.js" type="text/javascript"></script>

    <script src="Scripts/Wrapper/OrderManagementFunctions.js" type="text/javascript"></script>

    <script src="Scripts/Wrapper/InvoiceManagementFunctions.js" type="text/javascript"></script>

    <script src="Scripts/Wrapper/StockManagementFunctions.js" type="text/javascript"></script>

    <script type="text/javascript">
        function GetOBServiceUrl() {
            return '<%= Page.ResolveUrl("~/admin/OB/OBServices.asmx") %>';
        }

        function GetWrapperServiceUrl() 
        {
            return '<%= Page.ResolveUrl("~/admin/OB/WrapperServices.asmx") %>';
        }    
               
    </script>

    <script type="text/javascript" for="ADViewer" event="OnEndLoadItem(bstrItemType,vData,vResult)">     
      if (bstrItemType == 'DOCUMENT')
      {         
      
        // var ADViewer = document.getElementById("ADViewer");
        // var ECompViewer = ADViewer.ECompositeViewer;
        //ECompViewer.ToolbarVisible = false;     	       
        //ECompViewer.MarkupsVisible = false;
                  
      }      


    </script>

</head>
<body id="docbody">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
        <Services>
            <asp:ServiceReference InlineScript="true" Path="~/admin/OB/OBServices.asmx" />
            <asp:ServiceReference InlineScript="true" Path="~/admin/OB/WrapperServices.asmx" />
        </Services>
    </asp:ScriptManager>
    <div id="tabContainer">
    </div>
    <div id="nonReportTabPanel1" class="hideDiv" style="overflow: auto">
    </div>
    <div id="nonReportTabPanel2" class="hideDiv" style="overflow: auto">
    </div>
    <div id="nonReportTabPanel3" class="hideDiv" style="overflow: auto">
    </div>
    <div id="nonReportTabPanel4" class="hideDiv" style="overflow: auto">
    </div>
    <div id="reportContainer" style="width: 100%;">
        <div id="header-wrap">
            <div id="header-container">
                <div id="header">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <div style="float: left">
                                    Report:
                                    <select name="drpReportList" style="width: 150px;" id="drpReportList" onchange="LoadReportArguments()"
                                        class="font">
                                    </select>
                                    Group By:
                                    <select id="drpGroupBy" style="width: 130px;" onchange="InitReport()" class="font">
                                    </select>
                                </div>
                                <div style="float: right; padding-right: 15px">
                                    <div id="divSaveColor" style="float: left; margin-right: 2px">
                                        <a onclick="SaveSelectedThemeColors()">
                                            <img src="./images/save-colors.png" style="padding-top: 5px;" alt="Save Color" title="Save Color" /></a>
                                    </div>
                                    <div id="divAddRecord" style="float: left; margin-right: 2px">
                                        <a onclick="AddRecord(event)">
                                            <img src="./images/add.png" style="padding-top: 5px;" alt="Add Record" title="Add Record" /></a>
                                    </div>
                                    <div id="divSaveReportSettings" style="float: left; margin-right: 2px">
                                        <a onclick="SaveUsersCurrentSettings()">
                                            <img src="./images/save-settings.png" style="padding-top: 5px;" alt="Save Settings"
                                                title="Save Settings" /></a>
                                    </div>
                                    <div id="divReport" style="float: left; margin-right: 2px">
                                        <a onclick="OpenReportOption()">
                                            <img src="./images/report.png" style="padding-top: 5px;" alt="Create Report" title="Create Report" /></a>
                                    </div>
                                    <div id="divThemeColor" style="float: left; margin-right: 2px">
                                        <a name="cmdTheme" onclick="ShowThemeColor()">
                                            <img id="Img2" src="./images/themecolor.png" style="padding-top: 5px;" alt="Theme Color"
                                                title="Theme Color" /></a>
                                    </div>
                                    <div style="float: left;">
                                        <a onclick="OpenQueryBuilder()">
                                            <img src="./images/querybuilder.gif" style="padding-top: 5px;" alt="Query Builder"
                                                title="Query Builder" /></a>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="Obrowser">
            <div id="gridContainer" style="background-color: #eeffdd; position: relative; overflow: hidden;
                top: 1px; left: 1px;">
            </div>
        </div>
    </div>
    <div id="footer-wrap" style="vertical-align:middle">
        <div id="footer-container">
            <div id="footer">
                <div style="float: left; width: 275px;margin-left:20px">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <a onclick="OBSettings.GotoFirstPage()">
                                    <img src="./images/nav_firstpage.gif" alt="First page" /></a>
                            </td>
                            <td>
                                <a onclick="OBSettings.GotoPreviousPage()">
                                    <img src="./images/nav_prevpage.gif" alt="Previous page" /></a>
                            </td>
                            <td>
                                <a onclick="OBSettings.GotoPrevRow()">
                                    <img src="./images/nav_prevrow.gif" alt="Previous Item" /></a>
                            </td>
                            <td>
                                <input type="text" id="txtSelectedRow" style="width: 25px; height: 15px;" value="1"
                                    class="font" />
                            </td>
                            <td>
                                <a onclick="OBSettings.GotoNextRow()">
                                    <img src="./images/nav_nextrow.gif" alt="Next Item" /></a>
                            </td>
                            <td>
                                <a onclick="OBSettings.GotoNextPage()">
                                    <img src="./images/nav_nextpage.gif" alt="Next page" /></a>
                            </td>
                            <td>
                                <a onclick="OBSettings.GotoLastPage()">
                                    <img src="./images/nav_lastpage.gif" alt="Last page" /></a>
                            </td>
                            <td>
                                of&nbsp;&nbsp;
                            </td>
                            <td>
                                <label id="lblTotalRow">
                                </label>
                                &nbsp;&nbsp;
                            </td>
                            <td>
                                <input type="text" value="" style="width: 25px; height: 15px" id="txtGotoPage" class="font" />
                            </td>
                            <td>
                                <a onclick="OBSettings.GotoPage()">
                                    <img id="Img1" src="./images/gotopage.png" alt="Goto Page" /></a>
                            </td>
                            <td>
                                <a href="#">
                                    <img id="Img3" src="./images/separator.png" style="margin-left: 3px; margin-right: 3px;" /></a>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divSearch" style="float: left; margin-left: 5px; margin-top:-2px;">
                    <table cellpadding="0" cellspacing="2px" border="0">
                        <tr>
                            <td>
                                <label id="lblSortedFieldName">
                                </label>
                            </td>
                            <td>
                                <select id="quickSearchOperator" class="font">
                                    <option value="=" selected>=</option>
                                    <option value="&lt;&gt;">&lt;&gt;</option>
                                    <option value="&lt;">&lt;</option>
                                    <option value="&gt;">&gt;</option>
                                    <option value="&lt;=">&lt;=</option>
                                    <option value="&gt;=">&gt;=</option>
                                    <option value="%LIKE%">%LIKE%</option>
                                    <option value="%LIKE">%LIKE</option>
                                    <option value="LIKE%">LIKE%</option>
                                </select>
                            </td>
                            <td>
                                <input id="txtSearch" type="text" style="width: 98px;height:15px" class="font" />
                            </td>
                            <td>
                                <a onclick="OBSettings.QuickSearchOnUserData()">
                                    <img src="./images/filter.png" alt="Add to filter" /></a>
                            </td>
                            <td>
                                <a onclick="OBSettings.ClearFilterString()">
                                    <img src="./images/clear.png" id="btnClearFilter" alt="Clear filter" /></a>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divAction" style="float: left; margin-left: 5px; width: 150px;">
                    <%--<div>Met geselecteerde records :</div>
                    <div></div>--%>
                </div>
                <div style="float: right; padding-right: 5px;">
                    <table cellpadding="2" cellspacing="0" border="0">
                        <tr>
                            <td>Page Size:</td>
                            <td><input type="text" id="txtPageSize" style="width: 30px;" value="25" class="font" /></td>
                            <td><a onclick="OBSettings.RefreshPage()">
                        <img id="btnRefresh" src="./images/refresh.gif" alt="Refresh" style="margin-right:30px;" /></a></td>
                            </tr></table>
                </div>
            </div>
        </div>
    </div>
    <iframe id="iframUploadRedline" height="0px" width="0px"></iframe>
    <asp:HiddenField ID="hdnUserName" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnRoleName" runat="server" />
    </form>
</body>
</html>
