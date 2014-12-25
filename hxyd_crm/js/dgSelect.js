//所有的选择和取消
function DGSelectAll(obj,dgName,selectName)
{
	/*
	var colnum = document.all(dgName).rows.length+1;
	var controlname;
	if(obj.checked)	
		for(var i =1; i<=colnum; i++ )
		{
			controlname = dgName+"__ctl"+i+"_"+selectName;
			if(eval("document.all."+ controlname) != null)	
			{
				document.all(controlname).checked = true;
			}
		}							
	else
		for(var i =1; i<=colnum ; i++ )
			{
				controlname = dgName+"__ctl"+i+"_"+selectName;
				if(eval("document.all."+ controlname) != null)	
				{
					document.all(controlname).checked = false;
				}
	}*/
	//兼容1.1和2.0
	var dg = document.getElementById(dgName);
	if (typeof (dg) == 'undefined' || dg === null) {
		return;
	}
	
	var chk = obj.checked;
	var chks = dg.getElementsByTagName("INPUT");
	var sel = "_" + selectName;
	for(var i = 0; i < chks.length; i ++) {
		if (chks[i].type == "checkbox" && chks[i].id.indexOf(sel) >= 0) {
			chks[i].checked = chk;
		}
	}
}
