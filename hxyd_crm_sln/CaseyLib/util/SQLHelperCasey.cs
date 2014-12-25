using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Configuration;

namespace CaseyLib.util
{
	/// <summary>
	/// SQLHelperCasey 的摘要说明。
	/// </summary>
	public class SQLHelperCasey
	{
		public SQLHelperCasey()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		public static string connectionString = System.Configuration.ConfigurationSettings.AppSettings["connectionstring"].ToString().Trim();
		//public static string connectionString = System.Configuration.ConfigurationSettings.GetConfig("connectionstring").ToString();

		public static string connectionString2 = System.Configuration.ConfigurationSettings.AppSettings["connectionstring1"].ToString().Trim();
		
		public static SqlConnection GetConnection()
		{
			SqlConnection con=new SqlConnection(connectionString);
			con.Open();
			return con;
		}

		public static SqlConnection GetConnection2()
		{
			SqlConnection con=new SqlConnection(connectionString2);
			con.Open();
			return con;
		}
       
		private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			if (commandParameters != null)
			{
				foreach (SqlParameter parameter in commandParameters)
				{
					if (parameter != null)
					{
						if (((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) && (parameter.Value == null))
						{
							parameter.Value = DBNull.Value;
						}
						command.Parameters.Add(parameter);
					}
				}
			}
		}
       
		private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, SQLHelperCasey.SqlConnectionOwnership connectionOwnership)
		{
			SqlDataReader reader2;
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			bool mustCloseConnection = false;
			SqlCommand command = new SqlCommand();
			try
			{
				SqlDataReader reader;
				PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
				if (connectionOwnership == SqlConnectionOwnership.External)
				{
					reader = command.ExecuteReader();
				}
				else
				{
					reader = command.ExecuteReader(CommandBehavior.CloseConnection);
				}
				bool flag2 = true;
				foreach (SqlParameter parameter in command.Parameters)
				{
					if (parameter.Direction != ParameterDirection.Input)
					{
						flag2 = false;
					}
				}
				if (flag2)
				{
					command.Parameters.Clear();
				}
				reader2 = reader;
			}
			catch
			{
				if (mustCloseConnection)
				{
					connection.Close();
				}
				throw;
			}
			return reader2;
		}
       
		private static void FillDataset(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (dataSet == null)
			{
				throw new ArgumentNullException("dataSet");
			}
			SqlCommand command = new SqlCommand();
			bool mustCloseConnection = false;
			PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
			using (SqlDataAdapter adapter = new SqlDataAdapter(command))
			{
				if ((tableNames != null) && (tableNames.Length > 0))
				{
					string sourceTable = "Table";
					for (int i = 0; i < tableNames.Length; i++)
					{
						if ((tableNames[i] == null) || (tableNames[i].Length == 0))
						{
							throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
						}
						adapter.TableMappings.Add(sourceTable, tableNames[i]);
						sourceTable = sourceTable + ((i + 1)).ToString();
					}
				}
				adapter.Fill(dataSet);
				command.Parameters.Clear();
			}
			if (mustCloseConnection)
			{
				connection.Close();
			}
		}
       
		private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			if ((commandText == null) || (commandText.Length == 0))
			{
				throw new ArgumentNullException("commandText");
			}
			if (connection.State != ConnectionState.Open)
			{
				mustCloseConnection = true;
				connection.Open();
			}
			else
			{
				mustCloseConnection = false;
			}
			command.Connection = connection;
			command.CommandText = commandText;
			if (transaction != null)
			{
				if (transaction.Connection == null)
				{
					throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
				}
				command.Transaction = transaction;
			}
			command.CommandType = commandType;
			if (commandParameters != null)
			{
				AttachParameters(command, commandParameters);
			}
		}
       
		public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText)
		{
			return ExecuteDataset(connection, commandType, commandText, null);
		}
       
		public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText)
		{
			return ExecuteDataset(transaction, commandType, commandText, null);
		}
       
		public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			SqlCommand command = new SqlCommand();
			bool mustCloseConnection = false;
			PrepareCommand(command, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
			using (SqlDataAdapter adapter = new SqlDataAdapter(command))
			{
				DataSet dataSet = new DataSet();
				adapter.Fill(dataSet);
				command.Parameters.Clear();
				if (mustCloseConnection)
				{
					connection.Close();
				}
				return dataSet;
			}
		}
       
		public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if ((transaction != null) && (transaction.Connection == null))
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			SqlCommand command = new SqlCommand();
			bool mustCloseConnection = false;
			PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
			using (SqlDataAdapter adapter = new SqlDataAdapter(command))
			{
				DataSet dataSet = new DataSet();
				adapter.Fill(dataSet);
				command.Parameters.Clear();
				return dataSet;
			}
		}
       
		public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText)
		{
			return ExecuteNonQuery(connection, commandType, commandText, null);
		}
       
		public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText)
		{
			return ExecuteNonQuery(transaction, commandType, commandText, null);
		}
       
		public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			SqlCommand command = new SqlCommand();
			bool mustCloseConnection = false;
			PrepareCommand(command, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
			int num = command.ExecuteNonQuery();
			command.Parameters.Clear();
			if (mustCloseConnection)
			{
				connection.Close();
			}
			return num;
		}
       
		public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if ((transaction != null) && (transaction.Connection == null))
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			SqlCommand command = new SqlCommand();
			bool mustCloseConnection = false;
			PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
			int num = command.ExecuteNonQuery();
			command.Parameters.Clear();
			return num;
		}
       
		public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText)
		{
			return ExecuteReader(connection, commandType, commandText, null);
		}
       
		public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText)
		{
			return ExecuteReader(transaction, commandType, commandText, null);
		}
       
		public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			return ExecuteReader(connection, null, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
		}
       
		public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if ((transaction != null) && (transaction.Connection == null))
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
		}
       
		public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText)
		{
			return ExecuteScalar(connection, commandType, commandText, null);
		}
       
		public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText)
		{
			return ExecuteScalar(transaction, commandType, commandText, null);
		}
       
		public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			SqlCommand command = new SqlCommand();
			bool mustCloseConnection = false;
			PrepareCommand(command, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
			object obj2 = command.ExecuteScalar();
			command.Parameters.Clear();
			if (mustCloseConnection)
			{
				connection.Close();
			}
			return obj2;
		}
       
		public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if ((transaction != null) && (transaction.Connection == null))
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			SqlCommand command = new SqlCommand();
			bool mustCloseConnection = false;
			PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
			object obj2 = command.ExecuteScalar();
			command.Parameters.Clear();
			return obj2;
		}
       
		public static void FillDataset(SqlConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
		{
			FillDataset(connection, commandType, commandText, dataSet, tableNames, null);
		}
       
		public static void FillDataset(SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
		{
			FillDataset(transaction, commandType, commandText, dataSet, tableNames, null);
		}
       
		public static void FillDataset(SqlConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
		{
			FillDataset(connection, null, commandType, commandText, dataSet, tableNames, commandParameters);
		}
       
		public static void FillDataset(SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
		{
			FillDataset(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames, commandParameters);
		}
    
		private enum SqlConnectionOwnership
		{
			Internal,
			External
		}
    
	}




}
