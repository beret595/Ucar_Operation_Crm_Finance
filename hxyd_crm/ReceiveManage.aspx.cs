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
	/// ReceiveManage 的摘要说明。
	/// </summary>
	public class ReceiveManage : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.DataGrid dgdCompany;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!Page.IsPostBack)
			{
				BinderData();
			}
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
			this.dgdCompany.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgdCompany_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		public void BinderData()
		{
            //获取当前登录的用户
			UserAssignHelper obj_userAssign = new UserAssignHelper();	
			String fullname = CookieHelper.getUserIndentity(this).UserInfo["fullName"].ToString();
			string person_name = "";
			if(TextBox2.Text!="")
			{
				person_name = TextBox2.Text;
			}
			int pag_num = 0;
			try
			{
				pag_num = Convert.ToInt32(TextBox1.Text.ToString().Trim());
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this.Page,"填写显示记录数格式不对");
			}

			DataTable dt = obj_userAssign.GetAssigner_ByName(person_name,fullname,pag_num,DropDownList1.SelectedValue);
			dgdCompany.DataSource = dt;
			dgdCompany.DataBind();
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			BinderData();
		}

		private void dgdCompany_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//点击接受的效验
			UserAssignHelper obj_userAssign = new UserAssignHelper();	
			if(e.CommandName =="Select")
			{
				//判断是否是未接受的状态
				String assign_role = e.Item.Cells[11].Text.ToString();
				if(assign_role == "已分配")
				{
					string kehu_no = e.Item.Cells[10].Text.ToString();
					string card_id = e.Item.Cells[9].Text.ToString();
					string assign_role_update = "已接受";
					Hashtable ht = new Hashtable();
					ht["kehu_no"] = kehu_no;
					ht["assign_role"] = assign_role_update;
					ht["card_id"] = card_id;
					if(obj_userAssign.UpdateUserAssigner_Role(ht))
					{
						JavaScriptHelper.AlertMessage(this.Page,"接受成功");
						this.BinderData();
					}
				}
				else
				{
					JavaScriptHelper.AlertMessage(this.Page,"该条业务状态不能接受");
					return;
				}
			}
			if(e.CommandName =="Compl")
			{
				//判断是否是已接受的状态
				String assign_role = e.Item.Cells[11].Text.ToString();
				String carid=e.Item.Cells[9].Text;
				if(assign_role == "已接受")
				{
					string kehu_no = e.Item.Cells[10].Text.ToString();
					string assign_role_update = "已完成";
					Hashtable ht = new Hashtable();
					ht["kehu_no"] = kehu_no;
					ht["assign_role"] = assign_role_update;
					if(obj_userAssign.UpdateUserAssigner_Role(ht))
					{						
						string strScript="openDialog('CallCustomer.aspx?carid="+carid+"',600,900);";
						JavaScriptHelper.RunScript(this,ScriptPos.Begin,strScript);
					}
				}
				else
				{
					JavaScriptHelper.AlertMessage(this.Page,"该条业务状态不能呼叫");
					return;
				}
			}
		}



	}
}
