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
	/// Global ��ժҪ˵����
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{
			Timer t = new Timer(900000);//���ʱ���������һ��Сʱִ��һ�ξ͸�Ϊ3600000 ��������ӵ���һ��
����		t.Elapsed += new ElapsedEventHandler(t_Elapsed);
����		t.AutoReset = true;
����		t.Enabled = true;
		}

		private void t_Elapsed(object sender, ElapsedEventArgs e)
����	{
			//�����Ҫ���͵��ʼ���Ϣ
			DataTable dt = UserEmail.GetEmail_Data_ForPerson();
			DataTable dt_admin = UserEmail.GetEmail_Data_ForAdmin();
				if(dt.Rows.Count > 0)
				{
					string _server = "";//�����������
					string _name = "";	//����������
					string _pwd = "";	//����������
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

						//������microsoft cdo for windows 2000 Library(��com���) 			
						Msg.Fields.Add(CdoConfiguration.cdoSMTPAuthenticate, "1");    
						Msg.Fields.Add(CdoConfiguration.cdoSendUserName,_name);
						Msg.Fields.Add(CdoConfiguration.cdoSendPassword,_pwd);
						Msg.Fields.Add(CdoConfiguration.cdoSMTPServer,_server);
						Msg.Fields.Add(CdoConfiguration.cdoSMTPServerPort,_sendPort);	
						Msg.Subject = "��������";
						string strbody = "�װ����û�:<br>";
						strbody = strbody + "����һ�����ʼ�<br>";
						DataTable dt_context = UserEmail.GetEmail_Data(dt.Rows[k]["userId"].ToString());
						for(int i = 0; i<dt_context.Rows.Count; i++)
						{
							strbody+=dt_context.Rows[i]["personName"]+"�ͻ���������Ҫ��ȥ��������������ܽ���ȥ���ܸ�������;<br>";
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

						//������microsoft cdo for windows 2000 Library(��com���) 			
						Msg.Fields.Add(CdoConfiguration.cdoSMTPAuthenticate, "1");    
						Msg.Fields.Add(CdoConfiguration.cdoSendUserName,_name);
						Msg.Fields.Add(CdoConfiguration.cdoSendPassword,_pwd);
						Msg.Fields.Add(CdoConfiguration.cdoSMTPServer,_server);
						Msg.Fields.Add(CdoConfiguration.cdoSMTPServerPort,_sendPort);	
						Msg.Subject = "��������";
						strbody = "�װ����û�:<br>";
						strbody = strbody + "����һ�����ʼ�<br>";
						for(int i = 0; i<dt_context.Rows.Count; i++)
						{
							strbody+="������"+dt_context.Rows[i]["personName"]+"�ͻ����������˴�����������������ȥ���·��������;<br>";
						}
						Msg.Body = strbody;
						Msg.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
						Msg.BodyFormat = MailFormat.Html;
						Msg.Priority = MailPriority.Normal;				
						System.Web.Mail.SmtpMail.SmtpServer=_server;
						System.Web.Mail.SmtpMail.Send(Msg);			
			
						//���͸�����Ա 
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
							Msg.Subject = "��������";
							strbody = "�װ����û�:<br>";
							strbody = strbody + "����һ�����ʼ�<br>";
							for(int j = 0; j<dt_context.Rows.Count; j++)
							{
								strbody+="������"+dt_context.Rows[j]["personName"]+"�ͻ����������˴�����������������ȥ���·��������;<br>";
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
			
		#region Web ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

