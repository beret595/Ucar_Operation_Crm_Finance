using System;
using System.Collections;
using System.Data;

namespace CaseyLib.Interface
{



	public interface IResultObj
	{
		string DEFAULT_RESULTSET { get; }
		ErrorObj error { get; set; }
		string FuncID { get; set; }
       
		void destroy();
       
		Hashtable getResult();
       
		object getResult(string rstName);
       
		Hashtable getResultSet();
       
		DataTable getResultSet(string rstName);
       
		void init();
       
		void setResult(string rstName, object result);
       
		void setResultSet(string rstName, DataTable resultSet);
    
	}



}
