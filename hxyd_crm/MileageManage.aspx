<%@ Page language="c#" Codebehind="MileageManage.aspx.cs" AutoEventWireup="false" Inherits="casey.hxyd_crm.Web.UI.MileageManage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>InsurCompanyManager</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="./css/special.css">
		<LINK rel="stylesheet" type="text/css" href="./css/css.css">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE border="1" style="WIDTH:100%;BORDER-COLLAPSE:collapse">
				<tr>
					<td colspan="4" class="sub_title">品牌里程维护</td>
				</tr>
				<TR>
					<TD class="bg_column">ID</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtID" runat="server"></asp:TextBox></TD>
					<TD class="bg_column">英文品牌</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtBrandNameEN" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="bg_column">中文品牌</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtBrandNameCN" runat="server"></asp:TextBox></TD>
					<TD class="bg_column">维护里程</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtMileage" runat="server"></asp:TextBox></TD>
				</TR>
				<tr>
					<td colspan="4" align="center">&nbsp;
						<asp:Button id="btnQuery" runat="server" Text="查  询" CssClass="button" style="Z-INDEX: 0"></asp:Button>
						<asp:Button id="btnAdd" runat="server" Text="清  空" CssClass="button"></asp:Button>
						<asp:Button id="btnSave" runat="server" Text="保  存" CssClass="button"></asp:Button></td>
				<tr>
					<td colspan="4" class="sub_title">品牌信息</td>
				</tr>
				<tr>
					<td colspan="4">
						<asp:DataGrid id="dgdCompany" Width="100%" runat="server" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="#CCCCFF"></AlternatingItemStyle>
							<HeaderStyle BackColor="#CCCCFF"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="id" HeaderText="品牌ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="brandNameEN" HeaderText="品牌英文"></asp:BoundColumn>
								<asp:BoundColumn DataField="brandNameCN" HeaderText="品牌中文"></asp:BoundColumn>
								<asp:BoundColumn DataField="mileage" HeaderText="维护里程"></asp:BoundColumn>
								<asp:ButtonColumn Text="编辑" HeaderText="操作" CommandName="Select"></asp:ButtonColumn>
							</Columns>
						</asp:DataGrid></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
