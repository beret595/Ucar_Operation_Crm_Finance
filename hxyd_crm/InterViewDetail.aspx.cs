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
	/// InterViewDetail 的摘要说明。
	/// </summary>
	public class InterViewDetail : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtCompany;
		protected System.Web.UI.WebControls.TextBox TxtPersonName;
		protected System.Web.UI.WebControls.DropDownList ddlBrand;
		protected System.Web.UI.WebControls.DropDownList ddlContactState;
		protected System.Web.UI.WebControls.DropDownList ddlGender;
		protected System.Web.UI.WebControls.TextBox TxtGender;
		protected System.Web.UI.WebControls.TextBox TxtModel;
		protected System.Web.UI.WebControls.DropDownList ddlFailedReason;
		protected System.Web.UI.WebControls.TextBox TxtPhone;
		protected System.Web.UI.WebControls.RegularExpressionValidator CheckTelphone;
		protected System.Web.UI.WebControls.TextBox TxtVIN;
		protected System.Web.UI.WebControls.TextBox TxtIntroducer;
		protected System.Web.UI.WebControls.TextBox TxtArea;
		protected System.Web.UI.WebControls.TextBox TxtLicensePlate;
		protected System.Web.UI.WebControls.DropDownList ddlCustomerType;
		protected System.Web.UI.WebControls.TextBox TxtAddress;
		protected System.Web.UI.WebControls.TextBox TxtInsuranceFees;
		protected System.Web.UI.WebControls.TextBox TxtReturnPoint;
		protected System.Web.UI.WebControls.DropDownList ddlInsuranceCompany;
		protected System.Web.UI.WebControls.TextBox TxtForceInsur;
		//protected System.Web.UI.WebControls.TextBox TxtViewTime;
		protected System.Web.UI.HtmlControls.HtmlInputText TxtSalesDate;
		protected System.Web.UI.HtmlControls.HtmlInputText txtSingleDate;
		protected System.Web.UI.HtmlControls.HtmlInputText txtViewDate;
		protected System.Web.UI.WebControls.DataGrid dgdSearchResult;
		protected System.Web.UI.WebControls.Button btnSave;
		
		protected System.Web.UI.WebControls.TextBox TxtEngineNo;
		protected System.Web.UI.WebControls.TextBox TxtTrafficPoint;
		protected System.Web.UI.WebControls.TextBox TxtTravelTax;
		protected System.Web.UI.WebControls.DropDownList ddlViewTime;
		protected System.Web.UI.WebControls.TextBox TxtIdCard;
		protected System.Web.UI.WebControls.TextBox TxtInsurancePoint;
		protected System.Web.UI.HtmlControls.HtmlInputText TxtBirthday;
		protected System.Web.UI.WebControls.DropDownList ddlServiceType;
		protected System.Web.UI.WebControls.DropDownList ddlCarType;
		protected System.Web.UI.WebControls.TextBox TxtCheSun;
		protected System.Web.UI.WebControls.TextBox TxtHuaHen;
		protected System.Web.UI.WebControls.TextBox TxtSanZhe;
		protected System.Web.UI.WebControls.TextBox TxtDaoCheJing;
		protected System.Web.UI.WebControls.TextBox TxtRenYuan;
		protected System.Web.UI.WebControls.TextBox TxtBoLi;
		protected System.Web.UI.WebControls.TextBox TxtDaoQiang;
		protected System.Web.UI.WebControls.TextBox TxtSheShui;
		protected System.Web.UI.WebControls.TextBox TxtBuJiMianPei;
		protected System.Web.UI.WebControls.TextBox TxtZiRan;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdPersonID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdInterViewID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdCarID;
		protected System.Web.UI.HtmlControls.HtmlInputText TxtExpireDate;
		
		protected System.Web.UI.HtmlControls.HtmlTextArea TxtComment;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			
			try
			{
					
				if(!this.IsPostBack)
				{
					initControl();
					this.TxtInsuranceFees.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtReturnPoint.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtForceInsur.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtTrafficPoint.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtTravelTax.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtInsurancePoint.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
//					if(this.Request.QueryString["interViewListId"]!=null && this.Request.QueryString["interViewListId"].ToString()!="")
//					{
//
//						Hashtable htbCon=new Hashtable();
//						htbCon.Add("interViewListId",this.Request.QueryString["interViewListId"].ToString());
//						DataTable dtInterView=Customer.GetCustomerInterview(htbCon);
//						FillCustomer(dtInterView.Rows[0]);
//					}
					if(this.Request.QueryString["interviewListId"]!=null && this.Request.QueryString["interviewListId"].ToString()!="")
					{
						Hashtable htbCon=new Hashtable();
						htbCon.Add("interviewListId",this.Request.QueryString["interviewListId"].ToString());
						
						DataTable dtInterView=Customer.GetCustomerInterview(htbCon);
						FillCustomer(dtInterView.Rows[0]);
						Hashtable htbConAll=new Hashtable();
						htbConAll.Add("carid",this.Request.QueryString["carid"].ToString());
						DataTable dtInterViewAll=Customer.GetCustomerInterview(htbConAll);
						DataGridHelper.bindData(this.dgdSearchResult,dtInterViewAll);
					
						
						
					}

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

			dt=CodeTableHelper.getCodeTable("brand");
			ddlBrand.DataSource=dt;
			ddlBrand.DataValueField ="id";
			ddlBrand.DataTextField="brandNameCN";
			ddlBrand.DataBind();
			li=new ListItem("请选择品牌","0");
			ddlBrand.Items.Insert(0,li);

			dt=CodeTableHelper.getCodeTable("failedReason");
			ddlFailedReason.DataSource=dt;
			ddlFailedReason.DataTextField ="display_value";
			ddlFailedReason.DataValueField="data_value";
			ddlFailedReason.DataBind();
			li=new ListItem( "请选择失败原因","0");
			ddlFailedReason.Items.Insert(0,li);

			dt=CodeTableHelper.getCodeTable("insurCompanyName");
			ddlInsuranceCompany.DataSource=dt;
			ddlInsuranceCompany.DataTextField ="insurCompanyName";
			ddlInsuranceCompany.DataValueField="InsurCompanyID";
			ddlInsuranceCompany.DataBind();
			li=new ListItem( "请选择保险公司","0");
			ddlInsuranceCompany.Items.Insert(0,li);


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

			dt=CodeTableHelper.getCodeTable("view_time");
			ddlViewTime.DataSource=dt;
			ddlViewTime.DataTextField ="display_value";
			ddlViewTime.DataValueField="data_value";
			ddlViewTime.DataBind();
			li=new ListItem( "请选择预约到店时间","0");
			ddlViewTime.Items.Insert(0,li);

			dt=CodeTableHelper.getCodeTable("service_type");
			ddlServiceType.DataSource=dt;
			ddlServiceType.DataTextField ="display_value";
			ddlServiceType.DataValueField="display_value";
			ddlServiceType.DataBind();
			li=new ListItem( "请选择服务类别","0");
			ddlServiceType.Items.Insert(0,li);

			dt=CodeTableHelper.getCodeTable("car_type");
			ddlCarType.DataSource=dt;
			ddlCarType.DataTextField ="display_value";
			ddlCarType.DataValueField="data_value";
			ddlCarType.DataBind();

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
		private void FillCustomer(DataRow drCustomer)
		{
			this.TxtIdCard.Text=drCustomer["idcard"].ToString();
			TxtBirthday.Value=drCustomer["birthday"].ToString();
			//ddlInsuranceCompany.SelectedItem.Text=drCustomer["insuranceCompany"].ToString();
			string strInsuranceCompany=drCustomer["insuranceCompany"].ToString();
			ddlInsuranceCompany.SelectedIndex=ddlInsuranceCompany.Items.IndexOf( ddlInsuranceCompany.Items.FindByText(strInsuranceCompany));
			txtCompany.Text=drCustomer["companyName"].ToString();
			TxtPersonName.Text=drCustomer["personName"].ToString();
			hdPersonID.Value=drCustomer["id"].ToString();
			hdInterViewID.Value=drCustomer["interviewListId"].ToString();
			hdCarID.Value=drCustomer["carId"].ToString();
			TxtGender.Text=drCustomer["gender"].ToString();
			ddlCarType.SelectedValue=drCustomer["car_type"].ToString();
			string strGender=drCustomer["gender"].ToString();
			ddlGender.SelectedIndex=ddlGender.Items.IndexOf(ddlGender.Items.FindByText(strGender));
			if(drCustomer["service_type"].ToString()=="")
			{
				ddlServiceType.SelectedIndex=0;
			}
			else
			{
				ddlServiceType.SelectedValue=drCustomer["service_type"].ToString();
			}
			TxtPhone.Text=drCustomer["phone"].ToString();
			TxtArea.Text=drCustomer["area"].ToString();
			TxtAddress.Text=drCustomer["address"].ToString();
			TxtModel.Text=drCustomer["model"].ToString();
			TxtLicensePlate.Text=drCustomer["licensePlate"].ToString();
			TxtVIN.Text=drCustomer["VIN"].ToString();
			TxtIntroducer.Text=drCustomer["introducer"].ToString();
			TxtSalesDate.Value=drCustomer["salesDate"].ToString();
			TxtExpireDate.Value=drCustomer["expire_date"].ToString();
			string strBrand=drCustomer["brand"].ToString();
			ddlBrand.SelectedIndex=ddlBrand.Items.IndexOf( ddlBrand.Items.FindByText(strBrand));
			string strFailedReason=drCustomer["failedReason"].ToString();
			ddlFailedReason.SelectedIndex=ddlFailedReason.Items.IndexOf( ddlFailedReason.Items.FindByText(strFailedReason));

			string strContactState=drCustomer["contactState"].ToString();
			ddlContactState.SelectedIndex=ddlContactState.Items.IndexOf( ddlContactState.Items.FindByText(strContactState));

			string strCustomerType=drCustomer["customerType"].ToString();
			ddlCustomerType.SelectedIndex=ddlCustomerType.Items.IndexOf(ddlCustomerType.Items.FindByText(strCustomerType));


			txtViewDate.Value=drCustomer["view_Date"].ToString();
		
			TxtComment.Value=drCustomer["comment"].ToString();
			

			TxtInsuranceFees.Text=drCustomer["insuranceFees"].ToString();
			if(drCustomer["returnPoint"].ToString().IndexOf(".")>0)
			{
				TxtReturnPoint.Text=drCustomer["returnPoint"].ToString().Substring(0,drCustomer["returnPoint"].ToString().IndexOf("."));
			}
			else
			{
				TxtReturnPoint.Text=drCustomer["returnPoint"].ToString();
			}
			TxtForceInsur.Text=drCustomer["forceInsur"].ToString();
			TxtTravelTax.Text=drCustomer["travelTax"].ToString();
			if(drCustomer["trafficPoint"].ToString().IndexOf(".")>0)
			{
				TxtTrafficPoint.Text=drCustomer["trafficPoint"].ToString().Substring(0,drCustomer["trafficPoint"].ToString().IndexOf("."));
			}
			else
			{
				TxtTrafficPoint.Text=drCustomer["trafficPoint"].ToString();
			}
			txtSingleDate.Value=drCustomer["single_date"].ToString();
			if(drCustomer["insurancePoint"].ToString().IndexOf(".")>0)
			{
				this.TxtInsurancePoint.Text=drCustomer["insurancePoint"].ToString().Substring(0,drCustomer["insurancePoint"].ToString().IndexOf("."));
			}
			else
			{
				this.TxtInsurancePoint.Text=drCustomer["insurancePoint"].ToString();
			}
			if(drCustomer["view_time"].ToString()=="")
			{
				ddlViewTime.SelectedIndex=0;
			}
			else
			{
				ddlViewTime.SelectedValue=drCustomer["view_time"].ToString();
			}
			TxtEngineNo.Text=drCustomer["engine_no"].ToString();

			TxtCheSun.Text=drCustomer["Fee_CheSun"].ToString();
			TxtHuaHen.Text=drCustomer["Fee_HuaHen"].ToString();
			TxtDaoCheJing.Text=drCustomer["Fee_DaoCheJing"].ToString();
			TxtSanZhe.Text=drCustomer["Fee_SanZhe"].ToString();
			TxtRenYuan.Text=drCustomer["Fee_RenYuan"].ToString();
			TxtBoLi.Text=drCustomer["Fee_BoLi"].ToString();
			TxtDaoQiang.Text=drCustomer["Fee_DaoQiang"].ToString();
			TxtSheShui.Text=drCustomer["Fee_SheShui"].ToString();
			TxtBuJiMianPei.Text=drCustomer["Fee_BuJiMianPei"].ToString();
			TxtZiRan.Text=drCustomer["Fee_ZiRan"].ToString();
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
			this.dgdSearchResult.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgdSearchResult_ItemCommand);
			this.btnSave.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void dgdSearchResult_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName=="Detail")
				{
					Hashtable htbCon=new Hashtable();
					string strInterViewListId=e.Item.Cells[12].Text;
					htbCon.Add("interviewListId",strInterViewListId);
					DataTable dtInterView=Customer.GetCustomerInterview(htbCon);
					FillCustomer(dtInterView.Rows[0]);
				}
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
		
			try
			{
				if(hdInterViewID.Value=="")
				{
					JavaScriptHelper.AlertMessage(this,"请先选择联系记录");
					return;
				}
				Hashtable hdbInterView=new Hashtable();


		
		
				hdbInterView.Add("companyName",txtCompany.Text);
				hdbInterView.Add("personName",TxtPersonName.Text);
				hdbInterView.Add("model",TxtModel.Text);
				hdbInterView.Add("phone",TxtPhone.Text);
				hdbInterView.Add("vin",TxtVIN.Text);
				hdbInterView.Add("introducer",TxtIntroducer.Text);
				hdbInterView.Add("area",TxtArea.Text);
				hdbInterView.Add("licensePlate",TxtLicensePlate.Text);
				hdbInterView.Add("salesDate",TxtSalesDate.Value);
				hdbInterView.Add("idcard",TxtIdCard.Text);
				hdbInterView.Add("engine_no",TxtEngineNo.Text);
				hdbInterView.Add("address",TxtAddress.Text);
				hdbInterView.Add("birthday",TxtBirthday.Value);

				if(ddlGender.SelectedValue!="0")
				{
					hdbInterView.Add("gender",ddlGender.SelectedItem.Text);
				}
				else
				{
					hdbInterView.Add("gender","");
				}

				if(ddlBrand.SelectedValue!="0")
				{
					hdbInterView.Add("brand",ddlBrand.SelectedItem.Text);
				}
				else
				{
					hdbInterView.Add("brand","");
				}
				hdbInterView.Add("car_type",ddlCarType.SelectedValue);
				hdbInterView.Add("forceInsur",TxtForceInsur.Text);
				hdbInterView.Add("view_date",txtViewDate.Value);
				if(ddlViewTime.SelectedValue=="0")
				{
					hdbInterView.Add("view_time","");
				}
				else
				{
					hdbInterView.Add("view_time",ddlViewTime.SelectedValue);

				}
				
				hdbInterView.Add("single_date",txtSingleDate.Value);
				hdbInterView.Add("expire_date",TxtExpireDate.Value);
				if(ddlContactState.SelectedValue!="0")
				{				
					hdbInterView.Add("contactState",ddlContactState.SelectedItem.Text);
				}
				else
				{
					hdbInterView.Add("contactState","");
				}
				if(ddlFailedReason.SelectedValue!="0")
				{
					hdbInterView.Add("failedReason",ddlFailedReason.SelectedItem.Text);
				}
				else
				{
					hdbInterView.Add("failedReason","");
				}
				
				hdbInterView.Add("travelTax",TxtTravelTax.Text);
				hdbInterView.Add("trafficPoint",TxtTrafficPoint.Text);
				if(ddlCustomerType.SelectedValue!="0")
				{
					hdbInterView.Add("customerType",ddlCustomerType.SelectedItem.Text);
				}
				else
				{
					hdbInterView.Add("customerType","");
				}
				
				hdbInterView.Add("insurancePoint",TxtInsurancePoint.Text.Trim());

				if(ddlServiceType.SelectedValue=="0")
				{
					hdbInterView.Add("service_type","");
					
				}
				else
				{
					hdbInterView.Add("service_type",ddlServiceType.SelectedValue);
				}
				
				hdbInterView.Add("comment",TxtComment.Value.Trim());
				hdbInterView.Add("insuranceFees",TxtInsuranceFees.Text);
				hdbInterView.Add("returnPoint",TxtReturnPoint.Text);
				if(ddlInsuranceCompany.SelectedValue!="0")
				{
					hdbInterView.Add("insuranceCompany",ddlInsuranceCompany.SelectedItem.Text);					
				}
				else		
				{
					hdbInterView.Add("insuranceCompany","");	
				}


				if(TxtInsuranceFees.Text.Trim()==string.Empty || TxtInsuranceFees.Text.Trim()=="0")
				{
					hdbInterView.Add("profit","0");
				}
				else
				{
					if(hdbInterView["insuranceCompany"].ToString()=="")
					{
						JavaScriptHelper.AlertMessage(this,"请选择保险公司");
						return;
					}
					if(hdbInterView["insurancePoint"].ToString()=="")
					{
						JavaScriptHelper.AlertMessage(this,"请填写保险折扣");
						return;
					}
					float fReturnPoint;
					if(hdbInterView["returnPoint"].ToString()=="")
					{
						fReturnPoint= 0;
					}
					else
					{
						fReturnPoint= int.Parse(hdbInterView["returnPoint"].ToString());
					}
					float fForceInsur;
					if(hdbInterView["forceInsur"].ToString()=="")
					{
						fForceInsur= 0;
					}
					else
					{
						fForceInsur= float.Parse(hdbInterView["forceInsur"].ToString());
					}
					
					float fFees=float.Parse(hdbInterView["insuranceFees"].ToString());
					float fInsurancePoint=int.Parse(hdbInterView["insurancePoint"].ToString());
					//处理利润
					DataTable dtTemp =CodeTableHelper.getCodeTable("insurCompanyName");
					string strRebate = dtTemp.Rows.Find(hdbInterView["insuranceCompany"].ToString())["Rebate"].ToString();
				
					//保险利润=(商业保费*保险折扣)-(商业保费*商业折扣)+(交强险*3%)
					hdbInterView["profit"]=Math.Round(fFees*fInsurancePoint/100-fFees*fReturnPoint/100+fForceInsur*0.03,2);
					hdbInterView["rebate"]=hdbInterView["insurancePoint"].ToString();
				}
				if(hdbInterView["rebate"]==null)
					hdbInterView["rebate"]="";
				
				if(hdbInterView["contactState"].ToString()=="续保成功" && ( hdbInterView["insuranceFees"].ToString()=="" || hdbInterView["insuranceFees"].ToString()=="0"))
				{
					JavaScriptHelper.AlertMessage(this,"续保成功，必须填写保费");
					return;
				}
				hdbInterView.Add("id",this.hdPersonID.Value);
				hdbInterView.Add("carid",this.hdCarID.Value);
				Customer.UpdateInterViewByID(hdInterViewID.Value,hdbInterView);
				Hashtable htbConAll=new Hashtable();
				htbConAll.Add("carid",this.hdCarID.Value);
				DataTable dtInterViewAll=Customer.GetCustomerInterview(htbConAll);
				DataGridHelper.bindData(this.dgdSearchResult,dtInterViewAll);
				JavaScriptHelper.AlertMessage(this,"保存成功!");

			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}

	
	}
}
