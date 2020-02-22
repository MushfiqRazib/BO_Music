<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="contact, App_Web_contact.aspx.cdcab7d2" title="Contact" validaterequest="false" theme="ThemeOne" %>

<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MainLayOut.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="Server">
    <link href="include/kallol.css" rel="stylesheet" type="text/css" />
   
<script type="text/javascript">



    $(document).ready(function() {
    
     $('.menu-ul li a[href="contact.aspx"]').addClass('active');
    });

    </script>
 <div class="content-header">
        <div class="content-header-container">
            <label id="lblNewsMore" runat="server">
                Contact</label>
        </div>
    </div>
   
    <table class="table-content"  border="0" cellspacing="0" cellpadding="2" align="left">
                                                        <tr>
                                                            <td colspan="3" align="left" valign="top">
                                                               
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                               
                                                                <td style="height: 20px; width:20%" valign="top" align="right">
                                                                    <asp:Label ID="lblCompany" runat="server" Text="Company Name"></asp:Label></td>
                                                                <td style="height: 20px; " valign="top" align="left" >
                                                                    <asp:TextBox ID="txtCompany" runat="server" CssClass="textbox" MaxLength="100" 
                                                                        Width="145px"></asp:TextBox>
                                                                </td>
                                                                 <td style="height: 20px; " valign="top" align="left">
                                                                </td>
                                                            </tr>
                                                       
                                                        <tr>
                                                            <td align="right" valign="top" style="height: 20px;">
                                                                <asp:Label ID="lblDFName" runat="server" Text="First Name"></asp:Label></td>
                                                            <td align="left" valign="top" style="height: 20px;">
                                                                <asp:TextBox ID="txtDFName" runat="server" CssClass="textbox" Width="147px"></asp:TextBox>
                                                                <span style='font-size:17px; vertical-align:top;' >*</span> 
                                                                </td>
                                                            <td style="height: 20px;">
                                                                <asp:RequiredFieldValidator ID="valFName" runat="server" ControlToValidate="txtDFName"
                                                                    ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top" style="height: 20px;">
                                                                <asp:Label ID="lblDMName" runat="server" Text="Middle Name"></asp:Label></td>
                                                            <td align="left" valign="top" style="height: 20px;">
                                                                <asp:TextBox ID="txtDMName" runat="server" CssClass="textbox" Width="87px"></asp:TextBox></td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top" style="height: 20px;">
                                                                <asp:Label ID="lblDLName" runat="server" Text="Last Name"></asp:Label></td>
                                                            <td align="left" valign="top" style="height: 20px;">
                                                                <asp:TextBox ID="txtDLName" runat="server" CssClass="textbox" Width="146px"></asp:TextBox>
                                                                
                                                                 <span style='font-size:17px; vertical-align:top;' >*</span> 
                                                                </td>
                                                          
                                                          
                                                            <td style="height: 20px;">
                                                                <asp:RequiredFieldValidator ID="valLName" runat="server" ControlToValidate="txtDLName"
                                                                    ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                       <tr>
                                                            <td align="right" valign="top" style="height: 20px;">
                                                                <asp:Label ID="lblDCountry" runat="server" Text="Country">
                                                                </asp:Label></td>
                                                            <td colspan="2" align="left" valign="top" style="height: 20px;">
                                                                <asp:DropDownList ID="ddlCountry" runat="server" Width="147px">
                                                                </asp:DropDownList>
                                                                <span style='font-size:17px; vertical-align:top;' >*</span> 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top" style="height: 20px;">
                                                                <asp:Label ID="Label2" runat="server" Text="Phone"></asp:Label></td>
                                                            <td align="left" valign="top" style="height: 20px;">
                                                                <asp:TextBox ID="txtPhone" runat="server" CssClass="textbox" Width="146px"></asp:TextBox></td>
                                                            <td style="height: 20px;">
                                                            </td>
                                                        </tr>
                                                            <tr>
                           
                                                        <td  valign="top" align="right" style="height: 20px;">
                                                            <asp:Label ID="lblEMail" runat="server" Text="E-mail address"></asp:Label>
                                                        </td>
                                                        <td  valign="top" align="left" style="height: 20px;" >
                                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="50" 
                                                                Width="146px"></asp:TextBox>
                                                             <span style='font-size:17px; font-weight:bold; vertical-align:top;' >*</span> 
                                                        </td>
                                                        <td  valign="top" align="left" style="height: 20px;" >
                                                            <asp:RequiredFieldValidator ID="valEmail" runat="server" ErrorMessage="E-mail address Required"
                                                                ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                                    ID="valInvalidEmail" runat="server" ErrorMessage="Invalid E-mail address" ControlToValidate="txtEmail"
                                                                    Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                        </td>
                                                         </tr>
                                                         
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                <asp:Label ID="lblMsg" runat="server" Text="Message">
                                                                </asp:Label>&nbsp;</td>
                                                            <td align="left" valign="top" colspan="2">
                                                                <asp:TextBox ID="txtMessage" runat="server"  CssClass="textbox" 
                                                                    MaxLength="300" Height="86px" Width="414px" TextMode="MultiLine" ></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="valPostCode" runat="server" ControlToValidate="txtMessage"
                                                                    ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="button" OnClick="btnSend_Click" />
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                       
                                                    </table>
                                                    
                                  <div  style="height:10px; clear:both;"></div>                  
                              
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SidebarPlace" runat="Server">

    <div class="content-sidebar-header">
        <div class="sidebar-container">
            <label id="lblContact" runat="server">
                Contact Details</label>
        </div>
    </div>
    <div class="sidebar-content-body" style="height: 130px;">
        <table border="0" width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <strong>Boeijenga Music</strong>
                </td>
                <td height="25" style="width: 90px">
                    <center>
                        <asp:Button ID="btnContact" Text="Contact" runat="server" CausesValidation="False"
                       CssClass="button" onclick="btnContact_Click"      /></center>
                </td>
            </tr>
            <tr>
                <td>
                    Hoofdweg 156
                </td>
            </tr>
            <tr>
                <td>
                    9341 BM Veenhuizen
                </td>
            </tr>
            <tr>
                <td>
                    The Netherlands
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    Phone: +31 (0) 592-304142
                    <asp:Label ID="lblAvailable" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Fax: +31 (0) 592-304143
                </td>
            </tr>
            <tr>
                <td style="padding-top: 5px">
                   Email: <a class="pinklink" href="mailto:info@boeigengamusic.com">info@boeigengamusic.com</a>
                </td>
            </tr>
        </table>
    </div>
    
    
    <div class="content-sidebar-header">
        <div class="sidebar-container">
            <label id="Label1" runat="server">
                Contact Details</label>
        </div>
    </div>
   <div class="sidebar-content-body" style="height: 130px;">
        <ul class="contact-ul">
            <li><a href="javascript:void(0)" id="locationlink" runat="server" rel="s" class="category-link">» Locatie  </a> </li>
            <li><a href="route.aspx" id="routelink" runat="server" rel="b"  class="category-link">» Route  </a>
            </li>
            <li><a href="about.aspx" id="aboutlink" runat="server" rel="c"  class="category-link">» About  </a>
            </li>
        </ul>
    </div>
    </asp:Content>