
	/******************************************************************************
					Getting the name of the Operting System
	******************************************************************************/
	function GetOSName(){
		if (navigator.appVersion.indexOf("Win")!=-1) OSName = "Windows";
		else if (navigator.appVersion.indexOf("Mac")!=-1) OSName = "MacOS";
		else if (navigator.appVersion.indexOf("X11")!=-1) OSName = "UNIX";
		else if (navigator.appVersion.indexOf("Linux")!=-1) OSName = "Linux";
		else OSName = "Unknown OS";
		return OSName;
	}

	/******************************************************************************
					Getting the name of the Browser
	******************************************************************************/
	function GetBRName(){
		if (navigator.appName.indexOf("Micro")!=-1)	BrName="IE";		//Microsoft Internet Explorer
		else if (navigator.appName.indexOf("Netsc")!=-1) BrName="NN";	//Netscape nevigator or Mozilla
		else BrName="Unknown Browser";
		return BrName;
	}


	/******************************************************************************
	*		This function returns 'plugins' string of a popular player based on
	*		Differnt Operating system
	*		
	*		Default Player:
	*			For Windows  OS   : Embedding Media Player Plugins
	*			For Macintos OS   : Embedding QuickTime Plugins
	*			For Unix or Linux : Embedding RealPlayer Plugins
	******************************************************************************/
//	function GetPlugins(OSName, BRName, filename){
	function GetPlugins(filename,player){	    
		plugIns = "";

	    /******************************************************************************
	    *		This is a plugin for flush audio player
	    ******************************************************************************/
	    
		path = location.href;
		path = path.substring(0,path.indexOf("MusicPlayer"));
        filename= path+"player/"+filename;        
        plugIns  = '<object type="application/x-shockwave-flash" width="405" height="190" data="player/'+player+'?playlist_url='+filename+'" >';
        plugIns += '<param name="movie" value="player/'+player+'?playlist_url='+filename+'" />';
        plugIns += '</object>';        
		return plugIns;
	}
