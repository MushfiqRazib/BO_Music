<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="SearchResult, Bo02" title="Search Result" validaterequest="false" enableeventvalidation="false" theme="ThemeOne" %>

<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MainLayOut.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">

      window.onunload = function CheckChild()
      {                 
           if(player && player!=1)
           {
              try{
                    player.Reassign();                            
                }
                catch(err)
                {}
           }
      }
      function ReAssign(hand)
      {
         player = hand;      
      }
</script>
    <table align="center" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" runat="server" id="header" style="height: 19px; width: 509px;">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 30px">
                <table cellpadding="0" cellspacing="0" class="contentArea">
                    <tr>
                        <td valign="top" align="left" class="pageLocation" style="height: 65px">
                            <%--<asp:Label ID="lblCurrentPage" runat="server" Font-Bold='True'></asp:Label>--%>
                            
                            <%--<asp:Label ID="lblPageRoot" runat="server" ForeColor="AppWorkspace" Text=" "></asp:Label>--%>
                            <%--<asp:Label ID="lblActivePage" runat="server" ForeColor="#3300ff" Text=" ">
                            </asp:Label>--%>
                            <asp:Label ID="lblStaticText" Font-Bold="False" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblStaticTextB" runat="server" Visible="False"></asp:Label><asp:LinkButton
                                ID="lbtBook" runat="server" OnClick="LinkButton1_Click" Visible="False"></asp:LinkButton>
                            <asp:Label ID="lblStaticS" runat="server" Visible="False"></asp:Label>
                            <asp:LinkButton ID="lbtSM" runat="server" OnClick="LinkButton1_Click" Visible="False"></asp:LinkButton>
                            <asp:Label ID="lblStaticCD" runat="server" Visible="False"></asp:Label>
                            <asp:LinkButton ID="lbtCD" runat="server" OnClick="LinkButton1_Click" Visible="False"></asp:LinkButton><br />
                            <br />
                            <asp:Label ID="lblResultCount" Font-Bold="true" runat="server" Text=""></asp:Label>
                            &nbsp;<asp:Label ID="lblResult" runat="server" ForeColor="AppWorkspace" Text="results"></asp:Label></td>
                        <td align="right" valign="top" style="height: 65px">
                            <uc1:Search ID="Search1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="middle">
            <td colspan="2" align="left" style="height: 30px">
                &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Label runat="server" ID="lblSortBy"></asp:Label>&nbsp&nbsp;&nbsp;
                <asp:DropDownList ID="drpSort" runat="server">
                    <%--<asp:ListItem Selected="True" Value="title"></asp:ListItem>
                    <asp:ListItem Value="c.firstname"></asp:ListItem>
                    <asp:ListItem Value="p.firstname"></asp:ListItem>
                    <asp:ListItem Value="price"></asp:ListItem>--%>
                </asp:DropDownList>
                &nbsp;
                <%--<asp:DropDownList ID="drpOrder" runat="server">
                    <asp:ListItem Selected="True" Value="asc">Ascending</asp:ListItem>
                    <asp:ListItem Value="desc">Descending</asp:ListItem>
                </asp:DropDownList>--%>
                &nbsp;
                <asp:ImageButton ImageAlign="AbsMiddle" ID="btnOk" runat="server" ImageUrl='~/graphics/btn_ase.gif'
                    CausesValidation="False" OnClick="btnOk_Click"></asp:ImageButton>
                &nbsp;
                <asp:ImageButton ImageAlign="AbsMiddle" ID="btnDesc" runat="server" ImageUrl='~/graphics/btn_desc.gif'
                    CausesValidation="False" OnClick="btnDesc_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr valign="top" class="searchResultTDstyle1">
            <td colspan="2">
                <!-- result module start here -->
                <asp:GridView ID="grdSearchResult" AlternatingRowStyle-BackColor="#a7a6a6" BorderWidth="0"
                    runat="server" AutoGenerateColumns="False" OnRowDataBound="grdSearchResult_RowDataBound" ShowHeader="False">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr style="background-color: #FAFAFA;" valign="top">
                                        <td style="padding-top:20px; padding-left:20px" align="left" class="searchResultTDstyle2">
                                            <%#ShowImage(DataBinder.Eval(Container.DataItem, "imagefile").ToString()) %>
                                        </td>
                                        <td style="padding-top: 10px">
                                            <table width="750" height="132" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td align="left" style="width: 120px;">
                                                        <%#(string)base.GetGlobalResourceObject("string", "title") %>
                                                        :</td>
                                                    <td align="left" style="font-weight: bold">
                                                        <asp:Label ID="lblTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "title")%>'></asp:Label>                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 120px;">                                                        
                                                    </td>
                                                    <td align="left">
                                                        <%# DataBinder.Eval(Container.DataItem, "subtitle")%>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td align="left">
                                                        <%#(string)base.GetGlobalResourceObject("string", GetAuthorComposer())%>
                                                        :</td>
                                                    <td align="left" style="font-weight: bold">
                                                        <%# DataBinder.Eval(Container.DataItem, "author")%>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td align="left"><%# GetInstrumentationText()%></td>
                                                    <td align="left" style="font-weight: bold">
                                                        <%# DataBinder.Eval(Container.DataItem, "instrumentation")%></td>
                                                </tr>
                                                
                                                <tr style="height: 6px">
                                                    <td align="left">
                                                        <asp:Label ID="lblPublisher" runat="server" Text='<%#(string)base.GetGlobalResourceObject("string", "Publisher")+":"%>'></asp:Label></td>
                                                    <td align="left" style="font-weight: bold">
                                                        <%# DataBinder.Eval(Container.DataItem, "publisher")%>
                                                    </td>
                                                </tr>                                               
                                                <tr>
                                                    <td align="left">
                                                        <%#(string)base.GetGlobalResourceObject("string", "price") %>
                                                        :</td>
                                                    <td align="left" style="font-weight: bold">
                                                        <b>€
                                                            <%# string.Format("{0:F2}",double.Parse(DataBinder.Eval(Container.DataItem, "price").ToString()))%>
                                                    </td>
                                                </tr>
                                                <tr valign="bottom">
                                                    <td style="padding-bottom:0px;">
                                                    </td>
                                                    <td align="right">
                                                        <!-- Edited Area-->
                                                       
                                                       
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="background-color: #FAFAFA;">
                                        <td>                                            
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td align="left" style="width:40%; vertical-align:bottom">
                                                        <table  cellspacing="0" border="0" style="vertical-align:bottom">
                                                            <tr>                                                                
                                                                <td>
                                                                     <%--<%#ShowMusicButton(DataBinder.Eval(Container.DataItem, "Audio").ToString(),DataBinder.Eval(Container.DataItem, "Code").ToString())%>&nbsp;--%>
                                                                    <asp:ImageButton ImageAlign="AbsMiddle" ID="btnPlay" runat="server" 
                                                                         AlternateText="Play" ImageUrl="~/graphics/btn_play.png" 
                                                                         onmouseover="javascript:this.src='graphics/btn_play_over.png'" onmouseout="javascript:this.src='graphics/btn_play.png'"
                                                                                  
                                                                         CommandArgument='<%# DataBinder.Eval(Container.DataItem, "articlecode")%>'  
                                                                         CausesValidation="False" 
                                                                         Visible='<%#DataBinder.Eval(Container.DataItem, "containsmusic")%>' 
                                                                          ToolTip="Play"></asp:ImageButton>&nbsp;                                                                            
                                                                   <%-- <%#ShowMusicButton()%>&nbsp;--%>                                                                   
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ImageAlign="AbsMiddle" ID="btnAddtoPlay" runat="server" 
                                                                        AlternateText="Add to playlist" ToolTip="Add to playlist" ImageUrl="~/graphics/btn_addtoplaylist.png" 
                                                                        onmouseover="javascript:this.src='graphics/btn_addtoplaylist_over.png'" onmouseout="javascript:this.src='graphics/btn_addtoplaylist.png'"
                                                                         
                                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "articlecode")%>'   
                                                                        
                                                                        CausesValidation="False" 
                                                                        Visible='<%#DataBinder.Eval(Container.DataItem, "containsmusic")%>' 
                                                                        ></asp:ImageButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="vertical-align:bottom; width:60%" align="right">
                                                        <table  cellspacing="0" border="0" class="homeTdStyle2">
                                                            <tr>
                                                                <!--<td style="width: 5px;"><img src="graphics/bgBtnRound.png" /></td>-->                                                                
                                                                <td style="padding-left:3px; padding-top:3px; padding-bottom:3px;">
                                                                    <asp:ImageButton ImageAlign="AbsMiddle" ID="btnQuickBuy" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "articlecode")%>'
                                                                        OnCommand="btnQuickBuy_Command" ImageUrl='<%#ShowButonImage("btnQuickBuy") %>'></asp:ImageButton>
                                                                </td>
                                                                <td style=" padding-top:3px; padding-bottom:3px; background-color: #cccccc;">
                                                                    <asp:ImageButton ImageAlign="AbsMiddle" ID="btnAddToCart" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "articlecode")%>'
                                                                        OnCommand="btnAddToCart_Command" runat="server" ImageUrl='<%#ShowButonImage("btnAddToCart") %>'>
                                                                    </asp:ImageButton>
                                                                </td>
                                                                <td style=" background-color: #cccccc; padding-right:3px; padding-top:3px; padding-bottom:3px;">
                                                                    <asp:ImageButton ImageAlign="AbsMiddle" ID="btnDetail" runat="server" ImageUrl='<%#ShowButonImage("btnDetail") %>'
                                                                        OnCommand="btnDetail_Command" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "articlecode")%>'
                                                                        CausesValidation="False"></asp:ImageButton>
                                                                </td>
                                                                 
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                             
                                        </td>
                                    </tr>
                                    <tr style="height: 6px; background-color: #F0F0F0;">
                                        <td colspan="2" style="height: 4px">
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#A7A6A6" />
                </asp:GridView>
                <!-- result module end here -->
            </td>
        </tr>
    </table>
</asp:Content>
