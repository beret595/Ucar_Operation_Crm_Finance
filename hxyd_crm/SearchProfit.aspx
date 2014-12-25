<%@ Page language="c#" Codebehind="SearchProfit.aspx.cs" AutoEventWireup="false" Inherits="casey.hxyd_crm.Web.UI.SearchProfit" %>
<%@ Register TagPrefix="cc1" Namespace="Powerise.Hygeia.Web.UI.WebControls" Assembly="Powerise.Hygeia.Web.UI.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�����ѯ</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
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
					<TD class="bg_column">��������</TD>
					<TD class="bg_data"><INPUT id="txtSingleDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" readOnly
							size="26" name="txtSingleDate" runat="server">�� <INPUT id="TxtEndTime" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" readOnly
							size="26" name="TxtEndTime" runat="server"></TD>
					<TD class="bg_column">������</TD>
					<TD class="bg_data"><asp:dropdownlist id="ddlAgent" runat="server"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="bg_column">��ϵ�绰</TD>
					<TD class="bg_data"><asp:textbox id="TxtPhone" runat="server"></asp:textbox></TD>
					<TD class="bg_column">��ϵ״̬</TD>
					<TD class="bg_data"><asp:dropdownlist id="ddlContactState" runat="server"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="bg_column">����</TD>
					<TD class="bg_data"><asp:textbox id="TxtLicense" runat="server"></asp:textbox></TD>
					<TD class="bg_column">Ʒ��</TD>
					<TD class="bg_data"><asp:dropdownlist id="ddlBrand" runat="server"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="bg_column">VIN</TD>
					<TD class="bg_data">
						<asp:TextBox id="TxtVIN" runat="server"></asp:TextBox></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<tr>
					<td colSpan="4" align="center">
						<asp:Button id="btnQuery" runat="server" Text="��  ��" CssClass="button"></asp:Button>
						<asp:Button id="btnExport" runat="server" CssClass="button" Text="���ݵ���" Visible="False"></asp:Button></td>
				</tr>
				<tr>
					<td class="sub_title" colSpan="4">��ѯ���</td>
				</tr>
				<tr>
					<td colSpan="4"><asp:datagrid id="dgdSearchResult" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True">
							<AlternatingItemStyle BackColor="#CCCCFF"></AlternatingItemStyle>
							<HeaderStyle BackColor="#CCCCFF"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="carid" HeaderText="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="���">
									<ItemTemplate>
										<%# this.dgdSearchResult.CurrentPageIndex * this.dgdSearchResult.PageSize + Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="personName" HeaderText="�ͻ�"></asp:BoundColumn>
								<asp:BoundColumn DataField="brand" HeaderText="Ʒ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="VIN" HeaderText="VIN"></asp:BoundColumn>
								<asp:BoundColumn DataField="model" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="insuranceCompany" HeaderText="���չ�˾" FooterText="��ҳ�ϼ�"></asp:BoundColumn>
								<asp:BoundColumn DataField="insuranceFees" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="returnPoint" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="profit" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="single_date" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="username" HeaderText="������"></asp:BoundColumn>
								<asp:ButtonColumn Text="����" HeaderText="����" CommandName="Select"></asp:ButtonColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				
			</TABLE>
		</form>
	</body>
</HTML>
