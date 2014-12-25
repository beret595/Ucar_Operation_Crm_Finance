<%@ Page language="c#" Codebehind="AgentKPI.aspx.cs" AutoEventWireup="false" Inherits="casey.hxyd_crm.Web.UI.AgentKPI" %>
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
								<asp:BoundColumn Visible="False" DataField="userID" HeaderText="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="序号">
									<ItemTemplate>
										<%# this.dgdAgentAPI.CurrentPageIndex * this.dgdAgentAPI.PageSize + Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="userName" HeaderText="操作人"></asp:BoundColumn>
								<asp:BoundColumn DataField="begin_date" HeaderText="统计开始日期"></asp:BoundColumn>
								<asp:BoundColumn DataField="end_date" HeaderText="统计结束日期"></asp:BoundColumn>
								
								<asp:BoundColumn DataField="success_num" Visible="False" HeaderText="成功量"></asp:BoundColumn>
								<asp:BoundColumn DataField="renewal_num" Visible="False" HeaderText="续保成功量"></asp:BoundColumn>
								<asp:BoundColumn DataField="calls_num" Visible="False" HeaderText="致电量"></asp:BoundColumn>
								<asp:BoundColumn DataField="invalid_num"  HeaderText="无效电话"></asp:BoundColumn>
								<asp:BoundColumn DataField="call_num"  HeaderText="外呼总量"></asp:BoundColumn>
								<asp:BoundColumn DataField="zwyx_num"  HeaderText="暂无意向"></asp:BoundColumn>
								<asp:BoundColumn DataField="xygj_num"  HeaderText="需要跟进"></asp:BoundColumn>
								<asp:BoundColumn DataField="khyy_num"  HeaderText="客户预约"></asp:BoundColumn>
								<asp:BoundColumn DataField="xbgj_num"  HeaderText="续保跟进"></asp:BoundColumn>
								<asp:BoundColumn DataField="arrive_num"  HeaderText="客户到店"></asp:BoundColumn>
								<asp:BoundColumn DataField="success_rate" HeaderText="成功率"></asp:BoundColumn>
								<asp:BoundColumn DataField="arrive_rate" Visible="False" HeaderText="到店率"></asp:BoundColumn>
								
							</Columns>
						</asp:datagrid></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
