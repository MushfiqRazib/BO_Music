<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="signup, Bo02" title="Signup" validaterequest="false" theme="ThemeOne" %>
<%@ MasterType  virtualPath="~/MainLayOut.master"%>
<%@ Register Assembly="HawarIT.WebControls" Namespace="HawarIT.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
<!--

function IMG1_onLoad() {

}

// -->
</script>

    <table cellpadding="0" cellspacing="0" width="882">
        
         <tr>
           <td colspan="2" id=header  runat=server style="height:20px; width: 834px;"></td>  
         </tr>
         <tr>
               <td colspan="2" style="width: 834px">
                 <table cellpadding="0" cellspacing="0" class="contentArea">
                    <tr>
                       <td valign="top" align="left" class="signupTDStyle1">
                       <%--<asp:Label ID="lblCurrentPage" runat="server"  Font-Bold='true' Text="Current page">  </asp:Label>&nbsp;&nbsp;<b>:</b>&nbsp;&nbsp;<asp:Label ID="lblPageRoot" runat="server"   ForeColor="AppWorkspace"  Text=""></asp:Label>
                       <asp:Label ID="lblActivePage" runat="server"   ForeColor="#3300FF"  Text=" "></asp:Label>&nbsp;&nbsp;--%>
                       </td>
                       <td valign="top" align="right" style="padding-right:3px; height: 80px;">
                            <table>
                            
                                
                                <tr style="height:58px;">
                                   
                                    <td align="right" ><img src="graphics/step2.gif" id="IMG1" onLoad="return IMG1_onLoad()" /></td>
                                </tr>
                                 
                                
                            </table>
                       </td>       
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 217px;padding-left:5px;padding-right:5px">
                           <table width="100%" cellpadding=0 cellspacing=0>
                              
                              <tr  style="background-color:#FFFFFF;">
                                
                                <td colspan="2" align="right" valign="bottom" style="height: 30px" >
                                    <table align="center" border="0" height="100%" width="100%">
                                        <tr valign="middle">
                                            <td style="height: 17px" width="20">
                                            </td>
                                            <td align="left" style="height: 17px" valign="bottom" width="10%">
                                            </td>
                                            <td align="left" style="width: 20%" valign="bottom">
                                            </td>
                                            <td align="left" style="height: 17px; width: 1%;" valign="bottom">
                                            </td>
                                            <td align="left" style="height: 17px" valign="bottom" width="35%">
                                            </td>
 
                                        <tr height=10px></tr>
                                        <tr valign="middle">
                                            <td style="height: 17px" width="20">
                                            </td>
                                            <td align="left" colspan="4" style="height: 17px" valign="bottom">
                                                <asp:Label ID="lblCustomerAlready" runat="server" Font-Bold="True"></asp:Label></td>
                                        </tr>
                                        <tr valign="middle">
                                            <td style="height: 17px" width="20">
                                            </td>
                                            <td align="left" colspan="4" style="height: 17px" valign="bottom">
                                                <asp:Label ID="lblFillUserName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr height=10px></tr>
                                        <tr valign="middle">
                                            <td width="20">
                                            </td>
                                            <td align="left" valign="middle" width="10%">
                                                <asp:Label ID="lblUserName" runat="server"></asp:Label>:</td>
                                            <td align="left" style="width: 20%" valign="bottom">
                                                <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox" MaxLength="50"></asp:TextBox></td>
                                            <td align="left" valign="middle" style="width: 1%">
                                            </td>
                                            <td align="left" valign="middle" width="35%">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtUserName"
                                                    Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                        </tr>
                                        <tr valign="middle">
                                            <td width="20">
                                            </td>
                                            <td align="left" valign="middle" width="10%">
                                                <asp:Label ID="lblPassword" runat="server"></asp:Label>:</td>
                                            <td align="left" style="width: 20%" valign="baseline">
                                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textbox" MaxLength="50"></asp:TextBox></td>
                                            <td align="left" valign="middle" style="width: 1%">
                                            </td>
                                            <td align="left" valign="middle" width="35%">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                                    Display="Dynamic"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr valign="middle">
                                            <td style="height: 18px" width="20">
                                            </td>
                                            <td style="height: 18px" width="10%" align="left">
                                            </td>
                                            <td align="left" colspan="3" style="height: 18px" valign="middle">
                                                &nbsp;<asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label></td>
                                        </tr>
                                        <tr valign="middle">
                                            <td width="20">
                                            </td>
                                            <td width="10%" align="left">
                                            </td>
                                            <td align="right" valign="middle">
                                               
                                                <table border=0 style="background-image:url('graphics/bgTwoButtons.gif');background-repeat:no-repeat">
                                                 <tr>                                                 
                                                   
                                                    <td>
                                                        <asp:ImageButton  ImageAlign="AbsMiddle"  ID="btnLogin" runat="server" ImageUrl="graphics/btnLogin_en.png" OnClick="btnLogin_Click" />
                                                    </td>
                                                    
                                                    <td>
                                                        <asp:ImageButton ImageAlign="AbsMiddle"  ID="btnClear" runat="server" CausesValidation="False" ImageUrl="graphics/btnClear_en.png" OnClick="btnClear_Click" />
                                                    </td>
                                                    
                                                </tr>
                                                </table>                       
                                                   
                                            
                                            </td>
                                            <td align="left" valign="middle" style="width: 1%">
                                            </td>
                                            <td align="left" valign="middle" width="35%">
                                            </td>
                                        </tr>
                                        <tr valign="middle">
                                            <td style="height: 17px" width="20">
                                            </td>
                                            <td align="left" style="height: 17px" width="10%">
                                            </td>
                                            <td align="center" style="width: 20%; height: 17px" valign="middle">
                                            </td>
                                            <td align="left" style="width: 1%; height: 17px" valign="middle">
                                            </td>
                                            <td align="left" style="height: 17px" valign="middle" width="35%">
                                            </td>
                                        </tr>
                                        <tr valign="middle">
                                            <td align="left" colspan="5" style="height: 17px" valign="middle">
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr valign="middle">
                                            <td style="height: 17px" width="20">
                                            </td>
                                            <td align="left" colspan="2" style="height: 17px" valign="middle">
                                                <asp:Label ID="lblCustomerNotYet" runat="server" Font-Bold="True"></asp:Label></td>
                                            <td align="left" style="width: 1%; height: 17px" valign="middle">
                                            </td>
                                            <td align="left" style="height: 17px" valign="middle" width="35%">
                                            </td>
                                        </tr>
                                        <tr valign="middle">
                                            <td style="height: 17px" width="20">
                                            </td>
                                            <td align="left" colspan="4" style="height: 17px" valign="middle">
                                                <asp:Label ID="lblClickHere" runat="server" Font-Bold="False"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                              </tr>  
                              <tr style="height:50px">
                              
                              </tr>
                         
                            </table>
                        </td>
                    </tr>  
                       
                 </table>
               
               
               </td>
       </tr>
     
    
   </table>
</asp:Content>

