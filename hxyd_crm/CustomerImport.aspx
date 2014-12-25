<%@ Page language="c#" Codebehind="CustomerImport.aspx.cs" AutoEventWireup="false" Inherits="casey.hxyd_crm.Web.UI.CustomerImport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CustomerImport</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="JavaScript" src="./js/datepicker/WdatePicker.js"></SCRIPT>
		<LINK rel="stylesheet" type="text/css" href="./css/special.css">
		<LINK rel="stylesheet" type="text/css" href="./css/css.css">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE style="WIDTH: 100%; BORDER-COLLAPSE: collapse" border="1">
				<TR>
					<TD class="bg_column" width="20%" align="right">客户信息导入</TD>
					<TD class="bg_data" width="80%"><INPUT type="file" size="40" id="File1" runat="server">
						<asp:Button id="btnSave" runat="server" Text="导  入" CssClass="button"></asp:Button>
						<asp:Button id="Button1" runat="server" Text="下载模板" CssClass="button" style="Z-INDEX: 0"></asp:Button>
						<asp:Button id="btnError" runat="server" CssClass="button" Text="下载错误信息"></asp:Button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
