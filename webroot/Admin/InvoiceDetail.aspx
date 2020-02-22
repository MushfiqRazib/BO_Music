<%@ Page Language="C#" Culture="nl-NL" AutoEventWireup="true" CodeFile="InvoiceDetail.aspx.cs"
    Inherits="Admin_InvoiceDetail" Title="Invoice Detail" ValidateRequest="false" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
        <table cellpadding="0" cellspacing="0" border="0" class="contentArea">
            <tr height="76px" valign="top">
                <td class="header">
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" border="0" width="882" style="background-color: #DEDEDE;">
                        <tr>
                            <td valign="top" align="left" class="contentHeader">
                                
                                    Invoice Detail
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="HeaderTable" cellpadding="2" cellspacing="0" border="0" width="882">
                        <tr>
                            <td width="50%" align="left" valign="top">
                                <table cellpadding="2" cellspacing="0" border="0">
                                    <tr>
                                        <td valign="top">
                                            <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice#" Font-Bold="true" ForeColor="red"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblInvoiceNoValue" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:Label ID="lblCustomer" runat="server" Text="Customer" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblCustomerValue" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <asp:Label ID="lblCustBTW" runat="server" Text="Customer BTW#" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtCustBTWValue" runat="server" MaxLength="20"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:Label ID="lblInvAddress" runat="server" Text="Invoice Address" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            <table cellpadding="2" cellspacing="0" border="0">
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <asp:TextBox ID="txtHouseNr" runat="server" MaxLength="20"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <asp:TextBox ID="txtAddress" runat="server" MaxLength="100"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <asp:TextBox ID="txtResidence" runat="server" MaxLength="50"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <asp:TextBox ID="txtPostCode" runat="server" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <asp:DropDownList ID="ddlCountry" runat="server" Width="125px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="50%" align="center" valign="middle" >
                                <table cellpadding="2" cellspacing="0" border="0">
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtInvoiceDate" runat="server" Enabled="true" MaxLength="10"></asp:TextBox>
                                            <br />
                                            <asp:RegularExpressionValidator ID="valInvDate" runat="server" ControlToValidate="txtInvoiceDate"
                                                Display="Dynamic" ErrorMessage="Invalid Date Format" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                                    ID="valInvDateReq" runat="server" ErrorMessage="Required" ControlToValidate="txtInvoiceDate"
                                                    Display="dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label ID="lblStatus" runat="server" Text="Status" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="drpStatus" runat="server" Width= "125px">
                                                <asp:ListItem Value="1" Text="Boeken"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Geboekt"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label ID="lblTotalDiscount" runat="server" Text="Total Discount" Font-Bold="True"></asp:Label>&nbsp;
                                        </td>
                                        <td align="left" valign="top">
                                            &nbsp;<asp:Label ID="lblTotalDiscountValue" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label ID="lblVAT" runat="server" Text="Total VAT" Font-Bold="True"></asp:Label>&nbsp;
                                        </td>
                                        <td align="left" valign="top">
                                            &nbsp;<asp:Label ID="lblVATValue" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label ID="lblNetPrice" runat="server" Text="Net Price" Font-Bold="True"></asp:Label>&nbsp;
                                        </td>
                                        <td align="left" valign="top">
                                            &nbsp;<asp:Label ID="lblNetPriceValue" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--<tr height=50></tr>--%>
            <tr>
                <td>
                    <asp:GridView ID="grdInvoiceLine" runat="server" GridLines="None" AutoGenerateColumns="False"
                        CellPadding="5" Width="882px">
                        <Columns>
                            <asp:TemplateField HeaderText="Order #">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"orderid") %>'></asp:Label>
                                    <asp:Label ID="lblArticleID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"articlecode") %>'
                                        Visible="false"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Article">
                                <ItemTemplate>
                                    <asp:Label ID="lblArticle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"article") %> '></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="300px" />
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" Style="text-align: right" ID="intCtrQuanity" Text='<%# DataBinder.Eval(Container.DataItem,"quantity") %> '
                                        MaxLength="6" Width="20px" onfocus="javascript: SetLatestValue(this.value)" onchange="return CheckCorrectValue(this,'0123456789')"
                                        onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789')"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="50px" />
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stock">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" Style="text-align: right" ID="intCtrStock" Text='<%# DataBinder.Eval(Container.DataItem,"stock") %> '
                                        MaxLength="6" Width="35px" onfocus="javascript: SetLatestValue(this.value)" onchange="return CheckCorrectValue(this,'0123456789')"
                                        onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789')"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="50px" />
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price (excl. VAT)">
                                <ItemTemplate>
                                    €
                                    <asp:TextBox runat="server" ID="intCtrUnitPrice" Style="text-align: right" Text='<%# DataBinder.Eval(Container.DataItem,"unitprice") %> '
                                        Width="50px" MaxLength="6" onfocus="javascript: SetLatestValue(this.value)" onchange="return CheckCorrectValue(this,'0123456789,')"
                                        onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789,')"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="100px" />
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount (%)">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiscount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"discountpc") %> '></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="80px" />
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net. Price">
                                <ItemTemplate>
                                    €
                                    <asp:Label ID="lblShippingCost" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"totalprice") %> '></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="100px" />
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VAT (%)">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="intCtrVat" Style="text-align: right" Text='<%# DataBinder.Eval(Container.DataItem,"vatpc") %> '
                                        Width="50px" MaxLength="6" onfocus="javascript: SetLatestValue(this.value)" onchange="return CheckCorrectValue(this,'0123456789,')"
                                        onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789,')"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="80px" />
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" Width="80px" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#DEDEDE" BorderColor="White" HorizontalAlign="Center" Font-Size="11px" />
                        <AlternatingRowStyle BackColor="#EFEFEF" />
                        <RowStyle BackColor="White" VerticalAlign="Middle" HorizontalAlign="Center" />
                    </asp:GridView>
                </td>
            </tr>
            <tr style="background-color: white;">
                <td align="center" valign="top">
                    <asp:Label ID="lblErrorMsg" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding: 5px">
                    <hr style="border: 2px solid #2bab1c" />
                    <asp:LinkButton ID="lnkCreditInvoice" runat="server" Text="Credit this" OnClick="lnkCreditInvoice_Click"
                        Visible="false" />
                    <asp:LinkButton ID="lnkPrint" Visible="false" runat="server" Text="Print" OnClick="lnkPrint_Click" />
                    <asp:LinkButton ID="lnkSave" Visible="false" runat="server" Text="Save" OnClick="lnkSave_Click" />
                    <asp:LinkButton ID="lnkDelete" Visible="false" runat="server" Text="Delete" OnClick="lnkDelete_Click" />
                    <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" OnClick="lnkCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
