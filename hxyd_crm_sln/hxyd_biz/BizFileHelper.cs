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
	/// FileHelper ��ժҪ˵����
	/// </summary>
	public class BizFileHelper
	{
		public BizFileHelper()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// �ϴ��ļ���Ĭ��·��,�˷������Զ����Ƿ�������ͬ���ļ�
		/// </summary>
		/// <param name="File1"></param>
		/// <returns></returns>
		public static string UploadFile(HtmlInputFile File1)
		{
			return UploadFile(File1,null);
		}
		/// <summary>
		/// �ϴ��ļ���ָ��Ŀ¼��Ĭ��·��(export\temp),�˷������Զ����Ƿ�������ͬ���ļ�
		/// </summary>
		/// <param name="strTargetPath">ָ���������ϵ�·��</param>
		/// <returns>�������µ��ļ���ȫ�޶���</returns>
		public static string UploadFile(HtmlInputFile File1, string strTargetPath)
		{
			StreamReader sr = new StreamReader(File1.PostedFile.InputStream,System.Text.Encoding.Default);	
			string fileName = File1.Value.Substring(File1.Value.LastIndexOf(@"\") + 1);
			if(fileName.Equals(""))
			{
				throw new Exception("����ѡ�����ļ���");
			}
			if(fileName.LastIndexOf(".")==-1)
			{
				throw new Exception("�ļ����������׺����");
			}
			string strFileEnd=fileName.Substring(fileName.LastIndexOf(".")).ToLower();//��׺
			string strName=fileName.Substring(0,fileName.LastIndexOf("."));//�ļ���������׺
			string FileTypes=".xlsx|.xls|.xml|.txt|XLSX|.XLS|.XML|.TXT|.mdb|.MDB";
			if(FileTypes.IndexOf(strFileEnd)<0)
			{
				
				throw new Exception("��֧�ָ������ļ���");

			}
			string strUploadPath=null;
			if(strTargetPath!=null && strTargetPath.Trim() != string.Empty)
				strUploadPath=strTargetPath;
			else strUploadPath =HttpContext.Current.Server.MapPath("~")+"\\temp\\";
			System.IO.Directory.CreateDirectory(strUploadPath);				
			HttpPostedFile hpf = File1.PostedFile;

			string strNewFileName = strName+strFileEnd;//���ļ���
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
				//��ȡEXCEL�ܹ���Ϣ�����еı���,�������еı�����
				string strSheetName=dtTables.Rows[0]["TABLE_NAME"].ToString();//Ĭ�϶�ȡ��һ����
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
					throw new Exception("����EXCEL�ļ�ʱ����"+ex.Message);
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
				//��ȡEXCEL�ܹ���Ϣ�����еı���,�������еı�����
				string strSheetName=dtTables.Rows[0]["TABLE_NAME"].ToString();//Ĭ�϶�ȡ��һ����
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
					throw new Exception("����EXCEL�ļ�ʱ����"+ex.Message);
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
//			sheet.SetSelection(1,1,1,10);  //SetSelection(��,��ʼ��,��,������)
//			TTF160.F1CellFormat cellFormat2 = sheet.GetCellFormat();
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 14;
//			
//			cellFormat2.FontName="����";
//			cellFormat2.FontBold=true;
//			cellFormat2.MergeCells = true;//�ϲ���Ԫ��
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(1, 1,"��Զ���ű�ȫ�����������ϸ��");
//
//			
//
//			sheet.SetSelection(3,1,3,1);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.set_BorderStyle(TTF160.F1BorderConstants.F1RightBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			cellFormat2.set_BorderStyle(TTF160.F1BorderConstants.F1LeftBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			cellFormat2.set_BorderStyle(TTF160.F1BorderConstants.F1TopBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="����";
//			cellFormat2.FontBold=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3, 1,"���");
//
//			sheet.SetSelection(3,2,3,2);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="����";
//			cellFormat2.FontBold=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3,2 ,"����");
//
//			sheet.SetSelection(3,3,3,3);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="����";
//			cellFormat2.FontBold=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3,3 ,"�Ա�");
//
//			sheet.SetSelection(3,4,3,4);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="����";
//			cellFormat2.FontBold=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3,4 ,"��������");
//
//			sheet.SetSelection(3,5,3,5);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="����";
//			cellFormat2.FontBold=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3,5 ,"֤������");
//
//			sheet.SetSelection(3,6,3,6);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="����";
//			cellFormat2.FontBold=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3,6 ,"���֤��");
//
//			sheet.SetSelection(3,7,3,7);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="����";
//			cellFormat2.FontBold=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3,7 ,"��ȫ����");
//
//			sheet.SetSelection(3,8,3,8);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="����";
//			cellFormat2.FontBold=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3,8 ,"������λ");
//
//			sheet.SetSelection(3,9,3,9);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="����";
//			cellFormat2.FontBold=true;
//			cellFormat2.WordWrap=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3,9 ,"��־λ�������1�������2��");
//
//			sheet.SetSelection(3,10,3,10);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="����";
//			cellFormat2.FontBold=true;
//			cellFormat2.WordWrap=false;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(3,10,"ϵͳ���Ա��");
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
//			sheet.SetSelection(rowsCount1,1,rowsCount1,1);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat2.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat2.set_BorderStyle(TTF160.F1BorderConstants.F1RightBorder,TTF160.F1BorderStyleConstants.F1BorderNone);
//			cellFormat2.set_BorderStyle(TTF160.F1BorderConstants.F1LeftBorder,TTF160.F1BorderStyleConstants.F1BorderNone);
//			cellFormat2.set_BorderStyle(TTF160.F1BorderConstants.F1TopBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			cellFormat2.FontSize = 10;
//			cellFormat2.FontName="����";
//			cellFormat2.FontBold=true;
//			sheet.SetCellFormat(cellFormat2);
//			sheet.set_TextRC(rowsCount1, 1,"�����ˣ�	");
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
//			sheet.set_SheetName(1,"��Ա�嵥(������Ҫ��ע���ˡ����˻���������Ϣ���)");
//			/////////
//			///
//
//
//			sheet.EditInsertSheets();
//			
//		
//			sheet.set_SheetName(1,"���������ձ�ȫ���������");
//
//			sheet.set_ColWidth(1, 300 * 25);
//			sheet.set_ColWidth(2, 150 * 25);
//			sheet.set_ColWidth(3, 200 * 25);
//			sheet.set_ColWidth(4, 350 * 25);
//			sheet.set_ColWidth(5, 200 * 25);
//			sheet.set_ColWidth(6, 200 * 25);
//
//           
//			sheet.SetSelection(1,1,1,6);  //SetSelection(��,��ʼ��,��,������)
//			TTF160.F1CellFormat cellFormat1 = sheet.GetCellFormat();
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat1.FontSize = 22;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=true;
//		
//			cellFormat1.MergeCells = true;//�ϲ���Ԫ��
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(1, 1,"���������ձ�ȫ���������(���Ӱ�ר��)");
//
//			sheet.SetSelection(3, 1, 3,1);
//			cellFormat1 = sheet.GetCellFormat();
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 16;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=true;
//			
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(3, 1, "ƽ���������չɷ����޹�˾"); 
//
//			sheet.SetSelection(5, 1, 5,1);
//			cellFormat1 = sheet.GetCellFormat();
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 14;
//			cellFormat1.FontName="����";
//			
//			cellFormat1.FontBold=true;
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(5, 1,"    �����˾�������±����ı�ȫ������ˣ�"); 
//			
//
//			sheet.SetSelection(6,1,6,1);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=true;
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1RightBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1LeftBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1TopBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(6, 1,"*������");
//
//			sheet.SetSelection(6,2,6,2);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=true;
//			
//			
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(6, 2,"*��������");
//
//			sheet.SetSelection(6,3,6,3);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=true;
//			
//			
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(6, 3,"*��������");
//
//
//			sheet.SetSelection(6,4,6,4);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=true;
//			
//			
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(6, 4,"*����嵥�ļ���");
//
//
//			sheet.SetSelection(6,5,6,5); //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=true;
//		
//			
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(6, 5,"*�嵥���������");
//
//
//			sheet.SetSelection(6,6,6,6);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=true;
//			
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(6, 6,"��ע");
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
//			sheet.SetSelection(rowsCount,1,rowsCount,1);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1RightBorder,TTF160.F1BorderStyleConstants.F1BorderNone);
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1LeftBorder,TTF160.F1BorderStyleConstants.F1BorderNone);
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1TopBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=true;
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount, 1,"Ͷ���˱�ע˵����");
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
//			sheet.SetSelection(rowsCount+1,1,rowsCount+1,6);  //SetSelection(��,��ʼ��,��,������)
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1RightBorder,TTF160.F1BorderStyleConstants.F1BorderThin);
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=false;
//			cellFormat1.MergeCells = true;//�ϲ���Ԫ��
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
//			sheet.SetSelection(rowsCount+3,1,rowsCount+3,3);  //SetSelection(��,��ʼ��,��,������)
//			cellFormat1.set_BorderStyle(TTF160.F1BorderConstants.F1TopBorder,TTF160.F1BorderStyleConstants.F1BorderNone);
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.AlignVertical = TTF160.F1VAlignConstants.F1VAlignCenter;
//			cellFormat1.FontSize = 11;
//			cellFormat1.WordWrap=true;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=true;
//			cellFormat1.MergeCells = true;//�ϲ���Ԫ��
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+3, 1,"�Ƿ�������������������(ָ��������ͱ���嵥���������,��:���˽�����֪��)");
//
//
//			sheet.SetSelection(rowsCount+3,4,rowsCount+3,4);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignCenter;
//			cellFormat1.AlignVertical = TTF160.F1VAlignConstants.F1VAlignCenter;
//			cellFormat1.FontSize = 11;
//			cellFormat1.WordWrap=false;
//			cellFormat1.FontBold=false;
//			cellFormat1.FontName="����";
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+3, 4,"��");
//
//			sheet.SetSelection(rowsCount+4,1,rowsCount+4,1);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			
//			
//			
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=true;
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+4, 1,"Ͷ��������:");
//
//			sheet.SetSelection(rowsCount+5,1,rowsCount+5,1);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 10;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=false;
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+5, 1,"1��������������Ϊ�����ٱ������ˡ��ı�ȫ����������ˣ�Ͷ������֪���������մ�����ʱ����ر���������ֹ���Ѿ���֪������Ӧ��������");
//			
//			sheet.SetSelection(rowsCount+6,1,rowsCount+6,1);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 10;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=false;
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+6, 1,"2�����������������еĸ�������Ϊ���α������ı������˱���嵥��Ӧ���ļ���");
//			
//			sheet.SetSelection(rowsCount+7,2,rowsCount+7,3);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=false;
//			cellFormat1.MergeCells = true;//�ϲ���Ԫ��
//			sheet.SetCellFormat(cellFormat1);
//
//			sheet.SetSelection(rowsCount+8,2,rowsCount+8,3);  //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=false;
//			cellFormat1.MergeCells = true;//�ϲ���Ԫ��
//			sheet.SetCellFormat(cellFormat1);
//
//
//
//			sheet.SetSelection(rowsCount+9,1,rowsCount+9,1);   //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 11;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=true;
//			
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+9, 1,"���չ�˾˵����");
//
//
//			sheet.set_RowHeight(rowsCount+10,60* 15);
//			sheet.SetSelection(rowsCount+10,1,rowsCount+10,6);   //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.AlignVertical = TTF160.F1VAlignConstants.F1VAlignCenter;
//			cellFormat1.FontSize = 10;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=false;
//			cellFormat1.WordWrap=true;
//			cellFormat1.MergeCells = true;//�ϲ���Ԫ��
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+10, 1,"1��Ͷ���˿�����д�˵��ӱ�������鼰����嵥���Ը�����ʽ��ָ������Ȩ�������չ�˾�ύ��ȫ������룬����ʱ�ʼ��������ʹ�á���������+��λ���κ�+��λ���ơ���һ�����չ�˾ȷ���յ����ظ��󣬼���Ϊ���չ�˾�������籣�չ�˾��˹�������Ҫ������ṩ��������������ϣ�Ҳ�������");
//
//
//			sheet.SetSelection(rowsCount+11,1,rowsCount+11,1);   //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 10;
//			cellFormat1.FontName="����";
//			cellFormat1.WordWrap=false;
//			cellFormat1.FontBold=false;
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+11, 1,"2�����ж��ű���ͬʱ���룬�������д��ͬʱ��ע�ⲻͬ�ı�����Ӧ�ı���嵥�ļ��������������֣�������ͬ");
//
//			sheet.SetSelection(rowsCount+12,1,rowsCount+12,6);   //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 10;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=false;
//			cellFormat1.MergeCells = true;//�ϲ���Ԫ��
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+12, 1,"3������δʹ�ñ������齨��ġ�ƽ�����������ձ��������嵥����ʽ������Ͷ���˱�ע˵������ȷ˵�����α��������Ҫ��ı�ȫ�����Чʱ�㣬�Թ����չ�˾���");
//
//			sheet.SetSelection(rowsCount+13,1,rowsCount+13,1);   //SetSelection(��,��ʼ��,��,������)
//			
//			cellFormat1.AlignHorizontal = TTF160.F1HAlignConstants.F1HAlignLeft;
//			cellFormat1.FontSize = 10;
//			cellFormat1.FontName="����";
//			cellFormat1.FontBold=false;
//			sheet.SetCellFormat(cellFormat1);
//			sheet.set_TextRC(rowsCount+13, 1,"4������������ɫ����Ϊ���������Ķ������������ಿ��");
//
//			
//			sheet.WriteEx(strFullPathName, TTF160.F1FileTypeConstants.F1FileExcel97);
//
//		}
//
		public static int InsertData(SqlTransaction trans, string strTargetTable,DataTable dt)
		{
			

			if(dt==null || dt.Rows.Count==0)
				throw new Exception("û�����ݿɵ���!");
			StringBuilder lSQL=new StringBuilder();
			lSQL.Append("select ");

			for(int i=0;i<dt.Columns.Count;i++)
			{
				lSQL.Append(dt.Columns[i].ColumnName+",");				
			}
			lSQL.Remove(lSQL.Length-1,1);
			lSQL.Append(" from "+strTargetTable);
			//���ܹ���Ϣ

			DataSet dsTarget=new DataSet();
			DataTable dtTemp=new DataTable(strTargetTable);
			dsTarget.Tables.Add(dtTemp);
			SqlCommand cmd=new SqlCommand(lSQL.ToString(),trans.Connection,trans);
			SqlDataAdapter adapter=new SqlDataAdapter(cmd);
			SqlCommandBuilder cmdBuilder=new SqlCommandBuilder(adapter);//�˴��벻��ʡ
						
						
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
			//����ָ��EXCEL OLEDB ��������
			System.Data.OleDb.OleDbConnection objConn = new System.Data.OleDb.OleDbConnection(
				"Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + strFullPathName + 
				";Extended Properties=Excel 8.0;");
			

			System.Data.OleDb.OleDbCommand objCmd = new OleDbCommand();
				
			objConn.Open();

			objCmd.Connection = objConn;
			StringBuilder sbCreateText= new StringBuilder();
			sbCreateText.Append("CREATE TABLE Sheet1(");
			foreach(DataColumn dc in dt.Columns)//��װSQL��䣨����EXCEL���д������Ϣ��
			{
				string strColumnName = dc.ColumnName;
				sbCreateText.Append(strColumnName);
				sbCreateText.Append(" varchar,");
			}
			sbCreateText.Remove(sbCreateText.Length-1,1);
			sbCreateText.Append(")");

			objCmd.CommandText=sbCreateText.ToString();
			objCmd.ExecuteNonQuery();//ִ��SQL����������������Ϣ��
			sbCreateText.Remove(0,sbCreateText.Length);

			//ѭ�������ݲ���EXCLE����
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

