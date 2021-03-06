﻿<%@ page language="C#" enableeventvalidation="false" autoeventwireup="true" inherits="PropertyEditor, Bo02" theme="ThemeOne" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Property Editor</title>
    <link rel="stylesheet" type="text/css" href="Style/Main.css" />

    <script src="script/jquery-1.3.min.js" type="text/javascript"></script>

    <script>
       
        var dataSaved = false;
        var window_width  = 0;
        var window_hieght = 0;
        $(document).ready(function(){
            var arrIDs = $("#hdnDrpIDs").val().split(",");
            window_width = parseInt("<%=maxLabelLength %>") + parseInt("<%=inputBoxMaxLength %>") +100;
            AdjectMainContainerWidth();
            $.each(arrIDs,function(i,val){
                if(val != "")
                {
                    var leftAdjustment = 6;
                    var topAdjustment = 2;
                    if($.browser.msie && parseFloat($.browser.version) < 8 )
                    {
                       leftAdjustment = 1;
                       topAdjustment = 1;
                    }
                    
      
                    var _left =  $("#"+val).offset().left;
                    var _top =  $("#"+val).offset().top;
                    $("#txt"+val).css({left:_left+leftAdjustment}).css({top:_top+topAdjustment}).css({'z-index':500});
                    //$("#txt"+val).val($("#"+val).val());
                    var _width = $("#"+val).width() - 17;
                    var _height = $("#"+val).width();
                    $("#txt"+val).css({width:_width}).css({height:'14px'}).css({display:'block'});
                }
                
                
            });
           
            CalculateMainContainerHeight();
        });
        
       
        
        function AdjectMainContainerWidth()
        {
            $("#dvMainContainer").css({width:window_width+"px" });
           
        }
        function CalculateMainContainerHeight()
        {
            window_hieght =  $("#dvMainContainer").height();
           
        }
        
        function ShowCalander(btn)
        {
            $(btn).trigger("click");
            $("#caldate_popupDiv").css({'z-index':1000});
            $(btn).trigger("click");
        }
        
        function GetDDLSelectedValueToTextBox()
        {
            var arrIDs = $("#hdnDrpIDs").val().split(",");
            $.each(arrIDs,function(i,val){
                
                if($("#"+val).val() != null)
                {
                    $("#txt"+val).val($("#"+val).val());
                }
            });
        }
        
        
        function SelectDDLValue(drp,txtBxID)
        {
            $("#"+txtBxID).val($(drp).val());
        }
        
        function SaveData()
        {
           window.parent.SaveData(dataSaved);
        }
        
        function GetComfirmation(msg)
        {
            $("#hdnConfirmation").val('');
            if(confirm(msg))
            {
                $("#hdnConfirmation").val('yes');
                __doPostBack('btnSave','');
                //$("#btnOpslaan").trigger("click");
                
            }
            
        }
        
        function MarkDropdownRedError()
        {
           var ctrID = $("#hdnErrorCtrID").val();
           if(ctrID.match("^ddl") != null)
           {
                if($("#hdnErrorCtrID").val() != "")
                {
                    $("#txt"+$("#hdnErrorCtrID").val()).css({color:'red'});
                }
           }
           
        }
    </script>

</head>
<body id="peBody" bgcolor="#eeffdd">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <center>
        <div id="dvMainContainer" style="height: auto; padding-left: 25px;padding-right: 25px;margin-top:25px">
            <fieldset style="width: 100%; text-align: left">
                <legend><font style="font-size: large">Property Editor </font></legend>
                <br />
                <asp:Panel ID="container" runat="server">
                    <asp:Panel ID="pnlControlName" runat="server" Style="float: left">
                    </asp:Panel>
                    <asp:Panel ID="pnlControl" runat="server" Style="float: left">
                    </asp:Panel>
                </asp:Panel>
                <div style="clear: left; padding-top: 20px;">
                    <asp:Label ID="lblError" runat="server" Style="margin-left: 200px; border-bottom: 1px solid red"
                        Text=""></asp:Label>
                </div>
                <div style="clear: left; padding-top: 20px; text-align: right">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="peValidation">
                    </asp:Button>
                </div>
            </fieldset>
        </div>
    </center>
    <div id="dvPopUpGrid">
        <asp:Button Style="display: none" ID="bttnHidden" runat="server"></asp:Button>
        <ajaxToolkit:ModalPopupExtender ID="modalGrid" runat="server" BackgroundCssClass="modalBackground"
            TargetControlID="bttnHidden" PopupControlID="basketDiv" />
        <asp:Panel ID="basketDiv" runat="server" class="modalPopup" Style="display: none;
            width: 500px; position: relative; height: 450px;">
            <asp:UpdatePanel ID="upAddEdit" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div runat="server" id="divmpeAddEdit" class="modalPopupTitle">
                        <asp:Label ID="lblpopupHeader" runat="server" SkinID="popupheader" Text="Select Item"
                            Style="float: left; margin-left: 20px;" />
                        <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="Images/close.png" ImageAlign="Right"
                            AlternateText="Select date" OnClick="btnClose_Click" />
                    </div>
                    <div style="padding-top: 15px; overflow: auto; height: 300px">
                        <center>
                            <asp:GridView ID="gvPopup" runat="server" AllowSorting="true" AllowPaging="true"
                                PageSize="10" UseAccessibleHeader="true" OnSorting="gvPopup_Sorting" OnRowDataBound="gvPopup_RowDataBound"
                                OnPageIndexChanging="gvPopup_PageIndexChanging" AutoGenerateColumns="true">
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle BackColor="#EEEEEE" />
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                <SelectedRowStyle BackColor="#DFDF00" />
                            </asp:GridView>
                        </center>
                    </div>
                    <div style="padding: 10px 0 0 410px">
                        <asp:Button ID="btnOpslaan" OnClick="btnSelectValue_Click" runat="server" Style="width: 71px"
                            Text="Choose" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="hdnCurTargetControlIdForGridvalue" runat="server" />
    <asp:HiddenField ID="hdnDrpIDs" runat="server" />
    <asp:HiddenField ID="hdnConfirmation" runat="server" />
    <asp:HiddenField ID="hdnErrorCtrID" runat="server" />
    </form>
</body>
</html>
