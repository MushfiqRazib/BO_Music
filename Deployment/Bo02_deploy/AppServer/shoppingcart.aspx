<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="shoppingcart, Bo02" title="Shopping Cart" theme="ThemeOne" %>

<%@ Register Assembly="HawarIT.WebControls" Namespace="HawarIT.WebControls" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MainLayOut.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type='text/javascript'>
    var loader;
    function SubmitForm()
    {
       // document.forms[0].submit();     
       document.location.replace(document.location);
    }
    function setCookie(value)
    {   
        document.cookie = "loader="+value.toString();    
        //alert("cookie is : "+getCookie("loader"));
        return true;
    }
    
    function getCookie(c_name)
    {
    if (document.cookie.length>0)
      {
      c_start=document.cookie.indexOf(c_name + "=");
      if (c_start!=-1)
        { 
        c_start=c_start + c_name.length+1; 
        c_end=document.cookie.indexOf(";",c_start);
        if (c_end==-1) c_end=document.cookie.length;
        return unescape(document.cookie.substring(c_start,c_end));
        } 
      }
    return "";
    }

window.onload = function test()
{
   //debugger;
   loader = getCookie("loader");
   
   if(loader!=null && loader!="" &&loader!="false")
    {
        SubmitForm();
        setCookie('false');
    }

}

    </script>

    <table cellpadding="0" cellspacing="0" width="100%" border="0">
        <tr>
            <td colspan="2" runat="server" id="header" style="height: 19px; width: 509px;">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 834px">
                <table cellpadding="0" cellspacing="0" class="contentArea">
                    <tr>
                        <td valign="top" align="left" class="shoppingCartTDStyle2">
                            <%--<asp:Label ID="lblCurrentPage" runat="server" Font-Bold='true' Text="Current page">  </asp:Label>
                            <asp:Label ID="lblPageRoot" runat="server" ForeColor="AppWorkspace" Text=""></asp:Label>
                            <asp:Label ID="lblActivePage" runat="server" ForeColor="#3300ff" Text=""></asp:Label>--%>
                        </td>
                        <td valign="baseline" align="right" style="padding-right: 3px">
                            <table>
                                <tr>
                                    <td align="right" valign="bottom">
                                        <img src="graphics/step1.gif" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="shoppingCartTDStyle1">
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td colspan="2">
                                        <asp:GridView ID="grdOrder" runat="server" AutoGenerateColumns="False" Width="100%"
                                            GridLines="None" BorderColor="#E0E0E0" BorderStyle="Solid" BorderWidth="0px" CellPadding="10"
                                            AllowSorting="True" OnRowDataBound="grdOrder_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Product Type">
                                                    <ItemTemplate>
                                                        <%#ShowArticleTypeImg(DataBinder.Eval(Container.DataItem, "productType").ToString())%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product Description">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>                                                                    
                                                                    <b><%# DataBinder.Eval(Container.DataItem, "title")%></b>                                                                    
                                                                </td>
                                                            </tr>
                                                                <td>
                                                                    <%# DataBinder.Eval(Container.DataItem, "subtitle")%>
                                                                </td>
                                                            <tr>
                                                                <td>
                                                                    <i><%# DataBinder.Eval(Container.DataItem, "publisher")%></i>
                                                                </td>
                                                            </tr>                                                                
                                                            
                                                        </table>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="350px" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="350px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price">
                                                    <ItemTemplate>
                                                        &#8364;
                                                        <asp:Label ID="lblPrice" runat="server"><%# DataBinder.Eval(Container.DataItem, "price")%></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" Style="text-align: right" ID="intCtrQuanity" Text='<%# DataBinder.Eval(Container.DataItem, "quantity")%>'
                                                            MaxLength="6" Width="50px" onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789')"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total">
                                                    <ItemTemplate>
                                                        &#8364;
                                                        <asp:Label ID="lblTotPrice" runat="server"><%# DataBinder.Eval(Container.DataItem, "total")%></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="130px" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="130px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "articlecode")%>'
                                                            OnCommand="lnkDelete_Command">Delete</asp:LinkButton>--%>
                                                        <asp:ImageButton ID="lnkDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "articlecode")%>' OnCommand="lnkDelete_Command"
                                                         ImageUrl="graphics/lnkDelete.png"/>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px" />
                                                    <HeaderStyle Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtArticleCode" Text='<%# DataBinder.Eval(Container.DataItem, "articlecode")%>'
                                                            runat="server" Visible="False"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5px" />
                                                    <HeaderStyle Width="5px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#DEDEDE" BorderColor="White" Height="30px" HorizontalAlign="Center" />
                                            <AlternatingRowStyle BackColor="#EFEFEF" VerticalAlign="middle" HorizontalAlign="center" />
                                            <RowStyle BackColor="White" VerticalAlign="Middle" Height="50px" HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="background-color: #DEDEDE;">
                                    <td>
                                        <asp:Label ID="lblEmptyCart" runat="server" ForeColor="Red" Visible="False" Font-Bold="True">Empty Cart!</asp:Label></td>
                                    <td colspan="1" align="right" style="padding-top: 10px; padding-right: 65px">
                                        <b>
                                            <asp:Label ID="lblHeaderTotPrice" runat="server">Total Price</asp:Label>
                                            : €
                                            <asp:Label ID="lblTotalPrice" runat="server" Text=""></asp:Label></b><br />
                                        <span style="font-size: 10pt; font-family: Arial">
                                            <asp:Label ID="lblShipping" runat="server" Text="Label"></asp:Label></span><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right" valign="bottom" style="height: 30px">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="background-color: #e6e6e6;" align="right">
                                                    <table cellspacing="0" border="0" style="height: 23px;" class="confirmTDStyle5">
                                                        <tr>                                                            
                                                            <td style="padding-left:3px; padding-top:3px; padding-bottom:3px;">
                                                                <asp:ImageButton ID="btnAddMore" ImageAlign="AbsMiddle" runat="server" ImageUrl="graphics/btn_addmore_en.png"
                                                                    OnClick="btnaddmore_Click"></asp:ImageButton>
                                                            </td>
                                                            <td style=" background-color: #cccccc; padding-right:3px; padding-top:3px; padding-bottom:3px;">
                                                                <asp:ImageButton ID="btnNext" ImageAlign="AbsMiddle" runat="server" ImageUrl="graphics/btn_next_nl.png"
                                                                    OnClick="btnNext_Click" OnClientClick="return setCookie('true')"></asp:ImageButton>
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
                    <tr style="height: 0px">
                        <td colspan="2">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
