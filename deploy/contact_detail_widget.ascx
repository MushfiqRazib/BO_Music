<%@ control language="C#" autoeventwireup="true" inherits="contact_detail_widget, App_Web_contact_detail_widget.ascx.cdcab7d2" %>


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
    