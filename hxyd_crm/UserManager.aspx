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
					<td colspan="4" class="sub_title">用户信息管理</td>
				</tr>
				<TR>
					<TD class="bg_column">用户名</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtUserName" runat="server"></asp:TextBox></TD>
					<TD class="bg_column">密码</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtPassWord" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="bg_column">角色</TD>
					<TD class="bg_data">
						<asp:DropDownList id="ddlRole" runat="server"></asp:DropDownList></TD>
					<TD class="bg_column">岗位</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtRemark" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="bg_column">真实姓名</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtFullName" runat="server"></asp:TextBox></TD>
					<TD class="bg_column">联系电话</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtPhone" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="bg_column">创建日期</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtCreateTime" runat="server" Enabled="False"></asp:TextBox></TD>
					<TD class="bg_column">最后登录日期</TD>
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
						<asp:Button id="btnQuery" runat="server" Text="查  询" CssClass="button" style="Z-INDEX: 0"></asp:Button>
						<asp:Button id="btnAdd" runat="server" Text="清  空" CssClass="button"></asp:Button>
						<asp:Button id="btnSave" runat="server" Text="保  存" CssClass="button"></asp:Button>
						<asp:Button id="btnDel" runat="server" Text="删  除" CssClass="button"></asp:Button></td>
				</tr>
				<tr>
					<td colspan="4" class="sub_title">用户信息</td>
				</tr>
				<tr>
					<td colspan="4">
						<asp:DataGrid id="dgdUserInfo" Width="100%" runat="server" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="#CCCCFF"></AlternatingItemStyle>
							<HeaderStyle BackColor="#CCCCFF"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="userid" HeaderText="userid"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="序号">
									<ItemTemplate>
										<%# this.dgdUserInfo.CurrentPageIndex * this.dgdUserInfo.PageSize + Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="username" HeaderText="用户名"></asp:BoundColumn>
								<asp:BoundColumn DataField="userpassword" HeaderText="密码"></asp:BoundColumn>
								<asp:BoundColumn DataField="role" HeaderText="角色"></asp:BoundColumn>
								<asp:BoundColumn DataField="remark" HeaderText="岗位"></asp:BoundColumn>
								<asp:BoundColumn DataField="fullname" HeaderText="真实姓名"></asp:BoundColumn>
								<asp:BoundColumn DataField="phone" HeaderText="联系电话"></asp:BoundColumn>
								<asp:BoundColumn DataField="createTime" HeaderText="创建日期"></asp:BoundColumn>
								<asp:BoundColumn DataField="lastUpdate" HeaderText="最后登录日期"></asp:BoundColumn>
								<asp:BoundColumn DataField="delflag" HeaderText="状态"></asp:BoundColumn>
								<asp:ButtonColumn Text="编辑" HeaderText="操作" CommandName="Select"></asp:ButtonColumn>
							</Columns>
						</asp:DataGrid></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
