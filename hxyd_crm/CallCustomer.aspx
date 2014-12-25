<%@ Page language="c#" Codebehind="CallCustomer.aspx.cs" AutoEventWireup="false" Inherits="casey.hxyd_crm.Web.UI.CallCustomer" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�ͻ�����</title>
		<base target="_self">
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
				<tr>
					<td colSpan="6" align="center"><asp:button style="Z-INDEX: 0" id="Button1" runat="server" Text="������һ��" CssClass="button"></asp:button><asp:button style="Z-INDEX: 0" id="Button2" runat="server" Text="��  ��" CssClass="button"></asp:button></td>
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
					<td class="bg_column">�Ա�</td>
					<td class="bg_data"><asp:dropdownlist id="ddlGender" runat="server"></asp:dropdownlist><asp:textbox style="Z-INDEX: 0" id="TxtGender" runat="server" Visible="False"></asp:textbox></td>
					<td class="bg_column">��ϵ�绰</td>
					<td class="bg_data"><asp:textbox id="TxtPhone" runat="server"></asp:textbox><asp:regularexpressionvalidator id="CheckTelphone" ErrorMessage="�绰��ʽ������-�绰-�ֻ���" Display="Dynamic" ControlToValidate="TxtPhone"
							ValidationExpression="((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)" Runat="server"></asp:regularexpressionvalidator></td>
				</tr>
				<tr>
					<td class="bg_column">�ͻ���Դ</td>
					<td class="bg_data"><asp:dropdownlist style="Z-INDEX: 0" id="ddlCustomerType" runat="server"></asp:dropdownlist></td>
					<td class="bg_column">��ϵ״̬</td>
					<td class="bg_data"><asp:dropdownlist id="ddlContactState" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
					<td class="bg_column">��������</td>
					<td class="bg_data"><asp:textbox id="TxtArea" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="bg_column">����</td>
					<td class="bg_data"><INPUT id="TxtBirthday" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="TxtBirthday" runat="server"></td>
					<td class="bg_column">�ͻ����֤��</td>
					<td class="bg_data"><asp:textbox id="TxtIdCard" runat="server"></asp:textbox></td>
					<td class="bg_column">������</td>
					<td class="bg_data"><asp:textbox id="TxtIntroducer" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="bg_column">ʧ��ԭ��</td>
					<td class="bg_data"><asp:dropdownlist id="ddlFailedReason" runat="server"></asp:dropdownlist></td>
					<td class="bg_column">��ϵ��ַ</td>
					<td class="bg_data" colSpan="3"><asp:textbox id="TxtAddress" runat="server" Width="100%"></asp:textbox></td>
				</tr>
				<tr>
					<td class="bg_column">�ͻ�����</td>
					<td class="bg_data"><asp:dropdownlist id="ddlCustomerLevel" runat="server">
							<asp:ListItem Value="" Selected="True">��ѡ��ͻ�����</asp:ListItem>
							<asp:ListItem Value="A">A</asp:ListItem>
							<asp:ListItem Value="B">B</asp:ListItem>
							<asp:ListItem Value="C">C</asp:ListItem>
							<asp:ListItem Value="D">D</asp:ListItem>
							<asp:ListItem Value="E">E</asp:ListItem>
						</asp:dropdownlist></td>
					<td class="bg_column">�������</td>
					<td class="bg_data"><asp:dropdownlist id="ddlServiceType" runat="server"></asp:dropdownlist></td>
					<td class="bg_column"></td>
					<td class="bg_data"></td>
				</tr>
				<tr>
					<td class="sub_title" colSpan="6">������Ϣ
					</td>
				</tr>
				<tr>
					<td class="bg_column">Ʒ��</td>
					<td class="bg_data"><asp:dropdownlist id="ddlBrand" runat="server"></asp:dropdownlist></td>
					<td class="bg_column">����</td>
					<td class="bg_data"><asp:textbox id="TxtLicensePlate" runat="server"></asp:textbox></td>
					<td class="bg_column">��������</td>
					<td class="bg_data"><INPUT id="TxtSalesDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="TxtSalesDate" runat="server"></td>
				</tr>
				<tr>
					<td class="bg_column">��ǰ���</td>
					<td class="bg_data"><asp:textbox id="TxtCurrentMileage" runat="server" ReadOnly="True"></asp:textbox></td>
					<td class="bg_column">����</td>
					<td class="bg_data"><asp:textbox id="TxtModel" runat="server"></asp:textbox></td>
					<td class="bg_column">��ɫ</td>
					<td class="bg_data"><asp:dropdownlist id="ddlShapeColors" runat="server">
							<asp:ListItem Value="" Selected="True">��ѡ����ɫ</asp:ListItem>
							<asp:ListItem Value="��">��</asp:ListItem>
							<asp:ListItem Value="��">��</asp:ListItem>
							<asp:ListItem Value="��">��</asp:ListItem>
							<asp:ListItem Value="��">��</asp:ListItem>
							<asp:ListItem Value="��">��</asp:ListItem>
							<asp:ListItem Value="��">��</asp:ListItem>
							<asp:ListItem Value="��">��</asp:ListItem>
							<asp:ListItem Value="��">��</asp:ListItem>
							<asp:ListItem Value="��">��</asp:ListItem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td class="bg_column">���յ���</td>
					<td class="bg_data"><INPUT id="TxtExpireDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="TxtExpireDate" runat="server"></td>
					<td class="bg_column">�վ����</td>
					<td class="bg_data"><asp:textbox id="TxtAverageMileage" runat="server" ReadOnly="True"></asp:textbox></td>
					<td class="bg_column">VIN</td>
					<td class="bg_data"><asp:textbox id="TxtVIN" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="bg_column">��������</td>
					<td class="bg_data"><asp:textbox id="TxtEngineNo" runat="server"></asp:textbox></td>
					<td class="bg_column"></td>
					<td class="bg_data"></td>
					<td class="bg_column"></td>
					<td class="bg_data"></td>
				</tr>
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
					<td class="sub_title" colSpan="6">������ϵ���</td>
				</tr>
				<tr>
					<td class="bg_column">��ϵ��</td>
					<td class="bg_data"><asp:textbox id="TxtAgent" runat="server" ReadOnly="True" Enabled="False"></asp:textbox></td>
					<td class="bg_column">��ϵʱ��</td>
					<td class="bg_data"><asp:textbox id="TxtInterviewTime" runat="server" ReadOnly="True" Enabled="False"></asp:textbox></td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td class="bg_column">ԤԼ��������</td>
					<td class="bg_data"><INPUT id="txtViewDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="txtViewDate" runat="server"></td>
					<td class="bg_column">ԤԼ����ʱ��</td>
					<td class="bg_data"><asp:dropdownlist id="ddlViewTime" runat="server"></asp:dropdownlist></td>
					<td></td>
					<td><INPUT style="Z-INDEX: 0" id="hdPersonID" type="hidden" name="hdPersonID" runat="server"><INPUT style="Z-INDEX: 0" id="hdCarID" type="hidden" name="hdCarID" runat="server"></td>
				</tr>
				<tr>
					<td class="sub_title" colSpan="6">������ϵ���</td>
				</tr>
				<tr>
					<td colSpan="6"><TEXTAREA style="WIDTH: 100%; HEIGHT: 60px" id="TxtComment" cols="24" name="TEXTAREA1" runat="server">											</TEXTAREA></td>
				</tr>
			</TABLE>
			<table style="WIDTH: 100%; DISPLAY: none; BORDER-COLLAPSE: collapse" border="1">
				<tr>
					<td class="sub_title" colSpan="6">��ʷ��ϵ���</td>
				</tr>
				<tr>
					<td colSpan="6"><TEXTAREA style="WIDTH: 100%; HEIGHT: 60px" id="TxtCommentHis" cols="24" readOnly name="TEXTAREA2"
							runat="server">	</TEXTAREA>
					</td>
				</tr>
			</table>
			<table style="WIDTH: 100%; BORDER-COLLAPSE: collapse" border="1">
				<tr>
					<td class="sub_title" colSpan="6">��ʷ��ϵ���</td>
				</tr>
				<tr>
					<td colSpan="6"><asp:datagrid id="dgdSearchResult" runat="server" Width="100%" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="#CCCCFF"></AlternatingItemStyle>
							<HeaderStyle BackColor="#CCCCFF"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="���">
									<ItemTemplate>
										<%# this.dgdSearchResult.CurrentPageIndex * this.dgdSearchResult.PageSize + Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="interviewTime" HeaderText="��ϵʱ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="comment" HeaderText="��ע"></asp:BoundColumn>
								<asp:BoundColumn DataField="agentName" HeaderText="������"></asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
