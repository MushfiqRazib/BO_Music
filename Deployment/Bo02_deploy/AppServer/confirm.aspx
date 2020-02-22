<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="cofirm, Bo02" title="Confirm Page" theme="ThemeOne" %>

<%@ MasterType VirtualPath="~/MainLayOut.master" %>
<%@ Register Assembly="HawarIT.WebControls" Namespace="HawarIT.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="882">
        <tr>
            <td colspan="2" runat="server" id="header" style="height: 19px; width: 509px;">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 834px">
                <table cellpadding="0" cellspacing="0" style="background-color: #F0F0F0; width: 882px;">
                    <tr>
                        <td valign="top" align="left" class="confirmSiteMap">
                            <asp:Label ID="lblCurrentPage" runat="server" Font-Bold='True' Text="Current page"
                                Visible="False"></asp:Label>&nbsp;&nbsp;<b> </b>&nbsp;&nbsp;<asp:Label ID="lblPageRoot"
                                    runat="server" ForeColor="AppWorkspace" Visible="False"></asp:Label>
                            <asp:Label ID="lblActivePage" runat="server" ForeColor="#3300FF" Visible="False"></asp:Label>&nbsp;&nbsp;
                        </td>
                        <td valign="top" align="right" style="padding-right: 3px">
                            <table>
                                <tr style="height: 58px;">
                                    <td align="right" style="width: 31px">
                                        <img src="graphics/step4.gif" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="confirmTDStyle1">
                            <table border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr style="height: 20px">
                                    <td colspan="2" class="confirmTDStyle2" style="height: 20px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width: 873px">
                                        <asp:GridView ID="grdTest" BorderWidth="0px" CellPadding="10" runat="server" AutoGenerateColumns="False"
                                            Width="100%" GridLines="None" BorderColor="#E0E0E0" BorderStyle="Solid" OnSelectedIndexChanged="grdTest_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Product Type">
                                                    <ItemTemplate>
                                                        <%#ShowArticleImage(DataBinder.Eval(Container.DataItem, "ProductType").ToString())%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product Description">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>                                                                    
                                                                    <b><%# DataBinder.Eval(Container.DataItem, "title")%></b>                                                                    
                                                                </td>
                                                            </tr>
                                                                <td>
                                                                    <%# DataBinder.Eval(Container.DataItem, "subtitle")%>
                                                                </td>
                                                            <tr>
                                                                <td>
                                                                    <i><%# DataBinder.Eval(Container.DataItem, "publisher")%></i>
                                                                </td>
                                                            </tr> 
                                                        </table>                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="290px" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="290px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price (excl. VAT)">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Price")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discount (%)">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Discount")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="90px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Quantity")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Net Price">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Total")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="VAT (%)">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "VAT")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="80px" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="background-color: #FFFFFF;">
                                    <td bgcolor="#DEDEDE">
                                        <div style="width: 300">
                                            &nbsp;</div>
                                    </td>
                                    <td bgcolor="#DEDEDE" colspan="5" align="right" class="confirmTDStyle3" style="text-align: right">
                                        <table border="0" cellspacing="2" cellpadding="5" style="text-align: right">
                                            <tr>
                                                <td align="right" style="padding-right: 5px; width: 500px;">
                                                    <asp:Label ID="lblSubTotalText" runat="server" Text="Label"></asp:Label>
                                                    :</td>
                                                <td style="width: 130px">
                                                    <asp:Label ID="lblSubTotal" runat="server" Text="Label"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="padding-right: 5px; width: 500px;">
                                                    <asp:Label ID="lblVatText" runat="server" Text="Label"></asp:Label>
                                                    :</td>
                                                <td style="width: 130px">
                                                    <asp:Label ID="lblVat" runat="server" Text="Label" Font-Underline="False"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="padding-right: 5px; width: 500px;">
                                                    <b>
                                                        <asp:Label ID="lblTotalText" runat="server" Text="Label"></asp:Label>
                                                        :</b></td>
                                                <td style="width: 130px; border-top: 1px solid black;">
                                                    <asp:Label ID="lblTotal" Font-Bold="true" runat="server" Text="Label">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="right" style="width: 295px; text-align: right;">
                                                    (<asp:Label ID="lblShipping" runat="server" Text="Label"></asp:Label>)</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr align="center" class="confirmTDStyle4">
                                    <td colspan="2" align="center">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <%--Table for invoice address--%>
                                                    <table border="0" cellpadding="2" cellspacing="0" align="left">
                                                        <tr>
                                                            <td align="left">
                                                                <b>
                                                                    <asp:Label ID="lblInvoiceAddressHeader" runat="server" Text="Label"></asp:Label></b></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="lblAddress" runat="server" Text="Label"></asp:Label>
                                                                <asp:Label ID="lblHouseNum" runat="server" Text="Label"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="lblPostCode" runat="server" Text="Label"></asp:Label>
                                                                <asp:Label ID="lblResidence" runat="server" Text="Label"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="lblCountry" runat="server" Text="Label"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="180px">
                                                </td>
                                                <td align="left">
                                                    <%--Table for delivary address--%>
                                                    <table border="0" cellpadding="2" cellspacing="0" align="left">
                                                        <tr>
                                                            <td align="left">
                                                                <b>
                                                                    <asp:Label ID="lblDelAdd" runat="server" Text="Label"></asp:Label></b></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDName" runat="server" Text="Label"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDAddress" runat="server" Text="Label"></asp:Label>&nbsp;
                                                                <asp:Label ID="lblDHouseNum" runat="server" Text="Label"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDPostCode" runat="server" Text="Label"></asp:Label>&nbsp;
                                                                <asp:Label ID="lblDResidence" runat="server" Text="Label"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDCountry" runat="server" Text="Label"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" valign="top" align="center" style="height: 30px">
                                        <asp:Label ID="lblTransection" runat="server" Text="Label" Font-Bold="true" ForeColor="red"></asp:Label>&nbsp;</td>
                                </tr>
                                <tr style="background-color: #FFFFFF;">
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="2" align="left">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="lblPaymentType" runat="server" Text="Label" Font-Bold="true" Height="18px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPtype" runat="server">
                                                        
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" align="right">
                                        <table class="confirmTDStyle6">
                                            <tr>
                                            <td>
                                                <asp:ImageButton ID="btnCart" runat="server" ImageUrl="graphics/btn_cart.jpg" OnClick="btnCart_Click" />
                                            </td>
                                                <td>
                                                    <asp:ImageButton ID="btnBack" runat="server" ImageUrl="graphics/btn_goback_en.png"
                                                        OnClick="btnBack_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnConfirm" runat="server" OnClick="ImageButton1_Click" ImageUrl="graphics/btn_confirm_en.png" />
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 50px">
                        <td colspan="2">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
