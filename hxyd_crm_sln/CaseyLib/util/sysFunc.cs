
using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;



namespace CaseyLib.util
{
	/// <summary>
	/// sysFunc ��ժҪ˵����
	/// </summary>
	public class sysFunc
	{
		public sysFunc()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public static long getMaxNo(string strColumnType)
		{
			
			string strSql="select current_value from sys_serial where serial_type='"+strColumnType+"'";

			using (IDbConnection con=DBFunc.getConnection())
			{
				object objRet =  DBFunc.executeScalar(con,strSql);

				int nRet=int.Parse( objRet.ToString());
				int nCurrent=nRet+1;

				IDbTransaction trans=con.BeginTransaction();

				string strUpdate=" update sys_serial set current_value="+nCurrent.ToString()+" where serial_type='"+strColumnType+"'";
				try
				{
					DBFunc.executeNonQuery(trans,strUpdate);
					trans.Commit();
					return nRet;
				}
				catch(Exception ex)
				{
					throw new Exception("��ȡ������к�ʱ����"+ex.Message);
				}
			}
		}
	}
}