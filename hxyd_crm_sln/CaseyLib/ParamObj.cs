using System;
using System.Collections;
using System.Text;
using CaseyLib.Interface;

namespace CaseyLib
{



	public class ParameterObj : IParameterObj
	{
		private UserIndentity _userIndentity;
		private string funcId;
		private Hashtable parameterMap;
		private Hashtable paramSetMap;
       
		public ParameterObj()
		{
			this.init();
		}
       
		public void destroy()
		{
			this.parameterMap.Clear();
			this.paramSetMap.Clear();
			this.funcId = null;
			this.parameterMap = null;
			this.paramSetMap = null;
		}
       
		public string getActionCode()
		{
			return this.funcId;
		}
       
		public Hashtable getAllParameters()
		{
			return this.parameterMap;
		}
       
		public Hashtable getAllParamSet()
		{
			return this.paramSetMap;
		}
       
		public object getParameter(string paramName)
		{
			return this.parameterMap[paramName];
		}
       
		public ArrayList getParamSet(string paramSetName)
		{
			return (ArrayList) this.paramSetMap[paramSetName];
		}
       
		public UserIndentity getUserIndentity()
		{
			return this._userIndentity;
		}
       
		public void init()
		{
			this.parameterMap = new Hashtable();
			this.paramSetMap = new Hashtable();
		}
       
		public void setFuncitionID(string funcId)
		{
			this.funcId = funcId;
		}
       
		public void setParameter(string paramName, object param)
		{
			this.parameterMap.Add(paramName, param);
		}
       
		public void setParamSet(string paramSetName, ArrayList paramSet)
		{
			this.paramSetMap.Add(paramSetName, paramSet);
		}
       
		public void setUserIndentity(UserIndentity userIndentity)
		{
			this._userIndentity = userIndentity;
		}
       
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("FunctionId:");
			builder.Append(this.funcId);
			builder.Append("\n");
			builder.Append("------ Parameters ------");
			builder.Append("\n");
			builder.Append(this.parameterMap.ToString());
			builder.Append("\n");
			builder.Append("------ ParameterSets ------");
			builder.Append("\n");
			builder.Append(this.paramSetMap.ToString());
			return builder.ToString();
		}
    
	}


}
