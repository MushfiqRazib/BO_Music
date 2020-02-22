<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="contact, Bo02" title="Contact" validaterequest="false" theme="ThemeOne" %>

<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MainLayOut.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

function StopPropogation(e, txtQuestion) {
////alert ("This is a Javascript Alert")
//    if(e.keyCode == "13") {
//        txtQuestion.value += '\n ';
//        return false;
//    }
//    return true;
   var characterCode
   //literal character code will be stored in this variable
   
   if(e && e.which)
   {
    //if which property of event object is supported (NN4)
    e = e
    characterCode = e.which 
    //character code is contained in NN4's which property
   }
   else
   {
    e = event
    characterCode = e.keyCode
    //character code is contained in IE's keyCode property
   }
   if(characterCode == 13)
    { //if generated character code is equal to ascii 13 (if enter key)
        //submit the form
        //alert("enterclick");
        txtQuestion.value += '\n ';
        //valid(info);
        return false 
    }
    else
    {
    return true 
    }

}
    </script>

    <table cellpadding="0" cellspacing="0" border="0" bordercolor="red" width="882">
        <tr>
            <td class="contactTdStyle1">
            </td>
            <td width="5" style="height: 460px">
            </td>
            <td width="564" valign="top" style="height: 460px">
                <table cellpadding="0" cellspacing="0" border="0" style="width: 564px">
                    <tr style="width: 564px;">
                        <td id="headerContactForm" runat="server" class="contactTdStyle2">
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <table cellpadding="0" cellspacing="3" border="0" class="contactTdStyle3">
                                <tr>
                                </tr>
                                <tr>
                                    <td class="contactTdStyle4" align="left" width="110">
                                        <asp:Label ID="lblSubject" runat="server" Text="Subject"></asp:Label></td>
                                    <td align="left" style="width: 270px">
                                        <asp:DropDownList ID="ddlSubject" runat="server">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="contactTdStyle4" align="left" width="110">
                                        <asp:Label ID="lblTitle" runat="server" Text="Title:"></asp:Label></td>
                                    <td align="left" style="width: 270px">
                                        <asp:DropDownList ID="ddlInitialName" runat="server">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="contactTdStyle4" align="left" width="110">
                                        <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label></td>
                                    <td style="width: 270px;" align="left">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td style="width: 130px;">
                                                    <asp:TextBox ID="txtName" runat="server" Width="145px" TabIndex="1"></asp:TextBox></td>
                                                <td style="width: 10px;" align="left">
                                                    *</td>
                                                <td style="width: 95px;">
                                                    <asp:RequiredFieldValidator ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Name required"
                                                        ID="validatorName" runat="server" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td colspan="2" align="left" width="170" style="">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                            <tr>
                                                <td width="170">
                                                    <asp:Label ID="lblRequiredMessage" runat="server" Text="The fields with * are required"
                                                        Font-Size="Small" Font-Italic="True" Width="170px"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="contactTdStyle4" align="left" width="110">
                                        <asp:Label ID="lblCompany" runat="server" Text="Company Name"></asp:Label></td>
                                    <td style="width: 270px;" align="left">
                                        <asp:TextBox ID="txtCompany" runat="server" Width="145px" TabIndex="2"></asp:TextBox></td>
                                    <td style=" width: 100px;">
                                    </td>
                                    <td width="92" align="center">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="contactTdStyle4" align="left" width="110" >
                                        <asp:Label ID="lblAddress" runat="server" Text="Address:"></asp:Label></td>
                                    <td style=" width: 270px;" align="left">
                                        <asp:TextBox ID="txtAddress" runat="server" Width="145px" TabIndex="2"></asp:TextBox></td>
                                    <td style=" width: 100px;">
                                    </td>
                                    <td width="92" align="center" >
                                    </td>
                                </tr>
                                <tr>
                                    <td class="contactTdStyle4" align="left" width="110">
                                        <asp:Label ID="lblPostalCode" runat="server" Text="Postal code:"></asp:Label></td>
                                    <td style="width: 270px;" align="left">
                                        <asp:TextBox ID="txtPostCode" runat="server" Width="145px" TabIndex="3"></asp:TextBox></td>
                                    <td style="width: 100px">
                                    </td>
                                    <td width="92" align="center">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="contactTdStyle4" align="left" width="110">
                                        <asp:Label ID="lblCity" runat="server" Text="City:"></asp:Label></td>
                                    <td style="width: 270px;" align="left">
                                        <asp:TextBox ID="txtCity" runat="server" Width="145px" TabIndex="4"></asp:TextBox></td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="contactTdStyle4" align="left" width="110">
                                        <asp:Label ID="lblCountry" runat="server" Text="Country:"></asp:Label></td>
                                    <td style="width: 270px;" align="left">
                                        <asp:DropDownList ID="ddlCountry" runat="server" Width="150px" TabIndex="5">
                                        </asp:DropDownList></td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="contactTdStyle4" align="left" width="110">
                                        <asp:Label ID="lblEmail" runat="server" Text="E-mail Address:"></asp:Label></td>
                                    <td align="left" colspan="2" style="">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                            <tr>
                                                <td style="width: 150px" align="left">
                                                    <asp:TextBox ID="txtEmail" runat="server" Width="145px" TabIndex="6"></asp:TextBox></td>
                                                <td style="width: 10px" align="left">
                                                    *</td>
                                                <td style="width: 190px">
                                                    <asp:RequiredFieldValidator ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="E-mail address Required"
                                                        ID="validatorEmail" runat="server" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="validatorEmailReg" runat="server" ControlToValidate="txtEmail"
                                                        Display="Dynamic" ErrorMessage="Not valid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="92" align="center">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="contactTdStyle4" align="left" width="110">
                                        <asp:Label ID="lblPhone" runat="server" Text="Phone number:"></asp:Label></td>
                                    <td style="" align="left" colspan="2">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                            <tr>
                                                <td style="width: 145px">
                                                    <asp:TextBox ID="txtPhone" runat="server" Width="145px" onfocus="javascript: SetLatestValue('')"
                                                        onchange="return CheckCorrectValue(this,'+-0123456789')" onkeypress="return CheckNumericKeyStroke(event,this.value,'+-0123456789')"
                                                        TabIndex="7"></asp:TextBox></td>
                                                <td style="width: 10px" align="left">
                                                    *</td>
                                                <td style="width: 185px">
                                                    <asp:RequiredFieldValidator ControlToValidate="txtPhone" Display="Dynamic" ErrorMessage="Phone number required"
                                                        ID="validatorPhone" runat="server" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="92" align="center">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="contactTdStyle4" align="left" valign="top" width="110">
                                        <asp:Label ID="lblQuestion" runat="server" Text="Question:"></asp:Label></td>
                                    <td colspan="2" valign="top" style="">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                            <tr style="padding-top: 0px">
                                                <td style="width: 190px; height: 103px;">
                                                    <%--<textarea id="txtQuestion" cols="25" runat="server" rows="6"></textarea>--%>
                                                    <asp:TextBox ID="txtQuestion" runat="server" Rows="6" TextMode="MultiLine" Columns="25"
                                                        Height="95px" Width="190px" TabIndex="8" onKeyDown="if(event.keyCode==13)return StopPropogation(event,this)"> </asp:TextBox></td>
                                                <td style="width: 10px; height: 103px;" align="left" valign="top">
                                                    *</td>
                                                <td style="width: 140px; height: 103px;" align="left" valign="top">
                                                    <asp:RequiredFieldValidator ID="validatorQuestion" runat="server" ControlToValidate="txtQuestion"
                                                        Display="Dynamic" ErrorMessage="Question required" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="92" align="center" valign="bottom">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 100%">
                                            <tr>
                                                <td style="height: 30px; width: 90px;" align="center">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" align="center" style="width: 90px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 25px; width: 90px;" align="center">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="contactTdStyle5" style="padding-top:3px; padding-bottom:3px;" align="center">
                                                    <asp:ImageButton ID="btnSend" ImageAlign="AbsMiddle" runat="server" ImageUrl="~/graphics/btn_Confirm_en.png"
                                                        OnClick="btnSend_Click" TabIndex="9" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 564px; height: 5px">
                        </td>
                    </tr>
                    <tr>
                        <td id="headerContactInfor" runat="server" class="contactTdStyle6">
                        </td>
                    </tr>
                    <tr align="center">
                        <td valign="top" style="height: 144px">
                            <table cellpadding="0" cellspacing="0" border="0" class="contactTdStyle7">
                                <tr>
                                    <td height="9">
                                    </td>
                                    <td align="right" height="9">
                                    </td>
                                    <td height="9" style="width: 9px">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 40%; padding-left: 10px">
                                        <table style="text-align: left;" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <strong>Boeijenga Music</strong></td>
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
                                                <td>
                                                    Phone: +31 (0) 592-304142</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Fax: +31 (0) 592-304143</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 30%" class="contactTdStyle9" align="right">
                                    </td>
                                    <td style="width: 30%" valign="top" align="left">
                                        <table>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Label ID="lblOW" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 15px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Text="PDF"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="btnPreviewPdf" runat="server" Style="padding-right: 10px" OnClick="btnPreviewPdf_Click"
                                                        ImageUrl="~/graphics/btn_Download_en.jpg" CausesValidation="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text="Googlemaps"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="btnLinkOW" runat="server" Style="padding-right: 10px" ImageUrl="~/graphics/btn_Link_en.jpg"
                                                        CausesValidation="False" OnClick="btnLinkOW_Click" />
                                                    <%--<a href="http://maps.google.com/maps?f=d&hl=nl&geocode=&time=&date=&ttype=&saddr=Oosterwolde,+N381&daddr=Veenhuizen,+Hoofdweg+158&sll=37.0625,-95.677068&sspn=49.891082,82.265625&ie=UTF8&z=13&om=1">
                                                        Oosterwolde</a>--%>
                                                </td>
                                            </tr>
                                            <tr style="height: 2px">
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 9px">
                                    </td>
                                </tr>
                                <tr style="height: 15px">
                                </tr>
                                <tr>
                                    <td align="left" style="width: 40%; padding-left: 10px">
                                    </td>
                                    <td style="width: 30%" class="contactTdStyle8" align="right">
                                    </td>
                                    <td style="width: 30%" valign="top" align="left">
                                        <table>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Label ID="lblAssen" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 15px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="PDF"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="ImageButton2" runat="server" Style="padding-right: 10px" ImageUrl="~/graphics/btn_Download_en.jpg"
                                                        CausesValidation="False" OnClick="btnAssen_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="Googlemaps"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="btnLinkAss" runat="server" Style="padding-right: 10px" ImageUrl="~/graphics/btn_Link_en.jpg"
                                                        CausesValidation="False" OnClick="btnLinkAss_Click" />
                                                    <%--<a href="http://maps.google.com/maps?f=d&hl=nl&geocode=&time=&date=&ttype=&saddr=Assen,+a28&daddr=Veenhuizen,+Hoofdweg+158&sll=53006945,6.335835&sspn=0.07468,0.160675&ie=UTF8&ll=53.012201,6.458588&spn=0.074671,0.160675&z=13&om=1">
                                                        Assen</a>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 9px">
                                    </td>
                                </tr>
                                <tr>
                                    <td height="9" width="413">
                                    </td>
                                    <td align="right" height="9">
                                    </td>
                                    <td height="9" style="width: 9px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%--<tr height="100">
        </tr>--%>
    </table>
</asp:Content>
