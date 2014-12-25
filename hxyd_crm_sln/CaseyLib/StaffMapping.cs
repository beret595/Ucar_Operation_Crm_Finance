
using System;
using System.Data;
using System.Reflection;
using CaseyLib.util;

namespace CaseyLib
{


		public class StaffMapping
		{
			private StaffMapping()
			{
			}
       
			private static void init()
			{
				IDbConnection connOrTrans = null;
				try
				{
					string selectSql = "select * from hdb_user where isnull(deleteFlag,0)=0";
					connOrTrans = DBFunc.getConnection();
					
					_dtStaff = DBFunc.executeDataTable(connOrTrans, selectSql);
					_dtStaff.PrimaryKey = new DataColumn[] { _dtStaff.Columns["userName"] };
				}
				catch (Exception exception)
				{
					_dtStaff = null;
					throw exception;
				}
				finally
				{
					if (connOrTrans != null)
					{
						connOrTrans.Close();
					}
				}
			}
       
			private static DataTable _dtStaff = null;
			private static StaffMapping _instance = new StaffMapping();
			private static string _version = "";
       
			public DataRow this[string strLoginUser]
			{
				get
				{
					if (_dtStaff != null)
					{
						return _dtStaff.Rows.Find(strLoginUser);
					}
					return null;
					
				}
			}
       
			public static StaffMapping getInstance()
			{
				string str ="1";
				if ((_dtStaff == null) || (_version != str))	
				{
					lock (_instance)
					{
						if ((_dtStaff == null) || (_version != str))
						if(_dtStaff==null)
						{
							init();
							_version = str;
						}
					}
				}
				init();
				return _instance;
			}
    
		}




}
