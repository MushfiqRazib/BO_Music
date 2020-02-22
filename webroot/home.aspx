<%@ Page Language="C#" MasterPageFile="~/MainLayOut.master" AutoEventWireup="true"
    CodeFile="home.aspx.cs" Inherits="home" Title="Home" ValidateRequest="false" %>

<%@ Register TagPrefix="uc1" TagName="Subscribe" Src="~/subscribe_widget.ascx" %>
<%@ Register TagPrefix="uc2" TagName="ContactWidget" Src="~/contact_detail_widget.ascx" %>
<%@ MasterType VirtualPath="~/MainLayOut.master" %>
<asp:Content ID="Content3" ContentPlaceHolderID="headerPlaceHolder" runat="Server">

    <script src="include/Home.js" type="text/javascript"></script>

    <link href="include/kallol.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="Server">
    <div style="height: 263px;">
        <div class="content-header">
            <div class="preview-header">
                <div class="preview-link">
                    <asp:Label Text="Sheetmusic Orgaan" runat="server" />
                </div>
            </div>
            <div class="preview-topheader">
            </div>
            <div class="preview-header">
                <div class="preview-link">
                    <asp:Label ID="Label5" Text="Books Orgaan" runat="server" />
                </div>
            </div>
            <div class="preview-topheader">
            </div>
            <div class="preview-header">
                <div class="preview-link">
                    <asp:Label ID="Label6" Text="CD/DVD Orgaan" runat="server" />
                </div>
            </div>
            <div class="preview-topheader">
            </div>
            <div class="preview-header" style="width: 168px">
                <div class="preview-link">
                    <asp:Label ID="Label7" Text="Sheetmusic Other" runat="server" />
                </div>
            </div>
        </div>
        <div style="display: block; float: left; background-color: White; margin-top: 0px;
            width: 100%">
            <div class="preview-content">
                <div align="center" class="preview-image-container" style="">
                    <asp:Image ID="imgSheetmusicOrgaanPreview" ImageUrl="~/graphics/preview.png" runat="server" />
                </div>
                <div class="preview-sheetmusic-orgaan-logo">
                </div>
                <div align="center" class="preview-button">
                    <asp:Button CssClass="button" OnCommand="detail_command" runat="server" ID="btnSheetmusicOrgaanPreview"
                        Text="View" />
                </div>
                <div align="center" class="preview-title">
                    <asp:Label runat="server" ID="lblSheetmusicOrgaanPreviewTitle" Text="Aan de han van Batch.." />
                </div>
            </div>
            <div class="preview-bottomheader">
            </div>
            <div class="preview-content">
                <div align="center" class="preview-image-container" style="">
                    <asp:Image ID="imgBookPreview" ImageUrl="~/graphics/preview.png" runat="server" />
                </div>
                <div class="preview-sheetmusic-books-logo">
                </div>
                <div align="center" class="preview-button">
                    <asp:Button CssClass="button" OnCommand="detail_command" runat="server" ID="btnBookOrgaanPreview"
                        Text="View" />
                </div>
                <div align="center" class="preview-title">
                    <asp:Label runat="server" ID="lblBookPreview" Text="Aan de han van Batch.." />
                </div>
            </div>
            <div class="preview-bottomheader">
            </div>
            <div class="preview-content">
                <div align="center" class="preview-image-container" style="">
                    <asp:Image ID="imgCDDVDPreview" ImageUrl="~/graphics/preview.png" runat="server" />
                </div>
                <div class="preview-sheetmusic-cd-logo">
                </div>
                <div align="center" class="preview-button">
                    <asp:Button CssClass="button" OnCommand="detail_command" runat="server" ID="btnCDDVDPreview"
                        Text="View" />
                </div>
                <div align="center" class="preview-title">
                    <asp:Label runat="server" ID="lblCDDVDPreview" Text="Aan de han van Batch.." />
                </div>
            </div>
            <div class="preview-bottomheader">
            </div>
            <div class="preview-content" style="width: 168px">
                <div align="center" class="preview-image-container" style="">
                    <asp:Image ID="imgOtherSheetmusicPreview" ImageUrl="~/graphics/preview.png" runat="server" />
                </div>
                <div class="preview-sheetmusic-other-logo">
                </div>
                <div align="center" class="preview-button">
                    <asp:Button CssClass="button" OnCommand="detail_command" runat="server" ID="btnOtherSheetmusicPreview"
                        Text="View" />
                </div>
                <div align="center" class="preview-title">
                    <asp:Label runat="server" ID="lblOtherSheetmusicPreview" Text="Aan de han van Batch.." />
                </div>
            </div>
        </div>
    </div>
    <div class="content-header">
        <div class="content-header-container">
            <asp:Label id="lblNewsMore" runat="server" Text="News and more">
                News and more</asp:Label>
        </div>
    </div>
    <asp:GridView ID="grdNews" CssClass="grdNews" runat="server" ShowHeader="false" BorderWidth="0"
        AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <div class="grid-news-left-row">
                        <asp:Image runat="server" ImageUrl='<%#GetNewsImagePath(DataBinder.Eval(Container.DataItem, "newsimagefile").ToString())%>' />
                    </div>
                    <div class="grid-news-right-row">
                        <span class="gridtext"><b>
                            <%# DataBinder.Eval(Container.DataItem, "date")%></span> &nbsp; |&nbsp; <span class="gridtext">
                                <%# DataBinder.Eval(Container.DataItem, "title")%>
                            </span></b> <span class="gridtext news-description">
                                  <%# GetDescriptionWithMoreIfRequired(DataBinder.Eval(Container.DataItem, "fulldescription").ToString())%>
                            </span><span style="display: none" class="gridtext full-news-description">
                    <%# DataBinder.Eval(Container.DataItem, "fulldescription")%>...&nbsp;<a href="javascript:void(0)" class="lessnewslink pinklink"> <%= ((string)base.GetGlobalResourceObject("string", "less")).ToLower() %></a>
                 </span>
                    </div>
                     <div class="seperator">
                     </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div class="footer-container">
        <a id="lnkMoreNews" href="news.aspx" text="morenews &raquo;" forecolor="white" runat="server">
            <%= (string)base.GetGlobalResourceObject("string", "morenews") + " &raquo;"%></a></div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarPlace" runat="Server">
    <uc2:ContactWidget runat="server" />
    <uc1:Subscribe runat="server" ID="usbscribe" />
    <div class="content-sidebar-header">
        <div class="sidebar-container">
            <label id="Label2" runat="server">
                Spotlight</label>
        </div>
    </div>
    <div class="sidebar-content-body" style="height: auto;">
        <div style="float: left; width: 104px; height: 148px;">
            <asp:Image ID="imgSpotlight" ImageUrl="~/Resources/images/spotlight.png" runat="server" />
        </div>
        <div style="float: right; width: 175px;">
            <asp:Label ID="lblTitle" runat="server" Font-Bold="True"></asp:Label>
            <br />
            <asp:Label ID="lblSubTitle" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblComposer" runat="server" />
            <br />
            <div class="spotlight-description">
                <asp:Literal ID="lblDescription" runat="server"></asp:Literal>
            </div>
            <div class="spotlight-full-description" style="display: none;">
                <asp:Literal ID="lblFullDescription" runat="server"></asp:Literal>
            </div>
            <br />
            <div style="float: left; margin-right: 5px; padding: 2px;">
                <asp:Label ID="lblPriceInfo" runat="server"></asp:Label>
            </div>
            <div style="float: left">
                <div class="rounded-button-left">
                </div>
                <div class="rounded-button-middle">
                    <asp:Label ID="lblPrice" runat="server"></asp:Label></div>
                <div class="rounded-button-right">
                </div>
            </div>
            <div style="float: left; clear: left; margin-top: 2px;margin-bottom: 3px;">
                <asp:Button CssClass="basket-button button" Width="80px" ID="btnQuickBuy" Text="Basket"
                    runat="server" OnClick="btnQuickBuy_Click" CausesValidation="False"></asp:Button>
                <asp:Button Width="90px" ID="btnDetail" runat="server" Text="Detail" CssClass="button"
                    OnClick="btnSpotlightDetail_Click" CommandArgument="0" CausesValidation="False">
                </asp:Button>
            </div>
        </div>
    </div>
    <div class="content-sidebar-header" style="clear: both; margin-top: 10px;">
        <div class="sidebar-container">
            <label id="Label3" runat="server">
                Partners</label>
        </div>
    </div>
</asp:Content>
