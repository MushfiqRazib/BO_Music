<%@ control language="C#" autoeventwireup="true" inherits="Playlist, Bo02" %>
<link href="include/style.css" rel="stylesheet" type="text/css" />

<%--<table width="139" cellspacing="0" cellpadding="0" border="1" align="center" style="height:250px;" class="playlist">
    <tr>
        <td>
            
        </td>
    </tr>    
</table>--%>
<div class="playlist" id="UcPlaylist" style=" display:block">
    <div style="width:284; height:427px; border: solid 3px #808080">
        <INPUT type="hidden" id="txtPlayList" runat="server">
        <div id="divB" align="center" class="menu" onclick="return ShowPlayListItems('divBPlaylist','divUPlaylist','Boeijenga Playlist');" >
            <%--<asp:Button ID="btnBoeijenga" runat="server" Text="Boeijenga Playlist" OnClientClick="return ShowPlayListItems('divBPlaylist','divUPlaylist');" />--%>
            Boeijenga Playlist
        </div>
        <div id="divU" align="center" class="menu" onclick="return ShowPlayListItems('divUPlaylist','divBPlaylist','User Playlist');" >
            <%--<asp:Button ID="btnUser" runat="server" Text="User Playlist"  OnClientClick="return ShowPlayListItems('divUPlaylist','divBPlaylist');" />--%>
            <%--<asp:LinkButton ID="btnUser" runat="server" CssClass="seeming" Text="User Playlist" OnClientClick="return ShowPlayListItems('divUPlaylist','divBPlaylist','User Playlist');"></asp:LinkButton>--%>
            User Playlist
        </div>
        <div style="padding-top:5px; padding-left:5px;">
            <asp:Label ID="lblCurrentPlaylist" runat="server" Text="Boeijenga Playlist" Font-Underline="true"></asp:Label>
        </div>
        <div id="divBPlaylist" style="height:350px; overflow:auto;padding:5px 5px 0px 5px; ">            
             <div style="height:325px; overflow:auto"> 
                <asp:Label ID="lblBoeijengaPlaylist" runat="server" Text=""></asp:Label>
             </div>    
             <div style="vertical-align:bottom;" align="right">
                 <asp:ImageButton ImageAlign="AbsMiddle" ID="btnPlay" runat="server" 
                    AlternateText="Play all" ToolTip="Play all" ImageUrl="~/graphics/btn_play.png" 
                    onmouseover="javascript:this.src='graphics/btn_play_over.png'" onmouseout="javascript:this.src='graphics/btn_play.png'"
                    OnClientClick="javascript:ClosePlayList();return OpenPlayer();" />
             </div>
        </div>
        
        <div id="divUPlaylist" style="height:350px; overflow:auto; visibility:hidden;padding:0px 5px 5px 5px ">
            <div style="height:325px; overflow:auto">               
                <asp:Label ID="lblUserPlaylist" runat="server" Text=""></asp:Label>
            </div>
            <div style="vertical-align:bottom;" align="right">
                    
                 <asp:ImageButton ImageAlign="AbsMiddle" ID="ImageButton1" runat="server" 
                    AlternateText="Play all" ToolTip="Play all" ImageUrl="~/graphics/btn_play.png" 
                    onmouseover="javascript:this.src='graphics/btn_play_over.png'" onmouseout="javascript:this.src='graphics/btn_play.png'"
                    OnClientClick="javascript:ClosePlayList();return Playall();" />
                    
                 <asp:ImageButton ImageAlign="AbsMiddle" ID="ImageButton2" runat="server" 
                    AlternateText="Remove all from playlist" ToolTip="Remove all from playlist" ImageUrl="~/graphics/btn_removefromlaylist.png" 
                    onmouseover="javascript:this.src='graphics/btn_removefromplaylist_over.png'" onmouseout="javascript:this.src='graphics/btn_removefromlaylist.png'"
                    OnClientClick="javascript:return DeletePlaylist();" />
             </div>
        </div>
    </div>
</div>
