<%@ Page language="c#" Codebehind="DataChange.aspx.cs" AutoEventWireup="false" Inherits="hxyd_crm.DataChange" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>数据迁移</title>
		<base target="_self">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="JavaScript" src="./js/datepicker/WdatePicker.js"></SCRIPT>
		<script type="text/javascript" src="./js/business.js"></script>
		<LINK rel="stylesheet" type="text/css" href="./css/special.css">
		<LINK rel="stylesheet" type="text/css" href="./css/css.css">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE style="WIDTH: 100%; BORDER-COLLAPSE: collapse" border="1">
				<TR>
					<td align="center">
						<asp:Button style="Z-INDEX: 0" id="Button2" runat="server" Text="数据迁移人员信息" CssClass="button"></asp:Button>
					</td>
				</TR>
				<tr>
					<td align="center">						
						<asp:Button style="Z-INDEX: 0" id="Button1" runat="server" Text="数据迁移车辆信息" CssClass="button"></asp:Button>
					</td>
				</tr>
				<tr>
					<td align="center">
						<asp:Label id="Label1" runat="server" Width="591px"></asp:Label>
					</td>
				</tr>
				<tr>
					<td align="center">
						<asp:Label id="Label2" runat="server" Width="591px"></asp:Label>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
