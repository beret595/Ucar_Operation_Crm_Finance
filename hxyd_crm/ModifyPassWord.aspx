<%@ Page language="c#" Codebehind="ModifyPassWord.aspx.cs" AutoEventWireup="false" Inherits="hxyd_crm.ModifyPassWord" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>密码修改</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema"  content="http://schemas.microsoft.com/intellisense/ie5">
		<base target="_self">
		<script type="text/javascript" src="./js/business.js"></script>
		<LINK rel="stylesheet" type="text/css" href="./css/special.css">
		<LINK rel="stylesheet" type="text/css" href="./css/css.css">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE style="WIDTH: 100%; BORDER-COLLAPSE: collapse" border="1">
				<tr>
					<TD class="td_column">旧密码</TD>
					<td class="td_data">
						<asp:TextBox id="TxtOldPass" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<TD class="td_column">新密码</TD>
					<td class="td_data">
						<asp:TextBox id="TxtNewPass" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<TD class="td_column">确认密码</TD>
					<td class="td_data">
						<asp:TextBox id="TxtConfirmPass" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="2" align="center">
						<asp:Button id="btnSave" runat="server" Text="修  改" CssClass="button"></asp:Button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
