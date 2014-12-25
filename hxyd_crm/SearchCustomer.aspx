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
							<asp:ListItem Value="1" Selected="True">预约日期</asp:ListItem>
							<asp:ListItem Value="2">致电日期</asp:ListItem>
							<asp:ListItem Value="4">购车日期</asp:ListItem>
							<asp:ListItem Value="5">保险到期</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD class="bg_data"><INPUT id="txtInterViewTime" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"
							readOnly size="26" name="txtInterViewTime" runat="server"> 至 <INPUT id="TxtEndTime" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" readOnly
							size="26" name="TxtEndTime" runat="server"></TD>
					<TD class="bg_column">操作者</TD>
					<TD class="bg_data"><asp:dropdownlist id="ddlAgent" runat="server"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="bg_column">联系电话</TD>
					<TD class="bg_data"><asp:textbox id="TxtPhone" runat="server"></asp:textbox></TD>
					<TD class="bg_column">联系状态</TD>
					<TD class="bg_data"><asp:dropdownlist id="ddlContactState" runat="server"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="bg_column">车牌</TD>
					<TD class="bg_data"><asp:textbox id="TxtLicense" runat="server"></asp:textbox></TD>
					<TD class="bg_column">品牌</TD>
					<TD class="bg_data"><asp:dropdownlist id="ddlBrand" runat="server"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="bg_column">VIN</TD>
					<TD class="bg_data"><asp:textbox id="TxtVIN" runat="server"></asp:textbox></TD>
					<TD class="bg_column">客户来源</TD>
					<TD class="bg_data"><asp:dropdownlist style="Z-INDEX: 0" id="ddlCustomerType" runat="server"></asp:dropdownlist></TD>
				</TR>
				<tr>
					<td colSpan="4" align="center"><asp:button id="btnQuery" runat="server" Text="搜  索" CssClass="button"></asp:button><asp:button id="btnExport" runat="server" Text="数据导出" CssClass="button" Visible="False"></asp:button></td>
				</tr>
				<tr>
					<td class="sub_title" colSpan="4">查询结果</td>
				</tr>
				<tr>
					<td colSpan="4"><asp:datagrid id="dgdSearchResult" runat="server" ShowFooter="True" AutoGenerateColumns="False"
							Width="100%">
							<AlternatingItemStyle BackColor="#CCCCFF"></AlternatingItemStyle>
							<HeaderStyle BackColor="#CCCCFF"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="carId" HeaderText="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="序号">
									<ItemTemplate>
										<%# this.dgdSearchResult.CurrentPageIndex * this.dgdSearchResult.PageSize + Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="personName" HeaderText="客户"></asp:BoundColumn>
								<asp:BoundColumn DataField="brand" HeaderText="品牌"></asp:BoundColumn>
								<asp:BoundColumn DataField="VIN" Visible="False" HeaderText="VIN"></asp:BoundColumn>
								<asp:BoundColumn DataField="model" HeaderText="车型"></asp:BoundColumn>
								<asp:BoundColumn DataField="licensePlate" HeaderText="车牌"></asp:BoundColumn>
								<asp:BoundColumn DataField="service_type" HeaderText="服务类别"></asp:BoundColumn>
								<asp:BoundColumn DataField="view_date" HeaderText="预约日期"></asp:BoundColumn>
								<asp:BoundColumn DataField="view_time" HeaderText="预约时间"></asp:BoundColumn>
								<asp:BoundColumn DataField="contactState" HeaderText="联系状态"></asp:BoundColumn>
								<asp:BoundColumn DataField="insuranceCompany" Visible="False" HeaderText="保险公司" FooterText="本页合计"></asp:BoundColumn>
								<asp:BoundColumn DataField="insuranceFees" Visible="False" HeaderText="商业险"></asp:BoundColumn>
								<asp:BoundColumn DataField="returnPoint" Visible="False" HeaderText="商折"></asp:BoundColumn>
								<asp:BoundColumn DataField="forceInsur" Visible="False" HeaderText="交强险"></asp:BoundColumn>
								<asp:BoundColumn DataField="trafficPoint" Visible="False" HeaderText="交折"></asp:BoundColumn>
								<asp:BoundColumn DataField="profit" Visible="False" HeaderText="利润"></asp:BoundColumn>
								<asp:BoundColumn DataField="single_date" Visible="False" HeaderText="出单日期"></asp:BoundColumn>
								<asp:BoundColumn DataField="agentName" HeaderText="操作者"></asp:BoundColumn>
								<asp:BoundColumn DataField="comment" Visible="False" HeaderText="备注"></asp:BoundColumn>
								<asp:BoundColumn DataField="insuranceCompany" Visible="False" HeaderText="保险公司"></asp:BoundColumn>
								<asp:ButtonColumn Text="呼叫" HeaderText="操作" CommandName="Select"></asp:ButtonColumn>
								<asp:ButtonColumn Text="查看" HeaderText="明细" CommandName="Detail"></asp:ButtonColumn>
								<asp:BoundColumn Visible="False" DataField="interViewListId" HeaderText="联系记录ID"></asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
