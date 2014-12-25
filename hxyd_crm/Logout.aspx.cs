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
	/// �û�ע����
	/// </summary>
	public class Logout : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			string action = PageHelper.getQueryString(this, "action", "").ToLower();

			//�Ƴ��û�
			UserIndentity user = CookieHelper.getUserIndentity(this);
			if(user != null)
			{
				//StaffMapping.getInstance().remvoe(user.LoginUser);
			}
			else//���ǵ�¼�û�ֱ����ת����¼ҳ��
			{
				action = "relogin";
			}

			//ɾ��cookie
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
