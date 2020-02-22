<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="advancesearch, Bo02" title="Advance Serach" validaterequest="false" theme="ThemeOne" %>
<%@ MasterType  virtualPath="~/MainLayOut.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table cellpadding=0 cellspacing=0 border=0 class="contentArea" align=left >
<tr ><td runat=server id="header"  align=left valign=top style="height:20px; width: 834px;"></td></tr>
<tr >
    <td  valign=top align=left>
        <table cellpadding="0" cellspacing="0" style="width:882px">
            <tr >
                <td valign="top" align="left"  class="pageLocation"><asp:Label ID="lblCurrentPage" runat="server"  Font-Bold='true' Text="">  </asp:Label>
                        &nbsp;&nbsp;<b>:</b>&nbsp;&nbsp;
                        <asp:Label ID="lblPageRoot" runat="server"   ForeColor="AppWorkspace"  Text=" "></asp:Label>
                        <asp:Label ID="lblActivePage" runat="server"   ForeColor="#3300ff"  Text=" ">
                        </asp:Label>
                </td>
            </tr> 
         </table>
    </td>
</tr>
<tr ><td style="height:5px;"></td></tr>
<tr ><td  align=left  class="advanceSearchText">
    <asp:Label ID="lblSearchText" runat="server"  Font-Size=16px  Font-Bold=true ForeColor=white Text="Advanced search / "></asp:Label>
</td></tr>
<tr >
    <td align=left valign=top style="background-color:#E6E6E6;">
        <table cellpadding =5 cellspacing=0 border=0 align=left>
            <tr>
                <td  align=left valign=bottom width=19px></td>
                <td>
                    <asp:Label ID="lblCategory" runat="server" Text="Category" Font-Bold=true></asp:Label>
                </td>
                <td >
                    <asp:DropDownList ID="ddlCategory" runat="server"  CssClass="ComboHeight" AutoPostBack=true OnTextChanged="ddlCategory_SelectedTextChanged"   >
                        <asp:ListItem Text="All" Value="a"></asp:ListItem>
                        <asp:ListItem Text="Book" Value="b"></asp:ListItem>
                        <asp:ListItem Text="CD/DVD" Value="c"></asp:ListItem>
                        <asp:ListItem Text="Sheetmusic" Value="s"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width=20px></td>
                <td align=left valign=top>   
                    <asp:Label ID="lblSearch" runat="server" Text="Search" Font-Bold=true></asp:Label>
                </td>
              
                <td align=left  valign=top>
                    <asp:TextBox ID="txtSearch"  runat="server"></asp:TextBox>
                </td>
                <td align=left valign=top  style=" padding-top:6px;">
                    <asp:ImageButton ID="imbGo"  runat="server" Height=21px ImageUrl="graphics/btn_Go_nl.png" OnClick="imbGo_Click" />
                </td>
            </tr>
        </table>
    </td>
</tr>
<tr><td align="right" style="height: 15px; width: 883px;">
<asp:Label ID="lblSelDeSelAll" runat="server" Text='<a href="javascript:SelectDeselectAll()">Select / Deselect all</a>'>
</asp:Label>
</td></tr>
<tr >
      <td width=100% align=left>
        <table id="tblCriteriaMain" cellpadding=0 cellspacing=0 border=0 width=100% align=left>
        <!-- Load Criteria List for Sheet Music -->
        <%--<tr>
            <td>
                <asp:CheckBoxList ID="Test" runat=server>
                    <asp:ListItem Text="Test1" Value="Test1"></asp:ListItem>
                    <asp:ListItem Text="Test2" Value="Test2"></asp:ListItem>
                    <asp:ListItem Text="Test3" Value="Test3"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>--%>
        <tr>
            <td>
                <div id="divSheetMusic" runat=server>
                <table id="tblSheetMusic" cellpadding=5 cellspacing=0 border=0 width=500px >
                <!-- category -->
                    <tr id="trCategory" runat="server"> 
                        <td valign=top  align=right  class="advanceSearchTD"><a href="javascript:SelectDeselectAll('cblCategoryMusic')"><asp:Label ID="lblCategoryListMusic" runat="server" Text="Category" Font-Bold=true></asp:Label> </a></td>
                        <td valign =top>
                                 <asp:CheckBoxList ID="cblCategoryMusic" runat="server"    RepeatDirection=vertical RepeatLayout=table BorderWidth=0px RepeatColumns=2 CellPadding=2 Width=348px >
                                   
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <!-- Event -->
                    <tr id="trEvent" runat="server"> 
                        <td valign=top  align=right  class="advanceSearchTD"><a href="javascript:SelectDeselectAll('cblEventMusic')"><asp:Label ID="lblEventListMusic" runat="server" Text="Event" Font-Bold=true></asp:Label></a> </td>
                        <td valign =top>
                            <asp:CheckBoxList ID="cblEventMusic" runat="server" RepeatDirection=Vertical RepeatLayout=table BorderWidth=0px  RepeatColumns=2 CellPadding=2 Width=232px>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <!-- Period -->
                    <tr id="trPeriod" runat="server"> 
                        <td valign=top align=right class="advanceSearchTD"><a href="javascript:SelectDeselectAll('cblPeriod')"><asp:Label ID="lblPeriodListMusic" runat="server" Text="Period" Font-Bold=true></asp:Label></a> </td>
                        <td valign =top>
                            <asp:CheckBoxList ID="cblPeriod" runat="server" RepeatDirection=Vertical RepeatLayout=table BorderWidth=0px  RepeatColumns=2 CellPadding=2 Width=309px>
                                  
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <!-- Country -->
                    <tr id="trCountry" runat="server" > 
                        <td valign=top  align=right  class="advanceSearchTD"><a href="javascript:SelectDeselectAll('cblCountryMusic')"><asp:Label ID="lblCountryListMusic" runat="server" Text="Country" Font-Bold=true></asp:Label></a> </td>
                        <td valign =top>
                            <asp:CheckBoxList ID="cblCountryMusic" runat="server" RepeatDirection=Vertical RepeatLayout=table BorderWidth=0px  RepeatColumns=2 CellPadding=2 Width=305px>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <!-- Grade -->
                    <tr id="trGrade" runat="server"> 
                        <td valign=top  align =right  class="advanceSearchTD"><a href="javascript:SelectDeselectAll('cblGradeMusic')"><asp:Label ID="lblGradeList" runat="server" Text="Difficulty/Grade" Font-Bold=true></asp:Label></a> </td>
                        <td valign =top>
                            <asp:CheckBoxList ID="cblGradeMusic" runat="server" RepeatDirection=Vertical RepeatLayout=table BorderWidth=0px  RepeatColumns=2 CellPadding=2 Width=310px>
                                   
                                    
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr > <!-- ISMN -->
                        <td valign=top align=right  class="advanceSearchTD"><asp:Label ID="lblISMNMusic" runat="server" Text="ISMN-search" Font-Bold=true></asp:Label> </td>
                        <td valign =top style="padding-top:8px; padding-left:10px;">
                            <asp:TextBox ID="txtISMNSearchMusic"  runat="server"></asp:TextBox>
                        </td>
                    </tr>
                   
                </table>
                </div>
            </td>
        </tr>
        <!-- End Criteria List for Sheet Music --> 
        <!-- Start Criteria List for Books --> 
        <tr>
            <td>
                <div id="divBooks" runat=server>
                    <table id="tblBooks" cellpadding=5 cellspacing=0 border=0 width=500px>
                        <tr>
                            <td valign=top align=right class="advanceSearchTD">
                               <asp:Label ID="lblISBNSearchBook" runat="server" Text="ISBN-search" Font-Bold=true></asp:Label> 
                            </td>
                            <td valign =top style="padding-top:8px; padding-left:9px;">
                                <asp:TextBox ID="txtISBNSearchBook"  runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trbookCategory" runat="server">
                            <td valign=top align=right class="advanceSearchTD">
                                <a href="javascript:SelectDeselectAll('cblCategoryBook')"><asp:Label ID="lblCategoryBook" runat="server" Text="Category" Font-Bold=true></asp:Label> </a>
                            </td>
                            <td valign=top style="padding-left:5px;">
                                <asp:CheckBoxList ID="cblCategoryBook" runat="server" RepeatDirection=Vertical RepeatLayout=table BorderWidth=0px  RepeatColumns=2 CellPadding=2 Width=250px>
                                 </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr id="trbookLanguage" runat="server">
                            <td valign=top  align=right  class="advanceSearchTD">
                                <a href="javascript:SelectDeselectAll('cblLanguageBook')"><asp:Label ID="lblLanguageBook" runat="server" Text="Language" Font-Bold=true></asp:Label> </a>
                            </td>
                            <td valign=top>
                                <asp:CheckBoxList ID="cblLanguageBook" runat="server" RepeatDirection=Vertical RepeatLayout=table BorderWidth=0px  RepeatColumns=2 CellPadding=2 Width=206px>
                                 </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <!-- End Criteria List for Books --> 
        <!-- Start Criteria List for CD/DVD -->
        <tr>
            <td>
                <div id="divCDDVD" runat=server>
                    <table id="tblCDDVD" cellpadding=5 cellspacing=0 border=0 width=500px>
                        <tr id="trcddvdCategory" runat="server">
                            <td valign=top align=right class="advanceSearchTD">
                                <a href="javascript:SelectDeselectAll('cblCategoryCD')"><asp:Label ID="lblCategoryCD" runat="server" Text="Category" Font-Bold=true></asp:Label> </a>
                            </td>
                            <td valign=top>
                                <asp:CheckBoxList ID="cblCategoryCD" runat="server" RepeatDirection=Vertical RepeatLayout=table BorderWidth=0px  RepeatColumns=2 CellPadding=2 Width=280px>
                                 </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td valign=top  align=right  class="advanceSearchTD">
                                <a href="javascript:SelectDeselectAll('cblTypeCD')"><asp:Label ID="lblType" runat="server" Text="Type of product" Font-Bold=true></asp:Label> </a>
                            </td>
                             <td valign=top>
                                <asp:CheckBoxList ID="cblTypeCD" runat="server" RepeatDirection=Vertical RepeatLayout=table BorderWidth=0px  RepeatColumns=2 CellPadding=2 Width=306px>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <!-- End Criteria List for CD/DVD --> 
        <!-- Start Criteria List for ALL --> 
        <tr>
            <td>
                <div id="divAll" runat=server >
                    <table id="tblAll" cellpadding=5 cellspacing=0 border=0 width=500px>
                        <tr>
                            <td valign=top  align=right  style="padding-top:8px; padding-left:5px;">
                               <asp:Label ID="lblISBNAll" runat="server" Text="ISBN-search" Font-Bold=true></asp:Label>  
                            </td>
                             <td valign =top style="padding-top:8px; padding-left:9px;">
                                <asp:TextBox ID="txtISBNAll"  runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign=top  align =right class="advanceSearchTD">
                               <asp:Label ID="lblISMNAll" runat="server" Text="ISMN-search" Font-Bold=true></asp:Label>  
                            </td>
                             <td valign =top style="padding-top:8px; padding-left:9px;">
                                <asp:TextBox ID="txtISMNAll"   runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr >
                            <td valign=top  align =right  class="advanceSearchTD">
                                <a href="javascript:SelectDeselectAll('cblLanguageAll')"><asp:Label ID="lblLanguageAll" runat="server" Text="Language" Font-Bold=true></asp:Label> </a>
                            </td>
                            <td valign=top>
                                <asp:CheckBoxList ID="cblLanguageAll" runat="server" RepeatDirection=Vertical RepeatLayout=table BorderWidth=0px  RepeatColumns=3 CellPadding=2 Width=290px >
                                 </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <!-- End Criteria List for ALL --> 
        </table>
      </td>
</tr>
<tr >
<td width=100% height=300px>&nbsp</td>
</tr>
</table>
</asp:Content>

