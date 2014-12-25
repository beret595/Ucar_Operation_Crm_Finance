<%@ Register TagPrefix="cc1" Namespace="Powerise.Hygeia.Web.UI.WebControls" Assembly="Powerise.Hygeia.Web.UI.WebControls" %>
<%@ Page language="c#" Codebehind="InterViewDetail.aspx.cs" AutoEventWireup="false" Inherits="casey.hxyd_crm.Web.UI.InterViewDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�ͻ���ϵ���</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<base target="_self">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="JavaScript" src="./js/datepicker/WdatePicker.js"></SCRIPT>
		<LINK rel="stylesheet" type="text/css" href="./css/special.css">
		<LINK rel="stylesheet" type="text/css" href="./css/css.css">
		<script type="text/javascript" src="./js/business.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE style="WIDTH: 100%; BORDER-COLLAPSE: collapse" border="1">
				<tr>
					<td class="sub_title" colSpan="6">��ʷ�������</td>
				</tr>
				<tr>
					<td colSpan="6"><asp:datagrid id="dgdSearchResult" runat="server" AutoGenerateColumns="False" Width="100%">
							<AlternatingItemStyle BackColor="#CCCCFF"></AlternatingItemStyle>
							<HeaderStyle BackColor="#CCCCFF"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="id" HeaderText="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="���">
									<ItemTemplate>
										<%# this.dgdSearchResult.CurrentPageIndex * this.dgdSearchResult.PageSize + Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="interviewTime" HeaderText="��ϵʱ��"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="��ע">
									<ItemTemplate>
										<FONT face="����"></FONT><a onclick="javascript:void(0)"  title='<%#DataBinder.Eval(Container.DataItem,"comment")%>'
										</a>
										<%#Substr(((System.Data.DataRowView)Container.DataItem).Row["comment"].ToString())%>
									</ItemTemplate>
									<EditItemTemplate>
										<FONT face="����"></FONT>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="VIN" Visible="False" HeaderText="VIN"></asp:BoundColumn>
								<asp:BoundColumn DataField="model" Visible="False" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="insuranceCompany" Visible="False" HeaderText="���չ�˾"></asp:BoundColumn>
								<asp:BoundColumn DataField="insuranceFees" Visible="False" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="returnPoint" Visible="False" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="single_date" Visible="False" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="agentName" HeaderText="������"></asp:BoundColumn>
								<asp:ButtonColumn Text="��ϸ" HeaderText="�鿴��ϸ" CommandName="Detail"></asp:ButtonColumn>
								<asp:BoundColumn Visible="False" DataField="interViewListId" HeaderText="��ϵ��¼ID"></asp:BoundColumn>
								<asp:ButtonColumn Visible="False" Text="ѡ��" CommandName="Select"></asp:ButtonColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td class="sub_title" colSpan="6">�ͻ���Ϣ
					</td>
				</tr>
				<TR>
					<TD class="bg_column">��˾</TD>
					<TD class="bg_data" colSpan="5"><asp:textbox id="txtCompany" runat="server" Width="100%"></asp:textbox></TD>
				</TR>
				<tr>
					<td class="bg_column">�ͻ�����</td>
					<td class="bg_data"><asp:textbox id="TxtPersonName" runat="server"></asp:textbox></td>
					<td class="bg_column">Ʒ��</td>
					<td class="bg_data"><asp:dropdownlist id="ddlBrand" runat="server" Width="136px"></asp:dropdownlist></td>
					<td class="bg_column">��ϵ״̬</td>
					<td class="bg_data"><asp:dropdownlist id="ddlContactState" runat="server" Width="128px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td class="bg_column">�Ա�</td>
					<td class="bg_data"><asp:dropdownlist id="ddlGender" runat="server" Width="128px" Height="23px"></asp:dropdownlist><asp:textbox style="Z-INDEX: 0" id="TxtGender" runat="server" Visible="False"></asp:textbox></td>
					<td class="bg_column">����</td>
					<td class="bg_data"><asp:textbox id="TxtModel" runat="server"></asp:textbox></td>
					<td class="bg_column">ʧ��ԭ��</td>
					<td class="bg_data"><asp:dropdownlist id="ddlFailedReason" runat="server" Width="128px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td class="bg_column">��ϵ�绰</td>
					<td class="bg_data"><asp:textbox id="TxtPhone" runat="server"></asp:textbox><asp:regularexpressionvalidator id="CheckTelphone" ErrorMessage="�绰��ʽ������-�绰-�ֻ���" Display="Dynamic" ControlToValidate="TxtPhone"
							ValidationExpression="((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)" Runat="server"></asp:regularexpressionvalidator></td>
					<td class="bg_column">VIN</td>
					<td class="bg_data"><asp:textbox id="TxtVIN" runat="server"></asp:textbox></td>
					<td class="bg_column">������</td>
					<td class="bg_data"><asp:textbox id="TxtIntroducer" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="bg_column">��������</td>
					<td class="bg_data"><asp:textbox id="TxtArea" runat="server"></asp:textbox></td>
					<td class="bg_column">����</td>
					<td class="bg_data"><asp:textbox id="TxtLicensePlate" runat="server"></asp:textbox></td>
					<td class="bg_column">��������</td>
					<td class="bg_data"><INPUT id="TxtSalesDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="TxtSalesDate" runat="server"></td>
				</tr>
				<tr>
					<td class="bg_column">�ͻ���Դ</td>
					<td class="bg_data"><asp:dropdownlist style="Z-INDEX: 0" id="ddlCustomerType" runat="server" Width="128px"></asp:dropdownlist></td>
					<td class="bg_column">�ͻ����֤��</td>
					<td class="bg_data"><asp:textbox id="TxtIdCard" runat="server"></asp:textbox></td>
					<td class="bg_column">��������</td>
					<td class="bg_data"><asp:textbox id="TxtEngineNo" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="bg_column">��ϵ��ַ</td>
					<td class="bg_data"><asp:textbox id="TxtAddress" runat="server" Width="100%"></asp:textbox></td>
					<td class="bg_column">����</td>
					<td class="bg_data"><INPUT id="TxtBirthday" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="TxtBirthday" runat="server"></td>
					<td class="bg_column">�������</td>
					<td class="bg_data"><asp:dropdownlist id="ddlServiceType" runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td class="bg_column">����״̬</td>
					<td class="bg_data"><asp:dropdownlist id="ddlCarType" runat="server"></asp:dropdownlist></td>
					<td class="bg_column">���յ���</td>
					<td class="bg_data"><INPUT id="TxtExpireDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="TxtExpireDate" runat="server"></td>
				<tr>
					<td class="sub_title" colSpan="6">������Ϣ
					</td>
				</tr>
				<tr>
					<td colSpan="6">
						<table width="100%">
							<tr>
								<td class="bg_column">����</td>
								<td class="bg_data"><asp:textbox id="TxtCheSun" runat="server"></asp:textbox></td>
								<td class="bg_column">����</td>
								<td class="bg_data"><asp:textbox id="TxtHuaHen" runat="server"></asp:textbox></td>
								<td class="bg_column">��ҵ���ܼ�</td>
								<td class="bg_data"><asp:textbox id="TxtInsuranceFees" runat="server"></asp:textbox></td>
								<td class="bg_column">���չ�˾</td>
								<td class="bg_data"><asp:dropdownlist id="ddlInsuranceCompany" runat="server"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td class="bg_column">����</td>
								<td class="bg_data"><asp:textbox id="TxtSanZhe" runat="server"></asp:textbox></td>
								<td class="bg_column">������</td>
								<td class="bg_data"><asp:textbox id="TxtDaoCheJing" runat="server"></asp:textbox></td>
								<td class="bg_column">��ǿ��</td>
								<td class="bg_data"><asp:textbox id="TxtForceInsur" runat="server"></asp:textbox></td>
								<td class="bg_column">��ҵ�ۿ�</td>
								<td class="bg_data"><asp:textbox id="TxtReturnPoint" runat="server"></asp:textbox>%</td>
							</tr>
							<tr>
								<td class="bg_column">��Ա</td>
								<td class="bg_data"><asp:textbox id="TxtRenYuan" runat="server"></asp:textbox></td>
								<td class="bg_column">����</td>
								<td class="bg_data"><asp:textbox id="TxtBoLi" runat="server"></asp:textbox></td>
								<td class="bg_column">����</td>
								<td class="bg_data"><asp:textbox id="TxtTravelTax" runat="server"></asp:textbox></td>
								<td class="bg_column">��ǿ���ۿ�</td>
								<td class="bg_data"><asp:textbox id="TxtTrafficPoint" runat="server"></asp:textbox>%</td>
							</tr>
							<tr>
								<td class="bg_column">����</td>
								<td class="bg_data"><asp:textbox id="TxtDaoQiang" runat="server"></asp:textbox></td>
								<td class="bg_column">��ˮ</td>
								<td class="bg_data"><asp:textbox id="TxtSheShui" runat="server"></asp:textbox></td>
								<td class="bg_column"></td>
								<td class="bg_data"></td>
								<td class="bg_column">�����ۿ�</td>
								<td class="bg_data"><asp:textbox id="TxtInsurancePoint" runat="server"></asp:textbox>%</td>
							</tr>
							<tr>
								<td class="bg_column">��������</td>
								<td class="bg_data"><asp:textbox id="TxtBuJiMianPei" runat="server"></asp:textbox></td>
								<td class="bg_column">��ȼ</td>
								<td class="bg_data"><asp:textbox id="TxtZiRan" runat="server"></asp:textbox></td>
								<td class="bg_column"></td>
								<td class="bg_data"></td>
								<td class="bg_column">��������</td>
								<td class="bg_data"><INPUT id="txtSingleDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
										name="txtSingleDate" runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="sub_title" colSpan="6">��ϵ���<INPUT style="Z-INDEX: 0" id="hdPersonID" type="hidden" name="hdPersonID" runat="server">
						<INPUT style="Z-INDEX: 0" id="hdInterViewID" type="hidden" name="hdInterViewID" runat="server"><INPUT style="Z-INDEX: 0" id="hdCarID" type="hidden" name="hdCarID" runat="server"></td>
				</tr>
				<tr>
					<td class="bg_column">ԤԼ��������</td>
					<td class="bg_data"><INPUT id="txtViewDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="txtViewDate" runat="server"></td>
					<td class="bg_column">ԤԼ����ʱ��</td>
					<td class="bg_data"><asp:dropdownlist id="ddlViewTime" runat="server"></asp:dropdownlist></td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td class="sub_title" colSpan="6">������ϵ���</td>
				</tr>
				<tr>
					<td colSpan="6"><TEXTAREA style="WIDTH: 100%; HEIGHT: 160px" id="TxtComment" rows="6" cols="117" name="TEXTAREA1"
							runat="server">						</TEXTAREA></td>
				</tr>
				<tr>
					<td colSpan="6" align="center"><asp:button id="btnSave" runat="server" CssClass="button" Text="������ϵ���"></asp:button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
