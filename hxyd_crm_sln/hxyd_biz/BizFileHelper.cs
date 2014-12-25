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
using TTF160;

namespace  hxyd_biz
{
	/// <summary>
	/// FileHelper 的摘要说明。
	/// </summary>
	public class BizFileHelper
	{
		public BizFileHelper()
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
			string strFileEnd=fileName.Substring(fileName.LastIndexOf(".")).ToLower();//后缀
			string strName=fileName.Substring(0,fileName.LastIndexOf("."));//文件名不含后缀
			string FileTypes=".xlsx|.xls|.xml|.txt|XLSX|.XLS|.XML|.TXT|.mdb|.MDB";
			if(FileTypes.IndexOf(strFileEnd)<0)
			{
				
				throw new Exception("不支持该类型文件！");

			}
			string strUploadPath=null;
			if(strTargetPath!=null && strTargetPath.Trim() != string.Empty)
				strUploadPath=strTargetPath;
			else strUploadPath =HttpContext.Current.Server.MapPath("~")+"\\temp\\";
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
			using( OleDbConnection oleDbConn = new OleDbConnection("Provider= Microsoft.Ace.OleDb.12.0;Data Source=" + strFullPathName + ";Extended Properties=Excel 12.0"))
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

		public static DataTable WriteXLSFile(string strFullPathName,DataTable dtResult)
		{
			DataTable dt=new DataTable();
			/*@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\MyExcel.xls;Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""" 
			"HDR=Yes;" indicates that the first row contains columnnames, not data 
			"IMEX=1;" tells the driver to always read "intermixed" data columns as text 
			TIP! SQL syntax: "SELECT * FROM [sheet1$]" - i.e. worksheet name followed by a "$" and wrapped in "[" "]";HDR=Yes;IMEX=1 brackets*/
			using( OleDbConnection oleDbConn = new OleDbConnection("Provider= Microsoft.Ace.OleDb.12.0;Data Source=" + strFullPathName + ";Extended Properties=Excel 12.0"))
			{
				oleDbConn.Open();
				DataTable dtTables = oleDbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,new object[] { null, null, null, "TABLE" });
				//获取EXCEL架构信息（所有的表单）,包含所有的表单名字
				string strSheetName=dtTables.Rows[0]["TABLE_NAME"].ToString();//默认读取第一个表单
				try 
				{	
					//				string strFileName=strFullPathName.Substring(strFullPathName.LastIndexOf(@"\")+1);
					//				strFileName = strFileName.Substring(0, strFileName.IndexOf(@"."));
				//	OleDbCommand oleDbComm = new OleDbCommand(" insert into  ["+strSheetName+"] values(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18)",oleDbConn);
					string strInsert=null;
					
					for(int i=0;i<dtResult.Rows.Count;i++)
					{
						strInsert=" insert into  ["+strSheetName+"] values(";
						for(int j=0;j<dtResult.Columns.Count;j++)
						{
							strInsert+="'"+dtResult.Rows[i][j].ToString()+"'";
							if(j!=dtResult.Columns.Count-1)
							{
								strInsert+=",";
							}
						}
						strInsert+=")";
						OleDbCommand oleDbComm = new OleDbCommand(strInsert,oleDbConn);
						oleDbComm.ExecuteNonQuery();
						strInsert=null;

					}
					
				
//
//					OleDbDataAdapter oleDbDA = new OleDbDataAdapter(oleDbComm);
//					oleDbDA.Fill(dt);
				}
				catch(Exception ex)
				{
					throw new Exception("导入EXCEL文件时出错："+ex.Message);
				}
			}
			return dt;
		}
//		private void MakeBPFileTotal(System.Data.DataTable dtBPInfoTotal,System.Data.DataTable dtBPInfo,string strFullPathName)
//		{
//		
//			TTF160.IF1BookView sheet = new F1BookView();
//			IF1BookView s=new  F1BookView(
//			
//			sheet.set_ColWidth(1, 80 * 25);
//			sheet.set_ColWidth(2, 100 * 25);
//			sheet.set_ColWidth(3, 100 * 25);
//			sheet.set_ColWidth(4, 120 * 25);
//			sheet.set_ColWidth(5, 120 * 25);
//			sheet.set_ColWidth(6, 220 * 25);
//			sheet.set_ColWidth(7, 140 * 25);
//			sheet.set_ColWidth(8, 400 * 25);
//			sheet.set_ColWidth(9, 140 * 25);
//			sheet.set_ColWidth(10, 160 * 25);
//		
//			
//			sheet.SetSelection(1,1,1,10);  //SetSelection(行,起始列,行,结束列)
//			TTF160.F1CellFormat cellFormat2 = sheet.GetCellFormat();
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 14;
//			
//			cellFormat2.FontName="宋体";
//			cellFormat2.FontBold=true;
//			cellFormat2.MergeCells = true;//合并单元格
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(1, 1,"中远集团保全变更申请书明细表");
//
//			
//
//			sheet.SetSelection(3,1,3,1);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.set_BorderStyle(TTF160.F1BorderConstants.F1RightBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			cellFormat2.set_BorderStyle(TTF160.F1BorderConstants.F1LeftBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			cellFormat2.set_BorderStyle(TTF160.F1BorderConstants.F1TopBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="宋体";
//			cellFormat2.FontBold=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3, 1,"序号");
//
//			sheet.SetSelection(3,2,3,2);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="宋体";
//			cellFormat2.FontBold=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3,2 ,"姓名");
//
//			sheet.SetSelection(3,3,3,3);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="宋体";
//			cellFormat2.FontBold=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3,3 ,"性别");
//
//			sheet.SetSelection(3,4,3,4);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="宋体";
//			cellFormat2.FontBold=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3,4 ,"出生日期");
//
//			sheet.SetSelection(3,5,3,5);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="宋体";
//			cellFormat2.FontBold=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3,5 ,"证件类型");
//
//			sheet.SetSelection(3,6,3,6);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="宋体";
//			cellFormat2.FontBold=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3,6 ,"身份证号");
//
//			sheet.SetSelection(3,7,3,7);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="宋体";
//			cellFormat2.FontBold=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3,7 ,"保全类型");
//
//			sheet.SetSelection(3,8,3,8);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="宋体";
//			cellFormat2.FontBold=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3,8 ,"所属单位");
//
//			sheet.SetSelection(3,9,3,9);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="宋体";
//			cellFormat2.FontBold=true;
//			cellFormat2.WordWrap=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3,9 ,"标志位：新人填“1”老人填“2”");
//
//			sheet.SetSelection(3,10,3,10);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="宋体";
//			cellFormat2.FontBold=true;
//			cellFormat2.WordWrap=false;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3,10,"系统电脑编号");
//						
//			                        
//			int rows1 =0;
//			for(int i=0;i<dtBPInfo.Rows.Count;i++)
//			{
//				rows1 = i+4;
//				sheet.SetSelection(rows1,1,rows1,1);
//				cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//				cellFormat2.FontBold=false;
//				sheet.SetCellFormat(cellFormat2);
//				sheet.set_TextRC(rows1,1,dtBPInfo.Rows[i]["rownum"].ToString());
//				sheet.SetSelection(rows1,2,rows1,2);
//				cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//				
//				sheet.SetCellFormat(cellFormat2);       
//				sheet.set_TextRC(rows1,2,dtBPInfo.Rows[i]["name"].ToString());   
//				sheet.SetSelection(rows1,3,rows1,3);
//				cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//				
//				sheet.SetCellFormat(cellFormat2); 
//				sheet.set_TextRC(rows1,3,dtBPInfo.Rows[i]["sex_name"].ToString()); 
//				sheet.SetSelection(rows1,4,rows1,4);
//				cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//				
//				sheet.SetCellFormat(cellFormat2);
//				sheet.set_TextRC(rows1,4,dtBPInfo.Rows[i]["birthday"].ToString());
//				sheet.SetSelection(rows1,5,rows1,5);
//				cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//				
//				sheet.SetCellFormat(cellFormat2);
//				sheet.set_TextRC(rows1,5,dtBPInfo.Rows[i]["idcard_type"].ToString()); 
//				
//				sheet.SetSelection(rows1,6,rows1,6);
//				cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//				sheet.SetCellFormat(cellFormat2);
//				sheet.set_TextRC(rows1,6,dtBPInfo.Rows[i]["idcard"].ToString()); 
//
//				sheet.SetSelection(rows1,7,rows1,7);
//				cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//				sheet.SetCellFormat(cellFormat2);
//				sheet.set_TextRC(rows1,7,dtBPInfo.Rows[i]["busi_name"].ToString()); 
//
//				sheet.SetSelection(rows1,8,rows1,8);
//				cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//				sheet.SetCellFormat(cellFormat2);
//				sheet.set_TextRC(rows1,8,dtBPInfo.Rows[i]["corp_name"].ToString()); 
//
//				sheet.SetSelection(rows1,9,rows1,9);
//				cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//				sheet.SetCellFormat(cellFormat2);
//				sheet.set_TextRC(rows1,9,dtBPInfo.Rows[i]["new_old_type"].ToString()); 
//
//				sheet.SetSelection(rows1,10,rows1,10);
//				cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//				sheet.SetCellFormat(cellFormat2);
//				sheet.set_TextRC(rows1,10,dtBPInfo.Rows[i]["indi_id"].ToString()); 
//				
//			}
//			
//
//			int rowsCount1 =rows1+1;
//			
//			sheet.SetSelection(rowsCount1,1,rowsCount1,1);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat2.set_BorderStyle(TTF160.F1BorderConstants.F1RightBorder,TTF160.F1BorderStyleConstants.F1BorderNone);
//			cellFormat2.set_BorderStyle(TTF160.F1BorderConstants.F1LeftBorder,TTF160.F1BorderStyleConstants.F1BorderNone);
//			cellFormat2.set_BorderStyle(TTF160.F1BorderConstants.F1TopBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="宋体";
//			cellFormat2.FontBold=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(rowsCount1, 1,"经办人：	");
//
//			sheet.SetSelection(rowsCount1,2,rowsCount1,2); 
//			cellFormat2.FontBold=false;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(rowsCount1, 2,staff_name);
//			sheet.SetSelection(rowsCount1,3,rowsCount1,3); 
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(rowsCount1, 3,"");
//			sheet.SetSelection(rowsCount1,4,rowsCount1,4); 		
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(rowsCount1, 4,"");
//			sheet.SetSelection(rowsCount1,5,rowsCount1,5); 
//			
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(rowsCount1, 5,"");
//			sheet.SetSelection(rowsCount1,6,rowsCount1,6); 
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(rowsCount1, 6,"");
//			sheet.SetSelection(rowsCount1,7,rowsCount1,7); 
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(rowsCount1, 7,"");
//			sheet.SetSelection(rowsCount1,8,rowsCount1,8); 
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(rowsCount1, 8,"");
//			sheet.SetSelection(rowsCount1,9,rowsCount1,9); 
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(rowsCount1, 9,"");
//			sheet.SetSelection(rowsCount1,10,rowsCount1,10); 
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(rowsCount1, 10,"");
//
//		
//			
//			sheet.WriteEx(strFullPathName, TTF160.F1FileTypeConstants.F1FileExcel97);
//			
//			
//			sheet.set_SheetName(1,"人员清单(根据需要标注加人、减人或者银行信息变更)");
//			/////////
//			///
//
//
//			sheet.EditInsertSheets();
//			
//		
//			sheet.set_SheetName(1,"团体人身险保全变更申请书");
//
//			sheet.set_ColWidth(1, 300 * 25);
//			sheet.set_ColWidth(2, 150 * 25);
//			sheet.set_ColWidth(3, 200 * 25);
//			sheet.set_ColWidth(4, 350 * 25);
//			sheet.set_ColWidth(5, 200 * 25);
//			sheet.set_ColWidth(6, 200 * 25);
//
//           
//			sheet.SetSelection(1,1,1,6);  //SetSelection(行,起始列,行,结束列)
//			TTF160.F1CellFormat cellFormat1 = sheet.GetCellFormat();
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat1.FontSize = 22;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=true;
//		
//			cellFormat1.MergeCells = true;//合并单元格
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(1, 1,"团体人身险保全变更申请书(电子版专用)");
//
//			sheet.SetSelection(3, 1, 3,1);
//			cellFormat1 = sheet.GetCellFormat();
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 16;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=true;
//			
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(3, 1, "平安健康保险股份有限公司"); 
//
//			sheet.SetSelection(5, 1, 5,1);
//			cellFormat1 = sheet.GetCellFormat();
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 14;
//			cellFormat1.FontName="宋体";
//			
//			cellFormat1.FontBold=true;
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(5, 1,"    兹向贵司申请以下保单的保全变更事宜："); 
//			
//
//			sheet.SetSelection(6,1,6,1);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=true;
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1RightBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1LeftBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1TopBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(6, 1,"*保单号");
//
//			sheet.SetSelection(6,2,6,2);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=true;
//			
//			
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(6, 2,"*结算类型");
//
//			sheet.SetSelection(6,3,6,3);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=true;
//			
//			
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(6, 3,"*申请类型");
//
//
//			sheet.SetSelection(6,4,6,4);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=true;
//			
//			
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(6, 4,"*变更清单文件名");
//
//
//			sheet.SetSelection(6,5,6,5); //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=true;
//		
//			
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(6, 5,"*清单包含表格数");
//
//
//			sheet.SetSelection(6,6,6,6);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=true;
//			
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(6, 6,"备注");
//
//                                  
//			int rows =0;
//			for(int i=0;i<dtBPInfoTotal.Rows.Count;i++)
//			{
//				rows = i+7;
//				sheet.SetSelection(rows,1,rows,1);
//				cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//				cellFormat1.FontBold=false;
//				sheet.SetCellFormat(cellFormat1);
//				sheet.set_TextRC(rows,1,dtBPInfoTotal.Rows[i]["insurance_no"].ToString());
//				sheet.SetSelection(rows,2,rows,2);
//				cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//				cellFormat1.FontBold=false;
//				sheet.SetCellFormat(cellFormat1);       
//				sheet.set_TextRC(rows,2,dtBPInfoTotal.Rows[i]["dqjs"].ToString());   
//				sheet.SetSelection(rows,3,rows,3);
//				cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//				cellFormat1.FontBold=false;
//				sheet.SetCellFormat(cellFormat1); 
//				sheet.set_TextRC(rows,3,dtBPInfoTotal.Rows[i]["busi_name"].ToString()); 
//				sheet.SetSelection(rows,4,rows,4);
//				cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//				cellFormat1.FontBold=false;
//				sheet.SetCellFormat(cellFormat1);
//				sheet.set_TextRC(rows,4,dtBPInfoTotal.Rows[i]["busi_name2"].ToString());
//				sheet.SetSelection(rows,5,rows,5);
//				cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//				cellFormat1.FontBold=false;
//				sheet.SetCellFormat(cellFormat1);
//				sheet.set_TextRC(rows,5,dtBPInfoTotal.Rows[i]["num"].ToString()); 
//				sheet.SetSelection(rows,6,rows,6);
//				cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//				cellFormat1.FontBold=false;
//				sheet.SetCellFormat(cellFormat1);
//				sheet.set_TextRC(rows,6,dtBPInfoTotal.Rows[i]["remark"].ToString()); 
//				
//			}
//			
//
//			int rowsCount =rows+1;
//            
//			sheet.SetSelection(rowsCount,1,rowsCount,1);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1RightBorder,TTF160.F1BorderStyleConstants.F1BorderNone);
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1LeftBorder,TTF160.F1BorderStyleConstants.F1BorderNone);
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1TopBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=true;
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount, 1,"投保人备注说明：");
//
//			sheet.SetSelection(rowsCount,2,rowsCount,2); 
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1TopBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount, 2,"");
//			sheet.SetSelection(rowsCount,3,rowsCount,3); 
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount, 3,"");
//			sheet.SetSelection(rowsCount,4,rowsCount,4); 		
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount, 4,"");
//			sheet.SetSelection(rowsCount,5,rowsCount,5); 
//			
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount, 5,"");
//			sheet.SetSelection(rowsCount,6,rowsCount,6); 
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount, 6,"");
//
//			
//
//
//			sheet.set_RowHeight(rowsCount+1,60* 15);
//			sheet.SetSelection(rowsCount+1,1,rowsCount+1,6);  //SetSelection(行,起始列,行,结束列)
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1RightBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=false;
//			cellFormat1.MergeCells = true;//合并单元格
//			sheet.SetCellFormat(cellFormat1);
//
//
//			sheet.SetSelection(rowsCount+2,1,rowsCount+2,1); 
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1TopBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1RightBorder,TTF160.F1BorderStyleConstants.F1BorderNone);
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+2, 1,"");
//			sheet.SetSelection(rowsCount+2,2,rowsCount+2,2); 
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+2, 2,"");
//			sheet.SetSelection(rowsCount+2,3,rowsCount+2,3); 
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+2, 3,"");
//			sheet.SetSelection(rowsCount+2,4,rowsCount+2,4); 		
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+2, 4,"");
//			sheet.SetSelection(rowsCount+2,5,rowsCount+2,5); 
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+2, 5,"");
//			sheet.SetSelection(rowsCount+2,6,rowsCount+2,6); 
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+2, 6,"");
//
//
//			sheet.set_RowHeight(rowsCount+3,60* 15);
//			sheet.SetSelection(rowsCount+3,1,rowsCount+3,3);  //SetSelection(行,起始列,行,结束列)
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1TopBorder,TTF160.F1BorderStyleConstants.F1BorderNone);
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.AlignVertical = TTF160.F1VAlignConstants.F1VAlignCenter;
//			cellFormat1.FontSize = 11;
//			cellFormat1.WordWrap=true;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=true;
//			cellFormat1.MergeCells = true;//合并单元格
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+3, 1,"是否有其他书面申请资料(指除申请书和变更清单以外的资料,如:个人健康告知等)");
//
//
//			sheet.SetSelection(rowsCount+3,4,rowsCount+3,4);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat1.AlignVertical = TTF160.F1VAlignConstants.F1VAlignCenter;
//			cellFormat1.FontSize = 11;
//			cellFormat1.WordWrap=false;
//			cellFormat1.FontBold=false;
//			cellFormat1.FontName="宋体";
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+3, 4,"否");
//
//			sheet.SetSelection(rowsCount+4,1,rowsCount+4,1);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			
//			
//			
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=true;
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+4, 1,"投保人申明:");
//
//			sheet.SetSelection(rowsCount+5,1,rowsCount+5,1);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 10;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=false;
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+5, 1,"1、对于申请类型为“减少被保险人”的保全变更申请事宜，投保人已知晓自申请日次日零时起相关保险责任终止并已经告知所有相应被保险人");
//			
//			sheet.SetSelection(rowsCount+6,1,rowsCount+6,1);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 10;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=false;
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+6, 1,"2、上述申请变更事宜中的附件名称为本次变更申请的被保险人变更清单对应的文件名");
//			
//			sheet.SetSelection(rowsCount+7,2,rowsCount+7,3);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=false;
//			cellFormat1.MergeCells = true;//合并单元格
//			sheet.SetCellFormat(cellFormat1);
//
//			sheet.SetSelection(rowsCount+8,2,rowsCount+8,3);  //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=false;
//			cellFormat1.MergeCells = true;//合并单元格
//			sheet.SetCellFormat(cellFormat1);
//
//
//
//			sheet.SetSelection(rowsCount+9,1,rowsCount+9,1);   //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=true;
//			
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+9, 1,"保险公司说明：");
//
//
//			sheet.set_RowHeight(rowsCount+10,60* 15);
//			sheet.SetSelection(rowsCount+10,1,rowsCount+10,6);   //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.AlignVertical = TTF160.F1VAlignConstants.F1VAlignCenter;
//			cellFormat1.FontSize = 10;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=false;
//			cellFormat1.WordWrap=true;
//			cellFormat1.MergeCells = true;//合并单元格
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+10, 1,"1、投保人可在填写此电子变更申请书及变更清单后以附件方式从指定的授权邮箱向保险公司提交保全变更申请，发送时邮件主题必须使用“发送日期+两位批次号+单位名称”，一经保险公司确认收到并回复后，即视为保险公司已受理；如保险公司审核过程中需要您配合提供其他相关书面资料，也请您配合");
//
//
//			sheet.SetSelection(rowsCount+11,1,rowsCount+11,1);   //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 10;
//			cellFormat1.FontName="宋体";
//			cellFormat1.WordWrap=false;
//			cellFormat1.FontBold=false;
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+11, 1,"2、如有多张保单同时申请，请分行填写，同时请注意不同的保单对应的变更清单文件名必须予以区分，不能相同");
//
//			sheet.SetSelection(rowsCount+12,1,rowsCount+12,6);   //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 10;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=false;
//			cellFormat1.MergeCells = true;//合并单元格
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+12, 1,"3、如您未使用本申请书建议的“平安团体人身险被保险人清单”样式，请在投保人备注说明中明确说明本次变更申请您要求的保全变更生效时点，以供保险公司审核");
//
//			sheet.SetSelection(rowsCount+13,1,rowsCount+13,1);   //SetSelection(行,起始列,行,结束列)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 10;
//			cellFormat1.FontName="宋体";
//			cellFormat1.FontBold=false;
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+13, 1,"4、申请书中蓝色部分为必填项，请勿改动申请书中其余部分");
//
//			
//			sheet.WriteEx(strFullPathName, TTF160.F1FileTypeConstants.F1FileExcel97);
//
//		}
//
		public static int InsertData(SqlTransaction trans, string strTargetTable,DataTable dt)
		{
			

			if(dt==null || dt.Rows.Count==0)
				throw new Exception("没有数据可导入!");
			StringBuilder lSQL=new StringBuilder();
			lSQL.Append("select ");

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

