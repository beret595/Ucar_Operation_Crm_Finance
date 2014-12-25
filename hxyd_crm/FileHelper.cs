using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.OleDb;
using System.Text;
using System.Xml;

namespace casey.hxyd_crm.Web.UI
{
	/// <summary>
	/// FileHelper 的摘要说明。
	/// </summary>
	public class FileHelper
	{
		public FileHelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 上传文件到默认路径,此方法会自动覆盖服务器上同名文件
		/// </summary>
		/// <param name="File1"></param>
		/// <returns></returns>
		public static string UploadFile(HtmlInputFile File1)
		{
			return UploadFile(File1,null);
		}
		/// <summary>
		/// 上传文件到指定目录或默认路径(export\temp),此方法会自动覆盖服务器上同名文件
		/// </summary>
		/// <param name="strTargetPath">指定服务器上的路径</param>
		/// <returns>服务器新的文件完全限定名</returns>
		public static string UploadFile(HtmlInputFile File1, string strTargetPath)
		{
			StreamReader sr = new StreamReader(File1.PostedFile.InputStream,System.Text.Encoding.Default);	
			string fileName = File1.Value.Substring(File1.Value.LastIndexOf(@"\") + 1);
			if(fileName.Equals(""))
			{
				throw new Exception("请先选择导入文件！");
			}
			if(fileName.LastIndexOf(".")==-1)
			{
				throw new Exception("文件必须包含后缀名！");
			}
			string strFileEnd=fileName.Substring(fileName.LastIndexOf("."));//后缀
			string strName=fileName.Substring(0,fileName.LastIndexOf("."));//文件名不含后缀
			string FileTypes=".xls|.xml|.txt|.XLS|.XML|.TXT|.mdb|.MDB";
			if(FileTypes.IndexOf(strFileEnd)<0)
			{
				
				throw new Exception("不支持该类型文件！");

			}
			string strUploadPath=null;
			if(strTargetPath!=null && strTargetPath.Trim() != string.Empty)
				strUploadPath=strTargetPath;
			else strUploadPath =HttpContext.Current.Server.MapPath("~")+"\\temp";
			System.IO.Directory.CreateDirectory(strUploadPath);				
			HttpPostedFile hpf = File1.PostedFile;

			string strNewFileName = strName+strFileEnd;//新文件名
			string strFullPath=strUploadPath+strNewFileName;
			hpf.SaveAs(strFullPath);
			return strFullPath;
		}


		public static DataTable ImportXLSFile(string strFullPathName)
		{
			DataTable dt=new DataTable();
			/*@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\MyExcel.xls;Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""" 
			"HDR=Yes;" indicates that the first row contains columnnames, not data 
			"IMEX=1;" tells the driver to always read "intermixed" data columns as text 
			TIP! SQL syntax: "SELECT * FROM [sheet1$]" - i.e. worksheet name followed by a "$" and wrapped in "[" "]";HDR=Yes;IMEX=1 brackets*/
			using( OleDbConnection oleDbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFullPathName + ";Extended Properties=Excel 8.0"))
			{
				oleDbConn.Open();
				DataTable dtTables = oleDbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,new object[] { null, null, null, "TABLE" });
				//获取EXCEL架构信息（所有的表单）,包含所有的表单名字
				string strSheetName=dtTables.Rows[0]["TABLE_NAME"].ToString();//默认读取第一个表单
				try 
				{	
					//				string strFileName=strFullPathName.Substring(strFullPathName.LastIndexOf(@"\")+1);
					//				strFileName = strFileName.Substring(0, strFileName.IndexOf(@"."));
					OleDbCommand oleDbComm = new OleDbCommand("SELECT * FROM ["+strSheetName+"]",oleDbConn);

					OleDbDataAdapter oleDbDA = new OleDbDataAdapter(oleDbComm);
					oleDbDA.Fill(dt);
				}
				catch(Exception ex)
				{
					throw new Exception("导入EXCEL文件时出错："+ex.Message);
				}
			}
			return dt;
		}

		public static int InsertData(SqlTransaction trans, string strTargetTable,DataTable dt)
		{
			

			if(dt==null || dt.Rows.Count==0)
				throw new Exception("没有数据可导出!");
			StringBuilder lSQL=new StringBuilder();
			lSQL.Append("select batch_no,");

			for(int i=0;i<dt.Columns.Count;i++)
			{
				lSQL.Append(dt.Columns[i].ColumnName+",");				
			}
			lSQL.Remove(lSQL.Length-1,1);
			lSQL.Append(" from "+strTargetTable);
			//填充架构信息

			DataSet dsTarget=new DataSet();
			DataTable dtTemp=new DataTable(strTargetTable);
			dsTarget.Tables.Add(dtTemp);
			SqlCommand cmd=new SqlCommand(lSQL.ToString(),trans.Connection,trans);
			SqlDataAdapter adapter=new SqlDataAdapter(cmd);
			SqlCommandBuilder cmdBuilder=new SqlCommandBuilder(adapter);//此代码不能省
						
						
			try
			{
				adapter.FillSchema(dsTarget,SchemaType.Mapped,strTargetTable);			

				for(int i=0;i<dt.Rows.Count;i++)
				{
					
					DataRow dr=dtTemp.NewRow();
					for(int j=0;j<dt.Columns.Count;j++)
					{
						if(dt.Rows[i][j].ToString()=="")
						{
							dr[dt.Columns[j].ColumnName]=DBNull.Value;
						}
						else
						{
							dr[dt.Columns[j].ColumnName]=dt.Rows[i][j];
						}
					}
			
					dtTemp.Rows.Add(dr);
				}
				adapter.Update(dsTarget,strTargetTable);
							
			}
			catch(Exception ex)
			{
				throw new Exception("FileHelper.InsertData:"+ex.Message);
			}
			return dt.Rows.Count;
		}

		public static int ExportXLSFile(DataTable dt,string strFullPathName)
		{

			int nCount=0;
			//创建指向EXCEL OLEDB 数据连接
			System.Data.OleDb.OleDbConnection objConn = new System.Data.OleDb.OleDbConnection(
				"Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + strFullPathName + 
				";Extended Properties=Excel 8.0;");
			

			System.Data.OleDb.OleDbCommand objCmd = new OleDbCommand();
				
			objConn.Open();

			objCmd.Connection = objConn;
			StringBuilder sbCreateText= new StringBuilder();
			sbCreateText.Append("CREATE TABLE Sheet1(");
			foreach(DataColumn dc in dt.Columns)//组装SQL语句（创建EXCEL表格写入列信息）
			{
				string strColumnName = dc.ColumnName;
				sbCreateText.Append(strColumnName);
				sbCreateText.Append(" varchar,");
			}
			sbCreateText.Remove(sbCreateText.Length-1,1);
			sbCreateText.Append(")");

			objCmd.CommandText=sbCreateText.ToString();
			objCmd.ExecuteNonQuery();//执行SQL创建表单（包含列信息）
			sbCreateText.Remove(0,sbCreateText.Length);

			//循环将数据插入EXCLE表中
			StringBuilder sbCmdText=new StringBuilder();
			foreach(DataRow dtRow in dt.Rows)
			{
				sbCmdText.Append("Insert Into Sheet1 values(");
				for(int i=0;i<dt.Columns.Count ;i++)
				{
					sbCmdText.Append("'"+dtRow[i].ToString()+"',");
				}
				sbCmdText.Remove(sbCmdText.Length-1,1);
				sbCmdText.Append(")");

				objCmd.CommandText=sbCmdText.ToString();
				objCmd.ExecuteNonQuery();
				sbCmdText.Remove(0,sbCmdText.Length);
				nCount++;
				
			}
			objConn.Close();
			return nCount;
		}
		public static DataTable ExportTransfer(Hashtable htbColumn,DataTable dtSource)
		{
			IDictionaryEnumerator ide=htbColumn.GetEnumerator();
			
			DataTable dtTemp=dtSource.Clone();
			ide.Reset();
			bool bFound=false;
			string strTempColumn=null;
			for(int i=0;i<dtTemp.Columns.Count;i++)
			{
				bFound=false;
				strTempColumn=dtTemp.Columns[i].ColumnName;
				while(ide.MoveNext())
				{
					if(ide.Key.ToString().ToLower()==strTempColumn.ToLower())
					{
						dtSource.Columns[strTempColumn].ColumnName=ide.Value.ToString();
						bFound=true;
						break;
					}
				}
				if(!bFound)
					dtSource.Columns.Remove(strTempColumn);
				ide.Reset();
			}
				
			
			return dtSource;
		}
	}
}
