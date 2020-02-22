<%@ Page Language="C#" Culture="nl-NL" AutoEventWireup="true" CodeFile="Article.aspx.cs"
    Inherits="Admin_Article" Title="Article Management" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/style.css" type="text/css" rel="stylesheet">
    <link href="media/style.css" type="text/css" rel="stylesheet">
    <link href="media/master.css" type="text/css" rel="stylesheet">

    <script language="JavaScript" src="../include/CommonFuctions.js"></script>

    <script language="JavaScript" src="../include/jscript.js"></script>

    <script language="JavaScript" src="../include/Datepicker.js"></script>

    <script language="JavaScript" src="../include/tabber.js"></script>

    <script type="text/javascript" language="javascript">
        document.write('<style type="text/css">.tabber{display:none;}<\/style>');
    </script>

</head>
<body class="adminbody">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <div align="left" style="left: 0px; top: 0px; width: 881px">
        <div class="header" style="height: 76px; valign: top; width: 881px;">
        </div>
        <div class="contentHeader" align="left" style="width: 876px; height: 20px; padding-top: 5px;
            background-color: #DEDEDE;">
            Article Management
        </div>
        <div style="background-color: #f0f0f0; height: 173px;">
            <div class="contentArea" style="height: 160px;">
                <div class="FormLeftColumn" style="width: 50%">
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 20%;">
                            <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtTile" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 20%;">
                            <asp:Label ID="lblArticleType" runat="server" Text="Article Type"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:DropDownList ID="ddlArticleType" CssClass="CommonDropDownList" Width="150px"
                                runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="upnlComposerPicker" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSaveComposer" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="TableRow">
                                <div class="TableFormLeble" style="width: 20%;">
                                    <asp:Label ID="lblComposer" runat="server" Text="Composer"></asp:Label>:
                                </div>
                                <div class="TableFormContent">
                                    <asp:DropDownList ID="ddlComposer" CssClass="CommonDropDownList" Width="150px" runat="server">
                                        <asp:ListItem Text="Please Select"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:LinkButton ID="btnComposer" OnClick="LoadPnlComposer" runat="server" Text="Add" />
                                </div>
                            </div>
                            <asp:LinkButton runat="server" Style="display: none" ID="lnkHidden4" />
                            <cc1:ModalPopupExtender ID="mpeComposer" runat="server" BackgroundCssClass="modalBackground"
                                CancelControlID="btnCancelComposer" DropShadow="false" OkControlID="" PopupControlID="pnlAddComposer"
                                TargetControlID="lnkHidden4">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 20%;">
                            <asp:Label ID="lblGrade" runat="server" Text="Grade"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:DropDownList ID="ddlGrade" CssClass="CommonDropDownList" Width="150px" runat="server">
                                <asp:ListItem Text="Please Select"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 20%;">
                            <asp:Label ID="lblPrice" runat="server" Text="Price"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox runat="server" ID="txtPrice" CssClass="CommonTextBox" Style="text-align: right"
                                MaxLength="12" onfocus="javascript: SetLatestValue(this.value)" onchange="return CheckCorrectValue(this,'0123456789,')"
                                onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789,')"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="FormRightColumn" style="right: 40%; float: left;">
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 20%;">
                            <asp:Label ID="lblPublisher" runat="server" Text="Publisher"></asp:Label>:
                        </div>
                        <asp:UpdatePanel ID="upnlPublisher" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="TableFormContent">
                                    <asp:DropDownList ID="ddlPublisher" Width="150px" CssClass="CommonDropDownList" runat="server">
                                    </asp:DropDownList>
                                    <asp:LinkButton ID="btnPublisher" runat="server" OnClick="LoadPnlPublisher" Text="Add" />
                                </div>
                                <asp:LinkButton runat="server" Style="display: none" ID="lnkHidden2" />
                                <cc1:ModalPopupExtender ID="mpeAddPublisher" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="btnCancel" DropShadow="false" OkControlID="" PopupControlID="pnlAddPublisher"
                                    TargetControlID="lnkHidden2">
                                </cc1:ModalPopupExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 20%;">
                            <asp:Label ID="lblEditionNumber" runat="server" Text="Edition Number"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtEditionNumber" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 20%;">
                            <asp:Label ID="lblInstrumentation" runat="server" Text="Instrumentation"></asp:Label>:
                        </div>
                        <asp:UpdatePanel ID="upnlRightColumn" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnInstrumentationSave" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="TableFormContent">
                                    <asp:TextBox ID="txtInstrumentation" ReadOnly="true" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="btnSelectInstrumentation" runat="server" OnClick="LoadgrdInstrumentation"
                                        Text="Select" /></div>
                                <asp:LinkButton runat="server" Style="display: none" ID="lnkHidden" />
                                <cc1:ModalPopupExtender ID="mpeInstrumentation" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="btnCancelInstrumentation" DropShadow="false" OkControlID=""
                                    PopupControlID="pnlInstrumentation" TargetControlID="lnkHidden">
                                </cc1:ModalPopupExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:UpdatePanel ID="upnlCategoryPicker" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="grdCategory" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="TableRow">
                                <div class="TableFormLeble" style="width: 20%;">
                                    <asp:Label ID="lblCategories" runat="server" Text="Categories"></asp:Label>:
                                </div>
                                <div class="TableFormContent">
                                    <asp:TextBox ID="txtCategories" ReadOnly="true" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="btnSelectCategories" OnClick="LoadgrdCategory" runat="server"
                                        Text="Select" /></div>
                            </div>
                            <asp:LinkButton runat="server" Style="display: none" ID="lnkHidden3" />
                            <cc1:ModalPopupExtender ID="mpeCategory" runat="server" BackgroundCssClass="modalBackground"
                                CancelControlID="btnPanelCategoryCancel" DropShadow="false" OkControlID="" PopupControlID="pnlCategory"
                                TargetControlID="lnkHidden3">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div style="background-color: #f0f0f0;">
                <div class="tabber" id="myTab">
                    <div class="tabbertab" style="width: 93.5%; height: 350px;">
                        <h2>
                            Media</h2>
                        <p>
                            <div style="float: left; width: 33%">
                                <div class="TableRow">
                                    <div class="TableFormLeble">
                                        <asp:Label ID="lblPdf" runat="server" Text="Pdf"></asp:Label>
                                    </div>
                                    <div class="TableFormContent">
                                        <asp:FileUpload ID="pdfUpload" runat="server" />
                                        <asp:LinkButton ID="btnShowPdf" Width="49px" runat="server" Text="upload" OnClick="btnShowPdf_Load" ValidationGroup="pdf" />
                                        <asp:RegularExpressionValidator runat="server" ID="valUpTest" ControlToValidate="pdfUpload"
                                            ErrorMessage="only *.pdf is allowed" ValidationGroup="pdf" Display="Dynamic"
                                            ValidationExpression="^.+\.(pdf)$" />
                                        <asp:RequiredFieldValidator runat="server" ID="rfvPdf" ControlToValidate="pdfUpload"
                                            ValidationGroup="pdf" Display="Dynamic" ErrorMessage="Select a pdf file first!"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="TableRow">
                                    <div id="dvflash" runat="server" style="padding-top: 15px; height: 250px; border: 1px solid gray;"
                                        class="TableFormContent">
                                    </div>
                                </div>
                            </div>
                            <div style="padding-left: 3px; float: left; width: 33%">
                                <div class="TableRow">
                                    <div class="TableFormLeble">
                                        <asp:Label ID="lblimage" runat="server" Text="Image"></asp:Label>
                                    </div>
                                    <div class="TableFormContent">
                                        <asp:FileUpload ID="ImageUpload" runat="server" />
                                        <asp:LinkButton ID="btnShowImage" Width="49px" runat="server" Text="upload" OnClick="btnShowImage_Load"
                                            ValidationGroup="image" />
                                        <asp:RegularExpressionValidator runat="server" ID="revImage" ControlToValidate="ImageUpload"
                                            ErrorMessage="Only image is allowed" ValidationGroup="image" Display="Dynamic"
                                            ValidationExpression="^.+\.(jpg|jpeg|png|gif|bmp)$" />
                                        <asp:RequiredFieldValidator runat="server" ID="rfvImage" ControlToValidate="ImageUpload"
                                            ValidationGroup="image" Display="Dynamic" ErrorMessage="Select an image first!"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="TableRow">
                                    <div id="dvImage" runat="server" style="padding-top: 15px; height: 250px; width:275px; border: 1px solid gray;"
                                        class="TableFormContent">
                                    </div>
                                </div>
                            </div>
                            <div style="float: right; width: 33%">
                                <div class="TableRow">
                                    <div class="TableFormLeble">
                                        <asp:Label ID="lblMp3" runat="server" Text="Mp3"></asp:Label>
                                    </div>
                                    <div class="TableFormContent">
                                        <asp:FileUpload runat="server" ID="Mp3Upload" />
                                        <asp:LinkButton ID="btnMp3Upload" Width="49px" runat="server" Text="upload" OnClick="btnMp3Upload_Load"
                                            ValidationGroup="mp3" />
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="Mp3Upload"
                                            ErrorMessage="Only MP3 is allowed" ValidationGroup="mp3" Display="Dynamic" ValidationExpression="^.+\.(mp3)$" />
                                        <asp:RequiredFieldValidator runat="server" ID="rfvMp3" ControlToValidate="Mp3Upload"
                                            ValidationGroup="mp3" Display="Dynamic" ErrorMessage="Select an MP3 first!"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="TableRow">
                                    <div id="dvmp3" runat="server" style="padding-top: 15px; height: 250px; border: 1px solid gray;"
                                        class="TableFormContent">
                                    </div>
                                </div>
                            </div>
                        </p>
                    </div>
                    <div style="width: 93.5%; height: 350px;" class="tabbertab">
                        <h2>
                            Shop</h2>
                        <p>
                            <div class="FormLeftColumn">
                                <div class="TableRow">
                                    <div class="TableFormLeble" style="padding-right: 3px;">
                                        <asp:CheckBox ID="chkIsactive" runat="server" Text="Is Active"></asp:CheckBox>
                                    </div>
                                </div>
                                <div class="TableRow">
                                    <div class="TableFormLeble">
                                        <asp:Label ID="lblPublishDate" runat="server" Text="Publish Date"></asp:Label>:
                                    </div>
                                    <div class="TableFormContent">
                                        <asp:TextBox ID="ADate" runat="server" ReadOnly="false" MaxLength="10"></asp:TextBox>
                                        <input type="button" value=".." onclick="displayDatePicker('ADate');" style="height: 21px" />
                                    </div>
                                </div>
                            </div>
                            <div class="FormRightColumn">
                                <div class="TableRow">
                                    <div class="TableFormLeble">
                                        <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>:
                                    </div>
                                    <div class="TableFormContent">
                                        <asp:TextBox ID="txtQuantity" CssClass="CommonTextBox" runat="server" Style="text-align: right"
                                            MaxLength="12" onfocus="javascript: SetLatestValue(this.value)" onchange="return CheckCorrectValue(this,'0123456789,')"
                                            onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789,')"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="TableRow">
                                    <div class="TableFormLeble">
                                        <asp:Label ID="lblPurchasePrice" runat="server" Text="Purchase Price"></asp:Label>:
                                    </div>
                                    <div class="TableFormContent">
                                        <asp:TextBox ID="txtPurchasePrice" CssClass="CommonTextBox" runat="server" Style="text-align: right"
                                            MaxLength="12" onfocus="javascript: SetLatestValue(this.value)" onchange="return CheckCorrectValue(this,'0123456789,')"
                                            onkeypress="return CheckNumericKeyStroke(event,this.value,'0123456789,')"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </p>
                    </div>
                    <div class="tabbertab" style="width: 93.5%; height: 350px;">
                        <h2>
                            Detail</h2>
                        <p>
                            <div class="FormLeftColumn">
                                <div class="TableRow">
                                    <div class="TableFormLeble">
                                        <asp:Label ID="lblSubtitle" runat="server" Text="SubTitle"></asp:Label>:
                                    </div>
                                    <div class="TableFormContent">
                                        <asp:TextBox ID="txtSubtitle" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="TableRow">
                                    <div class="TableFormLeble">
                                        <asp:Label ID="lblSerie" runat="server" Text="Serie"></asp:Label>:
                                    </div>
                                    <div class="TableFormContent">
                                        <asp:TextBox ID="txtSerie" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="TableRow">
                                    <div class="TableFormLeble">
                                        <asp:Label ID="lblPeriod" runat="server" Text="Period"></asp:Label>:
                                    </div>
                                    <div class="TableFormContent">
                                        <asp:DropDownList ID="ddlPeriod" Width="150px" CssClass="CommondropDownList" runat="server">
                                            <asp:ListItem Text="Please Select">
                                            </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="TableRow">
                                    <div class="TableFormLeble">
                                        <asp:Label ID="lblDetailSubCategory" runat="server" Text="SubCategory"></asp:Label>:
                                    </div>
                                    <div class="TableFormContent">
                                        <asp:DropDownList ID="ddlDetailSubCategory" Width="150px" CssClass="CommondropDownList"
                                            runat="server">
                                            <asp:ListItem Text="Please Select">
                                            </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="FormRightColumn">
                                <div class="TableRow">
                                    <div class="TableFormLeble">
                                        <asp:Label ID="lblDuration" runat="server" Text="Duration"></asp:Label>:
                                    </div>
                                    <div class="TableFormContent">
                                        <asp:TextBox ID="txtDuration" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="TableRow">
                                    <div class="TableFormLeble">
                                        <asp:Label ID="lblISBN" runat="server" Text="ISBN"></asp:Label>:
                                    </div>
                                    <div class="TableFormContent">
                                        <asp:TextBox ID="txtISBN" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="TableRow">
                                    <div class="TableFormLeble">
                                        <asp:Label ID="lblKeywords" runat="server" Text="Keywords"></asp:Label>:
                                    </div>
                                    <div class="TableFormContent">
                                        <asp:TextBox ID="txtKeywords" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </p>
                    </div>
                    <div class="tabbertab" style="width: 93.5%; height: 350px;">
                        <h2>
                            Description</h2>
                        <p>
                        </p>
                        <div class="tabber" id="tab1-1">
                            <div class="tabbertab" style="height: 300px;">
                                <h3>
                                    English</h3>
                                <p>
                                    <div id="RTBdiv" runat="server" style="position: absolute">
                                        <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" BasePath="~/fckeditor/" Height="238px"
                                            Width="750px">
                                        </FCKeditorV2:FCKeditor>
                                        <asp:Button ID="btnRTBsave" Visible="false" runat="server" Text="Save" />
                                        <asp:Button ID="btnRTBclose" Visible="false" runat="server" Text="Close" />
                                    </div>
                                </p>
                            </div>
                            <div class="tabbertab" style="height: 300px;">
                                <h3>
                                    Dutch</h3>
                                <p>
                                    <div id="RTBdiv1" runat="server" style="position: absolute">
                                        <FCKeditorV2:FCKeditor ID="FCKeditor2" runat="server" BasePath="~/fckeditor/" Height="238px"
                                            Width="750px">
                                        </FCKeditorV2:FCKeditor>
                                        <asp:Button ID="btnRTBsave1" Visible="false" runat="server" Text="Save" />
                                        <asp:Button ID="btnRTBclose1" Visible="false" runat="server" Text="Close" />
                                    </div>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="TableRow">
                <div class="TableFormContent" style="text-align: left; padding-left: 3px; padding-top: 15px;
                    background-color: #DEDEDE !important;">
                    <asp:Button ID="btnSaveArticle" runat="server" OnClick="btnSaveArticle_Click" Text="Save" />
                    <asp:Button ID="btnArticleCancel" runat="server" Text="Cancel" OnClick="btnArticleCancel_Click" />
                </div>
            </div>
        </div>
        <asp:Panel runat="server" ID="pnlCategory">
            <asp:UpdatePanel ID="upnlCategory" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div style="margin: auto; height: 300px; overflow: auto; width: 577px; left: 0px;
                        top: 0px; background-color: #f0f0f0;">
                        <table width="550px" height="300px" border="0" align="center">
                            <tr>
                                <td align="center" style="padding-left: 0">
                                    <asp:GridView ID="grdCategory" BorderWidth="0px" BorderStyle="Outset" CellPadding="5"
                                        runat="server" AutoGenerateColumns="False" Width="550px" GridLines="none" BorderColor="white">
                                        <HeaderStyle CssClass="DataGridFixedHeader2" HorizontalAlign="Left" VerticalAlign="top"
                                            Height='10px'></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Id">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "categoryid")%>'
                                                        OnCommand="lnkSelect_Command">
                                                        <asp:Label ID="lblCategoryId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "categoryid")%>'></asp:Label>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" Width="250px" />
                                                <HeaderStyle HorizontalAlign="left" Width="250px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category Name(en)">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "categorynameen")%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="75px" />
                                                <HeaderStyle HorizontalAlign="Left" Width="75px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category Name(nl)">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "categorynamenl")%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="175px" />
                                                <HeaderStyle HorizontalAlign="Left" Width="175px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vatpc">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "vatpc")%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                <HeaderStyle HorizontalAlign="Right" Width="50px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#DEDEDE" BorderColor="White" HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="#EFEFEF" />
                                        <RowStyle BackColor="White" VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="text-align: left; padding-left: 10px; padding-top: 10px; padding-right: 10px;
                background-color: #f0f0f0; height: 20px;">
                <asp:LinkButton ID="btnPanelCategoryCancel" runat="server" Text="Cancel" OnClick="btnPanelCategoryCancel_Load" />
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlInstrumentation">
            <asp:UpdatePanel ID="upnlInstrumentation" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div style="overflow: auto; height: 300px; width: 427px;">
                        <table width="100%" height="200px" border="0" align="center">
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="grdInstrumentation" BorderWidth="0px" BorderStyle="Outset" CellPadding="5"
                                        runat="server" AutoGenerateColumns="False" Width="100%" GridLines="none" BorderColor="white">
                                        <HeaderStyle CssClass="DataGridFixedHeader2" HorizontalAlign="Left" VerticalAlign="top"
                                            Height="10px"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Id">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="lnkSelect" runat="server" CausesValidation="false"></asp:CheckBox>
                                                    <%--  <asp:HiddenField ID="hdfInstrumentId" runat="server" Value='<%# Eval("id") %>' />--%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" Width="250px" />
                                                <HeaderStyle HorizontalAlign="left" Width="250px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "instrumentname")%>
                                                    <asp:HiddenField ID="hdfInstrumentname" runat="server" Value='<%# Eval("instrumentname") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="75px" />
                                                <HeaderStyle HorizontalAlign="Left" Width="75px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#DEDEDE" BorderColor="White" HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="#EFEFEF" />
                                        <RowStyle BackColor="White" VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="text-align: left; padding-left: 10px; padding-top: 10px; background-color: #f0f0f0;
                padding-right: 10px; height: 20px;">
                <asp:LinkButton ID="btnInstrumentationSave" runat="server" Text="Select" OnClick="btnInstrumentationSave_Load" />
                <asp:LinkButton ID="btnCancelInstrumentation" runat="server" Text="Cancel" OnClick="btnCancelInstrumentation_Load" />
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlAddPublisher">
            <div>
                <div class="contentHeader" align="left" style="width: 534px; height: 20px; padding-top: 5px;
                    background-color: #DEDEDE;">
                    Add Publisher
                </div>
                <div class="TableRow">
                    <div class="TableFormContent">
                        <asp:Label ID="lblError" EnableViewState="false" runat="server" Width="510px" Font-Bold="true"
                            ForeColor="red"></asp:Label>
                    </div>
                </div>
                <div class="FormLeftColumn">
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtFirstName" EnableViewState="false" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblLastName" runat="server" Text="last Name"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtLatName" EnableViewState="false" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblHouseNr" runat="server" Text="HouseNr"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtHouseNr" EnableViewState="false" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblPostCode" runat="server" Text="Post Code"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtPostCode" EnableViewState="false" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblCountry" runat="server" Text="Country"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtCountry" EnableViewState="false" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblWebsite" runat="server" Text="Website"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtWebsite" EnableViewState="false" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblFax" runat="server" Text="Fax"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtFax" EnableViewState="false" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="FormRightColumn">
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtMiddleName" EnableViewState="false" CssClass="CommonTextBox"
                                runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblInitialName" runat="server" Text="InitialName"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtInitialName" EnableViewState="false" CssClass="CommonTextBox"
                                runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtAddress" EnableViewState="false" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblResidence" runat="server" Text="Residence"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtResidence" EnableViewState="false" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtEmail" EnableViewState="false" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblTelephone" runat="server" Text="Telephone"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtTelephone" EnableViewState="false" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblCompanyName" runat="server" Text="Company Name"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtCompanyName" EnableViewState="false" CssClass="CommonTextBox"
                                runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div style="padding-left: 90px; font: 11px tahoma,arial,helvetica; color: #444444;"
                            class="TableFormContent">
                            <asp:CheckBox EnableViewState="false" ID="chkIsPublisher" Text="Is Publisher" runat="server">
                            </asp:CheckBox>
                        </div>
                    </div>
                </div>
                <div class="TableRow">
                    <div class="TableFormContent" style="text-align: left; padding-left: 5px; padding-top: 20px;">
                        <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
                        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" />
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlAddComposer">
            <div>
                <div class="contentHeader" align="left" style="width: 534px; height: 20px; padding-top: 5px;
                    background-color: #DEDEDE;">
                    Add Composer
                </div>
                <div class="TableRow">
                    <div class="TableFormContent">
                        <asp:Label ID="lblError1" EnableViewState="false" runat="server" Width="510px" Font-Bold="true"
                            ForeColor="red"></asp:Label>
                    </div>
                </div>
                <div class="FormLeftColumn">
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblMiddleNameComposer" runat="server" Text="Middle Name"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtMiddleNameComposer" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblCountryComposer" runat="server" Text="Country"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtCountryComposer" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblDod" runat="server" Text="Dod"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtDod" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="FormRightColumn">
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblFirstNameComposer" runat="server" Text="First Name"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtFirstNameComposer" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblLastNameComposer" runat="server" Text="Last Name"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtLastNameComposer" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="TableRow">
                        <div class="TableFormLeble" style="width: 30%;">
                            <asp:Label ID="lblDob" runat="server" Text="Dob"></asp:Label>:
                        </div>
                        <div class="TableFormContent">
                            <asp:TextBox ID="txtDob" CssClass="CommonTextBox" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="TableRow">
                    <div class="TableFormContent" style="text-align: left; padding-left: 5px; padding-top: 30px;">
                        <asp:LinkButton ID="btnSaveComposer" runat="server" OnClick="btnSaveComposer_Click"
                            Text="Save" />
                        <asp:LinkButton ID="btnCancelComposer" runat="server" OnClick="btnCancelComposer_Click"
                            Text="Cancel" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
