<%@ Page Language="C#" Culture="nl-NL" AutoEventWireup="true" CodeFile="stockmanagement.aspx.cs"
    Inherits="Admin_stockmanagement" Title="Stock Management" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script type='text/javascript'>
        function checkDelete() {

            //var myTextField =document.getElementById("txtHideValue");

            //var myTextField =document.getElementById("txtHideValue");
            var answer = confirm("Are You Sure You want to Delete?");
            if (answer) {
                GetObject("txtHideValue").value = "yes";
                //myTextField.value="yes";
            }
            else {
                GetObject("txtHideValue").value = "no";
                //myTextField.value="no";
            }
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" class="contentArea">
            <tr>
                <td align="left" class="pageHeader">
                    <h4 class="contentHeader">
                        Stock Management</h4>
                </td>
                <td align="left" style="width: 883px; height: 15px">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 883px; height: 47px;">
                    <table cellpadding="5" cellspacing="0" border="0">
                        <tr>
                            <td align="left" valign="top" style="height: 28px">
                                <asp:Label ID="lblReceivingType" runat="server" Text="Receiving Type"></asp:Label>
                            </td>
                            <td valign="top" align="left" style="height: 28px">
                                <asp:DropDownList ID="ddlReceivingType" runat="server" CssClass="ComboHeight">
                                    <asp:ListItem Selected="True" Value="N">Not Received</asp:ListItem>
                                    <asp:ListItem Value="F">Full Received</asp:ListItem>
                                    <asp:ListItem Value="A">All</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td valign="top" align="left" style="height: 28px">
                                <asp:ImageButton ID="btnOrderFilter" CssClass="ButtonHeight" runat="server" ImageUrl="~/graphics/btn_OK_en.png"
                                    OnClick="btnOrderFilter_Click"></asp:ImageButton>
                            </td>
                            <td valign="top" align="left" style="height: 28px">
                                <input id="txtHideValue" type="hidden" runat="server" value="" />
                            </td>
                            <td align="center" valign="top" width="350px" style="height: 28px">
                                <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="true" ForeColor="red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <td align="left" style="width: 883px; height: 47px">
                    </td>
            </tr>
            <tr>
                <td style="width: 882px">
                    <asp:GridView ID="grdStockOrder" BorderWidth="1px" CellPadding="5" runat="server"
                        AutoGenerateColumns="false" Width="882px" GridLines="both" BorderColor="white"
                        BorderStyle="Solid" AllowPaging="True" AllowSorting="True" PageSize="7" PagerSettings-Mode="Numeric"
                        OnSorting="grdStockOrder_Sorting" OnPageIndexChanging="grdStockOrder_PageIndexChanging"
                        ShowFooter="true">
                        <EmptyDataTemplate>
                            <asp:LinkButton runat="server" ID="lnkNew" Text="New Order" OnClick="lnkNew_Click"></asp:LinkButton>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText=" ">
                                <HeaderTemplate>
                                    <input id="chkAll" onclick="javascript:SelectDeselectCheck(this.status)" type="checkbox"
                                        runat="server">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkOrder" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="20px" />
                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Date" SortExpression="sdate">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "sdate")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton runat="server" ID="lnkNew" OnClick="lnkNew_Click">New Order</asp:LinkButton>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                <FooterStyle HorizontalAlign="left" Width="80px" />
                                <HeaderStyle HorizontalAlign="Left" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order No" SortExpression="orderno">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "orderno")%>'
                                        OnCommand="lnkSelect_Command">
                                        <asp:Label ID="lblOrderId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "orderno")%>'></asp:Label></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Supplier " SortExpression="supplier">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "supplier")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="220px" />
                                <HeaderStyle HorizontalAlign="Left" Width="220px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delivery Date" SortExpression="ddate">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ddate")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receive Status" SortExpression="rstatus">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "rstatus")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                <HeaderStyle HorizontalAlign="Left" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Status" SortExpression="pstatus">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "pstatus")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                <HeaderStyle HorizontalAlign="Left" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPrint" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "orderno")%>'
                                        OnCommand="lnkPrint_Command">Print</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                                <HeaderStyle HorizontalAlign="Right" Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "orderno")%>'
                                        OnCommand="lnkEdit_Command" Text="Order Edit"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                <HeaderStyle HorizontalAlign="Right" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkReceiving" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "orderno")%>'
                                        OnCommand="lnkReceiving_Command">Receiving</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                                <HeaderStyle HorizontalAlign="Right" Width="20px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#DEDEDE" VerticalAlign="Top" />
                    </asp:GridView>
                </td>
                <td style="width: 882px">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 883px; background-color: White;">
                </td>
                <td align="left" style="width: 883px; background-color: white">
                </td>
            </tr>
            <tr valign="top">
                <td align="left" bgcolor="white" style="height: 20px;" valign="top">
                    &nbsp;&nbsp;
                    <table cellpadding="5" cellspacing="0" border="0">
                        <tr>
                            <td valign="top" align="left">
                                <asp:Label ID="lblPaymentStatus" runat="server" Text="Change Payment Status " Style="valign: top;"></asp:Label>
                            </td>
                            <td valign="top" align="left">
                                <asp:DropDownList ID="ddlPaymentStatus" runat="server" CssClass="ComboHeight">
                                    <asp:ListItem Value="F">Full Paid</asp:ListItem>
                                    <asp:ListItem Value="P">Partial Paid</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="U">Unpaid</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td valign="top" align="left">
                                <asp:ImageButton ID="btnUpdatePaymentStatus" CssClass="ButtonHeight" runat="server"
                                    ImageUrl="~/graphics/btn_OK_en.png" OnClick="btnUpdatePaymentStatus_Click" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="left" bgcolor="white" style="height: 10px" valign="top">
                </td>
            </tr>
            <tr style="height: 10px">
                <td align="left" bgcolor="white" style="height: 10px">
                </td>
                <td align="left" bgcolor="white" style="height: 10px">
                </td>
            </tr>
            <tr style="height: 10px">
                <td bgcolor="white" style="height: 10px" align="left">
                    <asp:Label ID="lblOrderDetails" runat="server" Font-Bold="True" Text="Order Details"
                        Visible="False"></asp:Label>
                </td>
                <td align="left" bgcolor="white" style="height: 10px">
                </td>
            </tr>
            <tr style="height: 10px">
                <td align="left" bgcolor="white" style="height: 10px">
                </td>
                <td align="left" bgcolor="white" style="height: 10px">
                </td>
            </tr>
            <tr>
                <td bgcolor="white">
                    <asp:GridView ID="grdSupplyOrderLine" BorderWidth="1px" CellPadding="5" runat="server"
                        AutoGenerateColumns="False" Width="882px" GridLines="both" BorderColor="white"
                        BorderStyle="Solid" AllowSorting="True" PageSize="7">
                        <Columns>
                            <asp:TemplateField HeaderText="Article">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "articlename")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="250px" Wrap="true" />
                                <HeaderStyle HorizontalAlign="Left" Width="250px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Current Stock">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "stock")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                <HeaderStyle HorizontalAlign="Right" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ordered QTY ">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "qty")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                <HeaderStyle HorizontalAlign="Right" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Price">
                                <ItemTemplate>
                                    €
                                    <%# DataBinder.Eval(Container.DataItem, "price")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vat">
                                <ItemTemplate>
                                    €
                                    <%# DataBinder.Eval(Container.DataItem, "svat")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net Price">
                                <ItemTemplate>
                                    €
                                    <%# DataBinder.Eval(Container.DataItem, "netprice")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast"
                            NextPageText="Next" PreviousPageText="Previous" />
                    </asp:GridView>
                </td>
                <td bgcolor="white">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
