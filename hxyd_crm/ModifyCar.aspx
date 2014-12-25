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
					alert("请先填写购车日期！");
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
				iDays = parseInt((oDate1 -oDate2)/1000/60/60/24); //把相差的毫秒数转换为天数
			 return iDays ;
}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE style="WIDTH: 100%; BORDER-COLLAPSE: collapse" border="1">
				<tr>
					<td class="bg_data" colSpan="4" align="center"><asp:button id="btnClear" runat="server" CssClass="button" Text="新  增"></asp:button><asp:button style="Z-INDEX: 0" id="btnSave" runat="server" CssClass="button" Text="保  存"></asp:button><asp:button style="Z-INDEX: 0" id="btnClose" runat="server" CssClass="button" Text="返  回"></asp:button></td>
				</tr>
			</TABLE>
			<TABLE style="WIDTH: 100%; BORDER-COLLAPSE: collapse" border="1">
				<tr>
					<TD class="sub_title" colSpan="6">车辆信息<INPUT id="hdID" type="hidden" name="hdID" runat="server"><INPUT id="hdPersonId" type="hidden" name="hdPersonId" runat="server"></TD>
				</tr>
				<tr>
					<td class="bg_column">客户姓名</td>
					<td class="bg_data"><asp:textbox id="TxtPersonName" ReadOnly="True" runat="server"></asp:textbox></td>
					<td class="bg_column">车型</td>
					<td class="bg_data"><asp:textbox id="TxtModel" runat="server"></asp:textbox></td>
					<td class="bg_column">品牌</td>
					<td class="bg_data"><asp:dropdownlist id="ddlBrand" runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td class="bg_column">VIN</td>
					<td class="bg_data"><asp:textbox id="TxtVIN" runat="server"></asp:textbox></td>
					<td class="bg_column">车牌</td>
					<td class="bg_data"><asp:textbox id="TxtLicensePlate" runat="server"></asp:textbox></td>
					<td class="bg_column">发动机号</td>
					<td class="bg_data"><asp:textbox id="TxtEngineNo" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="bg_column">购车日期</td>
					<td class="bg_data"><INPUT id="TxtSalesDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="TxtSalesDate" runat="server"></td>
					<td class="bg_column">当前车辆里程</td>
					<td class="bg_data"><asp:textbox onblur="calculation()" id="TxtCurrentMileage" runat="server"></asp:textbox></td>
					<td class="bg_column">日均里程</td>
					<td class="bg_data"><asp:textbox ReadOnly="True" id="TxtAverageMileage" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="bg_column">保险到期</td>
					<td class="bg_data"><INPUT id="TxtExpireDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="TxtExpireDate" runat="server"></td>
					<td class="bg_column">车辆状态</td>
					<td class="bg_data"><asp:dropdownlist id="ddlCarType" runat="server"></asp:dropdownlist></td>
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
					<td class="bg_column">保养日期</td>
					<td class="bg_data"><INPUT id="TxtKeepDate" class="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" size="26"
							name="TxtKeepDate" runat="server"></td>
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
								<asp:BoundColumn DataField="carId" HeaderText="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="序号">
									<ItemTemplate>
										<%# this.dgdSearchResult.CurrentPageIndex * this.dgdSearchResult.PageSize + Container.ItemIndex + 1%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="personName" HeaderText="客户"></asp:BoundColumn>
								<asp:BoundColumn DataField="manufacturers" HeaderText="品牌"></asp:BoundColumn>
								<asp:BoundColumn DataField="VIN" HeaderText="VIN"></asp:BoundColumn>
								<asp:BoundColumn DataField="car_model" HeaderText="车型"></asp:BoundColumn>
								<asp:BoundColumn DataField="expire_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="保险到期"></asp:BoundColumn>
								<asp:ButtonColumn Text="编辑" HeaderText="操作" CommandName="Update"></asp:ButtonColumn>
								<asp:ButtonColumn Text="呼叫" HeaderText="呼叫" CommandName="Call"></asp:ButtonColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
