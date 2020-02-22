<%@ Page Language="C#" MasterPageFile="~/MainLayOut.master" AutoEventWireup="true"
    CodeFile="register.aspx.cs" Inherits="Register" Title="Registration" %>

<%@ MasterType VirtualPath="~/MainLayOut.master" %>
<%@ Register TagPrefix="uc2" TagName="ContactWidget" Src="~/contact_detail_widget.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Subscribe" Src="~/subscribe_widget.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="Server">
    <link href="include/kallol.css" rel="stylesheet" type="text/css" />
    <div class="content-header">
        <div class="content-header-container">
            <label id="lblAboutUs" runat="server">
                <%= (string)base.GetGlobalResourceObject("string", "header_register") %></label>
        </div>
    </div>
    <table cellpadding="0" cellspacing="0" width="882">
        <tr>
            <td colspan="2" class="registerTDStyle1">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 800px">
                <table class="table-content" cellpadding="0" cellspacing="0" style="background-color: #F0F0F0;
                    width: 882px;">
                    <tr>
                        <td>
                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 217px;" align="left">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr style="background-color: #FFFFFF;">
                                    <td colspan="2" align="left" valign="bottom" style="height: 30px">
                                        <table align="center" border="0" height="100%" width="100%">
                                            <tr valign="middle">
                                                <td align="left" style="height: 18px" valign="middle">
                                                    <table border="0" id="table3" cellpadding="2" cellspacing="0" align="left" style="height: 110px">
                                                        <tr>
                                                            <td style="background-color: white;">
                                                                <table style="height: 110px" id="table1" cellspacing="0" cellpadding="2" align="left"
                                                                    border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td colspan="3">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <asp:Label ID="lblRegistrationHeader" runat="server" Text="My account details" Font-Size="12px"
                                                                                    Font-Bold="true"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 16px; width: 20%;" valign="bottom" align="right">
                                                                            </td>
                                                                            <td style="height: 16px; width: 30%" valign="bottom" align="left">
                                                                                <asp:Label ID="Label13" runat="server" Font-Italic="true" Text="Fields with * are required"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 16px; width: 50%" valign="bottom" align="left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 20%;" valign="top" align="right">
                                                                                <asp:Label ID="lblCompany" runat="server" Text="Company Name"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px; width: 30%" valign="top" align="left">
                                                                                <asp:TextBox ID="txtCompany" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                            </td>
                                                                            <td style="height: 26px; width: 60%" valign="top" align="left">
                                                                                <ul class="registration-li">
                                                                                    <li><span>
                                                                                        <%= (string)base.GetGlobalResourceObject("string", "registration_notice_1")%></span>
                                                                                    </li>
                                                                                    <li><span>
                                                                                        <%= (string)base.GetGlobalResourceObject("string", "registration_notice_2")%></span></li>
                                                                                </ul>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="middle" align="right">
                                                                                <asp:Label ID="lblInitialName" runat="server" Text="Initial Name"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="200">
                                                                                <asp:RadioButtonList runat="server" ID="rdoInitialName" RepeatLayout="Table" RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Text="Mr." Value="Mr."></asp:ListItem>
                                                                                    <asp:ListItem Text="Mrs." Value="Mrs."></asp:ListItem>
                                                                                    <asp:ListItem Text="Dhr." Value="Dhr."></asp:ListItem>
                                                                                    <asp:ListItem Text="Mevr." Value="Mevr."></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="300">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:TextBox ID="txtFName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                                <span style='font-size: 22px; vertical-align: top;'>*</span>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="300">
                                                                                <asp:RequiredFieldValidator ValidationGroup="registration_validate" ID="valFName"
                                                                                    runat="server" ErrorMessage="First Name Required" ControlToValidate="txtFName"
                                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="lblMName" runat="server" Text="Middle Name"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:TextBox ID="txtMName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="300">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="lblLastName" runat="server" Text="LastName"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:TextBox ID="txtLName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                                <span style='font-size: 22px; vertical-align: top;'>*</span>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="300">
                                                                                <asp:RequiredFieldValidator ID="rfvTxtLName" runat="server" ErrorMessage="First Name Required"
                                                                                    ValidationGroup="registration_validate" ControlToValidate="txtLName" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="lblHouse" runat="server" Text="House #"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:TextBox ID="txtHousenr" runat="server" CssClass="textbox" MaxLength="20"></asp:TextBox>
                                                                                <span style='font-size: 22px; vertical-align: top;'>*</span>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="300">
                                                                                <asp:RequiredFieldValidator ID="rfvTxtHousenr" runat="server" ErrorMessage="" ValidationGroup="registration_validate"
                                                                                    ControlToValidate="txtHousenr" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                                <span style='font-size: 22px; vertical-align: top;'>*</span>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="300">
                                                                                <asp:RequiredFieldValidator ID="rfvTxtAddress" runat="server" ErrorMessage="" ValidationGroup="registration_validate"
                                                                                    ControlToValidate="txtAddress" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="lblPostCode" runat="server" Text="PostCode"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:TextBox ID="txtPostCode" runat="server" CssClass="textbox" MaxLength="10"></asp:TextBox>
                                                                                <span style='font-size: 22px; vertical-align: top;'>*</span>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="300">
                                                                                <asp:RequiredFieldValidator ID="rfvTxtPostCode" runat="server" ErrorMessage="" ValidationGroup="registration_validate"
                                                                                    ControlToValidate="txtPostCode" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="lblResidence" runat="server" Text="Residence"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:TextBox ID="txtResidence" runat="server" CssClass="textbox" MaxLength="50"></asp:TextBox>
                                                                                <span style='font-size: 22px; vertical-align: top;'>*</span>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="300">
                                                                                <asp:RequiredFieldValidator ID="valResidence" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                                    ValidationGroup="registration_validate" ControlToValidate="txtResidence" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="lblCountry" runat="server" Text="Country"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:DropDownList ID="ddlCountry" runat="server" Width="125px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="300">
                                                                                <asp:RequiredFieldValidator ID="valCountry" runat="server" ErrorMessage="Select a Country"
                                                                                    ValidationGroup="registration_validate" ControlToValidate="ddlCountry" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="lblTelephone" runat="server" Text="Telephone"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:TextBox ID="txtTelephone" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="300">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="lblFax" runat="server" Text="Fax"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:TextBox ID="txtFax" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="300">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="lblEMail" runat="server" Text="E-mail"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="50"></asp:TextBox>
                                                                                <span style='font-size: 22px; vertical-align: top;'>*</span>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="300">
                                                                                <asp:RequiredFieldValidator ID="valEmail" runat="server" ErrorMessage="E-mail address Required"
                                                                                    ValidationGroup="registration_validate" ControlToValidate="txtEmail" Display="Dynamic"
                                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                                                        ID="valInvalidEmail" runat="server" ErrorMessage="Invalid E-mail address" ControlToValidate="txtEmail"
                                                                                        Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="lblWebsite" runat="server" Text="Web- site"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:TextBox ID="txtWebsite" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                            </td>
                                                                            <td style="height: 26px" width="300">
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="4" style="height: 17px" valign="middle">
                                                                <div class="seperator">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="background-color: white; height: 329px;">
                                                                <table style="height: 110px" id="table4" cellspacing="0" cellpadding="2" align="left"
                                                                    border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td colspan="3">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <asp:Label ID="Label14" runat="server" Text="Delivary address" Font-Size="12px" Font-Bold="true"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 18%;">
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="80%" colspan="2">
                                                                                <asp:CheckBox ID="chkDelivery" CausesValidation=false AutoPostBack=true  OnCheckedChanged="chkDelivery_CheckedChanged"  runat="server" Text="Different Delevery Address" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="middle" align="right">
                                                                                <asp:Label ID="Label1" runat="server" Text="Initial Name"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:RadioButtonList runat="server" ID="rdoDInitialName" RepeatLayout="Table" RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Text="Mr." Value="Mr."></asp:ListItem>
                                                                                    <asp:ListItem Text="Mrs." Value="Mrs."></asp:ListItem>
                                                                                    <asp:ListItem Text="Dhr." Value="Dhr."></asp:ListItem>
                                                                                    <asp:ListItem Text="Mevr." Value="Mevr."></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="Label2" runat="server" Text="First Name"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:TextBox ID="txtDFName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                                <span style='font-size: 22px; vertical-align: top;'>*</span>
                                                                                <asp:RequiredFieldValidator ID="rfvTxtDFName" runat="server" ErrorMessage="First Name Required"
                                                                                    ValidationGroup="registration_delivery_validate" ControlToValidate="txtDFName" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="Label3" runat="server" Text="Middle Name"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:TextBox ID="txtDMName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left">
                                                                                &nbsp;&nbsp;&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="Label4" runat="server" Text="LastName"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:TextBox ID="txtDLName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                                <span style='font-size: 22px; vertical-align: top;'>*</span>
                                                                                <asp:RequiredFieldValidator ID="rfvTxtDLName" runat="server" ErrorMessage="Last Name Required"
                                                                                    ValidationGroup="registration_delivery_validate" ControlToValidate="txtDLName" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="Label5" runat="server" Text="House #"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:TextBox ID="txtDHousenr" runat="server" CssClass="textbox" MaxLength="20"></asp:TextBox>
                                                                                <span style='font-size: 22px; vertical-align: top;'>*</span>
                                                                                <asp:RequiredFieldValidator ID="rfvTxtDHousenr" runat="server" ErrorMessage="" ValidationGroup="registration_delivery_validate"
                                                                                    ControlToValidate="txtDHousenr" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="Label6" runat="server" Text="Address"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:TextBox ID="txtDAddress" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                                <span style='font-size: 22px; vertical-align: top;'>*</span>
                                                                                <asp:RequiredFieldValidator ID="rfvTxtDAddress" runat="server" ErrorMessage="" ValidationGroup="registration_delivery_validate"
                                                                                    ControlToValidate="txtDAddress" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="Label7" runat="server" Text="PostCode"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:TextBox ID="txtDPostCode" runat="server" CssClass="textbox" MaxLength="10"></asp:TextBox>
                                                                                <span style='font-size: 22px; vertical-align: top;'>*</span>
                                                                                <asp:RequiredFieldValidator ID="rfvTxtDPostCode" runat="server" ErrorMessage="" ValidationGroup="registration_delivery_validate"
                                                                                    ControlToValidate="txtDPostCode" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="Label8" runat="server" Text="Residence"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:TextBox ID="txtDResidence" runat="server" CssClass="textbox" MaxLength="50"></asp:TextBox>
                                                                                <span style='font-size: 22px; vertical-align: top;'>*</span>
                                                                                <asp:RequiredFieldValidator ID="rfvTxtDResidence" runat="server" ErrorMessage=""
                                                                                    ValidationGroup="registration_delivery_validate" ControlToValidate="txtDResidence" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                                <asp:Label ID="Label9" runat="server" Text="Country"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left" width="100">
                                                                                <asp:DropDownList ID="ddlDCountry" runat="server" CssClass="textbox" Width="125px">
                                                                                </asp:DropDownList>
                                                                                <span style='font-size: 22px; vertical-align: top;'>*</span>
                                                                            </td>
                                                                            <td style="height: 26px" valign="top" align="left">
                                                                                &nbsp;&nbsp;&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="4" style="height: 17px" valign="middle">
                                                                <div class="seperator">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td colspan="4">
                                                                            <asp:Label ID="Label16" runat="server" Text="Password" Font-Size="12px" Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 26px; width: 130px;" valign="top" align="right">
                                                                            <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                                                                        </td>
                                                                        <td style="height: 26px" valign="top" align="left" width="160px">
                                                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox" TextMode="Password"
                                                                                MaxLength="50"></asp:TextBox>
                                                                            <span style='font-size: 22px; vertical-align: top;'>*</span>
                                                                        </td>
                                                                        <td style="height: 26px" valign="top" align="left" width="300">
                                                                            <asp:RequiredFieldValidator ID="reqPasswordValidator" runat="server" ErrorMessage="Password Required"
                                                                                ValidationGroup="registration_validate" ControlToValidate="txtPassword" Display="Dynamic"
                                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 130px; height: 26px" valign="top">
                                                                            <asp:Label ID="lblRePassword" runat="server" Text="Re-Type Password"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="height: 26px" valign="top" width="160px">
                                                                            <asp:TextBox ID="txtRePassword" runat="server" CssClass="textbox" MaxLength="50"
                                                                                TextMode="Password"></asp:TextBox>
                                                                            <span style='font-size: 22px; vertical-align: top;'>*</span>
                                                                        </td>
                                                                        <td align="left" style="height: 26px" valign="top" width="300">
                                                                            <asp:RequiredFieldValidator ID="valPassword" runat="server" ControlToValidate="txtRePassword"
                                                                                ValidationGroup="registration_validate" Display="Dynamic" ErrorMessage="Re-Type Password Required"
                                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td style="width: 18%">
                                                                        </td>
                                                                        <td style="width: 82%">
                                                                            <asp:CheckBox ID="chkAgree" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td valign="middle" style="height: 29px; width: 20px">
                                                                            <asp:Button ValidationGroup="registration_validate" ID="btnSubmit" runat="server"
                                                                                Text="Register" CssClass="button" OnClick="btnSubmit_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
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
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarPlace" runat="Server">
    <uc2:ContactWidget ID="ContactWidget1" runat="server" />
    <uc1:Subscribe runat="server" ID="usbscribe" />
</asp:Content>
