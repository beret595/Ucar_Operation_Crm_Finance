<%@ Page language="c#" Codebehind="SearchCustomer.aspx.cs" AutoEventWireup="false" Inherits="casey.hxyd_crm.Web.UI.SearchCustomer" %>
<%@ Register TagPrefix="cc1" Namespace="Powerise.Hygeia.Web.UI.WebControls" Assembly="Powerise.Hygeia.Web.UI.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SearchCustomer</title>
		<base target="_self">
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
				<TR>
					<TD class="bg_column"><asp:dropdownlist id="ddl_date_type" runat="server" AutoPostBack="True">
							<asp:ListItem Value="1" Selected="True">ԤԼ����</asp:ListItem>
							<asp:ListItem Value="2">�µ�����</asp:ListItem>
							<asp:ListItem Value="4">��������</asp:ListItem>
							<asp:ListItem Value="5">���յ���</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD class="bg_data"><INPUT id="txtInterViewTime" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"
							readOnly size="26" name="txtInterViewTime" runat="server"> �� <INPUT id="TxtEndTime" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" readOnly
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
					<TD class="bg_data"><asp:textbox id="TxtVIN" runat="server"></asp:textbox></TD>
					<TD class="bg_column">�ͻ���Դ</TD>
					<TD class="bg_data"><asp:dropdownlist style="Z-INDEX: 0" id="ddlCustomerType" runat="server"></asp:dropdownlist></TD>
				</TR>
				<tr>
					<td colSpan="4" align="center"><asp:button id="btnQuery" runat="server" Text="��  ��" CssClass="button"></asp:button><asp:button id="btnExport" runat="server" Text="���ݵ���" CssClass="button" Visible="False"></asp:button></td>
				</tr>
				<tr>
					<td class="sub_title" colSpan="4">��ѯ���</td>
				</tr>
				<tr>
					<td colSpan="4"><asp:datagrid id="dgdSearchResult" runat="server" ShowFooter="True" AutoGenerateColumns="False"
							Width="100%">
							<AlternatingItemStyle BackColor="#CCCCFF"></AlternatingItemStyle>
							<HeaderStyle BackColor="#CCCCFF"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="carId" HeaderText="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="���">
									<ItemTemplate>
										<%# this.dgdSearchResult.CurrentPageIndex * this.dgdSearchResult.PageSize + Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="personName" HeaderText="�ͻ�"></asp:BoundColumn>
								<asp:BoundColumn DataField="brand" HeaderText="Ʒ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="VIN" Visible="False" HeaderText="VIN"></asp:BoundColumn>
								<asp:BoundColumn DataField="model" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="licensePlate" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="service_type" HeaderText="�������"></asp:BoundColumn>
								<asp:BoundColumn DataField="view_date" HeaderText="ԤԼ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="view_time" HeaderText="ԤԼʱ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="contactState" HeaderText="��ϵ״̬"></asp:BoundColumn>
								<asp:BoundColumn DataField="insuranceCompany" Visible="False" HeaderText="���չ�˾" FooterText="��ҳ�ϼ�"></asp:BoundColumn>
								<asp:BoundColumn DataField="insuranceFees" Visible="False" HeaderText="��ҵ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="returnPoint" Visible="False" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="forceInsur" Visible="False" HeaderText="��ǿ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="trafficPoint" Visible="False" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="profit" Visible="False" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="single_date" Visible="False" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="agentName" HeaderText="������"></asp:BoundColumn>
								<asp:BoundColumn DataField="comment" Visible="False" HeaderText="��ע"></asp:BoundColumn>
								<asp:BoundColumn DataField="insuranceCompany" Visible="False" HeaderText="���չ�˾"></asp:BoundColumn>
								<asp:ButtonColumn Text="����" HeaderText="����" CommandName="Select"></asp:ButtonColumn>
								<asp:ButtonColumn Text="�鿴" HeaderText="��ϸ" CommandName="Detail"></asp:ButtonColumn>
								<asp:BoundColumn Visible="False" DataField="interViewListId" HeaderText="��ϵ��¼ID"></asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
