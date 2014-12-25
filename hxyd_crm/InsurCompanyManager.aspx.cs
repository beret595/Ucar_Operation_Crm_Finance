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
using hxyd_biz;
using CaseyLib;
using CaseyLib.util;
using Powerise.Hygeia.Web.UI.WebControls;

namespace casey.hxyd_crm.Web.UI
{
	/// <summary>
	/// InsurCompanyManager ��ժҪ˵����
	/// </summary>
	public class InsurCompanyManager : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.WebControls.TextBox TxtID;
		protected System.Web.UI.WebControls.TextBox TxtCompanyName;
		protected System.Web.UI.WebControls.TextBox TxtRebate;
		protected System.Web.UI.WebControls.DataGrid dgdCompany;
		protected System.Web.UI.WebControls.TextBox TxtRemark;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				TxtID.Enabled=false;
				this.TxtRebate.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
			}
		}

		public Hashtable GetInfoFromPage()
		{
			Hashtable htbCompany=new Hashtable();
			htbCompany["insurCompanyID"]=TxtID.Text;
			htbCompany["insurCompanyName"]=TxtCompanyName.Text;
			htbCompany["rebate"]=TxtRebate.Text;
			htbCompany["remark"]=TxtRemark.Text;
			
			return htbCompany;
		}
		public void FillCompany(DataRow dr)
		{
			TxtID.Text=dr["insurCompanyID"].ToString();
			TxtCompanyName.Text=dr["insurCompanyName"].ToString();
			TxtRebate.Text=dr["rebate"].ToString();
			TxtRemark.Text=dr["remark"].ToString();

		}

		public void clear()
		{
		
			TxtID.Text="";
			TxtCompanyName.Text="";
			TxtRebate.Text="";
			TxtRemark.Text="";
		}
		public void queryData()
		{
			DataTable dt=InsurCompany.GetInsurCompanyInfo(null);
			DataGridHelper.bindData(dgdCompany,dt);
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
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.dgdCompany.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgdCompany_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			try
			{
				queryData();
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
		
			try
			{
				clear();
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				Hashtable htbCompany=GetInfoFromPage();
				bool bRet=false;
				if(TxtID.Text.Trim()==string.Empty)
				{
					bRet = InsurCompany.InsertCompany(htbCompany);
					if(bRet)
					{
						JavaScriptHelper.AlertMessage(this,"�������չ�˾�ɹ�!");
						queryData();
					}

				}
				else
				{
						bRet =InsurCompany.UpdateInsurCompany(htbCompany);
					if(bRet)
					{
						JavaScriptHelper.AlertMessage(this,"�޸ı��չ�˾�ɹ�!");
						CodeTableHelper.Refresh();
						queryData();
					}
				}
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}

		private void dgdCompany_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName=="Select")
				{
					string strCompanyID=e.Item.Cells[0].Text;
					DataTable dt=InsurCompany.GetInsurCompanyInfo(strCompanyID);
					FillCompany(dt.Rows[0]);
				}
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}
	}
}
