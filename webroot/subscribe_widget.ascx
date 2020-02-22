<%@ Control Language="C#" AutoEventWireup="true" CodeFile="subscribe_widget.ascx.cs" Inherits="subscribe_widget" %>
<%@ Reference virtualPath="~/MainLayOut.master" %>
<div class="content-sidebar-header">
        <div class="sidebar-container">
            <label id="Label1" runat="server">
                Newsletter</label>
        </div>
    </div>
    <div style="height: 55px;" class="sidebar-content-body">
        <div style="float: left; height: 20px; vertical-align: middle; padding-top: 2px">
            <asp:Label ID="lblEmail" runat="server" Text="E-mail"></asp:Label>:
        </div>
        <div style="padding-left: 4px;float: left">
            <asp:TextBox ID="txtMail" runat="server" Text="" CausesValidation="True" MaxLength="40"></asp:TextBox>
        </div>
        <div style="padding-left: 4px">
            <asp:Button   ID="btnsubscribe" CssClass="button"  runat="server" Text="register" OnClick="btnsubscribe_Click">
            </asp:Button>
        </div>
        <div style="float:left">
            <%= (string)base.GetGlobalResourceObject("string", "subscribe_mail")%>
        </div>
    </div>
    