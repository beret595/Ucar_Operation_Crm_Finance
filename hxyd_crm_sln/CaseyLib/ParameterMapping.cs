using System;
using System.Collections;
using System.Runtime.CompilerServices;
using CaseyLib.Interface;

namespace CaseyLib
{
 


	public class ParameterMapping : IMapping
	{
		private ParameterMapping()
		{
			this.init();
		}
       
		private static ParameterMapping _instance = null;
		private Hashtable paraMapping;
       
		public object get()
		{
			return this.paraMapping;
		}
       
		public object getByCode(string code)
		{
			if (this.paraMapping.ContainsKey(code))
			{
				return this.paraMapping[code];
			}
			return null;
		}
       
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static ParameterMapping getInstance()
		{
			if (_instance == null)
			{
				_instance = new ParameterMapping();
			}
			return _instance;
		}
       
		public void init()
		{
			this.paraMapping = new Hashtable();
		}
       
		public void refresh()
		{
		}
       
		public void set(string code, object name)
		{
			this.paraMapping.Add(code, name);
		}
    
	}




}
