<%@ Page Language="C#" MasterPageFile="~/MainLayOut.master" AutoEventWireup="true"
    CodeFile="confirm.aspx.cs" Inherits="cofirm" Title="Confirm Page" %>

<%@ Register TagPrefix="uc1" TagName="Subscribe" Src="~/subscribe_widget.ascx" %>
<%@ Register TagPrefix="uc2" TagName="ContactWidget" Src="~/contact_detail_widget.ascx" %>
<%@ MasterType VirtualPath="~/MainLayOut.master" %>


<asp:Content ID="Content3" ContentPlaceHolderID="headerPlaceHolder" runat="Server">



    <script src="include/confirm.js" type="text/javascript"></script>
    <style type="text/css">
.tdwidth
{
    display:none;
    width:0px;
}
</style>
</asp:Content>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="Server">
   <div class="content-header">
            <div class="content-header-container">
               <asp:Label id="lblHeader" runat="server" Text='1) Basket'>
                   </asp:Label>
            </div>
        </div>
        <div class="order-step-header-container">
            <div class="order-step-right-panel" style="width:101px">
                <asp:Label id="lblBasket" runat="server" Text='1) Basket'>
                   </asp:Label>
            </div>
            <div class="order-step-right-panel">
               <asp:Label id="lblLogReg" runat="server" Text='2) Login /Register'>
                   </asp:Label>
            </div>
            <div class="order-step-right-panel">
                <asp:Label id="lblDelAddress" runat="server" Text='3) Deliver Addresses' >
                   </asp:Label>
            </div>
            <div class="order-step-left-panel" style="width:140px">
             <asp:Label id="lblPayment" runat="server" Text='4) Payment' ForeColor="Black">
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
                <asp:TemplateField HeaderStyle-CssClass="tdwidth" ItemStyle-CssClass="tdwidth" ItemStyle-BackColor="White" ItemStyle-Width="0px" HeaderStyle-Width="0px">
                    <ItemTemplate>
                        <asp:TextBox Style="display: none" runat="server" ID="txtArticleCode" Text='<%# DataBinder.Eval(Container.DataItem, "articlecode")%>'></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle Width="0px" />
                    <HeaderStyle Width="0px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="tdwidth" ItemStyle-CssClass="tdwidth" ItemStyle-BackColor="White" ItemStyle-Width="0px" HeaderStyle-Width="0px">
                    <ItemTemplate>
                        <a style="display: none" class="order-vat-price-tag" rel='<%# DataBinder.Eval(Container.DataItem, "vatpc")%>'>
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

    <div class="order-row table-content" style="height: 50px">
        <div style="float: right; line-height: 20px; margin-right:65px;width:180px;">
            <asp:Label ID="lblSubTotalText" runat="server" Text="Label"></asp:Label>:
            <div style="display: inline; float: right; margin-right: 18px;">
                <div class="rounded-graybutton-left">
                </div>
                <div class="rounded-graybutton-middle">
                    <asp:Label ID="lblSubTotal" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="rounded-graybutton-right">
                </div>
            </div>
        </div>
    </div>
    <div class="shoppincart-footer">
    </div>
    <table class="order-row table-content" style="background-color:#ececec !important;width:100%;float:left;clear:left;" border="0" cellspacing="2" cellpadding="5">
        <tr>
              <td align="right" style="padding-right: 5px; width: 250px;">
                <asp:Label ID="lblShippingCostText" runat="server" Text=""></asp:Label>
                :
            </td>
            <td align="right" style="width: 50px;margin-right:200px;">
                <div style="display: inline; float: left; margin-right: 18px;">
                    <div class="rounded-button-left">
                    </div>
                    <div class="rounded-button-middle">
                        <asp:Label ID="lblShippingCost" runat="server" Text="2,93" Font-Underline="False"></asp:Label>
                    </div>
                    <div class="rounded-button-right">
                    </div>
                </div>
            </td>
           
        </tr>
        <tr>
            <td align="right" style="">
                <asp:Label ID="lblVatText" runat="server" Text="Label"></asp:Label>
                :
            </td>
            <td style="">
                <div style="display: inline; float: left; margin-right: 18px;">
                    <div class="rounded-button-left">
                    </div>
                    <div class="rounded-button-middle">
                        <asp:Label ID="lblVat" runat="server" Text="Label" Font-Underline="False"></asp:Label>
                    </div>
                    <div class="rounded-button-right">
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td align="right" style="padding-right: 5px;">
                <b>
                    <asp:Label ID="lblTotalText" runat="server" Text="Label"></asp:Label>
                    :</b>
            </td>
            <td style=" border-top: 1px solid black;">
                <div style="display: inline; float: left; margin-right: 18px;">
                    <div class="rounded-button-left">
                    </div>
                    <div class="rounded-button-middle">
                        <asp:Label ID="lblTotal" Font-Bold="true" runat="server" Text="Label">
                        </asp:Label>
                    </div>
                    <div class="rounded-button-right">
                    </div>
                </div>
            </td>
        </tr>
       
    </table>
    <div class="footer-line">
    </div>
    
    
    
    <div style="display:block;height:150px;margin-left:100px">
    
    
    <table class=" table-content" border="0" cellpadding="2" cellspacing="0" align="left">
        <tr>
            <td align="left">
                <b>
                    <asp:Label ID="lblInvoiceAddressHeader" runat="server" Text="Label"></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblAddress" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblHouseNum" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblPostCode" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblResidence" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblCountry" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>
    <table class="table-content" style="margin-left:50px" border="0" cellpadding="2" cellspacing="0" align="left">
        <tr>
            <td align="left">
                <b>
                    <asp:Label ID="lblDelAdd" runat="server" Text="Label"></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDName" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDAddress" runat="server" Text="Label"></asp:Label>&nbsp;
                <asp:Label ID="lblDHouseNum" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDPostCode" runat="server" Text="Label"></asp:Label>&nbsp;
                <asp:Label ID="lblDResidence" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDCountry" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
         <tr>
            <td>
                <asp:Button ID="btnChangeAdress" OnClick="changeAddress_Click" CssClass="button" runat="server" Text="Change"></asp:Button>
            </td>
        </tr>
    </table>
    
    
    </div>
    
     <div style="display:block;height:50px;margin-left:100px;float:left;clear:left;">
    
    
    <asp:Label ID="lblPaymentType" runat="server" Text="Label" Font-Bold="true" Height="18px"></asp:Label>
    <asp:DropDownList ID="paymentTypeList" runat="server">
    </asp:DropDownList>
    <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" Text="Pay"
        CssClass="button confirm-button" />
        </div>
        <div style="display:block;height:50px;margin-left:100px;float:left;clear:left;">
        
        <asp:Label ID="lblTransection" runat="server" Text="" Font-Bold="true" ForeColor="red"></asp:Label>&nbsp;
    
        
        </div>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="SidebarPlace" runat="Server">
<div class="content-sidebar-header">
        <div class="sidebar-container">
            <label id="Label2"  runat="server">
                 <%= (string)base.GetGlobalResourceObject("string", "myAccount")%></label>
        </div>
    </div>
    
      <div class="sidebar-content-body" style="height: auto;">
      <div style="display:block">
         <a href="register.aspx" style="color: #2F2F2F  !important"> &raquo; <%= (string)base.GetGlobalResourceObject("string", "Profile")%> </a>
       </div>
       <div style="display:block">
          <a href="delivery.aspx" style="color: #2F2F2F  !important"> &raquo; <%= (string)base.GetGlobalResourceObject("string", "stepDelivaery")%> </a>
     </div>
    </div>
    
    
    <uc2:ContactWidget ID="ContactWidget1" runat=server />
    <uc1:Subscribe ID="Subscribe1" runat=server />

</asp:Content>