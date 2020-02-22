<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="Login, App_Web_login.aspx.cdcab7d2" title="Log In Page" theme="ThemeOne" %>

<%@ MasterType VirtualPath="~/MainLayOut.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" align="center" class="contentArea">
        <tr>
            
            <td runat="server" id="header" style="height: 20px; background-repeat: no-repeat;">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="" valign="top">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 95%" align="left" style="height:20px">&nbsp;
                            <%--<asp:Label ID="lblCurrentPage" runat="server" Font-Bold='true' Text="Current page"></asp:Label>&nbsp;&nbsp;<b>:</b>&nbsp;&nbsp;
                            <asp:Label ID="lblPageRoot" runat="server" ForeColor="AppWorkspace" Text=""></asp:Label>
                            <asp:Label ID="lblActivePage" runat="server" ForeColor="#3300FF" Text=" "></asp:Label>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width:5px">&nbsp;
                        </td>
                        <td align="center" style="width: 906px;background-color: white;" valign="middle">
                            <table border="0">
                                <tr>
                                    <td colspan="3" style="height:75px;">
                                    &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:middle" align="left">
                                        <asp:Label ID="lblUserName" runat="server"></asp:Label> :
                                    </td>
                                    <td  style="vertical-align:middle; padding-left:10px;" align="left">
                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox"></asp:TextBox>
                                    </td>
                                    <td style="vertical-align:middle; width:60px;" align="left">
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtUserName"
                                            Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:middle" align="left">
                                         <asp:Label ID="lblPassword" runat="server"></asp:Label> :
                                    </td>
                                    <td style="vertical-align:middle;padding-left:10px;" align="left">
                                         <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textbox"></asp:TextBox>
                                    </td>
                                    <td style="vertical-align:middle" align="left">
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="2" style="padding-left:10px;height: 18px" valign="middle" align="left">
                                        <asp:Label ID="labelMessage" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:middle;" colspan="2" align="right">
                                        <table border="0" style="background-image:url('graphics/bgTwoButtons.gif');background-repeat:no-repeat">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ImageAlign="AbsMiddle" ID="btnLogin" runat="server" ImageUrl="graphics/btnLogin_en.png"
                                                        OnClick="btnLogin_Click" />
                                                </td>
                                                <td>
                                                     <asp:ImageButton ImageAlign="AbsMiddle" ID="btnClear" runat="server" CausesValidation="False"
                                                        ImageUrl="graphics/btnClear_en.png" OnClick="btnClear_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td></td>                        
                                </tr>
                                <tr>
                                    <td colspan="3" style="height:75px;">
                                    &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width:5px">&nbsp;
                        </td>
                    </tr>
                </table>
            </td>            
        </tr>
        <tr>
            <td align="center" style="height: 20px; width: 60%; background-color: #F0F0F0;" valign="top">
            </td>
        </tr>
    </table>
</asp:Content>
