<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="delivery, App_Web_delivery.aspx.cdcab7d2" title="Delivery Page" theme="ThemeOne" %>

<%@ Register TagPrefix="uc1" TagName="Subscribe" Src="~/subscribe_widget.ascx" %>
<%@ Register TagPrefix="uc2" TagName="ContactWidget" Src="~/contact_detail_widget.ascx" %>
<%@ MasterType VirtualPath="~/MainLayOut.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="Server">

 

 <div class="content-header">
            <div class="content-header-container">
               <asp:Label id="lblHeader" runat="server" Text='1) Basket'>
                   </asp:Label>
            </div>
        </div>
        <div class="order-step-header-container">
            <div class="order-step-right-panel">
                <asp:Label id="lblBasket" runat="server" Text='1) Basket'>
                   </asp:Label>
            </div>
            <div class="order-step-right-panel">
               <asp:Label id="lblLogReg" runat="server" Text='2) Login /Register'>
                   </asp:Label>
            </div>
            <div class="order-step-left-panel">
                <asp:Label id="lblDelAddress" runat="server" Text='3) Deliver Addresses' ForeColor="Black">
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

        

                                                    <table class="table-content"  border="0" cellspacing="0" cellpadding="2" style="height:280px" align="left">
                                                        <tr>
                                                            <td colspan="3" align="left" valign="top">
                                                               
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                               
                                                                <td style="height: 26px;" valign="top" align="right">
                                                                    <asp:Label ID="lblCompany" runat="server" Text="Company Name"></asp:Label></td>
                                                                <td style="height: 26px; " valign="top" align="left" >
                                                                    <asp:TextBox ID="txtCompany" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                                </td>
                                                                 <td style="height: 26px; " valign="top" align="left">
                                                                </td>
                                                            </tr>
                                                        <tr>
                                                            <td align="right" valign="middle" style=" width:30%" >
                                                                <asp:Label ID="lblInitialName" runat="server" Text="Initial Name"></asp:Label></td>
                                                            <td colspan="2" align="left" valign="top">
                                                                <asp:RadioButtonList runat="server" ID="rdoInitialName" RepeatLayout="Table" RepeatDirection="Horizontal" >
                                                                   <asp:ListItem Text="Mr." Value="Mr."></asp:ListItem>
                                                                    <asp:ListItem Text="Mrs." Value="Mrs."></asp:ListItem>
                                                                     <asp:ListItem Text="Dhr." Value="Dhr."></asp:ListItem>
                                                                      <asp:ListItem Text="Mevr." Value="Mevr."></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                <asp:Label ID="lblDFName" runat="server" Text="First Name"></asp:Label></td>
                                                            <td align="left" valign="top">
                                                                <asp:TextBox ID="txtDFName" runat="server" CssClass="textbox"></asp:TextBox>
                                                                <span style='font-size:22px; vertical-align:top;'>*</span> 
                                                                </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="valFName" runat="server" ControlToValidate="txtDFName"
                                                                    ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top" style="height: 26px;">
                                                                <asp:Label ID="lblDMName" runat="server" Text="Middle Name"></asp:Label></td>
                                                            <td align="left" valign="top">
                                                                <asp:TextBox ID="txtDMName" runat="server" CssClass="textbox"></asp:TextBox></td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                <asp:Label ID="lblDLName" runat="server" Text="Last Name"></asp:Label></td>
                                                            <td align="left" valign="top">
                                                                <asp:TextBox ID="txtDLName" runat="server" CssClass="textbox"></asp:TextBox></td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="valLName" runat="server" ControlToValidate="txtDLName"
                                                                    ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                <asp:Label ID="lblDHouseNum" runat="server" Text="House#">
                                                                </asp:Label>&nbsp;</td>
                                                            <td align="left" valign="top">
                                                                <asp:TextBox ID="txtDHouseNum" runat="server" CssClass="textbox"></asp:TextBox></td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="valHouseNum" runat="server" ControlToValidate="txtDHouseNum"
                                                                    ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                <asp:Label ID="lblDAddress" runat="server" Text="Address">
                                                                </asp:Label>&nbsp;</td>
                                                            <td align="left" valign="top">
                                                                <asp:TextBox ID="txtDAddress" runat="server" CssClass="textbox"></asp:TextBox></td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="valAddress" runat="server" ControlToValidate="txtDAddress"
                                                                    ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                <asp:Label ID="lblDResidence" runat="server" Text="Residence">
                                                                </asp:Label>&nbsp;</td>
                                                            <td align="left" valign="top">
                                                                <asp:TextBox ID="txtDResidence" runat="server" CssClass="textbox"></asp:TextBox>
                                                                <span style='font-size:22px; vertical-align:top;'>*</span> 
                                                            </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="valResidence" runat="server" ControlToValidate="txtDResidence"
                                                                    ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                <asp:Label ID="lblDPostCode" runat="server" Text="Post Code">
                                                                </asp:Label>&nbsp;</td>
                                                            <td align="left" valign="top">
                                                                <asp:TextBox ID="txtDPostCode" runat="server" CssClass="textbox" MaxLength="10"></asp:TextBox></td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="valPostCode" runat="server" ControlToValidate="txtDPostCode"
                                                                    ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                <asp:Label ID="lblDCountry" runat="server" Text="Country">
                                                                </asp:Label></td>
                                                            <td colspan="2" align="left" valign="top">
                                                                <asp:DropDownList ID="ddlCountry" runat="server" Width="147px">
                                                                </asp:DropDownList>
                                                                <span style='font-size:22px; vertical-align:top;'>*</span> 
                                                            </td>
                                                        </tr>
                                                    </table>
                                               
  <div class="footer-container" style="clear:both">
  <div style="float: right; margin-right: 10px;margin-top:5px;">
                                                                <asp:Button ID="btnNext" runat="server" CssClass="button" Text="next"  OnClick="btnNext_Click" />
                                                                </div>
                                                                </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarPlace" runat="Server">
<div class="content-sidebar-header">
        <div class="sidebar-container">
            <label id="Label1"  runat="server">
                 <%= (string)base.GetGlobalResourceObject("string", "myAccount")%></label>
        </div>
    </div>
    
      <div class="sidebar-content-body" style="height: auto;">
      <div style="display:block">
         <a href="register.aspx" style="color: #2F2F2F  !important"> &raquo; <%= (string)base.GetGlobalResourceObject("string", "Profile")%> </a>
       </div>
       <div style="display:block">
          <a href="shoppingcart.aspx" style="color: #2F2F2F  !important"> &raquo; <%= (string)base.GetGlobalResourceObject("string", "basket")%></a>
     </div>
    </div>
    
    
    <uc2:ContactWidget runat=server />
    <uc1:Subscribe runat=server />

</asp:Content>

