<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayOut.master" AutoEventWireup="true" CodeFile="errorpage.aspx.cs" Inherits="errorpage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">
<div class="content-header">
        <div class="content-header-container">
            <label  runat="server">
                Error Occured</label>
        </div>
    </div>
   
<asp:Label ID="lblErrorMessage" Text="This is Error Page" runat="server"></asp:Label>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SidebarPlace" Runat="Server">
</asp:Content>

