﻿<%@ page language="C#" autoeventwireup="true" inherits="MP3Player, App_Web_mp3player.aspx.cdcab7d2" theme="ThemeOne" %>
<%@ Register TagPrefix="pseudoengine" Namespace="PseudoEngine" Assembly="PseudoMp3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dvPlayer" runat="server">
         <pseudoengine:pseudomp3 id="PseudoMP31" runat="server">
         </pseudoengine:pseudomp3>
    </div>
    </form>
</body>
</html>
