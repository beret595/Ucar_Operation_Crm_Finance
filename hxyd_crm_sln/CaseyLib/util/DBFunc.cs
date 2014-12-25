
using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace CaseyLib.util
{
		public class DBFunc
		{
			private DBFunc()
			{
			}
       
			private static IList createListFromDataReader(SqlDataReader reader)
			{
				IList list = new ArrayList();
				while (reader.Read())
				{
					Hashtable hashtable = new Hashtable();
					for (int i = 0; i < reader.FieldCount; i++)
					{
						if (reader.IsDBNull(i))
						{
							hashtable.Add(reader.GetName(i), "");
						}
						else
						{
							hashtable.Add(reader.GetName(i).ToLower(), reader.GetValue(i));
						}
					}
					list.Add(hashtable);
				}
				return list;
			}
       
			private const string rEGEX_PATTERN = @":([\s]?){0}\b";
       
			public static IDbDataParameter[] createParameters(Hashtable hash)
			{
				if ((hash == null) || (hash.Count == 0))
				{
					return null;
				}
				SqlParameter[] parameterArray = new SqlParameter[hash.Count];
				
				int index = 0;
				IDictionaryEnumerator enumerator = hash.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if ((enumerator.Value == null) || (enumerator.Value.ToString() == ""))
					{
						parameterArray[index] = new SqlParameter("@" + enumerator.Key.ToString(), DBNull.Value);
						
					}
					else
					{
						parameterArray[index] = new SqlParameter("@" + enumerator.Key.ToString(), enumerator.Value);
					}
					index++;
				}
				return parameterArray;
			}
       
			public static IDbDataParameter[] createParameters(DataRow row)
			{
				if (((row == null) || (row.Table == null)) || (row.Table.Columns.Count == 0))
				{
					return null;
				}
				DataTable table = row.Table;
				SqlParameter[] parameterArray = new SqlParameter[table.Columns.Count];
				int index = 0;
				foreach (DataColumn column in table.Columns)
				{
					if ((row[column] == null) || (row[column].ToString() == ""))
					{
						parameterArray[index] = new SqlParameter("@" + column.ColumnName, DBNull.Value);
					}
					else
					{
						parameterArray[index] = new SqlParameter("@" + column.ColumnName, row[column]);
					}
					index++;
				}
				return parameterArray;
			}
       
			public static IDbDataParameter[] createParameters(Hashtable hash, string strSql)
			{
				if ((hash == null) || (hash.Count == 0))
				{
					return null;
				}
				ArrayList list = new ArrayList();
				IDictionaryEnumerator enumerator = hash.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (Regex.IsMatch(strSql, string.Format(@"@([\s]?){0}\b", enumerator.Key), RegexOptions.IgnoreCase))
					{
						if ((enumerator.Value == null) || (enumerator.Value.ToString() == ""))
						{
							list.Add(new SqlParameter("@" + enumerator.Key.ToString(), DBNull.Value));
						}
						else
						{
							list.Add(new SqlParameter("@" + enumerator.Key.ToString(), enumerator.Value));
						}
					}
				}
				return (SqlParameter[]) list.ToArray(typeof(SqlParameter));
			}
       
			public static IDbDataParameter[] createParameters(DataRow row, string strSql)
			{
				if (((row == null) || (row.Table == null)) || (row.Table.Columns.Count == 0))
				{
					return null;
				}
				DataTable table = row.Table;
				ArrayList list = new ArrayList();
				foreach (DataColumn column in table.Columns)
				{
					if (!Regex.IsMatch(strSql, string.Format(@":([\s]?){0}\b", column.ColumnName), RegexOptions.IgnoreCase))
					{
						continue;
					}
					if ((row[column] == null) || (row[column].ToString() == ""))
					{
						list.Add(new SqlParameter("@" + column.ColumnName, DBNull.Value));
						continue;
					}
					list.Add(new SqlParameter("@" + column.ColumnName, row[column]));
				}
				return (SqlParameter[]) list.ToArray(typeof(SqlParameter));
			}
       
			public static DataSet executeDataSet(object connOrTrans, string selectSql)
			{
				if (connOrTrans is SqlConnection)
				{
					return SQLHelperCasey.ExecuteDataset((SqlConnection)connOrTrans, CommandType.Text, selectSql);
				}
				return SQLHelperCasey.ExecuteDataset((SqlTransaction) connOrTrans, CommandType.Text, selectSql);
			}
       
			public static DataSet executeDataSet(object connOrTrans, string selectSql, Hashtable hashParams)
			{
				IDbDataParameter[] commandParameters = createParameters(hashParams, selectSql);
				return executeDataSet(connOrTrans, selectSql, commandParameters);
			}
       
			public static DataSet executeDataSet(object connOrTrans, string selectSql, DataRow row)
			{
				IDbDataParameter[] commandParameters = createParameters(row, selectSql);
				return executeDataSet(connOrTrans, selectSql, commandParameters);
			}
       
			public static DataSet executeDataSet(object connOrTrans, string selectSql, IDbDataParameter[] commandParameters)
			{
				if (connOrTrans is SqlConnection)
				{
					return SQLHelperCasey.ExecuteDataset((SqlConnection) connOrTrans, CommandType.Text, selectSql, (SqlParameter[]) commandParameters);
				}
				return SQLHelperCasey.ExecuteDataset((SqlTransaction) connOrTrans, CommandType.Text, selectSql, (SqlParameter[]) commandParameters);
			}
       
			public static DataTable executeDataTable(object connOrTrans, string selectSql)
			{
				DataSet set;
				if (connOrTrans is SqlConnection)
				{
					set = SQLHelperCasey.ExecuteDataset((SqlConnection) connOrTrans, CommandType.Text, selectSql);
				}
				else
				{
					set = SQLHelperCasey.ExecuteDataset((SqlTransaction) connOrTrans, CommandType.Text, selectSql);
				}
				if ((set != null) && (set.Tables.Count > 0))
				{
					return set.Tables[0];
				}
				return null;
			}
       
			public static DataTable executeDataTable(object connOrTrans, string selectSql, IDbDataParameter[] commandParameters)
			{
				DataSet set;
				if (connOrTrans is SqlConnection)
				{
					set = SQLHelperCasey.ExecuteDataset((SqlConnection) connOrTrans, CommandType.Text, selectSql, (SqlParameter[]) commandParameters);
				}
				else
				{
					set = SQLHelperCasey.ExecuteDataset((SqlConnection) connOrTrans, CommandType.Text, selectSql, (SqlParameter[]) commandParameters);
				}
				if ((set != null) && (set.Tables.Count > 0))
				{
					return set.Tables[0];
				}
				return null;
			}
       
			public static DataTable executeDataTable(object connOrTrans, string selectSql, Hashtable hashParams)
			{
				IDbDataParameter[] commandParameters = createParameters(hashParams, selectSql);
				return executeDataTable(connOrTrans, selectSql, commandParameters);
			}
       
			public static DataTable executeDataTable(object connOrTrans, string selectSql, DataRow row)
			{
				IDbDataParameter[] commandParameters = createParameters(row, selectSql);
				return executeDataTable(connOrTrans, selectSql, commandParameters);
			}
       
			public static IList executeList(object connOrTrans, string selectSql)
			{
				if (connOrTrans is SqlConnection)
				{
					using (SqlDataReader reader = SQLHelperCasey.ExecuteReader((SqlConnection) connOrTrans, CommandType.Text, selectSql))
					{
						return createListFromDataReader(reader);
					}
				}
				using (SqlDataReader reader2 = SQLHelperCasey.ExecuteReader((SqlTransaction) connOrTrans, CommandType.Text, selectSql))
				{
					return createListFromDataReader(reader2);
				}
			}
       
			public static IList executeList(object connOrTrans, string selectSql, Hashtable hashParams)
			{
				IDbDataParameter[] commandParameters = createParameters(hashParams, selectSql);
				return executeList(connOrTrans, selectSql, commandParameters);
			}
       
			public static IList executeList(object connOrTrans, string selectSql, DataRow row)
			{
				IDbDataParameter[] commandParameters = createParameters(row, selectSql);
				return executeList(connOrTrans, selectSql, commandParameters);
			}
       
			public static IList executeList(object connOrTrans, string selectSql, IDbDataParameter[] commandParameters)
			{
				if (connOrTrans is SqlConnection)
				{
					using (SqlDataReader reader = SQLHelperCasey.ExecuteReader((SqlConnection) connOrTrans, CommandType.Text, selectSql, (SqlParameter[]) commandParameters))
					{
						return createListFromDataReader(reader);
					}
				}
				using (SqlDataReader reader2 = SQLHelperCasey.ExecuteReader((SqlTransaction) connOrTrans, CommandType.Text, selectSql, (SqlParameter[]) commandParameters))
				{
					return createListFromDataReader(reader2);
				}
			}
       
			public static int executeNonQuery(object connOrTrans, string sql)
			{
				if (connOrTrans is SqlConnection)
				{
					return SQLHelperCasey.ExecuteNonQuery((SqlConnection) connOrTrans, CommandType.Text, sql);
				}
				return SQLHelperCasey.ExecuteNonQuery((SqlTransaction) connOrTrans, CommandType.Text, sql);
			}
       
			public static int executeNonQuery(object connOrTrans, string sql, params IDbDataParameter[] commandParameters)
			{
				if (connOrTrans is SqlConnection)
				{
					return SQLHelperCasey.ExecuteNonQuery((SqlConnection) connOrTrans, CommandType.Text, sql, (SqlParameter[]) commandParameters);
				}
				return SQLHelperCasey.ExecuteNonQuery((SqlTransaction) connOrTrans, CommandType.Text, sql, (SqlParameter[]) commandParameters);
			}
       
			public static int executeNonQuery(object connOrTrans, string sql, Hashtable hashParams)
			{
				IDbDataParameter[] commandParameters = createParameters(hashParams, sql);
				return executeNonQuery(connOrTrans, sql, commandParameters);
			}
       
			public static int executeNonQuery(object connOrTrans, string sql, DataRow row)
			{
				IDbDataParameter[] commandParameters = createParameters(row, sql);
				return executeNonQuery(connOrTrans, sql, commandParameters);
			}
       
			public static IDataReader executeReader(object connOrTrans, string selectSql)
			{
				if (connOrTrans is SqlConnection)
				{
					return SQLHelperCasey.ExecuteReader((SqlConnection) connOrTrans, CommandType.Text, selectSql);
				}
				return SQLHelperCasey.ExecuteReader((SqlTransaction) connOrTrans, CommandType.Text, selectSql);
			}
       
			public static IDataReader executeReader(object connOrTrans, string selectSql, Hashtable hashParams)
			{
				IDbDataParameter[] commandParameters = createParameters(hashParams, selectSql);
				return executeReader(connOrTrans, selectSql, commandParameters);
			}
       
			public static IDataReader executeReader(object connOrTrans, string selectSql, params IDbDataParameter[] commandParameters)
			{
				if (connOrTrans is SqlConnection)
				{
					return SQLHelperCasey.ExecuteReader((SqlConnection) connOrTrans, CommandType.Text, selectSql, (SqlParameter[]) commandParameters);
				}
				return SQLHelperCasey.ExecuteReader((SqlTransaction) connOrTrans, CommandType.Text, selectSql, (SqlParameter[]) commandParameters);
			}
       
			public static IDataReader executeReader(object connOrTrans, string selectSql, DataRow row)
			{
				IDbDataParameter[] commandParameters = createParameters(row, selectSql);
				return executeReader(connOrTrans, selectSql, commandParameters);
			}
       
			public static object executeScalar(object connOrTrans, string selectSql)
			{
				if (connOrTrans is SqlConnection)
				{
					return SQLHelperCasey.ExecuteScalar((SqlConnection) connOrTrans, CommandType.Text, selectSql);
				}
				return SQLHelperCasey.ExecuteScalar((SqlTransaction) connOrTrans, CommandType.Text, selectSql);
			}
       
			public static object executeScalar(object connOrTrans, string selectSql, IDbDataParameter[] commandParameters)
			{
				if (connOrTrans is SqlConnection)
				{
					return SQLHelperCasey.ExecuteScalar((SqlConnection) connOrTrans, CommandType.Text, selectSql, (SqlParameter[]) commandParameters);
				}
				return SQLHelperCasey.ExecuteScalar((SqlTransaction) connOrTrans, CommandType.Text, selectSql, (SqlParameter[]) commandParameters);
			}
       
			public static object executeScalar(object connOrTrans, string selectSql, Hashtable hashParams)
			{
				IDbDataParameter[] commandParameters = createParameters(hashParams, selectSql);
				return executeScalar(connOrTrans, selectSql, commandParameters);
			}
       
			public static object executeScalar(object connOrTrans, string selectSql, DataRow row)
			{
				IDbDataParameter[] commandParameters = createParameters(row, selectSql);
				return executeScalar(connOrTrans, selectSql, commandParameters);
			}
       
			public static IDbConnection getConnection()
			{
//				IDbConnection connection = new SqlConnection();
//				connection.ConnectionString = (string) ParameterMapping.getInstance().getByCode("connectionstring");
//				connection.Open();
				
//				return connection;
				return SQLHelperCasey.GetConnection();
			}

			public static IDbConnection getConnection2()
			{
				return SQLHelperCasey.GetConnection2();
			}
    
		}




}
