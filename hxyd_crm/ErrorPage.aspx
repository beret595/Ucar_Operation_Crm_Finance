<%@ Page language="c#" codebehind="ErrorPage.aspx.cs" autoeventwireup="false" inherits="Powerise.Hygeia.Web.UI.ErrorPage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML	4.0	Transitional//EN" >
<html>
<head>
    <title>����</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio	.NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <style>
      body{background:#eee;color:#000;font:14px "����",verdana,arial,sans-serif;}
	  #errorinfo{background:#fff;border:#bbb 1px solid;color:#f00;margin:32px;padding:32px;}
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div id="errorinfo">
        �ܱ�Ǹ�������ʵ�ҳ����ִ�����ϸ��Ϣ��鿴<%=ServerLogUrl%>��
    </div>
    </form>
</body>
</html>