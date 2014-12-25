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

namespace  casey.hxyd_crm.Web.UI
{
	/// <summary>
	/// SearchCustomer ��ժҪ˵����
	/// </summary>
	public class SearchCustomer : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid dgdSearchResult;
		//protected Powerise.Hygeia.Web.UI.WebControls.Pager Pager1;
		protected System.Web.UI.WebControls.DropDownList ddlAgent;
		protected System.Web.UI.WebControls.TextBox TxtPhone;
		protected System.Web.UI.WebControls.DropDownList ddlContactState;
		protected System.Web.UI.WebControls.TextBox TxtLicense;
		protected System.Web.UI.WebControls.DropDownList ddlBrand;
		protected System.Web.UI.WebControls.TextBox TxtVIN;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Button btnExport;
		protected System.Web.UI.HtmlControls.HtmlInputText TxtEndTime;
		protected System.Web.UI.HtmlControls.HtmlInputText txtInterViewTime;
		protected System.Web.UI.WebControls.DropDownList ddl_date_type;
		protected System.Web.UI.WebControls.DropDownList ddlCustomerType;
		//protected System.Web.UI.HtmlControls.HtmlInputText TxtTime;
		private decimal nTotal=0;
		private decimal nTotal2=0;
		private decimal nTotal3=0;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			try
			{

				if(!CookieHelper.isLogin(this))
				{
					string strScript="window.parent.location='index.aspx';";
					JavaScriptHelper.RunScript(this,ScriptPos.Begin, strScript);
					return;
				}	

				if(!this.IsPostBack)
				{
					initControl();

					
				}

				string strRight=CookieHelper.getUserIndentity(this).UserInfo["role"].ToString();	
				
//				if(strRight=="admin")
//				{
//					btnExport.Visible=true;
//				}
//				else
//				{
//					btnExport.Visible=false;
//				}
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}
		private void initControl()
		{//ά�����
			DataTable dt=CodeTableHelper.getCodeTable("userinfo");
			ddlAgent.DataSource=dt;
			ddlAgent.DataTextField ="userName";
			ddlAgent.DataValueField="userid";
			ddlAgent.DataBind();
			ListItem li=new ListItem( "��ѡ�������","0");
			ddlAgent.Items.Insert(0,li);
			string strUserID=CookieHelper.getUserIndentity(this).UserInfo["userid"].ToString();
			string strRole=CookieHelper.getUserIndentity(this).UserInfo["Role"].ToString();
			ddlAgent.SelectedIndex=ddlAgent.Items.IndexOf( ddlAgent.Items.FindByValue(strUserID));
			if(strRole=="admin")
			{
				ddlAgent.Enabled=true;
				
				ListItem liDate=new ListItem("��������","3");
				ddl_date_type.Items.Insert(2,liDate);
			}
			else
			{
				ddlAgent.Enabled=false;
			}

			 dt=CodeTableHelper.getCodeTable("contact_state");
			ddlContactState.DataSource=dt;
			ddlContactState.DataValueField ="data_value";
			ddlContactState.DataTextField="display_value";
			ddlContactState.DataBind();
			 li=new ListItem("��ѡ����ϵ״̬","0");
			ddlContactState.Items.Insert(0,li);

			 dt=CodeTableHelper.getCodeTable("brand");
			 ddlBrand.DataSource=dt;
			ddlBrand.DataValueField ="id";
			ddlBrand.DataTextField="brandNameCN";
			ddlBrand.DataBind();
			 li=new ListItem("��ѡ��Ʒ��","0");
			ddlBrand.Items.Insert(0,li);

			dt=CodeTableHelper.getCodeTable("customerType");
			ddlCustomerType.DataSource=dt;
			ddlCustomerType.DataTextField ="display_value";
			ddlCustomerType.DataValueField="data_value";
			ddlCustomerType.DataBind();
			li=new ListItem( "��ѡ��ͻ���Դ","0");
			ddlCustomerType.Items.Insert(0,li);

		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
			//DataGridHelper.setStyle(dgdSearchResult);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.ddl_date_type.SelectedIndexChanged += new System.EventHandler(this.ddl_date_type_SelectedIndexChanged);
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.dgdSearchResult.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgdSearchResult_ItemCommand);
			this.dgdSearchResult.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgdSearchResult_ItemDataBound);
			this.dgdSearchResult.SelectedIndexChanged += new System.EventHandler(this.dgdSearchResult_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			//Pager1.CurrentPageIndex=1;
			QueryData();
			
		}
		private void QueryData()
		{
			try
			{
				DataTable dt=QueryResult();
				//DataGridHelper.bindData(dgdSearchResult,dt,Pager1);
				DataGridHelper.bindData(dgdSearchResult,dt);


			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
				
			}
		}

		private DataTable QueryResult()
		{
			//��������id,interviewTime agent_id,personName,phone,VIN,licensePlate,brand,contactState
			string  strInterviewTime=txtInterViewTime.Value;
		//	string  strTime=this.TxtTime.Value;
			string strEndTime=TxtEndTime.Value;
			string strPhone=TxtPhone.Text.Trim();
			string strLicense=TxtLicense.Text.Trim();
			string strVIN=TxtVIN.Text.Trim();
			string strAgentID=ddlAgent.SelectedValue;

			
			string strContactState=ddlContactState.SelectedValue;
			string strBrand=ddlBrand.SelectedItem.Text;
			string brand=ddlBrand.SelectedValue;

			Hashtable htbCondition=new Hashtable();
			htbCondition["view_date"]=strInterviewTime;
		//	htbCondition["interviewTime"]=strTime;
			htbCondition["end_time"]=strEndTime;
			htbCondition["date_type"]=this.ddl_date_type.SelectedValue;

			if(ddlCustomerType.SelectedValue!="0")
			{
				htbCondition["customerType"]=ddlCustomerType.SelectedItem.Text;
			}
			else
			{
				htbCondition["customerType"]=string.Empty;
			}
			htbCondition["phone"]=strPhone;
			htbCondition["VIN"]=strVIN;
			htbCondition["licensePlate"]=strLicense;
			if(brand!="0")
				htbCondition["brand"]=strBrand;
			if(strContactState!="0")
				htbCondition["contactState"]=ddlContactState.SelectedItem.Text;
			if(strAgentID!="0")
				htbCondition["agent_id"]=strAgentID;

			//DataTable dt= Customer.GetCustomerSaleInfo(htbCondition);
			//DataTable dt=Customer.GetCustomerInterview(htbCondition);
			DataTable dt=Customer.GetCustomerLastInfo(htbCondition,0);
			
			return dt;
		}
		public string Substr(string str)
		{
			if(str!=null && str.Length>10)
			{
				return str.Substring(0,10)+"..";
			}
			else
				return str;
		}

		private void dgdSearchResult_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType==ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem)
			{
				decimal  dFee1=0;
				decimal  dFee2=0;
				decimal  dFee3=0;
				try
				{
					if(e.Item.Cells[12].Text=="&nbsp;")
					{
						dFee1=0;
					}
					else
					{
						dFee1=decimal.Parse( e.Item.Cells[12].Text);
					}
					if(e.Item.Cells[14].Text=="&nbsp;")
					{
						dFee2=0;
					}
					else
					{
						dFee2=decimal.Parse( e.Item.Cells[14].Text);
					}
					if(e.Item.Cells[16].Text=="&nbsp;")
					{
						dFee3=0;
					}
					else
					{
						dFee3=decimal.Parse( e.Item.Cells[16].Text);
					}
					nTotal +=dFee1;
					nTotal2 +=dFee2;
					nTotal3 +=dFee3;

				}
				catch
				{
				}				
			
			}
			if(e.Item.ItemType==ListItemType.Footer)
			{
				
				e.Item.Cells[12].Text= nTotal.ToString();
				e.Item.Cells[14].Text= nTotal2.ToString();
				e.Item.Cells[16].Text= nTotal3.ToString();

			}
		}

//		private void Pager1_PageChanged(object src, Powerise.Hygeia.Web.UI.WebControls.PageChangedEventArgs e)
//		{
//			if(e.NewPageIndex!=0)
//			{
//				Pager1.CurrentPageIndex=e.NewPageIndex;
//
//				QueryData();
//			}
//		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			try
			{
				//DataTable dt= Customer.GetCustomerSaleInfo(htbCondition);
				DataTable dt=QueryResult();




				string strPath = HttpContext.Current.Server.MapPath("~")+"\\temp";
				string strFileName="�ͻ���Ϣ"+DateTime.Now.ToString("yyyyMMddhhmmss")+".xls";
				string strFullName=strPath+"\\"+strFileName;


				Hashtable htbExportColumn=new Hashtable();
				htbExportColumn["id"]="�ͻ����";
				htbExportColumn["personName"]="�ͻ�����";
				htbExportColumn["brand"]="Ʒ��";
				htbExportColumn["VIN"]="VIN";
				htbExportColumn["model"]="����";
				htbExportColumn["insuranceCompany"]="���չ�˾";
				htbExportColumn["insuranceFees"]="����";
				htbExportColumn["returnPoint"]="����";
				htbExportColumn["single_date"]="��������";
				htbExportColumn["remark"]="��ע";
				DataTable dtExport = FileHelper.ExportTransfer(htbExportColumn,dt);

				FileHelper.ExportXLSFile(dtExport,strFullName);
				FileDownHelper.DownFile(this.Context,strFullName,true);

			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}
		

		private void dgdSearchResult_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName=="Select")
				{
					//string strPersonID=e.Item.Cells[0].Text;
					//string strScript="window.parent.frames.setFrame('CallCustomer.aspx?personid="+strPersonID+"');";
					//string strScript="openDialog('CallCustomer.aspx?personid="+strPersonID+"',600,900);";
					string carid=e.Item.Cells[0].Text;
					string strScript="openDialog('CallCustomer.aspx?carid="+carid+"',600,900);";
					JavaScriptHelper.RunScript(this,ScriptPos.Begin,strScript);
				}
				if(e.CommandName=="Detail")
				{
					//string strId=e.Item.Cells[0].Text;
					//string strScript=" window.parent.frames.setFrame('InterViewDetail.aspx?id="+strId +"');";
					//string strScript="openDialog('InterViewDetail.aspx?id="+strId+"',600,900);";

					string carid=e.Item.Cells[0].Text;
					string interviewListId=e.Item.Cells[23].Text;
					string strScript="openDialog('InterViewDetail.aspx?interviewListId="+interviewListId+"&carid="+carid+"',600,900);";
					
					JavaScriptHelper.RunScript(this,ScriptPos.Begin,strScript);
				}
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}

		private void ddl_date_type_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(ddl_date_type.SelectedValue=="3")
			{
				dgdSearchResult.Columns[12].Visible=true;
				dgdSearchResult.Columns[13].Visible=true;
				dgdSearchResult.Columns[14].Visible=true;
				//dgdSearchResult.Columns[15].Visible=true;
				dgdSearchResult.Columns[16].Visible=true;
				dgdSearchResult.Columns[7].Visible=false;
				dgdSearchResult.Columns[8].Visible=false;
				dgdSearchResult.Columns[9].Visible=false;
				dgdSearchResult.Columns[10].Visible=false;
				dgdSearchResult.Columns[20].Visible=true;
				//dgdSearchResult.Columns[19].Visible=false;
			}
			else
			{
				dgdSearchResult.Columns[12].Visible=false;
				dgdSearchResult.Columns[13].Visible=false;
				dgdSearchResult.Columns[14].Visible=false;
				//dgdSearchResult.Columns[15].Visible=false;
				dgdSearchResult.Columns[16].Visible=false;
				dgdSearchResult.Columns[7].Visible=true;
				dgdSearchResult.Columns[8].Visible=true;
				dgdSearchResult.Columns[9].Visible=true;
				dgdSearchResult.Columns[10].Visible=true;
				dgdSearchResult.Columns[20].Visible=false;
				//dgdSearchResult.Columns[19].Visible=true;
			}
		}

		private void dgdSearchResult_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		


	}
}
