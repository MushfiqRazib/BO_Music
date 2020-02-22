<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MP3Player.aspx.cs" Inherits="MP3Player" %>
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
