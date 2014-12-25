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
	/// index 的摘要说明。
	/// </summary>
	public class index : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TxtPassWord;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.TextBox TxtUserName;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			try
			{
			
				//获取用户名、密码
				string strUserName=TxtUserName.Text.Trim();
				string strPassword=TxtPassWord.Text.Trim();
				DataRow dr= StaffMapping.getInstance()[strUserName];
				if(dr==null)
				{
					JavaScriptHelper.AlertMessage(this,"不存在的用户名");
					return;
				}
				if(strPassword==CryptoHelper.CommonDecrypt( dr["userPassword"].ToString()))
				{
					string strCryPass = CryptoHelper.CommonEncrypt(strPassword);
				}
				else
				{
					JavaScriptHelper.AlertMessage(this,"密码错误!");
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
				//日志记录
			}
		
		}
	}
}
