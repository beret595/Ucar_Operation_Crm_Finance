<%@ Page language="c#" Codebehind="ReceiveManage.aspx.cs" AutoEventWireup="false" Inherits="hxyd_crm.ReceiveManage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>任务接受</title>
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
				<tr>
					<td align="right">客户名称</td>
					<td align="left">
						<asp:TextBox id="TextBox2" runat="server" Width="168px"></asp:TextBox></td>
					<td align="right">显示记录数</td>
					<td><asp:textbox style="Z-INDEX: 0" id="TextBox1" runat="server">50</asp:textbox>条</td>
				</tr>
				<tr>
					<td align="right">案件状态</td>
					<td align="left"><asp:dropdownlist style="Z-INDEX: 0" id="DropDownList1" runat="server" Width="168px">
							<asp:ListItem Value="全部" Selected="True">全部</asp:ListItem>
							<asp:ListItem Value="已分配">已分配未接受</asp:ListItem>
							<asp:ListItem Value="已接受">已分配已接受</asp:ListItem>
							<asp:ListItem Value="已完成">已完成</asp:ListItem>
						</asp:dropdownlist></td>
					<td></td>
					<td></td>
				<TR>
					<td colSpan="4" align="center"><asp:button style="Z-INDEX: 0" id="btnQuery" runat="server" CssClass="button" Text="查  询"></asp:button></td>
				</TR>
			</TABLE>
			<table style="WIDTH: 100%; BORDER-COLLAPSE: collapse" border="1">
				<tr>
					<td colspan="4">
						<asp:DataGrid id="dgdCompany" Width="100%" runat="server" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="#CCCCFF"></AlternatingItemStyle>
							<HeaderStyle BackColor="#CCCCFF"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="personName" HeaderText="客户"></asp:BoundColumn>
								<asp:BoundColumn DataField="brandNameCN" HeaderText="车型"></asp:BoundColumn>
								<asp:BoundColumn DataField="licensePlate" HeaderText="车牌"></asp:BoundColumn>
								<asp:BoundColumn DataField="salesdate" HeaderText="生成日期"></asp:BoundColumn>
								<asp:BoundColumn DataField="average_mileage" HeaderText="平均里程"></asp:BoundColumn>
								<asp:BoundColumn DataField="mileage" HeaderText="维护里程"></asp:BoundColumn>
								<asp:BoundColumn DataField="username" HeaderText="接受人"></asp:BoundColumn>
								<asp:BoundColumn DataField="assign_type" HeaderText="案件状态"></asp:BoundColumn>
								<asp:BoundColumn DataField="personId" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="car_id" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="kuhu_no" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="assign_role" HeaderText="任务状态"></asp:BoundColumn>
								<asp:ButtonColumn Text=" 接 受 " HeaderText="操作" CommandName="Select" ItemStyle-ForeColor="red" ItemStyle-Font-Bold="True"></asp:ButtonColumn>
								<asp:ButtonColumn Text=" 呼 叫 " HeaderText="操作" CommandName="Compl" ItemStyle-ForeColor="#000099" ItemStyle-Font-Bold="True"></asp:ButtonColumn>
							</Columns>
						</asp:DataGrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
