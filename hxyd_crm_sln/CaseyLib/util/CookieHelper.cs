//using Powerise.Hygeia.Framework;
using System;
using System.Data;
using System.Web;
using System.Web.UI;


namespace CaseyLib.util
{

		public class CookieHelper 
		{
			private CookieHelper()
			{
			}
       
			private const string cOOKIE_NAME = "hxyd_crm";
			private const string cOOKIE_STRING = "{0}|{1}";
       
			public static HttpCookie createCookie(string userName)
			{
				string str = CryptoHelper.CommonEncrypt(string.Format("{0}|{1}", userName, DateTime.Now));
				HttpCookie cookie = new HttpCookie(cOOKIE_NAME, str);
				cookie.Expires = DateTime.Now + new TimeSpan( 0, 1, 0, 0);
				
				return cookie;
			}
			public static void setCookie(string userName,HttpContext context)
			{

				HttpCookie cookie=createCookie(userName);
				context.Response.SetCookie(cookie);
				
			}
			public static void delCookie(HttpContext context)
			{
			

				context.Response.Cookies[cOOKIE_NAME].Expires= DateTime.Now.AddDays(-1);
				//context.Response.Cookies.Remove(cOOKIE_NAME);
			}
       
			public static UserIndentity getUserIndentity(Page page)
			{
				HttpCookie cookie = page.Request.Cookies[cOOKIE_NAME];
				if (((cookie == null) || (cookie.Value == null)) || (cookie.Value == ""))
				{
					return null;
				}
				string[] strArray = CryptoHelper.CommonDecrypt(cookie.Value).Split(new char[] { '|' });
				UserIndentity indentity = new UserIndentity();
				indentity.LoginUser = strArray[0];
				indentity.LoginTime = strArray[1];
				return indentity;
			}

			public static bool isLogin(HttpRequest request)
			{
				HttpCookie cookie = request.Cookies[cOOKIE_NAME];
				if (((cookie == null) || (cookie.Value == null)) || (cookie.Value == ""))
				{
					return false;
				}
				return true;
			}
       
			public static bool isLogin(Page page)
			{
				return isLogin(page.Request);
			}
       
			public static bool isValidUser(string strUser, string strPassword)
			{
				DataRow row = StaffMapping.getInstance()[strUser];
				if (row == null)
				{
					return false;
				}
				if ((strPassword == "") && (row["staff_pwd"] == DBNull.Value))
				{
					return true;
				}
				strPassword = CryptoHelper.CommonEncrypt(strPassword);
				return (strPassword == row["staff_pwd"].ToString());
			}
    
		}



}
