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
using System.Text;

using hxyd_biz;
using CaseyLib;
using CaseyLib.util;
using Powerise.Hygeia.Web.UI.WebControls;

namespace casey.hxyd_crm.Web.UI
{
	/// <summary>
	/// WebForm1 的摘要说明。
	/// </summary>
	public class MainManager : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lbUserName;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			try
			{
				if(!CookieHelper.isLogin(this))
				{
					Response.Redirect("index.aspx", false);
					return;
				}	
			

				
				lbUserName.Text =CookieHelper.getUserIndentity(this).UserInfo["username"].ToString();	

				string strRight=CookieHelper.getUserIndentity(this).UserInfo["role"].ToString();	
				StringBuilder sbScript=new StringBuilder();
				sbScript.Append("  S('li_Manage').style.display='none';");
				sbScript.Append("  S('li_Profit').style.display='none';");
				sbScript.Append("  S('li_Company').style.display='none';");
				sbScript.Append("  S('li_Import').style.display='none';");
				sbScript.Append("  S('li_KPI').style.display='none';");
				
				sbScript.Append("  S('li_Mileage').style.display='none';");
				sbScript.Append("  S('li_Taskfenpei').style.display='none';");
				sbScript.Append("  S('li_Data').style.display='none';");
				
				
				string strDisplay=null;
				DateTime dtDisplay=new DateTime(2014,8,1);
				if(DateTime.Now.CompareTo(dtDisplay)<0)
				{
					sbScript.Append("  S('divSupport').style.display='none';");
					strDisplay=" S('divSupport').style.display='none';";
				}
				
				if(strRight=="admin")
				{
					JavaScriptHelper.RunScript(this,ScriptPos.End,strDisplay);
					return;
				}
				else
				{
					JavaScriptHelper.RunScript(this,ScriptPos.End,sbScript.ToString());
					return;
				}

				
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			
				
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
