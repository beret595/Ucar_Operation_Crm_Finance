<%@ Page language="c#" Codebehind="index.aspx.cs" AutoEventWireup="false" Inherits="hxyd_crm.index" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>U车汇-汽车服务改装中心--客户关系管理系统</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script type="text/javascript">
    function setSize(){
    
				var height =  document.documentElement.offsetHeight;// documentElement.clientHeight;
				 
				if (height > 0) {
					var h = (height - 60);
			        
					document.getElementById("container").style.height = h+"px";
					document.getElementById("td_place1").style.height =Math.round( h/3)+"px";
					document.getElementById("td_place2").style.height =Math.round( h/3)+"px";
					document.getElementById("td_place0").style.height =Math.round( h/3)+"px";
				//	alter(document.getElementById("td_place2").style.height);
					
					
				}
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div id="container" style=" TEXT-ALIGN:center;  WIDTH:100%">
				<div id="divLogin">
					<table id="table1" width="100%" border="0">
						<tr>
							<td id="td_place1" width="30%">
							</td>
							<td width="40%">
							</td>
							<td width="30%">
							</td>
						</tr>
						<tr>
							<td class="td_place0"></td>
							<td align="center">
								<TABLE border="0" style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; WIDTH: 400px; BORDER-COLLAPSE: collapse; HEIGHT: 300px; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid">
									<tr>
										<td colspan="3" height="40" bgcolor="#003399" align="center">
											<font color="#ffffff" size="5" style="Z-INDEX: 0">U车汇-汽车服务改装中心</font></td>
									</tr>
									<tr>
										<td colspan="3" height="20">&nbsp;&nbsp;&nbsp;
										</td>
									</tr>
									<tr>
										<td colspan="3" height="20" bgcolor="#003399">&nbsp;&nbsp;&nbsp;
										</td>
									</tr>
									<tr height="20">
										<td colspan="2"></td>
									</tr>
									<TR height="20">
										<TD align="center">用户名</TD>
										<TD>
											<span style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid">
												<asp:TextBox id="TxtUserName" runat="server" Width="160" BorderStyle="None"></asp:TextBox></span></TD>
									</TR>
									<tr height="20">
										<td></td>
									</tr>
									<tr height="20">
										<TD align="center">密码</TD>
										<TD>
											<span style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid">
												<asp:TextBox id="TxtPassWord" runat="server" Width="160" BorderStyle="None" TextMode="Password"></asp:TextBox></span></TD>
									</tr>
									<tr height="20">
										<td colspan="2"></td>
									</tr>
									<tr height="20">
										<td colspan="2" align="center">
											<asp:Button id="Button1" runat="server" Text=" 登  录 "></asp:Button></td>
									</tr>
									<tr>
										<td colspan="2">&nbsp;&nbsp;&nbsp;</td>
									</tr>
								</TABLE>
							</td>
							<td></td>
						</tr>
						<tr>
							<td id="td_place2">
							</td>
							<td>
							</td>
							<td>
							</td>
						</tr>
					</table>
				</div>
			</div>
			<script type="text/javascript">
	setSize();
			</script>
		</form>
	</body>
</HTML>
