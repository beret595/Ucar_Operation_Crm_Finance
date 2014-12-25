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

using hxyd_biz;
using CaseyLib;
using CaseyLib.util;
using Powerise.Hygeia.Web.UI.WebControls;

namespace hxyd_crm
{
	/// <summary>
	/// ModifyPassWord ��ժҪ˵����
	/// </summary>
	public class ModifyPassWord : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TxtOldPass;
		protected System.Web.UI.WebControls.TextBox TxtNewPass;
		protected System.Web.UI.WebControls.TextBox TxtConfirmPass;
		protected System.Web.UI.WebControls.Button btnSave;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!CookieHelper.isLogin(this))
			{
				string strScript="window.parent.location='index.aspx';";
				JavaScriptHelper.RunScript(this,ScriptPos.Begin, strScript);
				return;
			}	
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strUserID=CookieHelper.getUserIndentity(this).UserInfo["userid"].ToString();
				string strPassword=CryptoHelper.CommonDecrypt( CookieHelper.getUserIndentity(this).UserInfo["userid"].ToString());

				if(TxtOldPass.Text.Trim()!=strPassword)
				{
					JavaScriptHelper.AlertMessage(this,"���������");
					return ;
				}
				if(TxtNewPass.Text.Trim().Length<5)
				{
					JavaScriptHelper.AlertMessage(this,"���벻��С��5���ַ�!");
					return;
				}
				if(TxtNewPass.Text.Trim()!=TxtConfirmPass.Text.Trim())
				{
					JavaScriptHelper.AlertMessage(this,"��������ȷ�����벻һ��");
					return;
				}

				if(UserHelper.ModifyPassword(CryptoHelper.CommonEncrypt(TxtNewPass.Text.Trim()),strUserID))
				{
					JavaScriptHelper.AlertMessage(this,"�����޸ĳɹ�!");
					JavaScriptHelper.RunScript(this,ScriptPos.End,"window.close();");
					
				}
				
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}
	}
}
