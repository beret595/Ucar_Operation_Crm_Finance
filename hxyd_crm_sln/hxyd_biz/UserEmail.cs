using System;
using System.Text;
using System.Collections;
using CaseyLib;
using CaseyLib.util;
using System.Data;

namespace hxyd_biz
{
	/// <summary>
	/// UserEmail 的摘要说明。
	/// </summary>
	public class UserEmail
	{
		public UserEmail()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		//获取管理员的邮箱地址
		public static DataTable GetEmail_Data_ForAdmin()
		{
			DataTable dt = new DataTable();
			StringBuilder lSQL = new StringBuilder();
			lSQL.Append(" select email from dbo.hdb_user where role = 'admin' ");

			using (IDbConnection con=DBFunc.getConnection())
			{
				dt =DBFunc.executeDataTable(con,lSQL.ToString());

			}
			return dt;

		}
		//查询需要发送邮件的人
		public static DataTable GetEmail_Data_ForPerson()
		{
			DataTable dt = new DataTable();
			StringBuilder lSQL = new StringBuilder();
			lSQL.Append(" select distinct ue.email,ue.userId,ueto.email as to_email");
			lSQL.Append(" from dbo.Car_info ci,brandinfo bi,hdb_caseInfo hc,user_assign ua,dbo.hdb_user ue, dbo.hdb_user ueto");
			lSQL.Append(" where ci.manufacturers = brandNameCN ");
			lSQL.Append("   and ci.personId = hc.id ");
			lSQL.Append("   and ua.car_id = ci.id ");
			lSQL.Append("   and ua.personId = hc.id ");
			lSQL.Append("   and ua.userId = ue.userId ");
			lSQL.Append("   and ua.plan_id = ueto.userId ");
			lSQL.Append("   and ua.assign_role='已分配' ");
			lSQL.Append("   and DATEDIFF(hour,assign_data,GETDATE()) >= 12");
			using (IDbConnection con=DBFunc.getConnection())
			{
				dt =DBFunc.executeDataTable(con,lSQL.ToString());
			}
			return dt;
		}

		//查询需要发送邮件的任务
		public static DataTable GetEmail_Data(string userId)
		{
			DataTable dt = new DataTable();
			StringBuilder lSQL = new StringBuilder();
			lSQL.Append(" select ci.personId,ci.licensePlate, ");
			lSQL.Append("     ci.id as car_id, ");
			lSQL.Append("     kehu_no as kuhu_no, ");
			lSQL.Append("     hc.id, ");
			lSQL.Append("     hc.personName, ");
			lSQL.Append("     brandNameCN,brandNameEN,mileage, ");
			lSQL.Append("     keep_date,ci.average_mileage,     ");
			lSQL.Append("     datediff(day,ci.keep_date,GETDATE()), ");
			lSQL.Append("     ci.salesdate, ");
			lSQL.Append("     ua.assign_type as assign_type, ");
			lSQL.Append("     ua.assign_role,");
			lSQL.Append("     ue.email,");
			lSQL.Append("     ueto.email, ");
			lSQL.Append("     ue.fullName as username ");
			lSQL.Append(" from dbo.Car_info ci,brandinfo bi,hdb_caseInfo hc,user_assign ua,dbo.hdb_user ue, dbo.hdb_user ueto");
			lSQL.Append(" where ci.manufacturers = brandNameCN ");
			lSQL.Append("   and ci.personId = hc.id ");
			lSQL.Append("   and ua.car_id = ci.id ");
			lSQL.Append("   and ua.personId = hc.id ");
			lSQL.Append("   and ua.userId = ue.userId ");
			lSQL.Append("   and ua.plan_id = ueto.userId ");
			lSQL.Append("   and ua.assign_role='已分配' ");
			lSQL.Append("   and ue.userId = "+userId+"");
			lSQL.Append("   and DATEDIFF(hour,assign_data,GETDATE()) >= 12");
			using (IDbConnection con=DBFunc.getConnection())
			{
				dt =DBFunc.executeDataTable(con,lSQL.ToString());
			}
			return dt;
		}



	}
}
