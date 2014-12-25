using System;
using System.Collections;
using System.Runtime.CompilerServices;
using CaseyLib.Interface;
namespace CaseyLib
{


	public class MappingManager
	{
		private MappingManager()
		{
		}
       
		private static MappingManager _instance = null;
		private ArrayList mappings = new ArrayList();
       
		public void add(IMapping ins)
		{
			if (!this.mappings.Contains(ins))
			{
				this.mappings.Add(ins);
			}
		}
       
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static MappingManager getInstance()
		{
			if (_instance == null)
			{
				_instance = new MappingManager();
			}
			return _instance;
		}
       
		public void refresh()
		{
			for (int i = 0; i < this.mappings.Count; i++)
			{
				((IMapping) this.mappings[i]).refresh();
			}
		}
    
	}




}
