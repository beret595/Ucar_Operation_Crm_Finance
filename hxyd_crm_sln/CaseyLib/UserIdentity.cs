using System;
using System.Data;
namespace CaseyLib
{

		public class UserIndentity
		{
			private string _loginTime;
			private string _loginUser;
       
			public string LoginTime
			{
				get
				{
					return this._loginTime;
				}
				set
				{
					this._loginTime = value;
				}
			}
       
			public string LoginUser
			{
				get
				{
					return this._loginUser;
				}
				set
				{
					this._loginUser = value;
				}
			}
       
			public DataRow UserInfo
			{
				get
				{
					return StaffMapping.getInstance()[this._loginUser];
				}
			}
    
		}



}
