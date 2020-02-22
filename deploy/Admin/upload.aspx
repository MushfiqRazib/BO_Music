<%@ page language="C#" autoeventwireup="true" inherits="Admin_upload, App_Web_upload.aspx.fdf7a39c" title="Upload resources" validaterequest="false" theme="ThemeOne" %>

<%@ Register Src="../Search.ascx" TagName="Search" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" class="contentArea">
            <tr>
                <td>
                    <table bgcolor="#DEDEDE" width='882px' border='0px' style="padding-left: 0; padding-right: 0;
                        padding-bottom: 0; padding-top: 0;">
                        <tr>
                            <td valign="top" align="left" style="width: 400px; padding-left: 8px; padding-top: 8px;
                                background-color: #DEDEDE;">
                                <h4 class="contentHeader">
                                    Upload Resource</h4>
                            </td>
                            <td valign="top" align="right" style="padding-right: 8px; padding-top: 8px; background-color: #DEDEDE;">
                                <div style="text-align: right">
                                    <table border='0px' cellpadding='1px'>
                                        <tr>
                                            <td valign="middle">
                                                <asp:Label ID="Label2" runat="server" Text="Filter "></asp:Label>&nbsp;
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlFilter" runat="server">
                                                    <asp:ListItem Value='article'>Article</asp:ListItem>
                                                    <asp:ListItem Value='news'>News</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFilter" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnFilter" runat="server" Text="Search" OnClick="btnFilter_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 30px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" style="width: 98%; background-color: #FFFFFF;" align="center">
                        <tr>
                            <td>
                                <table border="0" style="width: 90%; background-color: #FFFFFF;" align="center">
                                    <tr>
                                        <td align="center" style="width: 50%; align: right;">
                                            <fieldset><LEGEND>Article</LEGEND><TABLE style="WIDTH: 100%; BACKGROUND-COLOR: #f0f0f0" align=center><TBODY><TR><TD style="HEIGHT: 24px" vAlign=top align=left colSpan=4>&nbsp;
                     <asp:GridView id="grdArticle" runat="server" OnSorting="grdArticle_Sorting" Width="100%" AllowPaging="True" AllowSorting="True" PageSize="6" BorderColor="White" BorderWidth="1px" CellPadding="5" AutoGenerateColumns="False" OnPageIndexChanging="grdArticle_PageIndexChanging" BackColor="White" OnSelectedIndexChanged="grdArticle_SelectedIndexChanged">
                     <Columns>

<asp:TemplateField SortExpression="Code" HeaderText="Article Code">
<ItemStyle Width="70px" HorizontalAlign="Left"></ItemStyle>
<ItemTemplate>
    <asp:Label ID="lblArticleCode" runat="server" Text = '<%# DataBinder.Eval(Container.DataItem, "Code")%>' />

</ItemTemplate>
    <HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>
                     
<asp:TemplateField SortExpression="title" HeaderText="Title">
<ItemStyle Width="250px" HorizontalAlign="Left"></ItemStyle>
<ItemTemplate>
<%# DataBinder.Eval(Container.DataItem, "Title")%>
</ItemTemplate>
    <HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>
<asp:TemplateField SortExpression="publishdate" HeaderText="Date">
<ItemStyle HorizontalAlign="Left" />
<ItemTemplate>
<%# DataBinder.Eval(Container.DataItem, "Date")%>
</ItemTemplate>
    <HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>
<asp:TemplateField SortExpression="articletype" HeaderText="Type">
<ItemStyle HorizontalAlign="Left" />
<ItemTemplate>
<%# DataBinder.Eval(Container.DataItem, "Type")%>
</ItemTemplate>
    <HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>
<asp:TemplateField SortExpression="imagefile" HeaderText="Image">
<ItemStyle HorizontalAlign="Left" />
<ItemTemplate>
<asp:Label ID="lblImage" runat="server" Text = '<%# DataBinder.Eval(Container.DataItem, "Image")%>' />
</ItemTemplate>
    <HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>
<asp:TemplateField SortExpression="pdffile" HeaderText="Document">
<ItemStyle HorizontalAlign="Left" />
<ItemTemplate>
<asp:Label ID="lblFile" runat="server" Text = '<%# DataBinder.Eval(Container.DataItem, "File")%>' />
</ItemTemplate>
    <HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Music File" SortExpression="musicfile">
<ItemStyle HorizontalAlign="Left" />
<ItemTemplate>
<asp:Label ID="lblMusicFile" runat="server" Text = '<%# DataBinder.Eval(Container.DataItem, "musicfile")%>' />
</ItemTemplate>
    <HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:CommandField ShowSelectButton="True"  />
</Columns>

<RowStyle BackColor="White"></RowStyle>

<PagerStyle HorizontalAlign="Left"></PagerStyle>

<HeaderStyle BackColor="LightGray"></HeaderStyle>

<AlternatingRowStyle BackColor="#EFEFEF"></AlternatingRowStyle>
</asp:GridView></TD></TR><TR><TD vAlign=top align=right width="96%" colSpan=4>
<FIELDSET><LEGEND>Upload Article</LEGEND><TABLE style="WIDTH: 80%" align=right><TBODY><TR><TD vAlign=top align=right width=100 height=10><asp:Label id="lblArticleImage" runat="server" Text="Image"></asp:Label></TD><TD style="WIDTH: 101px" vAlign=top align=left height=10><asp:TextBox id="txtArtImg" runat="server" Width="100px" ReadOnly="True"></asp:TextBox></TD><TD vAlign=top align=left width=192 height=10><INPUT style="WIDTH: 192px; HEIGHT: 20px" id="uplTheFileArticleImg" type=file size=12 name="uplTheFile" runat="server" contenteditable="false" /></TD><TD vAlign=top align=left width=200 height=10>&nbsp;&nbsp;&nbsp;<asp:Label id="lblArtImage" runat="server" ForeColor="Red"></asp:Label></TD></TR>
<TR>
    <TD vAlign=top align=right width=100>
        Document
    </TD>
    <TD style="WIDTH: 101px" vAlign=top align=left>
        <asp:TextBox id="txtArticlePDF" runat="server" Width="100px" ReadOnly="True"></asp:TextBox>
    </TD>
    <TD vAlign=top align=left width=192>
        <INPUT style="WIDTH: 192px; HEIGHT: 20px" id="uplTheFileArticlePDF" type=file size=12 name="uplTheFile" runat="server" contenteditable="false" />
    </TD>
    <TD vAlign=top align=left width=200>&nbsp;&nbsp;&nbsp;
        <asp:Label id="lblArtFile" runat="server" ForeColor="Red"></asp:Label>
    </TD>
 </TR>
 
 
 
 <TR>
    <TD vAlign=top align=right width=100>
        Music
    </TD>
    <TD style="WIDTH: 101px" vAlign=top align=left>
        <asp:TextBox id="txtArticleMusic" runat="server" Width="100px" ReadOnly="True"></asp:TextBox>
    </TD>
    <TD vAlign=top align=left width=192>
        <INPUT style="WIDTH: 192px; HEIGHT: 20px" id="uplMusic" type=file  size=12 name="uplTheFile" runat="server" contenteditable="false" />
    </TD>
    <TD vAlign=top align=left width=200>&nbsp;&nbsp;&nbsp;
        <asp:Label id="lblArtMusic" runat="server" ForeColor="Red"></asp:Label>
    </TD>
 </TR>
 
 
 <TR><TD vAlign=top align=right width=100></TD><TD style="WIDTH: 101px" vAlign=top align=right></TD><TD style="WIDTH: 192px; align: right" vAlign=top align=left width=192><asp:Button id="btnUploadArticle" onclick="btnUploadArticle_Click" runat="server" Text="Upload"></asp:Button></TD><TD vAlign=top align=left width=200>
     &nbsp;</TD></TR><TR><TD style="HEIGHT: 16px" vAlign=top align=right width=100></TD><TD style="HEIGHT: 16px" vAlign=top align=left colSpan=2><asp:Label id="lblMessageArticle" runat="server" ForeColor="Red"></asp:Label></TD><TD style="HEIGHT: 16px" vAlign=top align=left width=200>&nbsp;&nbsp;&nbsp;</TD></TR></TBODY></TABLE></FIELDSET>
<FIELDSET><LEGEND>Synchronization</LEGEND>
<TABLE style="WIDTH: 80%" align=right>
<TBODY>
<TR><TD vAlign="top" align="right" width="100" height="10">
    <asp:Button
         ID="btnSynchronize" runat="server" Text="Synchronize Resources" 
         onclick="btnSynchronize_Click" Width="180" />
     </TD></TR>
 </TABLE>
 </FIELDSET>
 
 </TD></TR></TBODY></TABLE></fieldset>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 100%; align: center;">
                                            <fieldset><LEGEND>News</LEGEND><TABLE style="WIDTH: 100%; BACKGROUND-COLOR: #f0f0f0"><TBODY><TR>
                    <TD style="HEIGHT: 24px; vertical-align: top" vAlign=top align=left colSpan=4>
                    <table border='0'>
                    <tr>
                    <td style="vertical-align: top" vAlign=top align=left>
                    <asp:Label ID="Label1" runat="server" Text="Show "></asp:Label>                    
                    </td>
                    <td style="vertical-align: top" vAlign=top align=left>
                    <asp:DropDownList id="ddlNews" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNews_SelectedIndexChanged"><asp:ListItem Selected="True" Value="true">Active</asp:ListItem>
                    <asp:ListItem Value="false">Inactive</asp:ListItem>
                    <asp:ListItem Value="all">All</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    </tr>
                    </table>
                    </TD></TR><TR><TD style="HEIGHT: 24px" vAlign=top align=left colSpan=4>

<asp:GridView id="grdNews" runat="server" OnPageIndexChanging="grdNews_PageIndexChanging" AutoGenerateColumns="False" CellPadding="5" BorderColor="White" BorderWidth="1px" PageSize="6" AllowSorting="True" AllowPaging="True" Width="100%" OnSorting="grdNews_Sorting" OnSelectedIndexChanged="grdNews_SelectedIndexChanged">
<Columns>
<asp:TemplateField SortExpression="Code" HeaderText="News Id">
<ItemStyle Width="50px" HorizontalAlign="Right" />
<ItemTemplate>
<asp:Label ID="lblCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Code")%>' />

</ItemTemplate>
    <HeaderStyle HorizontalAlign="Right" />
</asp:TemplateField>


<asp:TemplateField SortExpression="title" HeaderText="Title">
<ItemStyle HorizontalAlign="Left" />
<ItemTemplate>
<%# DataBinder.Eval(Container.DataItem, "Title")%>
<ItemStyle HorizontalAlign="left" />
</ItemTemplate>
    <HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>
<asp:TemplateField SortExpression="newsdate" HeaderText="Date">
<ItemStyle HorizontalAlign="Left" />
<ItemTemplate>
<%# DataBinder.Eval(Container.DataItem, "Date")%>
</ItemTemplate>
    <HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>
<asp:TemplateField SortExpression="newsimagefile" HeaderText="Image">
<ItemStyle HorizontalAlign="Left" />
<ItemTemplate>
<asp:Label ID="lblNwsImg" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Image")%>' />
</ItemTemplate>
    <HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

    <asp:CommandField ShowSelectButton="True" />
</Columns>

<RowStyle BackColor="White"></RowStyle>

<PagerStyle HorizontalAlign="Left"></PagerStyle>

<HeaderStyle BackColor="LightGray"></HeaderStyle>

<AlternatingRowStyle BackColor="#EFEFEF"></AlternatingRowStyle>
</asp:GridView></TD></TR><TR><TD style="HEIGHT: 16px" vAlign=top align=right colSpan=4><FIELDSET><LEGEND>Upload News</LEGEND><TABLE style="WIDTH: 80%" align=right><TBODY><TR><TD vAlign=top align=right width=100 height=10><asp:Label id="lblNewsImage" runat="server" Text="Image"></asp:Label></TD><TD style="WIDTH: 101px" vAlign=top align=left height=10><asp:TextBox id="txtNwsImg" runat="server" Width="100px" ReadOnly="True"></asp:TextBox></TD><TD vAlign=top align=left width=192 height=10><INPUT style="WIDTH: 192px; HEIGHT: 20px" id="uplTheFileNewsImg" type=file size=12 name="uplTheFile" runat="server" contenteditable="false" /></TD><TD vAlign=top align=left width=200 height=10>&nbsp;&nbsp;&nbsp;<asp:Label id="lblNwsImage" runat="server" ForeColor="Red"></asp:Label></TD></TR><TR><TD style="HEIGHT: 26px" vAlign=top align=right width=100></TD><TD style="WIDTH: 101px; HEIGHT: 26px" vAlign=top align=left><asp:CheckBox id="chkProcessImg" runat="server" Width="100px" Text="Allow Process" ToolTip="Allow Image Processing"></asp:CheckBox></TD><TD style="WIDTH: 192px; HEIGHT: 26px; align: right" vAlign=top align=left width=192><asp:Button id="btnUploadNews" onclick="btnUploadNews_Click" runat="server" Text="Upload"></asp:Button></TD><TD style="HEIGHT: 26px" vAlign=top align=left width=200>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD></TR><TR><TD style="HEIGHT: 16px" vAlign=top align=right width=100></TD><TD style="HEIGHT: 16px" vAlign=top align=left colSpan=2><asp:Label id="lblMessageNews" runat="server" ForeColor="Red"></asp:Label></TD><TD style="HEIGHT: 16px" vAlign=top align=left width=200>&nbsp;&nbsp;&nbsp;</TD></TR><TR><TD style="HEIGHT: 16px" vAlign=top align=center colSpan=3><LI>[ <B>Allow Process </B>will resize the image to the system size ]</LI></TD><TD style="HEIGHT: 16px" vAlign=top align=left width=200></TD></TR></TBODY></TABLE></FIELDSET> </TD></TR></TBODY></TABLE></fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="30px">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
