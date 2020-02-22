<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="home, Bo02" title="Home" validaterequest="false" theme="ThemeOne" %>

<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MainLayOut.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script language="javascript">
    
    function GetURL()
    {
       var str=location.href;
        str=str.substring(7,location.href.length)
        var vars = str.split("/"); 
        for (var i=0;i<vars.length;i++)

         for (var i=0;i<vars.length;i++)
        {
            var pageName=vars[i];
            
            if(pageName.toLowerCase()=="home.aspx")
            {
                
                document.getElementById("divAdvSearch").style.visibility="hidden";
            }
        }
        
       
    }
    //onLoad=GetURL();
        
    </script>--%>
    <table align="center" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td colspan="2">
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td valign="top">
<%--                            <table border="0" cellpadding="0" cellspacing="0" width="562">
                                <tr>
                                    <td valign="top" style="height: 100%; text-align: left;">
                                        <asp:ImageButton ImageAlign="AbsMiddle" ID="imgChristmas" runat="server" OnClick="imgChristmas_Click" CausesValidation="False"></asp:ImageButton>
                                    </td>
                                </tr>
                            </table>
--%>                            <%-- Spotlight section Start--%>
                            <table border="0" cellpadding="0" cellspacing="0" width="562">
                                <tr>
                                    <td id="headerSoptLight" runat="server" class="homeTdStyle1">
                                    </td>
                                </tr>
                                <tr style="background-color: #E6E6E6;">
                                    <td valign="top" style="height: 100%; text-align: left;">
                                        <table border="0" style="height: 100%" cellpadding="0" cellspacing="0" id="TABLE1">
                                            <tr style="height: 11px;">
                                            </tr>
                                            <tr style="height: 262px;">
                                                <td style="width: 7px; height: 200px;">
                                                </td>
                                                <td style="width: 180px; height: 200px;" valign="top">
                                                    <asp:Image ID="imgSpotlight" runat="server" /></td>
                                                <td valign="top" style="height: 200px">
                                                    <table cellspacing="0" border="0" cellpadding="10">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTitle" runat="server" Font-Bold="True"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblSubTitle" runat="server"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblComposer" runat="server" />
                                                            </td>
                                                        </tr>                                                        
                                                        <tr>
                                                            <td>
                                                                <p align="justify">
                                                                    <asp:Literal ID="lblDescription" runat="server"></asp:Literal><%--<asp:Label ID="lblDescription" runat="server"></asp:Label>--%></p>
                                                            </td>
                                                        </tr>
                                                        <%--<tr>
                                                            <td>
                                                                <asp:Label ID="lblID" runat="server"></asp:Label></td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPrice" runat="server"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="background-color: #E6E6E6;">
                                    <td>
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" valign="bottom">
                                                    <img src="graphics/corner.png" /></td>
                                                <td align="right">
                                                    <table cellspacing="0" border="0" class="homeTdStyle2">
                                                        <tr>
                                                            <td style="width: 1">
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ImageAlign="AbsMiddle" ID="btnSpotlight" runat="server" ImageUrl="graphics/btn_spotlight.png"
                                                                    OnClick="btnSpotlight_Click" CausesValidation="False"></asp:ImageButton>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ImageAlign="AbsMiddle" ID="btnQuickBuy" runat="server" ImageUrl="graphics/btn_quickbuy.png"
                                                                    OnClick="btnQuickBuy_Click" CausesValidation="False"></asp:ImageButton>
                                                            </td>
                                                            <td valign="middle" style="height: 23px; background-color: #cccccc">
                                                                <asp:ImageButton ImageAlign="AbsMiddle" ID="btnDetail" runat="server" ImageUrl="graphics/btn_details.png"
                                                                    OnClick="btnDetail_Click" CommandArgument="0" CausesValidation="False"></asp:ImageButton>
                                                            </td>
                                                            <td style="width: 2">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="height: 12px">
                                    <td style="width: 509px; height: 12px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td id="headerNews" runat="server" class="homeTdStyle3">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="grdNews" runat="server" ShowHeader="false" BorderWidth="0" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <table cellpadding="3" cellspacing="0" border="0">
                                                            <tr style="background-color: #ffffff;">
                                                                <td style="text-align: justify;">
                                                                  <b>  
                                                                    <%# DataBinder.Eval(Container.DataItem, "subject")%></b>
                                                                <%--</td>
                                                                <td align="right">--%><span style="color:Gray; font-size:smaller">
                                                                  &nbsp; |&nbsp; <%# DataBinder.Eval(Container.DataItem, "date")%></span></td>
                                                            </tr>
                                                            <tr style="background-color: #ffffff;">
                                                                <td style="text-align: justify" colspan="2">
                                                                    <%# DataBinder.Eval(Container.DataItem, "description")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            <%-- Spotlight section End--%>
                        </td>
                        <td style="width: 7px;">
                        </td>
                        <td valign="top">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td id="headerSearch" runat="server" class="homeTdStyle4">
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <uc1:Search ID="Search1" runat="server" />
                                    </td>
                                </tr>
                                <tr style="height: 8px;">
                                </tr>
                                <tr>
                                    <td id="headerContactInfo" runat="server" class="homeTdStyle5">
                                    </td>
                                </tr>
                                <tr valign="top" style="background-color: #E6E6E6;">
                                    <td style="text-align: left;">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <img src="graphics/house.gif" /></td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top" style="padding-left: 5px;">
                                                    <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <strong>Boeijenga Music</strong></td>
                                                            <td class="homeTdStyle7" height="25" style="width: 90px">
                                                                <center>
                                                                    <asp:ImageButton ID="btnContact" runat="server" ImageUrl="~/graphics/btn_contact_en.gif"
                                                                        CausesValidation="False" OnClick="btnContact_Click" /></center>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Hoofdweg 156</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                9341 BM Veenhuizen</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                The Netherlands</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                Phone: +31 (0) 592-304142 
                                                                <asp:Label ID="lblAvailable" runat="server"/></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Fax: +31 (0) 592-304143</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-top:5px">
                                                                <asp:Label ID="lblOpeninghours" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <img style="border: 0px;" src="graphics/corner.png" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="height: 8px;">
                                </tr>
                                <tr>
                                    <td class="homeTdStyle6">
                                    </td>
                                </tr>
                                <tr valign="top" style="background-color: #E6E6E6;">
                                    <td valign="top" align="left">
                                        <table cellpadding="4" cellspacing="0" border="0">
                                            <tr>
                                                <td align="left">
                                                    <div>
                                                        <div style="float:left; height:20px; vertical-align:middle; padding-top:2px">
                                                            <asp:Label ID="lblEmail" runat="server" Text="E-mail"></asp:Label>:
                                                        </div>
                                                        <div style="padding-left:4px; padding-right:4px; float:left">
                                                            <asp:TextBox ID="txtMail" runat="server" Text="" CausesValidation="True" MaxLength="40"></asp:TextBox>
                                                        </div>                        
                                                        <div style="padding-left:4px">
                                                            <asp:ImageButton ID="btnsubscribe" runat="server" ImageUrl="graphics/btn_subscribe.png"
                                                                OnClick="btnsubscribe_Click"></asp:ImageButton>    
                                                        </div>
                                                    </div>                                                                                
                                                </td>
                                                <td align="center" valign="top">
                                                </td>
                                                <td align="left" valign="top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="left" valign="top">
                                                    <font color="red">
                                                        
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMail"
                                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMail"
                                                                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </font>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" valign="middle" align="left">                                                    
                                                        <font color="red">
                                                            <asp:Label ID="labelMailMessage" runat="server"></asp:Label></font>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="left">
                                                    Wilt u op de hoogte worden gehouden van nieuwe uitgaven en activiteiten?<br />
                                                    Laat dan hier uw emailadres achter!</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="background-color: #E6E6E6;">
                                    <td align="left">
                                        <img style="border: 0px;" src="graphics/corner.png" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <tr>
                <%--<td colspan="2" align="right"><asp:Label ID="lblHitCount" runat="server" Font-Bold="True"></asp:Label></td>--%>
                </tr>
            </td>
        </tr>
    </table>
    
</asp:Content>
