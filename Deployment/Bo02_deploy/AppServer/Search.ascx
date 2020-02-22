<%@ control language="C#" autoeventwireup="true" inherits="Search, Bo02" %>
<table cellpadding="0" runat="server" id="tabSearch" cellspacing="0" border="0" style="background-image: url('graphics/bgAdvanceSearch.gif');
    background-repeat: no-repeat; width: 313px; height: 50px">
    <tr>
        <td style="width: 313px; height: 32px" align="center">
            <table cellpadding="2" cellspacing="0" border="0">
                <tr valign="middle">
                    <td>
                        <select id="drpSearch" name="filter" runat="server" class="ComboHeight">
                        </select>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSearch" runat="server" Text="" Width="120px" CssClass="TxtboxHeight"></asp:TextBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="btnGo" ImageAlign="AbsMiddle" runat="server" ImageUrl="graphics/btn_go_en.png"
                            OnClick="btnGo_Click" CausesValidation="False"></asp:ImageButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left:0px" colspan="3">
                        <asp:LinkButton ID="lnkAuthors" runat="server" Text="composer/author A-Z" OnClick="lnkAuthors_Click"
                            CausesValidation="false" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <%--<tr id="tdAdv" runat="server">
        <td style="height: 18px" align="right">
            <div id="divAdvSearch" style="position: relative; top: 0px; left: -5px;">
                <a href="advancesearch.aspx" style="font-size: 8pt">Advanced search</a>
            </div>
        </td>
    </tr>--%>
</table>
