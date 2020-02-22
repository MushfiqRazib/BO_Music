<%@ page language="C#" autoeventwireup="true" inherits="Admin_Login, Bo02" title="Login" theme="ThemeOne" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
  	<LINK href="../include/style.css" type="text/css" rel="stylesheet">
    <LINK href="media/style.css" type="text/css" rel="stylesheet">
    <LINK href="media/master.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" height="450px" width="882px" border="0" align="center"
            class="contentArea">
            <tr>
                <td align="left" valign="top">
                    <table style="width: 100%" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td align="left" class="pageHeader">
                                <h4 class="contentHeader">
                                    Login Admin</h4>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 60%; background-color: white;" valign="middle">
                    <table width="650px" height="100px" border="0" align="center" style="background-color: white;">
                        <tr valign="middle">
                            <td width="20%">
                            </td>
                            <td align="left" style="height: 17px;" valign="bottom" width="18%">
                            </td>
                            <td align="left" valign="bottom" width="25%">
                                &nbsp;
                            </td>
                            <td align="left" style="height: 17px" valign="bottom" width="1%">
                            </td>
                            <td align="left" style="width: 40%; height: 17px;" valign="bottom">
                            </td>
                        </tr>
                        <tr valign="middle">
                            <td width="20%">
                            </td>
                            <td align="left" valign="middle" width="18%">
                                <asp:Label ID="lblUserName" runat="server" Text="Username"></asp:Label>:
                            </td>
                            <td align="left" valign="bottom" width="25%">
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" width="1%">
                            </td>
                            <td style="width: 40%" align="left" valign="middle">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtUserName"
                                    Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr valign="middle">
                            <td width="20%">
                            </td>
                            <td align="left" valign="middle" width="18%">
                                <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>:
                            </td>
                            <td align="left" valign="baseline" width="25%">
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" width="1%">
                            </td>
                            <td style="width: 40%" align="left" valign="middle">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr valign="middle">
                            <td style="height: 18px;" width="20%">
                            </td>
                            <td style="height: 18px;" align="left" width="18%">
                            </td>
                            <td align="left" colspan="3" style="height: 18px" valign="middle" width="25%">
                                &nbsp;<asp:Label ID="labelMessage" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr valign="middle">
                            <td width="20%">
                            </td>
                            <td align="left" width="18%">
                            </td>
                            <td align="center" valign="middle" width="25%">
                                <table cellspacing="1" border="0" style="background-image: url('../graphics/bgTwoButtons.gif');
                                    background-repeat: no-repeat">
                                    <tr>
                                        <td style="width: 1">
                                        </td>
                                        <td valign="middle" style="height: 23px">
                                            <asp:ImageButton ImageAlign="AbsMiddle" ID="btnLogin" runat="server" ImageUrl="../graphics/btn_Login_en.png"
                                                OnClick="btnLogin_Click" />
                                        </td>
                                        <td valign="middle" style="height: 23px">
                                            <asp:ImageButton ImageAlign="AbsMiddle" ID="btnClear" runat="server" CausesValidation="False"
                                                ImageUrl="../graphics/btn_Clear_nl.png" OnClick="btnClear_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="left" valign="middle" width="1%">
                            </td>
                            <td align="left" style="width: 30%" valign="middle">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="loginFooter" valign="top">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
