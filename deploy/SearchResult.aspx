<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="SearchResult, App_Web_searchresult.aspx.cdcab7d2" title="Search Result" validaterequest="false" enableeventvalidation="false" theme="ThemeOne" %>

<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MainLayOut.master" %>
<asp:Content ID="Content3" ContentPlaceHolderID="headerPlaceHolder" runat="Server">
    <link href="include/inlineplayer.css" rel="stylesheet" type="text/css" />   
    <link href="include/kallol.css" rel="stylesheet" type="text/css" />
    <script src="include/tell_a_friend_widget.js" type="text/javascript"></script>
    <script src="include/soundmanager2.js" type="text/javascript"></script>
    <script src="include/inlineplayer.js" type="text/javascript"></script>


    <script src="include/Searchresult.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="Server">

   

    <div class="content-header">
        <div class="content-header-container">
            <label id="lblNewsMore" runat="server">
                Search Result</label>
        </div>
    </div>
    <table align="center" cellpadding="0" cellspacing="0" width="100%">
        <tr valign="top" class="searchResultTDstyle1">
            <td colspan="2">
                <div class="grid-pager-container">
                    <div style="display: inline; margin-left: 10px;">
                        <asp:Label ID="lblArticleCount" runat="server" Text=""></asp:Label>
                    </div>
                    
                    <div style="float: right; display: inline; margin-right: 5px;">
                        <asp:PlaceHolder ID="searchResultGridHeader" EnableViewState="true" runat="server">
                        </asp:PlaceHolder>
                    </div>
                </div>
                <div runat="server" id="divHeader" style="clear:both; padding:10px;">
                           <asp:Label ID="lblSheader" runat="server" ForeColor="Black" ></asp:Label>
                    </div>
                <!-- result module start here -->
                <asp:GridView  ID="grdSearchResult" CssClass="grdSearchResult" BorderWidth="0" ShowHeader="false" PageSize="5"
                    runat="server" AutoGenerateColumns="False"
                     RowStyle-CssClass="search-result-row">
                    <AlternatingRowStyle CssClass="search-result-row" BackColor="White" HorizontalAlign="Left"
                        VerticalAlign="Top" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                            <%string s = this.SearchType;
                              if (s == "%%")
                              { %>
                            <div class="grid-pager-container" style="width:700px" > 
                                <div style="margin-left:5px">
                                 <span>  <%# this.GetType(DataBinder.Eval(Container.DataItem, "articletype").ToString()) %></span>
                                 
                            
                                </div> 
                             </div>
                             <% }%> 
                                <div class="searchresult-grid-row-wrapper">
                                    <div class="searchresult-left-row-container">
                                        <div class="search-result-image-container">
                                        
                                        
                                        
                                            <asp:Image ID="imgSheetmusicOrgaanPreview" ImageUrl='<%#GetArticleImagePath(DataBinder.Eval(Container.DataItem, "imagefile").ToString())%>' runat="server" />
                                        </div>
                                       
                                   <!--PDF -->
                                    
                                            <div runat="server" class="divPdf"  visible ='<%#IsPdfExists(DataBinder.Eval(Container.DataItem, "pdffile").ToString())%>' >
                                              
                                               <input type="button" class="button" id="btnPdf" value= "<%#(string)base.GetGlobalResourceObject("string", "viewpdf")%>"
                                               onclick ="javascript:window.open('<%#getViewPdfPath(DataBinder.Eval(Container.DataItem, "pdffile").ToString())%>')" />
                                            </div>
                                           
                                       
                                
                                   
                                    </div>
                                    <div class="searchresult-right-row-container">
                                    <div style="height:40px;margin-top:5px;">
                                        <div class="searchresult-title-column">
                                            <%#(string)base.GetGlobalResourceObject("string", "title") %>
                                        </div>
                                        <div class="searchresult-content-column">
                                            <asp:Label ID="lblTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "title")%>'></asp:Label>
                                        </div>
                                    </div>
                                    <div class="middiv" >
                                        <div class="searchresult-title-column">
                                            <%#(string)base.GetGlobalResourceObject("string", GetAuthorComposer())%>
                                        </div>
                                        <div class="searchresult-content-column">
                                            <%# DataBinder.Eval(Container.DataItem, "author")%>
                                        </div>
                                        
                                        <div class="searchresult-title-column">
                                            <%#(string)base.GetGlobalResourceObject("string", "publisher")%>
                                        </div>
                                        <div class="searchresult-content-column">
                                            <%# DataBinder.Eval(Container.DataItem, "publisher")%>
                                        </div>
                                        <div class="searchresult-title-column">
                                           ISBN
                                        </div>
                                        <div class="searchresult-content-column">
                                            <%# DataBinder.Eval(Container.DataItem, "isbn")%>
                                        </div
                                        
                                         <!-- Play -->
                                        <div id="Div1" runat ="server" visible ='<%# IsMusicFileExists(Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "containsmusic")),DataBinder.Eval(Container.DataItem, "articlecode").ToString() )%>' >
                                             <div class="searchresult-title-column">
                                                <div class="divPlay">
                                                </div>
                                            </div>
                                            <div class="searchresult-content-column">
                                            <a class="play-link" href="javascript:void(0)" rel='Resources/audio/<%#DataBinder.Eval(Container.DataItem, "articlecode")%>.mp3'><%#DataBinder.Eval(Container.DataItem, "title")%></a>
                                            </div>
                                           
                                        </div>
                                        
                                       </div> 
                                      
                                     
                                        <div class="search-result-description">
                                            <div class="searchresult-title-column">
                                                <%#(string)base.GetGlobalResourceObject("string", "detail")%>
                                            </div>
                                            <div class="searchresult-content-column" style="height:auto;">
                                                <%# DataBinder.Eval(Container.DataItem, "description")%>
                                            </div>
                                        </div>
                                        
                                        <div class="search-price-place-container" style="width:520px" >
                                         <div class="search-price-place">
                                          <%#(string)base.GetGlobalResourceObject("string", "price") %>
                                          </div>
                                          <div style="display:inline;margin-left:3px;">
                                                <div class="rounded-graybutton-left">
                                                </div>
                                                <div class="rounded-graybutton-middle">
                                                    <b>€
                                                        <%# string.Format("{0:F2}",double.Parse(DataBinder.Eval(Container.DataItem, "price").ToString()))%>
                                                    </b>
                                                </div>
                                                <div class="rounded-graybutton-right">
                                                </div>
                                            </div>
                                           
                                           
                                           <div class="search-price-place" style=" text-align:right; width:125px">
                                           
                                                Delivery  <%# DataBinder.Eval(Container.DataItem, "deliverytime")%>
                                           
                                           </div>
                                           <div style="display:inline;float: right;margin-right:5px;">
                                               <asp:Button CssClass="button"  ID="btnQuickBuy" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "articlecode")%>'
                                                 Text='<%#(string)base.GetGlobalResourceObject("string", "basket")%>'  OnCommand="btnAddToCart_Command" >
                                                </asp:Button>
                                                <input type="submit" value="  <%#(string)base.GetGlobalResourceObject("string", "detail")%>" class="button search-detail"   />
                                                <input type="submit" value="  <%#(string)base.GetGlobalResourceObject("string", "tellafriend")%>" class="button tell-a-friend-btn"   />
                                              
                                            </div>
                                            
                                        </div>
                                      
   
                                    </div>
                                    <div class="searchresult-container-footer" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                
                <!-- result module end here -->
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarPlace" runat="Server">
    <div class="content-sidebar-header">
        <div class="sidebar-container">
            <asp:Label Style="color: White;" ID="lblSort" runat="server">
                Sort</asp:Label>
        </div>
    </div>
    <div class="sidebar-content-body" style="height: 130px;">
        <ul class="sort-ul">
            <li><a href="javascript:void(0)" rel="price" class="sort-link">» Price</a> </li>
            <li><a href="javascript:void(0)" rel="publisher" class="sort-link">» Publisher</a>
            
            
            </li>
            <li><a href="javascript:void(0)" rel="author" class="sort-link">» Author</a> </li>
            <li><a href="javascript:void(0)" rel="title" class="sort-link">» Title</a> </li>
        </ul>
    </div>
    <div class="content-sidebar-header">
        <div class="sidebar-container">
            <asp:Label ID="lblCategoryHeader" Style="color: White;" Text='Category' runat="server" />
        </div>
    </div>
    <div class="sidebar-content-body" style="height: auto;">
        <ul class="sort-ul">
            <li><a href="javascript:void(0)" id="sheetmusicCountAnchor" runat="server" rel="s"
                class="category-link">» Sheetmusic <span class='category-count'>(0)</span></a>
                                
            </li>
            
            <asp:placeholder id="sheetMusicSubcategoryConatiner" runat="server"></asp:placeholder>
            
            <li><a href="javascript:void(0)" id="booksCountAnchor" runat="server" rel="b" class="category-link">
                » Books <span class='category-count'>(0)</span></a> </li>
            <asp:placeholder id="bookSubcategoryConatiner" runat="server"></asp:placeholder>
            
            <li><a href="javascript:void(0)" id="cdDVDCountAnchor" runat="server" rel="c" class="category-link">
                » CD/DVD <span class='category-count'>(0)</span></a> </li>
                <asp:placeholder id="cddvdSubcategoryConatiner" runat="server"></asp:placeholder>
        
        </ul>
    </div>
    <!-- Top 10 -->
    <div class="content-sidebar-header">
        <div class="sidebar-container">
            <asp:Label ID="lblTopTenProducts" Style="color: White;" Text='Top Ten' runat="server" />
        </div>
    </div>
    <div class="sidebar-content-body" style="height: 130px;">
        <asp:GridView ID="gridTopTen" runat="server" AutoGenerateColumns="false" BorderWidth="0"
            RowStyle-BorderStyle="None" ShowHeader="false" Width="100%" BorderStyle="None"
            CellPadding="0" BackColor="#EFEFEF" BorderColor="#EFEFEF">
            <RowStyle BorderStyle="None" BackColor="#EFEFEF" BorderColor="#EFEFEF"></RowStyle>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div style="background-color: #efefef">
                            <a style="color: #2F2F2F  !important" href="SearchResult.aspx?articlecode=<%#DataBinder.Eval(Container.DataItem, "articlecode")%>">
                                <%# Container.DataItemIndex + 1 %>)&nbsp;&nbsp;
                                <%# DataBinder.Eval(Container.DataItem, "title").ToString().Replace("''"," ").Replace("'"," ")%></a>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <AlternatingRowStyle BackColor="#EFEFEF" BorderColor="#EFEFEF" BorderStyle="None" />
        </asp:GridView>
    </div>
</asp:Content>
