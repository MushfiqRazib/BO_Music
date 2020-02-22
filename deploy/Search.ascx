<%@ control language="C#" autoeventwireup="true" inherits="Search, App_Web_search.ascx.cdcab7d2" %>
<link href="include/autoSuggest.css" rel="stylesheet" type="text/css" />
<script src="include/Searchbox.js" type="text/javascript"></script>

<script src="include/jquery.autoSuggest.js" type="text/javascript"></script> 

<style type="text/css">
    .searchkeywords-button
    {
        background: url(graphics/button-bg.png) ;
        color: #2f2f2f;
        cursor: pointer;
        background-repeat:repeat;
    }
</style>

<table  style="margin-top:-4px;">
    <tr>
        <td>
         <asp:Label ID="lblSearch" Text="Search" runat="server" /> 
        </td>
        <td>
        <div style="float:left">
                      <asp:TextBox AutoPostBack="false" ID="txtSearch" title="keywords.." runat="server" Text="" Width="165px" Height="14px"  CssClass="TxtboxHeight searchkeywords-text"></asp:TextBox>
                   </div>
        </td>
        
        <td>
        
        
         <asp:Button ID="btnGo" Text="Search" runat="server" Height="23px"
                            OnClick="btnGo_Click" CssClass="searchkeywords-button" CausesValidation="False"></asp:Button>
                 
        </td>
    </tr>
</table>
