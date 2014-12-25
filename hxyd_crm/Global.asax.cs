using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Timers;
using hxyd_biz;
using System.Data;
using CDO;
using System.IO;
using System.Web.Mail;

namespace hxyd_crm 
{
	/// <summary>
	/// Global 的摘要说明。
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{
			Timer t = new Timer(900000);//设计时间间隔，如果一个小时执行一次就改为3600000 ，这里分钟调用一次
　　		t.Elapsed += new ElapsedEventHandler(t_Elapsed);
　　		t.AutoReset = true;
　　		t.Enabled = true;
		}

		private void t_Elapsed(object sender, ElapsedEventArgs e)
　　	{
			//获得需要发送的邮件信息
			DataTable dt = UserEmail.GetEmail_Data_ForPerson();
			DataTable dt_admin = UserEmail.GetEmail_Data_ForAdmin();
				if(dt.Rows.Count > 0)
				{
					string _server = "";//发件箱服务器
					string _name = "";	//发件箱名称
					string _pwd = "";	//发件箱密码
					string _sendMail="";
					string _sendPort="";		
					_server =  System.Configuration.ConfigurationSettings.AppSettings["_server"].ToString().Trim();
					_name = System.Configuration.ConfigurationSettings.AppSettings["_name"].ToString().Trim();
					_pwd = System.Configuration.ConfigurationSettings.AppSettings["_pwd"].ToString().Trim();
					_sendMail=System.Configuration.ConfigurationSettings.AppSettings["_sendMail"].ToString().Trim();
					_sendPort="25";
					for(int k=0; k<dt.Rows.Count; k++)
					{
						string email = dt.Rows[k]["email"].ToString();
						System.Web.Mail.MailMessage Msg = new MailMessage();
						Msg.From = _sendMail;// "yefei<lisc@cosco.com>";
						Msg.To = email;

						//需引用microsoft cdo for windows 2000 Library(的com组件) 			
						Msg.Fields.Add(CdoConfiguration.cdoSMTPAuthenticate, "1");    
						Msg.Fields.Add(CdoConfiguration.cdoSendUserName,_name);
						Msg.Fields.Add(CdoConfiguration.cdoSendPassword,_pwd);
						Msg.Fields.Add(CdoConfiguration.cdoSMTPServer,_server);
						Msg.Fields.Add(CdoConfiguration.cdoSMTPServerPort,_sendPort);	
						Msg.Subject = "续保跟进";
						string strbody = "亲爱的用户:<br>";
						strbody = strbody + "您有一封新邮件<br>";
						DataTable dt_context = UserEmail.GetEmail_Data(dt.Rows[k]["userId"].ToString());
						for(int i = 0; i<dt_context.Rows.Count; i++)
						{
							strbody+=dt_context.Rows[i]["personName"]+"客户的任务需要您去处理，请在任务接受界面去接受该条任务;<br>";
						}
						Msg.Body = strbody;
						Msg.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
						Msg.BodyFormat = MailFormat.Html;
						Msg.Priority = MailPriority.Normal;				
						System.Web.Mail.SmtpMail.SmtpServer=_server;
						System.Web.Mail.SmtpMail.Send(Msg);

						email = dt.Rows[k]["to_email"].ToString();
						Msg = new MailMessage();
						Msg.From = _sendMail;// "yefei<lisc@cosco.com>";
						Msg.To = email;

						//需引用microsoft cdo for windows 2000 Library(的com组件) 			
						Msg.Fields.Add(CdoConfiguration.cdoSMTPAuthenticate, "1");    
						Msg.Fields.Add(CdoConfiguration.cdoSendUserName,_name);
						Msg.Fields.Add(CdoConfiguration.cdoSendPassword,_pwd);
						Msg.Fields.Add(CdoConfiguration.cdoSMTPServer,_server);
						Msg.Fields.Add(CdoConfiguration.cdoSMTPServerPort,_sendPort);	
						Msg.Subject = "续保跟进";
						strbody = "亲爱的用户:<br>";
						strbody = strbody + "您有一封新邮件<br>";
						for(int i = 0; i<dt_context.Rows.Count; i++)
						{
							strbody+="您分配"+dt_context.Rows[i]["personName"]+"客户的任务还无人处理，请在任务分配界面去重新分配该任务;<br>";
						}
						Msg.Body = strbody;
						Msg.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
						Msg.BodyFormat = MailFormat.Html;
						Msg.Priority = MailPriority.Normal;				
						System.Web.Mail.SmtpMail.SmtpServer=_server;
						System.Web.Mail.SmtpMail.Send(Msg);			
			
						//抄送给管理员 
						for(int i = 0; i<dt_admin.Rows.Count; i++)
						{
							email = dt_admin.Rows[i]["email"].ToString();
							Msg = new MailMessage();
							Msg.From = _sendMail;// "yefei<lisc@cosco.com>";
							Msg.To = email;

							Msg.Fields.Add(CdoConfiguration.cdoSMTPAuthenticate, "1");    
							Msg.Fields.Add(CdoConfiguration.cdoSendUserName,_name);
							Msg.Fields.Add(CdoConfiguration.cdoSendPassword,_pwd);
							Msg.Fields.Add(CdoConfiguration.cdoSMTPServer,_server);
							Msg.Fields.Add(CdoConfiguration.cdoSMTPServerPort,_sendPort);	
							Msg.Subject = "续保跟进";
							strbody = "亲爱的用户:<br>";
							strbody = strbody + "您有一封新邮件<br>";
							for(int j = 0; j<dt_context.Rows.Count; j++)
							{
								strbody+="您分配"+dt_context.Rows[j]["personName"]+"客户的任务还无人处理，请在任务分配界面去重新分配该任务;<br>";
							}
							Msg.Body = strbody;
							Msg.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
							Msg.BodyFormat = MailFormat.Html;
							Msg.Priority = MailPriority.Normal;				
							System.Web.Mail.SmtpMail.SmtpServer=_server;
							System.Web.Mail.SmtpMail.Send(Msg);
						}
					}				
				}
		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
			


		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{

		}

		protected void Session_End(Object sender, EventArgs e)
		{

		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
			
		#region Web 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

