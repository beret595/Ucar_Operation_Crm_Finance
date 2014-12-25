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
	/// ModifyCustomer 的摘要说明。
	/// </summary>
	public class ModifyCustomer : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid dgdSearchResult;
		protected Powerise.Hygeia.Web.UI.WebControls.Pager Pager1;
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.WebControls.TextBox txtCompany;
		protected System.Web.UI.WebControls.TextBox TxtPersonName;
		//protected System.Web.UI.WebControls.DropDownList ddlBrand;
		protected System.Web.UI.WebControls.DropDownList ddlContactState;
		protected System.Web.UI.WebControls.TextBox TxtGender;
		//protected System.Web.UI.WebControls.TextBox TxtModel;
		protected System.Web.UI.WebControls.DropDownList ddlFailedReason;
		protected System.Web.UI.WebControls.TextBox TxtPhone;
		//protected System.Web.UI.WebControls.TextBox TxtVIN;
		protected System.Web.UI.WebControls.TextBox TxtIntroducer;
		protected System.Web.UI.WebControls.TextBox TxtArea;
		//protected System.Web.UI.WebControls.TextBox TxtLicensePlate;
		protected System.Web.UI.WebControls.TextBox TxtAddress;
		//protected System.Web.UI.HtmlControls.HtmlInputText TxtExpireDate;
		protected System.Web.UI.WebControls.TextBox TxtName;
		//protected System.Web.UI.HtmlControls.HtmlInputText TxtSalesDate;
		protected System.Web.UI.WebControls.TextBox TxtTelephone;
		protected System.Web.UI.WebControls.Button btnClear;
		protected System.Web.UI.WebControls.TextBox TxtLicense;
		protected System.Web.UI.WebControls.TextBox TxtVinInfo;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdID;
		protected System.Web.UI.WebControls.RegularExpressionValidator CheckTelphone;
		protected System.Web.UI.WebControls.DropDownList ddlGender;
		protected System.Web.UI.WebControls.DropDownList ddlCustomerType;
		protected System.Web.UI.WebControls.TextBox TxtIdCard;
		protected System.Web.UI.HtmlControls.HtmlInputText TxtBirthday;
		protected System.Web.UI.WebControls.DropDownList ddlCustomerLevel;
		protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
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
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}

		private void initControl()
		{//维护码表


			DataTable dt=CodeTableHelper.getCodeTable("contact_state");
			ddlContactState.DataSource=dt;
			ddlContactState.DataValueField ="data_value";
			ddlContactState.DataTextField="display_value";
			ddlContactState.DataBind();
			ListItem li=new ListItem("请选择联系状态","0");
			ddlContactState.Items.Insert(0,li);

//			dt=CodeTableHelper.getCodeTable("brand");
//			ddlBrand.DataSource=dt;
//			ddlBrand.DataValueField ="id";
//			ddlBrand.DataTextField="brandNameCN";
//			ddlBrand.DataBind();
//			li=new ListItem("请选择品牌","0");
//			ddlBrand.Items.Insert(0,li);

			dt=CodeTableHelper.getCodeTable("failedReason");
			ddlFailedReason.DataSource=dt;
			ddlFailedReason.DataTextField ="display_value";
			ddlFailedReason.DataValueField="data_value";
			ddlFailedReason.DataBind();
			li=new ListItem( "请选择失败原因","0");
			ddlFailedReason.Items.Insert(0,li);


			dt=CodeTableHelper.getCodeTable("gender");
			ddlGender.DataSource=dt;
			ddlGender.DataTextField ="display_value";
			ddlGender.DataValueField="data_value";
			ddlGender.DataBind();
			li=new ListItem( "请选择性别","0");
			ddlGender.Items.Insert(0,li);

			dt=CodeTableHelper.getCodeTable("customerType");
			ddlCustomerType.DataSource=dt;
			ddlCustomerType.DataTextField ="display_value";
			ddlCustomerType.DataValueField="data_value";
			ddlCustomerType.DataBind();
			li=new ListItem( "请选择客户来源","0");
			ddlCustomerType.Items.Insert(0,li);


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
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.dgdSearchResult.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgdSearchResult_ItemCommand);
			this.Pager1.PageChanged += new Powerise.Hygeia.Web.UI.WebControls.PageChangedEventHandler(this.Pager1_PageChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
		
			if(TxtTelephone.Text.Trim()==""&&TxtLicense.Text.Trim()==""&&TxtVinInfo.Text.Trim()==""&&TxtName.Text.Trim()=="")
			{
				JavaScriptHelper.AlertMessage(this,"请先填写搜索条件");
				return;
			}
			try
			{
				Pager1.CurrentPageIndex=1;
				QueryData();
				
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}
		private void QueryData()
		{
			try
			{
				//参数名：id,interviewTime agent_id,personName,phone,VIN,licensePlate,brand,contactState
			 
				string strPhone=TxtTelephone.Text.Trim();
				string strLicense=TxtLicense.Text.Trim();
				string strVIN=TxtVinInfo.Text.Trim();
				string strName=TxtName.Text.Trim();

				Hashtable htbCondition=new Hashtable();
				htbCondition["phone"]=strPhone;
				htbCondition["VIN"]=strVIN;
				htbCondition["licensePlate"]=strLicense;
				htbCondition["personName"]=strName;
				
				DataTable dt=Customer.GetModifyCustomerInfo(htbCondition);
				DataGridHelper.bindData(dgdSearchResult,dt,Pager1);


			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
				
			}
		}
		private void FillCaseInfo(DataRow drCustomer)
		{

			txtCompany.Text=drCustomer["companyName"].ToString();
			TxtPersonName.Text=drCustomer["personName"].ToString();
			TxtGender.Text=drCustomer["gender"].ToString();
			string strGender=drCustomer["gender"].ToString();
			ddlGender.SelectedIndex=ddlGender.Items.IndexOf(ddlGender.Items.FindByText(strGender));
			TxtPhone.Text=drCustomer["phone"].ToString();
			TxtArea.Text=drCustomer["area"].ToString();
			TxtAddress.Text=drCustomer["address"].ToString();
			//TxtModel.Text=drCustomer["model"].ToString();
			//TxtLicensePlate.Text=drCustomer["licensePlate"].ToString();
			//TxtVIN.Text=drCustomer["VIN"].ToString();
			TxtIntroducer.Text=drCustomer["introducer"].ToString();
			this.TxtIdCard.Text=drCustomer["idcard"].ToString();
			TxtBirthday.Value=drCustomer["birthday"].ToString();
			//TxtExpireDate.Value=drCustomer["expire_date"].ToString();
			//string strBrand=drCustomer["brand"].ToString();
			//ddlBrand.SelectedIndex=ddlBrand.Items.IndexOf( ddlBrand.Items.FindByText(strBrand));
			string strFailedReason=drCustomer["failedReason"].ToString();
			ddlFailedReason.SelectedIndex=ddlFailedReason.Items.IndexOf( ddlFailedReason.Items.FindByText(strFailedReason));

			string strContactState=drCustomer["contactState"].ToString();
			ddlContactState.SelectedIndex=ddlContactState.Items.IndexOf( ddlContactState.Items.FindByText(strContactState));

			string strCustomerType=drCustomer["customerType"].ToString();
			ddlCustomerType.SelectedIndex=ddlCustomerType.Items.IndexOf(ddlCustomerType.Items.FindByText(strCustomerType));

			//TxtSalesDate.Value=drCustomer["salesDate"].ToString();
			hdID.Value=drCustomer["id"].ToString();
			this.ddlCustomerLevel.SelectedValue=drCustomer["customer_level"].ToString();
		
		}
		private void Clear()
		{
			this.TxtIdCard.Text="";
			TxtBirthday.Value="";
			txtCompany.Text="";
			TxtPersonName.Text="";
			TxtGender.Text="";
			ddlGender.SelectedIndex=0;
			TxtPhone.Text="";
			TxtArea.Text="";
			TxtAddress.Text="";
			//TxtModel.Text="";
			//TxtLicensePlate.Text="";
			//TxtVIN.Text="";
			TxtIntroducer.Text="";
			//TxtExpireDate.Value="";
			//ddlBrand.SelectedIndex=0;
			ddlFailedReason.SelectedIndex=0;
			ddlContactState.SelectedIndex=0;
			//TxtSalesDate.Value="";
			hdID.Value="";
			ddlCustomerType.SelectedIndex=0;
			ddlCustomerLevel.SelectedValue="";
		}
		private Hashtable getCustomerFromPage()
		{
			Hashtable htbCaseInfo=new Hashtable();
			htbCaseInfo["companyName"]=txtCompany.Text;
			htbCaseInfo["personName"]=TxtPersonName.Text;
			htbCaseInfo["id"]=hdID.Value;
			htbCaseInfo["idcard"]=this.TxtIdCard.Text;
			htbCaseInfo["birthday"]=TxtBirthday.Value;
			//htbCaseInfo["gender"]=TxtGender.Text.Trim();
			htbCaseInfo["phone"]=TxtPhone.Text.Trim();
			htbCaseInfo["area"]=TxtArea.Text.Trim();
			htbCaseInfo["address"]=TxtAddress.Text.Trim();
			//htbCaseInfo["model"]=TxtModel.Text.Trim();
			//htbCaseInfo["licensePlate"]=TxtLicensePlate.Text.Trim();
			//htbCaseInfo["VIN"]=TxtVIN.Text.Trim();
			htbCaseInfo["introducer"]=TxtIntroducer.Text.Trim();
			//htbCaseInfo["expire_date"]=TxtExpireDate.Value;
			//htbCaseInfo["salesDate"]=TxtSalesDate.Value;

			if(ddlGender.SelectedValue!="0")
			{
				htbCaseInfo["gender"]=ddlGender.SelectedItem.Text;
			}
			else
			{
				htbCaseInfo["gender"]=string.Empty;
			}
			if(ddlCustomerLevel.SelectedValue!="")
			{
				htbCaseInfo["customer_level"]=this.ddlCustomerLevel.SelectedValue;
			}
			else
			{
				htbCaseInfo["customer_level"]=string.Empty;
			}

//			if(ddlBrand.SelectedValue!="0")
//			{
//				htbCaseInfo["brand"]=ddlBrand.SelectedItem.Text;
//			}
//			else
//			{
//				htbCaseInfo["brand"]=string.Empty;
//			}
			if(ddlFailedReason.SelectedValue!="0")
			{
				htbCaseInfo["failedReason"]=ddlFailedReason.SelectedItem.Text;
			}
			else
			{
				htbCaseInfo["failedReason"]=string.Empty;
			}
			if(ddlContactState.SelectedValue!="0")
			{
				htbCaseInfo["contactState"]=ddlContactState.SelectedItem.Text;
			}
			else
			{
				htbCaseInfo["contactState"]=string.Empty;
			}

			if(ddlCustomerType.SelectedValue!="0")
			{
				htbCaseInfo["customerType"]=ddlCustomerType.SelectedItem.Text;
			}
			else
			{
				htbCaseInfo["customerType"]=string.Empty;
			}

			return htbCaseInfo;
		}
		private DataTable getCaseInfo(string strID)
		{
			return Customer.GetModifyCustomerInfo(strID);
			
		}

		private void Pager1_PageChanged(object src, Powerise.Hygeia.Web.UI.WebControls.PageChangedEventArgs e)
		{
			if(e.NewPageIndex!=0)
			{
				Pager1.CurrentPageIndex=e.NewPageIndex;

				QueryData();
			}
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			try
			{
				 Clear();
				
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
				Hashtable htbCustomer=getCustomerFromPage();
				if(hdID.Value==string.Empty)
				{
					bool bRet=Customer.InsertCustomer(htbCustomer);
					if(bRet)
					{
						Clear();
						JavaScriptHelper.AlertMessage(this,"新增客户成功");
					}
					
				}
				else
				{
					bool bRet=Customer.ModifyCustomer(htbCustomer);
					if(bRet)
					{
						Clear();
						JavaScriptHelper.AlertMessage(this,"修改客户信息成功");
					}
				}
				
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
					string strID=e.Item.Cells[0].Text.ToString();
					DataTable dt=getCaseInfo(strID);
					FillCaseInfo(dt.Rows[0]);
				}
//				if(e.CommandName=="Call")
//				{
//					string strPersonID=e.Item.Cells[0].Text;
//					string strScript="window.parent.frames.setFrame('CallCustomer.aspx?personid="+strPersonID+"');";
//					JavaScriptHelper.RunScript(this,ScriptPos.Begin,strScript);
//				}
				if(e.CommandName=="Detail")
				{
					string strPersonID=e.Item.Cells[0].Text;
					string strPersonName=e.Item.Cells[2].Text;
					string strScript="window.parent.frames.setFrame('ModifyCar.aspx?personid="+strPersonID+"&personName="+strPersonName+"');";
					//string strScript="openDialog('ModifyCar.aspx?personid="+strPersonID+"',600,900);";		
					JavaScriptHelper.RunScript(this,ScriptPos.Begin,strScript);

				
				}
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}
	}
}
