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
using System.IO;

namespace casey.hxyd_crm.Web.UI
{
	/// <summary>
	/// AgentKPI 的摘要说明。
	/// </summary>
	public class AgentKPI : System.Web.UI.Page
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

				DataTable dt=QueryKPI();
				DataGridHelper.bindData(dgdAgentAPI,dt);
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}
		private DataTable QueryKPI()
		{
			string strBeginDate=txtInterViewTime.Value;
			string strEndDate=TxtEndTime.Value;
			return Customer.QueryAgentKPI(strBeginDate,strEndDate);
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			try
			{
				//DataTable dt= Customer.GetCustomerSaleInfo(htbCondition);
				DataTable dt=QueryKPI();




				string strPath = HttpContext.Current.Server.MapPath("~")+"\\temp";
				string strFileName="绩效考核"+DateTime.Now.ToString("yyyyMMddhhmmss")+".xls";
				string strFullName=strPath+"\\"+strFileName;


				Hashtable htbExportColumn=new Hashtable();
				htbExportColumn["userName"]="agent姓名";
				htbExportColumn["begin_date"]="统计开始日期";
				htbExportColumn["end_date"]="统计截止日期";
				//htbExportColumn["call_num"]="外呼量";
				//htbExportColumn["success_num"]="成功量";
				//htbExportColumn["success_rate"]="成功率";
//				htbExportColumn["call_num"]="外呼数量";
//				htbExportColumn["arrive_num"]="成功到店量";
//				htbExportColumn["renewal_num"]="续保成功量";
//				htbExportColumn["arrive_rate"]="到店率";
				htbExportColumn["invalid_num"]="无效电话";
				htbExportColumn["call_num"]="外呼总量";
				htbExportColumn["zwyx_num"]="暂无意向";
				htbExportColumn["xygj_num"]="需要跟进";
				htbExportColumn["khyy_num"]="客户预约";
				htbExportColumn["xbgj_num"]="续保跟进";
				htbExportColumn["arrive_num"]="客户到店";
				htbExportColumn["success_rate"]="成功率";
		
				DataTable dtExport = FileHelper.ExportTransfer(htbExportColumn,dt);

				FileHelper.ExportXLSFile(dtExport,strFullName);
				FileDownHelper.DownFile(this.Context,strFullName,true);

			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}




		}
	}
}
