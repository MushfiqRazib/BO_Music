<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="newsdetails, App_Web_newsdetails.aspx.cdcab7d2" title="news details" theme="ThemeOne" %>

<%@ MasterType VirtualPath="~/MainLayOut.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" style="width: 882px">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="background-color: white; width: 882px">
                    <tr>
                        <td valign="top" align="left" class="pageLocation">
                            <%--<asp:Label ID="lblCurrentPage" runat="server" Font-Bold='true' Text="">  </asp:Label>
                            &nbsp;&nbsp;<b>:</b>&nbsp;&nbsp;
                            <asp:Label ID="lblPageRoot" runat="server" ForeColor="AppWorkspace" Text=" "></asp:Label>
                            <asp:Label ID="lblActivePage" runat="server" ForeColor="#3300ff" Text=" ">
                            </asp:Label>--%>
                        </td>
                        <td align="right" valign="top" height="25px">
                            <table id="tblBtn" cellpadding="0" cellspacing="1" border="0" class="newsDetailsTDStyle1">
                                <tr>
                                    <td style="padding-left:3px; padding-top:2px; padding-bottom:3px;">
                                        <asp:ImageButton ID="btnGoback" runat="server" OnClick="btnGoback_Click" /></td>
                                    <td style="padding-right:2px; padding-top:2px; padding-bottom:3px;">
                                        <asp:ImageButton ID="btnNewsArchive" runat="server" OnClick="btnNewsArchive_Click1" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" runat="server" style="height: 5px; width: 834px;">
            </td>
        </tr>
        <tr>
            <td colspan="2" runat="server" id="header" valign="top" style="height: 19px; width:882px;">
            </td>
        </tr>
        <!-- News Template starts-->
        <tr>
            <td id="newsTD">
                <asp:GridView ID="grdNews" runat="server" ShowHeader="false" BorderWidth="0" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr style="background-color: #E5E5E5;">
                                        <td colspan="3" align="left">
                                            <table border="0">
                                                <tr>
                                                    <td align="left" height="10">
                                                        <b></b>
                                                        <%# DataBinder.Eval(Container.DataItem, "subject")%>
                                                    </td>
                                                    <td align="center" width="20" height="10">
                                                        <img id="imgBar" height="19" src="graphics/bg_menu.gif" /></td>
                                                    <td align="center" height="10" width="105">
                                                        Date:
                                                        <%# DataBinder.Eval(Container.DataItem, "date")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="background-color:#ffffff; padding-top:5px; padding-bottom:10px">
                                        <td align="left" valign="top" colspan="3">
                                            <p align="left">
                                                <span style="font-size: 9pt; font-family: Tahoma">
                                                    <%# ShowImage(DataBinder.Eval(Container.DataItem, "newsimagefile").ToString())%>
                                                </span>
                                            </p>
                                            <p align="justify">
                                                <span>
                                                    <%# DataBinder.Eval(Container.DataItem, "description")%>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <!-- News Template Ends-->
    </table>
</asp:Content>
