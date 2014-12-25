<%@ Page language="c#" Codebehind="ModifyCustomer.aspx.cs" AutoEventWireup="false" Inherits="casey.hxyd_crm.Web.UI.ModifyCustomer" %>
<%@ Register TagPrefix="cc1" Namespace="Powerise.Hygeia.Web.UI.WebControls" Assembly="Powerise.Hygeia.Web.UI.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ModifyCustomer</title>
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
					<TD class="sub_title" colSpan="4">搜索条件</TD>
				</tr>
				<tr>
					<td class="bg_column">客户姓名</td>
					<td class="bg_data"><asp:textbox id="TxtName" runat="server"></asp:textbox></td>
					<td class="bg_column">车牌</td>
					<td class="bg_data"><asp:textbox id="TxtLicense" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="bg_column">电话</td>
					<td class="bg_data"><asp:textbox id="TxtTelephone" runat="server"></asp:textbox></td>
					<td class="bg_column">VIN</td>
					<td class="bg_data"><asp:textbox id="TxtVinInfo" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="bg_data" colSpan="4" align="center"><asp:button style="Z-INDEX: 0" id="Button1" runat="server" CssClass="button" Text="搜  索"></asp:button><asp:button id="btnClear" runat="server" CssClass="button" Text="新  建"></asp:button><asp:button style="Z-INDEX: 0" id="btnSave" runat="server" CssClass="button" Text="保  存"></asp:button></td>
				</tr>
			</TABLE>
			<TABLE style="WIDTH: 100%; BORDER-COLLAPSE: collapse" border="1">
				<tr>
					<TD class="sub_title" colSpan="6">客户信息<INPUT id="hdID" type="hidden" runat="server"></TD>
				</tr>
				<TR>
					<TD class="bg_column">公司</TD>
					<TD class="bg_data"><asp:textbox id="txtCompany" runat="server" Width="100%"></asp:textbox></TD>
					<td class="bg_column">客户身份证号</td>
					<td class="bg_data"><asp:textbox id="TxtIdCard" runat="server"></asp:textbox></td>
					<td class="bg_column">生日</td>
					<td class="bg_data"><INPUT id="TxtBirthday" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="TxtBirthday" runat="server"></td>
				</TR>
				<tr>
					<td class="bg_column">客户姓名</td>
					<td class="bg_data"><asp:textbox id="TxtPersonName" runat="server"></asp:textbox></td>
					<td class="bg_column">性别</td>
					<td class="bg_data"><asp:dropdownlist id="ddlGender" runat="server"></asp:dropdownlist><asp:textbox style="Z-INDEX: 0" id="TxtGender" runat="server" Visible="False"></asp:textbox></td>
					<td class="bg_column">联系电话</td>
					<td class="bg_data"><asp:textbox id="TxtPhone" runat="server"></asp:textbox><asp:regularexpressionvalidator id="CheckTelphone" ErrorMessage="电话格式（区号-电话-分机）" Display="Dynamic" ControlToValidate="TxtPhone"
							ValidationExpression="((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)" Runat="server"></asp:regularexpressionvalidator></td>
				</tr>
				<tr>
					<td class="bg_column">联系状态</td>
					<td class="bg_data"><asp:dropdownlist id="ddlContactState" runat="server"></asp:dropdownlist></td>
					<td class="bg_column">失败原因</td>
					<td class="bg_data"><asp:dropdownlist id="ddlFailedReason" runat="server"></asp:dropdownlist></td>
					<td class="bg_column">介绍人</td>
					<td class="bg_data"><asp:textbox id="TxtIntroducer" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="bg_column">客户来源</td>
					<td class="bg_data"><asp:dropdownlist style="Z-INDEX: 0" id="ddlCustomerType" runat="server"></asp:dropdownlist></td>
					<td class="bg_column">所属区域</td>
					<td class="bg_data"><asp:textbox id="TxtArea" runat="server"></asp:textbox></td>
					<td class="bg_column">联系地址</td>
					<td class="bg_data" colSpan="3"><asp:textbox id="TxtAddress" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="bg_column">客户级别</td>
					<td class="bg_data"><asp:dropdownlist id="ddlCustomerLevel" runat="server">
							<asp:ListItem Value="" Selected="True">请选择客户级别</asp:ListItem>
							<asp:ListItem Value="A">A</asp:ListItem>
							<asp:ListItem Value="B">B</asp:ListItem>
							<asp:ListItem Value="C">C</asp:ListItem>
							<asp:ListItem Value="D">D</asp:ListItem>
							<asp:ListItem Value="E">E</asp:ListItem>
						</asp:dropdownlist></td>
					<td class="bg_column"></td>
					<td class="bg_data"></td>
					<td class="bg_column"></td>
					<td class="bg_data"></td>
				</tr>
				<tr>
					<td class="sub_title" colSpan="6">搜索结果</td>
				</tr>
				<tr>
					<td colSpan="6"><asp:datagrid id="dgdSearchResult" runat="server" Width="100%" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="#CCCCFF"></AlternatingItemStyle>
							<HeaderStyle BackColor="#CCCCFF"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="id" HeaderText="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="序号">
									<ItemTemplate>
										<%# this.dgdSearchResult.CurrentPageIndex * this.dgdSearchResult.PageSize + Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="personName" HeaderText="客户"></asp:BoundColumn>
								<asp:BoundColumn DataField="gender" HeaderText="性别"></asp:BoundColumn>
								<asp:BoundColumn DataField="phone" HeaderText="联系电话"></asp:BoundColumn>
								<asp:BoundColumn DataField="area" HeaderText="所在区"></asp:BoundColumn>
								<asp:BoundColumn DataField="address" HeaderText="地址"></asp:BoundColumn>
								<asp:ButtonColumn Text="编辑客户" HeaderText="操作" CommandName="Select"></asp:ButtonColumn>
								<asp:ButtonColumn Text="编辑车辆" HeaderText="操作" CommandName="Detail"></asp:ButtonColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td colSpan="6"><cc1:pager id="Pager1" runat="server"></cc1:pager></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
