using System;

namespace hxyd_biz
{
	/// <summary>
	/// UserAssignerBean ��ժҪ˵����
	/// </summary>
	public class UserAssignerBean
	{
		public UserAssignerBean()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
