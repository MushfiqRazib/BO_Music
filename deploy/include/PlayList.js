// JScript File

    function AddToPlayList(edition,title){                   
    //debugger
        GetUserPlaylist();
        txtPlaylist = document.getElementById("_ctl0_playlist_txtPlayList");
        if(!IsExist(txtPlaylist.value,edition)){
            txtPlaylist.value += edition + ",";            
            lblPlaylist = document.getElementById("_ctl0_playlist_lblUserPlaylist"); 
            lblPlaylist.innerHTML +="<table border=0 cellspacing=1 cellpadding=1><tr><td valign='top' width=10 ><img src=graphics/bullet.png></td><td valign='top' style='padding-bottom:3px;' class='PlayListFont'>"+title+ "</td></tr></table>";
                    
        }

        SetPlaylistInCookie();
        return false;
    }


    function Playall(){
		sItems = document.getElementById("_ctl0_playlist_txtPlayList").value;
		sItems = sItems.substring(0,sItems.length-1);
		if(sItems != ""){
		    //OpenWindow('listenmusic.aspx?code='+sItems);
		    OpenPlayer(sItems);
		}
		else{
		    alert('Sorry... You have no music in the playlist.\nPlease add music in the playlist');
		}
		return false;
    }
    /*************************************************
        This function delete music from playlist as
        well as from cookies
    **************************************************/
    function DeletePlaylist(){
        document.getElementById("_ctl0_playlist_txtPlayList").value="";
        document.getElementById("_ctl0_playlist_lblUserPlaylist").innerHTML="";
        SetPlaylistInCookie();
        return false;
    }

    function GetUserPlaylist(){

        editions = GetCookie('editions');
        isNotNull=true;    
        titles  = GetCookie('titles');
        
        if(editions!=null && titles !=null){
            document.getElementById("_ctl0_playlist_txtPlayList").value = GetCookie('editions');
            document.getElementById("_ctl0_playlist_lblUserPlaylist").innerHTML = GetCookie('titles');          
            
        }
        else{
            document.getElementById("_ctl0_playlist_txtPlayList").value = "";
            document.getElementById("_ctl0_playlist_lblUserPlaylist").innerHTML = "";          
            
        }
    }

  
    function SetPlaylistInCookie(){
        SetCookie('editions',document.getElementById("_ctl0_playlist_txtPlayList").value,7);
        SetCookie('titles',document.getElementById("_ctl0_playlist_lblUserPlaylist").innerHTML,7);
    }
    /*************************************************
        Get the value of cookie according to the
        Cookie name
    **************************************************/
    function GetCookie(c_name){
        if (document.cookie.length>0){
            c_start = document.cookie.indexOf(c_name + "=");
            if (c_start!=-1){ 
                c_start = c_start + c_name.length + 1;
                c_end = document.cookie.indexOf(";",c_start);
                if (c_end==-1) c_end = document.cookie.length;
                return unescape(document.cookie.substring(c_start,c_end));
            } 
        }
        return null;
    }
    /*************************************************
        We need to add a relative amount of time to
        the current date. The basic unit of JavaScript
        time is milliseconds, so we need to convert the
        days value to Miliseconds.
    **************************************************/
    function SetCookie(cookieName,cookieValue,nDays) {
        var today = new Date();
        var expire = new Date();
        if (nDays==null || nDays==0) nDays=1;
        expire.setTime(today.getTime() + 3600000*24*nDays);
        document.cookie = cookieName+"="+escape(cookieValue)
                         + ";expires="+expire.toGMTString();
    }
    
    /*************************************************
        Check wether a search string is exist or not
        in a large string and returns true if exist
        otherwise false.
    **************************************************/
    function IsExist(largeText,searchText){
        return largeText.indexOf(searchText)==-1?false:true;
    }
    
