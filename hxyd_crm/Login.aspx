<%@ Page Language="c#" CodeBehind="Login.aspx.cs" AutoEventWireup="false" Inherits="casey.hxyd_crm.Web.UI.Login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<title>登录 中远集团基本养老保险查询</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style type="text/css">
        BODY { PADDING-BOTTOM: 0px; MARGIN: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; FONT: 12px/160% Verdana; BACKGROUND: #eee; COLOR: #000; OVERFLOW: hidden; PADDING-TOP: 0px }
        IMG { BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px }
        INPUT { DISPLAY: block; FONT: 12px Verdana,Arial,sans-serif; FLOAT: left }
        LABEL { LINE-HEIGHT: 24px; DISPLAY: block; FLOAT: left; COLOR: #fff }
        P { PADDING-BOTTOM: 0px; MARGIN: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; DISPLAY: block; PADDING-TOP: 0px }
        .btn { BORDER-BOTTOM: #4a95c9 1px solid; BORDER-LEFT: #4a95c9 1px solid; PADDING-BOTTOM: 3px; LINE-HEIGHT: normal; FONT-VARIANT: normal; FONT-STYLE: normal; MARGIN: 2px; PADDING-LEFT: 4px; PADDING-RIGHT: 4px; BACKGROUND: #cfe8f5; FONT-SIZE: 12px; BORDER-TOP: #4a95c9 1px solid; CURSOR: pointer; FONT-WEIGHT: normal; BORDER-RIGHT: #4a95c9 1px solid; PADDING-TOP: 3px }
        .error { COLOR: #f00 }
        .infobar { MARGIN: 3px 0px 0px }
        .infobar1 { BORDER-BOTTOM: #fadc80 1px solid; BORDER-LEFT: #fadc80 1px solid; PADDING-BOTTOM: 3px; LINE-HEIGHT: 120%; MARGIN: 0px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px; BACKGROUND: #fff9e3; FLOAT: none; CLEAR: both; BORDER-TOP: #fadc80 1px solid; BORDER-RIGHT: #fadc80 1px solid; PADDING-TOP: 3px }
        .loginname { BORDER-BOTTOM: #666 1px solid; TEXT-ALIGN: left; PADDING-BOTTOM: 6px; PADDING-LEFT: 4px; WIDTH: 300px; PADDING-RIGHT: 0px; FONT: bold 14px Verdana; COLOR: #666; PADDING-TOP: 4px }
        .loginplan_bg { BORDER-BOTTOM: #d6d6d6 1px solid; TEXT-ALIGN: center; BORDER-LEFT: #d6d6d6 1px solid; MARGIN: 0px auto; WIDTH: 1000px; BACKGROUND: url(images/login-bg.jpg) #fff no-repeat center 50%; HEIGHT: 600px; BORDER-TOP: #d6d6d6 1px solid; BORDER-RIGHT: #d6d6d6 1px solid }
        .loginright { TEXT-ALIGN: left; MARGIN-TOP: 190px; WIDTH: 250px; FLOAT: right; MARGIN-RIGHT: 100px }
        .text { BORDER-BOTTOM: #d7d7d7 1px solid; BORDER-LEFT: #d7d7d7 1px solid; PADDING-BOTTOM: 2px; PADDING-LEFT: 2px; WIDTH: 170px; PADDING-RIGHT: 2px; HEIGHT: 20px; FONT-SIZE: 14px; BORDER-TOP: #d7d7d7 1px solid; BORDER-RIGHT: #d7d7d7 1px solid; PADDING-TOP: 2px }
        DIV.row { MARGIN: 8px 0px 0px; HEIGHT: 25px }
        LABEL.column { TEXT-ALIGN: left; PADDING-BOTTOM: 0px; PADDING-LEFT: 2px; WIDTH: 60px; PADDING-RIGHT: 0px; FONT-SIZE: 14px; PADDING-TOP: 0px }
        #VerifyArea { MARGIN: 0px; DISPLAY: none; FLOAT: left }
		</style>
		<script type="text/javascript">
			//window.onerror=function(){return true;};
            var S = function(object) {
	            return document.getElementById(object);
            };

            Object.prototype._attachEvent = Object.prototype.attachEvent;
            Object.prototype.attachEvent = function(eventType, method, type) {
	            if (document.all) {
		            return this._attachEvent(eventType, method);
	            } else {
		            return this.addEventListener(eventType.replace(/\bon/ig, ""), method,
				            type);
	            }
            }

            var bCheckVerifyCode = (<%=USE_CHECKCODE %> != '0');
            var bAlwaysShowVerifyCode = (false == true);

            function CheckName() {
	            if (!bCheckVerifyCode)
		            return;
	            if (bAlwaysShowVerifyCode)
		            return;

	            if (S("tbUserName").value != "") {
		            if (S("vfcode").src == "") {
			            changeImg();
		            }

		            S("VerifyArea").style.display = "block";
		            S("tbVerifyCode").enabled = true;
	            } else {
		            S("VerifyArea").style.display = "none";
		            S("tbVerifyCode").enabled = false;
	            }
            }

            function checkCookie() {
	            var agt, cookieEnabled, isSafari, number;

	            agt = navigator.userAgent.toLowerCase();
	            cookieEnabled = navigator.cookieEnabled;
	            isSafari = (agt.indexOf("safari") != -1 && agt.indexOf("chrome") == -1);// chrome
	            number = Math.random();
	            document.cookie = "CheckCookie=" + number + ";";

	            if (((document.cookie.indexOf(number) == -1 || !cookieEnabled) && !isSafari)
			            || (!cookieEnabled && isSafari)) {
		            S("infobarNoCookie").style.display = "block";
		            return false;
	            } else {
		            var date = new Date();
		            date.setTime(date.getTime() - 1000);
		            document.cookie = "CheckCookie=;expires=" + date.toUTCString() + ";";

		            return true;
	            }
            }

            function changeImg() {
	            S('vfcode').src = 'VerifyCode.aspx?id=' + Math.random();
            }

            function checkInput() {
	            if (!checkCookie()) {
		            return false;
	            }
	            if (S("tbUserName").value == "") {
		            showMsg("emptyUserName");
		            S("tbUserName").focus();
		            return false;
	            }

	            if (S("tbUserNo").value == "") {
		            showMsg("emptyUserNo");
		            S("tbUserNo").focus();
		            return false;
	            }

	            var len = S("tbUserNo").value.length;
	            if (len != 15 && len != 18) {
		            /*
		             * showMsg("errorUserNo"); S("tbUserNo").focus(); return false;
		             */
	            }

	            if (S("tbUserPassword").value == "") {
		            /*
		             * showMsg("emptyPassword"); document.all.tbUserPassword.focus(); return
		             * false;
		             */
	            }

	            if (S("tbUserPassword").value.length >= 100) {
		            showMsg("errorPassowrdTooLong");
		            S("tbUserPassword").focus();
		            return false;
	            }

	            if (bCheckVerifyCode) {
		            if (S("tbVerifyCode").value == "") {
			            showMsg("emptyVerifyCode");
			            S("tbVerifyCode").focus();
			            return false;
		            }
	            }

	            return true;
            }

            function showMsg(msgId, method, txt) {
	            var msg, msgTemplate;
	            msg = {
		            errorUserName : "您填写的用户名不正确，请重新输入。",
		            emptyUserName : "请填写用户名。",
		            errorUserNo : "您填写的身份证号不正确，请重新输入。",
		            emptyUserNo : "请填写身份证号。",
		            emptyPassword : "请填写用户密码。",
		            emptyVerifyCode : "请填写验证码。",
		            errorPassowrdTooLong : "用户密码不能超过100个字符。",
		            errorNamePassowrd : "您填写的用户或密码不正确，请再次尝试。",
		            errorVerifyCode : "您填写的验证码不正确。",
		            errorUserNameNo : "您填写的用户名或身份证号不正确，请重新输入。",
		            errorPassowrd : "您填写的密码不正确，请再次尝试。"
	            };

	            msgTemplate = '<div class="infobar error" id="%_id_%">%_msg_%</div>';
	            if (msgId != undefined && msgId != "") {
		            if (!txt)
			            txt = msg[msgId];
		            S("msgContainer").innerHTML = msgTemplate.replace(/%_msg_%/ig, txt).replace(/%_id_%/ig, msgId);
		            S("msgContainer").style.cssText = "margin-top:-4px;"
		            return true;
	            } else {
		            return false;
	            }
            }

            function init() {
				S("VerifyArea").style.display = (bAlwaysShowVerifyCode ? "block" : "none");
				
	            checkCookie();
	            CheckName();

	            S("tbUserName").attachEvent("onblur", CheckName);
	            S("tbUserNo").attachEvent("onfocus", CheckName);
	            S("tbUserPassword").attachEvent("onfocus", CheckName);

	            S("tbVerifyCode").value = "";
            }
		</script>
	</HEAD>
	<body onunload="document.getElementById('btnLogin').disabled = false;">
		<form id="Form1" method="post" onsubmit="return checkInput();" runat="server">
			<div class="loginplan_bg">
				<div class="loginright">
					<div class="loginname">
						登录</div>
					<div style="PADDING-BOTTOM: 0px; MARGIN: 10px 0px; PADDING-LEFT: 2px; PADDING-RIGHT: 0px; PADDING-TOP: 4px">
						<div id="msgContainer">
						</div>
						<noscript>
							<div class="infobar1">
								您的浏览器不支持或已经禁止网页脚本，您无法正常登录。</div>
						</noscript>
						<div class="infobar1" id="infobarNoCookie" style="DISPLAY: none">
							您的浏览器不支持或已经禁止使用Cookie，您无法正常登录。</div>
					</div>
					<div class="row">
						<label for="tbUserName" class="column">用户名：</label> <input id="tbUserName" name="tbUserName" tabindex="1" class="text" runat="server">
					</div>
					<div class="row">
						<label for="tbUserNo" class="column">身份证：</label> <input id="tbUserNo" maxlength="18" name="tbUserNo" tabindex="2" style="IME-MODE: disabled"
							class="text" runat="server">
					</div>
					<div class="row">
						<label for="tbUserPassword" class="column">密&nbsp;&nbsp;码：</label> <input type="password" id="tbUserPassword" name="tbUserPassword" tabindex="3" class="text"
							runat="server">
					</div>
					<div id="VerifyArea">
						<div class="row">
							<label for="tbVerifyCode" class="column">验证码：</label> <input id="tbVerifyCode" name="tbVerifyCode" tabindex="4" style="IME-MODE: disabled" autocomplete="off"
								maxlength="4" class="text" runat="server">
						</div>
						<div style="MARGIN: 6px 0px 0px 62px; COLOR: #ddd; CLEAR: both">
							按下图数字填写</div>
						<div style="MARGIN: 6px 0px 6px 62px; COLOR: #ddd">
							<script type="text/javascript">
                            document.write("<img id='vfcode' style='cursor:pointer;border:1px solid #e4eef9' onclick='changeImg()' alt='看不清楚？换一个'>");
							</script>
							<br>
							<a href="javascript:changeImg()" style="COLOR: #fffc00">看不清楚？&nbsp;换一个</a>
						</div>
					</div>
					<div style="PADDING-BOTTOM: 0px; PADDING-LEFT: 60px; PADDING-RIGHT: 10px; HEIGHT: 27px; CLEAR: both; PADDING-TOP: 12px">
						<asp:Button CssClass="btn" TabIndex="5" Text="登 录" ID="btnLogin" ToolTip="登录" runat="server"></asp:Button>
					</div>
				</div>
			</div>
			<script type="text/javascript">
        init();
			</script>
		</form>
	</body>
</HTML>
