using System;

namespace hxyd_biz
{
	/// <summary>
	/// UserAssignerBean 的摘要说明。
	/// </summary>
	public class UserAssignerBean
	{
		public UserAssignerBean()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		private int kehu_no;
		private string assign_type;
		private int personId;
		private int car_id;
		private string assign_date;
		private int user_id;

		public int Get_kehu_no()
		{
			return kehu_no;
		}
		public void Set_kehu_no(int kehu_no)
		{
			this.kehu_no = kehu_no;
		}


	}
}
