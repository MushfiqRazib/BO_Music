<%@ Page Language="C#" MasterPageFile="~/MainLayOut.master" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="Details" Title="Detail Page" %>

<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc1" %>
<%@ MasterType  virtualPath="~/MainLayOut.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" Runat="Server">
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
    <table cellpadding=0 cellspacing=0 border=0  class="contentArea">
        
        <tr ><td colspan="2" runat=server id="header" style="height:20px; width: 834px;"></td></tr>
        <tr >
            <td >
                    <table cellpadding="0" cellspacing="0" style="background-color:#F0F0F0; width:882px">
                        <tr >
                            <td valign="top" align="left" class="pageLocation">
                                <%--<asp:Label ID="lblCurrentPage" runat="server"  Font-Bold='true' Text="">  </asp:Label>
                                &nbsp;&nbsp;<b>:</b>&nbsp;&nbsp;
                                <asp:Label ID="lblPageRoot" runat="server"   ForeColor="AppWorkspace"  Text=" "></asp:Label>
                                <asp:Label ID="lblActivePage" runat="server"   ForeColor="#3300ff"  Text=" ">
                                </asp:Label>--%>
                            </td>
                            <td align="right" valign="top">
                                <uc1:Search ID="Search2" runat="server"  />
                         
                            </td>
                        </tr> 
                        
                    </table>
            </td>
        </tr>
        
        <tr><td>&nbsp;</td></tr>
        <tr style="background-color:#F0F0F0;">
            <td colspan=2>
                <table cellpadding="0" cellspacing="0" border="0" align="left" style="width: 882px">
                    <tr>
                        <td style="vertical-align:top" align="center">
                            <table border=0 cellpadding=0 cellspacing=0 style="width:862px " >
                                <tr>
                                    
                                    <td align="right" class="detailsTdStyle1" >
                                        <table border=0 cellpadding=0 cellspacing=0 height=320px width=100%>
                                        <tr height=80% align=center valign=top>
                                            <td><asp:Image ID="imgArticle" runat="server" style="padding:10px" /></td>
                                        </tr>
                                        <tr height=10% align=center valign=top>
                                            <td><asp:ImageButton ID="btnPreviewPdf" runat="server" style="padding-right:10px" OnClick="btnPreviewPdf_Click" /></td>
                                        </tr>
                                        </table>
                                        
                                        
                                    </td>
                                    <td valign="top" align="left" style="background-color:#FAFAFA;">
                                        <table cellspacing=0 cellpadding=0 border=0 width=100% >
                                            <tr>
                                                <td>
                                                    <table cellspacing=0 cellpadding=0 border=0 height="300">
                                                        <tr style="vertical-align:top ">
                                                            <td colspan="3" style="height:10px;">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr style="vertical-align:top ">
                                                            <td width="10" style="width:10px"></td>
                                                            <td style="width:120px ">
                                                                <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td  >
                                                                <asp:Label ID="lblTitleValue" runat="server" Font-Bold =True></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblSubTitle" runat="server"></asp:Label>                                                                
                                                            </td>
                                                        </tr>
                                                        <tr style="vertical-align:top ">
                                                            <td ></td>
                                                            <td  >
                                                                <asp:Label ID="lblAuthor" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td  >
                                                                <asp:Label ID="lblAuthorValue" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="vertical-align:top ">
                                                            <td colspan="3" style="height:10px;">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <%--<tr>
                                                            <td ></td>
                                                            <td  >
                                                                <asp:Label ID="lblDegree" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td  >
                                                                <asp:Label ID="lblDegreeValue" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>--%>
                                                        
                                                        <tr style="vertical-align:top ">
                                                            <td ></td>
                                                            <td  >
                                                                <asp:Label ID="lblInstrumentation" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td  >
                                                                <asp:Label ID="lblInstrumentationValue" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        
                                                        <tr style="vertical-align:top ">
                                                            <td ></td>
                                                            <td  >
                                                                <asp:Label ID="lblPublisher" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td  >
                                                                <asp:Label ID="lblPublisherValue" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="vertical-align:top ">
                                                            <td colspan="3" style="height:10px;">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr style="vertical-align:top ">
                                                            <td ></td>
                                                            <td style="text-align:justify;">
                                                               <asp:Label ID="lblDescription" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td  style="text-align:justify; padding-right:5px;">
                                                                <asp:Label ID="lblDescriptionValue" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        
                                                        <%--<tr style="vertical-align:top ">
                                                            <td colspan="3" style="height:2px;">&nbsp;
                                                            </td>
                                                        </tr>--%>
                                                        
                                                        <tr style="vertical-align:top ">
                                                            <td ></td>
                                                            <td style="text-align:justify;">
                                                               <asp:Label ID="lblisbn13" runat="server" Text="ISBN"></asp:Label>
                                                            </td>
                                                            <td  style="text-align:justify; padding-right:5px;">
                                                                <asp:Label ID="lblisbn13Value" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        
                                                        <tr style="vertical-align:top ">
                                                            <td colspan="3" style="height:10px;">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr style="vertical-align:top ">
                                                            <td ></td>
                                                            <td  >
                                                                <asp:Label ID="lblPrice" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td  >
                                                                <asp:Label ID="lblPriceValue" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                  </table>
                                                </td>
                                               
                                            </tr>
                                            <tr align=right valign =top >
                                                <td valign="bottom" >
                                                    <table  cellpadding="0" cellspacing="0" border="0" width="100%" style="padding-left:10px;">
                                                        <tr>
                                                            <td align="left" style="width:40%; vertical-align:bottom">
                                                                <table  cellspacing="0" border="0" style="vertical-align:bottom">
                                                                    <tr>                                                                
                                                                        <td>                                                                             
                                                                            <asp:ImageButton ImageAlign="AbsMiddle" ID="btnPlay" runat="server" 
                                                                                 AlternateText="Play" ToolTip="Play" ImageUrl="~/graphics/btn_play.png" 
                                                                                 onmouseover="javascript:this.src='graphics/btn_play_over.png'" onmouseout="javascript:this.src='graphics/btn_play.png'"                                                                                          
                                                                                 CausesValidation="False"                                                                                  
                                                                                 ></asp:ImageButton>&nbsp;                                                                                                                                                                                                                 
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ImageAlign="AbsMiddle" ID="btnAddtoPlay" runat="server" 
                                                                                AlternateText="Add to playlist" ToolTip="Add to playlist" ImageUrl="~/graphics/btn_addtoplaylist.png" 
                                                                                onmouseover="javascript:this.src='graphics/btn_addtoplaylist_over.png'" onmouseout="javascript:this.src='graphics/btn_addtoplaylist.png'"
                                                                                CausesValidation="False" 
                                                                                ></asp:ImageButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td  style="vertical-align:bottom; width:60%" align="right">
                                                                <table cellspacing="0" border="0" class="homeTdStyle2">
                                                                    <tr >                                                                      
                                                                        <td style="padding-left:3px; padding-top:3px; padding-bottom:3px;" >
                                                                            <asp:ImageButton ImageAlign="AbsMiddle" ID="btnQuickBuy" runat="server" OnClick="btnQuickBuy_Click" />
                                                                          </td>
                                                                        <td style=" padding-top:3px; padding-bottom:3px; background-color: #cccccc;" >
                                                                          <asp:ImageButton ImageAlign="AbsMiddle" ID="btnAddToCart" runat="server" OnClick="btnAddToCart_Click" />
                                                                        </td>
                                                                        <td style=" background-color: #cccccc; padding-right:3px; padding-top:3px; padding-bottom:3px;" >
                                                                            <asp:imagebutton ImageAlign="AbsMiddle" id="btnGoBack" OnClick="btnBack_Click" runat="server"  />
                                                                        </td>
                                                                    </tr>
                                                                   
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                                
                                                                
                                                                
                                                            
                                                          <%--
                                                                       <table  cellspacing="1" border="0" style="background-image :url('graphics/bgThreeButtons.gif');background-repeat:no-repeat; height: 23px; left: 2px; position: relative; top: 2px;">
                                                                             <tr>
                                                                                <td style="width:1"></td>
                                                                                <td valign="middle" style="height: 23px">
                                                                                    <asp:imagebutton ImageAlign="AbsMiddle" id="btnSpotlight" runat="server" ImageUrl="graphics/btn_spotlight.png" OnClick="btnSpotlight_Click" CausesValidation="False"></asp:imagebutton>
                                                                                </td>
                                                                                <td valign="middle" style="height: 23px" >
                                                                                    <asp:imagebutton ImageAlign="AbsMiddle" id="btnQuickBuy" runat="server" ImageUrl="graphics/btn_quickbuy.png" OnClick="btnQuickBuy_Click" CausesValidation="False"></asp:imagebutton>
                                                                                </td>
                                                                                <td valign="middle" style="height: 23px">
                                                                                    <asp:imagebutton ImageAlign="AbsMiddle" id="btnDetail" runat="server" ImageUrl="graphics/btn_details.png" OnClick="btnDetail_Click" CommandArgument="0" CausesValidation="False"></asp:imagebutton>
                                                                                </td>
                                                                                <td style="width:2"></td>
                                                                            </tr>
                                                                            <tr><td style="height:1;"></td></tr>
                                                                       </table>   
                                                     --%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr><td style="height:30px"></td></tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

