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
	/// TaskManage ��ժҪ˵����
	/// </summary>
	public class TaskManage : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.DropDownList DropDownList2;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label lb_text;
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected System.Web.UI.WebControls.DataGrid dgdCompany;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!Page.IsPostBack)
			{
				binderData();
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
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.dgdCompany.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgdCompany_ItemCommand);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		public void binderData()
		{
			UserAssignHelper obj_userAssign = new UserAssignHelper();		
			DataTable dt = obj_userAssign.getDataUeser();
			DropDownList2.DataSource = dt;
			DropDownList2.DataBind();
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			BinderBaoYang();
		}

		#region ��ѯ������Ϣ
		public void BinderBaoYang()
		{	
			UserAssignHelper obj_userAssign = new UserAssignHelper();		
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
				JavaScriptHelper.AlertMessage(this.Page,"��д��ʾ��¼����ʽ����");
			}
			DataTable dt = obj_userAssign.GetAssignCar(DropDownList1.SelectedValue,pag_num,person_name);
			dgdCompany.DataSource = dt;
			dgdCompany.DataBind();
		}
		#endregion

		public void SetVisble()
		{
			lb_text.Visible=false;
			DropDownList2.Visible=false;
			Button1.Visible = false;
		}

		private void dgdCompany_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
//			if(e.CommandName=="Select")
//			{
//				if( e.Item.Cells[11].Text.ToString().Trim()=="�ѽ���"|| e.Item.Cells[11].Text.ToString().Trim()=="�����")
//				{
//					JavaScriptHelper.AlertMessage(this.Page,"�ѽ��ܻ�����ɵ������޷��ٴη���");
//					return;
//				}
//				lb_text.Visible=true;
//				DropDownList2.Visible=true;
//				Button1.Visible = true;
//				ViewState["person_id"] = e.Item.Cells[8].Text.ToString().Trim();
//				ViewState["car_id"] = e.Item.Cells[9].Text.ToString().Trim();
//				ViewState["kehu_no"] = e.Item.Cells[10].Text.ToString().Trim().Replace("&nbsp;","");
//			}
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			System.Web.UI.HtmlControls.HtmlInputCheckBox ctrChkBox;
			string user_id = DropDownList2.SelectedValue.ToString();
			foreach(DataGridItem dg_item in dgdCompany.Items)
			{				
				ctrChkBox = (HtmlInputCheckBox)dg_item.FindControl("chkSelect");	
				#region 
				if(ctrChkBox.Checked)
				{
					string person_id  = dg_item.Cells[9].Text.ToString().Trim();
					string car_id = dg_item.Cells[10].Text.ToString().Trim();
					string kehu_no = dg_item.Cells[11].Text.ToString().Trim().Replace("&nbsp;","");
					string assign_type = "";
					if(DropDownList1.SelectedValue == "δ���䱣����������")
					{
						assign_type = "��������";
					}
					if(DropDownList1.SelectedValue == "δ������������")
					{
						assign_type = "��Ҫ��ϵ";
					}
					if(kehu_no == "")
					{
						//�����������
						Hashtable ht = new Hashtable();
						ht["userId"] = user_id;
						ht["car_id"] = car_id;
						ht["assign_type"] = assign_type;
						ht["personId"] = person_id;
						ht["assign_role"] = "�ѷ���";
						//¼����
						ht["plan_id"] = CookieHelper.getUserIndentity(this).UserInfo["userId"].ToString();
						UserAssignHelper objassign = new UserAssignHelper();
						if(objassign.SaveUserAssigner(ht))
						{
							JavaScriptHelper.AlertMessage(this.Page,"����ɹ�");
							SetVisble();						
						}
					}
					else
					{
						//�����������
						Hashtable ht = new Hashtable();
						ht["kehu_no"] =kehu_no;
						ht["userId"] = user_id;
						UserAssignHelper objassign = new UserAssignHelper();
						if(objassign.UpdateUserAssigner(ht))
						{
							JavaScriptHelper.AlertMessage(this.Page,"��������ɹ�");
							SetVisble();
							
						}
					}
				}
				#endregion

				
			}
			BinderBaoYang();
		}


	}
}
