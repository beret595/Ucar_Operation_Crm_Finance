using System;
using System.Collections;
using System.Data;
using System.Text;
using CaseyLib.Interface;

namespace CaseyLib
{



	public class ResultObj : IResultObj
	{
		private ErrorObj _errorObj = null;
		private ParameterObj _paramObj;
		private const string dEFAULT_RESULTSET_NAME = "_default_resultset";
		private string funcId;
		private Hashtable retResult;
		private Hashtable retResultSet;
       
		internal ParameterObj ParamObj
		{
			get
			{
				return this._paramObj;
			}
			set
			{
				this._paramObj = value;
			}
		}
       
		public string DEFAULT_RESULTSET
		{
			get
			{
				return "_default_resultset";
			}
		}
       
		public ErrorObj error
		{
			get
			{
				if (this._errorObj == null)
				{
					this._errorObj = new ErrorObj();
				}
				return this._errorObj;
			}
			set
			{
				this._errorObj = value;
			}
		}
       
		public string FuncID
		{
			get
			{
				return this.funcId;
			}
			set
			{
				this.funcId = value;
			}
		}
       
		public ResultObj()
		{
			this.init();
		}
       
		public void destroy()
		{
			this.funcId = null;
			this._errorObj = null;
			this.retResult.Clear();
			this.retResult = null;
			this.retResultSet.Clear();
			this.retResultSet = null;
		}
       
		public Hashtable getResult()
		{
			return this.retResult;
		}
       
		public object getResult(string rstName)
		{
			return this.retResult[rstName];
		}
       
		public Hashtable getResultSet()
		{
			return this.retResultSet;
		}
       
		public DataTable  getResultSet(string rstName)
		{
			return (DataTable) this.retResultSet[rstName];
		}
       
		public void init()
		{
			this.retResult = new Hashtable();
			this.retResultSet = new Hashtable();
		}
       
		public void setResult(string rstName, object result)
		{
			this.retResult.Add(rstName.ToLower(), result);
		}
       
		public void setResultSet(string rstName, DataTable resultSet)
		{
			this.retResultSet.Add(rstName.ToLower(), resultSet);
		}
       
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("FunctionId:");
			builder.Append(this.funcId);
			builder.Append("\r\n");
			builder.Append("--- Begin Result List ---");
			builder.Append("\r\n");
			foreach (DictionaryEntry entry in this.retResult)
			{
				builder.Append(entry.Key);
				builder.Append(" : ");
				builder.Append(entry.Value);
				builder.Append("\r\n");
			}
			builder.Append("--- End Result List ---");
			builder.Append("\r\n");
			builder.Append("--- Begin ResultSet List ---");
			foreach (DictionaryEntry entry2 in this.retResultSet)
			{
				builder.Append(entry2.Key);
			}
			return builder.ToString();
		}
    
	}




}
