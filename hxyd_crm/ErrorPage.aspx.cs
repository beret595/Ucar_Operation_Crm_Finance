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
	/// ������ҳ��
	/// </summary>
	public class ErrorPage : System.Web.UI.Page
	{
		protected String ServerLogUrl = "������������־";
		private void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		/// <summary>
		/// �����־��ַ
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
						ServerLogUrl = String.Format("<a href=\"biz/Sys/ServerLog.aspx\" target=\"_self\" title=\"�鿴{0}�ķ�������־\">������������־</a>", AppConfig.InstanceName);
					}
				}
			}
			catch (System.Exception ex)
			{
				ServerLogUrl = "������������־";
				LogHelper.log(this.GetType(), LogType.ERROR, "��ʾ����ҳ�����", ex);
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

			RenderLogUrl();
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
