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
	/// ModifyPassWord 的摘要说明。
	/// </summary>
	public class ModifyPassWord : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TxtOldPass;
		protected System.Web.UI.WebControls.TextBox TxtNewPass;
		protected System.Web.UI.WebControls.TextBox TxtConfirmPass;
		protected System.Web.UI.WebControls.Button btnSave;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!CookieHelper.isLogin(this))
			{
				string strScript="window.parent.location='index.aspx';";
				JavaScriptHelper.RunScript(this,ScriptPos.Begin, strScript);
				return;
			}	
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
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
					JavaScriptHelper.AlertMessage(this,"旧密码错误");
					return ;
				}
				if(TxtNewPass.Text.Trim().Length<5)
				{
					JavaScriptHelper.AlertMessage(this,"密码不能小于5个字符!");
					return;
				}
				if(TxtNewPass.Text.Trim()!=TxtConfirmPass.Text.Trim())
				{
					JavaScriptHelper.AlertMessage(this,"新密码与确认密码不一致");
					return;
				}

				if(UserHelper.ModifyPassword(CryptoHelper.CommonEncrypt(TxtNewPass.Text.Trim()),strUserID))
				{
					JavaScriptHelper.AlertMessage(this,"密码修改成功!");
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
