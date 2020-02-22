<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" title="News" inherits="news, App_Web_news.aspx.cdcab7d2" theme="ThemeOne" %>
<%@ MasterType VirtualPath="~/MainLayOut.master" %>

<%@ Register TagPrefix="uc2" TagName="ContactWidget" Src="~/contact_detail_widget.ascx" %>

<%@ Register TagPrefix="uc1" TagName="Subscribe" Src="~/subscribe_widget.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" Runat="Server">


    <script src="include/news.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">
<div class="content-header">
        <div class="content-header-container">
            <label id="lblNewsMore" runat="server">
                News and more</label>
        </div>
    </div>
    <div class="grid-pager-container">
        <div style="display: inline; margin-left: 5px;">
            <asp:Label ID="lblArticleCount" runat="server" Text="92 articles"></asp:Label>
        </div>
       
    </div>

  <asp:GridView ID="grdNews" CssClass="grdNews" runat="server" ShowHeader="false" BorderWidth="0" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                
                
                <div class="grid-news-left-row">
                <asp:Image ID="Image1" runat="server" ImageUrl='<%#GetNewsImagePath(DataBinder.Eval(Container.DataItem, "newsimagefile").ToString())%>'/>
              
                            
                </div>
                 <div class="grid-news-right-row">
                <span class="gridtext"> <b>
                        <%# DataBinder.Eval(Container.DataItem, "date")%></span> &nbsp; |&nbsp; <span class="gridtext">
                        
                            <%# DataBinder.Eval(Container.DataItem, "title")%>
                            </span></b>
                             <span class="gridtext news-description">
                             <%# GetDescriptionWithMoreIfRequired(DataBinder.Eval(Container.DataItem, "fulldescription").ToString())%>
                    </span>
                     <span style="display:none" class="gridtext  full-news-description">
                    <%# DataBinder.Eval(Container.DataItem, "fulldescription")%>...&nbsp;<a href="javascript:void(0)" class="lessnewslink pinklink"> <%= ((string)base.GetGlobalResourceObject("string", "less")).ToLower() %></a>
                    </span>
                  
                
                </div>
                      <div class="seperator">
                     </div>
                </ItemTemplate>
               
            </asp:TemplateField>
        </Columns>
        
    </asp:GridView>
<div class="grid-pager-container">
        
        <div style="float: right; display: inline; margin-right: 5px;line-height:20px;">
            <asp:PlaceHolder ID="searchResultGridHeader" EnableViewState="true" runat="server">
            </asp:PlaceHolder>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SidebarPlace" Runat="Server">

<uc2:ContactWidget ID="ContactWidget1" runat=server />
     <uc1:Subscribe  runat=server/>
</asp:Content>

