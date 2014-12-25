

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
	public class MileageManage : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.WebControls.TextBox TxtID;
	
		protected System.Web.UI.WebControls.DataGrid dgdCompany;
		protected System.Web.UI.WebControls.TextBox TxtBrandNameEN;
		protected System.Web.UI.WebControls.TextBox TxtBrandNameCN;
		protected System.Web.UI.WebControls.TextBox TxtMileage;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				TxtID.Enabled=false;
				TxtBrandNameEN.Enabled=false;
				TxtBrandNameCN.Enabled=false;

				this.TxtMileage.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
			}
		}

		public Hashtable GetInfoFromPage()
		{
			Hashtable htMileage=new Hashtable();
			htMileage["id"]=TxtID.Text;
			htMileage["brandNameEN"]=TxtBrandNameEN.Text;
			htMileage["brandNameCN"]=TxtBrandNameCN.Text;
			htMileage["mileage"]=TxtMileage.Text;
			
			return htMileage;
		}
		public void FillCompany(DataRow dr)
		{
			TxtID.Text=dr["id"].ToString();
			TxtBrandNameEN.Text=dr["brandNameEN"].ToString();
			TxtBrandNameCN.Text=dr["brandNameCN"].ToString();
			TxtMileage.Text=dr["mileage"].ToString();

		}

		public void clear()
		{
		
			TxtID.Text="";
			TxtBrandNameEN.Text="";
			TxtBrandNameCN.Text="";
			TxtMileage.Text="";
		}
		public void queryData()
		{
			DataTable dt=InsurCompany.GetMileageInfo(null);
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
				Hashtable htMileage=GetInfoFromPage();
				bool bRet=false;

				if(TxtID.Text.Trim()==string.Empty)
				{
					//�ݲ�����
					JavaScriptHelper.AlertMessage(this,"��ѡ����Ҫά����Ʒ��!");
					return;
//					bRet = InsurCompany.InsertCompany(htMileage);
//					if(bRet)
//					{
//						JavaScriptHelper.AlertMessage(this,"�������չ�˾�ɹ�!");
//						queryData();
//					}

				}
				else
				{
					if(TxtMileage.Text.Trim()==string.Empty)
					{
						JavaScriptHelper.AlertMessage(this,"����дƷ�����ά����Ϣ!");
						return;
					}
					bRet =InsurCompany.UpdateMileage(htMileage);// �޸�Ʒ�����
					if(bRet)
					{
						JavaScriptHelper.AlertMessage(this,"�޸�Ʒ����̳ɹ�!");
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
					string strID=e.Item.Cells[0].Text;
					DataTable dt=InsurCompany.GetMileageInfo(strID);
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

