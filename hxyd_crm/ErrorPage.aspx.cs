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

using Powerise.Hygeia.Framework;
using Powerise.Hygeia.Framework.util;
using Powerise.Hygeia.Framework.exception;

namespace Powerise.Hygeia.Web.UI
{
	/// <summary>
	/// 错误处理页面
	/// </summary>
	public class ErrorPage : System.Web.UI.Page
	{
		protected String ServerLogUrl = "服务器错误日志";
		private void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		/// <summary>
		/// 输出日志地址
		/// </summary>
		/// <returns></returns>
		protected void RenderLogUrl()
		{
			try
			{
				UserIndentity userIndentity = CookieHelper.getUserIndentity(this.Page);
				if (userIndentity != null)
				{
					Object obj = userIndentity.UserInfo["is_developer"];
					if (obj != null && obj.ToString().Equals("1"))
					{
						ServerLogUrl = String.Format("<a href=\"biz/Sys/ServerLog.aspx\" target=\"_self\" title=\"查看{0}的服务器日志\">服务器错误日志</a>", AppConfig.InstanceName);
					}
				}
			}
			catch (System.Exception ex)
			{
				ServerLogUrl = "服务器错误日志";
				LogHelper.log(this.GetType(), LogType.ERROR, "显示错误页面出错", ex);
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

			RenderLogUrl();
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
