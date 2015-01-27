<%@ Page language="c#" Codebehind="TaskManage.aspx.cs" AutoEventWireup="false" Inherits="hxyd_crm.TaskManage" %>
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
				</tr>
				<tr>
					<td align="right">����״̬</td>
					<td align="left"><asp:dropdownlist style="Z-INDEX: 0" id="DropDownList1" runat="server" Width="168px">
							<asp:ListItem Value="δ���䱣����������">������������</asp:ListItem>
							<asp:ListItem Value="δ������������">������������</asp:ListItem>
							<asp:ListItem Value="�ѷ���δ����">�ѷ���δ����</asp:ListItem>
							<asp:ListItem Value="�ѷ����ѽ���">�ѷ����ѽ���</asp:ListItem>
							<asp:ListItem Value="�����">�����</asp:ListItem>
						</asp:dropdownlist></td>
					<td align="right">��ʾ��¼��</td>
					<td><asp:textbox style="Z-INDEX: 0" id="TextBox1" runat="server">50</asp:textbox>��</td>
				</tr>
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
								<asp:TemplateColumn HeaderText="&lt;input type='checkbox' onclick = 'DGSelectAll(this,&quot;dgdCompany&quot;,&quot;chkSelect&quot;)' id='SelectAll'&gt;ȫѡ&lt;/span&gt;">
									<HeaderStyle Width="7%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<input id="chkSelect" type="checkbox" runat="server" NAME="chkSelect">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="personName" HeaderText="�ͻ�" ItemStyle-Width="10%">
								
								</asp:BoundColumn>
								<asp:BoundColumn DataField="brandNameCN" HeaderText="Ʒ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="car_model" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="licensePlate" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="average_mileage" HeaderText="ƽ�����"></asp:BoundColumn>
								<asp:BoundColumn DataField="curr_mileage" HeaderText="��ǰ���"></asp:BoundColumn>
								<asp:BoundColumn DataField="mileage" HeaderText="ά�����"></asp:BoundColumn>								
								<asp:BoundColumn DataField="personId" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="car_id" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="kuhu_no" Visible="False"></asp:BoundColumn>								
								<asp:BoundColumn DataField="expire_date" HeaderText="���յ���"  Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="insuranceCompany" HeaderText="���չ�˾"  Visible="False"></asp:BoundColumn>		
								<asp:BoundColumn DataField="userName" HeaderText="��ԼԱ"  Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="salesDate" HeaderText="�۳�����"  Visible="False" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:BoundColumn DataField="assign_data" HeaderText="��������"  Visible="False"></asp:BoundColumn>						
								<asp:BoundColumn DataField="keep_date" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="assign_data" HeaderText="��������"></asp:BoundColumn>								
								<asp:BoundColumn DataField="assign_type" HeaderText="����״̬"></asp:BoundColumn>
								<asp:BoundColumn DataField="username" HeaderText="������"></asp:BoundColumn>
								<asp:BoundColumn DataField="assign_role" HeaderText="����״̬"></asp:BoundColumn>	
								<asp:ButtonColumn Text=" �� �� " HeaderText="����" CommandName="View"></asp:ButtonColumn>
							</Columns>
						</asp:DataGrid></td>
				</tr>
				<tr>
					<td align="right">
						<asp:Label style="Z-INDEX: 0" id="lb_text" runat="server">������</asp:Label>&nbsp;
					</td>
					<td>
						<asp:DropDownList id="DropDownList2" runat="server" Width="111px" DataTextField="fullName" DataValueField="userId"></asp:DropDownList>
					</td>
					<td>
					</td>
					<td>
						<asp:Button style="Z-INDEX: 0" id="Button1" runat="server" CssClass="button" Text=" ��  ��"></asp:Button>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
