<%@ page title="" language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="route, App_Web_route.aspx.cdcab7d2" theme="ThemeOne" %>
<%@ MasterType VirtualPath="~/MainLayOut.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">
    <link href="include/kallol.css" rel="stylesheet" type="text/css" />
 <div class="content-header">
        <div class="content-header-container">
            <label id="lblNewsMore" runat="server">
                Route</label>
        </div>
    </div>
 <%--<script src="http://maps.google.com/maps?file=api&amp;v=2&amp;&amp;key=ABQIAAAAwgkZYXdhmAWYnjaOqDc8qRSISV-JvOm_xw-2wAnqB_ozB16mtxRMU8iQ25sKmxylRbAi-8UOE4ASqw" type="text/javascript">
 </script>

    <script src="include/googlemap.js" type="text/javascript"></script>
<script type="text/javascript">

function loadmap() 
{
      if (GBrowserIsCompatible()) {
        var point;
        var map=new GMap2(document.getElementById("map"));
         map.addControl(new GOverviewMapControl());
     map.enableDoubleClickZoom();
     map.enableScrollWheelZoom();
     map.addControl(new GMapTypeControl());
     map.addControl(new GSmallMapControl());
         var address='<img src="myimage.gif" width=150 height=40/><br/>' + 
         '<font size="2" face="Arial"><b>INDIA</b><br/><br/>Home.<br/>' + 
     'New York City<br/><br/>America<br/>Ph.: 23743823</font>';
         var marker = new GMarker(point);
         map.setCenter(point,17);
         map.addOverlay(marker);
         map.setMapType(G_HYBRID_MAP);
         GEvent.addListener(marker, "click", function() {
            marker.openInfoWindowHtml(address);});
         marker.openInfoWindowHtml(address); 
               
      }
    }
 </script>--%>


 <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAclK0B2lXQwV5lPy1rLiTFBSN1aiKepvDswXjKa4j2DDWdYvOjhQMO1tywqS8ObgP5dtO70AyyArhzA"
      type="text/javascript"></script>
    <script type="text/javascript">

        var map = null;
        var geocoder = null;

        function load(loc) {
            if (GBrowserIsCompatible()) {
                var point;
                var map = new GMap2(document.getElementById("map"));

                map.addControl(new GOverviewMapControl());
                map.enableDoubleClickZoom();
                map.enableScrollWheelZoom();
                map.addControl(new GMapTypeControl());
                map.addControl(new GSmallMapControl());
                //Boeijenga Music Hoofdweg 156 9341 BM Veenhuizen The Netherlands Phone: +31 (0) 592-304142 Fax: +31 (0) 592-304143
                var address = '<div style="background-color: white"><font size="2" face="Arial"><b>Boeijenga Music</b><br/><br/>Boeijenga Music<br/>Hoofdweg 156 <br/>9341 BM Veenhuizen<br/>The Netherlands <br/>Phone: +31 (0) 592-304142<br/>Fax: +31 (0) 592-304143</font></div>';
                //(53.038839151011814, 6.384000778198242)
                point = new GLatLng(53.03342, 6.38415);
            
                var marker = new GMarker(point);
                map.setCenter(point, 14);
                map.addOverlay(marker);
               // map.setMapType(G_HYBRID_MAP);
                GEvent.addListener(marker, "mouseover", function() { marker.openInfoWindowHtml(address); });
                marker.openInfoWindowHtml(address);
                map.setCenter(point, 14);
            }
        }
        $(document).ready(function() {
            load('1');
        });
        //]]>
    </script>

 
 

<div id="map" style="width: 100%; height: 600px"></div>


<%--
<iframe src ="http://maps.google.com/maps?f=d&hl=nl&geocode=&time=&date=&ttype=&saddr=Assen,+a28&daddr=Veenhuizen,+Hoofdweg+158&sll=53006945,6.335835&sspn=0.07468,0.160675&ie=UTF8&ll=53.012201,6.458588&spn=0.074671,0.160675&z=13&om=1" width="100%" height="700px">

</iframe>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SidebarPlace" Runat="Server">
  <div class="content-sidebar-header">
        <div class="sidebar-container">
            <label id="lblContact" runat="server">
                Contact Details</label>
        </div>
    </div>
    <div class="sidebar-content-body" style="height: 130px;">
        <table border="0" width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <strong>Boeijenga Music</strong>
                </td>
                <td height="25" style="width: 90px">
                    <center>
                        <asp:Button ID="btnContact" Text="Contact" runat="server" CausesValidation="False"
                       CssClass="button" onclick="btnContact_Click"      /></center>
                </td>
            </tr>
            <tr>
                <td>
                    Hoofdweg 156
                </td>
            </tr>
            <tr>
                <td>
                    9341 BM Veenhuizen
                </td>
            </tr>
            <tr>
                <td>
                    The Netherlands
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    Phone: +31 (0) 592-304142
                    <asp:Label ID="lblAvailable" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Fax: +31 (0) 592-304143
                </td>
            </tr>
            <tr>
                <td style="padding-top: 5px">
                   Email: <a class="pinklink" href="mailto:info@boeigengamusic.com">info@boeigengamusic.com</a>
                </td>
            </tr>
        </table>
    </div>
    
    
    <div class="content-sidebar-header">
        <div class="sidebar-container">
            <label id="Label1" runat="server">
                Contact Details</label>
        </div>
    </div>
   <div class="sidebar-content-body" style="height: 130px;">
        <ul class="contact-ul">
            <li><a href="javascript:void(0)" id="locationlink" runat="server" rel="s" >» Locatie  </a> </li>
            <li><a href="route.aspx" id="routelink" runat="server" rel="b"  class="category-link">» Route </a>
            </li>
            <li><a href="about.aspx" id="aboutlink" runat="server" rel="c"  class="category-link">» About </a>
            </li>
        </ul>
    </div>
</asp:Content>

