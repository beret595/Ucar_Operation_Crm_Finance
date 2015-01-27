<%@ Page language="c#" Codebehind="TaskManage.aspx.cs" AutoEventWireup="false" Inherits="hxyd_crm.TaskManage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>任务管理</title>
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
				</tr>
				<tr>
					<td align="right">案件状态</td>
					<td align="left"><asp:dropdownlist style="Z-INDEX: 0" id="DropDownList1" runat="server" Width="168px">
							<asp:ListItem Value="未分配保养提醒数据">保养提醒数据</asp:ListItem>
							<asp:ListItem Value="未分配其他数据">保险提醒数据</asp:ListItem>
							<asp:ListItem Value="已分配未接受">已分配未接受</asp:ListItem>
							<asp:ListItem Value="已分配已接受">已分配已接受</asp:ListItem>
							<asp:ListItem Value="已完成">已完成</asp:ListItem>
						</asp:dropdownlist></td>
					<td align="right">显示记录数</td>
					<td><asp:textbox style="Z-INDEX: 0" id="TextBox1" runat="server">50</asp:textbox>条</td>
				</tr>
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
								<asp:TemplateColumn HeaderText="&lt;input type='checkbox' onclick = 'DGSelectAll(this,&quot;dgdCompany&quot;,&quot;chkSelect&quot;)' id='SelectAll'&gt;全选&lt;/span&gt;">
									<HeaderStyle Width="7%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<input id="chkSelect" type="checkbox" runat="server" NAME="chkSelect">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="personName" HeaderText="客户" ItemStyle-Width="10%">
								
								</asp:BoundColumn>
								<asp:BoundColumn DataField="brandNameCN" HeaderText="品牌"></asp:BoundColumn>
								<asp:BoundColumn DataField="car_model" HeaderText="车型"></asp:BoundColumn>
								<asp:BoundColumn DataField="licensePlate" HeaderText="车牌"></asp:BoundColumn>
								<asp:BoundColumn DataField="average_mileage" HeaderText="平均里程"></asp:BoundColumn>
								<asp:BoundColumn DataField="curr_mileage" HeaderText="当前里程"></asp:BoundColumn>
								<asp:BoundColumn DataField="mileage" HeaderText="维护里程"></asp:BoundColumn>								
								<asp:BoundColumn DataField="personId" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="car_id" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="kuhu_no" Visible="False"></asp:BoundColumn>								
								<asp:BoundColumn DataField="expire_date" HeaderText="保险到期"  Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="insuranceCompany" HeaderText="保险公司"  Visible="False"></asp:BoundColumn>		
								<asp:BoundColumn DataField="userName" HeaderText="邀约员"  Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="salesDate" HeaderText="售车日期"  Visible="False" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:BoundColumn DataField="assign_data" HeaderText="分配日期"  Visible="False"></asp:BoundColumn>						
								<asp:BoundColumn DataField="keep_date" HeaderText="生成日期"></asp:BoundColumn>
								<asp:BoundColumn DataField="assign_data" HeaderText="分配日期"></asp:BoundColumn>								
								<asp:BoundColumn DataField="assign_type" HeaderText="案件状态"></asp:BoundColumn>
								<asp:BoundColumn DataField="username" HeaderText="接受人"></asp:BoundColumn>
								<asp:BoundColumn DataField="assign_role" HeaderText="任务状态"></asp:BoundColumn>	
								<asp:ButtonColumn Text=" 查 看 " HeaderText="操作" CommandName="View"></asp:ButtonColumn>
							</Columns>
						</asp:DataGrid></td>
				</tr>
				<tr>
					<td align="right">
						<asp:Label style="Z-INDEX: 0" id="lb_text" runat="server">接受人</asp:Label>&nbsp;
					</td>
					<td>
						<asp:DropDownList id="DropDownList2" runat="server" Width="111px" DataTextField="fullName" DataValueField="userId"></asp:DropDownList>
					</td>
					<td>
					</td>
					<td>
						<asp:Button style="Z-INDEX: 0" id="Button1" runat="server" CssClass="button" Text=" 保  存"></asp:Button>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
