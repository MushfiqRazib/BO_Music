<%@ page language="C#" autoeventwireup="true" inherits="Admin_TableEditor_Record, Bo02" title="Record" culture="en-US" validaterequest="false" theme="ThemeOne" %>

<%@ Register Assembly="HawarIT.WebControls" Namespace="HawarIT.WebControls" TagPrefix="cc2" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<html>
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/style.css" type="text/css" rel="stylesheet">
    <link href="media/master.css" type="text/css" rel="stylesheet">
    <link href="Media/style.css" rel="stylesheet" type="text/css" />

    <script language="JavaScript" src="../include/CommonFuctions.js"></script>

    <script language="JavaScript" src="../include/jscript.js"></script>

    <script language="JavaScript" src="../include/Datepicker.js"></script>

    <script src="../include/TableEditor.js" type="text/javascript"></script>

</head>
<body class="adminbody">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
        <asp:UpdatePanel runat="server" ID="outerUpdatePanel" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" border="0" class="contentBackground" style="width: 640px">
                    <tr height="76px" valign="top">
                        <td class="header">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <table cellpadding="5" cellspacing="0" border="0" style="height: 20px; background-color: #DEDEDE;width:100%;">
                                <tr>
                                    <td valign="top" align="left">
                                        <asp:Label ID="lblTable" runat="server" Text="Table Name: " Font-Bold="true" ForeColor="red"></asp:Label><asp:Label
                                            ID="lblTableName" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="center">
                                        <asp:Label ID="lblErrorMesg" runat="server" Font-Bold="true" ForeColor="red" Font-Size="14px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <asp:Table ID="tblControl" CellPadding="3" CellSpacing="0" BorderWidth="1px" BorderStyle="Solid"
                                GridLines="Both" runat="server" Width="100%" BorderColor="white" />
                        </td>
                    </tr>
                </table>
                <asp:LinkButton runat="server" Style="display: none" ID="lnkHidden" />
                <ajaxToolkit:ModalPopupExtender runat="server" ID="modalPopupExtender2" TargetControlID="lnkHidden"
                    PopupControlID="pnlForeignKey" OkControlID="lnkOK" CancelControlID="lnkArticleCancel"
                    BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="height: 20px; background-color: #DEDEDE;width: 635px; padding:5px 0px 5px 5px">
            <asp:LinkButton ID="lnkSave" runat="server" Text="Insert" OnClick="lnkSave_Click" />
            <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CausesValidation="false"
                OnClick="lnkCancel_Click" />
        </div>
        <asp:Panel runat="server" ID="pnlForeignKey">
            <asp:UpdatePanel runat="server" ID="upnlForeignKey" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <cc2:PagingGridView ID="grdForeignKey" AllowSorting="True" AllowPaging="true" PageSize="10"
                        PagerSettings-Mode="NumericFirstLast" BorderWidth="0px" BorderStyle="Outset"
                        CellPadding="3" runat="server" AutoGenerateColumns="true" Width="550px" GridLines="none"
                        BorderColor="white" OnSorting="grdForeignKey_Sorting" OnPageIndexChanging="grdForeignKey_PageIndexChanging"
                        OnRowDataBound="grdForeignKey_RowDataBound">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="top" Height="10px" BackColor="#DEDEDE"
                            BorderColor="White"></HeaderStyle>
                        <AlternatingRowStyle BackColor="#EFEFEF" />
                        <RowStyle BackColor="White" VerticalAlign="Middle" />
                        <Columns>
                        </Columns>
                    </cc2:PagingGridView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="height: 20px; background-color: #EFEFEF;width: 545px; ">
                <asp:LinkButton runat="server" ID="lnkOK" Text="OK" Style="padding-left: 5px" />
                <asp:LinkButton runat="server" ID="lnkArticleCancel" Text="Cancel" />
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
