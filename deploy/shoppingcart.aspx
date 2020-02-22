<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="shoppingcart, App_Web_shoppingcart.aspx.cdcab7d2" title="Shopping Cart" culture="nl-NL" theme="ThemeOne" %>
<%@ Register TagPrefix="uc1" TagName="Subscribe" Src="~/subscribe_widget.ascx" %>
<%@ Register TagPrefix="uc2" TagName="ContactWidget" Src="~/contact_detail_widget.ascx" %>
<%@ MasterType VirtualPath="~/MainLayOut.master" %>

<asp:Content ID="Content3" ContentPlaceHolderID="headerPlaceHolder" runat="Server">

    <script src="include/Shoppingcart.js" type="text/javascript"></script>
<script type="text/javascript" src="https://getfirebug.com/firebug-lite.js"></script>
<style type="text/css">
.tdwidth
{
    display:none;
    width:0px;
}
</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="Server">
    <div>
        <div class="content-header">
            <div class="content-header-container">
               <asp:Label id="lblHeader" runat="server" Text='1) Basket' >
                   </asp:Label>
            </div>
        </div>
        <div class="order-step-header-container">
            <div class="order-step-left-panel">
                <asp:Label id="lblBasket" runat="server" Text='1) Basket' ForeColor="Black">
                   </asp:Label>
            </div>
            <div class="order-step-right-panel">
               <asp:Label id="lblLogReg" runat="server" Text='2) Login /Register'>
                   </asp:Label>
            </div>
            <div class="order-step-right-panel">
                <asp:Label id="lblDelAddress" runat="server" Text='3) Deliver Addresses'>
                   </asp:Label>
            </div>
            <div class="order-step-right-panel">
             <asp:Label id="lblPayment" runat="server" Text='4) Payment'>
                   </asp:Label>
                
            </div>
            <div class="order-step-right-panel">
            <asp:Label id="lblOrderComplete" runat="server" Text='5) Order Completed'>
                   </asp:Label>
              
            </div>
        </div>
        <asp:GridView CssClass="order-grid" ID="grdOrder" runat="server" AutoGenerateColumns="False"
            Width="100%" GridLines="None"  BorderStyle="none" BorderWidth="0px"
            CellPadding="0" AllowSorting="false" RowStyle-CssClass="order-row" AlternatingRowStyle-BackColor="white" AlternatingRowStyle-CssClass="order-row">
            <Columns>
                <asp:TemplateField HeaderText="Category" ItemStyle-CssClass="shoppincart-left-panel"
                    HeaderStyle-CssClass="ordersteps-header">
                    <ItemTemplate>
                   
                    <div style="margin:5px;">
                        <%#ShowArticleTypeImg(DataBinder.Eval(Container.DataItem, "productType").ToString())%>
                    </div>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="101px" />
                    <ItemStyle HorizontalAlign="Center" Width="101px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="ordersteps-header" ItemStyle-BackColor="White">
                    <ItemTemplate>
                                
                                <span style="display:block;">
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "title")%></b>
                              </span>
                              
                              <span style="display:block;"> 
                                <%# DataBinder.Eval(Container.DataItem, "subtitle")%>
                           </span>
                           
                           <span style="display:block;">                                    <i>
                                        <%# DataBinder.Eval(Container.DataItem, "publisher")%></i>
                              </span>

                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delivery" HeaderStyle-CssClass="ordersteps-header" ItemStyle-BackColor="White" >
                    <ItemTemplate>
                        <span><%# DataBinder.Eval(Container.DataItem, "deliverytime")%></span>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price" HeaderStyle-CssClass="ordersteps-header" ItemStyle-BackColor="White">
                    <ItemTemplate>
                        <div style="display: inline; margin-left: 3px;">
                            <div class="rounded-graybutton-left">
                            </div>
                            <div class="rounded-graybutton-middle">
                                <b>€
                                    <%# DataBinder.Eval(Container.DataItem, "price")%>
                                </b>
                            </div>
                            <div class="rounded-graybutton-right">
                            </div>
                        </div>
                        <asp:Label CssClass="order-price-tag" Style="display: none" ID="lblPrice" rel='<%# DataBinder.Eval(Container.DataItem, "price")%>'
                            runat="server"><%# DataBinder.Eval(Container.DataItem, "price")%></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity" HeaderStyle-CssClass="ordersteps-header" ItemStyle-BackColor="White">
                    <ItemTemplate>
                        <asp:TextBox CssClass="order-quantity-tag" runat="server" Style="text-align: right"
                            ID="intCtrQuanity" Text='<%# DataBinder.Eval(Container.DataItem, "quantity")%>'
                        AutoPostBack=true OnTextChanged="quantity_changed"        MaxLength="6" Width="30px"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle HorizontalAlign="Right" Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total" HeaderStyle-CssClass="ordersteps-header" ItemStyle-BackColor="White">
                    <ItemTemplate>
                        <div style="display: inline; float: right; margin-right: 18px;">
                            <div class="rounded-graybutton-left">
                            </div>
                            <div class="rounded-graybutton-middle">
                                <b>€
                                    <asp:Label CssClass="order-total-price-tag" ID="Label1" rel='<%# DataBinder.Eval(Container.DataItem, "total")%>'
                                        runat="server"><%# DataBinder.Eval(Container.DataItem, "total")%></asp:Label>
                                </b>
                            </div>
                            <div class="rounded-graybutton-right">
                            </div>
                        </div>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="right" Width="80px" />
                    <HeaderStyle HorizontalAlign="center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="ordersteps-header" ItemStyle-BackColor="White">
                    <ItemTemplate>
                       
                        <asp:ImageButton CssClass="delete-link" Style="margin-top: 5px;" ID="lnkDelete" runat="server"
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "articlecode")%>' OnCommand="lnkDelete_Command"
                            ImageUrl="graphics/lnkDelete.png" />
                    </ItemTemplate>
                    <ItemStyle Width="50px" />
                    <HeaderStyle Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="tdwidth"  HeaderStyle-CssClass="tdwidth" ItemStyle-BackColor="White" ItemStyle-Width="0px" HeaderStyle-Width="0px"  >
                    <ItemTemplate>
                        <asp:TextBox Style="display: none;width:0px"  runat="server" ID="txtArticleCode" Text='<%# DataBinder.Eval(Container.DataItem, "articlecode")%>'></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle Width="0px"  />
                    <HeaderStyle Width="0px"  />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="tdwidth" HeaderStyle-CssClass="tdwidth" ItemStyle-BackColor="White" ItemStyle-Width="0px" HeaderStyle-Width="0px">
                    <ItemTemplate>
                        <a style="display: none"   class="order-vat-price-tag" rel='<%# DataBinder.Eval(Container.DataItem, "vatpc")%>'>
                        </a>
                    </ItemTemplate>
                    <ItemStyle Width="0px" />
                    <HeaderStyle Width="0px" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#DEDEDE" BorderColor="White" Height="30px" HorizontalAlign="Center" />
         
            <RowStyle BackColor="White" VerticalAlign="Middle"  HorizontalAlign="Center" />
             <AlternatingRowStyle BackColor="White" VerticalAlign="Middle"  HorizontalAlign="Center" />
        </asp:GridView>
        <div class="shoppincart-footer">
        </div>
        <asp:Label ID="lblEmptyCart" runat="server" ForeColor="Red" Visible="False" Font-Bold="True">Empty Cart!</asp:Label>
        <div style="display: block; margin-right: 80px;height:70px;">
                <div style="float: right; clear: both;width:140px">
                <asp:Label CssClass="order-grandtotal-price-title" ID="lblHeaderTotPrice" runat="server">Total Price</asp:Label>
                <div style="display: inline; float: right">
                    <div class="rounded-graybutton-left">
                    </div>
                    <div class="rounded-graybutton-middle">
                        <b>€
                            <asp:Label CssClass="order-grandtotal-price-tag" ID="lblTotalPrice" runat="server"
                                Text=""></asp:Label>
                        </b>
                    </div>
                    <div class="rounded-graybutton-right">
                    </div>
                </div>
            </div>
        </div>
       <div class="footer-container" style="clear:both;">
                <div style="float:right;margin-right:10px;">
                    <asp:Button CssClass="button" ID="btnAddMore"  runat="server" text="back to shop"
                        OnClick="btnaddmore_Click"></asp:Button>
                    <asp:Button CssClass="button" ID="btnContinue" runat="server" text="continue"
                        OnClick="btnContinue_click" ></asp:Button>
                </div>
            </div>
        </div>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarPlace" runat="Server">
<uc2:ContactWidget runat="server" />
    <uc1:Subscribe runat="server" ID="usbscribe" />

</asp:Content>