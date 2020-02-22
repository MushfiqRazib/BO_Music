<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="delivery, Bo02" title="Delivery Page" theme="ThemeOne" %>

<%@ MasterType VirtualPath="~/MainLayOut.master" %>
<%@ Register Assembly="HawarIT.WebControls" Namespace="HawarIT.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
function disp_confirm()
  {
  var r=confirm("Press a button")
  if (r==true)
    {
     return true;
    }
  else
    {
    return false; 
    }
  }
    </script>

    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" runat="server" id="header" class="deliveryTdStyle1">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 834px">
                <table cellpadding="0" cellspacing="0" class="deliveryTdStyle2">
                    <tr>
                        <td valign="top" align="left" class="deliveryTdStyle3">
                          <%--  <asp:Label ID="lblCurrentPage" runat="server" Font-Bold='true' Text="Current page">  </asp:Label>&nbsp;&nbsp;<b>:</b>&nbsp;&nbsp;<asp:Label
                                ID="lblPageRoot" runat="server" ForeColor="AppWorkspace" Text="home "></asp:Label>-<asp:Label
                                    ID="lblActivePage" runat="server" ForeColor="#3300ff" Text="order page "></asp:Label>--%>&nbsp;&nbsp;
                        </td>
                        <td valign="top" align="right" style="padding-right: 3px">
                            <table>
                                <tr style="height: 58px;">
                                    <td align="right" style="width: 31px; height: 58px;">
                                        <img src="graphics/step3.gif" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="deliveryTdStyle4">
                            <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                <tr style="height: 20px">
                                    <td colspan="2" style="height: 20px; width: 873px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width: 873px;">
                                        <table cellpadding="4" cellspacing="0" border="0" style="background-color: #FAFAFA;
                                            text-align: left;" width="100%" >
                                            <tr style="background-color: #FAFAFA;" valign="top">
                                                <td>
                                                    <table border="0" cellspacing="0" cellpadding="2" align="left" style="height: 33px">
                                                        <tr>
                                                            <td colspan="3" align="left" valign="top">
                                                                <asp:Label Font-Bold="true" ID="lblHeader" runat="server" Text="Delivery Address"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" align="left" valign="top">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <asp:Label ID="lblInitialName" runat="server" Text="Initial Name"></asp:Label></td>
                                                            <td colspan="2" align="left" valign="top">
                                                                <asp:DropDownList ID="ddlInitialName" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <asp:Label ID="lblDFName" runat="server" Text="First Name"></asp:Label></td>
                                                            <td align="left" valign="top">
                                                                <asp:TextBox ID="txtDFName" runat="server" CssClass="textbox"></asp:TextBox></td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="valFName" runat="server" ControlToValidate="txtDFName"
                                                                    ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top" style="height: 26px;">
                                                                <asp:Label ID="lblDMName" runat="server" Text="Middle Name"></asp:Label></td>
                                                            <td align="left" valign="top">
                                                                <asp:TextBox ID="txtDMName" runat="server" CssClass="textbox"></asp:TextBox></td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <asp:Label ID="lblDLName" runat="server" Text="Last Name"></asp:Label></td>
                                                            <td align="left" valign="top">
                                                                <asp:TextBox ID="txtDLName" runat="server" CssClass="textbox"></asp:TextBox></td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="valLName" runat="server" ControlToValidate="txtDLName"
                                                                    ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <asp:Label ID="lblDHouseNum" runat="server" Text="House#">
                                                                </asp:Label>&nbsp;</td>
                                                            <td align="left" valign="top">
                                                                <asp:TextBox ID="txtDHouseNum" runat="server" CssClass="textbox"></asp:TextBox></td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="valHouseNum" runat="server" ControlToValidate="txtDHouseNum"
                                                                    ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <asp:Label ID="lblDAddress" runat="server" Text="Address">
                                                                </asp:Label>&nbsp;</td>
                                                            <td align="left" valign="top">
                                                                <asp:TextBox ID="txtDAddress" runat="server" CssClass="textbox"></asp:TextBox></td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="valAddress" runat="server" ControlToValidate="txtDAddress"
                                                                    ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <asp:Label ID="lblDResidence" runat="server" Text="Residence">
                                                                </asp:Label>&nbsp;</td>
                                                            <td align="left" valign="top">
                                                                <asp:TextBox ID="txtDResidence" runat="server" CssClass="textbox"></asp:TextBox></td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="valResidence" runat="server" ControlToValidate="txtDResidence"
                                                                    ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <asp:Label ID="lblDPostCode" runat="server" Text="Post Code">
                                                                </asp:Label>&nbsp;</td>
                                                            <td align="left" valign="top">
                                                                <asp:TextBox ID="txtDPostCode" runat="server" CssClass="textbox" MaxLength="10"></asp:TextBox></td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="valPostCode" runat="server" ControlToValidate="txtDPostCode"
                                                                    ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <asp:Label ID="lblDCountry" runat="server" Text="Country">
                                                                </asp:Label></td>
                                                            <td colspan="2" align="left" valign="top">
                                                                <asp:DropDownList ID="ddlCountry" runat="server" Width="205px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="background-color: #FFFFFF;">
                                    <td colspan="3" align="left" valign="bottom" style="height: 30px; width: 873px;">
                                        <table border="0" cellspacing="0" cellpadding="3px" id="table_inner" align="left"
                                            width="100%">
                                            <tr>
                                                <td align="right" valign="bottom" style="padding-bottom: 0; height: 25px;">
                                                    <table cellspacing="0" border="0" class="homeTdStyle2">
                                                        <tr>
                                                            <td style=" padding-left:3px; padding-right:3px; padding-top:3px; padding-bottom:3px;">
                                                                <asp:ImageButton ID="btnNext" runat="server" ImageAlign="AbsMiddle" OnClick="btnNext_Click" ImageUrl="graphics/btn_next.png" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="left">
                                        <td>
                                </tr>
                                <tr style="height: 30px">
                                    <td colspan="2" align="right" valign="bottom" style="height: 30px; width: 873px;">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
