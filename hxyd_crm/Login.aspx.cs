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

//using Powerise.Hygeia.Framework;
//using Powerise.Hygeia.Framework.util;
using CaseyLib;
using CaseyLib.util;
using Powerise.Hygeia.Web.UI.WebControls;

namespace casey.hxyd_crm.Web.UI
{
	/// <summary>
	/// 用户登录。
	/// </summary>
	public class Login : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlInputText tbUserName;
		protected System.Web.UI.HtmlControls.HtmlInputText tbUserNo;
		protected System.Web.UI.HtmlControls.HtmlInputText tbUserPassword;
		protected System.Web.UI.HtmlControls.HtmlInputText tbVerifyCode;
		protected System.Web.UI.WebControls.Button btnLogin;
	
		//protected readonly string USE_CHECKCODE = ParameterMapping.getInstance().getStringByCode("use_checkcode", "1");

		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!this.IsPostBack)
			{
				JavaScriptHelper.RunScript(this, ScriptPos.End, "S('tbUserName').focus();");
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
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnLogin_Click(object sender, System.EventArgs e)
		{

			try
			{
				//获取用户名、密码
				string strUserName=null;
				string strPassword=null;
				DataRow dr= StaffMapping.getInstance()[strUserName];
				if(dr==null)
				{
					JavaScriptHelper.AlertMessage(this,"不存在的用户名");
					return;
				}
				if(strPassword==dr["userPassword"].ToString())
				{
					string strCryPass = CryptoHelper.CommonEncrypt(strPassword);
				}
				CookieHelper.createCookie("hxyd_crm");
				Response.Redirect("MainManager.aspx", false);
			}
			catch(Exception ex)
			{
				JavaScriptHelper.AlertMessage(this,ex.Message);
			}
			finally
			{
			}

//			int loginstatus = 0;
//
//			string loginuser = "";
//			DataRow dr = null;
//			try
//			{
//
////								if(USE_CHECKCODE != "0")
////								{
////									if (!CookieHelper.verifyCheckCode(this.Context, this.tbVerifyCode.Value))
////									{
////										JavaScriptHelper.RunScript(this, ScriptPos.End, "showMsg('errorVerifyCode');S('tbVerifyCode').focus();");
////										return;
////									}
////								}
//				
//				
//								if(this.tbUserName.Value == "")
//								{
//									JavaScriptHelper.RunScript(this, ScriptPos.End, "showMsg('emptyUserName');S('tbUserName').focus();");
//									return;
//								}
//								if(this.tbUserNo.Value == "")
//								{
//									JavaScriptHelper.RunScript(this, ScriptPos.End, "showMsg('emptyUserNo');S('tbUserNo').focus();");
//									return;
//								}
//				
//								loginuser = StaffMapping.combine(new string[]{this.tbUserName.Value, this.tbUserNo.Value});
//								//获取人员信息
//								dr = StaffMapping.getInstance().add(loginuser);
//								if(dr == null)
//								{
//									JavaScriptHelper.RunScript(this, ScriptPos.End, "showMsg('errorUserNameNo');S('tbUserName').focus();");
//									return;
//								}
//							
//								loginstatus = 1;
//				
//								//校验密码
//								string password = dr["staff_pwd"].ToString();
//								bool bPassword = false;
//								if(password == "null")
//								{
//									int rightlen = 1;
//									if (loginuser.Length > 6)
//									{
//										rightlen = loginuser.Length - 6;
//									}
//								
//									if(this.tbUserPassword.Value.ToLower() == loginuser.Substring(rightlen).ToLower())
//									{
//										bPassword = true;
//									}
//								}
//								else
//								{
//									if(CryptoHelper.CommonEncrypt(this.tbUserPassword.Value) == password)
//									{
//										bPassword = true;
//									}
//								}
//								if(!bPassword)
//								{
//									JavaScriptHelper.RunScript(this, ScriptPos.End, "showMsg('errorPassowrd');S('tbUserPassword').focus();");
//									return;
//								}
//				
//								loginstatus = 2;
//				
//								//登录成功跳转
//				
//								CookieHelper.setCookie(loginuser, Context);
//								//Server.Transfer("Main.aspx");//这种跳转客户端cookie没有发生变化
//								Response.Redirect("Main.aspx", false);
//							}
//							catch (System.Exception ex)
//							{
//								JavaScriptHelper.AlertMessage(this, "登录时出错", ex);
//							}
//							finally
//							{
//								if (loginstatus > 0 && dr != null)//存在用户才记录日志
//								{
//									//记录日志
//									IDbConnection conn = null;
//									IDbTransaction trans = null;
//									try
//									{
//										Hashtable ht = new Hashtable();
//										ht["userlog_id"] = SysFunc.getMaxNo("userlog_id");
//										ht["application_name"] = AppConfig.ApplicationName;
//										ht["login_user"] = loginuser;
//										ht["login_ip"] = PageHelper.getIpAddress(this);
//										ht["user_name"] = dr["staff_name"];
//										ht["user_id"] = dr["staff_id"];
//										ht["user_type"] = "1";//个人用户
//										ht["log_type"] = "1";//登录
//										ht["log_name"] = "login";
//										ht["log_content"] = loginstatus > 1 ? "登录成功" : "登录失败[密码错误]";
//										ht["log_flag"] = loginstatus > 1 ? "1" : "0";
//										ht["remark"] = "";
//				
//										conn = DBFunc.getConnection();
//										trans = conn.BeginTransaction();
//				
//										StringBuilder lSQL = new StringBuilder();
//				
//										lSQL.Append(" insert into sys_user_log ");
//										lSQL.Append("   (userlog_id, ");
//										lSQL.Append("    application_name, ");
//										lSQL.Append("    login_user, ");
//										lSQL.Append("    login_ip, ");
//										lSQL.Append("    user_name, ");
//										lSQL.Append("    user_id, ");
//										lSQL.Append("    user_type, ");
//										lSQL.Append("    log_type, ");
//										lSQL.Append("    log_name, ");
//										lSQL.Append("    log_content, ");
//										lSQL.Append("    log_flag, ");
//										lSQL.Append("    log_date, ");
//										lSQL.Append("    remark) ");
//										lSQL.Append(" values ");
//										lSQL.Append("   (:userlog_id, ");
//										lSQL.Append("    :application_name, ");
//										lSQL.Append("    :login_user, ");
//										lSQL.Append("    :login_ip, ");
//										lSQL.Append("    :user_name, ");
//										lSQL.Append("    :user_id, ");
//										lSQL.Append("    :user_type, ");
//										lSQL.Append("    :log_type, ");
//										lSQL.Append("    :log_name, ");
//										lSQL.Append("    :log_content, ");
//										lSQL.Append("    :log_flag, ");
//										lSQL.Append("    sysdate, ");
//										lSQL.Append("    :remark) ");
//				
//										DBFunc.executeNonQuery(trans, lSQL.ToString(), ht);
//				
//										trans.Commit();
//										trans = null;
//									}
//									catch (System.Exception ex)
//									{
//										LogHelper.log(this.GetType(), "记录操作日志时出错", ex);
//									}
//									finally
//									{
//										try
//										{
//											if (trans != null)
//											{
//												trans.Rollback();
//												trans = null;
//											}
//				
//											if (conn != null)
//											{
//												conn.Close();
//												conn = null;
//											}
//										}
//										catch (System.Exception se)
//										{
//											LogHelper.log(this.GetType(), "释放数据库连接出错", se);
//										}
//									}
//			}
//			catch(Exception ex)
//			{
//				JavaScriptHelper.AlertMessage(this,ex.Message);
//			}
		}
		}
	}
	

