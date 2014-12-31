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


namespace hxyd_crm
{
	/// <summary>
	/// DataChange ��ժҪ˵����
	/// </summary>
	public class DataChange : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label Label2;
	
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
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			UserAssignHelper objuser = new UserAssignHelper();
			int num_car = objuser.Update_Car_Other()-1;
			Label2.Text = "���θ��¹�����"+num_car+"����������";
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			UserAssignHelper objuser = new UserAssignHelper();
			int num_user = objuser.update_Indi_Other()-1;
			Label1.Text = "���θ��¹�����"+num_user+"����Ա����";
		}
	}
}
