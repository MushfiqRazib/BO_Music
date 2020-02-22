using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Npgsql;

public partial class Playlist : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBoeijengaPlaylist();
            LoadUserPlaylist();
        }
    }

    private void LoadBoeijengaPlaylist()
    {
        string sqlSelect = @"select p.articlecode,(COALESCE(a.title,''))AS title,
                            (COALESCE(c.firstname,''))||' '||(COALESCE(c.middlename,''))||' '||(COALESCE(c.lastname,'')) AS composer
                            from playlist p left join article a on a.articlecode=p.articlecode
                            left join composer c on a.composer=c.composerid
                            where p.isactive=true
                            order by p.priority";


        NpgsqlCommand cmd = new NpgsqlCommand(sqlSelect);

        DataTable dt = new DbHandler().GetDataTable(cmd);

        lblBoeijengaPlaylist.Text = "<table border=0 cellspacing=1 cellpadding=1 >";
        foreach (DataRow dr in dt.Rows)
        {
            txtPlayList.Value += dr["articlecode"].ToString() + ",";
            lblBoeijengaPlaylist.Text += "<tr><td valign='top' width=10><img src=graphics/bullet.png></td><td valign='top' style='padding-bottom:3px;' class='PlayListFont'>" + dr["title"].ToString() + "</td></tr>";
        }
        lblBoeijengaPlaylist.Text += "</table>";
    }
    private void LoadUserPlaylist()
    {
        Page.RegisterStartupScript("userPlaylist", @"<script>GetUserPlaylist();</script>");
    }
}
