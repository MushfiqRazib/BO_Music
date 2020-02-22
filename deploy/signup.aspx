<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="signup, App_Web_signup.aspx.cdcab7d2" title="Signup" validaterequest="false" theme="ThemeOne" %>
<%@ MasterType  virtualPath="~/MainLayOut.master"%>
<%@ Register TagPrefix="uc1" TagName="Subscribe" Src="~/subscribe_widget.ascx" %>
<%@ Register TagPrefix="uc2" TagName="ContactWidget" Src="~/contact_detail_widget.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" Runat="Server">
   





        
         <div class="content-header">
            <div class="content-header-container">
               <asp:Label id="lblHeader" runat="server" Text='1) Basket'>
                   </asp:Label>
            </div>
        </div>
        <div class="order-step-header-container">
            <div class="order-step-right-panel" style="width:101px;">
                <asp:Label id="lblBasket" runat="server" Text='1) Basket'>
                   </asp:Label>
            </div>
            <div class="order-step-left-panel" style="width:140px;">
               <asp:Label id="lblLogReg" runat="server" Text='2) Login /Register' ForeColor="Black">
                   </asp:Label>
            </div>
            <div class="order-step-right-panel">
                <asp:Label id="lblDelAddress" runat="server" Text='3) Deliver Addresses'>
                   </asp:Label>
            </div>
            <div class="order-step-right-panel">
             <asp:Label id="lblPayment" runat="server" Text='4) Payment'>
                   </asp:Label>
                
            </div>
            <div class="order-step-right-panel">
            <asp:Label id="lblOrderComplete" runat="server" Text='5) Order Completed'>
                   </asp:Label>
              
            </div>
      </div>

    <table class="table-content"  border="0" height="100%" width="100%" style="background-color: white;">
                                        
            <tr valign="middle">
               
                <td align="left" colspan="1" style="height: 17px" valign="bottom">
                    <asp:Label ID="lblCustomerAlready" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td colspan="2"> <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label></td>
            </tr>
          
            <tr valign="middle">
               
                <td align="right" valign="middle" style="width:20%" >
                    <asp:Label ID="lblUserName" runat="server"></asp:Label>:</td>
                <td align="left" style="width: 20%" valign="bottom">
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox" MaxLength="50"></asp:TextBox></td>
               
                <td align="left" valign="middle" style=" width:60%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                        Display="Dynamic" ValidationGroup="loginValidation"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtUserName" ValidationGroup="loginValidation"
                        Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr valign="middle">
                
                <td align="right" valign="middle">
                    <asp:Label ID="lblPassword" runat="server"></asp:Label>:</td>
                <td align="left" style="width: 20%" valign="baseline">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textbox" MaxLength="50"></asp:TextBox></td>
               
                <td align="left" valign="middle" width="35%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                        Display="Dynamic" ValidationGroup="loginValidation"></asp:RequiredFieldValidator></td>
            </tr>
            <tr valign="middle">
          
               
                <td>
                     &nbsp;</td>
                <td>
                    
                        
                           
                            <asp:Button  CssClass="button"   ID="btnLogin" runat="server" OnClick="btnLogin_Click" CausesValidation="true" ValidationGroup="loginValidation"  />
                 
                        
                   
                      
                </td>
            </tr>
          
          
            <tr valign="middle">
                <td align="left" colspan="4" style="height: 17px" valign="middle">
                    <hr />
                </td>
            </tr>
</table>
   
   <!-- Register Info -->
  <table class="table-content" border="0" height="100%" width="100%" style="background-color: white;">
    <tr>
        <td style="background-color: white;">
           
               <table class="table-content" border="0" height="100%" width="100%" style="background-color: white;">
                    <tbody>
                        <tr>
                            <td colspan="4">
                               <asp:Label ID="lblRegistrationHeader" runat="server" Text="My account details" Font-Size="12px" Font-Bold="true"></asp:Label>  </td>
                        </tr>
                          <tr>
                           
                            <td style="height: 16px; width: 20%;" valign="bottom" align="right">
                                
                            </td>
                            <td style="height: 16px; width:20%" valign="bottom" align="left" >
                              
                               <asp:Label ID="Label11" runat="server" Font-Italic="true" Text="Fields with 
                                <span style='font-size:20px; vertical-align:top;'>*</span> are required"></asp:Label>
                            </td>
                             <td style="height: 16px; width:60%" valign="bottom" align="left">
                            </td>
                        </tr>
                        
                          <tr>
                           
                            <td style="height: 26px; width: 20%;" valign="top" align="right">
                                <asp:Label ID="lblCompany" runat="server" Text="Company Name"></asp:Label></td>
                            <td style="height: 26px; width:30%" valign="top" align="left" >
                                <asp:TextBox ID="txtCompany" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                            </td>
                             <td style="height: 26px; width:60%" valign="top" align="left">
                            </td>
                        </tr>
                        
                        <tr>
                            
                             <td align="right" valign="middle" style="width:20%" >
                                <asp:Label ID="lblInitialName" runat="server" Text="Initial Name"></asp:Label></td>
                            <td style="height: 26px" valign="top" align="left" width="20%">
                                <!--  string[] initialName = {"Mr.", "Mrs.", "Dhr.", "Mevr."," "}; -->
                                <asp:RadioButtonList runat="server" ID="rdoInitialName" RepeatLayout="Table" RepeatDirection="Horizontal" >
                                   <asp:ListItem Text="Mr." Value="Mr."></asp:ListItem>
                                    <asp:ListItem Text="Mrs." Value="Mrs."></asp:ListItem>
                                     <asp:ListItem Text="Dhr." Value="Dhr."></asp:ListItem>
                                      <asp:ListItem Text="Mevr." Value="Mevr."></asp:ListItem>
                                </asp:RadioButtonList>
                               </td>
                            <td style="height: 26px; width:60%" valign="top" align="left">
                            </td>
                        </tr>
                      
                        <tr>
                           
                            <td style="height: 26px; width: 20%;" valign="top" align="right">
                                <asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label></td>
                            <td style="height: 26px" valign="middle" align="left" width="20%">
                                <asp:TextBox ID="txtFName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                <span style='font-size:22px; vertical-align:top;'>*</span> 
                            </td>
                            <td style="height: 26px" valign="top" align="left" width="60%">
                                <asp:RequiredFieldValidator ID="valFName" runat="server" ErrorMessage="First Name Required"
                                    ControlToValidate="txtFName" ValidationGroup="accValidation" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                           
                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                <asp:Label ID="lblMName" runat="server" Text="Middle Name"></asp:Label></td>
                            <td style="height: 26px" valign="top" align="left" width="100">
                                <asp:TextBox ID="txtMName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox></td>
                            <td style="height: 26px" valign="top" align="left" width="300">
                            </td>
                        </tr>
                        <tr>
                           
                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                <asp:Label ID="lblLastName" runat="server" Text="LastName"></asp:Label></td>
                            <td style="height: 26px" valign="top" align="left" width="100">
                                <asp:TextBox ID="txtLName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                            </td>
                            <td style="height: 26px" valign="top" align="left" width="300">
                                <asp:RequiredFieldValidator ID="valLName" runat="server" ErrorMessage="Last Name Required"
                                    ControlToValidate="txtLName" ValidationGroup="accValidation" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                        </tr>
                        
                        <tr>
                           
                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                <asp:Label ID="lblHouse" runat="server" Text="House #"></asp:Label></td>
                            <td style="height: 26px" valign="top" align="left" width="100">
                                <asp:TextBox ID="txtHousenr" runat="server" CssClass="textbox" MaxLength="20"></asp:TextBox>
                            </td>
                            <td style="height: 26px" valign="top" align="left" width="300">
                                <asp:RequiredFieldValidator ID="valHousenr" runat="server" ErrorMessage="House Number Required"
                                    ControlToValidate="txtHousenr" ValidationGroup="accValidation" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            
                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                            <td style="height: 26px" valign="top" align="left" width="100">
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                            </td>
                            <td style="height: 26px" valign="top" align="left" width="300">
                                <asp:RequiredFieldValidator ID="valAddress" runat="server" ErrorMessage="Address Required"
                                    ControlToValidate="txtAddress" ValidationGroup="accValidation" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                           
                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                <asp:Label ID="lblPostCode" runat="server" Text="PostCode"></asp:Label></td>
                            <td style="height: 26px" valign="top" align="left" width="100">
                                <asp:TextBox ID="txtPostCode" runat="server" CssClass="textbox" MaxLength="10"></asp:TextBox>
                            </td>
                            <td style="height: 26px" valign="top" align="left" width="300">
                                <asp:RequiredFieldValidator ID="valPostCode" runat="server" ErrorMessage="Post Code Required"
                                    ControlToValidate="txtPostCode" ValidationGroup="accValidation" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            
                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                <asp:Label ID="lblResidence" runat="server" Text="Residence"></asp:Label></td>
                            <td style="height: 26px" valign="top" align="left" width="100">
                                <asp:TextBox ID="txtResidence" runat="server" CssClass="textbox" MaxLength="50"></asp:TextBox>
                                <span style='font-size:22px; vertical-align:top;'>*</span> 
                             </td>
                            <td style="height: 26px" valign="top" align="left" width="300">
                                <asp:RequiredFieldValidator ID="valResidence" runat="server" ErrorMessage="RequiredFieldValidator"
                                    ControlToValidate="txtResidence" ValidationGroup="accValidation" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                           
                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                <asp:Label ID="lblCountry" runat="server" Text="Country"></asp:Label></td>
                            <td style="height: 26px" valign="top" align="left" width="100">
                                <asp:DropDownList ID="ddlCountry" runat="server" Width="147px">
                                </asp:DropDownList>
                                <span style='font-size:22px; vertical-align:top;'>*</span> 
                            </td>
                            <td style="height: 26px" valign="top" align="left" width="300">
                                <asp:RequiredFieldValidator ID="valCountry" runat="server" ErrorMessage="Select a Country"
                                    ControlToValidate="ddlCountry" ValidationGroup="accValidation" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                           
                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                <asp:Label ID="lblTelephone" runat="server" Text="Telephone"></asp:Label>
                            </td>
                            <td style="height: 26px" valign="top" align="left" width="100">
                                <asp:TextBox ID="txtTelephone" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox></td>
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
                                <span style='font-size:22px; vertical-align:top;'>*</span> 
                            </td>
                            <td style="height: 26px" valign="top" align="left" width="300">
                                <asp:RequiredFieldValidator ID="valEmail" runat="server" ErrorMessage="E-mail address Required"
                                    ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="accValidation" SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                        ID="valInvalidEmail" runat="server" ErrorMessage="Invalid E-mail address" ControlToValidate="txtEmail" ValidationGroup="accValidation"
                                        Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                       
                        <tr>
                           
                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                <asp:Label ID="lblWebsite" runat="server" Text="Web- site"></asp:Label></td>
                            <td style="height: 26px" valign="top" align="left" width="100">
                                <asp:TextBox ID="txtWebsite" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox></td>
                            <td style="height: 26px" width="300">
                            </td>
                        </tr>
                    </tbody>
                </table>
           
        </td>
    </tr>
    <tr>
       <td align="left" colspan="4" style="height: 17px" valign="middle">
                    <hr />
        </td>
    </tr>
    <tr>
            <td colspan="4">
               <asp:Label ID="Label1" runat="server" Text="Delivary address" Font-Size="12px" Font-Bold="true"></asp:Label>  </td>
        </tr>
    <tr>
        <td style="background-color: white;">
           
               
                <table style="height: 110px" id="table4" cellspacing="0" cellpadding="2" align="left"
                    border="0">
                    <tbody>
                        <tr>
                            <td  style="height: 26px; width: 18%;">
                                </td>
                                <td style="height: 26px" valign="top" align="left" width="80%" colspan="2">
                                <asp:CheckBox ID="chkDelivery" runat="server" Text="Different Delevery Address"   />
                                </td>
                        </tr>
                        <tr>
                        
                            <td style="height: 26px; width: 20%;" valign="top" align="right">
                                <asp:Label ID="Label3" runat="server" Text="First Name"></asp:Label></td>
                            <td style="height: 26px" valign="top" align="left" width="30%">
                                <asp:TextBox ID="txtDFName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                <span style='font-size:22px; vertical-align:top;'>*</span> 
                            </td>
                            <td style="height: 26px" valign="top" align="left">
                                <asp:RequiredFieldValidator ID="valFName0" runat="server" ErrorMessage="First Name Required"
                                    ControlToValidate="txtDFName" SetFocusOnError="True" ValidationGroup="delivaryValidation"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                           
                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                <asp:Label ID="Label4" runat="server" Text="Middle Name"></asp:Label></td>
                            <td style="height: 26px" valign="top" align="left" width="100">
                                <asp:TextBox ID="txtDMName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                </td>
                            <td style="height: 26px" valign="top" align="left">
                           
                                </td>
                        </tr>
                        <tr>
                          
                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                <asp:Label ID="Label5" runat="server" Text="LastName"></asp:Label></td>
                            <td style="height: 26px" valign="top" align="left" width="100">
                                <asp:TextBox ID="txtDLName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                <span style='font-size:22px; vertical-align:top;'>*</span> 
                            </td>
                            <td style="height: 26px" valign="top" align="left">
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Last Name Required"
                                    ControlToValidate="txtDLName" SetFocusOnError="True" ValidationGroup="delivaryValidation"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                          
                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                <asp:Label ID="Label6" runat="server" Text="House #"></asp:Label></td>
                            <td style="height: 26px" valign="top" align="left" width="100">
                                <asp:TextBox ID="txtDHousenr" runat="server" CssClass="textbox" MaxLength="20"></asp:TextBox>
                                <span style='font-size:22px; vertical-align:top;'>*</span> 
                            </td>
                            <td style="height: 26px" valign="top" align="left">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="House # Required"
                                    ControlToValidate="txtDHousenr" SetFocusOnError="True" ValidationGroup="delivaryValidation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                         
                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                <asp:Label ID="Label7" runat="server" Text="Address"></asp:Label></td>
                            <td style="height: 26px" valign="top" align="left" width="100">
                                <asp:TextBox ID="txtDAddress" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                <span style='font-size:22px; vertical-align:top;'>*</span> 
                            </td>
                            <td style="height: 26px" valign="top" align="left">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Address is Required"
                                    ControlToValidate="txtDAddress" SetFocusOnError="True" ValidationGroup="delivaryValidation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                        
                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                <asp:Label ID="Label8" runat="server" Text="PostCode"></asp:Label>
                                </td>
                            <td style="height: 26px" valign="top" align="left" width="100">
                                <asp:TextBox ID="txtDPostCode" runat="server" CssClass="textbox" MaxLength="10"></asp:TextBox>
                                <span style='font-size:22px; vertical-align:top;'>*</span> 
                            </td>
                            <td style="height: 26px" valign="top" align="left">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Post code is Required"
                                    ControlToValidate="txtDPostCode" SetFocusOnError="True" ValidationGroup="delivaryValidation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                          
                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                <asp:Label ID="Label9" runat="server" Text="Residence"></asp:Label></td>
                            <td style="height: 26px" valign="top" align="left" width="100">
                                <asp:TextBox ID="txtDResidence" runat="server" CssClass="textbox"  MaxLength="50"></asp:TextBox>
                                <span style='font-size:22px; vertical-align:top;'>*</span> 
                                </td>
                            <td style="height: 26px" valign="top" align="left">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Residance Required"
                                    ControlToValidate="txtDResidence" SetFocusOnError="True" ValidationGroup="delivaryValidation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                      
                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                <asp:Label ID="Label10" runat="server" Text="Country"></asp:Label></td>
                            <td style="height: 26px" valign="top" align="left" width="147px">
                                <asp:DropDownList ID="ddlDCountry" runat="server" CssClass="textbox" Width="147px">
                                </asp:DropDownList>
                                <span style='font-size:22px; vertical-align:top;'>*</span> 
                            </td>
                            <td style="height: 26px" valign="top" align="left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Select a country"
                                    ControlToValidate="ddlDCountry" SetFocusOnError="True" ValidationGroup="delivaryValidation"></asp:RequiredFieldValidator></td>
                        </tr>
                    </tbody>
                </table>
                </td>
     </tr>
   
   <tr>
       <td align="left" colspan="4" style="height: 17px" valign="middle">
                    <hr  />
        </td>
    </tr>
    <tr>
            <td colspan="4">
               <asp:Label ID="Label2" runat="server" Text="Password" Font-Size="12px" Font-Bold="true"></asp:Label>  </td>
        </tr>
   <tr>
        <td>
            <table>
                 <tr>
                           
                            <td style="height: 26px; width: 130px;" valign="top" align="right">
                                <asp:Label ID="lblPassword1" runat="server" Text="Password"></asp:Label></td>
                            <td style="height: 26px" valign="top" align="left" width="160px">
                                <asp:TextBox ID="txtRegPassword" runat="server" CssClass="textbox" TextMode="Password"
                                    MaxLength="50"></asp:TextBox>
                                    <span style='font-size:22px; vertical-align:top;'>*</span> 
                            </td>
                            <td style="height: 26px" valign="top" align="left" width="300">
                                <asp:RequiredFieldValidator ID="valPassword" runat="server" ErrorMessage="Password Required"
                                    ControlToValidate="txtRegPassword" ValidationGroup="accValidation" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                   
                                    </td>
                        </tr>
                        <tr>
                           
                            <td align="right" style="width: 130px; height: 26px" valign="top">
                                <asp:Label ID="lblRePassword" runat="server" Text="Re-Type Password"></asp:Label></td>
                            <td align="left" style="height: 26px" valign="top" width="160px">
                                <asp:TextBox ID="txtRePassword" runat="server" CssClass="textbox" MaxLength="50"
                                    TextMode="Password"></asp:TextBox>
                                     <span style='font-size:22px; vertical-align:top;'>*</span> 
                              </td>
                            <td align="left" style="height: 26px" valign="top" width="300">
                                <asp:RequiredFieldValidator ID="valRePassword" runat="server" ControlToValidate="txtRePassword"
                                    Display="Dynamic" ValidationGroup="accValidation" ErrorMessage="Re-Type Password Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                             </td>
                        </tr>
            </table>
        </td>
   </tr>
<tr>
    <td>
        <table width="100%">
            <tr>
                <td style="width:18%">
                   
                </td>
                <td style="width:82%">
                 <asp:CheckBox ID="chkAgree" runat="server" />
                </td>
                
            </tr>
            <tr>
                <td >
                    
                </td>
                <td valign="middle"  style="height:29px; width:20px">
                    <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="button" ValidationGroup="accValidation" OnClick="btnSubmit_Click"  />
                </td>
            </tr>
        </table>
    </td>
</tr>

            
    </table>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SidebarPlace" runat="Server">
<div class="content-sidebar-header">
        <div class="sidebar-container">
            <label  runat="server">
                 <%= (string)base.GetGlobalResourceObject("string", "myAccount")%></label>
        </div>
    </div>
    
      <div class="sidebar-content-body" style="height: auto;">
      <div style="display:block">
         <a href="register.aspx" style="color: #2F2F2F  !important"> &raquo; <%= (string)base.GetGlobalResourceObject("string", "Profile")%> </a>
       </div>
       <div style="display:block">
          <a href="delivery.aspx" style="color: #2F2F2F  !important"> &raquo; <%= (string)base.GetGlobalResourceObject("string", "stepDelivaery")%> </a>
     </div>
    </div>
    
    
    <uc2:ContactWidget runat=server />
    <uc1:Subscribe runat=server />

</asp:Content>

