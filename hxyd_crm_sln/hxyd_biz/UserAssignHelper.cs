using System;
using System.Text;
using System.Collections;
using CaseyLib;
using CaseyLib.util;
using System.Data;

namespace hxyd_biz
{
	/// <summary>
	/// UserAssignHelper 的摘要说明。
	/// </summary>
	public class UserAssignHelper
	{
		public UserAssignHelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}		

		//查询需要发送邮件的任务

		//更新业务库的人员信息
		public int update_Indi_Other()
		{
			int num = 1;
			StringBuilder lSQL = new StringBuilder();
			//获得需要变更的项目
			lSQL.Append("   select Column_correspondence_hdbcaseinfo,Column_correspondence_kehu_bossed from dbo.Correspondence_name_customers");
			//hdb库的连接
			IDbConnection Con_hdb = DBFunc.getConnection();
			//boss库的连接
			IDbConnection Con_boss = DBFunc.getConnection2();
			//trans
			IDbTransaction trans_hdb=Con_hdb.BeginTransaction();

			try
			{
				DataTable dt_config = new DataTable();
				dt_config = DBFunc.executeDataTable(trans_hdb,lSQL.ToString());
				//获取BOSS库中的人员信息
				lSQL = new StringBuilder();
				lSQL.Append("  select ");
				for(int i = 0; i<dt_config.Rows.Count; i++)
				{
					if(i == dt_config.Rows.Count-1)
					{
						lSQL.Append(dt_config.Rows[i]["Column_correspondence_kehu_bossed"].ToString());
					}
					else
					{
						lSQL.Append(dt_config.Rows[i]["Column_correspondence_kehu_bossed"].ToString()+",");
					}
				}
				lSQL.Append(" from kehu ");
				DataTable dt_user_Boss = new DataTable();
				dt_user_Boss = DBFunc.executeDataTable(Con_boss,lSQL.ToString());
				//匹配姓名
				for(int j = 0; j<dt_user_Boss.Rows.Count; j++)
				{
					String name = dt_user_Boss.Rows[j]["kehu_mc"].ToString();
					lSQL = new StringBuilder();
					lSQL.Append("   select * from hdb_caseInfo where personName = '"+name+"'");
					DataTable dt_uers_hdb = new DataTable();
					dt_uers_hdb = DBFunc.executeDataTable(trans_hdb,lSQL.ToString());
					if(dt_uers_hdb.Rows.Count == 1)
					{
						//更新 
						lSQL = new StringBuilder();
						lSQL.Append("  update hdb_caseInfo set ");
						for(int k = 0; k< dt_config.Rows.Count; k++)
						{
							if(dt_config.Rows[k]["Column_correspondence_hdbcaseinfo"].ToString() == "personName")
							{
							}
							else
							{
								if(k == dt_config.Rows.Count-1)
								{
									lSQL.Append(dt_config.Rows[k]["Column_correspondence_hdbcaseinfo"].ToString()+"= '"+dt_user_Boss.Rows[j][dt_config.Rows[k]["Column_correspondence_kehu_bossed"].ToString()].ToString()+"'");
								}
								else
								{
									lSQL.Append(dt_config.Rows[k]["Column_correspondence_hdbcaseinfo"].ToString()+"= '"+dt_user_Boss.Rows[j][dt_config.Rows[k]["Column_correspondence_kehu_bossed"].ToString()].ToString()+"',");
								}
							}
						}

						lSQL.Append("   where personName ='"+name+"' ");					
						DBFunc.executeNonQuery(trans_hdb,lSQL.ToString());
						num++;
					
					}
					else
					{
						if(dt_uers_hdb.Rows.Count <=0 )
						{
							//新增
							lSQL = new StringBuilder();
							lSQL.Append(" insert into hdb_caseInfo (");
							for(int k = 0; k< dt_config.Rows.Count; k++)
							{
						
								if(k == dt_config.Rows.Count-1)
								{
									lSQL.Append(dt_config.Rows[k]["Column_correspondence_hdbcaseinfo"].ToString());
								}
								else
								{
									lSQL.Append(dt_config.Rows[k]["Column_correspondence_hdbcaseinfo"].ToString()+",");
								}						
							}
							lSQL.Append(") values (");
							for(int k = 0; k< dt_config.Rows.Count; k++)
							{
								if(dt_config.Rows[k]["Column_correspondence_hdbcaseinfo"].ToString() == "personName")
								{
									lSQL.Append("'"+name+"',");
								}
								else
								{
									if(k == dt_config.Rows.Count-1)
									{
										lSQL.Append("'"+dt_user_Boss.Rows[j][dt_config.Rows[k]["Column_correspondence_kehu_bossed"].ToString()].ToString()+"'");
									}
									else
									{
										lSQL.Append("'"+dt_user_Boss.Rows[j][dt_config.Rows[k]["Column_correspondence_kehu_bossed"].ToString()].ToString()+"',");
									}
								}
							}

							lSQL.Append(")");
							DBFunc.executeNonQuery(trans_hdb,lSQL.ToString());
							num++;
						}
					}
				}
				trans_hdb.Commit();
			}
			catch(Exception ex)
			{
				trans_hdb.Rollback();
				throw new Exception("更新车辆数据出错");
			}
			finally
			{
				Con_hdb.Close();
				Con_hdb.Dispose();
				Con_boss.Close();
				Con_boss.Dispose();
			}

			return num;
	}


		//获取业务库的车辆信息信息
		public int Update_Car_Other()
		{
			int num = 1;
			//获得需要变更的项目 
			StringBuilder lSQL = new StringBuilder();
			lSQL.Append(" select Column_correspondence_carinfo,Column_correspondence_cheliang_bossed from dbo.Correspondence_name_car  ");
			IDbConnection Con=DBFunc.getConnection();
			IDbConnection Con1=DBFunc.getConnection2();

			IDbTransaction trans=Con.BeginTransaction();
			try
			{
				DataTable dt_config = new DataTable();
				dt_config = DBFunc.executeDataTable(trans,lSQL.ToString());
				//获取Car库的数据
				lSQL = new StringBuilder();
				lSQL.Append(" select ");
				for(int i = 0; i<dt_config.Rows.Count; i++)
				{
					if(i == dt_config.Rows.Count-1)
					{
						lSQL.Append(dt_config.Rows[i]["Column_correspondence_cheliang_bossed"].ToString());
					}
					else
					{
						lSQL.Append(dt_config.Rows[i]["Column_correspondence_cheliang_bossed"].ToString()+",");
					}
				}
				lSQL.Append("  from work_cheliang_sm ");
				DataTable dt_user_car = new DataTable();
				dt_user_car = DBFunc.executeDataTable(Con1,lSQL.ToString());
				for(int j= 0; j< dt_user_car.Rows.Count; j++)
				{
					string licensePlate = dt_user_car.Rows[j]["che_no"].ToString();			// 车牌号
					Hashtable ht = new Hashtable();
					ht["licensePlate"] = licensePlate;
					lSQL = new StringBuilder();
					lSQL.Append("  select * from Car_info where licenseplate = '"+licensePlate+"' ");
					DataTable dt_cheliang = new DataTable();
					dt_cheliang = DBFunc.executeDataTable(trans,lSQL.ToString());
					//如果是新增
					if(dt_cheliang.Rows.Count <= 0)
					{
						lSQL = new StringBuilder();
						lSQL.Append("  select * from kehu where kehu_no = '"+dt_user_car.Rows[j]["kehu_no"].ToString()+"' ");  //客户姓名
						String uers_name = "";
						uers_name =DBFunc.executeDataTable(Con1,lSQL.ToString()).Rows[0]["kehu_mc"].ToString(); 
						String user_id = "";
						lSQL = new StringBuilder();
						lSQL.Append("  select * from hdb_caseInfo where personName = '"+uers_name+"' ");
						user_id = DBFunc.executeDataTable(trans,lSQL.ToString()).Rows[0]["id"].ToString(); ;
					
						lSQL = new StringBuilder();
						lSQL.Append(" insert into Car_info (");
						for(int k = 0; k< dt_config.Rows.Count; k++)
						{
						
							if(k == dt_config.Rows.Count-1)
							{
								lSQL.Append(dt_config.Rows[k]["Column_correspondence_carinfo"].ToString());
							}
							else
							{
								lSQL.Append(dt_config.Rows[k]["Column_correspondence_carinfo"].ToString()+",");
							}						
						}
						lSQL.Append(") values (");
						for(int k = 0; k< dt_config.Rows.Count; k++)
						{
							if(dt_config.Rows[k]["Column_correspondence_carinfo"].ToString() == "personId")
							{
								lSQL.Append(user_id+",");
							}
							else
							{
								if(k == dt_config.Rows.Count-1)
								{
									if(dt_user_car.Rows[j][dt_config.Rows[k]["Column_correspondence_cheliang_bossed"].ToString()].ToString() =="")
									{
										lSQL.Append(" null ");
									}
									else
									{
										lSQL.Append("'"+dt_user_car.Rows[j][dt_config.Rows[k]["Column_correspondence_cheliang_bossed"].ToString()].ToString()+"'");
									}
								}
								else
								{
									if(dt_user_car.Rows[j][dt_config.Rows[k]["Column_correspondence_cheliang_bossed"].ToString()].ToString() =="")
									{
										lSQL.Append(" null,");
									}
									else
									{
										lSQL.Append("'"+dt_user_car.Rows[j][dt_config.Rows[k]["Column_correspondence_cheliang_bossed"].ToString()].ToString()+"',");
									}
								}
							}
						}

						lSQL.Append(")");				
						DBFunc.executeNonQuery(trans,lSQL.ToString());	
						num++;
					}
					else
					{
						//更新
						lSQL = new StringBuilder();
						lSQL.Append("   update Car_info set ");
						for(int k = 0; k< dt_config.Rows.Count; k++)
						{
							if(dt_config.Rows[k]["Column_correspondence_carinfo"].ToString() == "personId" || dt_config.Rows[k]["Column_correspondence_carinfo"].ToString() =="licensePlate")
							{
							}
							else
							{
								if(k == dt_config.Rows.Count-1)
								{
									if(dt_user_car.Rows[j][dt_config.Rows[k]["Column_correspondence_cheliang_bossed"].ToString()].ToString() =="")
									{
										lSQL.Append(dt_config.Rows[k]["Column_correspondence_carinfo"].ToString()+"= null ");
									}
									else
									{
										lSQL.Append(dt_config.Rows[k]["Column_correspondence_carinfo"].ToString()+"= '"+dt_user_car.Rows[j][dt_config.Rows[k]["Column_correspondence_cheliang_bossed"].ToString()].ToString()+"'" );
									}
								}
								else
								{
									if(dt_user_car.Rows[j][dt_config.Rows[k]["Column_correspondence_cheliang_bossed"].ToString()].ToString() =="")
									{
										lSQL.Append(dt_config.Rows[k]["Column_correspondence_carinfo"].ToString()+"= null, ");
									}
									else
									{
										lSQL.Append(dt_config.Rows[k]["Column_correspondence_carinfo"].ToString()+"= '"+dt_user_car.Rows[j][dt_config.Rows[k]["Column_correspondence_cheliang_bossed"].ToString()].ToString()+"',");
									}
								}		
							}
						}

						lSQL.Append("   where licensePlate = '"+licensePlate+"' ");

						DBFunc.executeNonQuery(trans,lSQL.ToString());	
						num++;
					}				
				}
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				throw new Exception("更新车辆数据出错");
			}
			finally
			{
				Con.Close();
				Con.Dispose();
				Con1.Close();
				Con1.Dispose();
			}
			return num;
		}
		

		//获取当前用户所分配的任务
		public DataTable GetAssigner_ByName(string person_name,string fullName,int pay_num,string assign_role)
		{
			StringBuilder lSQL = new StringBuilder();
			lSQL.Append(" select top "+pay_num+" ci.personId,ci.licensePlate, ");
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
			lSQL.Append("     ue.fullName as username ");
			lSQL.Append(" from dbo.Car_info ci,brandinfo bi,hdb_caseInfo hc,user_assign ua,dbo.hdb_user ue ");
			lSQL.Append(" where ci.manufacturers = brandNameCN ");
			lSQL.Append("   and ci.personId = hc.id ");
			lSQL.Append("   and ua.car_id = ci.id ");
			if(person_name !="")
			{
				lSQL.Append("   and hc.personName like '%"+person_name+"%' ");
			}
			lSQL.Append("   and ue.fullName='"+fullName+"'");
			lSQL.Append("   and ua.personId = hc.id ");
			lSQL.Append("   and ua.userId = ue.userId ");
			if(assign_role =="全部")
			{
			}
			else
			{
				lSQL.Append("   and ua.assign_role='"+assign_role+"' ");
			}
			lSQL.Append("   and ci.keep_date is not null ");
			lSQL.Append("    order by hc.id ");

			IDbConnection Con=DBFunc.getConnection();
			DataTable dt = new DataTable();
			using(Con)
			{
				dt = DBFunc.executeDataTable(Con,lSQL.ToString());
			}
			return dt;
		}

		public bool UpdateUserAssigner_Role(Hashtable ht)
		{
			bool bRet= true;
			StringBuilder lSQL = new StringBuilder();
			lSQL.Append(" update dbo.user_assign set assign_role = @assign_role where kehu_no = @kehu_no ");
			IDbConnection Con=DBFunc.getConnection();
			IDbTransaction trans=Con.BeginTransaction();

			try
			{
				DBFunc.executeNonQuery(trans,lSQL.ToString(),ht);
				if(ht.ContainsKey("card_id"))
				{
					lSQL = new StringBuilder();
					lSQL.Append(" update dbo.Car_info set keep_date = GETDATE() where id = @card_id ");
					DBFunc.executeNonQuery(trans,lSQL.ToString(),ht);
				}				
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				bRet=false;
				throw new Exception("更新分配任务信息出错："+ex.Message);
			}
			finally
			{
				Con.Close();
				Con.Dispose();
			}
			return bRet;
		}

		public bool UpdateUserAssigner(Hashtable ht)
		{
			bool bRet= true;
			StringBuilder lSQL = new StringBuilder();
			lSQL.Append(" update dbo.user_assign set userId = @userId where kehu_no = @kehu_no ");
			IDbConnection Con=DBFunc.getConnection();
			IDbTransaction trans=Con.BeginTransaction();

			try
			{
				DBFunc.executeNonQuery(trans,lSQL.ToString(),ht);
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				bRet=false;
				throw new Exception("更新分配任务信息出错："+ex.Message);
			}
			finally
			{
				Con.Close();
				Con.Dispose();
			}
			return bRet;
		}

		public bool SaveUserAssigner(Hashtable ht)
		{
			bool bRet= true;
			StringBuilder lSQL=new StringBuilder();
			lSQL.Append(" insert into dbo.user_assign(assign_type,personId,car_id,assign_data,userId,assign_role,plan_id) ");
			lSQL.Append(" values ");
			lSQL.Append(" (@assign_type,@personId,@car_id,getdate(),@userId,@assign_role,@plan_id) ");
			IDbConnection Con=DBFunc.getConnection();
			IDbTransaction trans=Con.BeginTransaction();

			try
			{
				DBFunc.executeNonQuery(trans,lSQL.ToString(),ht);
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				bRet=false;
				throw new Exception("插入分配任务信息出错："+ex.Message);
			}
			finally
			{
				Con.Close();
				Con.Dispose();
			}
			return bRet;

		}

		public DataTable getDataUeser()
		{
			DataTable dt=null;			
			StringBuilder lSQL=new StringBuilder();
			lSQL.Append(" select userId,fullName from dbo.hdb_user ");
			IDbConnection Con=DBFunc.getConnection();
			using(Con)
			{
				dt = DBFunc.executeDataTable(Con,lSQL.ToString());
			}
			return  dt;

		}

		public String getSQL_Assign_compl(int pay_num,string person_name)
		{
			StringBuilder lSQL = new StringBuilder();
			lSQL.Append(" select top "+pay_num+" ci.personId,ci.licensePlate, ");
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
			lSQL.Append("     ue.fullName as username ");
			lSQL.Append(" from dbo.Car_info ci,brandinfo bi,hdb_caseInfo hc,user_assign ua,dbo.hdb_user ue ");
			lSQL.Append(" where ci.manufacturers = brandNameCN ");
			lSQL.Append("   and ci.personId = hc.id ");
			lSQL.Append("   and ua.car_id = ci.id ");
			if(person_name !="")
			{
				lSQL.Append("   and hc.personName like '%"+person_name+"%' ");
			}
			lSQL.Append("   and ua.personId = hc.id ");
			lSQL.Append("   and ua.userId = ue.userId ");
			lSQL.Append("   and ua.assign_role='已完成' ");
			lSQL.Append("   and ci.keep_date is not null ");
			lSQL.Append("    order by hc.id ");
			return lSQL.ToString();
		}

		public String getSQL_Assign_jieshou(int pay_num,string person_name)
		{
			StringBuilder lSQL = new StringBuilder();
			lSQL.Append(" select top "+pay_num+" ci.personId,ci.licensePlate, ");
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
			lSQL.Append("     ue.fullName as username ");
			lSQL.Append(" from dbo.Car_info ci,brandinfo bi,hdb_caseInfo hc,user_assign ua,dbo.hdb_user ue ");
			lSQL.Append(" where ci.manufacturers = brandNameCN ");
			lSQL.Append("   and ci.personId = hc.id ");
			lSQL.Append("   and ua.car_id = ci.id ");
			if(person_name !="")
			{
				lSQL.Append("   and hc.personName like '%"+person_name+"%' ");
			}
			lSQL.Append("   and ua.personId = hc.id ");
			lSQL.Append("   and ua.userId = ue.userId ");
			lSQL.Append("   and ua.assign_role='已接受' ");
			lSQL.Append("   and ci.keep_date is not null ");
			lSQL.Append("    order by hc.id ");
			return lSQL.ToString();
		}

		public String getSQL_Assign_Applyed(int pay_num,string person_name)
		{
			StringBuilder lSQL = new StringBuilder();
			lSQL.Append(" select top "+pay_num+" ci.personId,ci.licensePlate, ");
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
			lSQL.Append("     ue.fullName as username ");
			lSQL.Append(" from dbo.Car_info ci,brandinfo bi,hdb_caseInfo hc,user_assign ua,dbo.hdb_user ue ");
			lSQL.Append(" where ci.manufacturers = brandNameCN ");
			lSQL.Append("   and ci.personId = hc.id ");
			lSQL.Append("   and ua.car_id = ci.id ");
			if(person_name !="")
			{
				lSQL.Append("   and hc.personName like '%"+person_name+"%' ");
			}
			lSQL.Append("   and ua.personId = hc.id ");
			lSQL.Append("   and ua.userId = ue.userId ");
			lSQL.Append("   and ua.assign_role='已分配' ");
			lSQL.Append("   and ci.keep_date is not null ");
			lSQL.Append("    order by hc.id ");

			return lSQL.ToString();
		}

		public String getSQL_Assign_Apply_Spical(int pay_num,string person_name)
		{
			StringBuilder lSQL = new StringBuilder();
			lSQL.Append(" select top "+pay_num+" ci.personId,ci.licensePlate, ");
			lSQL.Append("     ci.id as car_id, ");
			lSQL.Append("     null as kuhu_no,");
			lSQL.Append("     hc.id, ");
			lSQL.Append("     hc.personName, ");
			lSQL.Append("     brandNameCN,brandNameEN,mileage, ");
			lSQL.Append("     keep_date,ci.average_mileage,     ");
			lSQL.Append("     datediff(day,ci.keep_date,GETDATE()), ");
			lSQL.Append("     ci.salesdate, ");
			lSQL.Append("     '未分配' as assign_type, ");
			lSQL.Append("     '未分配' as assign_role,");
			lSQL.Append("     '' as  username ");
			lSQL.Append(" from dbo.Car_info ci,brandinfo bi,hdb_caseInfo hc "); 
			lSQL.Append(" where ci.manufacturers = brandNameCN ");
			lSQL.Append("   and ci.personId = hc.id ");
			lSQL.Append("   and ci.keep_date is not null ");
			if(person_name !="")
			{
				lSQL.Append("   and hc.personName like '%"+person_name+"%' ");
			}
			
			lSQL.Append("   and (datediff(day,ci.keep_date,GETDATE()))*ci.average_mileage<=bi.mileage ");
			lSQL.Append("   and ci.id not in  ");
			lSQL.Append("   ( ");
			lSQL.Append("  select car_id from user_assign where assign_role != '已关闭' ");
			lSQL.Append("   ) ");
			lSQL.Append("    order by hc.id");
			return lSQL.ToString();
		}

		public String getSQL_Assign_Apply(int pay_num,string person_name)
		{
			StringBuilder lSQL=new StringBuilder();
			lSQL.Append(" select top "+pay_num+" ci.personId,ci.licensePlate, ");
			lSQL.Append("     ci.id as car_id, ");
			lSQL.Append("     null as kuhu_no,");
			lSQL.Append("     hc.id, ");
			lSQL.Append("     hc.personName, ");
			lSQL.Append("     brandNameCN,brandNameEN,mileage, ");
			lSQL.Append("     keep_date,ci.average_mileage,     ");
			lSQL.Append("     datediff(day,ci.keep_date,GETDATE()), ");
			lSQL.Append("     ci.salesdate, ");
			lSQL.Append("     '未分配' as assign_type, ");
			lSQL.Append("     '未分配' as assign_role,");
			lSQL.Append("     '' as  username ");
			lSQL.Append(" from dbo.Car_info ci,brandinfo bi,hdb_caseInfo hc "); 
			lSQL.Append(" where ci.manufacturers = brandNameCN ");
			lSQL.Append("   and ci.personId = hc.id ");
			lSQL.Append("   and ci.keep_date is not null ");
			if(person_name !="")
			{
				lSQL.Append("   and hc.personName like '%"+person_name+"%' ");
			}
			
			lSQL.Append("   and (datediff(day,ci.keep_date,GETDATE()))*ci.average_mileage>bi.mileage ");
			lSQL.Append("   and ci.id not in  ");
			lSQL.Append("   ( ");
			lSQL.Append("  select car_id from user_assign where assign_role != '已关闭' ");
			lSQL.Append("   ) ");
			lSQL.Append("    order by hc.id");
			return lSQL.ToString();
		}

		public DataTable GetAssignCar(String assign_type,int pay_num,string person_name)
		{
			DataTable dt=null;			
			StringBuilder lSQL=new StringBuilder();
			switch(assign_type)
			{
				case "未分配保养提醒数据":
					lSQL.Append(this.getSQL_Assign_Apply(pay_num,person_name));
					break;
				case "已分配未接受":
					lSQL.Append(this.getSQL_Assign_Applyed(pay_num,person_name));
					break;
				case "未分配其他数据":
					lSQL.Append(this.getSQL_Assign_Apply_Spical(pay_num,person_name));
					break;
				case "已分配已接受":
					lSQL.Append(this.getSQL_Assign_jieshou(pay_num,person_name));
					break;
				case "已完成":
					lSQL.Append(this.getSQL_Assign_compl(pay_num,person_name));
					break;
			}
			IDbConnection Con=DBFunc.getConnection();
			using(Con)
			{
				dt = DBFunc.executeDataTable(Con,lSQL.ToString());
			}
			return dt;
		}

	}
}
