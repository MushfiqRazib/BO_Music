<%@ Page Language="C#" MasterPageFile="~/MainLayOut.master" AutoEventWireup="true" CodeFile="order.aspx.cs" Inherits="order" Title="Order Page" %>

<%@ Register Assembly="HawarIT.WebControls" Namespace="HawarIT.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" Runat="Server">
    <table cellpadding="0" cellspacing="0" width="882">
         <tr style="height:12px"></tr>   
         <tr>
           <td colspan="2" style="background-image:url(graphics/bar_order.jpg);height:20px; width: 834px;"></td>  
         </tr>
         <tr>
               <td colspan="2" style="width: 834px">
                 <table cellpadding="0" cellspacing="0" style="background-color:#F0F0F0; width:882px;">
                    <tr>
                       <td valign="top" align="left" style=" padding-right: 5px; padding-left: 15px; padding-top: 5px; width: 291px;"><asp:Label ID="lblCurrentPage" runat="server"  Font-Bold='true' Text="Current page">  </asp:Label>&nbsp;&nbsp;<b>:</b>&nbsp;&nbsp;<asp:Label ID="lblPageRoot" runat="server"   ForeColor="AppWorkspace"  Text="home "></asp:Label>-<asp:Label ID="lblActivePage" runat="server"   ForeColor="#3300ff"  Text="order page "></asp:Label>&nbsp;&nbsp;
                       </td>
                       <td valign="top" align="right" style="padding-right:3px">
                            <table>
                            
                                
                                <tr style="height:58px;">
                                   
                                    <td align="right" ><img src="graphics/step1.gif" /></td>
                                </tr>
                                 
                                
                            </table>
                       </td>       
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 217px;padding-left:5px;padding-right:5px">
                           <table width="100%" cellpadding=0 cellspacing=0>
                              <tr style="height:20px">
                          
                                    <td colspan="2" style="height: 20px"></td>
                             </tr>  
                          
                              <tr>
                                        <td colspan="2">
                                                <asp:GridView ID="grdTest" runat="server" AutoGenerateColumns="False"   Width="100%" GridLines="None" BorderColor="#E0E0E0" BorderStyle="Solid" BorderWidth="3px">
                                                
                                                        <Columns>
                                                         <asp:TemplateField  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"   HeaderText=" &nbsp;&nbsp;Product Type" >
                                                             <ItemTemplate  >
                                                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src="graphics/cd.png" />
                                                             </ItemTemplate>
                                                         </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Product Description">
                                                                <ItemTemplate>
                                                                 <b>Very nice CD</b><br />James Writer
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Price">
                                                            <ItemTemplate>
                                                            &#8364;17,50
                                                            </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity">
                                                            <ItemTemplate>
                                                          <cc1:integercontrol id="intCtrQuanity" runat="server" MaxLength="3" Width="50px"></cc1:integercontrol>
                                                             </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total">
                                                            <ItemTemplate>
                                                            &#8364;17,50
                                                             </ItemTemplate>
                                                            </asp:TemplateField>
                                                               
                                                        </Columns>
                                                       <HeaderStyle BackColor="#DEDEDE" BorderColor="White" Height="30px" HorizontalAlign="Center" />
                                                       <AlternatingRowStyle BackColor="#EFEFEF" />
                                                        <RowStyle BackColor="White" VerticalAlign="Middle" Height="50px" HorizontalAlign="Center" />
                                              </asp:GridView>
                                      </td>
                              
                              </tr>
                              
                              
                              <tr style="height:50px;background-color:#F0F0F0;">
                                <td></td>
                              </tr>
                               <tr  style="background-color:#FFFFFF; height:20px">
                                    <td colspan="2"  align="right" style="padding-top:10px;padding-right:40px">The prices are exclusive  shipping costs  <b>    Total Price :  € 54,20</b></td>
                               </tr>
                              
                              <tr  style="background-color:#FFFFFF;">
                                
                                <td colspan="2" align="right" valign="bottom" style="height: 30px" >
                                    <table  cellpadding="0" cellspacing="0" >
                                    <tr>
                                   <td style="width: 5px;"><img src="graphics/bgBtnRound.png" /></td>
                                            <td align="right" style="padding-top:3px;padding-right:3px;background-color:#CBCBCB;"  >
                                             <asp:imagebutton id="btnaddmore" runat="server" ImageUrl="graphics/addmore.png"></asp:imagebutton>
                                              <asp:imagebutton id="btnupdate" runat="server" ImageUrl="graphics/btn_update.png"></asp:imagebutton>&nbsp;<asp:imagebutton id="btnNext" runat="server" ImageUrl="graphics/btn_next.png"></asp:imagebutton>                                          
                                            </td>
                                    </tr>
                                    </table>    
                                </td>
                              </tr>  
                              <tr style="height:50px">
                              
                              </tr>
                         
                            </table>
                        </td>
                    </tr>  
                    <tr style="height:50px">
                      <td colspan="2"></td>
                    </tr>
                       
                 </table>
               
               
               </td>
       </tr>
     
    
   </table>
</asp:Content>

