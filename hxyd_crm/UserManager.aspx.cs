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

namespace hxyd_crm
{
	/// <summary>
	/// UserManager 的摘要说明。
	/// </summary>
	public class UserManager : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.WebControls.Button btnDel;
	
		protected System.Web.UI.WebControls.DataGrid dgdUserInfo;
		protected System.Web.UI.WebControls.TextBox TxtUserName;
		protected System.Web.UI.WebControls.TextBox TxtPassWord;
		protected System.Web.UI.WebControls.DropDownList ddlRole;
		protected System.Web.UI.WebControls.TextBox TxtPhone;
		protected System.Web.UI.WebControls.TextBox TxtCreateTime;
		protected System.Web.UI.WebControls.TextBox TxtLastUpdate;
		protected System.Web.UI.WebControls.TextBox TxtFullName;
		protected System.Web.UI.WebControls.TextBox TxtRemark;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdUserID;
		protected System.Web.UI.WebControls.TextBox TxtEmail;
		protected System.Web.UI.WebControls.Button btnQuery;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				// 在此处放置用户代码以初始化页面
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


			DataTable dt=CodeTableHelper.getCodeTable("role");
			ddlRole.DataSource=dt;
			ddlRole.DataValueField ="data_value";
			ddlRole.DataTextField="display_value";
			ddlRole.DataBind();
			ListItem li=new ListItem("请选择角色","0");
			ddlRole.Items.Insert(0,li);


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
			this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
			this.dgdUserInfo.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgdUserInfo_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		public Hashtable GetUserFromPage()
		{
			Hashtable htbUserInfo=new Hashtable();
			htbUserInfo["userid"]=hdUserID.Value;
			htbUserInfo["username"]=TxtUserName.Text.Trim();
			htbUserInfo["userPassword"]=TxtPassWord.Text.Trim();
			htbUserInfo["remark"]=TxtRemark.Text.Trim();
			htbUserInfo["fullname"]=TxtFullName.Text.Trim();
			htbUserInfo["phone"]=TxtPhone.Text.Trim();
			htbUserInfo["email"]=TxtEmail.Text.Trim();
			string strRole=ddlRole.SelectedValue;
			if(strRole!="0")
				htbUserInfo["role"]=ddlRole.SelectedItem.Text;
			else
				htbUserInfo["role"]="";
			return htbUserInfo;
		}
		public void FillUser(DataRow dr)
		{
			TxtUserName.Text=dr["username"].ToString();
			TxtPassWord.Text=CryptoHelper.CommonDecrypt( dr["userPassword"].ToString());
			string strRole=dr["role"].ToString().Trim().ToLower();
			ddlRole.SelectedIndex=ddlRole.Items.IndexOf(ddlRole.Items.FindByText(strRole));
			TxtRemark.Text=dr["remark"].ToString();
			TxtFullName.Text=dr["fullname"].ToString();
			TxtPhone.Text=dr["phone"].ToString();
			hdUserID.Value=dr["userid"].ToString();
			TxtEmail.Text=dr["email"].ToString();
			TxtCreateTime.Text=dr["createTime"].ToString();
			TxtLastUpdate.Text=dr["lastUpdate"].ToString();
		}
		public DataTable QueryUserInfo()
		{
			return null;
		}
		public DataTable GetUserInfoByUserID(string strUserID)
		{
			return null;
		}
		public void Clear()
		{
			try
			{

				TxtUserName.Text="";
				TxtPassWord.Text="";
				 
				ddlRole.SelectedIndex=0;
				TxtRemark.Text="";
				TxtFullName.Text="";
				TxtPhone.Text="";
				hdUserID.Value="";
				TxtCreateTime.Text="";
				TxtLastUpdate.Text="";
				this.TxtEmail.Text="";
				

			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			bindData();
			
		}
		private void bindData()
		{
			try
			{
				
				DataTable dtUser =UserHelper.GetUserInfo(TxtUserName.Text.Trim());
				DataGridHelper.bindData(dgdUserInfo,dtUser);
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}

		private void dgdUserInfo_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				
				if(e.CommandName=="Select")
				{
					DataTable dtUser=UserHelper.GetUserInfoByUserID(e.Item.Cells[0].Text);
					FillUser(dtUser.Rows[0]);
				}
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
			
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			
			Clear();
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				Hashtable htbUser=GetUserFromPage();
				if(htbUser["userPassword"]==null ||htbUser["userPassword"].ToString().Trim().Length<5)
				{
					JavaScriptHelper.AlertMessage(this,"密码不能小于5个字符");
					return ;
				}
				if(htbUser["role"]==null || htbUser["role"].ToString().Trim()==string.Empty)
				{
					JavaScriptHelper.AlertMessage(this,"请选择角色");
					return ;
				}
				htbUser["userPassword"]=CryptoHelper.CommonEncrypt(htbUser["userPassword"].ToString().Trim());
				if(hdUserID.Value==string.Empty)
				{
					bool bRet = UserHelper.InsertUser(htbUser);
					if(bRet)
					{
						bindData();
						JavaScriptHelper.AlertMessage(this,"新增用户信息成功");
					}
				}
				else
				{
					bool bRet = UserHelper.UpdateUser(htbUser);
					if(bRet)
					{
						bindData();
						JavaScriptHelper.AlertMessage(this,"修改用户信息成功");
					}
				}
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}

		private void btnDel_Click(object sender, System.EventArgs e)
		{
		
			try
			{
			
				if(hdUserID.Value==string.Empty)
				{
					JavaScriptHelper.AlertMessage(this,"请选择用户!");
					return;
				}
				else
				{					
					UserHelper.DeleteUser(hdUserID.Value);
				}
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}
	}
}
