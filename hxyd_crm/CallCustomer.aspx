<%@ Page language="c#" Codebehind="CallCustomer.aspx.cs" AutoEventWireup="false" Inherits="casey.hxyd_crm.Web.UI.CallCustomer" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>客户呼叫</title>
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
					<td colSpan="6" align="center"><asp:button style="Z-INDEX: 0" id="Button1" runat="server" Text="加载下一个" CssClass="button"></asp:button><asp:button style="Z-INDEX: 0" id="Button2" runat="server" Text="保  存" CssClass="button"></asp:button></td>
				</tr>
				<tr>
					<td class="sub_title" colSpan="6">客户信息
					</td>
				</tr>
				<TR>
					<TD class="bg_column">公司</TD>
					<TD class="bg_data" colSpan="5"><asp:textbox id="txtCompany" runat="server" Width="100%"></asp:textbox></TD>
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
					<td class="bg_column">客户来源</td>
					<td class="bg_data"><asp:dropdownlist style="Z-INDEX: 0" id="ddlCustomerType" runat="server"></asp:dropdownlist></td>
					<td class="bg_column">联系状态</td>
					<td class="bg_data"><asp:dropdownlist id="ddlContactState" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
					<td class="bg_column">所属区域</td>
					<td class="bg_data"><asp:textbox id="TxtArea" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="bg_column">生日</td>
					<td class="bg_data"><INPUT id="TxtBirthday" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="TxtBirthday" runat="server"></td>
					<td class="bg_column">客户身份证号</td>
					<td class="bg_data"><asp:textbox id="TxtIdCard" runat="server"></asp:textbox></td>
					<td class="bg_column">介绍人</td>
					<td class="bg_data"><asp:textbox id="TxtIntroducer" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="bg_column">失败原因</td>
					<td class="bg_data"><asp:dropdownlist id="ddlFailedReason" runat="server"></asp:dropdownlist></td>
					<td class="bg_column">联系地址</td>
					<td class="bg_data" colSpan="3"><asp:textbox id="TxtAddress" runat="server" Width="100%"></asp:textbox></td>
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
					<td class="bg_column">服务类别</td>
					<td class="bg_data"><asp:dropdownlist id="ddlServiceType" runat="server"></asp:dropdownlist></td>
					<td class="bg_column"></td>
					<td class="bg_data"></td>
				</tr>
				<tr>
					<td class="sub_title" colSpan="6">车辆信息
					</td>
				</tr>
				<tr>
					<td class="bg_column">品牌</td>
					<td class="bg_data"><asp:dropdownlist id="ddlBrand" runat="server"></asp:dropdownlist></td>
					<td class="bg_column">车牌</td>
					<td class="bg_data"><asp:textbox id="TxtLicensePlate" runat="server"></asp:textbox></td>
					<td class="bg_column">购车日期</td>
					<td class="bg_data"><INPUT id="TxtSalesDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="TxtSalesDate" runat="server"></td>
				</tr>
				<tr>
					<td class="bg_column">当前里程</td>
					<td class="bg_data"><asp:textbox id="TxtCurrentMileage" runat="server" ReadOnly="True"></asp:textbox></td>
					<td class="bg_column">车型</td>
					<td class="bg_data"><asp:textbox id="TxtModel" runat="server"></asp:textbox></td>
					<td class="bg_column">颜色</td>
					<td class="bg_data"><asp:dropdownlist id="ddlShapeColors" runat="server">
							<asp:ListItem Value="" Selected="True">请选择颜色</asp:ListItem>
							<asp:ListItem Value="红">红</asp:ListItem>
							<asp:ListItem Value="橙">橙</asp:ListItem>
							<asp:ListItem Value="黄">黄</asp:ListItem>
							<asp:ListItem Value="绿">绿</asp:ListItem>
							<asp:ListItem Value="青">青</asp:ListItem>
							<asp:ListItem Value="蓝">蓝</asp:ListItem>
							<asp:ListItem Value="紫">紫</asp:ListItem>
							<asp:ListItem Value="黑">黑</asp:ListItem>
							<asp:ListItem Value="白">白</asp:ListItem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td class="bg_column">保险到期</td>
					<td class="bg_data"><INPUT id="TxtExpireDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="TxtExpireDate" runat="server"></td>
					<td class="bg_column">日均里程</td>
					<td class="bg_data"><asp:textbox id="TxtAverageMileage" runat="server" ReadOnly="True"></asp:textbox></td>
					<td class="bg_column">VIN</td>
					<td class="bg_data"><asp:textbox id="TxtVIN" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="bg_column">发动机号</td>
					<td class="bg_data"><asp:textbox id="TxtEngineNo" runat="server"></asp:textbox></td>
					<td class="bg_column"></td>
					<td class="bg_data"></td>
					<td class="bg_column"></td>
					<td class="bg_data"></td>
				</tr>
				<tr>
					<td class="sub_title" colSpan="6">保险信息
					</td>
				</tr>
				<tr>
					<td colSpan="6">
						<table width="100%">
							<tr>
								<td class="bg_column">车损</td>
								<td class="bg_data"><asp:textbox id="TxtCheSun" runat="server"></asp:textbox></td>
								<td class="bg_column">划痕</td>
								<td class="bg_data"><asp:textbox id="TxtHuaHen" runat="server"></asp:textbox></td>
								<td class="bg_column">商业险总计</td>
								<td class="bg_data"><asp:textbox id="TxtInsuranceFees" runat="server"></asp:textbox></td>
								<td class="bg_column">保险公司</td>
								<td class="bg_data"><asp:dropdownlist id="ddlInsuranceCompany" runat="server"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td class="bg_column">三者</td>
								<td class="bg_data"><asp:textbox id="TxtSanZhe" runat="server"></asp:textbox></td>
								<td class="bg_column">倒车镜</td>
								<td class="bg_data"><asp:textbox id="TxtDaoCheJing" runat="server"></asp:textbox></td>
								<td class="bg_column">交强险</td>
								<td class="bg_data"><asp:textbox id="TxtForceInsur" runat="server"></asp:textbox></td>
								<td class="bg_column">商业折扣</td>
								<td class="bg_data"><asp:textbox id="TxtReturnPoint" runat="server"></asp:textbox>%</td>
							</tr>
							<tr>
								<td class="bg_column">人员</td>
								<td class="bg_data"><asp:textbox id="TxtRenYuan" runat="server"></asp:textbox></td>
								<td class="bg_column">玻璃</td>
								<td class="bg_data"><asp:textbox id="TxtBoLi" runat="server"></asp:textbox></td>
								<td class="bg_column">车船</td>
								<td class="bg_data"><asp:textbox id="TxtTravelTax" runat="server"></asp:textbox></td>
								<td class="bg_column">交强险折扣</td>
								<td class="bg_data"><asp:textbox id="TxtTrafficPoint" runat="server"></asp:textbox>%</td>
							</tr>
							<tr>
								<td class="bg_column">盗抢</td>
								<td class="bg_data"><asp:textbox id="TxtDaoQiang" runat="server"></asp:textbox></td>
								<td class="bg_column">涉水</td>
								<td class="bg_data"><asp:textbox id="TxtSheShui" runat="server"></asp:textbox></td>
								<td class="bg_column"></td>
								<td class="bg_data"></td>
								<td class="bg_column">保险折扣</td>
								<td class="bg_data"><asp:textbox id="TxtInsurancePoint" runat="server"></asp:textbox>%</td>
							</tr>
							<tr>
								<td class="bg_column">不计免赔</td>
								<td class="bg_data"><asp:textbox id="TxtBuJiMianPei" runat="server"></asp:textbox></td>
								<td class="bg_column">自燃</td>
								<td class="bg_data"><asp:textbox id="TxtZiRan" runat="server"></asp:textbox></td>
								<td class="bg_column"></td>
								<td class="bg_data"></td>
								<td class="bg_column">出单日期</td>
								<td class="bg_data"><INPUT id="txtSingleDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
										name="txtSingleDate" runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="sub_title" colSpan="6">本次联系情况</td>
				</tr>
				<tr>
					<td class="bg_column">联系人</td>
					<td class="bg_data"><asp:textbox id="TxtAgent" runat="server" ReadOnly="True" Enabled="False"></asp:textbox></td>
					<td class="bg_column">联系时间</td>
					<td class="bg_data"><asp:textbox id="TxtInterviewTime" runat="server" ReadOnly="True" Enabled="False"></asp:textbox></td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td class="bg_column">预约到店日期</td>
					<td class="bg_data"><INPUT id="txtViewDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="txtViewDate" runat="server"></td>
					<td class="bg_column">预约到店时间</td>
					<td class="bg_data"><asp:dropdownlist id="ddlViewTime" runat="server"></asp:dropdownlist></td>
					<td></td>
					<td><INPUT style="Z-INDEX: 0" id="hdPersonID" type="hidden" name="hdPersonID" runat="server"><INPUT style="Z-INDEX: 0" id="hdCarID" type="hidden" name="hdCarID" runat="server"></td>
				</tr>
				<tr>
					<td class="sub_title" colSpan="6">本次联系结果</td>
				</tr>
				<tr>
					<td colSpan="6"><TEXTAREA style="WIDTH: 100%; HEIGHT: 60px" id="TxtComment" cols="24" name="TEXTAREA1" runat="server">											</TEXTAREA></td>
				</tr>
			</TABLE>
			<table style="WIDTH: 100%; DISPLAY: none; BORDER-COLLAPSE: collapse" border="1">
				<tr>
					<td class="sub_title" colSpan="6">历史联系情况</td>
				</tr>
				<tr>
					<td colSpan="6"><TEXTAREA style="WIDTH: 100%; HEIGHT: 60px" id="TxtCommentHis" cols="24" readOnly name="TEXTAREA2"
							runat="server">	</TEXTAREA>
					</td>
				</tr>
			</table>
			<table style="WIDTH: 100%; BORDER-COLLAPSE: collapse" border="1">
				<tr>
					<td class="sub_title" colSpan="6">历史联系情况</td>
				</tr>
				<tr>
					<td colSpan="6"><asp:datagrid id="dgdSearchResult" runat="server" Width="100%" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="#CCCCFF"></AlternatingItemStyle>
							<HeaderStyle BackColor="#CCCCFF"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="序号">
									<ItemTemplate>
										<%# this.dgdSearchResult.CurrentPageIndex * this.dgdSearchResult.PageSize + Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="interviewTime" HeaderText="联系时间"></asp:BoundColumn>
								<asp:BoundColumn DataField="comment" HeaderText="备注"></asp:BoundColumn>
								<asp:BoundColumn DataField="agentName" HeaderText="操作者"></asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
