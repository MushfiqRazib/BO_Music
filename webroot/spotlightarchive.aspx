<%@ Page Language="C#" MasterPageFile="~/MainLayOut.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="spotlightarchive.aspx.cs" Inherits="spotlightarchive" Title="Spotlight Archive" %>
<%@ MasterType  virtualPath="~/MainLayOut.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" Runat="Server">
<table cellpadding=0 cellspacing=0 border=0 style="width: 882px" >
<tr >
    <td  valign=top>
        <table cellpadding="0" cellspacing="0" style="background-color:white; width:882px">
            <tr >
                <td valign="top" align="left" class="pageLocation"><asp:Label ID="lblCurrentPage" runat="server"  Font-Bold='true' Text="">  </asp:Label>
                        &nbsp;&nbsp;<b></b>&nbsp;&nbsp;
                        <asp:Label ID="lblPageRoot" runat="server"   ForeColor="AppWorkspace"  Text=" "></asp:Label>
                        <asp:Label ID="lblActivePage" runat="server"   ForeColor="#3300ff"  Text=" ">
                        </asp:Label>
                </td>
                <td align=right  valign=top>
                 <table id="tblBtn" cellpadding=3 cellspacing=0 border=0  class="twoButtonBg">
                    <tr>
                        <td ><asp:ImageButton ID="btnGoback" runat="server" OnClick="btnGoback_Click"  /></td>
                                          
                    </tr>
                </table>
                    
                </td>
                
            </tr> 
            
        </table>
    </td>
</tr>
<%--<tr ><td id="Td1" colspan="2" runat=server style="height:5px; width: 834px;"></td></tr>--%>
<tr ><td colspan="2" runat=server id="header" valign=top style="height:19px; width: 834px;"></td></tr>
<tr valign=top>
    <td colspan =2 valign =top >
        <asp:GridView ID="grdSpotlightArchive" runat="server" ShowHeader=false BorderWidth=0 AutoGenerateColumns="False" width=882px>
        <Columns>
            <asp:TemplateField>
                
                <ItemTemplate >
                     <table cellpadding=0 cellspacing=0px border=0 width=100% bgcolor="#e6e6e6">
                          <tr >
                            <td valign=top>
                            <table cellpadding=0px cellspacing=0px border=0 align=center width=100% >
               
                            <tr>
                                <td valign=top align=left style="padding-top:10px; padding-bottom:0px; width:180px;">
                                    <%# ShowImage(DataBinder.Eval(Container.DataItem, "imagefile").ToString())%>
                                <td>
                                <td align=left valign=top>
                                    <table border=0 cellpadding =0 cellspacing=0px align=left width=100%>
                                    <tr ><td style=" font-size:13px;padding-top:10px; padding-right:5px;"><b> <%# DataBinder.Eval(Container.DataItem, "title")%></b></td></tr>
                                    <tr ><td width=100% height=20px></td></tr>
                                    <tr ><td valign =top style="text-align:justify; height:150px; padding-right:5px;"> <%# DataBinder.Eval(Container.DataItem, "description")%></td></tr>
                                    <tr ><td width=100% height=10px></td></tr>
                                    <tr ><td style="text-align:justify; padding-right:5px;"> 
                                      <%# GetProperty(DataBinder.Eval(Container.DataItem, "articletype").ToString(), DataBinder.Eval(Container.DataItem, "articleProperty").ToString())%></td></tr>
                                    <tr ><td height=10px></td></tr>
                                    <tr ><td > 
                                        <asp:Label ID="lblPrice" runat="server" Text="Price: " Font-Bold =true></asp:Label><b>€ <%# string.Format("{0:F2}",double.Parse(DataBinder.Eval(Container.DataItem, "price").ToString())) %></b>
                                    </td></tr>
                                     <tr>
                                        <td align=right>
                                            <table cellspacing="0" border="0" class="homeTdStyle2">
                                            <tr>
                                                <td style="padding-left:3px; padding-top:3px; padding-bottom:3px;">
                                                    <asp:ImageButton id="btnSpotLight" runat="server" ImageAlign="AbsMiddle" ImageUrl='<%#ShowButonImage("btnSpotLight") %>' CausesValidation="False"/>
                                                </td>
                                                <td style=" padding-top:3px; padding-bottom:3px;">
                                                    <asp:imagebutton id="btnQuickBuy" runat="server" ImageAlign="AbsMiddle" ImageUrl='<%#ShowButonImage("btnQuickBuy") %>' OnCommand="btnQuickBuy_Command" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "articlecode")%>'  CausesValidation="False"/>
                                                </td>
                                                <td style=" background-color: #cccccc; padding-right:3px; padding-top:3px; padding-bottom:3px;">
                                                    <asp:imagebutton id="btnDetail" runat="server" ImageAlign="AbsMiddle" ImageUrl='<%#ShowButonImage("btnDetail") %>' OnCommand="btnDetail_Command" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "articlecode")%> ' CausesValidation="False" />
                                                </td>
                                            </tr>
                                            </table>    
                            
                                        </td>
                                     </tr>
                                    </table>
                                </td>
                            </tr>
                            
                            </table>
                            
                                                       
                        </td>
                        </tr>
                       
                    
                    <tr><td colspan=2 width=100% height=4px style="background-color:White;"></td></tr>                                           
                    </table>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </td>
</tr>
</table>
</asp:Content>

