var DEFAULT_RESULTSET = '_default_resultset';
var Open_Window_Width = eval(screen.width*15/16);
var Open_Window_Height = eval(screen.height*15/16);

function start(funcId) {
    return window.external.start(funcId);
}

function setParameter(paramName, paramValue) {
    return window.external.setParameter(paramName, paramValue);
}

function setParameterSet(setName) {
    return window.external.setParameterSet(setName);
}

function put(row, paramName, paramValue) {
    return window.external.put(row, paramName, paramValue);
}

function runCall() {
    var ret = window.external.runCall();
    showRunning(false);
    return ret;
}

function getResult(resultName) {
    var value = null;
    value = window.external.getResult(resultName);
    return value;
}

function setResultSet(setName) {
    return window.external.setResultSet(setName);
}

function getResultSetValue(colName) {
    var value = null;
    value = window.external.getResultSetValue(colName);
    return value;
}

function getRowCount() {
    return window.external.getRowCount();
}

function nextRow() {
    return window.external.nextRow();
}

function firstRow() {
    return window.external.firstRow();
}

function lastRow() {
    return window.external.lastRow();
}

function preRow() {
    return window.external.preRow();
}

function showMessage() {
    window.external.showMessage();
}

function openDialog(url, height, width) {
    /*try {
		if (height==null){
			height = Open_Window_Height ;
		}
		if (width==null){
			width = Open_Window_Width;
		}
        return window.external.showDialog(url, "", height, width);
    } catch (e) {
    }*/

    try {
    
        return window.showModalDialog(url, '', 'dialogHeight:' + height + 'px;dialogWidth:' + width + 'px;status=no;help=no;top=0;left=0;scroll=yes;menubar=no');
    } catch (e) {
    }
}

function openDialogWithMenuBar(url, height, width) {
    /*try {
		if (height==null){
			height = Open_Window_Height ;
		}
		if (width==null){
			width = Open_Window_Width;
		}
        return window.external.showDialog(url, "", height, width);
    } catch (e) {
    }*/

    try {
    
        return window.showModalDialog(url, '', 'dialogHeight:' + height + 'px;dialogWidth:' + width + 'px;status=yes;help=yes;top=0;left=0;scroll=yes;menubar=yes');
    } catch (e) {
    }
}


function openDialogWithParam(url, param, height, width) {
    try {
        return window.external.showDialog(url, param, height, width);
    } catch (e) {
    }

    try {
        return window.showModalDialog(url, param, 'dialogHeight:' + height + 'px;dialogWidth:' + width + 'px;status=no;help=no;top=0;left=0;scroll=yes;menubar=no');
    } catch (e) {
    }
}

function getDialogArguments() {
    return window.external.getDialogArguments();
}

function setDialogReturnValue(returnValue) {
    window.external.setDialogReturnValue(returnValue);
}

function showRunning(show) {
    if (show === undefined || show === null) {
        show = true;
    }

    try {
        window.external.showRunning(show);
    } catch (e) {
        try {
            if (parent && typeof parent.showLoading === 'function') {
                parent.showLoading(show);
            } else {
				document.body.style.cursor = "wait";
            }
        } catch (de) {
			alert(de);
        }
    }
}

function showPrintSetupDialog() {
    window.external.showPrintSetupDialog();
}

function setPrintSetupData(dw) {
    var Printer = window.external.GetPrintSetupData("Printer");
    dw.Modify("Datawindow.printer = '" + Printer + "'");
    var Collate = window.external.GetPrintSetupData("Collate");
    dw.Modify("Datawindow.print.Collate = '" + Collate + "'");
    var Color = window.external.GetPrintSetupData("Color");
    dw.Modify("Datawindow.print.Color = " + Color);
    var Copies = window.external.GetPrintSetupData("Copies");
    dw.Modify("Datawindow.print.Copies = " + Copies);
    var Duplex = window.external.GetPrintSetupData("Duplex");
    dw.Modify("Datawindow.print.Duplex = " + Duplex);
    // var MarginBotton = null;
    // MarginBotton = window.external.GetPrintSetupData("Margin.Bottom");
    // if (MarginBotton == null)
    // alert("MarginBotton == NULL");
    // dw.Modify("Datawindow.print.Margin.Bottom = " + MarginBottom);
    // var MarginLeft = window.external.GetPrintSetupData("Margin.Left");
    // dw.Modify("Datawindow.print.Margin.Left = " + MarginLeft);
    // var MarginRight = window.external.GetPrintSetupData("Margin.Right");
    // dw.Modify("Datawindow.print.Margin.Right = " + MarginRight);
    // var MarginTop = window.external.GetPrintSetupData("Margin.Top");
    // dw.Modify("Datawindow.print.Margin.Top = " + MarginTop);
    var Orientation = window.external.GetPrintSetupData("Orientation");
    dw.Modify("Datawindow.print.Orientation = " + Orientation);
    var PageSize = window.external.GetPrintSetupData("Paper.Size");
    dw.Modify("Datawindow.print.Paper.Size = " + PageSize);
    var PaperSource = window.external.GetPrintSetupData("Paper.Source");
    dw.Modify("Datawindow.print.Paper.Source = " + PaperSource);
    var Quality = window.external.GetPrintSetupData("Quality");
    dw.Modify("Datawindow.print.Quality = " + Quality);
    var Scale = window.external.GetPrintSetupData("Scale");
    dw.Modify("Datawindow.print.Scale = " + Scale);
}

function fillDataGrid(rsName, arrayFields, dgTbl) {
    var rtv;

    rtv = setResultSet(rsName);
    if (rtv < 0)
        return rtv;

    var row = firstRow();
    var rowTbl;
    var cell;
    while (row > 0) {
        var filedValue;
        rowTbl = dgTbl.insertRow();

        for (var i in arrayFields) {
            cell = rowTbl.insertCell();
            filedValue = getResultSetValue(arrayFields[i]);
            if (filedValue == null)
                return -1;

            cell.innerText = filedValue;
        }

        row = nextRow();
    }
    // dgTbl.Format();

    return 1;
}

function resetDataGrid(dgTbl) {
    while (dgTbl.rows.length > 1) {
        dgTbl.deleteRow(1);
    }
}

function parseDataWindow(dw) {
    var sFieldName;
    var sFieldsString = "";
    var count = dw.describe("datawindow.column.count");

    for (var i = 1; i <= count; i++) {
        sFieldName = dw.Describe("#" + i + ".Name").toLowerCase();
        if (i == 1)
            sFieldsString += sFieldName;
        else
            sFieldsString += "," + sFieldName;
    }
    if (sFieldsString == "")
        sFieldsString = null;

    return sFieldsString;
}

function importDataWindow(rsname, dw) {
    dw.reset();
    var sImportString = null;
    var sFieldsString = parseDataWindow(dw);
    sImportString = window.external.getImportString(sFieldsString, rsname,
			"\t", "\n", "\"");
    if (sImportString == null)
        return -1;

    dw.ImportString(1, sImportString, 0, 0, 0, 0, 0);
    sImportString = null;
    return 1;
}

function importChildDataWindow(rsname, dw, childColumn) {
    dw.getChild(childColumn);
    var dwChild = dw.GetChildObject();
    if (dwChild == null) {
        alert("获得子数据窗口出错!");
        return 0;
    }
    return importDataWindow(rsname, dwChild);
}

function putDataWindow(rsName, dwName, dwCols, dwRows) {
	if (rsName == null || rsName.length == 0) {
		alert("参数集名称不能为空。");
		return -1;
	}
	
	var dwObj = document.getElementById(dwName);
	if(dwObj == null) {
		alert("无法获取数据窗口对象[" + dwName + "]。");
		return -1;
	}
	
	if (dwCols != null && !powersi.isarray(dwCols)) {
		alert("列集合必须是数组对象。");
		return -1;
	}
	if (dwRows != null && !powersi.isarray(dwRows)) {
		alert("行集合必须是数组对象。");
		return -1;
	}
	
	setParameterSet(rsName.toLowerCase());
	
	var rowCount = dwObj.RowCount();
	if (rowCount == 0) {
		return 0;
	}
	
	var colNames = [];
	var colTypes = [];
	
	var colCount = 0;
	var colType = "";
	
	if (dwCols != null) {
		colCount = dwCols.length;
		for(var j = 0; j < colCount; j ++)
		{
			colNames[j] = dwCols[j].toLowerCase();
			colType = dwObj.Describe(colNames[j] + ".coltype").toLowerCase();
			if (colType.length >= 5) {
				colType = colType.substring(0, 5);
			}
			if (colType == "!") {
				colType = "";
			}
			colTypes[j] = colType;
		}
	} else {
		colCount = dwObj.Describe("datawindow.Column.Count") * 1;
		for(var j = 0; j < colCount; j ++)
		{
			colNames[j] = dwObj.Describe("#" + (j+1) + ".name").toLowerCase();
			colType = dwObj.Describe("#" + (j+1) + ".coltype").toLowerCase();
			if (colType.length >= 5) {
				colType = colType.substring(0, 5);
			}
			if (colType == "!") {
				colType = "";
			}
			colTypes[j] = colType;
		}
	}
	
	colCount = colNames.length;
	
	if (colCount == 0) {
		return 0;
	}
	
	var colName = "";
	var colValue = "";
	var rsRow = 0;
	var dwRow = 0;
	
	if (dwRows != null) {
		for(var i = 0; i < dwRows.length; i ++) {
			dwRow = dwRows[i] * 1;
			if (dwRow < 1 || dwRow > rowCount) {
				continue;
			}
			
			rsRow ++;
			for(var j = 0; j < colCount; j ++) {
				colName = colNames[j];
				colType = colTypes[j];
				colValue = "";
				if (colType != null && colType.length > 0) {
					colValue = dwObj.GetItemString(dwRow, colName, 0, false);		
				}// end cols
			
				put(rsRow, colName, powersi.tostring(colValue));
			}//end cols
		}//end rows
	} else {
		for(var i = 1; i <= rowCount; i ++) {
			dwRow = i;
			
			rsRow ++;
			for(var j = 0; j < colCount; j ++) {
				colName = colNames[j];
				colType = colTypes[j];
				colValue = "";
				if (colType != null && colType.length > 0) {
					colValue = dwObj.GetItemString(dwRow, colName, 0, false);		
				}// end colType
				
				put(rsRow, colName, powersi.tostring(colValue));
			}//end cols
		}//end rows
	}
	
	return rsRow;
}
			
function saveStringToFile(fileName, fileVersion, fileContent) {
    return window.external.saveStringToFile(fileName, fileVersion, fileContent);
}

function getStringFromFile(fileName, fileVersion) {
    return window.external.getStringFromFile(fileName, fileVersion);
}

function saveConfigString(name, value) {
    return window.external.saveConfigString(name, value);
}

function getConfigString(name) {
    return window.external.getConfigString(name);
}

function exportExcel(obj) {
    var vHtml = obj.outerHTML;
    var oXL = new ActiveXObject("Excel.Application");
    var oBook = oXL.Workbooks.Add();
    oBook.HTMLProject.HTMLProjectItems("Sheet1").Text = vHtml;
    oBook.HTMLProject.RefreshDocument();
    oXL.Visible = true;
    oXL.UserControl = true;
}

function findRow(obj, path) {
    if (obj == null || obj.rows.length < 1)
        return -1;

    return openDialogWithParam("../commonBiz/TableFindRow.aspx", obj, 120, 530);
}

function findRowByType(obj, path) {
    if (obj == null || obj.find_table == null || obj.find_table.rows.length < 1)
        return -1;
    return openDialogWithParam("biz/commonBiz/TableFindRowByType.aspx", obj, 120, 430);
}

function running() {
    showRunning(true);
}

/* 打印页面 */
function printPage() {
    window.external.printPage();
}

/* 打印预览页面 */
function printPagePreview() {
    window.external.printPreviewPage();
}

/* 打印设置 */
function printPageSetup() {
    window.external.printPageSetup();
}

/* 页面查找 */
function findPage() {
    window.external.findPage();
}

/*
* 文件下载 
* url:文件下载地址（使用相对路径，不能为空）
* name:本地文件名（为空使用服务器文件名）
*/
function downloadFile(url, name) {
    if (url == null || url.length == 0) {
		alert("文件下载地址不能为空");
		return;
    }
    
    var fileurl = "DownloadHandler.aspx?url="+encodeURI(url);
    
    if (name != null && name.toString().length > 0) {
        fileurl += "&fn=" + encodeURI(name);
    }
    
    document.location = fileurl;
}

/* 获取页面高度 */
function getPageHeight() {
    var height = document.body.clientHeight;
    if (height == 0) {
        height = document.documentElement.clientHeight;
    }
    return height;
}

/* 调整对象高度 */
function fitHeight(objname, height) {
    var obj = document.getElementById(objname);
    if (obj == null) {
        return;
    }

    var fit = getPageHeight() - parseInt(height);
    if (fit > 0) {
        obj.style.height = fit + "px";
    }
}

/* datagrid相关处理方法 */
function dg_mouseover(obj) {
    try {
        if (powersi.isrow(this)) {
            obj = this;
        }

        var css = obj.className;
        if (css.indexOf("DataGride_data_selected") >= 0)
            return;
        if (css.indexOf("light") >= 0) {
            obj.className = "DataGride_select_data_light";
        } else {
            obj.className = "DataGride_select_data_dark";
        }
    } catch (e) {

    }
}

function dg_mouseout(obj) {
    try {
        if (powersi.isrow(this)) {
            obj = this;
        }
        var css = obj.className;
        if (css.indexOf("DataGride_data_selected") >= 0)
            return;

        if (css.indexOf("light") >= 0) {
            obj.className = "DataGride_data_light";
        } else {
            obj.className = "DataGride_data_dark";
        }
    } catch (e) {
    }
}

function dg_click(obj) {
    try {
        if (powersi.isrow(this)) {
            obj = this;
        }

        var css = obj.className;
        if (css.indexOf("DataGride_data_selected") >= 0)
            return;

        var parent = obj.parentNode;
        if (parent) {
            var id = parent.id || parent.parentNode.id || '';
            if (id.length == 0) {
                id = 'tid'
						+ powersi.lpad(Math.round(Math.random() * 100000), 6,
								'0');
                if (powersi.istable(parent)) {
                    parent.id = id;
                } else {
                    parent.parentNode.id = id;
                }
            }

            var cacheid = 'table_click_' + id;
            if (id.length > 0) {
                var his = powersi.getarray(powersi.cache[cacheid]);
                powersi.each(his, function(i, o) {
                    if (o.className.indexOf("light") >= 0) {
                        o.className = "DataGride_data_light";
                    } else {
                        o.className = "DataGride_data_dark";
                    }
                });
                his.length = 0;
                his.push(obj);
                powersi.cache[cacheid] = his;
            }
        }

        if (css.indexOf("light") >= 0) {
            obj.className = "DataGride_data_selected_light";
        } else {
            obj.className = "DataGride_data_selected_dark";
        }
    } catch (e) {
    }
}

function dg_selected(tab) {
	return powersi.getarray(powersi.cache['table_click_' + tab.id]);
}
        
function dg_init(tableid, options) {
    try {
        var settings = {
            usestyle: true,
            css: 'DataGride_Frame',
            border: '1',
            cellpadding: '1',
            cellspacing: '0',
            rules: 'all',
            headersize: 1,
            headertype: 'normal', /* normal fix */
            header: {
                'normal': 'DataGride_head',
                'fixed': 'DataGride_fixed_head'
            },
            footersize: 0,
            footer: 'DataGride_footer',
            even: 'DataGride_data_light',
            odd: 'DataGride_data_dark',
            usehover: true,
            mouseover: dg_mouseover,
            mouseout: dg_mouseout,
            useclick: true,
            click: dg_click,
            maxrows: 5000
        };

        powersi.extend(settings, options);

        var tab = powersi.obj(tableid);
        if (tab == null) {
			return;
        }
        
        if (settings.usestyle) {
            tab.className = settings.css;
            tab.border = settings.border;
            tab.cellPadding = settings.cellpadding;
            tab.cellSpacing = settings.cellspacing;
            tab.rules = settings.rules;
        }
        
        if(settings.useclick) {
			 powersi.cache['table_click_' + tab.id] = [];
        }

        var len = tab.rows.length;
        if (len > 0) {
            var rowcss = '';
            var bodyrow = 0;
            var headerrow = settings.headersize || 0;
            var footerrow = settings.footersize
					? (len - settings.footersize)
					: len;
            var rowflag = 0;
            var i = 0;

            var largeflag = (len - settings.headersize - settings.headersize) > settings.maxrows
					? true
					: false;
            if (largeflag) {
                settings.usehover = false; // close hover
            }
            if (settings.usestyle == false && settings.usehover == false
					&& settings.useclick == false) {
                return;
            }

            for (i = 0; i < len; i++) {
                if (i < headerrow) {
                    rowflag = 1;
                } else if (i >= footerrow) {
                    rowflag = 2;
                } else {
                    rowflag = 0;
                }

                if (settings.usestyle) {
                    if (rowflag == 1) {
                        rowcss = settings.header[settings.headertype];
                        switch (settings.headertype) {
                            case 'fix':
                                break;
                        }
                    } else if (rowflag == 2) {
                        rowcss = settings.footer;
                    } else {
                        rowcss = bodyrow % 2 == 0
								? settings.even
								: settings.odd;
                        bodyrow++;
                    }
                    tab.rows[i].className = rowcss;
                }

                if (rowflag > 0) {
                    continue;
                }

                if (settings.usehover) {
                    tab.rows[i].onmouseover = settings.mouseover;
                    tab.rows[i].onmouseout = settings.mouseout;
                }
                if (settings.useclick) {
                    tab.rows[i].onclick = settings.click;
                }
            }
        }
    } catch (e) {
    }
}

/* powersi辅助对象 */
if (!powersi) {
    var powersi = {};
    powersi = {
        obj: function(name) {
            return document.getElementById(name);
        },

        val: function(name, value) {
            var o = this.obj(name);
            if (o == null) {
                return "";
            }
            if (value != undefined) {
                o.value = value;
            }
            return o.value;
        },

        focus: function(name) {
            var o = this.obj(name);
            if (o == null) {
                return;
            }
            window.setTimeout(function() {
                o.focus();
            }, 0);
        },

        show: function(name) {
            var o = this.obj(name);
            if (o == null) {
                return;
            }

            o.style.display = "";
        },

        hide: function(name) {
            var o = this.obj(name);
            if (o == null) {
                return;
            }

            o.style.display = "none";
        },

        check: function(name, type) {
            var o = this.obj(name);
            if (o == null) {
                return false;
            }

            var chkval = this.trim(o.value);
            var chktype = (type == undefined ? "str" : type.toLowerCase());
            var chkresult = false;

            switch (chktype) {
                case "str":
                    chkresult = !this.isnull(chkval);
                    break;
                case "num":
                    chkresult = this.isnum(chkval);
                    break;
                case "int":
                    chkresult = this.isint(chkval);
                    break;
                case "dec":
                    chkresult = this.isdec(chkval);
                    break;
                case "date":
                    chkresult = this.isdate(chkval);
                    break;
                case "time":
                    chkresult = this.istime(chkval);
                    break;
                case "datetime":
                    chkresult = this.isdatetime(chkval);
                    break;
                default:
                    chkresult = false;
            }

            return chkresult;
        },

        each: function(object, callback) {
            var name, i = 0, length = object.length;

            if (length == undefined) {
                for (name in object)
                    if (callback.call(object[name], name, object[name]) === false)
                    break;
            } else {
                for (var value = object[0]; i < length
						&& callback.call(value, i, value) !== false; value = object[++i]) {
                }
            }

            return object;
        },

        extend: function() {
            // copy reference to target object
            var target = arguments[0] || {}, i = 1, length = arguments.length, deep = false, options;

            // Handle a deep copy situation
            if (target.constructor == Boolean) {
                deep = target;
                target = arguments[1] || {};
                // skip the boolean and the target
                i = 2;
            }

            // Handle case when target is a string or something (possible in
            // deep copy)
            if (typeof target != "object" && typeof target != "function")
                target = {};

            // extend jQuery itself if only one argument is passed
            if (length == i) {
                target = this;
                --i;
            }

            for (; i < length; i++)
            // Only deal with non-null/undefined values
                if ((options = arguments[i]) != null)
            // Extend the base object
                for (var name in options) {
                var src = target[name], copy = options[name];

                // Prevent never-ending loop
                if (target === copy)
                    continue;

                // Recurse if we're merging object values
                if (deep && copy && typeof copy == "object"
								&& !copy.nodeType)
                // Never move original objects, clone them
                    target[name] = powersi.extend(deep, src
											|| (copy.length != null ? [] : {}),
									copy);

                // Don't bring in undefined values
                else if (copy !== undefined)
                    target[name] = copy;

            }

            // Return the modified object
            return target;
        },
        isvalue: function(o) {
            return typeof (o) !== 'undefined' && o !== null ? true : false;
        },

        isarray: function(o) {
            return this.isvalue(o)
					? Object.prototype.toString.apply(o) === '[object Array]'
					: false;
        },

        ismap: function(o) {
            return this.isvalue(o) ? typeof (o) === 'object' : false;
        },

        getarraysize: function(o) {
            return this.isarray(o) ? o.length : -1;
        },

        getarray: function(o) {
            return this.isarray(o) ? o : new Array();
        },

        getmap: function(o) {
            return this.ismap(o) ? o : new Object();
        },

        getstring: function(str) {
            return this.isvalue(str) ? String(str) : '';
        },

        tostring: function(o) {
            var str = "";
            if (this.isvalue(o)) {
                var t = Object.prototype.toString.apply(o);

                if (t === '[object Array]') {
                    var a = [];
                    for (var i = 0; i < o.length; i++) {
                        a.push(this.tostring(o[i]));
                    }
                    str = '[' + a.join(',') + ']';

                    a.length = 0;
                    a = null;
                } else if (t === '[object Date]') {
                    var y = o.getYear();
                    if (y < 1900) {
                        y += 1900;
                    }
                    var m = o.getMonth() + 1;
                    str = y + "-" + this.lpad(m, 2, '0') + "-"
							+ this.lpad(o.getDate(), 2, '0') + " "
							+ this.lpad(o.getHours(), 2, '0') + ":"
							+ this.lpad(o.getMinutes(), 2, '0') + ":"
							+ this.lpad(o.getSeconds(), 2, '0');
                } else if (t === '[object Object]') {
                    var a = [], k;
                    for (k in o) {
                        var vt = Object.prototype.toString.apply(o[k]);
                        if (vt === '[object Array]' || vt === '[object Object]') {
                            a.push('"' + k + '":' + this.tostring(o[k]));
                        } else {
                            a.push('"' + k + '":"' + this.tostring(o[k]) + '"');
                        }
                    }
                    str = '{' + a.join(',') + '}';

                    a.length = 0;
                    a = null;
                } else {
                    str = String(o);
                }
            }

            return str;
        },

		tojson: function(str) {
			return eval("(" + str + ")");
		},
		
        isnull: function(str) {
            if (str == "") {
                return true;
            }
            var regu = "^[ ]+$";
            var re = new RegExp(regu);
            return re.test(str);
        },

        isnum: function(str) {
            var regu = /^(\d+)$/;
            return regu.test(str);
        },

        isint: function(str) {
            var regu = /^[-]{0,1}[0-9]{1,}$/;
            return regu.test(this.val(str));
        },

        isdec: function(str) {
            if (this.isint(v))
                return true;
            var re = /^[-]{0,1}(\d+)[\.]+(\d+)$/;
            if (re.test(str)) {
                if (RegExp.$1 == 0 && RegExp.$2 == 0)
                    return false;
                return true;
            } else {
                return false;
            }
        },

        getmaxday: function(year, month) {
            if (month == 4 || month == 6 || month == 9 || month == 11)
                return "30";
            if (month == 2)
                if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
                return "29";
            else
                return "28";
            return "31";
        },

        isdate: function(str) {
            if (str.length != 10)
                return false;

            var dateList = str.split("-");
            if (dateList.length != 3)
                return false;

            var year = dateList[0];
            if (year > "2100" || year < "1900")
                return false;

            var month = dateList[1];
            if (month > "12" || month < "01")
                return false;

            var day = dateList[2];
            if (day > this.getmaxday(year, month) || day < "01")
                return false;

            return true;
        },

        istime: function(str) {
            if (str.length != 8)
                return false;
            var timeList = str.split(":");
            if (timeList.length < 1 || timeList.length > 3)
                return false;

            if (!this.isnum(timeList[0]) || !this.isnum(timeList[1])
					|| !this.isnum(timeList[2]))
                return false;

            var hour = parseInt(timeList[0]);
            if (hour >= 24 || hour < 0)
                return false;

            var minute = parseInt(timeList[1]);
            if (minute >= 60 || minute < 0)
                return false;

            var second = parseInt(timeList[2]);
            if (second >= 60 || second < 0)
                return false;

            return true;
        },

        isdatetime: function(str) {
            if (str.length != 19)
                return false;
            if (!this.isdate(str.substring(0, 10)))
                return false;
            if (str.charAt(10) != ' ')
                return false;
            return this.istime(str.substring(11));
        },

        /* table begin */
        istable: function(el) {
            return (el.nodeName == 'TABLE') ? true : false;
        },

        istbody: function(el) {
            return (el.nodeName == 'TBODY') ? true : false;
        },

        istfoot: function(el) {
            return (el.nodeName == 'TFOOT') ? true : false;
        },

        isthead: function(el) {
            return (el.nodeName == 'THEAD') ? true : false;
        },

        iscell: function(el) {
            return (el.nodeName == 'TD' || el.nodeName == 'TH') ? true : false;
        },

        isrow: function(el) {
            return (el.nodeName == 'TR') ? true : false;
        },
        /* table end */

        /* string begin */
        lpad: function(str, len, pad) {
            str = this.getstring(str);
            if (typeof (len) === "undefined") {
                var len = 0;
            }
            if (typeof (pad) === "undefined") {
                var pad = ' ';
            }

            if (len + 1 >= str.length) {
                str = Array(len + 1 - str.length).join(pad) + str;
            }

            return str;
        },

        rpad: function(str, len, pad) {
            str = this.getstring(str);
            if (typeof (len) === "undefined") {
                var len = 0;
            }
            if (typeof (pad) === "undefined") {
                var pad = ' ';
            }

            if (len + 1 >= str.length) {
                str = str + Array(len + 1 - str.length).join(pad);
            }

            return str;
        },

        trim: function(text) {
            return (text || "").replace(/^\s+|\s+$/g, "");
        },
        /* string end */

        /* debug begin */
        benchmark: function(s, d) {
            this.debug(s + "" + (new Date().getTime() - d.getTime()) + "ms");
        },

        debug: function(s) {
            if (typeof console != "undefined"
					&& typeof console.debug != "undefined") {
                console.log(s);
            } else {
                try {
                    window.external.writeLog("", s);
                } catch (e) {
                    alert(s);
                }
            }
        },
        /* debug end */

        cache: {}
    }// end of powersi funciton
} // end of powersi var

/* filter:过滤需要保留回车的对象，每个对象用;结尾，对象名大小写区分 */
function keyEnter(filter) {
	try{
		if (event.keyCode == 13) {
			var src = event.srcElement;
			if (src) {
				if (filter != null && filter.indexOf(src.name + ";") >= 0) {
					return;
				}
			}

			event.keyCode = 9;
		}
    }catch(ex){}
}

function keyBack(e) {
	try{
		if(event.keyCode == 8) {
			var tagFilter = "object|input|textarea|";
			var typeFilter = "text|textarea|password|";
			
			if((tagFilter.indexOf(event.srcElement.tagName.toLowerCase()) < 0) || (typeFilter.indexOf(document.activeElement.type.toLowerCase()) < 0)) {
				event.keyCode = 0;
                event.returnValue = false;   
            } else if (event.srcElement.readOnly || event.srcElement.disabled) {
				event.keyCode = 0;
                event.returnValue = false;	
			}
		}
	}catch(ex){}
}
	
if(document.all) {
	document.attachEvent("onkeydown", keyBack);
}