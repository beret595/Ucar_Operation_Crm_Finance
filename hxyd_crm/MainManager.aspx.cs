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
	/// WebForm1 ��ժҪ˵����
	/// </summary>
	public class MainManager : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lbUserName;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
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
