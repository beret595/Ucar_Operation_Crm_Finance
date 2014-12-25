
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

namespace hxyd_crm
{
	/// <summary>
	/// ModifyCustomer 的摘要说明。
	/// </summary>
	public class ModifyCar : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid dgdSearchResult;
		protected System.Web.UI.WebControls.Button btnSave;
		
		protected System.Web.UI.WebControls.TextBox TxtPersonName;
		protected System.Web.UI.WebControls.DropDownList ddlBrand;
		
		protected System.Web.UI.WebControls.TextBox TxtModel;
		
		protected System.Web.UI.WebControls.TextBox TxtVIN;
	
		
		protected System.Web.UI.WebControls.TextBox TxtLicensePlate;
		
		protected System.Web.UI.HtmlControls.HtmlInputText TxtExpireDate;
		protected System.Web.UI.HtmlControls.HtmlInputText TxtSalesDate;
		
		protected System.Web.UI.WebControls.Button btnClear;
		
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdID;
		protected System.Web.UI.WebControls.RegularExpressionValidator CheckTelphone;
		
	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdPersonId;
		protected System.Web.UI.WebControls.TextBox TxtEngineNo;
		protected System.Web.UI.WebControls.TextBox TxtCurrentMileage;
		protected System.Web.UI.WebControls.TextBox TxtAverageMileage;
		protected System.Web.UI.WebControls.Button btnClose;
		protected System.Web.UI.WebControls.DropDownList ddlCarType;
		protected System.Web.UI.HtmlControls.HtmlInputText TxtKeepDate;
		protected System.Web.UI.WebControls.DropDownList ddlShapeColors;
		//protected System.Web.UI.WebControls.TextBox TxtShapeColors;
		string strPersonId;
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			try
			{
				
				if(!this.IsPostBack)
				{
					
					initControl();
					this.TxtCurrentMileage.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					if(this.Request.QueryString["personid"]!=null && this.Request.QueryString["personid"].ToString()!="")
					{
						
						strPersonId=this.Request.QueryString["personid"].ToString();
						this.hdPersonId.Value=strPersonId;
						Hashtable htbCon=new Hashtable();
						htbCon.Add("personid",strPersonId);
						DataTable dtCar=Customer.GetCarInfo(htbCon);
						DataGridHelper.bindData(this.dgdSearchResult,dtCar);
						this.TxtPersonName.Text=this.Request.QueryString["personName"].ToString();

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


		

			DataTable dt=CodeTableHelper.getCodeTable("brand");
			ddlBrand.DataSource=dt;
			ddlBrand.DataValueField ="id";
			ddlBrand.DataTextField="brandNameCN";
			ddlBrand.DataBind();
			ListItem li=new ListItem("请选择品牌","0");
			ddlBrand.Items.Insert(0,li);

			dt=CodeTableHelper.getCodeTable("car_type");
			ddlCarType.DataSource=dt;
			ddlCarType.DataTextField ="display_value";
			ddlCarType.DataValueField="data_value";
			ddlCarType.DataBind();
			ddlCarType.SelectedValue="1";
			


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
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			this.dgdSearchResult.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgdSearchResult_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		
		private void FillCaseInfo(DataRow drCustomer)
		{

			
			TxtPersonName.Text=drCustomer["personName"].ToString();	
			TxtModel.Text=drCustomer["model"].ToString();
			TxtLicensePlate.Text=drCustomer["licensePlate"].ToString();
			TxtVIN.Text=drCustomer["VIN"].ToString();
		
			TxtExpireDate.Value=drCustomer["expire_date"].ToString();
			string strBrand=drCustomer["brand"].ToString();
			ddlBrand.SelectedIndex=ddlBrand.Items.IndexOf( ddlBrand.Items.FindByText(strBrand));
			ddlCarType.SelectedValue=drCustomer["car_type"].ToString();
			TxtSalesDate.Value=drCustomer["salesDate"].ToString();
			TxtEngineNo.Text=drCustomer["engine_no"].ToString();
			//TxtShapeColors.Text=drCustomer["shape_colors"].ToString();
			ddlShapeColors.SelectedValue=drCustomer["shape_colors"].ToString();
			TxtCurrentMileage.Text=drCustomer["current_mileage"].ToString();
			TxtAverageMileage.Text=drCustomer["average_mileage"].ToString();
			TxtKeepDate.Value=drCustomer["keep_date"].ToString();
			hdID.Value=drCustomer["carId"].ToString();
		}
		private void Clear()
		{
			
			TxtModel.Text="";
			TxtLicensePlate.Text="";
			TxtVIN.Text="";
		
			TxtExpireDate.Value="";
			ddlBrand.SelectedIndex=0;
		
			TxtSalesDate.Value="";
			hdID.Value="";
			TxtEngineNo.Text="";
			//TxtShapeColors.Text="";
			ddlShapeColors.SelectedValue="";
			TxtCurrentMileage.Text="";
			TxtAverageMileage.Text="";
			ddlCarType.SelectedValue="1";
			this.TxtKeepDate.Value="";
			
		}
		private Hashtable getCarFromPage()
		{
			Hashtable htbCaseInfo=new Hashtable();
			
			
			
			htbCaseInfo["id"]=hdID.Value;
			htbCaseInfo["personId"]=this.hdPersonId.Value;
		
			
			
			
			htbCaseInfo["car_model"]=TxtModel.Text.Trim();
			htbCaseInfo["licensePlate"]=TxtLicensePlate.Text.Trim();
			htbCaseInfo["VIN"]=TxtVIN.Text.Trim();
		
			htbCaseInfo["expire_date"]=TxtExpireDate.Value;
			htbCaseInfo["salesDate"]=TxtSalesDate.Value;

			htbCaseInfo["engine_no"]=TxtEngineNo.Text;
			//htbCaseInfo["shape_colors"]=TxtShapeColors.Text;
			if(ddlShapeColors.SelectedValue!="")
			{
				htbCaseInfo["shape_colors"]=ddlShapeColors.SelectedValue;
			}
			else
			{
				htbCaseInfo["shape_colors"]=string.Empty;
			}
			
			htbCaseInfo["current_mileage"]=TxtCurrentMileage.Text;

			htbCaseInfo["average_mileage"]=TxtAverageMileage.Text;
			
			htbCaseInfo["keep_date"]=TxtKeepDate.Value;


			

			if(ddlBrand.SelectedValue!="0")
			{
				htbCaseInfo["manufacturers"]=ddlBrand.SelectedItem.Text;
			}
			else
			{
				htbCaseInfo["manufacturers"]=string.Empty;
			}
			htbCaseInfo["car_type"]=ddlCarType.SelectedValue;
			
			

			

			return htbCaseInfo;
		}
		private DataTable getCaseInfo(string strID)
		{
			return Customer.GetCustomerInfo(strID);
			
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
				if(TxtCurrentMileage.Text!=""&&TxtKeepDate.Value=="")
				{
					JavaScriptHelper.AlertMessage(this,"请选择保养时间");
					return;
				}
				Hashtable htbCar=getCarFromPage();
				if(hdID.Value==string.Empty)
				{
					bool bRet=Customer.InsertCar(htbCar);
					if(bRet)
					{
						Clear();
						JavaScriptHelper.AlertMessage(this,"新增车辆成功");
					}
					
				}
				else
				{
					bool bRet=Customer.ModifyCar(htbCar);
					if(bRet)
					{
						Clear();
						JavaScriptHelper.AlertMessage(this,"修改车辆信息成功");
					}
				}
				Hashtable htbCon=new Hashtable();
				htbCon.Add("personid",this.hdPersonId.Value);
				DataTable dtCar=Customer.GetCarInfo(htbCon);
				DataGridHelper.bindData(this.dgdSearchResult,dtCar);
				
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
				if(e.CommandName=="Update")
				{
					string strID=e.Item.Cells[0].Text.ToString();
					DataTable dt=getCaseInfo(strID);
					FillCaseInfo(dt.Rows[0]);
				}
				if(e.CommandName=="Call")
				{
					string strCarID=e.Item.Cells[0].Text;
					string strScript="window.parent.frames.setFrame('CallCustomer.aspx?carid="+strCarID+"');";
					JavaScriptHelper.RunScript(this,ScriptPos.Begin,strScript);
				}
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			string strScript="window.parent.frames.setFrame('ModifyCustomer.aspx');";
			JavaScriptHelper.RunScript(this,ScriptPos.Begin,strScript);
		}

	}
}

