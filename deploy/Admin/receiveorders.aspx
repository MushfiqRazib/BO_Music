<%@ page language="C#" culture="nl-NL" autoeventwireup="true" inherits="Admin_receiveorders, App_Web_receiveorders.aspx.fdf7a39c" title="Receiving Orders" theme="ThemeOne" %>

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

    <script type="text/javascript">

        function ChangeSaveButtonStatus() {
            return;
            var button = GetObject('lnkSave');
            var status = 1;
            var elm = document.aspnetForm.elements;
            for (i = 0; i < elm.length; i++) {
                if (elm[i].id.indexOf('intCtrRecvQty') != -1) {
                    if (elm[i].value > 0) {
                        button.disabled = false;
                        status = 0;
                        break;
                    }
                }
            }
            if (status == 1) {
                button.disabled = true;
            }

        }



    </script>

</head>
<body class="adminbody">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" border="0" class="contentArea">
            <tr height="76px" valign="top">
                <td class="header" colspan="2">
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" colspan="2">
                    <table cellpadding="5" cellspacing="0" border="0" width="882px" style="background-color: #DEDEDE;">
                        <tr>
                            <td valign="top" align="left" class="contentHeader">
                               
                                    Receive Order
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" width="50%">
                    <table cellpadding="5" cellspacing="0" border="0">
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="lblReceiveNo" runat="server" Text="Receive# " ForeColor="red" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblReceiveNoValue" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="lblReceiveDt" runat="server" Text="Receive Date:"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtReceiveDt" runat="server" MaxLength="10"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="valDateRequired" runat="server" Text="Required" ControlToValidate="txtReceiveDt"
                                    Display="dynamic" EnableClientScript="true"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="valDate" runat="server" ControlToValidate="txtReceiveDt"
                                    Display="Dynamic" Text="Invalid Date Format" EnableClientScript="true" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"></asp:RegularExpressionValidator>
                            </td>
                            <td align="left" valign="top" style="padding-left: 0px;">
                                <input style="height: 21px" type="button" value=".." onclick="displayDatePicker('txtReceiveDt');" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="lblReceiveBy" runat="server" Text="Receive By:"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="2">
                                <asp:TextBox ID="txtReceiveBy" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="top">
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="left" valign="top" width="50%" style="padding-left: 185px;">
                    <table cellpadding="5" cellspacing="0" border="0">
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="lblSupplyOrder" runat="server" Text="Supply Order#: "></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblSupplyOrderValue" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="lblSupOrdDate" runat="server" Text="Supply Order Date:"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblSupOrdDateValue" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="lblSupDelDate" runat="server" Text="Delivery Date:"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblSupDelDateValue" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <table cellpadding="5" cellspacing="0" border="0">
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="lblRemarks" runat="server" Text="Remarks:"></asp:Label>
                            </td>
                            <td align="left" valign="top" style="padding-left: 25px;">
                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="350px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" colspan="2">
                    <asp:GridView ID="grdReceive" runat="server" GridLines="both" BorderStyle="solid"
                        BorderWidth="1px" AutoGenerateColumns="false" CellPadding="5" CellSpacing="0"
                        Width="882px">
                        <Columns>
                            <asp:TemplateField HeaderText="Article">
                                <ItemTemplate>
                                    <asp:Label ID="lblArticle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "title")%> '></asp:Label>
                                    <asp:Label ID="lblArticleCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "articlecode")%> '
                                        Visible="false"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="300px" />
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Price">
                                <ItemTemplate>
                                    €
                                    <asp:Label ID="lblUnitPrice" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "unitprice")%> '></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="right" VerticalAlign="Top" Width="100px" />
                                <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Current Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "receivingstatus")%> '></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" />
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "orderqty")%> '></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="right" VerticalAlign="Top" Width="82px" />
                                <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="82px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Previous Received Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblPrevRecvQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "previous")%> '></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="right" VerticalAlign="Top" Width="100px" />
                                <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received Qty">
                                <ItemTemplate>
                                    <asp:TextBox Enabled="false" runat="server" Style="text-align: right" ID="intCtrRecvQty"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "orderqty")%> ' MaxLength="6" Width="50px"
                                        onfocus="javascript: SetLatestValue(this.value)" onchange="CheckCorrectValue(this,'0123456789'); ChangeSaveButtonStatus();"
                                        onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789')"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="valRcQty" runat="server" ErrorMessage="Required"
                                        ControlToValidate="intCtrRecvQty" Display="Dynamic"></asp:RequiredFieldValidator>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="right" VerticalAlign="Top" Width="100px" />
                                <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Purchase Price">
                                <ItemTemplate>
                                    €
                                    <asp:TextBox runat="server" ID="intCtrPurchasePrice" Style="text-align: right" Text='<%# DataBinder.Eval(Container.DataItem, "unitprice")%> '
                                        Width="50px" MaxLength="6" onfocus="javascript: SetLatestValue(this.value)" onchange="return CheckCorrectValue(this,'0123456789,')"
                                        onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789,')"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="valPPrice" runat="server" ErrorMessage="Required"
                                        ControlToValidate="intCtrPurchasePrice" Display="Dynamic"></asp:RequiredFieldValidator>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="100px" />
                                <HeaderStyle HorizontalAlign="right" VerticalAlign="top" Width="100px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" valign="top" style="background-color: #DEDEDE;">
                    <asp:Label ID="lblErrorMsg" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding: 5px" colspan="2">
                    <hr style="border: 2px solid #2bab1c" />
                    <asp:LinkButton ID="lnkSave" runat="server" Text="Save" OnClick="lnkSave_Click" />
                    <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" OnClick="lnkCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
