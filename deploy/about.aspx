<%@ page language="C#" masterpagefile="~/MainLayOut.master" autoeventwireup="true" inherits="about, App_Web_about.aspx.cdcab7d2" title="About us" theme="ThemeOne" %>
    
    <%@ Register TagPrefix="uc2" TagName="ContactWidget" Src="~/contact_detail_widget.ascx" %>

    
<%@ Register TagPrefix="uc1" TagName="Subscribe" Src="~/subscribe_widget.ascx" %>
 <%@ MasterType VirtualPath="~/MainLayOut.master" %>
<asp:content ID="Content3" ContentPlaceHolderID="headerPlaceHolder" runat="Server">

<script type="text/javascript">



    $(document).ready(function() {
    $('.menu-ul li a[href="about.aspx"]').addClass('active');
 
    $('.about-us-content > table a').addClass('pinklink');
    
    
    });

</script>

</asp:content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="Server">
   <div class="content-header">
        <div class="content-header-container">
            <label id="lblAboutUs" runat="server">
                About us</label>
        </div>
    </div>
    
    
    <div class="about-us-content" style="margin-top:20px;display:block;margin-bottom:20px;padding-left:10px;">
                     <strong>
                                <img src="graphics/winkel.jpg" alt="Winkel" width="309" height="462" hspace="10"
                                    align="left">De geschiedenis van Boeijenga Muziek</strong><br>
                            Boeijenga's Boek- en Muziekhandel werd opgericht in 1887 door Johannes Willem
                            Boeijenga, van oorsprong zilversmid en graveur. Het starten van een boekhandel bleek
                            een goede zet. In 1901 kwam zoon Fedde Boeijenga (2e generatie) ook in de zaak werken.
                            Naast de winkel werd een verzendboekhandel opgezet, terwijl ook werd rondgereisd
                            in de provincie Friesland om boeken te verkopen.
                            <br>
                            <br>
                            Inmiddels waren de Boeijenga's ook een uitgeverij begonnen.
                            <br>
                            Rond 1926 werd op vijftienjarige leeftijd de derde generatie Boeijenga, Johannes
                            Willem, zoon van Fedde, in het bedrijf opgenomen. Eigenlijk tegen wil en dank, want
                            Johannes Willem had een muzikale opleiding genoten met als doel zijn brood te verdienen
                            als musicus. Hij studeerde in Leeuwarden bij Jan Paardekooper en later bij diens
                            opvolger George Stam.
                            <br>
                            Toen Johannes Willem zijn muziekstudie afrondde, was er echter weinig emplooi voor
                            musici en zijn vader achtte het daarom beter dat ook hij maar in de zaak kwam.
                            <br>
                            Toch was het zijn vader Fedde die tegen het einde van de twintiger jaren van de
                            vorige eeuw - aan het Kleinzand in Sneek - begon met de verkoop van muziekboeken,
                            terwijl diens broer Johannes aan het Grootzand de kantoorboekhandel bleef leiden.
                            Dat duurde tot 1930. Toen ging Johannes in de journalistiek en kwam een einde aan
                            die activiteiten.
                            <br>
                            De slechte dertiger jaren braken aan.
                            <br>
                            <br>
                            De verzendboekhandel liep steeds slechter, maar de handel in muziekliteratuur ging
                            steeds beter. Populaire wijsjes, kerkbundels en dat soort dingen, gingen vlot over
                            de toonbank. Vader Boeijenga ging uitgaven verkopen, die nergens anders waren te
                            krijgen. Zoon Johannes Willem legde zich, gezien zijn muzikale achtergrond, toe
                            op de muziekuitgeverij. Nadat zijn vader in 1954 was overleden, begon de uitbouw
                            van de muziekhandel pas goed. Internationaal werden contacten gelegd. Vooral met
                            universiteiten in Amerika bestonden goede banden. Import kwam op gang uit landen
                            als bv. Spanje, Portugal, Duitsland en Frankrijk.
                            <br>
                            De activiteiten van Boeijenga begonnen in het buitenland de aandacht te trekken
                            en de handel in muziekliteratuur werd steeds belangrijker. Men gaf een eigen informatieblad
                            uit, na eerst met losse stencils gewerkt te hebben. Behalve bladmuziek werden ook
                            boeken over muziek-geschiedenis en over bijvoorbeeld orgelbouw verkocht.&nbsp;
                            <br>
                            <br>
                            In 1969 werd Fedde Boeijenga (4e generatie) in de zaak opgenomen. Toen zijn vader
                            overleed zette hij samen met zijn moeder de zaak voort. Na haar dood dreef hij de
                            zaak geheel alleen. Hij overleed zeer plotseling in april 2004 op 56-jarige leeftijd.
                            Hijzelf was vrijgezel en omdat ook verder in de familie geen opvolger kon worden
                            gevonden, kwam met zijn overlijden een einde aan een lange familietraditie.
                            <br>
                            Dat geldt gelukkig niet voor de onderneming. Boeijenga's Boek- en Muziekhandel
                            werd overgenomen door Wybe Sierksma, een vijftiger, amateur kerkorganist, die na
                            een loopbaan van dertig jaar in accountancy en organisatieadvies, zijn leven een
                            andere invulling wilde geven. Samen met zijn zoon Arjen heeft hij de activiteiten
                            voortgezet en daarnaast een nieuwe impuls gegeven aan het uitgeven van zowel bladmuziek
                            voor orgel als het (op bescheiden schaal) uitgeven van DVD- en CD producties rondom &nbsp;
                            het orgel en zijn muziek.
                            <br>
                            <br>
                            Aanvankelijk vanuit de vertrouwde locatie in Sneek, maar sinds 2005 vanuit een prachtig
                            historisch pand in Veenhuizen, gelegen in de provincie Drenthe (gemeente Noordenveld).
                            Ook daar weten de relaties (zowel uit binnen- als buitenland) Boeijenga Muziek weer
                            als vanouds te vinden.<br>
                            <br>
                            <table width="300" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="116" align="left" valign="top">
                                        <strong>Meer informatie:</strong> &nbsp;</td>
                                    <td width="184" align="left" valign="top">
                                        <a href="HistoryBoeijenga1.aspx" target="_top">Artikel Boeijenga historie
                                            - 1</a><br>
                                        <a href="HistoryBoeijenga2.aspx" target="_top">Artikel Boeijenga historie
                                            - 2</a></td>
                                </tr>
                            </table>
                 </div>   <div class="grid-pager-container"></div>    
               
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarPlace" runat="Server">
  <uc2:ContactWidget ID="ContactWidget1" runat=server />
   
    
    
     <uc1:Subscribe runat=server id="usbscribe" />
  
  
</asp:Content>