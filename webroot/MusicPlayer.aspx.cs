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
using System.IO;
using Npgsql;


public partial class MusicPlayer : BasePage
{
    public string filename;
    protected void Page_Load(object sender, EventArgs e)
    {        
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetNoStore();
        Response.Cache.SetNoServerCaching();
        Response.Cache.VaryByParams.IgnoreParams = true;

        if (!IsPostBack)
        {
            if (Request.Params["code"] != null)
            {
                string codes = string.Empty;                
                //string codes = Request.Params["code"].ToString().TrimEnd(',');
                string[] splitter = Request.Params["code"].ToString().TrimEnd(',').Split(new Char[] { ',' });
                for (int i = 0; i < splitter.Length; i++)
                {
                    codes += "'"+splitter[i]+"',";
                }
                codes = codes.TrimEnd(',');
                GeneratePlaylist(codes);             
            }
            else
            {
                GeneratePlaylist();                
            }
            filename = filename.Substring(filename.IndexOf("Boeijenga.xspf"));
        }
        
    }
    //private void GeneratePlaylist(string editionnrs)
    private void GeneratePlaylist()
    {
        string audioFile = "";
        string audioPath = System.Configuration.ConfigurationSettings.AppSettings["music-dir"];        
        
        string sqlSelect = @"select p.articlecode,(COALESCE(a.title,''))AS title,
                            (COALESCE(c.firstname,''))||' '||(COALESCE(c.middlename,''))||' '||(COALESCE(c.lastname,'')) AS composer
                            from playlist p left join article a on a.articlecode=p.articlecode
                            left join composer c on a.composer=c.composerid
                            where p.isactive=true
                            order by p.priority";
                            

        NpgsqlCommand cmd = new NpgsqlCommand(sqlSelect);

        DataTable musics = new DbHandler().GetDataTable(cmd);//if successfully executed then 
        filename = Request.PhysicalApplicationPath + @"player\Boeijenga" + ".xspf";
        StreamWriter writer = new StreamWriter(filename);
        writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        writer.WriteLine("<playlist version=\"1\" xmlns = \"http://xspf.org/ns/0/\">");     
        writer.WriteLine("<trackList>");

        foreach (DataRow music in musics.Rows)
        {           
            audioFile = GetAudioFileName(music["articlecode"].ToString());
            if (audioFile != "")
            {
                writer.WriteLine("<track>");                
                writer.WriteLine("<location>" + audioPath + audioFile + "</location>");               
                writer.WriteLine("<title>" + music["title"].ToString() + "</title>");
                writer.WriteLine("<creator>" + music["composer"].ToString() + "</creator>");                                
                writer.WriteLine("</track>");
            }
        }
        writer.WriteLine("</trackList>");
        writer.WriteLine("</playlist>");
        writer.Close();
    }


    private void GeneratePlaylist(string code)
    {
        string audioFile = "";
        string audioPath = System.Configuration.ConfigurationSettings.AppSettings["music-dir"];

//        string sqlSelect = @"select p.articlecode,(COALESCE(a.title,''))AS title,
//                            (COALESCE(c.firstname,''))||' '||(COALESCE(c.middlename,''))||' '||(COALESCE(c.lastname,'')) AS composer
//                            from playlist p left join article a on a.articlecode=p.articlecode
//                            left join composer c on a.composer=c.composerid
//                            where p.articlecode in("+ code+ @")
//                            order by p.priority";

        string sqlSelect = @"select a.articlecode,(COALESCE(a.title,''))AS title,
                            (COALESCE(c.firstname,''))||' '||(COALESCE(c.middlename,''))||' '||(COALESCE(c.lastname,'')) AS composer
                            from article a
                            left join composer c on a.composer=c.composerid
                            where a.articlecode in(" + code + @")";
                            


        NpgsqlCommand cmd = new NpgsqlCommand(sqlSelect);

        DataTable musics = new DbHandler().GetDataTable(cmd);//if successfully executed then 
        filename = Request.PhysicalApplicationPath + @"player\Boeijenga" + ".xspf";
        StreamWriter writer = new StreamWriter(filename);
        writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        writer.WriteLine("<playlist version=\"1\" xmlns = \"http://xspf.org/ns/0/\">");
        writer.WriteLine("<trackList>");

        foreach (DataRow music in musics.Rows)
        {
            audioFile = GetAudioFileName(music["articlecode"].ToString());
            if (audioFile != "")
            {
                writer.WriteLine("<track>");
                writer.WriteLine("<location>" + audioPath + audioFile + "</location>");
                writer.WriteLine("<title>" + music["title"].ToString() + "</title>");
                writer.WriteLine("<creator>" + music["composer"].ToString() + "</creator>");
                writer.WriteLine("</track>");
            }
        }
        writer.WriteLine("</trackList>");
        writer.WriteLine("</playlist>");
        writer.Close();
    }    



    private string GetAudioFileName(string edition)
    {        
       return edition + ".mp3";      
    }
}
