<%@ Page language="c#" Codebehind="UserManager.aspx.cs" AutoEventWireup="false" Inherits="hxyd_crm.UserManager" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>UserManager</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="./css/special.css">
		<LINK rel="stylesheet" type="text/css" href="./css/css.css">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE border="1" style="WIDTH:100%;BORDER-COLLAPSE:collapse">
				<tr>
					<td colspan="4" class="sub_title">�û���Ϣ����</td>
				</tr>
				<TR>
					<TD class="bg_column">�û���</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtUserName" runat="server"></asp:TextBox></TD>
					<TD class="bg_column">����</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtPassWord" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="bg_column">��ɫ</TD>
					<TD class="bg_data">
						<asp:DropDownList id="ddlRole" runat="server"></asp:DropDownList></TD>
					<TD class="bg_column">��λ</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtRemark" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="bg_column">��ʵ����</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtFullName" runat="server"></asp:TextBox></TD>
					<TD class="bg_column">��ϵ�绰</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtPhone" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="bg_column">��������</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtCreateTime" runat="server" Enabled="False"></asp:TextBox></TD>
					<TD class="bg_column">����¼����</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtLastUpdate" runat="server" Enabled="False"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="bg_column">Email</TD>
					<TD class="bg_data"><asp:TextBox id="TxtEmail" runat="server"></asp:TextBox></TD>
					<TD class="bg_column"></TD>
					<TD class="bg_data"></TD>
					<TD class="bg_column"></TD>
					<TD class="bg_data"></TD>
				</TR>
				<tr>
					<td colspan="4" align="center"><INPUT id="hdUserID" type="hidden" runat="server">
						<asp:Button id="btnQuery" runat="server" Text="��  ѯ" CssClass="button" style="Z-INDEX: 0"></asp:Button>
						<asp:Button id="btnAdd" runat="server" Text="��  ��" CssClass="button"></asp:Button>
						<asp:Button id="btnSave" runat="server" Text="��  ��" CssClass="button"></asp:Button>
						<asp:Button id="btnDel" runat="server" Text="ɾ  ��" CssClass="button"></asp:Button></td>
				</tr>
				<tr>
					<td colspan="4" class="sub_title">�û���Ϣ</td>
				</tr>
				<tr>
					<td colspan="4">
						<asp:DataGrid id="dgdUserInfo" Width="100%" runat="server" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="#CCCCFF"></AlternatingItemStyle>
							<HeaderStyle BackColor="#CCCCFF"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="userid" HeaderText="userid"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="���">
									<ItemTemplate>
										<%# this.dgdUserInfo.CurrentPageIndex * this.dgdUserInfo.PageSize + Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="username" HeaderText="�û���"></asp:BoundColumn>
								<asp:BoundColumn DataField="userpassword" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="role" HeaderText="��ɫ"></asp:BoundColumn>
								<asp:BoundColumn DataField="remark" HeaderText="��λ"></asp:BoundColumn>
								<asp:BoundColumn DataField="fullname" HeaderText="��ʵ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="phone" HeaderText="��ϵ�绰"></asp:BoundColumn>
								<asp:BoundColumn DataField="createTime" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="lastUpdate" HeaderText="����¼����"></asp:BoundColumn>
								<asp:BoundColumn DataField="delflag" HeaderText="״̬"></asp:BoundColumn>
								<asp:ButtonColumn Text="�༭" HeaderText="����" CommandName="Select"></asp:ButtonColumn>
							</Columns>
						</asp:DataGrid></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
