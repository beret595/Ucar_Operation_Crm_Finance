

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


using Powerise.Hygeia.Framework;
using Powerise.Hygeia.Framework.util;
using Powerise.Hygeia.Framework.exception;
using Powerise.Hygeia.Web.UI.WebControls;

namespace   casey.hxyd_crm.Web.UI
{
	/// <summary>
	/// DownloadHandler 的摘要说明
	/// </summary>
	public class FileDownHelper : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			HttpResponse Response = context.Response;
			HttpRequest Request = context.Request;

			System.IO.Stream iStream = null;

			byte[] buffer = new Byte[10240];
			int length;
			long dataToRead;

			bool delflag = false;
			string filename = "";
			string filepath = "";
			try
			{				
				
				filepath = HttpContext.Current.Server.MapPath("~/" + HttpUtility.UrlDecode(Request["url"]));
				System.IO.FileInfo fi = new System.IO.FileInfo(filepath);
				if(!fi.Exists)
				{
					throw new HygeiaException(String.Format("下载文件失败：指定的文件不存在({0}).",  fi.Name));
				}

				delflag = Request["df"] != null && Request["df"].ToString().Equals("true") ? true : false;

				if (Request["fn"] != null && Request["fn"].Length > 0)
				{
					filename = Request["fn"];
				} 
				else 
				{
					filename = fi.Name;
				}

				iStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open,
					System.IO.FileAccess.Read, System.IO.FileShare.Read);

				Response.Clear();

				dataToRead = iStream.Length;

				long p = 0;
				if (Request.Headers["Range"] != null)
				{
					Response.StatusCode = 206;
					p = long.Parse(Request.Headers["Range"].Replace("bytes=", "").Replace("-", ""));
				}
				if (p != 0)
				{
					Response.AddHeader("Content-Range", "bytes " + p.ToString() + "-" + ((long)(dataToRead - 1)).ToString() + "/" + dataToRead.ToString());
				}
				Response.AddHeader("Content-Length", ((long)(dataToRead - p)).ToString());
				Response.ContentType = "application/octet-stream";
				
				//string returnFileName = HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(filename));
				string returnFileName = HttpUtility.UrlEncode(filename);
				returnFileName = returnFileName.Replace("+", "%20");
				if (returnFileName.Length > 120) 
				{
					returnFileName = filename;
				}

				Response.AddHeader("Content-Disposition", "attachment; filename=" + returnFileName);
				//Response.AddHeader("Content-Disposition", "attachment; filename=" +  HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(filename)));

				iStream.Position = p;
				dataToRead = dataToRead - p;

				while (dataToRead > 0)
				{
					if (Response.IsClientConnected)
					{
						length = iStream.Read(buffer, 0, 10240);

						Response.OutputStream.Write(buffer, 0, length);
						Response.Flush();

						buffer = new Byte[10240];
						dataToRead = dataToRead - length;
					}
					else
					{
						dataToRead = -1;
					}
				}
			}
			catch (Exception ex)
			{
				Response.Write(filename + "文件下载失败。(" + ex.Message + ")");
			}
			finally
			{
				if (iStream != null)
				{
					iStream.Close();
				}

				try
				{
					if (delflag)
					{
						//FileFunc.DeleteFile(filepath);
					}

					Response.End();
				}
				catch (System.Exception)
				{
					
				}
			}
		}
		public static void DownFile(HttpContext context, string strFullName,bool delFlag)
		{
			HttpResponse Response = context.Response;
			HttpRequest Request = context.Request;

			System.IO.Stream iStream = null;

			byte[] buffer = new Byte[10240];
			int length;
			long dataToRead;

			bool delflag = delFlag;
			string filename = "";
			string filepath = "";
			try
			{				
				
				filepath =strFullName;
				System.IO.FileInfo fi = new System.IO.FileInfo(filepath);
				if(!fi.Exists)
				{
					throw new HygeiaException(String.Format("下载文件失败：指定的文件不存在({0}).",  fi.Name));
				}

				delflag = Request["df"] != null && Request["df"].ToString().Equals("true") ? true : false;

				if (Request["fn"] != null && Request["fn"].Length > 0)
				{
					filename = Request["fn"];
				} 
				else 
				{
					filename = fi.Name;
				}

				iStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open,
					System.IO.FileAccess.Read, System.IO.FileShare.Read);

				Response.Clear();

				dataToRead = iStream.Length;

				long p = 0;
				if (Request.Headers["Range"] != null)
				{
					Response.StatusCode = 206;
					p = long.Parse(Request.Headers["Range"].Replace("bytes=", "").Replace("-", ""));
				}
				if (p != 0)
				{
					Response.AddHeader("Content-Range", "bytes " + p.ToString() + "-" + ((long)(dataToRead - 1)).ToString() + "/" + dataToRead.ToString());
				}
				Response.AddHeader("Content-Length", ((long)(dataToRead - p)).ToString());
				Response.ContentType = "application/octet-stream";
				
				//string returnFileName = HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(filename));
				string returnFileName = HttpUtility.UrlEncode(filename);
				returnFileName = returnFileName.Replace("+", "%20");
				if (returnFileName.Length > 120) 
				{
					returnFileName = filename;
				}

				Response.AddHeader("Content-Disposition", "attachment; filename=" + returnFileName);
				//Response.AddHeader("Content-Disposition", "attachment; filename=" +  HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(filename)));

				iStream.Position = p;
				dataToRead = dataToRead - p;

				while (dataToRead > 0)
				{
					if (Response.IsClientConnected)
					{
						length = iStream.Read(buffer, 0, 10240);

						Response.OutputStream.Write(buffer, 0, length);
						Response.Flush();

						buffer = new Byte[10240];
						dataToRead = dataToRead - length;
					}
					else
					{
						dataToRead = -1;
					}
				}
			}
			catch (Exception ex)
			{
				Response.Write(filename + "文件下载失败。(" + ex.Message + ")");
			}
			finally
			{
				if (iStream != null)
				{
					iStream.Close();
				}

				try
				{
					if (delflag)
					{
						//FileFunc.DeleteFile(filepath);
					}

					Response.End();
				}
				catch (System.Exception)
				{
					
				}
			}
		
		}

		public bool IsReusable
		{
			get { return true; }
		}
	}
}
