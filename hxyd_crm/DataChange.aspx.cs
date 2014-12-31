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
	/// DataChange 的摘要说明。
	/// </summary>
	public class DataChange : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label Label2;
	
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
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			UserAssignHelper objuser = new UserAssignHelper();
			int num_car = objuser.Update_Car_Other()-1;
			Label2.Text = "本次更新共更新"+num_car+"条车辆数据";
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			UserAssignHelper objuser = new UserAssignHelper();
			int num_user = objuser.update_Indi_Other()-1;
			Label1.Text = "本次更新共更新"+num_user+"条人员数据";
		}
	}
}
