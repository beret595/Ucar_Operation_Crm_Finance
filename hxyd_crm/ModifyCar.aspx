<%@ Register TagPrefix="cc1" Namespace="Powerise.Hygeia.Web.UI.WebControls" Assembly="Powerise.Hygeia.Web.UI.WebControls" %>
<%@ Page language="c#" Codebehind="ModifyCar.aspx.cs" AutoEventWireup="false" Inherits="hxyd_crm.ModifyCar" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ModifyCar</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="JavaScript" src="./js/datepicker/WdatePicker.js"></SCRIPT>
		<script type="text/javascript" src="./js/business.js"></script>
		<LINK rel="stylesheet" type="text/css" href="./css/special.css">
		<LINK rel="stylesheet" type="text/css" href="./css/css.css">
		<script type="text/javascript">
			function calculation(){
    
				var d = new Date(); 
				
				var str = d.getFullYear()+"-"+(d.getMonth()+1)+"-"+d.getDate(); 
				
				
				if(document.all.TxtSalesDate.value=="")
				{
					alert("������д�������ڣ�");
					document.all.TxtSalesDate.focus();
					document.all.TxtCurrentMileage.value="";
					return;
				}
				
				if(document.all.TxtCurrentMileage.value>0)
				{
					var days=DateDiff(str,document.all.TxtSalesDate.value)+1;
					document.all.TxtAverageMileage.value=(document.all.TxtCurrentMileage.value/days).toFixed(2);
				}
				 						
				
			}
			
			function DateDiff(startDate, endDate){
				var aDate, oDate1, oDate2, iDays ;
				aDate = startDate.split('-');
				oDate1 = new Date(aDate[1]+'-'+aDate[2]+'-'+aDate[0]) ;
				aDate = endDate.split('-');
				oDate2 = new Date(aDate[1]+'-'+ aDate[2] +'-'+aDate[0]);
				iDays = parseInt((oDate1 -oDate2)/1000/60/60/24); //�����ĺ�����ת��Ϊ����
			 return iDays ;
}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE style="WIDTH: 100%; BORDER-COLLAPSE: collapse" border="1">
				<tr>
					<td class="bg_data" colSpan="4" align="center"><asp:button id="btnClear" runat="server" CssClass="button" Text="��  ��"></asp:button><asp:button style="Z-INDEX: 0" id="btnSave" runat="server" CssClass="button" Text="��  ��"></asp:button><asp:button style="Z-INDEX: 0" id="btnClose" runat="server" CssClass="button" Text="��  ��"></asp:button></td>
				</tr>
			</TABLE>
			<TABLE style="WIDTH: 100%; BORDER-COLLAPSE: collapse" border="1">
				<tr>
					<TD class="sub_title" colSpan="6">������Ϣ<INPUT id="hdID" type="hidden" name="hdID" runat="server"><INPUT id="hdPersonId" type="hidden" name="hdPersonId" runat="server"></TD>
				</tr>
				<tr>
					<td class="bg_column">�ͻ�����</td>
					<td class="bg_data"><asp:textbox id="TxtPersonName" ReadOnly="True" runat="server"></asp:textbox></td>
					<td class="bg_column">����</td>
					<td class="bg_data"><asp:textbox id="TxtModel" runat="server"></asp:textbox></td>
					<td class="bg_column">Ʒ��</td>
					<td class="bg_data"><asp:dropdownlist id="ddlBrand" runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td class="bg_column">VIN</td>
					<td class="bg_data"><asp:textbox id="TxtVIN" runat="server"></asp:textbox></td>
					<td class="bg_column">����</td>
					<td class="bg_data"><asp:textbox id="TxtLicensePlate" runat="server"></asp:textbox></td>
					<td class="bg_column">��������</td>
					<td class="bg_data"><asp:textbox id="TxtEngineNo" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="bg_column">��������</td>
					<td class="bg_data"><INPUT id="TxtSalesDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="TxtSalesDate" runat="server"></td>
					<td class="bg_column">��ǰ�������</td>
					<td class="bg_data"><asp:textbox onblur="calculation()" id="TxtCurrentMileage" runat="server"></asp:textbox></td>
					<td class="bg_column">�վ����</td>
					<td class="bg_data"><asp:textbox ReadOnly="True" id="TxtAverageMileage" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="bg_column">���յ���</td>
					<td class="bg_data"><INPUT id="TxtExpireDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="TxtExpireDate" runat="server"></td>
					<td class="bg_column">����״̬</td>
					<td class="bg_data"><asp:dropdownlist id="ddlCarType" runat="server"></asp:dropdownlist></td>
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
					<td class="bg_column">��������</td>
					<td class="bg_data"><INPUT id="TxtKeepDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="TxtKeepDate" runat="server"></td>
					<td class="bg_column"></td>
					<td class="bg_data"></td>
					<td class="bg_column"></td>
					<td class="bg_data"></td>
				</tr>
				<tr>
					<td class="sub_title" colSpan="6">�������</td>
				</tr>
				<tr>
					<td colSpan="6"><asp:datagrid id="dgdSearchResult" runat="server" Width="100%" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="#CCCCFF"></AlternatingItemStyle>
							<HeaderStyle BackColor="#CCCCFF"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="carId" HeaderText="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="���">
									<ItemTemplate>
										<%# this.dgdSearchResult.CurrentPageIndex * this.dgdSearchResult.PageSize + Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="personName" HeaderText="�ͻ�"></asp:BoundColumn>
								<asp:BoundColumn DataField="manufacturers" HeaderText="Ʒ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="VIN" HeaderText="VIN"></asp:BoundColumn>
								<asp:BoundColumn DataField="car_model" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="expire_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="���յ���"></asp:BoundColumn>
								<asp:ButtonColumn Text="�༭" HeaderText="����" CommandName="Update"></asp:ButtonColumn>
								<asp:ButtonColumn Text="����" HeaderText="����" CommandName="Call"></asp:ButtonColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
