using System;

namespace CaseyLib.Interface
{


	public interface IMapping
	{
		object get();
       
		object getByCode(string code);
       
		void init();
       
		void refresh();
       
		void set(string code, object name);
    
	}




}
