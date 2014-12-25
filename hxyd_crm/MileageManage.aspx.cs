

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
	/// InsurCompanyManager 的摘要说明。
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
			// 在此处放置用户代码以初始化页面
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
					//暂不新增
					JavaScriptHelper.AlertMessage(this,"请选择需要维护的品牌!");
					return;
//					bRet = InsurCompany.InsertCompany(htMileage);
//					if(bRet)
//					{
//						JavaScriptHelper.AlertMessage(this,"新增保险公司成功!");
//						queryData();
//					}

				}
				else
				{
					if(TxtMileage.Text.Trim()==string.Empty)
					{
						JavaScriptHelper.AlertMessage(this,"请填写品牌里程维护信息!");
						return;
					}
					bRet =InsurCompany.UpdateMileage(htMileage);// 修改品牌里程
					if(bRet)
					{
						JavaScriptHelper.AlertMessage(this,"修改品牌里程成功!");
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

