<%@ Page language="c#" Codebehind="InsurCompanyManager.aspx.cs" AutoEventWireup="false" Inherits="casey.hxyd_crm.Web.UI.InsurCompanyManager" %>
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
					<td colspan="4" class="sub_title">���չ�˾ά��</td>
				</tr>
				<TR>
					<TD class="bg_column">ID</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtID" runat="server"></asp:TextBox></TD>
					<TD class="bg_column">��˾����</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtCompanyName" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="bg_column">�Żݶ��</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtRebate" runat="server"></asp:TextBox></TD>
					<TD class="bg_column">��ע</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtRemark" runat="server"></asp:TextBox></TD>
				</TR>
				<tr>
					<td colspan="4" align="center">&nbsp;
						<asp:Button id="btnQuery" runat="server" Text="��  ѯ" CssClass="button" style="Z-INDEX: 0"></asp:Button>
						<asp:Button id="btnAdd" runat="server" Text="��  ��" CssClass="button"></asp:Button>
						<asp:Button id="btnSave" runat="server" Text="��  ��" CssClass="button"></asp:Button></td>
				<tr>
					<td colspan="4" class="sub_title">���չ�˾��Ϣ</td>
				</tr>
				<tr>
					<td colspan="4">
						<asp:DataGrid id="dgdCompany" Width="100%" runat="server" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="#CCCCFF"></AlternatingItemStyle>
							<HeaderStyle BackColor="#CCCCFF"></HeaderStyle>
							<Columns>
								<asp:BoundColumn  DataField="insurCompanyID" HeaderText="���չ�˾ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="insurCompanyName" HeaderText="��˾����"></asp:BoundColumn>
								<asp:BoundColumn DataField="rebate" HeaderText="�Żݶ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="remark" HeaderText="��ע"></asp:BoundColumn>
								<asp:ButtonColumn Text="�༭" HeaderText="����" CommandName="Select"></asp:ButtonColumn>
							</Columns>
						</asp:DataGrid></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
