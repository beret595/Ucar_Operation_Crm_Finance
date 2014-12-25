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
	/// ReceiveManage ��ժҪ˵����
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
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!Page.IsPostBack)
			{
				BinderData();
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		public void BinderData()
		{
            //��ȡ��ǰ��¼���û�
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
				JavaScriptHelper.AlertMessage(this.Page,"��д��ʾ��¼����ʽ����");
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
			//������ܵ�Ч��
			UserAssignHelper obj_userAssign = new UserAssignHelper();	
			if(e.CommandName =="Select")
			{
				//�ж��Ƿ���δ���ܵ�״̬
				String assign_role = e.Item.Cells[11].Text.ToString();
				if(assign_role == "�ѷ���")
				{
					string kehu_no = e.Item.Cells[10].Text.ToString();
					string card_id = e.Item.Cells[9].Text.ToString();
					string assign_role_update = "�ѽ���";
					Hashtable ht = new Hashtable();
					ht["kehu_no"] = kehu_no;
					ht["assign_role"] = assign_role_update;
					ht["card_id"] = card_id;
					if(obj_userAssign.UpdateUserAssigner_Role(ht))
					{
						JavaScriptHelper.AlertMessage(this.Page,"���ܳɹ�");
						this.BinderData();
					}
				}
				else
				{
					JavaScriptHelper.AlertMessage(this.Page,"����ҵ��״̬���ܽ���");
					return;
				}
			}
			if(e.CommandName =="Compl")
			{
				//�ж��Ƿ����ѽ��ܵ�״̬
				String assign_role = e.Item.Cells[11].Text.ToString();
				String carid=e.Item.Cells[9].Text;
				if(assign_role == "�ѽ���")
				{
					string kehu_no = e.Item.Cells[10].Text.ToString();
					string assign_role_update = "�����";
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
					JavaScriptHelper.AlertMessage(this.Page,"����ҵ��״̬���ܺ���");
					return;
				}
			}
		}



	}
}
