<%@ Page language="c#" Codebehind="ReceiveManage.aspx.cs" AutoEventWireup="false" Inherits="hxyd_crm.ReceiveManage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�������</title>
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
					<td align="right">�ͻ�����</td>
					<td align="left">
						<asp:TextBox id="TextBox2" runat="server" Width="168px"></asp:TextBox></td>
					<td align="right">��ʾ��¼��</td>
					<td><asp:textbox style="Z-INDEX: 0" id="TextBox1" runat="server">50</asp:textbox>��</td>
				</tr>
				<tr>
					<td align="right">����״̬</td>
					<td align="left"><asp:dropdownlist style="Z-INDEX: 0" id="DropDownList1" runat="server" Width="168px">
							<asp:ListItem Value="ȫ��" Selected="True">ȫ��</asp:ListItem>
							<asp:ListItem Value="�ѷ���">�ѷ���δ����</asp:ListItem>
							<asp:ListItem Value="�ѽ���">�ѷ����ѽ���</asp:ListItem>
							<asp:ListItem Value="�����">�����</asp:ListItem>
						</asp:dropdownlist></td>
					<td></td>
					<td></td>
				<TR>
					<td colSpan="4" align="center"><asp:button style="Z-INDEX: 0" id="btnQuery" runat="server" CssClass="button" Text="��  ѯ"></asp:button></td>
				</TR>
			</TABLE>
			<table style="WIDTH: 100%; BORDER-COLLAPSE: collapse" border="1">
				<tr>
					<td colspan="4">
						<asp:DataGrid id="dgdCompany" Width="100%" runat="server" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="#CCCCFF"></AlternatingItemStyle>
							<HeaderStyle BackColor="#CCCCFF"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="personName" HeaderText="�ͻ�"></asp:BoundColumn>
								<asp:BoundColumn DataField="brandNameCN" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="licensePlate" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="salesdate" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="average_mileage" HeaderText="ƽ�����"></asp:BoundColumn>
								<asp:BoundColumn DataField="mileage" HeaderText="ά�����"></asp:BoundColumn>
								<asp:BoundColumn DataField="username" HeaderText="������"></asp:BoundColumn>
								<asp:BoundColumn DataField="assign_type" HeaderText="����״̬"></asp:BoundColumn>
								<asp:BoundColumn DataField="personId" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="car_id" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="kuhu_no" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="assign_role" HeaderText="����״̬"></asp:BoundColumn>
								<asp:ButtonColumn Text=" �� �� " HeaderText="����" CommandName="Select" ItemStyle-ForeColor="red" ItemStyle-Font-Bold="True"></asp:ButtonColumn>
								<asp:ButtonColumn Text=" �� �� " HeaderText="����" CommandName="Compl" ItemStyle-ForeColor="#000099" ItemStyle-Font-Bold="True"></asp:ButtonColumn>
							</Columns>
						</asp:DataGrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
