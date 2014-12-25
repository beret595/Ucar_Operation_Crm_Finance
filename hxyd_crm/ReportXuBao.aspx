<%@ Page language="c#" Codebehind="ReportXuBao.aspx.cs" AutoEventWireup="false" Inherits="casey.hxyd_crm.Web.UI.ReportXuBao" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AgentKPI</title>
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
					<TD class="bg_column">联系日期</TD>
					<TD class="bg_data" colspan="3"><INPUT id="txtInterViewTime" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"
							readOnly size="26" name="txtInterViewTime" runat="server"> 至 <INPUT id="TxtEndTime" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" readOnly
							size="26" name="TxtEndTime" runat="server"></TD>
				</TR>
				<tr>
					<td colSpan="4" align="center"><asp:button id="btnQuery" runat="server" CssClass="button" Text="查  询"></asp:button><asp:button id="btnExport" runat="server" CssClass="button" Text="数据导出"></asp:button></td>
				</tr>
				<tr>
					<td class="sub_title" colSpan="4">查询结果</td>
				</tr> 
				<tr>
					<td colSpan="4"><asp:datagrid id="dgdAgentAPI" runat="server" Width="100%" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="#CCCCFF"></AlternatingItemStyle>
							<HeaderStyle BackColor="#CCCCFF"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="序号">
									<ItemTemplate>
										<%# this.dgdAgentAPI.CurrentPageIndex * this.dgdAgentAPI.PageSize + Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="contactState" HeaderText="联系状态"></asp:BoundColumn>
								<asp:BoundColumn DataField="single_date" HeaderText="成交日期"></asp:BoundColumn>
								<asp:BoundColumn DataField="phone" HeaderText="电话"></asp:BoundColumn>
								<asp:BoundColumn DataField="brand" HeaderText="piping"></asp:BoundColumn>
								<asp:BoundColumn DataField="model" HeaderText="车型"></asp:BoundColumn>
								<asp:BoundColumn DataField="VIN" HeaderText="VIN"></asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
