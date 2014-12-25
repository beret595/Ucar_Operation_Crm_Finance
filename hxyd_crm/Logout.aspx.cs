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

//using Powerise.Hygeia.Framework;
//using Powerise.Hygeia.Framework.util;
using hxyd_biz;
using CaseyLib;
using CaseyLib.util;
using Powerise.Hygeia.Web.UI.WebControls;

namespace casey.hxyd_crm.Web.UI
{
	/// <summary>
	/// 用户注销。
	/// </summary>
	public class Logout : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			string action = PageHelper.getQueryString(this, "action", "").ToLower();

			//移除用户
			UserIndentity user = CookieHelper.getUserIndentity(this);
			if(user != null)
			{
				//StaffMapping.getInstance().remvoe(user.LoginUser);
			}
			else//不是登录用户直接跳转到登录页面
			{
				action = "relogin";
			}

			//删除cookie
			CookieHelper.delCookie(Context);
			
			if(action == "relogin")
			{
				Response.Redirect(("index.aspx"),false);
			}
			else
			{
				this.Page.RegisterStartupScript("", "<script>setTimeout(\"location.href='about:blank';window.opener=null;window.open('', '_self');window.close();\", 0)</script>");
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
