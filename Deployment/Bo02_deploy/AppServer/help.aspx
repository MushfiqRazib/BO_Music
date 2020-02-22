<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="help, Bo02" title="Untitled Page" theme="ThemeOne" %>

<%@ MasterType VirtualPath="~/MainLayOut.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span style="font-size: 11pt; color: #000000">
        <br />
        <br />
        <strong>
            <asp:Label ID="lblCS" runat="server" Text="Label"></asp:Label>
        </strong></span>
</asp:Content>
