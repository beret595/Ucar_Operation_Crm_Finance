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
	/// ViwCustomer ��ժҪ˵����
	/// </summary>
	public class ViwCustomer : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtCompany;
		protected System.Web.UI.WebControls.TextBox TxtPersonName;
		protected System.Web.UI.WebControls.DropDownList ddlGender;
		protected System.Web.UI.WebControls.TextBox TxtGender;
		protected System.Web.UI.WebControls.TextBox TxtPhone;
		protected System.Web.UI.WebControls.RegularExpressionValidator CheckTelphone;
		protected System.Web.UI.WebControls.DropDownList ddlCustomerType;
		protected System.Web.UI.WebControls.DropDownList ddlContactState;
		protected System.Web.UI.WebControls.TextBox TxtArea;
		protected System.Web.UI.WebControls.TextBox TxtIdCard;
		protected System.Web.UI.WebControls.TextBox TxtIntroducer;
		protected System.Web.UI.WebControls.DropDownList ddlFailedReason;
		protected System.Web.UI.WebControls.TextBox TxtAddress;
		protected System.Web.UI.WebControls.DropDownList ddlCustomerLevel;
		protected System.Web.UI.WebControls.DropDownList ddlServiceType;
		protected System.Web.UI.WebControls.DropDownList ddlBrand;
		protected System.Web.UI.WebControls.TextBox TxtLicensePlate;
		protected System.Web.UI.WebControls.TextBox TxtCurrentMileage;
		protected System.Web.UI.WebControls.TextBox TxtModel;
		protected System.Web.UI.WebControls.DropDownList ddlShapeColors;
		protected System.Web.UI.WebControls.TextBox TxtAverageMileage;
		protected System.Web.UI.WebControls.TextBox TxtVIN;
		protected System.Web.UI.WebControls.TextBox TxtEngineNo;
		protected System.Web.UI.WebControls.TextBox TxtCheSun;
		protected System.Web.UI.WebControls.TextBox TxtHuaHen;
		protected System.Web.UI.WebControls.TextBox TxtInsuranceFees;
		protected System.Web.UI.WebControls.DropDownList ddlInsuranceCompany;
		protected System.Web.UI.WebControls.TextBox TxtSanZhe;
		protected System.Web.UI.WebControls.TextBox TxtDaoCheJing;
		protected System.Web.UI.WebControls.TextBox TxtForceInsur;
		protected System.Web.UI.WebControls.TextBox TxtReturnPoint;
		protected System.Web.UI.WebControls.TextBox TxtRenYuan;
		protected System.Web.UI.WebControls.TextBox TxtBoLi;
		protected System.Web.UI.WebControls.TextBox TxtTravelTax;
		protected System.Web.UI.WebControls.TextBox TxtTrafficPoint;
		protected System.Web.UI.WebControls.TextBox TxtDaoQiang;
		protected System.Web.UI.WebControls.TextBox TxtSheShui;
		protected System.Web.UI.WebControls.TextBox TxtInsurancePoint;
		protected System.Web.UI.WebControls.TextBox TxtBuJiMianPei;
		protected System.Web.UI.WebControls.TextBox TxtZiRan;
		protected System.Web.UI.WebControls.TextBox TxtAgent;
		protected System.Web.UI.WebControls.TextBox TxtInterviewTime;
		protected System.Web.UI.WebControls.DropDownList ddlViewTime;
		protected System.Web.UI.WebControls.DataGrid dgdSearchResult;
		protected System.Web.UI.HtmlControls.HtmlInputText TxtBirthday;
		protected System.Web.UI.HtmlControls.HtmlInputText TxtSalesDate;
		protected System.Web.UI.HtmlControls.HtmlInputText TxtExpireDate;
		protected System.Web.UI.HtmlControls.HtmlInputText txtSingleDate;
		protected System.Web.UI.HtmlControls.HtmlInputText txtViewDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdPersonID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdCarID;
		protected System.Web.UI.HtmlControls.HtmlTextArea TxtComment;
		protected System.Web.UI.HtmlControls.HtmlTextArea TxtCommentHis;
	
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
					this.TxtInsuranceFees.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtReturnPoint.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtForceInsur.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtTrafficPoint.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtTravelTax.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtInsurancePoint.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";

					this.TxtCheSun.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtHuaHen.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtDaoCheJing.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtSanZhe.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtRenYuan.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtBoLi.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtDaoQiang.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtSheShui.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtBuJiMianPei.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";
					this.TxtZiRan.Attributes["onkeyup"]=@"value=value.replace(/[^\d\.]/g,'')";

					//					if(this.Request.QueryString["personid"]!=null && this.Request.QueryString["personid"].ToString()!="")
					//					{
					//						DataTable dtCustomer=Customer.GetCustomerInfo(this.Request.QueryString["personid"].ToString());
					//						FillCustomer(dtCustomer.Rows[0]);
					//					}

					if(this.Request.QueryString["carid"]!=null && this.Request.QueryString["carid"].ToString()!="")
					{

						DataTable dtCustomer=Customer.GetCustomerInfo(this.Request.QueryString["carid"].ToString());
						FillCustomer(dtCustomer.Rows[0]);
						Hashtable htbConAll=new Hashtable();
						htbConAll.Add("carid",this.Request.QueryString["carid"].ToString());
						DataTable dtInterViewAll=Customer.GetCustomerInterview(htbConAll);
						DataGridHelper.bindData(this.dgdSearchResult,dtInterViewAll);
					}

					//					if(this.Request.QueryString["interviewListId"]!=null && this.Request.QueryString["interviewListId"].ToString()!="")
					//					{
					//						Hashtable htbCondition =new Hashtable();
					//						htbCondition["interviewListId"]=this.Request.QueryString["interviewListId"].ToString();
					//						DataTable dtCustomer=Customer.GetCustomerInfo(htbCondition);
					//						FillCustomer(dtCustomer.Rows[0]);
					//					}

				}
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void FillCustomer(DataRow drCustomer)
		{
			txtCompany.Text=drCustomer["companyName"].ToString();
			TxtPersonName.Text=drCustomer["personName"].ToString();
			hdPersonID.Value=drCustomer["id"].ToString();
			hdCarID.Value=drCustomer["carId"].ToString();
			TxtGender.Text=drCustomer["gender"].ToString();
			this.TxtIdCard.Text=drCustomer["idcard"].ToString();
			//	TxtBirthday.Value=drCustomer["birthday"].ToString();
			string strGender=drCustomer["gender"].ToString();
			ddlGender.SelectedIndex=ddlGender.Items.IndexOf(ddlGender.Items.FindByText(strGender));

			TxtPhone.Text=drCustomer["phone"].ToString();
			TxtArea.Text=drCustomer["area"].ToString();
			TxtAddress.Text=drCustomer["address"].ToString();
			TxtModel.Text=drCustomer["model"].ToString();
			TxtLicensePlate.Text=drCustomer["licensePlate"].ToString();
			TxtVIN.Text=drCustomer["VIN"].ToString();
			TxtEngineNo.Text=drCustomer["engine_no"].ToString();
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

			TxtAgent.Text=CookieHelper.getUserIndentity(this).UserInfo["username"].ToString();
			TxtInterviewTime.Text=DateTime.Now.ToString("yyyy/MM/dd HH:mm");
			TxtCommentHis.Value=drCustomer["comment"].ToString();
			this.TxtBirthday.Value=drCustomer["birthday"].ToString();
			this.ddlCustomerLevel.SelectedValue=drCustomer["customer_level"].ToString();
			
			TxtCurrentMileage.Text=drCustomer["current_mileage_new"].ToString();

			TxtAverageMileage.Text=drCustomer["average_mileage"].ToString();
			//TxtShapeColors.Text=drCustomer["shape_colors"].ToString();
			this.ddlShapeColors.SelectedValue=drCustomer["shape_colors"].ToString();


			//��ձ���¼����Ϣ
			TxtInsuranceFees.Text="";
			TxtReturnPoint.Text="";
			TxtForceInsur.Text="";
			TxtTravelTax.Text="";
			TxtTrafficPoint.Text="";
			txtSingleDate.Value="";
			txtViewDate.Value="";
			this.TxtInsurancePoint.Text="";
			//TxtViewTime.Text="";
			TxtComment.Value="";
			ddlInsuranceCompany.SelectedIndex=0;
			ddlViewTime.SelectedIndex=0;
			ddlServiceType.SelectedIndex=0;

			TxtCheSun.Text="";
			TxtHuaHen.Text="";
			TxtDaoCheJing.Text="";
			TxtSanZhe.Text="";
			TxtRenYuan.Text="";
			TxtBoLi.Text="";
			TxtDaoQiang.Text="";
			TxtSheShui.Text="";
			TxtBuJiMianPei.Text="";
			TxtZiRan.Text="";
		
		
		}

		private void initControl()
		{//ά�����


			DataTable dt=CodeTableHelper.getCodeTable("contact_state");
			ddlContactState.DataSource=dt;
			ddlContactState.DataValueField ="data_value";
			ddlContactState.DataTextField="display_value";
			ddlContactState.DataBind();
			ListItem li=new ListItem("��ѡ����ϵ״̬","0");
			ddlContactState.Items.Insert(0,li);

			dt=CodeTableHelper.getCodeTable("brand");
			ddlBrand.DataSource=dt;
			ddlBrand.DataValueField ="id";
			ddlBrand.DataTextField="brandNameCN";
			ddlBrand.DataBind();
			li=new ListItem("��ѡ��Ʒ��","0");
			ddlBrand.Items.Insert(0,li);

			dt=CodeTableHelper.getCodeTable("failedReason");
			ddlFailedReason.DataSource=dt;
			ddlFailedReason.DataTextField ="display_value";
			ddlFailedReason.DataValueField="data_value";
			ddlFailedReason.DataBind();
			li=new ListItem( "��ѡ��ʧ��ԭ��","0");
			ddlFailedReason.Items.Insert(0,li);

			dt=CodeTableHelper.getCodeTable("insurCompanyName");
			ddlInsuranceCompany.DataSource=dt;
			ddlInsuranceCompany.DataTextField ="insurCompanyName";
			ddlInsuranceCompany.DataValueField="InsurCompanyID";
			ddlInsuranceCompany.DataBind();
			li=new ListItem( "��ѡ���չ�˾","0");
			ddlInsuranceCompany.Items.Insert(0,li);


			dt=CodeTableHelper.getCodeTable("gender");
			ddlGender.DataSource=dt;
			ddlGender.DataTextField ="display_value";
			ddlGender.DataValueField="data_value";
			ddlGender.DataBind();
			li=new ListItem( "��ѡ���Ա�","0");
			ddlGender.Items.Insert(0,li);

			dt=CodeTableHelper.getCodeTable("customerType");
			ddlCustomerType.DataSource=dt;
			ddlCustomerType.DataTextField ="display_value";
			ddlCustomerType.DataValueField="data_value";
			ddlCustomerType.DataBind();
			li=new ListItem( "��ѡ��ͻ���Դ","0");
			ddlCustomerType.Items.Insert(0,li);

			dt=CodeTableHelper.getCodeTable("view_time");
			ddlViewTime.DataSource=dt;
			ddlViewTime.DataTextField ="display_value";
			ddlViewTime.DataValueField="data_value";
			ddlViewTime.DataBind();
			li=new ListItem( "��ѡ��ԤԼ����ʱ��","0");
			ddlViewTime.Items.Insert(0,li);


			dt=CodeTableHelper.getCodeTable("service_type");
			ddlServiceType.DataSource=dt;
			ddlServiceType.DataTextField ="display_value";
			ddlServiceType.DataValueField="display_value";
			ddlServiceType.DataBind();
			li=new ListItem( "��ѡ��������","0");
			ddlServiceType.Items.Insert(0,li);
			

		}


	}
}
