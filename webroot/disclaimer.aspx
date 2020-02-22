<%@ Page Language="C#" MasterPageFile="~/MainLayOut.master" AutoEventWireup="true" CodeFile="disclaimer.aspx.cs" Inherits="disclaimer" Title="Disclaimer" %>
 <%@ MasterType VirtualPath="~/MainLayOut.master" %>
 <asp:content ID="Content3" ContentPlaceHolderID="headerPlaceHolder" runat="Server">

 
 <script type="text/javascript">



    $(document).ready(function() {

    $('.content-body  a').addClass('pinklink');
    
    
    });

</script>

</asp:content>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" Runat="Server">
 <div class="content-header">
        <div class="content-header-container">
            <asp:Label Text="Disclaimer" id="lblAboutUs" runat="server">
                </asp:Label>
        </div>
    </div>
<div style="margin:10px">
<asp:Label style="color:#2F2F2F;" ID="lblDisclaimer" runat="server" Text="Label"></asp:Label>
</div>
    
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
                       CssClass="button"      OnClick="btnContact_Click" /></center>
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
            <asp:Button ID="btnsubscribe" CssClass="button"  runat="server" Text="register" OnClick="btnsubscribe_Click">
            </asp:Button>
        </div>
       <font color="red">
            <asp:Label ID="labelMailMessage" runat="server"></asp:Label></font> Wilt u op
        de hoogte worden gehouden van nieuwe uitgaven en activiteiten?<br />
        Laat dan hier uw emailadres achter!
    </div>
   
</asp:Content>

