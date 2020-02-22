<%@ Page Language="C#" Culture="nl-NL" AutoEventWireup="true" CodeFile="ordermanagement.aspx.cs"
    Inherits="Admin_ordermanagement" Title="Order Management" %>

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
                    <%--<asp:Label ID="lblHeader" Text="Order Management" Font-Bold="true"  runat="server"></asp:Label>--%>
                    <h4 class="contentHeader">
                        Order Management</h4>
                </td>
            </tr>
            <tr>
                <td width="100%" height="15">
                </td>
            </tr>
            <tr>
                <td align="left" valign="middle" style="width: 882px">
                    <table cellpadding="5" cellspacing="0" border="0">
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="lblFilter" runat="server" Text="Filter"></asp:Label>
                            </td>
                            <td valign="top" align="left">
                                <asp:DropDownList ID="drpOrderFilter" runat="server" CssClass="ComboHeight">
                                    <asp:ListItem Selected="True" Value="1">Assigned</asp:ListItem>
                                    <asp:ListItem Value="2">Ready</asp:ListItem>
                                    <asp:ListItem Value="3">Invoiced</asp:ListItem>
                                    <asp:ListItem Value="4">All</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td valign="top" align="left">
                                <asp:ImageButton ID="btnOrderFilter" OnClick="btnOrderFilter_Click" CssClass="ButtonHeight"
                                    Height="20px" runat="server"></asp:ImageButton>
                            </td>
                            <td valign="top" align="left">
                                <input id="txtHideValue" type="hidden" runat="server" value="" />
                            </td>
                            <td align="center" valign="top" width="350px">
                                <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="true" ForeColor="red"></asp:Label>
                            </td>
                        </tr>
                    </table>
            </tr>
            <tr>
                <td style="width: 882px">
                    <asp:GridView ID="grdOrder" BorderWidth="1px" CellPadding="5" runat="server" AutoGenerateColumns="false"
                        Width="882px" GridLines="both" BorderColor="white" BorderStyle="Solid" AllowPaging="True"
                        AllowSorting="True" OnPageIndexChanging="grdOrder_PageChanging" OnSorting="grdOrder_Sorting"
                        PageSize="7" PagerSettings-Mode="Numeric">
                        <Columns>
                            <asp:TemplateField HeaderText=" ">
                                <HeaderTemplate>
                                    <input id="chkAll" onclick="javascript:SelectDeselectCheck(this.status)" type="checkbox"
                                        runat="server">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkOrder" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Date" SortExpression="o.orderdate">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "orderdate")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                <HeaderStyle HorizontalAlign="Left" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order No" SortExpression="orderid">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "orderid")%>'
                                        OnCommand="lnkSelect_Command">
                                        <asp:Label ID="lblOrderId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "orderid")%>'></asp:Label></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer " SortExpression="customer ">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "customer")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                <HeaderStyle HorizontalAlign="Left" Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delivery Address">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "DAddress")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="352px" />
                                <HeaderStyle HorizontalAlign="Left" Width="352px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="status">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "status")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPrint" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "orderid")%>'
                                        OnCommand="lnkPrint_Click">Print</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                                <HeaderStyle HorizontalAlign="Right" Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "orderid")%>'
                                        OnCommand="lnkEdit_Command">Edit</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                                <HeaderStyle HorizontalAlign="Right" Width="20px" />
                            </asp:TemplateField>
                        </Columns>
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
                                    <asp:ListItem Selected="true" Value="2">Ready</asp:ListItem>
                                    <asp:ListItem Value="3">Fucturen</asp:ListItem>
                                    <asp:ListItem Value="1">Verwijderen</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" valign="top">
                                <asp:ImageButton ID="btnAction" CssClass="ButtonHeight" OnClick="btnAction_Click"
                                    runat="server"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 10px">
                <td bgcolor="white" style="height: 10px">
                </td>
            </tr>
            <tr>
                <td bgcolor="white">
                    <asp:GridView ID="grdOrderLine" BorderWidth="1px" CellPadding="2" runat="server"
                        AutoGenerateColumns="False" Width="882px" BorderColor="White" BorderStyle="Solid"
                        AllowPaging="True" AllowSorting="True" OnDataBound="grdOrderLine_DataBound" OnPageIndexChanging="grdOrderLine_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "index")%>
                                    <br />
                                    <%-- <%# DataBinder.Eval(Container.DataItem, "articlecode")%>--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Article">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "title")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                <HeaderStyle HorizontalAlign="Left" Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QTY ">
                                <ItemTemplate>
                                    <asp:Label ID="lblQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "quantity")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                <HeaderStyle HorizontalAlign="Right" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stock">
                                <ItemTemplate>
                                    <asp:Label ID="lblStock" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "stock")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                <HeaderStyle HorizontalAlign="Right" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price (excl. VAT)">
                                <ItemTemplate>
                                    €
                                    <asp:Label ID="lblUnitprice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "unitprice")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="150px" />
                                <HeaderStyle HorizontalAlign="Right" Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount (%)">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiscount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "discount")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                <HeaderStyle HorizontalAlign="Right" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net. Price">
                                <ItemTemplate>
                                    <label style="padding-right: 2px;" id="lblSym" runat="server">
                                    €</label>
                                    <%# DataBinder.Eval(Container.DataItem, "totalprice")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VAT (%)">
                                <ItemTemplate>
                                    <asp:Label ID="lblVat" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "vatpc")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                <HeaderStyle HorizontalAlign="Right" Width="80px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <%--<tr><td align="right"><asp:Label ID="lblHitCount" runat="server" Font-Bold="True"></asp:Label></td></tr>--%>
        </table>
    </div>
    </form>
</body>
</html>
