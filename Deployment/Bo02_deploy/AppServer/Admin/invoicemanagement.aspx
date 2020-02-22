<%@ page language="C#" culture="nl-NL" autoeventwireup="true" inherits="Admin_invoicemanagement, Bo02" title="Invoice Management" theme="ThemeOne" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        EnableViewState="false">
        <table cellpadding="0" cellspacing="0" class="contentArea">
            <tr>
                <td align="left" style="width: 883px; padding-left: 8px; padding-top: 8px; background-color: #DEDEDE;">
                    <h4 class="contentHeader">
                        Invoice Management</h4>
                </td>
            </tr>
            <tr>
                <td width="100%" height="15">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 882px; height: 50px;">
                    <table cellpadding="5" cellspacing="0" border="0">
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="lblFilter" runat="server" Text="Filter"></asp:Label>
                            </td>
                            <td valign="top" align="left">
                                <asp:DropDownList ID="drpInvoiceFilter" runat="server" CssClass="ComboHeight">
                                    <%--<asp:ListItem Selected="True" Value="1">Niet afgedrukt</asp:ListItem>--Not printed--%>
                                    <asp:ListItem Value="1" Selected="True">Nieuw</asp:ListItem>
                                    <%--New--%>
                                    <asp:ListItem Value="2">Verstuurd</asp:ListItem>
                                    <%--Send--%>
                                    <asp:ListItem Value="3">Geboekt</asp:ListItem>
                                    <%--Booked--%>
                                    <asp:ListItem Value="4">Alles</asp:ListItem>
                                    <%--Everything--%>
                                </asp:DropDownList>
                            </td>
                            <td valign="top" align="left">
                                <asp:ImageButton ID="btnOrderFilter" CssClass="ButtonHeight" ImageUrl="../graphics/btn_OK_nl.png"
                                    runat="server" OnClick="btnOrderFilter_Click"></asp:ImageButton>
                            </td>
                            <td valign="top" align="left">
                                <input id="txtHideValue" type="hidden" runat="server" />
                            </td>
                            <td align="center" valign="top" width="350">
                                <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="true" ForeColor="red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="grdInvoice" runat="server" AllowPaging="true" GridLines="both"
                        BorderColor="white" BorderWidth="1px" AllowSorting="true" AutoGenerateColumns="false"
                        CellPadding="5" CellSpacing="0" PagerSettings-Mode="Numeric" PageSize="7" PagerStyle-HorizontalAlign="Center"
                        OnPageIndexChanging="grdInvoice_PageChanging" OnSorting="grdInvoicer_Sorting"
                        Width="882px">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <input id="chkAll" onclick="javascript:SelectDeselectCheck(this.status)" type="checkbox"
                                        runat="server">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkInvoice" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Date" SortExpression="i.invoicedate">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "invoicedate")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                <HeaderStyle HorizontalAlign="Left" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice#" SortExpression="i.invoiceid">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "invoiceid")%>'
                                        OnCommand="lnkSelect_Command">
                                        <asp:Label ID="lblInvoiceNr" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "invoiceid")%>'></asp:Label></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="60px" />
                                <HeaderStyle HorizontalAlign="center" Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer" SortExpression="i.customer">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomer" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "customer")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                <HeaderStyle HorizontalAlign="Left" Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Address">
                                <ItemTemplate>
                                    <asp:Label ID="lblAddress" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "address")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="352px" />
                                <HeaderStyle HorizontalAlign="Left" Width="352px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "status")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="credit" HeaderText="Credit" SortExpression="credit" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPrint" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "invoiceid")%>'
                                        OnCommand="lnkPrint_Command">Print</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" Width="20px" />
                                <HeaderStyle HorizontalAlign="left" Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "invoiceid")%>'
                                        OnCommand="lnkEdit_Command">Edit</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" Width="20px" />
                                <HeaderStyle HorizontalAlign="left" Width="20px" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#DEDEDE" BorderColor="White" HorizontalAlign="Center" Font-Size="11px" />
                        <AlternatingRowStyle BackColor="#EFEFEF" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" VerticalAlign="Middle" HorizontalAlign="Center" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 883px; background-color: White;">
                    <table>
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="lblText" runat="server" Text="Met geselecteerde records:"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="drpAction" runat="server" CssClass="ComboHeight">
                                    <%--<asp:ListItem Selected="true" Value="1">Afdrukken Faktuur Blank</asp:ListItem>--%>
                                    <%--To print Faktuur--%>
                                    <asp:ListItem Selected="true" Value="2">Afdrukken Faktuur</asp:ListItem>
                                    <%--To print Faktuur--%>
                                    <%--<asp:ListItem Value="3">Afdrukken Invoice Blank</asp:ListItem>--%>
                                    <%--To print Invoice--%>
                                    <asp:ListItem Value="4">Afdrukken Invoice</asp:ListItem>
                                    <%--To print Invoice--%>
                                    <asp:ListItem Value="5">Boeken</asp:ListItem>
                                    <%--To book--%>
                                    <asp:ListItem Value="6">Verstuurd</asp:ListItem>
                                    <%--To send--%>
                                </asp:DropDownList>
                            </td>
                            <td align="left" valign="top">
                                <asp:ImageButton ID="btnAction" CssClass="ButtonHeight" ImageUrl="../graphics/btn_OK_nl.png"
                                    runat="server" OnClick="btnAction_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="grdInvoiceLine" runat="server" BorderColor="White" BorderWidth="1px"
                        AutoGenerateColumns="False" CellPadding="5" Width="882px">
                        <Columns>
                            <asp:TemplateField HeaderText="Order Nr.">
                                <ItemTemplate>
                                    <asp:Label ID="lblSl" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"orderid") %>'></asp:Label><br />
                                    <%-- <%# DataBinder.Eval(Container.DataItem, "articlecode")%>--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Article">
                                <ItemTemplate>
                                    <asp:Label ID="lblArticle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"article") %> '></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                <HeaderStyle HorizontalAlign="Left" Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"quantity") %> '></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                <HeaderStyle HorizontalAlign="Right" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stock">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"stock") %> '></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                <HeaderStyle HorizontalAlign="Right" Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price (excl. VAT)">
                                <ItemTemplate>
                                    €&nbsp&nbsp&nbsp
                                    <asp:Label ID="lblUnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"unitprice") %> '></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="130px" />
                                <HeaderStyle HorizontalAlign="Right" Width="130px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount (%)">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"discount") %> '></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                <HeaderStyle HorizontalAlign="Right" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net. Price">
                                <ItemTemplate>
                                    €&nbsp&nbsp&nbsp
                                    <asp:Label ID="lblTotalPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"totalprice") %> '></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VAT (%)">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"vat") %> '></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                <HeaderStyle HorizontalAlign="Right" Width="60px" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#DEDEDE" BorderColor="White" HorizontalAlign="Center" Font-Size="11px" />
                        <AlternatingRowStyle BackColor="#EFEFEF" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" VerticalAlign="Middle" HorizontalAlign="Center" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <div id="msgbox" style="border-right: black thin solid; border-top: black thin solid;
            z-index: 101; left: 336px; visibility: hidden; border-left: black thin solid;
            width: 360px; border-bottom: black thin solid; position: absolute; top: 216px;
            height: 130px; background-color: White;" designtimedragdrop="154">
            <table height="100%" width="100%">
                <tr>
                    <td style="font-size: 10px; color: black" bgcolor="white" colspan="4" height="20">
                        <strong>Invoice Info</strong>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" height="10">
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <asp:Label ID="lblMsgPrint" runat="server" Font-Size="10pt" ForeColor="Blue" Font-Bold="True">Weet u zeker dat u deze facturen wilt afdrukken?</asp:Label>
                    </td>
                    <%--Weet you certainly that you want print these invoices?--%>
                </tr>
                <tr>
                    <td colspan="4" height="20">
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                    </td>
                    <td align="left" width="30%">
                        <asp:Button ID="btnYes" runat="server" CssClass="button1" Text="Yes" OnClick="btnYes_Click">
                        </asp:Button>
                    </td>
                    <td align="right" width="30%">
                        <asp:Button ID="btnNo" runat="server" CssClass="button1" Text="No" OnClick="btnNo_Click">
                        </asp:Button>
                    </td>
                    <td width="20%">
                    </td>
                </tr>
                <tr>
                    <td colspan="4" height="10">
                    </td>
                </tr>
            </table>
        </div>
        <div id="msgbox2" style="border-right: black thin solid; border-top: black thin solid;
            z-index: 100; left: 336px; visibility: hidden; border-left: black thin solid;
            width: 360px; border-bottom: black thin solid; position: absolute; top: 216px;
            height: 130px; background-color: white;">
            <table height="100%" width="100%">
                <tr>
                    <td style="font-size: 10px; color: black" bgcolor="white" colspan="4" height="20">
                        <strong>Invoice Info</strong>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px" colspan="4" height="10">
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <asp:Label ID="Label1" runat="server" Font-Size="10pt" ForeColor="Blue" Font-Bold="True"> Wilt u de printdatums bijwerken?</asp:Label>
                    </td>
                    <%--Do you want update the printdatums?--%>
                </tr>
                <tr>
                    <td colspan="4" height="20">
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                    </td>
                    <td align="left" width="30%">
                        <asp:Button ID="btnYes2" runat="server" CssClass="button1" Text="Yes" OnClick="btnYes2_Click">
                        </asp:Button>
                    </td>
                    <td align="right" width="30%">
                        <asp:Button ID="btnNo2" runat="server" CssClass="button1" Text="No" OnClick="btnNo2_Click">
                        </asp:Button>
                    </td>
                    <td width="20%">
                    </td>
                </tr>
                <tr>
                    <td height="10" colspan="4" style="height: 10px">
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
