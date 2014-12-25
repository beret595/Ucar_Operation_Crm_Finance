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
	/// CustomerImport ��ժҪ˵����
	/// </summary>
	public class CustomerImport : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
		protected System.Web.UI.WebControls.Button btnError;
		protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				// �ڴ˴������û������Գ�ʼ��ҳ��
				if(!CookieHelper.isLogin(this))
				{
					string strScript="window.parent.location='index.aspx';";
					JavaScriptHelper.RunScript(this,ScriptPos.Begin, strScript);
					return;
				}	
				if(!this.IsPostBack)
				{
					btnError.Enabled=false;
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.btnError.Click += new System.EventHandler(this.btnError_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strPath = HttpContext.Current.Server.MapPath("~")+"\\template";
				string strFileName="�ͻ���Ϣģ��.xls";
				string strFullName=strPath+"\\"+strFileName;
				FileDownHelper.DownFile(this.Context,strFullName,true);
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
				string strFullName = BizFileHelper.UploadFile(File1);
				DataTable dt= BizFileHelper.ImportXLSFile(strFullName);
				if(dt==null || dt.Rows.Count==0)
				{
					JavaScriptHelper.AlertMessage(this,"û����Ϣ�ɵ���!");
					return;
				}
				DataTable dtTemp=dt.Clone();
				DataTable dtError=dt.Clone();
				for(int i=0;i<dt.Rows.Count;i++)
				{
					if(dt.Rows[i]["����"].ToString().Trim()!="")
					{
						string strPhone =dt.Rows[i]["�ֻ�"].ToString();
						if(strPhone=="")
						{
							dt.Rows[i]["��ע"]="�ֻ�����Ϊ��!";
							dtError.Rows.Add(dt.Rows[i].ItemArray);
							continue;
						}
						int nRet=Customer.existsPhone(strPhone);
						if(nRet>0)
						{
							dt.Rows[i]["��ע"]="�ֻ������Ѿ�����!";
							dtError.Rows.Add(dt.Rows[i].ItemArray);
						}
						else
						{
							dtTemp.Rows.Add(dt.Rows[i].ItemArray);
						}
					}
					else if(dt.Rows[i]["�ֻ�"].ToString().Trim()!="")
					{
						dt.Rows[i]["��ע"]="�ͻ�����Ϊ��!";
						dtError.Rows.Add(dt.Rows[i].ItemArray);
					}
				}
				/*���� personName	�Ա�gender	�ֻ�	phone �������� area	��ַ address
	Ʒ��brand	����model	VIN	����licensePlate	�������� salesDate	������introducer	��עremark*/


				Hashtable htbColumn=new Hashtable();
				htbColumn["����"]="personName";
				htbColumn["�Ա�"]="gender";
				htbColumn["�ֻ�"]="phone";
				htbColumn["��������"]="area";
				htbColumn["��ַ"]="address";
				htbColumn["Ʒ��"]="brand";
				htbColumn["����"]="model";
				htbColumn["VIN"]="VIN";
				htbColumn["����"]="licensePlate";

				htbColumn["��������"]="salesDate";
				htbColumn["������"]="introducer";
				htbColumn["�ͻ���Դ"]="customerType";
				htbColumn["��ע"]="remark";



				DataTable  dtImport = BizFileHelper.ExportTransfer(htbColumn,dtTemp);

				int nCount =0;
				if(dtImport.Rows.Count>0)
					nCount= Customer.ImportCustomerInfo(dtImport);
				int nError=dtError.Rows.Count;
				JavaScriptHelper.AlertMessage(this,"����ͻ���Ϣ�ɹ������ɹ������¼"+nCount+"����ʧ��"+nError+"��");
				ViewState["error"]=dtError;
				if(nError>0)
				{
					btnError.Enabled=true;
				}
				else
				{
					btnError.Enabled=false;
				}



			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		}

		private void btnError_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(ViewState["error"]==null)
				{
					JavaScriptHelper.AlertMessage(this,"û�д�����Ϣ!");
					return;
				}
				DataTable dtTemp=(DataTable) ViewState["error"];

				string strPath = HttpContext.Current.Server.MapPath("~")+"\\temp";
				string strFileName="����ʧ�ܿͻ���Ϣ"+DateTime.Now.ToString("yyyyMMddhhmmss")+".xls";
				string strFullName=strPath+"\\"+strFileName;

				FileHelper.ExportXLSFile(dtTemp,strFullName);
				FileDownHelper.DownFile(this.Context,strFullName,true);

				
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
		
		}
	}
}
