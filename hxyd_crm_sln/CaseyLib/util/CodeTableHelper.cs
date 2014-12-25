	using System;
using System.Data;


	namespace CaseyLib.util
	{


		public class CodeTableHelper
		{
			private CodeTableHelper()
			{
			}
       
			public static DataTable getCodeTable(string ColumnName)
			{
				return CodeTableMapping.getInstance()[ColumnName];
				
			}
       
			public static string getDisplayValue(int data, string ColumnName)
			{
				return CodeTableMapping.getInstance().getDisplayValue(data, ColumnName);
			}
			public static void Refresh()
			{
				CodeTableMapping.getInstance().Refresh();
			}
    
		}

	}
