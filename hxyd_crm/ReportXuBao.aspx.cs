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
using System.IO;
using Powerise.Hygeia.Web.UI.WebControls;

namespace casey.hxyd_crm.Web.UI
{
	/// <summary>
	/// AgentKPI 的摘要说明。
	/// </summary>
	public class ReportXuBao : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Button btnExport;
		protected System.Web.UI.WebControls.DataGrid dgdAgentAPI;
		protected System.Web.UI.HtmlControls.HtmlInputText txtInterViewTime;
		protected System.Web.UI.HtmlControls.HtmlInputText TxtEndTime;
	
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
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			try
			{

				DataTable dt=QueryXuBao();
				DataGridHelper.bindData(dgdAgentAPI,dt);
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}
		private DataTable QueryXuBao()
		{
			string strBeginDate=txtInterViewTime.Value;
			string strEndDate=TxtEndTime.Value;
			return Customer.QueryXuBao(strBeginDate,strEndDate);
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			

			try
			{
				//DataTable dt= Customer.GetCustomerSaleInfo(htbCondition);
				DataTable dt=QueryXuBao();




				string strPath = HttpContext.Current.Server.MapPath("~");
				string strFileName="续保台帐.xls";
				string strFullName=strPath+"\\template\\"+strFileName;

				string strDesFileName= strPath+"\\temp\\续保台帐"+DateTime.Now.ToString("yyyyMMddhhmmss")+".xls";


//				Hashtable htbExportColumn=new Hashtable();
//				htbExportColumn["userName"]="agent姓名";
//				htbExportColumn["begin_date"]="统计开始日期";
//				htbExportColumn["end_date"]="统计截止日期";
//				htbExportColumn["call_num"]="外呼量";
//				htbExportColumn["success_num"]="成功量";
//				htbExportColumn["success_rate"]="成功率";
// 
//				//DataTable dtExport = FileHelper.ExportTransfer(htbExportColumn,dt);
				File.Copy(strFullName,strDesFileName,true);

				BizFileHelper.WriteXLSFile(strDesFileName,dt);
				FileDownHelper.DownFile(this.Context,strDesFileName,true);

			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}
	}
}
