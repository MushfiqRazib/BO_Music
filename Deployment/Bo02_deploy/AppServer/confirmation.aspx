<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="confirmation, Bo02" title="Confirmation" theme="ThemeOne" %>
<%@ MasterType  virtualPath="~/MainLayOut.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="confirmationTDStyle1">
        <tr>
            <td valign=middle align=center>
                <table class="confirmationTDStyle2" cellpadding=0 cellspacing=0 border=0>
                <tr>
                    <td align="center" valign=middle colspan="3">
                        <asp:Label ID="lblRegisterMessage" runat="server" Font-Size="Large" ForeColor="Red" Font-Bold="False">Thank you for registering with us.</asp:Label>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

