<%@ page language="C#" culture="nl-NL" autoeventwireup="true" inherits="Admin_supplyorder, Bo02" title="Supply Order" validaterequest="false" theme="ThemeOne" %>

<%@ Register Assembly="HawarIT.WebControls" Namespace="HawarIT.WebControls" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/style.css" type="text/css" rel="stylesheet">
    <link href="media/style.css" type="text/css" rel="stylesheet">
    <link href="media/master.css" type="text/css" rel="stylesheet">

    <script language="JavaScript" src="../include/CommonFuctions.js"></script>

    <script language="JavaScript" src="../include/jscript.js"></script>

    <script language="JavaScript" src="../include/Datepicker.js"></script>

    <script type='text/javascript'>
        function PopupArticle() {
            el = document.getElementById("overlay");
            el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible";
            return false;

        }
        function CompareDates() {

            var orderDate = new Date; order = GetObject('txtOrderDate');
            var deliveryDate = new Date; delivery = GetObject('txtDeliveryDate');


            var aOrderDate = order.value.split("-");
            orderDate.setDate(parseInt(aOrderDate[0]));
            orderDate.setMonth(parseInt(aOrderDate[1]));
            orderDate.setYear(parseInt(aOrderDate[2]));

            var aDeliveryDate = delivery.value.split("-");
            deliveryDate.setDate(parseInt(aDeliveryDate[0]));
            deliveryDate.setMonth(parseInt(aDeliveryDate[1]));
            deliveryDate.setYear(parseInt(aDeliveryDate[2]));



            if (orderDate > deliveryDate) {
                alert('Delivery date cannot be lesser than order date');
                return false;
            }
            var result = confirm('Are you sure?');
            if (!result) {
                return false;
            }
            return true;

        }





        function Button1_onclick() {

            var obj = GetObject("Label1");
            //obj.innerHTML = "HELLO";
        }

    </script>

</head>
<body class="adminbody">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
        <table cellpadding="0" cellspacing="0" class="contentArea">
            <tr height="76" valign="top">
                <td class="header">
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" border="0" width="882px" style="background-color: #DEDEDE;">
                        <tr>
                            <td align="left" valign="top" class="contentHeader">
                                Edit Supply Order
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 11px; width: 883px;" align="center">
                    <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 883px">
                    <table align="right" style="width: 820px" cellpadding="0" cellspacing="1" border="0">
                        <tr>
                            <td align="right" width="5px" style="height: 21px">
                            </td>
                            <td align="left" width="100" style="height: 21px">
                                <asp:Label ID="lblOrderNo" runat="server" Font-Bold="True" Text="Order #"></asp:Label>
                            </td>
                            <td width="200" style="height: 21px">
                                <asp:Label ID="lblPrintSupplyOrderNo" runat="server" Font-Bold="True" ForeColor="Red"
                                    Width="100px"></asp:Label>
                            </td>
                            <td width="200" style="height: 21px">
                            </td>
                            <td align="left" width="70" style="height: 21px">
                            </td>
                            <td width="220" style="height: 21px">
                                &nbsp;&nbsp;
                            </td>
                            <td width="100" style="height: 21px">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="5">
                            </td>
                            <td align="left" width="100">
                            </td>
                            <td width="200">
                            </td>
                            <td width="200">
                            </td>
                            <td align="left" width="70">
                            </td>
                            <td width="220">
                            </td>
                            <td width="100">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="5">
                            </td>
                            <td align="left" width="100" valign="top">
                                <asp:Label ID="lblDate" runat="server" Font-Bold="False" Text="Order Date "></asp:Label>
                            </td>
                            <td width="200" valign="middle">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:DateControl ID="txtOrderDate" runat="server"></cc1:DateControl>
                                        </td>
                                        <td>
                                            <input id="lnkPickOrderDate" style="height: 20px" type="button" value=".." onclick="displayDatePicker('txtOrderDate');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="200" align="left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOrderDate"
                                    Display="Dynamic" ErrorMessage="empty!"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtOrderDate"
                                    Display="Dynamic" ErrorMessage="invalid date" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"></asp:RegularExpressionValidator>
                            </td>
                            <td align="left" width="70" valign="top">
                                <asp:Label ID="lblSupplier" runat="server" Font-Bold="False" Text="Supplier"></asp:Label>
                            </td>
                            <td colspan="2" width="250">
                                <asp:DropDownList ID="ddlSupplier" runat="server" Width="200px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged" TabIndex="7">
                                </asp:DropDownList>
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="5">
                            </td>
                            <td align="left" width="100" valign="top">
                                <asp:Label ID="lblDeliveryDate" runat="server" Font-Bold="False" Text="Delivery Date"></asp:Label>
                            </td>
                            <td width="200" valign="top">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:DateControl ID="txtDeliveryDate" runat="server" TabIndex="1"></cc1:DateControl>
                                        </td>
                                        <td>
                                            <input id="lnkPickDeliveryDate" style="height: 20px" type="button" value=".." onclick="displayDatePicker('txtDeliveryDate');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="200">
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDeliveryDate"
                                    ErrorMessage="empty!" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDeliveryDate"
                                    Display="Dynamic" ErrorMessage="invalid date" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"></asp:RegularExpressionValidator>
                            </td>
                            <td align="left" width="70" valign="top">
                                <asp:Label ID="lblName" runat="server" Font-Bold="False" Text="Name"></asp:Label>
                            </td>
                            <td colspan="2" valign="top" width="250">
                                <asp:Label ID="lblPrintSupplierName" runat="server" Font-Bold="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="5">
                            </td>
                            <td align="left" valign="top" colspan="2">
                                <asp:Label ID="lblDeliveryPlace" runat="server" Font-Bold="True" Text="Delivery Place"></asp:Label>
                            </td>
                            <td width="200">
                            </td>
                            <td align="left" width="70" valign="top">
                                <asp:Label ID="lblAddress" runat="server" Font-Bold="False" Text="Address"></asp:Label>
                            </td>
                            <td colspan="2" rowspan="2" valign="top" width="250">
                                <asp:Label ID="lblPrintSupplierAddress" runat="server" Font-Bold="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="5">
                            </td>
                            <td align="left" valign="top" width="100">
                                <asp:Label ID="lblDHouse" runat="server" Font-Bold="False" Text="House #"></asp:Label>
                            </td>
                            <td valign="top" width="200">
                                <asp:TextBox ID="txtDHouse" runat="server" Width="140px" TabIndex="2" MaxLength="20"
                                    Text="156"></asp:TextBox>
                            </td>
                            <td width="200">
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDHouse"
                                    ErrorMessage="empty!" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" valign="top" width="70">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="5" style="height: 26px">
                            </td>
                            <td align="left" valign="top" width="100" style="height: 26px">
                                <asp:Label ID="lblDAddress" runat="server" Font-Bold="False" Text="Address"></asp:Label>
                            </td>
                            <td valign="top" width="200" style="height: 26px">
                                <asp:TextBox ID="txtDAddress" runat="server" Width="140px" TabIndex="3" MaxLength="100"
                                    Text="Hoofdweg"></asp:TextBox>
                            </td>
                            <td width="200" style="height: 26px">
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDAddress"
                                    ErrorMessage="empty!" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" valign="top" width="70" style="height: 26px">
                            </td>
                            <td colspan="2" valign="top" width="250" style="height: 26px">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="5">
                            </td>
                            <td align="left" valign="top" width="100">
                                <asp:Label ID="lblDPostCode" runat="server" Font-Bold="False" Text="Postcode"></asp:Label>
                            </td>
                            <td valign="top" width="200">
                                <asp:TextBox ID="txtDPostCode" runat="server" Width="140px" TabIndex="4" MaxLength="10"
                                    Text="9341 BM"></asp:TextBox>
                            </td>
                            <td width="200">
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDPostCode"
                                    ErrorMessage="empty!" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" valign="top" width="70">
                                <asp:Label ID="lblOrderBy" runat="server" Font-Bold="False" Text="Ordered  By"></asp:Label>
                            </td>
                            <td colspan="2" valign="top" width="250">
                                <asp:TextBox ID="txtOrderBy" runat="server" Width="200px" TabIndex="8" MaxLength="50"
                                    Text="Arjen Sierksma"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtOrderBy"
                                    ErrorMessage="empty!" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="5">
                            </td>
                            <td align="left" valign="top" width="100">
                                <asp:Label ID="lblDResidence" runat="server" Font-Bold="False" Text="Residence"></asp:Label>
                            </td>
                            <td valign="top" width="200">
                                <asp:TextBox ID="txtDResidence" runat="server" Width="140px" TabIndex="5" MaxLength="50"
                                    Text="Veenhuizen"></asp:TextBox>
                            </td>
                            <td width="200">
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDResidence"
                                    ErrorMessage="empty!" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" valign="top" width="70">
                            </td>
                            <td colspan="2" valign="top" width="250">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="5">
                            </td>
                            <td align="left" valign="top" width="100">
                                <asp:Label ID="lblDCountry" runat="server" Font-Bold="False" Text="Country"></asp:Label>
                            </td>
                            <td valign="top" width="200">
                                <asp:DropDownList ID="ddlDCountry" runat="server" Width="140px" TabIndex="6">
                                </asp:DropDownList>
                            </td>
                            <td width="200">
                                &nbsp;
                            </td>
                            <td align="left" valign="top" width="70">
                            </td>
                            <td colspan="2" valign="top" width="250">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 10px">
                <td align="left" bgcolor="white" style="height: 10px; width: 883px;">
                </td>
            </tr>
            <tr>
                <td bgcolor="#F0F0F0" style="width: 883px">
                    <asp:UpdatePanel runat="server" ID="upnlOrder" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="grdArticle" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:GridView ID="grdOrder" GridLines="Both" BorderWidth="1px" CellPadding="5" runat="server"
                                AutoGenerateColumns="False" Width="882px" BorderColor="white" BorderStyle="Solid"
                                ShowFooter="True" FooterStyle-BackColor="#E0E0E0">
                                <Columns>
                                    <asp:TemplateField HeaderText="Article Code">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblArticle" Text='<%# DataBinder.Eval(Container.DataItem,"articlecode")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblFooterArticleCode" runat="server" Text="" Width="100px" BackColor="white"
                                                ForeColor="black"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" Width="100px" />
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Title">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "title")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="300px" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "title")%>'></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" Width="280px" />
                                        <HeaderStyle HorizontalAlign="Left" Width="280px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SupplyersArticleCode">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem,"supplyArticleID")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="10px" />
                                        <FooterTemplate>
                                            <asp:TextBox runat="server" Style="text-align: right" ID="txtSupplyOrderArticleCode"
                                                Text="" MaxLength="50" Width="60px"></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" Width="10px" />
                                        <HeaderStyle HorizontalAlign="Left" Width="10px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Stock">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "stock")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblStock" Text='<%# DataBinder.Eval(Container.DataItem, "stock")%>'></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Width="100px" />
                                        <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <FooterTemplate>
                                            <asp:TextBox runat="server" Style="text-align: right" ID="IntegerControl1" Text=""
                                                MaxLength="12" Width="60px" onfocus="javascript: SetLatestValue(this.value)"
                                                onchange="return CheckCorrectValue(this,'0123456789')" onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789')"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "qty")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="60px" />
                                        <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        <HeaderStyle HorizontalAlign="Right" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price">
                                        <FooterTemplate>
                                            <asp:TextBox runat="server" ID="txtPrice" Style="text-align: right" Text="" Width="80px"
                                                MaxLength="12" onfocus="javascript: SetLatestValue(this.value)" onchange="return CheckCorrectValue(this,'0123456789,')"
                                                onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789,')"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "price")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                                        <FooterStyle HorizontalAlign="Right" Width="70px" />
                                        <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VAT(%)">
                                        <FooterTemplate>
                                            <asp:TextBox runat="server" ID="txtVat" Style="text-align: right" Text="" Width="50px"
                                                MaxLength="12" onfocus="javascript: SetLatestValue(this.value)" onchange="return CheckCorrectValue(this,'0123456789,')"
                                                onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789,')"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "vat")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                        <HeaderStyle HorizontalAlign="Right" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net Price">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "netprice")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDelete" CausesValidation="false" Text="delete"
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"articlecode")%>' OnCommand="grdOrder_RowDelete"></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton runat="server" ID="lnkInsert" CausesValidation="false" Text="Confirm"
                                                OnClick="lnkInsert_Click"></asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="60px" />
                                        <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        <HeaderStyle HorizontalAlign="Right" Width="60px" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#DEDEDE" BorderColor="White" HorizontalAlign="Center" Font-Size="11px" />
                                <AlternatingRowStyle BackColor="#EFEFEF" />
                                <RowStyle BackColor="White" VerticalAlign="Middle" HorizontalAlign="Center" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="background-color: White">
                        <asp:UpdatePanel runat="server" ID="outerUpdatePanel" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:LinkButton runat="server" ID="outerPanelTrigger" Text="OuterPanelTrigger" Visible="false" />
                                <asp:LinkButton runat="server" ID="lnkArticle" Text="Add Article" Style="padding-left: 5px" />
                                <ajaxToolkit:ModalPopupExtender runat="server" ID="modalPopupExtender2" TargetControlID="lnkArticle"
                                    PopupControlID="pnlArticle" OkControlID="lnkOK" CancelControlID="lnkArticleCancel"
                                    BackgroundCssClass="modalBackground">
                                </ajaxToolkit:ModalPopupExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="padding: 5px">
                    <hr style="border: 2px solid #2bab1c" />
<%--                    <asp:UpdatePanel runat="server" ID="upnlButtons" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="grdOrder" />
                        </Triggers>
                        <ContentTemplate>
                           
                        </ContentTemplate>
                    </asp:UpdatePanel>
--%>                            <asp:LinkButton ID="lnkSubmit" runat="server" Visible="false" Text="Save Order" OnClick="lnkSubmit_Click" />
                            <asp:LinkButton ID="lnkSendOrder" runat="server" Visible="false" Text="Send Order"
                                OnClick="lnkSendOrder_Click" />
                     <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" OnClick="lnkCancel_Click" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#F0F0F0" align="center" style="width: 883px">
                    <div id="overlay" class="overlay">
                        <%--<div id="divArticle" style="overflow:auto;visibility:hidden;height:0px;width:577px; align:center" >--%>
                        <div style="background-color: #800728; margin: 10px;">
                            <table border="0" align="center">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td style="width: 200px">
                                                </td>
                                                <td style="width: 400px">
                                                    <asp:Label ID="lblOverlayHeader" runat="server" Text="List of Articles" ForeColor="White"></asp:Label>
                                                </td>
                                                <td style="width: 200px; padding-right: 3px;" align="right">
                                                    <asp:LinkButton ID="lnkClose" runat="server" Text="X" OnClientClick='return PopupArticle()' />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <asp:Panel runat="server" ID="pnlArticle" BackColor="gray" Style="display: none">
            <asp:UpdatePanel runat="server" ID="updatePanel3" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <cc1:PagingGridView ID="grdArticle" AllowSorting="True" AllowPaging="true" PageSize="10"
                        PagerSettings-Mode="NumericFirstLast" BorderWidth="0px" BorderStyle="Outset"
                        CellPadding="3" runat="server" AutoGenerateColumns="False" Width="550px" GridLines="none"
                        BorderColor="white" OnSorting="grdArticle_Sorting" OnPageIndexChanging="grdArticle_PageIndexChanging"
                        OnRowDataBound="grdArticle_RowDataBound">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="top" Height="10px"></HeaderStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Title" SortExpression="title">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "code")%>'
                                        OnCommand="lnkSelect_Command">
                                        <asp:Label ID="lblOrderId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "title")%>'></asp:Label></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" Width="250px" />
                                <HeaderStyle HorizontalAlign="left" Width="250px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type" SortExpression="type">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "type")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="75px" />
                                <HeaderStyle HorizontalAlign="Left" Width="75px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Author" SortExpression="author">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "author")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="175px" />
                                <HeaderStyle HorizontalAlign="Left" Width="175px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stock" SortExpression="qty">
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
                    </cc1:PagingGridView>
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
