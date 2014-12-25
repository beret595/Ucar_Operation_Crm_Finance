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
	/// AgentKPI ��ժҪ˵����
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
				string strFileName="��Ч����"+DateTime.Now.ToString("yyyyMMddhhmmss")+".xls";
				string strFullName=strPath+"\\"+strFileName;


				Hashtable htbExportColumn=new Hashtable();
				htbExportColumn["userName"]="agent����";
				htbExportColumn["begin_date"]="ͳ�ƿ�ʼ����";
				htbExportColumn["end_date"]="ͳ�ƽ�ֹ����";
				//htbExportColumn["call_num"]="�����";
				//htbExportColumn["success_num"]="�ɹ���";
				//htbExportColumn["success_rate"]="�ɹ���";
//				htbExportColumn["call_num"]="�������";
//				htbExportColumn["arrive_num"]="�ɹ�������";
//				htbExportColumn["renewal_num"]="�����ɹ���";
//				htbExportColumn["arrive_rate"]="������";
				htbExportColumn["invalid_num"]="��Ч�绰";
				htbExportColumn["call_num"]="�������";
				htbExportColumn["zwyx_num"]="��������";
				htbExportColumn["xygj_num"]="��Ҫ����";
				htbExportColumn["khyy_num"]="�ͻ�ԤԼ";
				htbExportColumn["xbgj_num"]="��������";
				htbExportColumn["arrive_num"]="�ͻ�����";
				htbExportColumn["success_rate"]="�ɹ���";
		
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
