
using System;
using System.Data;
using CaseyLib.util;

namespace CaseyLib
{


	public class MappingVersion
	{
		private MappingVersion()
		{
		}
       
		private static void init()
		{
			IDbConnection connOrTrans = null;
			try
			{
				string selectSql = "select name,version from sys_cache_mapping";
				connOrTrans = DBFunc.getConnection();
				_dtVersion = DBFunc.executeDataTable(connOrTrans, selectSql);
				_dtVersion.PrimaryKey = new DataColumn[] { _dtVersion.Columns["name"] };
			}
			catch (Exception exception)
			{
				_dtVersion = null;
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
       
		private static DataTable _dtVersion = null;
		private static MappingVersion _instance = new MappingVersion();
		private static DateTime _versionTime = DateTime.Now;
		private const int tIMMER_SECOND = 1;
       
		public static MappingVersion Instance
		{
			get
			{
				bool flag = DateTime.Now.Subtract(_versionTime) >= new TimeSpan(0, 0, 1);
				if ((_dtVersion == null) || flag)
				{
					lock (_instance)
					{
						if ((_dtVersion == null) || flag)
						{
							init();
							_versionTime = DateTime.Now;
						}
					}
				}
				return _instance;
			}
		}
       
		public string getVersion(Type type)
		{
			if (_dtVersion != null)
			{
				string fullName = type.FullName;
				DataRow row = _dtVersion.Rows.Find(fullName);
				if (row != null)
				{
					return row["version"].ToString();
				}
				return null;
			}
			return null;
		}
    
	}




}
