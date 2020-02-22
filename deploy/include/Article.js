document.write('<style type="text/css">.tabber{display:none;}<\/style>');


function GetRealId(partialid) {
    var re = new RegExp(partialid, 'g')
    var el = document.getElementsByTagName('input');
    var realId = "";
    for (var i = 0; i < el.length; i++) {
        if (el[i].type == 'file') {
            if (el[i].id.match(re)) {
                return el[i].id;
            }
        }
    }
    return '';
}
      
function DisplayImage() {
    var uploaderId = GetRealId('ImageUpload');
    var uploader = document.getElementById(uploaderId);
    var filename = uploader.value;
    document.getElementById("dvimage").innerHTML = "<img alt='' height='230px' width='260px' src='../img/"
                                + filename.substring(filename.lastIndexOf("\\") + 1) + "' />";
}

function DisplayMP3() {
    var uploaderId = GetRealId('Mp3Upload');
    var uploader = document.getElementById(uploaderId);
    uploader.value="";
    var filename = getQuerystring('articlecode',document.location.href)+".mp3";    
//    var filename = uploader.value;
    document.getElementById("dvmp3").innerHTML = "<iframe src='../MP3Player.aspx?" + filename + "' width='220px' style='padding-left: 30px; padding-top: 20px;border:none'></iframe>";
}
function DisplayPDF() {
    var uploaderId = GetRealId('PDFUpload');
    var uploader = document.getElementById(uploaderId);
    var filename = uploader.value;
    document.getElementById('dvflash').innerHTML = "<object height='230px'><param name='movie' value='../Resources/swf/flashfile.swf'><embed src='../Resources/swf/flashfile.swf' height='230px'></embed></object>";
    //var temp = document.getElementById('ifrmContainer');
    //temp.contentDocument.getElementById('dvflash').innerHtml='Hello Razon';
    //document.getElementById('dvflash').innerHTML='Hello Razon';
}

function SaveArticle(){
    alert('Saved.');
}
