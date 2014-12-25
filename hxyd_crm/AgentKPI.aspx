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
					<TD class="bg_column">��ϵ����</TD>
					<TD class="bg_data" colspan="3"><INPUT id="txtInterViewTime" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"
							readOnly size="26" name="txtInterViewTime" runat="server"> �� <INPUT id="TxtEndTime" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" readOnly
							size="26" name="TxtEndTime" runat="server"></TD>
				</TR>
				<tr>
					<td colSpan="4" align="center"><asp:button id="btnQuery" runat="server" CssClass="button" Text="��  ѯ"></asp:button><asp:button id="btnExport" runat="server" CssClass="button" Text="���ݵ���"></asp:button></td>
				</tr>
				<tr>
					<td class="sub_title" colSpan="4">��ѯ���</td>
				</tr>
				<tr>
					<td colSpan="4"><asp:datagrid id="dgdAgentAPI" runat="server" Width="100%" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="#CCCCFF"></AlternatingItemStyle>
							<HeaderStyle BackColor="#CCCCFF"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="userID" HeaderText="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="���">
									<ItemTemplate>
										<%# this.dgdAgentAPI.CurrentPageIndex * this.dgdAgentAPI.PageSize + Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="userName" HeaderText="������"></asp:BoundColumn>
								<asp:BoundColumn DataField="begin_date" HeaderText="ͳ�ƿ�ʼ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="end_date" HeaderText="ͳ�ƽ�������"></asp:BoundColumn>
								
								<asp:BoundColumn DataField="success_num" Visible="False" HeaderText="�ɹ���"></asp:BoundColumn>
								<asp:BoundColumn DataField="renewal_num" Visible="False" HeaderText="�����ɹ���"></asp:BoundColumn>
								<asp:BoundColumn DataField="calls_num" Visible="False" HeaderText="�µ���"></asp:BoundColumn>
								<asp:BoundColumn DataField="invalid_num"  HeaderText="��Ч�绰"></asp:BoundColumn>
								<asp:BoundColumn DataField="call_num"  HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn DataField="zwyx_num"  HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="xygj_num"  HeaderText="��Ҫ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="khyy_num"  HeaderText="�ͻ�ԤԼ"></asp:BoundColumn>
								<asp:BoundColumn DataField="xbgj_num"  HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="arrive_num"  HeaderText="�ͻ�����"></asp:BoundColumn>
								<asp:BoundColumn DataField="success_rate" HeaderText="�ɹ���"></asp:BoundColumn>
								<asp:BoundColumn DataField="arrive_rate" Visible="False" HeaderText="������"></asp:BoundColumn>
								
							</Columns>
						</asp:datagrid></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
