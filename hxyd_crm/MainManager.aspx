<%@ Page language="c#" Codebehind="MainManager.aspx.cs" AutoEventWireup="false" Inherits="casey.hxyd_crm.Web.UI.MainManager" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML xmlns="http://www.w3.org/1999/xhtml">
  <HEAD>
		<title>U车汇-汽车服务改装中心--客户关系管理系统</title>
<meta name=GENERATOR content="Microsoft Visual Studio .NET 7.1">
<meta name=CODE_LANGUAGE content=C#>
<meta name=vs_defaultClientScript content=JavaScript>
<meta name=vs_targetSchema content=http://schemas.microsoft.com/intellisense/ie5>
<script type=text/javascript src="./js/jquery.js"></script>
<LINK rel=stylesheet type=text/css href='./js/navmenu/navmenu.css"'  ?>
<script type=text/javascript src="./js/jquery.blockUI.js"></script>
<script type=text/javascript src="./js/jquery-1.3.2.min.js"></script>
<script type=text/javascript src="./js/navmenu/hoverIntent.js"></script>
<script type=text/javascript src="./js/navmenu/jquery.bgiframe.min.js"></script>
<script type=text/javascript src="./js/navmenu/superfish.js"></script>
<script type=text/javascript src="./js/navmenu/supersubs.js"></script>
<SCRIPT language=JavaScript src="./js/datepicker/WdatePicker.js"></SCRIPT>
<script type=text/javascript src="./js/business.js"></script>
<LINK rel=stylesheet type=text/css href="./css/special.css" ><LINK rel=stylesheet type=text/css href="./css/css.css" >
<style type=text/css>* {
	PADDING-BOTTOM: 0px; MARGIN: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 0px
}
BODY {
	TEXT-ALIGN: center; PADDING-BOTTOM: 5px; MARGIN: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; FONT: 12px "宋体" ,Arial,sans-serif, "Times New Roman"; BACKGROUND: url(images/body-bg.jpg) #d3e2e9 repeat-x; COLOR: #000; PADDING-TOP: 5px
}
.top_info {
	PADDING-LEFT: 12px; PADDING-RIGHT: 24px; FLOAT: right; HEIGHT: 40px; COLOR: #fff; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; PADDING-TOP: 1px
}
.top_info .bar {
	TEXT-ALIGN: right; LINE-HEIGHT: 18px; FLOAT: right; HEIGHT: 20px; CLEAR: both; PADDING-TOP: 2px
}
.top_info .MenuBar {
float:right;TEXT-ALIGN: center; PADDING-LEFT: 8px; PADDING-RIGHT: 8px; DISPLAY: inline; HEIGHT: 20px; COLOR: #fff; FONT-WEIGHT: normal; TEXT-DECORATION: none; PADDING-TOP: 2px; top_info: 3px
}
.top_info A:hover {
	BACKGROUND: #80afdb; COLOR: #fff; TEXT-DECORATION: underline
}
.top_info H2 {
	LINE-HEIGHT: 14px; DISPLAY: inline; COLOR: #ace; FONT-SIZE: 12px; FONT-WEIGHT: normal
}
#container {
	BORDER-BOTTOM: #599cd4 1px solid; TEXT-ALIGN: center; BORDER-LEFT: #599cd4 1px solid; PADDING-BOTTOM: 1px; MARGIN: 0px auto; PADDING-LEFT: 0px; WIDTH: 960px; PADDING-RIGHT: 0px; BACKGROUND: #fff; BORDER-TOP: #599cd4 1px solid; BORDER-RIGHT: #599cd4 1px solid; PADDING-TOP: 0px
}
#content {
	MARGIN: 0px auto; WIDTH: 100%; HEIGHT: 100%
}
#header {
	MARGIN: 0px auto; WIDTH: 100%; BACKGROUND: url(images/main-header-bg.jpg) #467aa7 no-repeat; HEIGHT: 80px
}
#navigation {
	WIDTH: 100%; CLEAR: both
}
.sf-menu {
	PADDING-RIGHT: 24px; PADDING-TOP: 8px
}
</style>

<style type=text/css>#menu {
	PADDING-BOTTOM: 0px; LIST-STYLE-TYPE: none; MARGIN: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; FLOAT: left; PADDING-TOP: 0px
}
#menu LI {
	PADDING-BOTTOM: 0px; LIST-STYLE-TYPE: none; MARGIN: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; FLOAT: left; PADDING-TOP: 0px
}
#menu LI A {
	TEXT-ALIGN: center; LINE-HEIGHT: 30px; WIDTH: 105px; DISPLAY: block; BACKGROUND: #3a4953; HEIGHT: 30px; COLOR: #fff; BORDER-RIGHT: #000 1px solid; TEXT-DECORATION: none
}
 #menu LI A:hover
        {
            background: #80afdb;
            color: #fff;
            text-decoration: underline;
        }
         #menu LI A:active
        {
            background: #80afdb;
            color: #fff;
            text-decoration: underline;
        }
#mobanwang_com li a:hover
{
	background: #80afdb;
            color: #fff;
            text-decoration: underline;
}
#mobanwang_com li a:active
{
background: #80afdb;
            color: #fff;
            text-decoration: underline;
}
#mobanwang_com li a:visited
{
            color: #fff;
            text-decoration: underline;
}
 .mainlevel ul {display:none; position:relative; z-index:10000; left:-5px;}

#div2 
{ 
display:none; 
font-size:12px; 
position:relative; 
left:10px; 
top:0px; 

width:100px; 
z-index:100; 
} 
</style>
		<!--
			var curmenuid = "-1";
			

			function init() {
				//adjust size
				setSize();
		
				//init menu
				$('ul.sf-menu').supersubs({
							minWidth : 15,
							maxWidth : 30,
							extraWidth : 1
						}).superfish({
							delay: 0,
							dropShadows: false
						});
				
				//fix ie6 bug
				if ($.browser.msie && parseInt($.browser.version) < 7) {
						$('.sf-menu').css('padding-top', '3px').find('ul').bgIframe({opacity:false});
				}
				
				//add bottom border
				$('ul.sf-menu ul li:last-child').each(function(){
					$("> a", this).css('border-bottom-width', '1px');
				});
				
				//find first menu
				$("ul.sf-menu a").each(function () {
					if(this.id.charAt(0) == 'm') {
						$(this).click();
						return false;
					}
				});
			}
			
			
			
			
			
			var systemname = document.title;
			function setTitle(text){
				var t = systemname;
				if (text) {
					t = text + " - " + t;
				}
				document.title = t;
			}
			
			function doMenu(menuid, menuurl){
				if (menuid == curmenuid) {
					return;
				}
				if (menuurl.length == 0) {
					return;
				}
			    
				showLoading(true);
			    
				curmenuid = menuid;
				setTitle($('#m' + menuid).text());
				setFrame(menuurl);
			}

			function doLog(){
				showLoading(true);
			    
				curmenuid = "-1";
			    setTitle('日志查询');
				setFrame('biz/Sys/UserLogQuery.aspx');
			}

			

			

			

			
			function showLoading(show, deplay){
				if (show === undefined || show === null) {
					show = true;
				}
				
				if (show == true) {
					$("#container").block({
						message: '<div id="divLoading"><img src="images/busy.gif" style="vertical-align : middle;"/><span style="font-size:12px;font-weight:bold;padding-left:3px;vertical-align : middle;">程序正在运行，请稍候...</span></div>',
						css: {
							border: '1px solid #aaccee',
							padding: '10px',
							width: '220px',
							backgroundColor: '#ffffff',
							color: '#07519a'
						},
						overlayCSS: {
							backgroundColor: '#e6e6e6'
						}
					});
				}
				else {
					var option = {};
					if (deplay === undefined || deplay === null) {
						deplay = 0;
					}
					option.fadeOut = deplay;
					$('#container').unblock(option);
					$('#container').css('cursor', 'default');
				}
			}
			-->
<script language=javascript>
		var curmenuid = "-1";
			var S = function(object){
				return document.getElementById(object);
			};
			var systemname = document.title;
				function setTitle(text){
				var t = systemname;
				if (text) {
					t = text + " - " + t;
				}
				document.title = t;
			}
			
			function showDiv(divName) 
			{ 
			document.getElementById(divName).style.display = "block"; 
			} 
			
$(document).ready(function(){
  
  $('li.mainlevel').mousemove(function(){
  $(this).find('ul').slideDown();//you can give it a speed
  });
  $('li.mainlevel').mouseleave(function(){
  $(this).find('ul').slideUp("fast");
  });
  
});
			
			//隐藏层 
			function hiddenDiv(divName) 
			{ 
			document.getElementById(divName).style.display = "none"; 
			} 
			function doExit1(){
			
			alert('begin');
				$.blockUI({
					message: null,
					overlayCSS: {
						backgroundColor: '#e6e6e6'
					}
				});
			    
				if (confirm('您确定要退出系统吗？')) {
					curmenuid = "-1";
					setTitle('退出');
					setTimeout('window.location = "Logout.aspx?action=exit";', 0);
				}
				else {
					$.unblockUI();
					setTimeout('S("main").focus();', 0);
				}
			}
			function doExit()
			{
				if (confirm('您确定要退出系统吗？')) {
				setTimeout('window.location = "Logout.aspx?action=exit";', 0);
				}
			}
			function doRelogin(){
			
			/*	$("#container").block({
					message: '<div><img src="images/busy.gif" style="vertical-align : middle;"/><span style="font-size:12px;font-weight:bold;padding-left:3px;vertical-align : middle;">程序正在重登录，请稍候...</span></div>',
					css: {
						border: '1px solid #aaccee',
						padding: '10px',
						width: '220px',
						backgroundColor: '#ffffff',
						color: '#07519a'
					},
					overlayCSS: {
						backgroundColor: '#e6e6e6'
					}
				});
			    
				curmenuid = "-1";
				setTitle('重登录');*/
				setTimeout('window.location = "Logout.aspx?action=relogin";', 0);
			}
			function doPassword(){
			openDialog('ModifyPassword.aspx',300,300);
			}
			function goHomePage(){
			//showLoading(true);
			setFrame('SearchCustomer.aspx');
			}
			function goCallPage(){
			//showLoading(true);
			setFrame('CallCustomer.aspx');
			}
			function goTaskPage(){
			//showLoading(true);
			setFrame('TaskManage.aspx');
			}
			function goTaskjieshou()
			{
			setFrame('ReceiveManage.aspx');
			}
			
			function goAddPage(){
			setFrame('ModifyCustomer.aspx');
			}
			function doManage(){
			setFrame('UserManager.aspx');
			}
			function doProfit(){
			setFrame('SearchProfit.aspx');
			}
			function doAgentKPI(){
			setFrame('AgentKPI.aspx');
			}
			function goXuBaoReport(){
			setFrame('ReportXuBao.aspx');
			}
			function doImport(){
			
			setFrame('CustomerImport.aspx');
			}
			function doCompany(){
			setFrame('InsurCompanyManager.aspx');
			}
			function doMileage(){
			setFrame('MileageManage.aspx');
			}
			function dochange()
			{
			setFrame("DataChange.aspx");
			}
			
			function setSize(){
				var height =  document.documentElement.offsetHeight;
				if (height > 0) {
					var h = (height - 200) + "px";
			        
					S("content").style.height = h;
					S("main").style.height = h;
				}
			}
			function setFrame(url){
				S("main").src = url;
			}
		</script>
<script type=text/javascript>
$(function(){
	$('#mobanwang_com li').hover(function(){
		$(this).children('ul').stop(true,true).show('slow');
	},function(){
		$(this).children('ul').stop(true,true).hide('slow');
	});
	
	$('#mobanwang_com li').hover(function(){
		$(this).children('div').stop(true,true).show('slow');
	},function(){
		$(this).children('div').stop(true,true).hide('slow');
	});
});
</script>
<style type="text/css">
* {
	margin:0;
	padding:0;
}
body {
}
a {
	text-decoration:none;
}
ul {
	list-style:none;
}
#mobanwang_com {
	height:32px;
	text-align:center;
	BACKGROUND: #3a4953;		
}
#mobanwang_com a {
}
#mobanwang_com li ul {
	display:none;
}
#mobanwang_com li ul li {
	float:none;
}
*html #mobanwang_com li ul li {
	display:inline;
}
#mobanwang_com li ul a {
	float:none;
	height:32px;
	line-height:32px;	
	text-transform:capitalize;
}
#mobanwang_com .height-auto {
	line-height:15px;
	padding:5px 10px;
}
.second-menu, .third-menu, .fourth-menu {
	position:absolute;
}
.first-menu li {
	float:left;
	COLOR: white;
	position:relative;
}
.first-menu a {
	float:left;
	display:block;
	padding:0 20px;
	height:35px;
	line-height:35px;
	border-top:0px solid #3a4953;
	border-left:0px solid #3a4953;
	border-bottom:0px solid #fff;
	border-right:0px solid #3a4953;
	WIDTH: 105px;
	COLOR: white;
}
.first-menu a:hover {
	BACKGROUND: #80afdb
	border-top:1px solid #5db1e0;
	border-left:1px solid #5db1e0;
	COLOR: white;
}
.first-menu a:active
  {
			height:35px;
            background: #80afdb;
            COLOR: white;
            text-decoration: underline;
    }
.second-menu {
	top:32px;
	right:0;
	
}
*html .second-menu {
   left:0;
}
.second-menu a {
	display:block;
	background:#3a4953;
	WIDTH: 105px;
	COLOR: white;
}
.second-menu a.mobanwang {
	background:#3a3a3a no-repeat right top;	
	COLOR: white;
}

.second-menu a.mobanwang:hover {
	background:#4698ca no-repeat right -32px;
	COLOR: white;
}
.second-menu a.mobanwang-02 {
	background:#3a3a3a url(image/mobanwang.gif) no-repeat right -64px;
}
.second-menu a.mobanwang-02:hover {
	background:#3a4953 url(image/mobanwang.gif) no-repeat right -110px;
}
.third-menu, .fourth-menu {
	width:177px;
	top:0;
	left:177px;
}
.third-menu a {
	background:#4c4c4c;
	font-weight:normal;
	border-top:1px solid #595959;
	border-left:1px solid #595959;
	border-bottom:1px solid #333;
	border-right:1px solid #333;
}
#subMgm {
	width:177px;
}
#subMgm .third-menu {
	left:177px;
}
#subMgm .fourth-menu {
	left:177px;
}
#subMusic, #subNews {
	width:177px;
}
</style>
</HEAD>
<body MS_POSITIONING="GridLayout">
<form id=form1 runat="server">
<div style="OVERFLOW: hidden" id=container>
<div id=header>
<div class=top_info>
<div class=bar>
<h2>欢迎您，<asp:label id=lbUserName runat="server"></asp:label></H2>&nbsp;&nbsp;&nbsp; 
<A onclick="doPassword(); " href="javascript:void(0);" >修改密码</A>| <A onclick=doRelogin(); href="javascript:void(0);" >重登录</A> <A onclick=doExit(); href="javascript:void(0);" >退出</A> </DIV></DIV>
<div id=navigation>
<div class=MenuBar>
<ul id=mobanwang_com class="first-menu"">
  <li id="li_homePage"><A onclick=goHomePage(); href="javascript:void(0);" >主页</A>
  
  </LI>   
  <li  id="li_CallPage"><A onclick=goCallPage(); href="javascript:void(0);" >呼叫页面</A> </LI>
  <li  id="li_TaskPage"><A href="javascript:void(0);" >任务管理</A> </LI>  
  <ul style="display: none;" id="subTask" class="second-menu">
  <li  id="li_Taskfenpei"  class="mobanwang"><A onclick=goTaskPage(); href="javascript:void(0);" >任务分配</A> </li>
   <li  id="li_Taskjieshou"  class="mobanwang"><A onclick=goTaskjieshou(); href="javascript:void(0);" >任务接受</A> </li>
  </ul>
  <li  id="li_AddPage"><A onclick=goAddPage(); href="javascript:void(0);" >新建客户</A> </LI>
  <li  id="li_Manage"><A onclick=doManage(); href="javascript:void(0);" >用户管理</A> 
  <li  id="li_Profit"><A onclick=doProfit(); href="javascript:void(0);" >利润管理</A> 
  <li  id="li_KPI" class="mainlevel"><A onclick=doAgentKPI(); href="javascript:void(0);"  >绩效管理</A>
  

  <!--      <ul id="sub_01">
            <li><a href="#" target="_blank">续保台帐</a></li>
            <li><a href="#" target="_blank">邀约台帐</a></li>
            <li><a href="#" target="_blank">其他</a></li>
      </ul>
  div id="div2" onmouseover="showDiv(this.id)" onmouseout="hiddenDiv(this.id)"> 
<A onclick=goXuBaoReport(); href="javascript:void(0);" > 续保台帐</A>
<A onclick=goInviteReport(); href="javascript:void(0);" > 邀约台帐</A>
</div

--> 

</li>
  
  <li  id="li_Data"><A  href="javascript:void(0);" >数据维护</A>
	 <ul style="display: none;" id="subMusic" class="second-menu">
    <li  id="li_Company" class="mobanwang" style="left:-37"><A onclick=doCompany(); href="javascript:void(0);" >保险维护</A>   
     </li>
        <li  id="li_Mileage" class="mobanwang" style="left:-37"><A onclick=doMileage(); href="javascript:void(0);" >里程维护</A> </li>
        <li  id="li_Import" class="mobanwang" style="left:-37"><A onclick=doImport(); href="javascript:void(0);" >信息导入</A></li>
        <li  id="li_Change" class="mobanwang" style="left:-37"><A onclick=dochange(); href="javascript:void(0);" >数据迁移</A></li>
    
   </ul>
  </li>
  <li  id="li_Exit"><A onclick=doExit(); href="javascript:void(0);" >退出登录</A> 
  </LI>
</UL></DIV></DIV>
<div id=image style="z-index:-1"><IMG src=".\images\hxyd_home.jpg" width="100%" height=80 > </DIV>
<div style="MARGIN: 2px; PADDING-LEFT: 5px" id=sys_title align=left>
<h2><font style="COLOR: #fff">U车汇-汽车服务改装中心--客户关系管理系统</FONT></H2></DIV></DIV>
<div id=content><iframe id="main" height="100%" src="./SearchCustomer.aspx" frameBorder=0 width="100%" name=main></IFRAME></DIV>

</DIV>
<div id="divSupport">
版权所有 肥猫创作室 
</div>
<script>
			setSize();
			</script>
</FORM>
	</body>
</HTML>
