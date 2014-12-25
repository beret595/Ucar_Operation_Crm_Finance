using System;
using System.Collections;

namespace CaseyLib.Interface
{



	public interface IParameterObj
	{
		void destroy();
       
		string getActionCode();
       
		Hashtable getAllParameters();
       
		Hashtable getAllParamSet();
       
		object getParameter(string paramName);
       
		ArrayList getParamSet(string paramSetName);
       
		UserIndentity getUserIndentity();
       
		void init();
       
		void setFuncitionID(string funcId);
       
		void setParameter(string paramName, object param);
       
		void setParamSet(string paramSetName, ArrayList paramSet);
       
		void setUserIndentity(UserIndentity userIndentity);
    
	}




}
