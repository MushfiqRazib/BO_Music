<%@ page language="C#" autoeventwireup="true" inherits="MusicPlayer, App_Web_musicplayer.aspx.cdcab7d2" theme="ThemeOne" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="include/style.css" rel="stylesheet" type="text/css" />

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Boeijenga</title>

    <script src="include/CommonFuctions.js" type="text/javascript"></script>
    <script language="JavaScript" src="include/player.js"></script>
    <script type="text/javascript">
   
    function Reassign()
    {        
       setTimeout('Assign',1000);  
    }
    
    function Assign()
    {        
        try{
            window.opener.ReAssign(self);         
        }
          catch(err)
                {}
    }
</script>
    <meta http-equiv="Expires" content="0">
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="Cache-Control" content="no-cache">
</head>
<body bgcolor="#F2E3E9">
		<form id="frmListenMusic">
			<table class="playlist_bg" align=center>
			    <tr><td height=90 colspan=3></td></tr>
				<tr width=10><td></td>
					<td valign="top" align=center>
						<script language="javascript">document.write(GetPlugins('<% =filename %>','boeijenga.swf'));</script>
					</td>
					<td width=10></td>
				</tr>
			    <tr><td height=5 colspan=3></td></tr>
				
			</table>
		</form>
        <script type="text/javascript">
            var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
            document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
        </script>
        <script type="text/javascript">
            try {
            var pageTracker = _gat._getTracker("UA-7658263-1");
            pageTracker._trackPageview();
            } catch(err) {}
        </script>	
	</body>
</html>
