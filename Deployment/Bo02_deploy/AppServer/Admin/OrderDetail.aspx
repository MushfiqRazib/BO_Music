<%@ page language="C#" culture="nl-NL" autoeventwireup="true" inherits="Admin_OrderDetail, Bo02" title="Order Detail" validaterequest="false" theme="ThemeOne" %>

<%@ Register Assembly="HawarIT.WebControls" Namespace="HawarIT.WebControls" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/style.css" type="text/css" rel="stylesheet">
    <link href="media/style.css" type="text/css" rel="stylesheet">
    <link href="media/master.css" type="text/css" rel="stylesheet">
    <%--    <script type='text/javascript'>
        function PopupArticle() {
            var div = document.getElementById("divArticle");
            if (div.style.visibility == "hidden") {
                div.style.visibility = "visible";
                div.style.height = "200px";
            }
            else {
                div.style.visibility = "hidden";
                div.style.height = "0px";
            }

        }
    </script>
--%>
</head>
<body class="adminbody">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" class="contentArea" border="0">
            <tr height="76" valign="top">
                <td class="header">
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" border="0" width="882px" style="background-color: #DEDEDE;">
                        <tr>
                            <td align="left" valign="top" class="contentHeader">
                                Order Detail
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" width="882">
                    <table cellpadding="2" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="padding-left: 10px;">
                                <input id="txtHideID" type="hidden" runat="server" />
                            </td>
                            <td align="right" colspan="2" style="padding-right: 125px;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" width="882">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td valign="top" align="left" colspan="2" width="420px">
                                <!-- Start of Table Customer Info -->
                                <table cellpadding="5" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="left" valign="top" colspan="2">
                                            <asp:Label ID="lblOrderNo" Font-Bold="true" ForeColor="red" runat="server" Text="Order#"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" width="80" style="padding-right: 1px;">
                                            <asp:Label ID="lblCustomerHeader" runat="server" Text="Customer" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblCustomerInvValue" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" width="80" style="padding-right: 1px;">
                                            <asp:Label ID="lblCustomerInv" runat="server" Text=" " Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblAddressInvValue" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" width="80" style="padding-right: 1px;">
                                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" MaxLength="200"
                                                Wrap="true" Height="50px" Width="270px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" valign="top" width="100%" style="padding-left: 15px;">
                                            <table cellpadding="0" cellspacing="0" border="0" width="420px">
                                                <tr>
                                                    <td align="left" valign="top" style="width: 90px">
                                                        <%--<asp:Label ID="lblDiscountpc" runat="server" Text="Discount(%)" Font-Bold="True"
                                                        Visible="False"></asp:Label>--%>
                                                    </td>
                                                    <td align="left" valign="top" style="width: 64px">
                                                        <%--<asp:TextBox runat="server" ID="intCtrDiscount" Style="text-align: right" Text=''
                                                        Width="50px" MaxLength="6" onfocus="javascript: SetLatestValue(this.value)" onchange="return CheckCorrectValue(this,'0123456789,')"
                                                        onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789,')" Visible="False"></asp:TextBox>--%>
                                                    </td>
                                                    <td align="left" valign="top" style="width: 88px">
                                                        <%--<asp:Label ID="lblShippingCost" runat="server" Text="Shipping Cost:" Font-Bold=true ></asp:Label>--%>
                                                    </td>
                                                    <td align="left" valign="top">
                                                        <%--<asp:TextBox runat="server" ID="intCtrShippingCost" style="text-align:right" Text='' Width="50px" MaxLength="6" onfocus="javascript: SetLatestValue(this.value)" onchange="return CheckCorrectValue(this,'0123456789,')" onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789,')"></asp:TextBox>--%>
                                                    </td>
                                                </tr>
                                                <%--                             <tr>
                                <td colspan=4 valign=top align=center>
                                    <asp:RequiredFieldValidator ID="valDiscount" runat="server"  ErrorMessage="value Required" ControlToValidate="intCtrDiscount"></asp:RequiredFieldValidator>&nbsp;<asp:RequiredFieldValidator ID="valShippingCost" runat="server" ErrorMessage="value Required" ControlToValidate="intCtrShippingCost"  Display=Dynamic></asp:RequiredFieldValidator>
                                </td>
                             </tr>--%>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <!-- end of Table Customer Info -->
                            </td>
                            <td valign="middle" align="left" colspan="2" style="padding-left: 50px;">
                                <!-- Delivery Column -->
                                <!-- Start of Table Delivery Info -->
                                <table cellpadding="1" cellspacing="0" border="0">
                                    <tr>
                                        <td valign="top" align="right" style="height: 26px">
                                            <asp:Label ID="lblOrderDate" runat="server" Text="Order Date " Font-Bold="true"></asp:Label>
                                        </td>
                                        <td valign="top" align="left" colspan="0" style="height: 26px">
                                            <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td align="left" valign="top">
                                                        &nbsp; &nbsp;
                                                        <asp:TextBox ID="ADate" runat="server" ReadOnly="false" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td align="left" valign="top">
                                                        <%--<input type="button" value=".." onclick="displayDatePicker('ADate');" />--%>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                               <td colspan=3 align=center valign=top>
                                   <asp:RequiredFieldValidator ID="valDateRequired" runat="server" ControlToValidate="txtOrderDateValue" Display=dynamic></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="valDate" runat="server" ControlToValidate="txtOrderDateValue" Display="Dynamic" ValidationExpression="(19|20)\d\d[-](0[1-9]|1[012])[-](0[1-9]|[12][0-9]|3[01])"></asp:RegularExpressionValidator>
                               </td>
                            </tr>--%>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <!--Delivery Customer Info -->
                                        <td align="right" valign="top" width="100" style="height: 16px">
                                            <asp:Label ID="lblCusomer" runat="server" Text="Delivery" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" colspan="2" style="height: 16px">
                                            <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td valign="top">
                                                        &nbsp; &nbsp;
                                                        <asp:DropDownList ID="ddlInitialName" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:TextBox ID="txtFirstName" runat="server" Width="50px" MaxLength="100"></asp:TextBox>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:TextBox ID="txtMiddleName" runat="server" Width="50px" MaxLength="100"></asp:TextBox>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:TextBox ID="txtLastName" runat="server" Width="50px" MaxLength="100"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                            <td colspan=4 valign=top align=left style="height: 15px"><asp:RequiredFieldValidator ID="valCustomer" runat="server"  ControlToValidate="txtFirstName" Display="Dynamic"></asp:RequiredFieldValidator></td>
                        </tr>--%>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <!-- DHouseNumber Info -->
                                        <td align="right" valign="top" width="100" style="height: 10px">
                                            <asp:Label ID="lblHousenr" runat="server" Text="House#" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" style="height: 10px">
                                            &nbsp; &nbsp;
                                            <asp:TextBox ID="txtHousenr" runat="server" MaxLength="20"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top" style="height: 10px">
                                            <%--<asp:RequiredFieldValidator ID="valHousenr" runat="server"  ControlToValidate="txtHousenr"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <!-- DAddress Info -->
                                        <td align="right" valign="top" width="100">
                                            <asp:Label ID="lblAddress" runat="server" Text="Address" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            &nbsp; &nbsp;
                                            <asp:TextBox ID="txtAddress" runat="server" MaxLength="100"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <%--<asp:RequiredFieldValidator ID="valAddress" runat="server"  ControlToValidate="txtAddress" ></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <!-- DPostCode Info -->
                                        <td align="right" valign="top" width="100">
                                            <asp:Label ID="lblPostCode" runat="server" Text="Post Code" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            &nbsp; &nbsp;
                                            <asp:TextBox ID="txtPostCode" runat="server" MaxLength="10"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <%--<asp:RequiredFieldValidator ID="valPostCode" runat="server"  ControlToValidate="txtPostCode"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <!-- DResidence Info -->
                                        <td align="right" valign="top" width="100">
                                            <asp:Label ID="lblResidence" runat="server" Text="Residence" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            &nbsp; &nbsp;
                                            <asp:TextBox ID="txtResidence" runat="server" MaxLength="50"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <%--<asp:RequiredFieldValidator ID="valResidence" runat="server"  ControlToValidate="txtResidence"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <!-- DCountry Info -->
                                        <td align="right" valign="top" width="100">
                                            <asp:Label ID="lblCountry" runat="server" Text="Country" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            &nbsp; &nbsp;
                                            <asp:DropDownList ID="ddlCountry" runat="server" Width="125px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" valign="top">
                                        </td>
                                    </tr>
                                </table>
                                <!-- End of Table Delivery Info -->
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 15px">
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="grdOrderLine" BorderWidth="0px" CellPadding="3" runat="server"
                        AutoGenerateColumns="False" Width="882px" GridLines="None" ShowFooter="True">
                        <Columns>
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "index")%>
                                    <br>
                                    <%--       <%# DataBinder.Eval(Container.DataItem, "articlecode")%>--%>
                                    <asp:Label ID="lblOrderId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "orderid")%>'></asp:Label>
                                    <asp:Label ID="lblArticleCode" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "articlecode")%>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFooterArticleCode" runat="server" Text='New' BackColor="white"
                                        ForeColor="black" Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lnkSelectArticle" runat="server" Text="..." OnClick="LoadgrdArticle"
                                        Enabled="false" /><%--<input id="lnkSelectArticle" type="button" value="..."  onclick="LoadgrdArticle" />--%>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Article">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "title")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "title")%>'></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Left" Width="300px" />
                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                <HeaderStyle HorizontalAlign="Left" Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QTY ">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" Style="text-align: right" ID="intCtrQuanity" MaxLength="6"
                                        Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "quantity")%>' onfocus="javascript: SetLatestValue(this.value)"
                                        onchange="return CheckCorrectValue(this,'0123456789')" onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789')"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblQuanityNew" runat="server" Text="" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" Width="100px" />
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stock">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "stock")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label runat="server" ID="lblStock" Text='<%# DataBinder.Eval(Container.DataItem, "stock")%>'></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" Width="80px" />
                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                <HeaderStyle HorizontalAlign="Right" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price (excl. VAT)">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="intCtrUnitPrice" Style="text-align: right" Text='<%# DataBinder.Eval(Container.DataItem,"unitprice") %> '
                                        Width="50px" MaxLength="6" onfocus="javascript: SetLatestValue(this.value)" onchange="return CheckCorrectValue(this,'0123456789,')"
                                        onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789,')"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblPriceNew" runat="server" Text="" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" Width="110px" />
                                <ItemStyle HorizontalAlign="Right" Width="110px" />
                                <HeaderStyle HorizontalAlign="Right" Width="110px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount (%)">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="intCtrDiscount" Style="text-align: right" Text='<%# DataBinder.Eval(Container.DataItem,"discount") %> '
                                        Width="50px" MaxLength="6" onfocus="javascript: SetLatestValue(this.value)" onchange="return CheckCorrectValue(this,'0123456789,')"
                                        onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789,')"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblDisNew" runat="server" Text="" Style="text-align: right" Width="50px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" Width="90px" />
                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                                <HeaderStyle HorizontalAlign="Right" Width="90px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net. Price">
                                <ItemTemplate>
                                    €
                                    <%# DataBinder.Eval(Container.DataItem, "TotalPrice")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblSubTotal" runat="server" Text=""></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" Width="100px" />
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VAT (%)">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="intCtrVat" Style="text-align: right" Text='<%# DataBinder.Eval(Container.DataItem,"vatpc") %> '
                                        Width="50px" MaxLength="6" onfocus="javascript: SetLatestValue(this.value)" onchange="return CheckCorrectValue(this,'0123456789,')"
                                        onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789,')"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblVatNew" runat="server" Text="" Style="text-align: right" Width="50px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" Width="90px" />
                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                                <HeaderStyle HorizontalAlign="Right" Width="90px" />
                            </asp:TemplateField>
                            <%--                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "articlecode")%>'
                                    OnCommand="lnkDelete_Click">Delete</asp:LinkButton>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton runat="server" ID="lnkInsert" CausesValidation="false" Text="Confirm"
                                    OnCommand="lnkInsert_Click" Enabled="false"></asp:LinkButton>
                            </FooterTemplate>
                            <FooterStyle HorizontalAlign="Center" Width="52px" />
                            <ItemStyle HorizontalAlign="Center" Width="52px" />
                            <HeaderStyle HorizontalAlign="Center" Width="52px" />
                        </asp:TemplateField>--%>
                        </Columns>
                        <HeaderStyle BackColor="#DEDEDE" HorizontalAlign="Center" Font-Size="11px" />
                        <AlternatingRowStyle BackColor="#EFEFEF" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <FooterStyle BackColor="#DEDEDE" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table cellpadding="5" cellspacing="0" border="0" width="100%">
                        <tr style="background-color: white;">
                            <td valign="top" align="right">
                                <asp:Label ID="lblSubTotal" runat="server" Text="Sub Total (excl. VAT): " Font-Bold="true"></asp:Label>
                            </td>
                            <td style="padding-right: 52px;" align="right" valign="top" width="100">
                                <asp:Label ID="lblSubTotalValue" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="background-color: white;">
                            <td valign="top" align="right">
                                <asp:Label ID="lblVat" runat="server" Text="VAT : " Font-Bold="true"></asp:Label>
                            </td>
                            <td style="padding-right: 52px;" align="right" valign="top" width="100">
                                <asp:Label ID="lblVatValue" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="background-color: #DEDEDE;">
                            <td valign="top" align="right">
                                <asp:Label ID="lblGrandTotal" runat="server" Text="Grand Total : " Font-Bold="True"></asp:Label>&nbsp;
                            </td>
                            <td style="padding-right: 52px; border-top: solid 1px black;" align="right" valign="top"
                                width="100">
                                &nbsp;<asp:Label ID="lblGrandTotalValue" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" align="center">
                    <asp:Label ID="lblError" runat="server" Width="510px" Font-Bold="true" ForeColor="red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding: 5px">
                    <hr style="border: 2px solid #2bab1c" />
                    <asp:LinkButton ID="lnkPrint" runat="server" Text="Print" OnClick="lnkPrint_Click"
                        Visible="false" />
                    <asp:LinkButton ID="lnkSave" runat="server" Text="Save" OnClick="lnkSave_Click" Visible="false" />
                    <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" OnClick="lnkCancel_Click" />
                </td>
            </tr>
        </table>
        <br />
        <div id="divArticle" runat="server" style="position: absolute; left: 200px; overflow: auto;
            height: 200px; width: 577px; top: 200px">
            <table width="550px" height="200px" border="1" align="center">
                <tr>
                    <td align="center" style="padding-left: 0">
                        <asp:GridView ID="grdArticle" BorderWidth="0px" BorderStyle="Outset" CellPadding="5"
                            runat="server" AutoGenerateColumns="False" Width="550px" GridLines="none" BorderColor="white">
                            <HeaderStyle CssClass="DataGridFixedHeader2" HorizontalAlign="Left" VerticalAlign="top"
                                Height='10px'></HeaderStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Title">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "code")%>'
                                            OnCommand="lnkSelect_Command">
                                            <asp:Label ID="lblOrderId" runat="server" onclick="javascript:PopupArticle()" Text='<%# DataBinder.Eval(Container.DataItem, "title")%>'></asp:Label></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" Width="250px" />
                                    <HeaderStyle HorizontalAlign="left" Width="250px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "type")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="75px" />
                                    <HeaderStyle HorizontalAlign="Left" Width="75px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Author">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "author")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="175px" />
                                    <HeaderStyle HorizontalAlign="Left" Width="175px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stock">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "qty")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                                    <HeaderStyle HorizontalAlign="Right" Width="50px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#DEDEDE" BorderColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="#EFEFEF" />
                            <RowStyle BackColor="White" VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
