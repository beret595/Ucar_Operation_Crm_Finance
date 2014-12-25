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
	/// CustomerImport 的摘要说明。
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
				// 在此处放置用户代码以初始化页面
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
				string strFileName="客户信息模板.xls";
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
					JavaScriptHelper.AlertMessage(this,"没有信息可导入!");
					return;
				}
				DataTable dtTemp=dt.Clone();
				DataTable dtError=dt.Clone();
				for(int i=0;i<dt.Rows.Count;i++)
				{
					if(dt.Rows[i]["姓名"].ToString().Trim()!="")
					{
						string strPhone =dt.Rows[i]["手机"].ToString();
						if(strPhone=="")
						{
							dt.Rows[i]["备注"]="手机号码为空!";
							dtError.Rows.Add(dt.Rows[i].ItemArray);
							continue;
						}
						int nRet=Customer.existsPhone(strPhone);
						if(nRet>0)
						{
							dt.Rows[i]["备注"]="手机号码已经存在!";
							dtError.Rows.Add(dt.Rows[i].ItemArray);
						}
						else
						{
							dtTemp.Rows.Add(dt.Rows[i].ItemArray);
						}
					}
					else if(dt.Rows[i]["手机"].ToString().Trim()!="")
					{
						dt.Rows[i]["备注"]="客户姓名为空!";
						dtError.Rows.Add(dt.Rows[i].ItemArray);
					}
				}
				/*姓名 personName	性别gender	手机	phone 所在区域 area	地址 address
	品牌brand	车型model	VIN	牌照licensePlate	购车日期 salesDate	介绍人introducer	备注remark*/


				Hashtable htbColumn=new Hashtable();
				htbColumn["姓名"]="personName";
				htbColumn["性别"]="gender";
				htbColumn["手机"]="phone";
				htbColumn["所在区域"]="area";
				htbColumn["地址"]="address";
				htbColumn["品牌"]="brand";
				htbColumn["车型"]="model";
				htbColumn["VIN"]="VIN";
				htbColumn["牌照"]="licensePlate";

				htbColumn["购车日期"]="salesDate";
				htbColumn["介绍人"]="introducer";
				htbColumn["客户来源"]="customerType";
				htbColumn["备注"]="remark";



				DataTable  dtImport = BizFileHelper.ExportTransfer(htbColumn,dtTemp);

				int nCount =0;
				if(dtImport.Rows.Count>0)
					nCount= Customer.ImportCustomerInfo(dtImport);
				int nError=dtError.Rows.Count;
				JavaScriptHelper.AlertMessage(this,"导入客户信息成功，共成功导入记录"+nCount+"条，失败"+nError+"条");
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
					JavaScriptHelper.AlertMessage(this,"没有错误信息!");
					return;
				}
				DataTable dtTemp=(DataTable) ViewState["error"];

				string strPath = HttpContext.Current.Server.MapPath("~")+"\\temp";
				string strFileName="导入失败客户信息"+DateTime.Now.ToString("yyyyMMddhhmmss")+".xls";
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
