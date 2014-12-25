using System;

namespace CaseyLib.util
{

		public class DateFunc
		{
			private DateFunc()
			{
			}
       
			public static bool isDate(string strDate)
			{
				try
				{
					DateTime time = DateTime.Parse(strDate);
					return true;
				}
				catch (Exception)
				{
					return false;
				}
			}
       
			public static string showDate(string strDate)
			{
				return ("to_char(" + strDate + ",'yyyy-mm-dd')");
			}
       
			public static string showDate(string strDate, string format)
			{
				return ("to_char(" + strDate + ",'" + format + "')");
			}
       
			public static string transDate(string strDate)
			{
				return ("to_date('" + strDate + "','yyyy-mm-dd')");
			}
       
			public static string transDate(string strDate, string format)
			{
				return ("to_date('" + strDate + "','" + format + "')");
			}
       
			public static string yearOLD(string birthDay)
			{
				return ("floor(months_between(SYSDATE," + birthDay + ")/12)");
			}
    
		}



}
