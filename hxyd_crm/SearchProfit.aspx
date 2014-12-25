<%@ Page language="c#" Codebehind="SearchProfit.aspx.cs" AutoEventWireup="false" Inherits="casey.hxyd_crm.Web.UI.SearchProfit" %>
<%@ Register TagPrefix="cc1" Namespace="Powerise.Hygeia.Web.UI.WebControls" Assembly="Powerise.Hygeia.Web.UI.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>利润查询</title>
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
					<TD class="bg_column">出单日期</TD>
					<TD class="bg_data"><INPUT id="txtSingleDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" readOnly
							size="26" name="txtSingleDate" runat="server">至 <INPUT id="TxtEndTime" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" readOnly
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
					<TD class="bg_data">
						<asp:TextBox id="TxtVIN" runat="server"></asp:TextBox></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<tr>
					<td colSpan="4" align="center">
						<asp:Button id="btnQuery" runat="server" Text="搜  索" CssClass="button"></asp:Button>
						<asp:Button id="btnExport" runat="server" CssClass="button" Text="数据导出" Visible="False"></asp:Button></td>
				</tr>
				<tr>
					<td class="sub_title" colSpan="4">查询结果</td>
				</tr>
				<tr>
					<td colSpan="4"><asp:datagrid id="dgdSearchResult" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True">
							<AlternatingItemStyle BackColor="#CCCCFF"></AlternatingItemStyle>
							<HeaderStyle BackColor="#CCCCFF"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="carid" HeaderText="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="序号">
									<ItemTemplate>
										<%# this.dgdSearchResult.CurrentPageIndex * this.dgdSearchResult.PageSize + Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="personName" HeaderText="客户"></asp:BoundColumn>
								<asp:BoundColumn DataField="brand" HeaderText="品牌"></asp:BoundColumn>
								<asp:BoundColumn DataField="VIN" HeaderText="VIN"></asp:BoundColumn>
								<asp:BoundColumn DataField="model" HeaderText="车型"></asp:BoundColumn>
								<asp:BoundColumn DataField="insuranceCompany" HeaderText="保险公司" FooterText="本页合计"></asp:BoundColumn>
								<asp:BoundColumn DataField="insuranceFees" HeaderText="保费"></asp:BoundColumn>
								<asp:BoundColumn DataField="returnPoint" HeaderText="让利"></asp:BoundColumn>
								<asp:BoundColumn DataField="profit" HeaderText="利润"></asp:BoundColumn>
								<asp:BoundColumn DataField="single_date" HeaderText="出单日期"></asp:BoundColumn>
								<asp:BoundColumn DataField="username" HeaderText="操作者"></asp:BoundColumn>
								<asp:ButtonColumn Text="呼叫" HeaderText="操作" CommandName="Select"></asp:ButtonColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				
			</TABLE>
		</form>
	</body>
</HTML>
