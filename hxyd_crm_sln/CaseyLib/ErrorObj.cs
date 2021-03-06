using System;
using System.Text;

namespace CaseyLib
{


	public class ErrorObj
	{
		private string _errMsg;
		private int _errType = 0;
		private string _expMsg;
		private string _retCode = "1";
		private const string eRROR_STRING = "错误号:{0}\r\n错误类型:{1}\r\n错误信息:{2}\r\n异常信息:{3}";
       
		public string ErrMsg
		{
			get
			{
				return this._errMsg;
			}
			set
			{
				this._errMsg = value;
			}
		}
       
		public int ErrType
		{
			get
			{
				return this._errType;
			}
			set
			{
				this._errType = value;
			}
		}
       
		public string ExpMsg
		{
			get
			{
				return this._expMsg;
			}
			set
			{
				this._expMsg = value;
			}
		}
       
		public string RetCode
		{
			get
			{
				return this._retCode;
			}
			set
			{
				this._retCode = value;
			}
		}
       
		public void setException(Exception e)
		{
			this._expMsg = e.ToString();
		}
       
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat("错误号:{0}\r\n错误类型:{1}\r\n错误信息:{2}\r\n异常信息:{3}", new object[] { this._retCode, this._errType, this._errMsg, this._expMsg });
			return builder.ToString();
		}
    
	}



}
