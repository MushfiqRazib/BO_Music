<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="Authors, Bo02" title="Composers" theme="ThemeOne" %>

<%@ MasterType VirtualPath="~/MainLayOut.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table align="center" cellpadding="0" cellspacing="0" width="880px">
        <tr>
            <td valign="top">
                <table border="0" cellpadding="0" cellspacing="0" style="width:160px">
                    <tr>
                        <td  align="left" style="background-image: url('graphics/index_header.gif'); height: 19px;
                            font-size: 10; color: White; background-repeat:no-repeat;padding-left: 2px;">
                            <asp:Label ID="lblIndexHeader" runat="server" Text="Auteurs / Componisten A-Z" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvIndex" runat="server" AutoGenerateColumns="false" CssClass="AuthorIndex"
                                OnRowCommand="gvIndex_RowCommand" OnRowDataBound="gvIndex_RowDataBound" ShowHeader="false" Width="160px">
                                <Columns>
                                    <asp:TemplateField HeaderText="Authors A-Z">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkAuthor" CssClass="seeming" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "index")%>'
                                                Text='<%# DataBinder.Eval(Container.DataItem, "index")%>' OnCommand="lnkAuthor_Command" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                </Columns>
                                <SelectedRowStyle BackColor="LightCyan" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" style="background-image: url('graphics/author_header.gif'); width: 710px; height: 19px;
                            font-size: 14; color: White; padding-left: 2px;background-repeat:no-repeat;">
                            <asp:Label ID="lblAuthorInfo" runat="server" Text="Auteurs / Componisten A" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataList ID="lstAuthors" runat="server" RepeatDirection="Vertical" RepeatColumns="2"
                                RepeatLayout="Table" CellPadding="2" CellSpacing="0" EnableViewState="false"
                                ShowFooter="False" ShowHeader="false" CssClass="AuthorList" Width="710px">
                                <ItemTemplate>
                                    <a class="seeming" href="SearchResult.aspx?composer=<%# DataBinder.Eval(Container.DataItem, "composerid")%>">
                                        <%# DataBinder.Eval(Container.DataItem, "info")%></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" BackColor="#EFEFEF" />
                                <AlternatingItemStyle HorizontalAlign="Left" BackColor="White" />
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
