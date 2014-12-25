
using System;
using System.Collections;
using System.Data;
using System.Reflection;
using CaseyLib.util;
namespace CaseyLib
{

	public class CodeTableMapping
	{
		private CodeTableMapping()
		{
		}
       
		private static void init()
		{
			IDbConnection connOrTrans = null;
			try
			{
				string selectSql = "select code_table_id,data_column_name,get_data_type,find_data_sql from sys_code_table";
				connOrTrans = DBFunc.getConnection();
				_dtCodeTable = DBFunc.executeDataTable(connOrTrans, selectSql);
				_dtCodeTable.PrimaryKey = new DataColumn[] { _dtCodeTable.Columns["data_column_name"] };
				_hashDetail = new Hashtable();
				for (int i = 0; i < _dtCodeTable.Rows.Count; i++)
				{
					if (_dtCodeTable.Rows[i]["get_data_type"].ToString() == "0")
					{
						selectSql = "select data_value,display_value from sys_code_table_detail where code_table_id = {0}";
						selectSql = string.Format(selectSql, _dtCodeTable.Rows[i]["code_table_id"]);
					}
					else
					{
						selectSql = _dtCodeTable.Rows[i]["find_data_sql"].ToString();
					}
					DataTable table = DBFunc.executeDataTable(connOrTrans, selectSql);
					if (table.PrimaryKey.Length <= 1)
					{
						table.PrimaryKey = new DataColumn[] { table.Columns[0] };
					}
					_hashDetail[_dtCodeTable.Rows[i]["data_column_name"].ToString().ToLower()] = table;
				}
			}
			catch (Exception exception)
			{
				_dtCodeTable = null;
				_hashDetail = null;
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
       
		private static DataTable _dtCodeTable = null;
		private static Hashtable _hashDetail = null;
		private static CodeTableMapping _instance = new CodeTableMapping();
		private static string _version = "";
       
		public DataTable this[string strColumn]
		{
			get
			{
				strColumn = strColumn.ToLower();
				if (((_hashDetail == null) || !_hashDetail.ContainsKey(strColumn)) || (_hashDetail[strColumn] == null))
				{
					throw new Exception("没有找到数据列 " + strColumn + " 对应的码表");
				}
				return (DataTable) _hashDetail[strColumn];
			}
		}

		public void Refresh()
		{
			string str="1";
			//_instance=null;
			_hashDetail=null;
			_dtCodeTable=null;
			_version="";
			if (((_dtCodeTable == null) || (_hashDetail == null)) || (_version != str))
			{
				lock (_instance)
				{
					if (((_dtCodeTable == null) || (_hashDetail == null)) || (_version != str))
					{
						init();
						_version = str;
					}
				}
			}
		}
       
		public string getDisplayValue(int data, string strColumn)
		{
			DataTable table = this[strColumn];
			if (table != null)
			{
				DataRow row = table.Rows.Find(data);
				if (row != null)
				{
					return row[1].ToString();
				}
			}
			return data.ToString();
		}
       
		public static CodeTableMapping getInstance()
		{
//			string str = MappingVersion.Instance.getVersion(_instance.GetType());
			string str="1";
			if (((_dtCodeTable == null) || (_hashDetail == null)) || (_version != str))
			{
				lock (_instance)
				{
					if (((_dtCodeTable == null) || (_hashDetail == null)) || (_version != str))
					{
						init();
						_version = str;
					}
				}
			}
			return _instance;
		}
    
	}
	



}
