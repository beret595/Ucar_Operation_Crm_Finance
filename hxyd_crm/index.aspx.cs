using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using CaseyLib;
using CaseyLib.util;
using Powerise.Hygeia.Web.UI.WebControls;

namespace hxyd_crm
{
	/// <summary>
	/// index ��ժҪ˵����
	/// </summary>
	public class index : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TxtPassWord;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.TextBox TxtUserName;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			try
			{
			
				//��ȡ�û���������
				string strUserName=TxtUserName.Text.Trim();
				string strPassword=TxtPassWord.Text.Trim();
				DataRow dr= StaffMapping.getInstance()[strUserName];
				if(dr==null)
				{
					JavaScriptHelper.AlertMessage(this,"�����ڵ��û���");
					return;
				}
				if(strPassword==CryptoHelper.CommonDecrypt( dr["userPassword"].ToString()))
				{
					string strCryPass = CryptoHelper.CommonEncrypt(strPassword);
				}
				else
				{
					JavaScriptHelper.AlertMessage(this,"�������!");
					return;
				}
//				Session["userid"]=dr["userid"];
//				Session["username"]=dr["username"];
				CookieHelper.setCookie(strUserName,this.Context);
			
				Response.Redirect("MainManager.aspx", false);
				
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
			finally
			{
				//��־��¼
			}
		
		}
	}
}
