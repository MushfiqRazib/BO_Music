<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="Register, Bo02" title="Registration" theme="ThemeOne" %>

<%@ MasterType VirtualPath="~/MainLayOut.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
<!--
   var Page_Validators = new Array(
   document.all["valFName"], 
   document.all["valLName"], 
   document.all["valHousenr"]
   );
     // -->
    </script>

    <table cellpadding="0" cellspacing="0" width="882">
        <tr>
            <td colspan="2" class="registerTDStyle1">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 834px">
                <table cellpadding="0" cellspacing="0" style="background-color: #F0F0F0; width: 882px;">
                    <tr>
                        <td valign="top" align="left" class="registerTDStyle2" style="height: 20px">
                            &nbsp;<asp:Label ID="lblCurrentPage" runat="server" Font-Bold="true" Text="">  </asp:Label>&nbsp;&nbsp;<b></b>&nbsp;&nbsp;
                            <asp:Label ID="lblPageRoot" runat="server" ForeColor="AppWorkspace" Text=" "></asp:Label>
                            <asp:Label ID="lblActivePage" runat="server" ForeColor="#3300ff" Text=" ">
                            </asp:Label>
                        </td>
                        <td valign="top" align="right" style="padding-right: 3px; height: 20px;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 217px; padding-left: 5px; padding-right: 5px">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr style="background-color: #FFFFFF;">
                                    <td colspan="2" align="right" valign="bottom" style="height: 30px">
                                        <table align="center" border="0" height="100%" width="100%">
                                            <tr valign="middle">
                                                <td style="height: 18px" width="5">
                                                </td>
                                                <td align="left" colspan="4" style="height: 18px" valign="middle">
                                                    <table border="0" id="table3" cellpadding="2" cellspacing="0" align="center" style="height: 110px">
                                                        <tr>
                                                            <td style="background-color: white;">
                                                                <fieldset>
                                                                    <legend>
                                                                        <asp:Label ID="lblInvoice" runat="server" Font-Bold="True" ForeColor="Black">Invoice Info</asp:Label></legend>
                                                                    <table style="height: 110px" id="table1" cellspacing="0" cellpadding="2" align="center"
                                                                        border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" align="right">
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="lblInitialName" runat="server" Text="Initial Name"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:DropDownList ID="ddlInitialName" runat="server">
                                                                                    </asp:DropDownList></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="300">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" valign="top" align="right">
                                                                                    <span style="color: #ff0000"></span>
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="lblCompany" runat="server" Text="Company Name"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtCompany" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" align="right">
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtFName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left" width="300">
                                                                                    <asp:RequiredFieldValidator ID="valFName" runat="server" ErrorMessage="First Name Required"
                                                                                        ControlToValidate="txtFName" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" align="right">
                                                                                    <span style="color: #ff0000"></span>
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="lblMName" runat="server" Text="Middle Name"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtMName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="300">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" align="right">
                                                                                    <span style="color: #ff0000"></span>
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="lblLastName" runat="server" Text="LastName"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtLName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left" width="300">
                                                                                    <asp:RequiredFieldValidator ID="valLName" runat="server" ErrorMessage="Last Name Required"
                                                                                        ControlToValidate="txtLName" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                                            </tr>
                                                                            
                                                                            <tr>
                                                                                <td style="height: 26px" valign="top" align="right">
                                                                                    <span style="color: #ff0000"></span>
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="lblHouse" runat="server" Text="House #"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtHousenr" runat="server" CssClass="textbox" MaxLength="20"></asp:TextBox>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left" width="300">
                                                                                    <asp:RequiredFieldValidator ID="valHousenr" runat="server" ErrorMessage="House Number Required"
                                                                                        ControlToValidate="txtHousenr" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" valign="top" align="right">
                                                                                    <span style="color: #ff0000"></span>
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left" width="300">
                                                                                    <asp:RequiredFieldValidator ID="valAddress" runat="server" ErrorMessage="Address Required"
                                                                                        ControlToValidate="txtAddress" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" valign="top" align="right">
                                                                                    <span style="color: #ff0000"></span>
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="lblPostCode" runat="server" Text="PostCode"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtPostCode" runat="server" CssClass="textbox" MaxLength="10"></asp:TextBox>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left" width="300">
                                                                                    <asp:RequiredFieldValidator ID="valPostCode" runat="server" ErrorMessage="Post Code Required"
                                                                                        ControlToValidate="txtPostCode" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" valign="top" align="right">
                                                                                    <span style="color: #ff0000"></span>
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="lblResidence" runat="server" Text="Residence"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtResidence" runat="server" CssClass="textbox" MaxLength="50"></asp:TextBox></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="300">
                                                                                    <asp:RequiredFieldValidator ID="valResidence" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                                        ControlToValidate="txtResidence" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" valign="top" align="right">
                                                                                    <span style="color: #ff0000"></span>
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="lblCountry" runat="server" Text="Country"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:DropDownList ID="ddlCountry" runat="server" Width="205px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left" width="300">
                                                                                    <asp:RequiredFieldValidator ID="valCountry" runat="server" ErrorMessage="Select a Country"
                                                                                        ControlToValidate="ddlCountry" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" align="right">
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="lblTelephone" runat="server" Text="Telephone"></asp:Label>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtTelephone" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="300">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" align="right">
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="lblFax" runat="server" Text="Fax"></asp:Label>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtFax" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left" width="300">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" align="right">
                                                                                    <span style="color: #ff0000"></span>
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="lblEMail" runat="server" Text="E-mail"></asp:Label>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="50"></asp:TextBox>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left" width="300">
                                                                                    <asp:RequiredFieldValidator ID="valEmail" runat="server" ErrorMessage="E-mail address Required"
                                                                                        ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                                                            ID="valInvalidEmail" runat="server" ErrorMessage="Invalid E-mail address" ControlToValidate="txtEmail"
                                                                                            Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" valign="top" align="right">
                                                                                    <span style="color: #ff0000"></span>
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox" TextMode="Password"
                                                                                        MaxLength="50"></asp:TextBox>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left" width="300">
                                                                                    <asp:RequiredFieldValidator ID="valPassword" runat="server" ErrorMessage="Password Required"
                                                                                        ControlToValidate="txtPassword" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="height: 26px" valign="top">
                                                                                </td>
                                                                                <td align="left" style="width: 130px; height: 26px" valign="top">
                                                                                    <asp:Label ID="lblRePassword" runat="server" Text="Re-Type Password"></asp:Label></td>
                                                                                <td align="left" style="height: 26px" valign="top" width="100">
                                                                                    <asp:TextBox ID="txtRePassword" runat="server" CssClass="textbox" MaxLength="50"
                                                                                        TextMode="Password"></asp:TextBox></td>
                                                                                <td align="left" style="height: 26px" valign="top" width="300">
                                                                                    <asp:RequiredFieldValidator ID="valRePassword" runat="server" ControlToValidate="txtRePassword"
                                                                                        Display="Dynamic" ErrorMessage="Re-Type Password Required" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" align="right">
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="lblWebsite" runat="server" Text="Web- site"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtWebsite" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox></td>
                                                                                <td style="height: 26px" width="300">
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </fieldset>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="height: 19px; background-color: white;">
                                                                <asp:CheckBox ID="chkDelivery" runat="server" Text="Different Delevery Address" />&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="background-color: white; height: 329px;">
                                                                <fieldset>
                                                                    <legend>
                                                                        <asp:Label ID="lblDelivery" runat="server" Text="Delivery Address" Font-Bold="True"
                                                                            ForeColor="Black"></asp:Label></legend>
                                                                    <table style="height: 110px" id="table4" cellspacing="0" cellpadding="2" align="left"
                                                                        border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" align="right">
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="Label1" runat="server" Text="Initial Name"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <select id="ddlDInitialName" name="ddlDInitialName" runat="server">
                                                                                        <option value="Mr." selected>Mr.</option>
                                                                                        <option value="Mrs.">Mrs.</option>
                                                                                        <option value="Dhr.">Dhr.</option>
                                                                                        <option value="Mevr.">Mevr.</option>
                                                                                    </select>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" align="right">
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="Label2" runat="server" Text="First Name"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtDFName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left">
                                                                                    &nbsp;&nbsp;&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" align="right">
                                                                                    <span style="color: #ff0000"></span>
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="Label3" runat="server" Text="Middle Name"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtDMName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox></td>
                                                                                <td style="height: 26px" valign="top" align="left">
                                                                                    &nbsp;&nbsp;&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" align="right">
                                                                                    <span style="color: #ff0000"></span>
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="Label4" runat="server" Text="LastName"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtDLName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left">
                                                                                    &nbsp;&nbsp;&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" valign="top" align="right">
                                                                                    <span style="color: #ff0000"></span>
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="Label5" runat="server" Text="House #"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtDHousenr" runat="server" CssClass="textbox" MaxLength="20"></asp:TextBox>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" valign="top" align="right">
                                                                                    <span style="color: #ff0000"></span>
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="Label6" runat="server" Text="Address"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtDAddress" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" valign="top" align="right">
                                                                                    <span style="color: #ff0000"></span>
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="Label7" runat="server" Text="PostCode"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtDPostCode" runat="server" CssClass="textbox" MaxLength="10"></asp:TextBox>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" valign="top" align="right">
                                                                                    <span style="color: #ff0000"></span>
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="Label8" runat="server" Text="Residence"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:TextBox ID="txtDResidence" runat="server" CssClass="textbox" MaxLength="50"></asp:TextBox></td>
                                                                                <td style="height: 26px" valign="top" align="left">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 26px" valign="top" align="right">
                                                                                    <span style="color: #ff0000"></span>
                                                                                </td>
                                                                                <td style="height: 26px; width: 130px;" valign="top" align="left">
                                                                                    <asp:Label ID="Label9" runat="server" Text="Country"></asp:Label></td>
                                                                                <td style="height: 26px" valign="top" align="left" width="100">
                                                                                    <asp:DropDownList ID="ddlDCountry" runat="server" CssClass="textbox" Width="205px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td style="height: 26px" valign="top" align="left">
                                                                                    &nbsp;&nbsp;&nbsp;</td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </fieldset>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="chkAgree" runat="server" />
                                                                                </td>
                                                                                <td style="width:67px">
                                                                                </td>
                                                                                <td valign="middle" class="oneButtonBg" style="height:29px; width:20px">
                                                                                    <asp:ImageButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"></asp:ImageButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <%--<tr>
                                                                    <td style="background-color: white;">
                                                                        <table style="height: 110px" id="table2" cellspacing="0" cellpadding="2" align="center"
                                                                            border="0" width="100%">
                                                                            <tbody>
                                                                                <tr>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" colspan="3" valign="top">
                                                                                        <table class="oneButtonBg" cellspacing="0" cellpadding="3" border="0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>--%>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="height: 50px">
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
