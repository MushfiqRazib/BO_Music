// ******************************************************************************
// *** ***
// *** Author : Rashidul ***
// *** Date : 02-08-2009 ***
// *** Copyright : (C) 2004 HawarIT BV ***
// *** Email : info@hawarIT.com ***
// *** ***
// *** Description: ***
// *** This file is only used for Wrapper functions only ***
// *** No Object browser functions allowed to put here. ***
// ******************************************************************************

// var COMPONENT_BASE_PATH = "./Components/";
var PROPERTY_EDITOR_VIRTUALDIRECTORY = "PropertyEditor";
var COMPONENT_BASE_PATH = "http://localhost/OBTool/Components/";
var supplyOrder = true;

function LoadReportList() {
	var params = '{"roleName":"' + document.getElementById('hdnRoleName').value
			+ '"}';
	var serviceName = "GetReportList";
	var result = GetSyncJSONResult_Wrapper(serviceName, params);
	result = eval('(' + result + ')');
	result = eval('(' + result.d + ')');
	var reportList = result.reportList;
	var drpReportList = document.getElementById("drpReportList");
	for (var k = 0; k < reportList.length; k++) {
		addDrpOption(drpReportList, reportList[k].report_code,
				reportList[k].report_name);
	}

	var repCode_Cookie = Get_Cookie("REPORT_CODE");

	if (repCode_Cookie) {

		SelectCookieReport(repCode_Cookie);
	}

	LoadReportArguments();
}

function LoadReportArguments() {
	// *** Call wrapper for report arguments/parameters
	var reportCode = GetSelectedReport();
	var params = '{"reportCode":"' + reportCode + '"}';
	var serviceName = "GetReportArguments";
	var reportArgs = GetSyncJSONResult_Wrapper(serviceName, params);

	var reportArgs = eval('(' + reportArgs + ')');
	reportArgs = reportArgs.d.replace(/"/g, "\"");
	reportArgs = eval('(' + reportArgs + ')');
	var fieldList = reportArgs.fieldList;
	PopulateFieldList(fieldList, reportArgs.settings.field_caps);
	SetOBSettings(reportArgs);

	if (OBSettings.grid) {
		OBSettings.REPORT_CHANGED = true;
	}

	// ProcessOBParameters(reportArgs);

	var actionControl = Ext.get('actioncontrol');
	if (actionControl) {
		Ext.destroy(actionControl);
	}

	if (OBSettings.FUNCTION_LIST.length > 0) {
		new hit.OB.ActionControl({
					data : OBSettings.FUNCTION_LIST
				});
	}
}

function SetOBSettings(reportArgs) {
	var dbSettings = reportArgs.settings;
	var sqlmandatory = reportArgs.sqlmandatory;
	var functionlist = reportArgs.functionlist;
	var sqlkeyfields = reportArgs.sqlkeyfields;

	OBSettings.REPORT_CODE = dbSettings.report_code;
	OBSettings.REPORT_NAME = dbSettings.report_name;

	OBSettings.FIELD_CAPS = dbSettings.field_caps;
	OBSettings.SQL_SELECT = dbSettings.sql_select;
	OBSettings.GIS_THEME_LAYER = dbSettings.gis_theme_layer;
	OBSettings.SQL_FROM = dbSettings.sql_from;
	OBSettings.MULTI_SELECT = dbSettings.multiselect;
	OBSettings.DETAIL_KEY_FIELDS = sqlkeyfields;
	OBSettings.SQL_MANDATORY = sqlmandatory;
	OBSettings.FUNCTION_LIST = functionlist;
	OBSettings.SQL_FIELD_TYPE = reportArgs.fieldTypes;
	OBSettings.RENDER_DIV = "";

	try {
		if (dbSettings.report_settings) {
			dbSettings.report_settings = eval('('
					+ dbSettings.report_settings.replace(/@@@/g, '"') + ')');
		}
	} catch (e) {
		dbSettings.report_settings = null;
	}

	// *** Reset/keep a backup of database settings
	SetDatabaseFieldSettings(dbSettings);

	var cookieSettings = GetSettingsFromCookie();
	if (cookieSettings) {
		var cookieCheckResult = CheckSettings(cookieSettings);
		var cookie_set_stat = SetSettingsToCore(cookieSettings,
				cookieCheckResult);
	}
	if (cookie_set_stat != -1) {
		dbCheckResult = CheckSettings(dbSettings);
		var db_set_stat = SetSettingsToCore(dbSettings, dbCheckResult);
	}

	// *** Destroy all grids if exists
	OBSettings.DeleteExpandedRow();
	OBSettings.DestoryGrid('NormalGrid');
	OBSettings.DestoryGrid('GroupGrid');

	if (cookie_set_stat == -1 || db_set_stat == -1) // -1 means set somehow
	{

		OBSettings.COOKIE_CHECKED = true;
		SetDefaultGroupBy(OBSettings.SQL_GROUP_BY);
		ReOrderSqlSelectFields();

		// *** Obrowser starts up
		setTimeout(function() {
					InitReport()
				}, 1);

	} else {
		var errorMsg = "";
		if (cookie_set_stat != undefined) {
			errorMsg = "Cookie error: "
					+ SETTING_CHECK_MESSAGE[cookie_set_stat];
		}
		var dbMessage = "Database error: " + SETTING_CHECK_MESSAGE[db_set_stat];
		errorMsg += (errorMsg == "") ? dbMessage : ("\n" + dbMessage);
		alert("Report could not create due to: \n\n" + errorMsg);
	}
}

function SetDefaultGroupBy(sqlGroupBy) {
	if (sqlGroupBy) {
		var drpGroupBy = document.getElementById("drpGroupBy");
		for (k = 0; k < drpGroupBy.options.length; k++) {
			if (drpGroupBy.options[k].value.toUpperCase() == sqlGroupBy
					.toUpperCase()) {
				drpGroupBy.selectedIndex = k;
			}
		}
	}
}

function GetSelectedReport() {
	var drpReportList = document.getElementById("drpReportList");
	var reportCode = drpReportList.options[drpReportList.selectedIndex].value;

	return reportCode;
}

function GetGroupByField() {
	var drpReportList = document.getElementById("drpGroupBy");
	var fieldName = drpReportList.options[drpReportList.selectedIndex].value;
	return fieldName;
}

function PopulateFieldList(fieldList, fieldCaps) {
	var drpReportList = document.getElementById("drpGroupBy");
	while (drpReportList.options.length > 0) {
		drpReportList.remove(drpReportList.options.length - 1);
	}
	addDrpOption(drpReportList, "NONE", "No Selection");
	var fieldCaps = fieldCaps.split(';');
	var caption, fieldCapPair;
	for (var k = 0; k < fieldList.length; k++) {
		caption = fieldList[k];
		for (var i = 0; i < fieldCaps.length; i++) {
			fieldCapPair = fieldCaps[i].split('=');
			if (fieldCapPair[0] == caption) {
				caption = fieldCapPair[1];
				break;
			}
		}
		addDrpOption(drpReportList, fieldList[k], caption);
	}
}

// *** This method add's an option to the
// *** supplied dropdownlist box.
function addDrpOption(reportList, value, text) {
	var optn = document.createElement("OPTION");
	optn.text = text;
	optn.value = value;
	reportList.options.add(optn);
}

function ExecuteCustomFieldAction(customFields) {

	var currentFields = OBSettings.SQL_SELECT.split(';');

	for (var k = 0; k < currentFields.length; k++) {
		if (currentFields[k].indexOf(" AS ") > 0) {
			currentFields.splice(k, 1);
			k--;
		}
	}

	var currentCustomFields = customFields.split(';');
	for (var k = 0; k < currentCustomFields.length; k++) {
		currentFields.push(currentCustomFields[k]);
	}

	OBSettings.QB_ACTION = true;
	OBSettings.SQL_SELECT = currentFields.join(';');
	OBSettings.QB_CUSTOM_FIELDS = customFields;
	OBSettings.SQL_GROUP_BY = "NONE";
	OBSettings.ACTIVE_GRID = 'MAIN_GRID';
	var drpGroupList = document.getElementById("drpGroupBy");
	setSelectedIndex(drpGroupList, OBSettings.SQL_GROUP_BY);
	OBSettings.ShowMainLoadingImage();
	setTimeout(function() {
				OBSettings.CreateNormalGrid();
			}, 1);
}

function SetReportByWhereFromQB(sqlwhere) {
	OBSettings.SQL_WHERE = sqlwhere;
	Ext.get('txtSearch').dom.value = "";
	OBSettings.ACTIVE_GRID = 'MAIN_GRID';
	if (OBSettings.SQL_GROUP_BY != "NONE") {
		setTimeout(function() {
					OBSettings.CreateGroupByGrid();
				}, 1);
	} else {
		setTimeout(function() {
					OBSettings.CreateNormalGrid();
				}, 1);
	}
}

function SetReportGroupByFromQB(sqlorderby, qbgbselectclause, sqlgroupby) {

	OBSettings.SQL_ORDER_BY = sqlorderby;
	OBSettings.QB_GB_SELECT_CLAUSE = qbgbselectclause;
	OBSettings.QB_ACTION = true;

	OBSettings.SQL_GROUP_BY = sqlgroupby;
	Ext.get('txtSearch').dom.value = "";
	var drpGroupList = document.getElementById("drpGroupBy");
	OBSettings.ACTIVE_GRID = 'MAIN_GRID';
	if (qbgbselectclause) {
		setSelectedIndex(drpGroupList, OBSettings.SQL_GROUP_BY);
		OBSettings.GB_SQL_SELECT = OBSettings.SQL_GROUP_BY + ';'
				+ OBSettings.QB_GB_SELECT_CLAUSE;
		OBSettings.COOKIE_SELECTED_FIELDS = OBSettings.GB_SQL_SELECT;
		setTimeout(function() {
					OBSettings.CreateGroupByGrid();
				}, 1);
	} else {
		OBSettings.SQL_GROUP_BY = "NONE";
		OBSettings.SQL_ORDER_BY = "";

		setSelectedIndex(drpGroupList, "NONE");
		OBSettings.ShowMainLoadingImage();
		setTimeout(function() {
					OBSettings.CreateNormalGrid();
				}, 1);
	}
}

function setSelectedIndex(drpDownList, value) {
	for (var i = 0; i < drpDownList.options.length; i++) {
		if (drpDownList.options[i].value == value) {
			drpDownList.options[i].selected = true;
			return;
		}
	}
}

function ShowReportTab() {
	Ext.fly('nonReportTabPanel1').update('');
	var headerDiv = Ext.DomQuery.select("div[id='header-wrap']");
	var obrowserDiv = Ext.DomQuery.select("div[id='Obrowser']");
	headerDiv[0].className = 'showDiv';
	obrowserDiv[0].className = 'showDiv';

	var tabItems = Ext.getCmp('obTabs').items.items;
	for (var k = 1; k < tabItems.length; k++) {
		var nonReportTabPanel = Ext.DomQuery.select("div[id='nonReportTabPanel"
				+ k + "']");
		nonReportTabPanel[0].className = 'hideDiv';
	}
	SetDefaultTitleForEditTab();
}

function SetDefaultTitleForEditTab() {
	Ext.getCmp('editRecord').setTitle('Edit');
}
function ShowDetailTab() {
	var tabIndex = 1;
	ShowOtherTabsLoadingImage(tabIndex);
	UpdateTabPanelVisibility(tabIndex);
	setTimeout(function() {
				ShowDetailTabContent();
			}, 1);
	SetDefaultTitleForEditTab();

}

function ShowEditTab() {
	var tabIndex = 2;
	ShowOtherTabsLoadingImage(tabIndex);
	UpdateTabPanelVisibility(tabIndex);
	setTimeout(function() {
				ShowEditTabContent();
			}, 1);
	SetDefaultTitleForEditTab();

}
function ShowAddTab() {
	var tabIndex = 2;
	ShowOtherTabsLoadingImage(tabIndex);
	UpdateTabPanelVisibility(tabIndex);
	setTimeout(function() {
				ShowAddTabContent();
			}, 1);
	Ext.getCmp('editRecord').setTitle('Add');

}
function ShowPDFTab() {
	var tabIndex = 2;
	ShowOtherTabsLoadingImage(tabIndex);
	UpdateTabPanelVisibility(tabIndex);
	setTimeout(function() {
				LoadViewerContent('pdf', 'nonReportTabPanel' + tabIndex)
			}, 1);
}

function ShowDWFTab() {
	var tabIndex = 3;
	ShowOtherTabsLoadingImage(tabIndex);
	UpdateTabPanelVisibility(tabIndex);
	setTimeout(function() {
				LoadViewerContent('dwf', 'nonReportTabPanel' + tabIndex)
			}, 1);
}

function UpdateTabPanelVisibility(tabId) {
	var headerDiv = Ext.DomQuery.select("div[id='header-wrap']");
	var obrowserDiv = Ext.DomQuery.select("div[id='Obrowser']");
	headerDiv[0].className = 'hideDiv';
	obrowserDiv[0].className = 'hideDiv';
	var tabItems = Ext.getCmp('obTabs').items.items;
	for (var k = 1; k < tabItems.length; k++) {
		var nonReportTabPanel = Ext.DomQuery.select("div[id='nonReportTabPanel"
				+ k + "']");
		if (tabId == k) {
			nonReportTabPanel[0].className = 'showDiv';
			nonReportTabPanel[0].style.height = OBSettings
					.GetOtherTabPanelHeight();
		} else {
			nonReportTabPanel[0].className = 'hideDiv';
		}
	}
}

function LoadViewerContent(docType, tabId) {
	if (OBSettings.DETAIL_KEY_FIELDS != '') {
		var keyValues = OBSettings.GetDelimittedKeyValuePair('$').split('$');
		var url = './DocLoadHandler.ashx?REPORT_CODE=' + OBSettings.REPORT_CODE
				+ '&KEYLIST=' + keyValues[0] + '&VALUELIST=' + keyValues[1]
				+ '&TYPE=' + docType + '&SQL_FROM=' + OBSettings.SQL_FROM
				+ '&GETSTATUS=true';
		var statusCode = HttpRequest(url);
		var html;
		if (statusCode.indexOf("$$$$$") == -1) {
			var statusCodeParts = statusCode.split("$$$");
			url = './DocLoadHandler.ashx?RELFILEPATH=' + statusCodeParts[0]
					+ '&TYPE=' + docType;
			html = GetObjectTag(url, docType, statusCodeParts[1]);
		} else {
			html = BuildFailureMessage(statusCode.replace("$$$$$:", ""));
		}

		Ext.fly(tabId).update(html);
	} else {
		var html = BuildFailureMessage('No data found. <br/>Key fields may not be set for the report.');
		Ext.fly(tabId).update(html);
	}

}

var drawingUrl;
function GetObjectTag(url, docType, redlineDocExists) {
	var tag;
	if (docType == "pdf") {
		tag = '<object id="viewer" classid="clsid:CA8A9780-280D-11CF-A24D-444553540000" width="100%"'
				+ 'height="100%">'
				+ '<param name="SRC" value="'
				+ url
				+ '"/>'
				+ '<embed src="'
				+ url
				+ '"'
				+ '   width="100%" height="100%">'
				+ '<noembed> Your browser does not support embedded PDF files. </noembed>'
				+ '</embed>' + '</object>';
	} else {
		drawingUrl = url;
		tag = '<object id="ADViewer" classid="clsid:a662da7e-ccb7-4743-b71a-d817f6d575df" width="100%"'
				+ 'height="95%">'
				+ '<param name="SRC" value="'
				+ url
				+ '"/>'
				+ '<param name="ToolbarVisible" value="true"/>'
				+ '<param name="MarkupsVisible" value="true"/>'
				+ '<embed id="ADViewer" src="'
				+ url
				+ '"'
				+ 'width="100%" height="95%">'
				+ '<param name="ToolbarVisible" value="true"/>'
				+ '<param name="MarkupsVisible" value="true"/>'
				+ '<noembed> Your browser does not support embedded DWF files. </noembed>'
				+ '</embed>' + '</object>';

	}
	return tag;
}

function ShowDetailTabContent() {

	var tpl;

	if (OBSettings.DETAIL_KEY_FIELDS != ''
			&& IsCustomDetailEdit(OBSettings.REPORT_NAME)) {
		Ext.fly('nonReportTabPanel1').update('');
		var keyValues = OBSettings.GetDelimittedKeyValuePair('$').split('$');
		new Ext.Panel({
					id : 'iframeEdit',
					renderTo : 'nonReportTabPanel1',
					items : [{
						xtype : 'box',
						anchor : '',
						autoEl : {
							tag : 'iframe',
							id: 'ifrmContainer',
							cls : '',
							frameBorder : 0,
							fitToFrame : true,
							src : this.GetEditDetailUrl(OBSettings.REPORT_NAME,
									'detail', keyValues),
							width : Ext.getBody().getWidth(),
							height : Ext.getBody().getHeight()
						}
					}]
				});
	}

	else if (OBSettings.DETAIL_KEY_FIELDS != '') {
		var keyValues = OBSettings.GetDelimittedKeyValuePair('$').split('$');
		var url = "Details.aspx?REPORT_CODE=" + OBSettings.REPORT_CODE
				+ "&KEYLIST=" + keyValues[0] + "&VALUELIST=" + keyValues[1];
		var result = HttpRequest(url);
		Ext.fly('nonReportTabPanel1').update(result);
	} else {

		var html = BuildFailureMessage('No data found. <br/>Key fields may not be set for the report.');
		Ext.fly('nonReportTabPanel1').update(html);
	}

}

function ShowAddTabContent() {
	var tpl;
	Ext.fly('nonReportTabPanel2').update('');
	/*
	 * if (OBSettings.DETAIL_KEY_FIELDS != '' &&
	 * IsCustomDetailEdit(OBSettings.REPORT_NAME)) {
	 */
	if (OBSettings.DETAIL_KEY_FIELDS != ''
			&& IsCustomDetailEdit(OBSettings.REPORT_NAME)) {
		var keyValues = OBSettings.GetDelimittedKeyValuePair('$').split('$');
		new Ext.Panel({
					id : 'iframeEdit',
					renderTo : 'nonReportTabPanel2',
					items : [{
								xtype : 'box',
								anchor : '',
								autoEl : {
									tag : 'iframe',
									id: 'ifrmContainer',
									cls : '',
									frameBorder : 0,
									fitToFrame : true,
									src : this
											.GetAddUrl(OBSettings.REPORT_NAME),
									width : Ext.getBody().getWidth(),
									height : Ext.getBody().getHeight()
								}
							}]
				});

	} else {
		var html = BuildFailureMessage('No data found. <br/>Key fields may not be set for the report.');
		Ext.fly('nonReportTabPanel4').update(html);
	}
}

function ShowEditTabContent() {
	var tpl;
	Ext.fly('nonReportTabPanel2').update('');
	/*
	 * if (OBSettings.DETAIL_KEY_FIELDS != '' &&
	 * IsCustomDetailEdit(OBSettings.REPORT_NAME)) {
	 */
	if (OBSettings.DETAIL_KEY_FIELDS != ''
			&& IsCustomDetailEdit(OBSettings.REPORT_NAME)) {
		var keyValues = OBSettings.GetDelimittedKeyValuePair('$').split('$');
		new Ext.Panel({
					id : 'iframeEdit',
					renderTo : 'nonReportTabPanel2',
					items : [{
						xtype : 'box',
						anchor : '',
						autoEl : {
							tag : 'iframe',
							id:'ifrmContainer',
							cls : '',
							frameBorder : 0,
							fitToFrame : true,
							src : this.GetEditDetailUrl(OBSettings.REPORT_NAME,
									'edit', keyValues),
							width : Ext.getBody().getWidth(),
							height : Ext.getBody().getHeight()
						}
					}]
				});

	} else {
		var html = BuildFailureMessage('No data found. <br/>Key fields may not be set for the report.');
		Ext.fly('nonReportTabPanel4').update(html);
	}

}

function ShowOtherTabsLoadingImage(tabId) {
	// *** Show loading image
	Ext.get('nonReportTabPanel' + tabId).setHeight(OBSettings
			.GetOtherTabPanelHeight());
	Ext.fly('nonReportTabPanel' + tabId).update(OBSettings
			.GetLoadingPage("#000000"));
}

function GetAddUrl(reportName) {
	switch (reportName.toUpperCase()) {
	    case 'STOCKMANAGEMENT':
	        Ext.getCmp('editRecord').setTitle('Add Supply Order');
			return '../../admin/supplyorder.aspx';
		case 'ARTICLE' :
			return '../../admin/article.aspx?mode=add';
		default :
			return '../../admin/Record.aspx?tablename='
					+ reportName + '&mode=new';
	}
}

function GetEditDetailUrl(reportName, editDetail, keyValue) {
	var url;
	var key = keyValue[0];
	var value = keyValue[1];
	switch (reportName.toUpperCase()) {
		case 'ORDERMANAGEMENT' :
			if (editDetail == 'detail') {
				return '../../admin/OrderDetail.aspx?order=' + value;
			} else if (editDetail == 'edit') {
				return '../../admin/editorder.aspx?order=' + value;
			} else {
				return '../../admin/printOrder.aspx?order=' + value;
			}
			break;
		case 'INVOICEMANAGEMENT' :
			if (editDetail == 'detail') {
				return '../../Admin/InvoiceDetail.aspx?inv=' + value;
			} else if (editDetail == 'edit') {
				return '../../admin/editInvoice.aspx?inv=' + value;
			} else {
				return '../../Admin/InvoiceEnglish.aspx?Factuurnr=' + value;
			}
			break;

		case 'STOCKMANAGEMENT' :
			if (editDetail == 'detail') {
				url = '../../Admin/SupplyOrderDetail.aspx?orderNo=' + value;
			} else if (editDetail == 'edit') {
				if (supplyOrder) {
					Ext.getCmp('editRecord').setTitle('Edit Supply Order');
					url = '../../admin/supplyorder.aspx?orderNo=' + value;
				} else {
					Ext.getCmp('editRecord').setTitle('Edit Received Order');
					url = '../../admin/receiveorders.aspx?soid=' + value;
				}
			} else {
				url = '../../Admin/stockPrint.aspx?orderNo=' + value;
			}
			return url;
			break;

		case 'ARTICLE' :
			if (editDetail == 'detail') {
				return '../../admin/Article.aspx?mode=detail&articlecode='
						+ value;
			} else if (editDetail == 'edit') {
				return '../../admin/Article.aspx?mode=edit&articlecode='
						+ value;
			}
			break;
		default :
			if (editDetail == 'detail') {
				return '../../admin/Record.aspx?tablename='
						+ reportName + '&' + key + '=' + value + '&mode=view';
			} else {
				return '../../admin/Record.aspx?tablename='
						+ reportName + '&' + key + '=' + value + '&mode=edit';
			}
			break;

		/*
		 * default : break;
		 */
	}
}

function IsCustomDetailEdit(reportName) {
	/*
	 * switch (reportName.toUpperCase()) { case 'INVOICEMANAGEMENT' : return
	 * true; break; case 'ORDERMANAGEMENT' : return true; break; case
	 * 'STOCKMANAGEMENT' : return true; break; case 'ARTICLE' : return true;
	 * break; case 'CATEGORY' : return true; break; default : return false;
	 * break; }
	 */
	return true;
}

function BuildFailureMessage(msg) {
	var msgContent = "";
	msgContent += '<table height="100%" width="100%" >';
	msgContent += '  <tr>';
	msgContent += '    <td align="center" style="background-color:#eeffdd; color:#000000;">';
	msgContent += '      <b>' + msg + '</b>';
	msgContent += '    </td>';
	msgContent += '  </tr>';
	msgContent += '</table>';

	return msgContent;
}

function PermformDelete() {
	switch (OBSettings.REPORT_NAME.toUpperCase()) {
		case 'ORDERMANAGEMENT' :
			new OrderManagementFunctions().Remove(OBSettings
					.GetMultiSelectValues('VERWIJDEREN'));
			break;

		default :
			break;

	}

}

function IsEditUsingPE(reportName) {
	/*
	 * switch (reportName.toUpperCase()) { case 'INVOICEMANAGEMENT' : return
	 * false; break; case 'ORDERMANAGEMENT' : return false; break; case
	 * 'STOCKMANAGEMENT' : return false; break; case 'ARTICLE' : return false;
	 * break; case 'CATEGORY' : return false; break; default : return true;
	 * break; }
	 */
	return false;
}
function AddRecord(event) {
	OBSettings.StopPropagation(event);
	tabPanel.setActiveTab(Ext.getCmp('editRecord'));
	ShowAddTab();
}
function EDIT(event) {
	OBSettings.StopPropagation(event);
	supplyOrder = true;
	if (!IsEditUsingPE(OBSettings.REPORT_NAME)) {
		tabPanel.setActiveTab(Ext.getCmp('editRecord'));
	} else {
		var fieldValPair = OBSettings.GetDelimittedKeyValuePair('$').split('$');
		var tableNameParts = OBSettings.SQL_FROM.split('_');
		if (tableNameParts[tableNameParts.length - 1] == 'v') {
			tableNameParts.pop();
		}
		var tableName = tableNameParts.join('_');
		OpenChild(PROPERTY_EDITOR_VIRTUALDIRECTORY + "/Wrapper.aspx?tableName="
						+ OBSettings.SQL_FROM + "&fieldNames="
						+ fieldValPair[0] + "&fieldValues=" + fieldValPair[1]
						+ "&groupName=" + tableName, "PropertyEditor", true,
				500, 500, "no", "no", false);
	}
}
function PRINT(event) {
	OBSettings.StopPropagation(event);
	var keyValues = OBSettings.GetDelimittedKeyValuePair('$').split('$');
	var url = GetEditDetailUrl(OBSettings.REPORT_NAME, 'print', keyValues);
	window.open(url);

}
function RECEIVE(event) {
	OBSettings.StopPropagation(event);
	supplyOrder = false;
	if (!IsEditUsingPE(OBSettings.REPORT_NAME)) {
		tabPanel.setActiveTab(Ext.getCmp('editRecord'));
	}
}
function DETAIL(event) {
	OBSettings.StopPropagation(event);
	tabPanel.setActiveTab(Ext.getCmp('detailRecord'));
	ShowDetailTab();
}

function VIEW(value, event) {
	OBSettings.StopPropagation(event);
	alert(value);
}
function DELETE(event) {
	OBSettings.StopPropagation(event);
	// alert(value);
	PermformDelete();

}

function ZOOM(value, event) {
	OBSettings.StopPropagation(event);
	alert(value);
}

// function ADD(value, event) {
// OBSettings.StopPropagation(event);
// // alert();
// // var params = OBSettings.GetKeyValuesAsQueryString();
// // alert(params);
// // var fieldValPair = params.split('&')[0].split('=');
// // var popup = OpenChild("PEditor/Wrapper.aspx?tableName="+ this.SQL_FROM
// // +"&fieldName="+ fieldValPair[0]
// // +"&fieldValue="+fieldValPair[1]+"&groupName="+this.SQL_FROM,
// // "PropertyEditor", true, 920, 720, "no", "no", false);
// // popup.focus();

// }

function GetValues() {
	var result1 = OBSettings.GetMultiSelectValues('Operation1');
	var result2 = OBSettings.GetMultiSelectValues('Operation2');
	var result3 = OBSettings.GetFunctionParams('Operation2');
}

function SelectCookieReport(repCode) {
	var drpReportList = document.getElementById("drpReportList");
	for (var i = 0; i < drpReportList.options.length; i++) {
		if (drpReportList.options[i].value.toUpperCase() === repCode
				.toUpperCase()) {
			drpReportList.selectedIndex = i;
			return;
		}
	}
}

// **************************** Refresh section ***************************

function refresh_notify() {

	// *** set refresh click status
	setElementAttrib("btnRefresh", "blinkstat", "start");

	// *** Change refresh button caption.
	setElementAttrib("btnRefresh", "value", "Refresh!");

	// *** Blink refresh button.
	blink_button("btnRefresh", "on");

	// *** Set focus to this window.
	window.focus();
}

function blink_button(key, state) {

	// *** Get button element/object.
	var oBtn = getElement(key);
	var refreshStat = getElementAttrib("btnRefresh", "blinkstat");

	if (!oBtn || refreshStat == "stop") {
		oBtn.style.backgroundColor = "Transparent";
		// *** Change refresh button caption.
		setElementAttrib("btnRefresh", "value", "Refresh");
		return false;
	}

	// ***
	if (state == "on") {
		// *** Set 'on' interface.
		oBtn.style.backgroundColor = "#00334F";

		// *** Keep blinking...
		setTimeout("blink_button('" + key + "', 'off')", 500);

	} else {
		// *** Set 'off' interface.
		oBtn.style.backgroundColor = "Transparent";

		// *** Keep blinking...
		setTimeout("blink_button('" + key + "', 'on')", 500);
	}
}

function getElementAttrib(key, attrib) {
	var elem = getElement(key);

	// *** Element not found.
	if (!elem)
		return false;

	// *** Return value of specified attribute.
	return elem[attrib];
}

function setElementAttrib(key, attrib, value) {
	var elem = getElement(key);

	// *** Element not found.
	if (!elem)
		return false;

	// *** Set value of specified attribute.
	elem[attrib] = value;
}

function getElement(key) {
	// *** First check if specified key is element id.
	var elem = document.getElementById(key);

	if (!elem) {
		// *** Now check if specified key is element name.
		elem = document.getElementsByName(key);

		// *** Element array found, set element to first one.
		if (elem.length > 0)
			elem = elem[0];
	}

	if (!elem) {
		// *** Element not found, raise error message.
		alert("Could not find element '" + key + "'");

		return false;
	}

	return elem;
}