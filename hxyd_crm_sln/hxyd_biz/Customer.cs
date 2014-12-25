using System;
using System.Text;
using System.Collections;
using CaseyLib;
using CaseyLib.util;
using System.Data;
using System.Data.SqlClient;

namespace hxyd_biz
{
	/// <summary>
	/// Customer 的摘要说明。
	/// </summary>
	public class Customer
	{
		public Customer()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public static DataTable GetCustomerInfo(string strID)
		{
			Hashtable htbCondition =new Hashtable();
			htbCondition["id"]=strID;
			return GetCustomerInfo(htbCondition);
		}
		/// <summary>
		/// 获取客户基本信息，参数名：customer_id, agent_id,personName,phone,VIN,licensePlate,brand,contactState
		/// </summary>
		/// <returns></returns>
		public static DataTable GetCustomerInfo(Hashtable htbCondition)
		{
			/*
			 *customer_id, agent_id,personName,phone,VIN,licensePlate,brand,contactState*/

			StringBuilder lSQL=new StringBuilder();

//			lSQL.Append("  select c.id,c.customerNum,c.personName,c.gender,c.phone,c.area,c.address,c.brand, ");
//			lSQL.Append(" c.model,  c.vin,c.licensePlate,c.contactState,c.failedReason,c.introducer,c.remark,c.lastUpdate,c.customerType,'' as comment, ");
//			lSQL.Append("  convert( varchar(10),c.expire_date,120)  as expire_date,c.companyname,u.userName, convert( varchar(10),c.salesDate,120) as salesDate");
//			lSQL.Append(" from  ");
//			lSQL.Append(" hdb_caseInfo c left join hdb_user u on c.agent_id=u.userId ");
//			lSQL.Append(" where 1=1 ");
//
//			if(htbCondition["id"]!=null && htbCondition["id"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.id=@id" );
//			}
//
//
//			if(htbCondition["personName"]!=null && htbCondition["personName"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and  c.personName=@personName");
//			}
//
//			if(htbCondition["phone"]!=null && htbCondition["phone"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.phone=@phone");
//			}
//			if(htbCondition["VIN"]!=null && htbCondition["VIN"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.VIN=@VIN");
//			}
//			if(htbCondition["licensePlate"]!=null && htbCondition["licensePlate"].ToString()!=string.Empty)
//			{
//				lSQL.Append("  and c.licensePlate=@licensePlate");
//			}
//			if(htbCondition["brand"]!=null && htbCondition["brand"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.brand=@brand");
//			}
//			if(htbCondition["contactState"]!=null && htbCondition["contactState"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.contactState=@contactState");
//			}
//
//			if(htbCondition["agent_id"]!=null && htbCondition["agent_id"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.agent_id=@agent_id");
//			}
//			lSQL.Append(" order by c.personName");


			lSQL.Append("  select c.id,c.customerNum,c.personName,c.gender,c.phone,c.idcard,convert( varchar(10),c.birthday,120) birthday,c.area,c.address,ca.manufacturers brand,isnull(c.customer_level,'') customer_level,ca.shape_colors, ");
			lSQL.Append(" ca.car_model model,  ca.vin,ca.licensePlate,ca.engine_no,ca.id carId,DATEDIFF(DD, ca.salesDate ,getdate()  )*average_mileage current_mileage_new,ca.current_mileage,ca.average_mileage,ca.car_type,c.contactState,c.failedReason,c.introducer,c.remark,c.lastUpdate,c.customerType,'' as comment, ");
			lSQL.Append("  convert( varchar(10),ca.expire_date,120)  as expire_date,c.companyname,u.userName,convert( varchar(10),ca.keep_date,120) as keep_date, convert( varchar(10),ca.salesDate,120) as salesDate");
			//lSQL.Append("  i.insuranceFees,i.returnPoint,i.insuranceCompany,i.forceInsur,i.trafficPoint,i.travelTax,i.single_date,i.view_date,i.view_time,i.interviewListId");
			lSQL.Append(" from  ");
			lSQL.Append(" hdb_caseInfo c left join hdb_user u on c.agent_id=u.userId ");
			lSQL.Append(" join Car_info ca on c.id=ca.personId ");
			//lSQL.Append(" left join interview_list i on ca.id=i.carId ");
			lSQL.Append(" where 1=1 ");

			if(htbCondition["id"]!=null && htbCondition["id"].ToString()!=string.Empty)
			{
				lSQL.Append(" and ca.id=@id" );
			}

//			if(htbCondition["interviewListId"]!=null && htbCondition["interviewListId"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and i.interviewListId=@interviewListId" );
//			}


			if(htbCondition["personName"]!=null && htbCondition["personName"].ToString()!=string.Empty)
			{
				lSQL.Append(" and  c.personName=@personName");
			}

			if(htbCondition["phone"]!=null && htbCondition["phone"].ToString()!=string.Empty)
			{
				lSQL.Append(" and c.phone=@phone");
			}
			if(htbCondition["VIN"]!=null && htbCondition["VIN"].ToString()!=string.Empty)
			{
				lSQL.Append(" and ca.VIN=@VIN");
			}
			if(htbCondition["licensePlate"]!=null && htbCondition["licensePlate"].ToString()!=string.Empty)
			{
				lSQL.Append("  and ca.licensePlate=@licensePlate");
			}
			if(htbCondition["brand"]!=null && htbCondition["brand"].ToString()!=string.Empty)
			{
				lSQL.Append(" and ca.manufacturers=@brand");
			}
			if(htbCondition["contactState"]!=null && htbCondition["contactState"].ToString()!=string.Empty)
			{
				lSQL.Append(" and c.contactState=@contactState");
			}

			if(htbCondition["agent_id"]!=null && htbCondition["agent_id"].ToString()!=string.Empty)
			{
				lSQL.Append(" and c.agent_id=@agent_id");
			}
			lSQL.Append(" order by c.personName");

			IDbConnection Con=DBFunc.getConnection();
			using(Con)
			{
				return DBFunc.executeDataTable(Con,lSQL.ToString(),htbCondition);
			}




		}

		public static DataTable GetModifyCustomerInfo(string strID)
		{
			Hashtable htbCondition =new Hashtable();
			htbCondition["id"]=strID;
			return GetModifyCustomerInfo(htbCondition);
		}

		public static DataTable GetCarInfo(Hashtable htbCondition)
		{
			StringBuilder lSQL=new StringBuilder();
			lSQL.Append(" select c.id,c.personName,ca.manufacturers,ca.VIN,ca.car_model,ca.expire_date,ca.id carId,ca.car_type");
			lSQL.Append(" from  ");
			lSQL.Append(" hdb_caseInfo c join Car_info ca on c.id=ca.personId ");
			lSQL.Append(" where 1=1 ");

			if(htbCondition["personid"]!=null && htbCondition["personid"].ToString()!=string.Empty)
			{
				lSQL.Append(" and c.id=@personid" );
			}
			
			IDbConnection Con=DBFunc.getConnection();
			using(Con)
			{
				return DBFunc.executeDataTable(Con,lSQL.ToString(),htbCondition);
			}

		}

		

		/// <summary>
		/// 只获取客户信息
		/// </summary>
		/// <returns></returns>
		public static DataTable GetModifyCustomerInfo(Hashtable htbCondition)
		{


			StringBuilder lSQL=new StringBuilder();

			lSQL.Append(" select c.id,c.personName,c.gender,c.phone,c.area,c.address,c.idcard,convert( varchar(10),c.birthday,120) birthday,c.companyName,c.introducer,c.failedReason,c.contactState,c.customerType,c.customer_level ");
			lSQL.Append(" from  ");
			lSQL.Append(" hdb_caseInfo c left join Car_info ca on c.id=ca.personId ");
			lSQL.Append(" where 1=1 ");

			if(htbCondition["id"]!=null && htbCondition["id"].ToString()!=string.Empty)
			{
				lSQL.Append(" and c.id=@id" );
			}
			if(htbCondition["personName"]!=null && htbCondition["personName"].ToString()!=string.Empty)
			{
				lSQL.Append(" and  c.personName=@personName");
			}

			if(htbCondition["phone"]!=null && htbCondition["phone"].ToString()!=string.Empty)
			{
				lSQL.Append(" and c.phone=@phone");
			}
			if(htbCondition["VIN"]!=null && htbCondition["VIN"].ToString()!=string.Empty)
			{
				lSQL.Append(" and ca.VIN=@VIN");
			}
			if(htbCondition["licensePlate"]!=null && htbCondition["licensePlate"].ToString()!=string.Empty)
			{
				lSQL.Append("  and ca.licensePlate=@licensePlate");
			}
			lSQL.Append(" group by c.id,c.personName,c.gender,c.phone,c.area,c.address,c.idcard,convert( varchar(10),c.birthday,120),c.companyName,c.introducer,c.failedReason,c.contactState,c.customerType,c.customer_level");
			lSQL.Append(" order by c.personName");

			IDbConnection Con=DBFunc.getConnection();
			using(Con)
			{
				return DBFunc.executeDataTable(Con,lSQL.ToString(),htbCondition);
			}




		}

		/// <summary>
		/// 获取客户历史联系情况
		/// </summary>
		/// <returns></returns>
		public static DataTable GetCustomerInterview(Hashtable htbCondition)
		{			
			return GetCustomerAllInfo(htbCondition,2);

		}

		
		/// <summary>
		/// 获取客户出单信息
		/// </summary>
		/// <returns></returns>
		public static DataTable GetCustomerSaleInfo(Hashtable htbCondition)
		{
			//return GetCustomerAllInfo(htbCondition,1);
			StringBuilder lSQL=new StringBuilder();

			lSQL.Append("  select c.id,c.customerNum,c.personName,c.gender,c.phone,c.area,c.address,ca.manufacturers brand,c.idcard,convert( varchar(10),c.birthday,120) birthday, ");
			lSQL.Append(" ca.car_model model,  ca.vin,ca.licensePlate,ca.id carId,ca.engine_no,c.contactState,c.failedReason,c.introducer,i.remark,c.lastUpdate,convert( varchar(10),ca.salesDate,120) as salesDate,c.customerType, ");
			//lSQL.Append("  convert( varchar(10),c.expire_date,120) as expire_date,c.companyname,u.userName,  ");
			lSQL.Append(" c.companyname,u.userName,  ");
			/*历史联系情况*/
			lSQL.Append(" i.interviewListId,  convert( varchar(10),i.interviewTime,120) as interviewTime,i.agentID,u2.userName as agentName,i.comment,c.createTime, ");
			lSQL.Append(" i.insuranceFees,i.returnPoint,i.forceInsur, convert( varchar(10),i.expire_date,120) as expire_date,i.insuranceCompany,i.profit,i.rebate,i.travelTax,i.trafficPoint,i.insurancePoint, ");
			lSQL.Append(" i.companyid,i.view_time,convert(varchar(10),i.view_date,120) as view_date,convert( varchar(10),i.single_date,120) as single_date   ");
			lSQL.Append(" from  ");
			lSQL.Append(" hdb_caseInfo c left join hdb_user u on c.agent_id=u.userId ");
			lSQL.Append(" join Car_info ca on c.id=ca.personId ");
			lSQL.Append(" join  interview_list i on ca.id=i.carId ");
			lSQL.Append(" left join hdb_user u2   on i.agentId=u2.userId ");

			lSQL.Append(" where 1=1 ");
			if(htbCondition["id"]!=null && htbCondition["id"].ToString()!=string.Empty)
			{
				lSQL.Append("and c.id=@id" );
			}

			if(htbCondition["carid"]!=null && htbCondition["carid"].ToString()!=string.Empty)
			{
				lSQL.Append("and ca.id=@carid" );
			}


			if(htbCondition["personName"]!=null && htbCondition["personName"].ToString()!=string.Empty)
			{
				lSQL.Append(" and c.personName=@personName");
			}

			if(htbCondition["single_date"]!=null && htbCondition["single_date"].ToString()!=string.Empty)
			{
				lSQL.Append(" and convert( varchar(10), i.single_date,120)>=@single_date");
			}
			if(htbCondition["end_time"]!=null && htbCondition["end_time"].ToString()!=string.Empty)
			{
				lSQL.Append(" and convert( varchar(10), i.single_date,120)<=@end_time");
			}


			if(htbCondition["phone"]!=null && htbCondition["phone"].ToString()!=string.Empty)
			{
				lSQL.Append(" and c.phone=@phone");
			}
			if(htbCondition["VIN"]!=null && htbCondition["VIN"].ToString()!=string.Empty)
			{
				lSQL.Append(" and ca.vin=@VIN ");
			}
			if(htbCondition["licensePlate"]!=null && htbCondition["licensePlate"].ToString()!=string.Empty)
			{
				lSQL.Append(" and ca.licensePlate=@licensePlate");
			}
			if(htbCondition["brand"]!=null && htbCondition["brand"].ToString()!=string.Empty)
			{
				lSQL.Append(" and ca.manufacturers=@brand");
			}
			if(htbCondition["contactState"]!=null && htbCondition["contactState"].ToString()!=string.Empty)
			{
				//lSQL.Append(" and c.contactState=@contactState");
				lSQL.Append(" and i.contactState=@contactState");
			}
			if(htbCondition["interviewListId"]!=null && htbCondition["interviewListId"].ToString()!=string.Empty)
			{
				lSQL.Append(" and i.interviewListId=@interviewListId");
			}

			if(htbCondition["agent_id"]!=null && htbCondition["agent_id"].ToString()!=string.Empty)
			{
				lSQL.Append(" and c.agent_id=@agent_id");
			}
			
			lSQL.Append(" and isnull(i.insuranceFees,0)>0");
			
			lSQL.Append(" order by c.personName");

			IDbConnection Con=DBFunc.getConnection();
			using(Con)
			{
				return DBFunc.executeDataTable(Con,lSQL.ToString(),htbCondition);
			}
		}
		public static DataTable GetCustomerAllInfo(Hashtable htbCondition)
		{
			return GetCustomerAllInfo(htbCondition,0);
		}

		public static  DataTable GetCustomerLastInfo(Hashtable htbCondition,int bQueryType)
		{
			StringBuilder lSQL=new StringBuilder();

//			lSQL.Append("  select c.id,c.customerNum,c.personName,c.gender,c.phone,c.area,c.address,c.brand, ");
//			lSQL.Append(" c.model,  c.vin,c.licensePlate,c.contactState,c.failedReason,c.introducer,i.remark,c.lastUpdate,c.salesDate,c.customerType, ");
//			lSQL.Append("  convert( varchar(10),c.expire_date,120) as expire_date,c.companyname,u.userName,  ");
//			lSQL.Append(" i.interviewListId,  convert( varchar(10),i.interviewTime,120) as interviewTime,i.agentID,u2.userName as agentName,i.comment,c.createTime, ");
//			lSQL.Append(" i.insuranceFees,i.returnPoint, convert( varchar(10),i.expire_date,120) as expire_date,i.insuranceCompany,i.profit,i.rebate, ");
//			lSQL.Append(" i.companyid,i.view_time,convert(varchar(10),i.view_date,120) as view_date,convert( varchar(10),i.single_date,120) as single_date   ");
//			lSQL.Append(" from  ");
//			lSQL.Append(" hdb_caseInfo c left join hdb_user u on c.agent_id=u.userId ");
//			lSQL.Append(" left join  (select * from interview_list where interviewListId in ( ");
//			lSQL.Append(" select max(interviewListId) from interview_list group by  personInfoId)) i on c.id=i.personInfoID ");
//			lSQL.Append(" left join hdb_user u2   on i.agentId=u2.userId ");
//
//			lSQL.Append(" where 1=1 ");
//
//			if(htbCondition["id"]!=null && htbCondition["id"].ToString()!=string.Empty)
//			{
//				lSQL.Append("and c.id=@id" );
//			}
//
//
//			if(htbCondition["personName"]!=null && htbCondition["personName"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.personName=@personName");
//			}
//
//			if(htbCondition["interviewTime"]!=null && htbCondition["interviewTime"].ToString()!=string.Empty)
//			{
//				//lSQL.Append(" and convert( varchar(10), i.interviewTime,120)>=@interviewTime");
//				lSQL.Append(" and convert( varchar(10), i.view_date,120)>=@interviewTime");
//			}
//			if(htbCondition["end_time"]!=null && htbCondition["end_time"].ToString()!=string.Empty)
//			{
//				//lSQL.Append(" and convert( varchar(10), i.interviewTime,120)<=@end_time");
//				lSQL.Append(" and convert( varchar(10), i.view_date,120)<=@end_time");
//			}
//
//
//			if(htbCondition["phone"]!=null && htbCondition["phone"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.phone=@phone");
//			}
//			if(htbCondition["VIN"]!=null && htbCondition["VIN"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.VIN=@VIN ");
//			}
//			if(htbCondition["licensePlate"]!=null && htbCondition["licensePlate"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.licensePlate=@licensePlate");
//			}
//			if(htbCondition["brand"]!=null && htbCondition["brand"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.brand=@brand");
//			}
//			if(htbCondition["contactState"]!=null && htbCondition["contactState"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.contactState=@contactState");
//			}
//			if(htbCondition["interViewListId"]!=null && htbCondition["interViewListId"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and i.interViewListId=@interViewListId");
//			}
//
//			if(htbCondition["agent_id"]!=null && htbCondition["agent_id"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.agent_id=@agent_id");
//			}
//			if(bQueryType==0)
//			{
//			}
//			else
//			{
//				if(bQueryType==1)//是否只查询成交信息
//				{
//					lSQL.Append(" and isnull(i.insuranceFees,0)>0");
//				}
//				else
//				{
//					lSQL.Append(" and (isnull(i.insuranceFees,0)=0 or isnull(i.insuranceFees,0)>0)");
//				}
//			}
//			
//			lSQL.Append(" order by c.personName");

			
			lSQL.Append("  select c.id,c.customerNum,c.personName,c.gender,c.phone,c.area,c.address,ca.manufacturers brand, ");
			lSQL.Append(" ca.car_model model,  ca.vin,ca.licensePlate,ca.id carId,c.contactState,c.failedReason,c.introducer,i.remark,c.lastUpdate,convert( varchar(10),ca.salesDate,120) as salesDate,c.customerType, ");
			//lSQL.Append("  convert( varchar(10),c.expire_date,120) as expire_date,c.companyname,u.userName,  ");
			lSQL.Append("  c.companyname,u.userName,  ");
			lSQL.Append(" i.interviewListId,  convert( varchar(10),i.interviewTime,120) as interviewTime,i.agentID,u2.userName as agentName,i.comment,c.createTime, ");
			lSQL.Append(" i.insuranceFees,i.returnPoint,i.forceInsur, convert( varchar(10),ca.expire_date,120) as expire_date,i.insuranceCompany,i.profit,i.rebate,i.service_type,i.trafficPoint, ");
			lSQL.Append(" i.companyid,i.view_time,convert(varchar(10),i.view_date,120) as view_date,convert( varchar(10),i.single_date,120) as single_date   ");
			lSQL.Append(" from  ");
			lSQL.Append(" hdb_caseInfo c left join hdb_user u on c.agent_id=u.userId ");
			
			lSQL.Append(" join Car_info ca on c.id=ca.personId ");
			//lSQL.Append(" left join  (select * from interview_list where interviewListId in ( ");
			lSQL.Append(" join  (select * from interview_list where interviewListId in ( ");
			lSQL.Append(" select max(interviewListId) from interview_list group by  carId)) i on ca.id=i.carId ");
			lSQL.Append(" left join hdb_user u2   on i.agentId=u2.userId ");

			lSQL.Append(" where 1=1 ");

			if(htbCondition["id"]!=null && htbCondition["id"].ToString()!=string.Empty)
			{
				lSQL.Append("and ca.id=@id" );
			}



			if(htbCondition["personName"]!=null && htbCondition["personName"].ToString()!=string.Empty)
			{
				lSQL.Append(" and c.personName=@personName");
			}


			if(htbCondition["date_type"]!=null && htbCondition["date_type"].ToString()!=string.Empty)
			{
				if(htbCondition["date_type"].ToString()=="1")
				{
					if(htbCondition["view_date"]!=null && htbCondition["view_date"].ToString()!=string.Empty)
					{
						
						lSQL.Append(" and convert( varchar(10), i.view_date,120)>=@view_date");
					}
					if(htbCondition["end_time"]!=null && htbCondition["end_time"].ToString()!=string.Empty)
					{
						lSQL.Append(" and convert( varchar(10), i.view_date,120)<=@end_time");
					}
				}
				else if(htbCondition["date_type"].ToString()=="2")
				{
					
					if(htbCondition["view_date"]!=null && htbCondition["view_date"].ToString()!=string.Empty)
					{
						lSQL.Append(" and convert( varchar(10), i.interviewTime,120)>=@view_date");
		
					}
					if(htbCondition["end_time"]!=null && htbCondition["end_time"].ToString()!=string.Empty)
					{
						
						lSQL.Append(" and convert( varchar(10), i.interviewTime,120)<=@end_time");
					}
				}
				else if(htbCondition["date_type"].ToString()=="3")
				{
					
					if(htbCondition["view_date"]!=null && htbCondition["view_date"].ToString()!=string.Empty)
					{
						lSQL.Append(" and convert( varchar(10), i.single_date,120)>=@view_date");
		
					}
					if(htbCondition["end_time"]!=null && htbCondition["end_time"].ToString()!=string.Empty)
					{
						
						lSQL.Append(" and convert( varchar(10), i.single_date,120)<=@end_time");
					}
				}
				else if(htbCondition["date_type"].ToString()=="4")
				{
					
					if(htbCondition["view_date"]!=null && htbCondition["view_date"].ToString()!=string.Empty)
					{
						lSQL.Append(" and convert( varchar(10), ca.salesdate,120)>=@view_date");
		
					}
					if(htbCondition["end_time"]!=null && htbCondition["end_time"].ToString()!=string.Empty)
					{
						
						lSQL.Append(" and convert( varchar(10), ca.salesdate,120)<=@end_time");
					}
				}
				else if(htbCondition["date_type"].ToString()=="5")
				{
					
					if(htbCondition["view_date"]!=null && htbCondition["view_date"].ToString()!=string.Empty)
					{
						lSQL.Append(" and convert( varchar(10), i.expire_date,120)>=@view_date");
		
					}
					if(htbCondition["end_time"]!=null && htbCondition["end_time"].ToString()!=string.Empty)
					{
						
						lSQL.Append(" and convert( varchar(10), i.expire_date,120)<=@end_time");
					}
				}
			}
			
		
			if(htbCondition["customerType"]!=null && htbCondition["customerType"].ToString()!=string.Empty)
			{
				lSQL.Append(" and c.customerType=@customerType");
			}


			if(htbCondition["phone"]!=null && htbCondition["phone"].ToString()!=string.Empty)
			{
				lSQL.Append(" and c.phone=@phone");
			}
			if(htbCondition["VIN"]!=null && htbCondition["VIN"].ToString()!=string.Empty)
			{
				lSQL.Append(" and ca.VIN=@VIN ");
			}
			if(htbCondition["licensePlate"]!=null && htbCondition["licensePlate"].ToString()!=string.Empty)
			{
				lSQL.Append(" and ca.licensePlate=@licensePlate");
			}
			if(htbCondition["brand"]!=null && htbCondition["brand"].ToString()!=string.Empty)
			{
				lSQL.Append(" and ca.manufacturers=@brand");
			}
			if(htbCondition["contactState"]!=null && htbCondition["contactState"].ToString()!=string.Empty)
			{
				lSQL.Append(" and c.contactState=@contactState");
			}
			if(htbCondition["interViewListId"]!=null && htbCondition["interViewListId"].ToString()!=string.Empty)
			{
				lSQL.Append(" and i.interViewListId=@interViewListId");
			}

			if(htbCondition["agent_id"]!=null && htbCondition["agent_id"].ToString()!=string.Empty)
			{
				lSQL.Append(" and c.agent_id=@agent_id");
			}
			if(bQueryType==0)
			{
			}
			else
			{
				if(bQueryType==1)//是否只查询成交信息
				{
					lSQL.Append(" and isnull(i.insuranceFees,0)>0");
				}
				else
				{
					lSQL.Append(" and (isnull(i.insuranceFees,0)=0 or isnull(i.insuranceFees,0)>0)");
				}
			}
			
			//lSQL.Append(" order by c.personName");
			lSQL.Append(" order by i.view_date,i.view_time");

			IDbConnection Con=DBFunc.getConnection();
			using(Con)
			{
				return DBFunc.executeDataTable(Con,lSQL.ToString(),htbCondition);
			}

		}


		/// <summary>
		/// 查询客户所有电话联系信息及出单信息，
		/// </summary>
		/// <param name="htbCondition"></param>
		/// <param name="bQueryType">0所有信息，1出单信息，2联系信息</param>
		/// <returns></returns>
		private static  DataTable GetCustomerAllInfo(Hashtable htbCondition,int bQueryType)
		{
			StringBuilder lSQL=new StringBuilder();

//			lSQL.Append("  select c.id,c.customerNum,c.personName,c.gender,c.phone,c.area,c.address,c.brand, ");
//			lSQL.Append(" c.model,  c.vin,c.licensePlate,c.contactState,c.failedReason,c.introducer,i.remark,c.lastUpdate,c.salesDate,c.customerType, ");
//			lSQL.Append("  convert( varchar(10),c.expire_date,120) as expire_date,c.companyname,u.userName,  ");
//			/*历史联系情况*/
//			lSQL.Append(" i.interviewListId,  convert( varchar(10),i.interviewTime,120) as interviewTime,i.agentID,u2.userName as agentName,i.comment,c.createTime, ");
//			lSQL.Append(" i.insuranceFees,i.returnPoint, convert( varchar(10),i.expire_date,120) as expire_date,i.insuranceCompany,i.profit,i.rebate, ");
//			lSQL.Append(" i.companyid,i.view_time,convert(varchar(10),i.view_date,120) as view_date,convert( varchar(10),i.single_date,120) as single_date   ");
//			lSQL.Append(" from  ");
//			lSQL.Append(" hdb_caseInfo c left join hdb_user u on c.agent_id=u.userId ");
//			lSQL.Append(" left join  interview_list i on c.id=i.personInfoID ");
//			lSQL.Append(" left join hdb_user u2   on i.agentId=u2.userId ");
//
//			lSQL.Append(" where 1=1 ");
//			if(htbCondition["id"]!=null && htbCondition["id"].ToString()!=string.Empty)
//			{
//				lSQL.Append("and c.id=@id" );
//			}
//
//
//			if(htbCondition["personName"]!=null && htbCondition["personName"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.personName=@personName");
//			}
//
//			if(htbCondition["interviewTime"]!=null && htbCondition["interviewTime"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and convert( varchar(10), i.interviewTime,120)>=@interviewTime");
//			}
//			if(htbCondition["end_time"]!=null && htbCondition["end_time"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and convert( varchar(10), i.interviewTime,120)<=@end_time");
//			}
//
//
//			if(htbCondition["phone"]!=null && htbCondition["phone"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.phone=@phone");
//			}
//			if(htbCondition["VIN"]!=null && htbCondition["VIN"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.VIN=@VIN ");
//			}
//			if(htbCondition["licensePlate"]!=null && htbCondition["licensePlate"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.licensePlate=@licensePlate");
//			}
//			if(htbCondition["brand"]!=null && htbCondition["brand"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.brand=@brand");
//			}
//			if(htbCondition["contactState"]!=null && htbCondition["contactState"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.contactState=@contactState");
//			}
//			if(htbCondition["interViewListId"]!=null && htbCondition["interViewListId"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and i.interViewListId=@interViewListId");
//			}
//
//			if(htbCondition["agent_id"]!=null && htbCondition["agent_id"].ToString()!=string.Empty)
//			{
//				lSQL.Append(" and c.agent_id=@agent_id");
//			}
//			if(bQueryType==0)
//			{
//			}
//			else
//			{
//				if(bQueryType==1)//是否只查询成交信息
//				{
//					lSQL.Append(" and isnull(i.insuranceFees,0)>0");
//				}
//				else
//				{
//					lSQL.Append(" and (isnull(i.insuranceFees,0)=0 or isnull(i.insuranceFees,0)>0)");
//				}
//			}
//			
//			lSQL.Append(" order by c.personName");

			lSQL.Append("  select c.id,c.customerNum,c.personName,c.gender,c.phone,c.area,c.address,ca.manufacturers brand,c.idcard,convert( varchar(10),c.birthday,120) birthday, ");
			lSQL.Append(" ca.car_model model,  ca.vin,ca.licensePlate,ca.id carId,ca.engine_no,ca.car_type,c.contactState,c.failedReason,c.introducer,i.remark,c.lastUpdate,convert( varchar(10),ca.salesDate,120) as salesDate,c.customerType, ");
			//lSQL.Append("  convert( varchar(10),c.expire_date,120) as expire_date,c.companyname,u.userName,  ");
			lSQL.Append(" c.companyname,u.userName,  ");
			/*历史联系情况*/
			lSQL.Append(" i.interviewListId,  convert( varchar(10),i.interviewTime,120) as interviewTime,i.agentID,u2.userName as agentName,i.comment,c.createTime, ");
			lSQL.Append(" i.insuranceFees,i.returnPoint,i.forceInsur, convert( varchar(10),i.expire_date,120) as expire_date,i.insuranceCompany,i.profit,i.rebate,i.travelTax,i.trafficPoint,i.insurancePoint,i.service_type, ");
			lSQL.Append(" i.companyid,i.view_time,convert(varchar(10),i.view_date,120) as view_date,convert( varchar(10),i.single_date,120) as single_date   ");
			lSQL.Append(",i.Fee_CheSun,i.Fee_HuaHen,i.Fee_SanZhe ,i.Fee_DaoCheJing,i.Fee_RenYuan,i.Fee_BoLi,i.Fee_DaoQiang,i.Fee_SheShui,i.Fee_BuJiMianPei,i.Fee_ZiRan");
			lSQL.Append(" from  ");
			lSQL.Append(" hdb_caseInfo c left join hdb_user u on c.agent_id=u.userId ");
			lSQL.Append(" join Car_info ca on c.id=ca.personId ");
			lSQL.Append(" join  interview_list i on ca.id=i.carId ");
			lSQL.Append(" left join hdb_user u2   on i.agentId=u2.userId ");

			lSQL.Append(" where 1=1 ");
			if(htbCondition["id"]!=null && htbCondition["id"].ToString()!=string.Empty)
			{
				lSQL.Append("and c.id=@id" );
			}

			if(htbCondition["carid"]!=null && htbCondition["carid"].ToString()!=string.Empty)
			{
				lSQL.Append("and ca.id=@carid" );
			}


			if(htbCondition["personName"]!=null && htbCondition["personName"].ToString()!=string.Empty)
			{
				lSQL.Append(" and c.personName=@personName");
			}

			if(htbCondition["interviewTime"]!=null && htbCondition["interviewTime"].ToString()!=string.Empty)
			{
				lSQL.Append(" and convert( varchar(10), i.interviewTime,120)>=@interviewTime");
			}
			if(htbCondition["end_time"]!=null && htbCondition["end_time"].ToString()!=string.Empty)
			{
				lSQL.Append(" and convert( varchar(10), i.interviewTime,120)<=@end_time");
			}


			if(htbCondition["phone"]!=null && htbCondition["phone"].ToString()!=string.Empty)
			{
				lSQL.Append(" and c.phone=@phone");
			}
			if(htbCondition["VIN"]!=null && htbCondition["VIN"].ToString()!=string.Empty)
			{
				lSQL.Append(" and ca.vin=@VIN ");
			}
			if(htbCondition["licensePlate"]!=null && htbCondition["licensePlate"].ToString()!=string.Empty)
			{
				lSQL.Append(" and ca.licensePlate=@licensePlate");
			}
			if(htbCondition["brand"]!=null && htbCondition["brand"].ToString()!=string.Empty)
			{
				lSQL.Append(" and ca.manufacturers=@brand");
			}
			if(htbCondition["contactState"]!=null && htbCondition["contactState"].ToString()!=string.Empty)
			{
				lSQL.Append(" and c.contactState=@contactState");
			}
			if(htbCondition["interviewListId"]!=null && htbCondition["interviewListId"].ToString()!=string.Empty)
			{
				lSQL.Append(" and i.interviewListId=@interviewListId");
			}

			if(htbCondition["agent_id"]!=null && htbCondition["agent_id"].ToString()!=string.Empty)
			{
				lSQL.Append(" and c.agent_id=@agent_id");
			}
			if(bQueryType==0)
			{
			}
			else
			{
				if(bQueryType==1)//是否只查询成交信息
				{
					lSQL.Append(" and isnull(i.insuranceFees,0)>0");
				}
				else
				{
					lSQL.Append(" and (isnull(i.insuranceFees,0)=0 or isnull(i.insuranceFees,0)>0)");
				}
			}
			
			lSQL.Append(" order by c.personName");

			IDbConnection Con=DBFunc.getConnection();
			using(Con)
			{
				return DBFunc.executeDataTable(Con,lSQL.ToString(),htbCondition);
			}

		}
		public static DataTable QueryXuBao(string strBeginDate,string strEndDate)
		{
			StringBuilder lSQL=new StringBuilder();
			lSQL.Append(" select i.contactState, single_date,phone,brand,model,VIN,'' AS cardNo,licensePlate,");
			lSQL.Append(" i.insuranceFees,c.forceInsur,'' as 车船, ");
			lSQL.Append(" i.returnPoint,'' as 交折,i.expire_date,agentid,i.insuranceCompany,i.remark ");
			lSQL.Append(" from interview_list i,hdb_caseInfo c ");
			lSQL.Append(" where i.personInfoID=c.id ");
			lSQL.Append(" and i.insuranceFees is not null ");
			if(strBeginDate!=null && strBeginDate!=string.Empty)
				lSQL.Append(" and convert( varchar(10),i.interviewTime,120) >='"+strBeginDate+"'");

			if(strEndDate!=null && strEndDate!=string.Empty)
				lSQL.Append(" and convert( varchar(10),i.interviewTime,120) <='"+strEndDate+"'");
			lSQL.Append("order by single_date ");
			IDbConnection Con=DBFunc.getConnection();
			using(Con)
			{
				return DBFunc.executeDataTable(Con,lSQL.ToString());
			}

		}
		public static DataTable QueryAgentKPI(string strBeginDate,string strEndDate)
		{
			StringBuilder lSQL=new StringBuilder();

			/*统计逻辑 
			外呼量=需要跟进+续保跟进+邀约跟进+暂无意向
			成功量=续保成功+邀约成功*/
//			lSQL.Append(" select a.userID,a.userName,a.call_num,a.success_num,a.meet_num, '");
//			lSQL.Append( strBeginDate +" 'as begin_date,'"+strEndDate+"' as end_date, ");
//			lSQL.Append("cast(100*a.meet_num/(case a.call_num when  0 then 1 else a.call_num end) as varchar(4))+'%' as meet_rate, ");
//			lSQL.Append("cast(100*a.success_num/(case  a.call_num  when 0 then 1 else a.call_num end) as varchar(4)) +'%' as success_rate ");
//			lSQL.Append(" from ");
//			lSQL.Append(" ( ");
//			lSQL.Append("  select  u.userID,u.userName,count(*) as call_num_total, ");
//			lSQL.Append("sum((case isnull(i.contactState,'0') when '0' then 0    when '续保成功' then 1 ");
//			lSQL.Append(" when '需要跟进' then 1   when '邀约成功' then 1  when '暂无意向' then 1 else 0 end)) as call_num,");//
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '邀约成功' then 1 else 0 end)) as meet_num,");
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '续保成功' then 1 when '邀约成功' then 1 else 0 end)) as success_num");
//			//lSQL.Append(" sum((case isnull(i.insuranceFees,0) when 0 then 0 else 1 end)) as success_num ");
//			lSQL.Append(" from interview_list i,hdb_user u ");
//			lSQL.Append(" where i.agentID=u.userid ");
//			if(strBeginDate!=null && strBeginDate!=string.Empty)
//				lSQL.Append(" and convert( varchar(10),i.interviewTime,120) >='"+strBeginDate+"'");
//
//			if(strEndDate!=null && strEndDate!=string.Empty)
//				lSQL.Append(" and convert( varchar(10),i.interviewTime,120) <='"+strEndDate+"'");
//			lSQL.Append(" group by u.userID,u.userName ");
//			lSQL.Append(" )a ");

			/*统计逻辑 
			外呼数量=暂无意向+续保跟进+客户到店+续保成功+需要跟进
			成功到店量=客户到店
			续保成功量=续保成功
			到店率=（客户到店+续保成功）/外呼数量 */

			/*新统计
			 * 外乎量＝需要跟进＋续保跟进＋邀约跟进＋暂无意向
			成功量＝续保成功＋客户到店
			致电量=需要跟进+邀约跟进+暂无意向+续保成功+客户到店
			 */
//			lSQL.Append(" select a.userID,a.userName,a.call_num,a.success_num,a.meet_num,a.arrive_num,a.renewal_num,a.calls_num, '");
//			lSQL.Append( strBeginDate +" 'as begin_date,'"+strEndDate+"' as end_date, ");
//			lSQL.Append("cast(100*a.meet_num/(case a.call_num when  0 then 1 else a.call_num end) as varchar(4))+'%' as meet_rate, ");
//			lSQL.Append("cast(100*(arrive_num+renewal_num)/(case a.call_num when  0 then 1 else a.call_num end) as varchar(4))+'%' as arrive_rate, ");
//			lSQL.Append("cast(100*a.success_num/(case  a.call_num  when 0 then 1 else a.call_num end) as varchar(4)) +'%' as success_rate ");
//			lSQL.Append(" from ");
//			lSQL.Append(" ( ");
//			lSQL.Append("  select  u.userID,u.userName, ");
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0    when '续保成功' then 1 ");
//			lSQL.Append(" when '需要跟进' then 1   when '续保跟进' then 1  when '暂无意向' then 1 when '客户到店' then 1 else 0 end)) as call_num,");
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '客户到店' then 1 else 0 end)) as arrive_num,");
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '续保成功' then 1 else 0 end)) as renewal_num,");
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '邀约成功' then 1 else 0 end)) as meet_num,");
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0    when '邀约跟进' then 1 ");
//			lSQL.Append(" when '需要跟进' then 1   when '续保成功' then 1 when '客户到店' then 1  when '暂无意向' then 1 else 0 end)) as calls_num,");
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '续保成功' then 1 when '客户到店' then 1 else 0 end)) as success_num");
//			//lSQL.Append(" sum((case isnull(i.insuranceFees,0) when 0 then 0 else 1 end)) as success_num ");
//			lSQL.Append(" from interview_list i,hdb_user u ");
//			lSQL.Append(" where i.agentID=u.userid ");
//			if(strBeginDate!=null && strBeginDate!=string.Empty)
//				lSQL.Append(" and convert( varchar(10),i.interviewTime,120) >='"+strBeginDate+"'");
//
//			if(strEndDate!=null && strEndDate!=string.Empty)
//				lSQL.Append(" and convert( varchar(10),i.interviewTime,120) <='"+strEndDate+"'");
//			lSQL.Append(" group by u.userID,u.userName ");
//			lSQL.Append(" )a ");


			/*1119
			 外呼总量=暂无意向+需要跟进+客户预约+续保跟进
								成功率=客户到店/外呼总量
								无效电话=空号错号+占线关机+无人接听
							
			 */
//			lSQL.Append(" select a.userID,a.userName,a.call_num,a.success_num,a.meet_num,a.arrive_num,a.renewal_num,a.calls_num,a.invalid_num,a.zwyx_num,a.xygj_num,a.khyy_num,a.xbgj_num, '");
//			lSQL.Append( strBeginDate +" 'as begin_date,'"+strEndDate+"' as end_date, ");
//			lSQL.Append("cast(100*a.meet_num/(case a.call_num when  0 then 1 else a.call_num end) as varchar(4))+'%' as meet_rate, ");
//			lSQL.Append("cast(100*(arrive_num+renewal_num)/(case a.call_num when  0 then 1 else a.call_num end) as varchar(4))+'%' as arrive_rate, ");
//			lSQL.Append("cast(100*a.arrive_num/(case  a.call_num  when 0 then 1 else a.call_num end) as varchar(4)) +'%' as success_rate ");//成功率
//			lSQL.Append(" from ");
//			lSQL.Append(" ( ");
//			lSQL.Append("  select  u.userID,u.userName, ");
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0    when '暂无意向' then 1 ");
//			lSQL.Append(" when '需要跟进' then 1   when '客户预约' then 1  when '续保跟进' then 1 else 0 end)) as call_num,");//外呼总量
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '客户到店' then 1 else 0 end)) as arrive_num,");//客户到店
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '暂无意向' then 1 else 0 end)) as zwyx_num,");//暂无意向
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '需要跟进' then 1 else 0 end)) as xygj_num,");//需要跟进
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '客户预约' then 1 else 0 end)) as khyy_num,");//客户预约
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '续保跟进' then 1 else 0 end)) as xbgj_num,");//续保跟进
//
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '续保成功' then 1 else 0 end)) as renewal_num,");
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '邀约成功' then 1 else 0 end)) as meet_num,");
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '空号错号' then 1 when '占线关机' then 1 when '无人接听' then 1 else 0 end)) as invalid_num,");//无效电话
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0    when '邀约跟进' then 1 ");
//			lSQL.Append(" when '需要跟进' then 1   when '续保成功' then 1 when '客户到店' then 1  when '暂无意向' then 1 else 0 end)) as calls_num,");
//			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '续保成功' then 1 when '客户到店' then 1 else 0 end)) as success_num");
//			//lSQL.Append(" sum((case isnull(i.insuranceFees,0) when 0 then 0 else 1 end)) as success_num ");
//			lSQL.Append(" from interview_list i,hdb_user u ");
//			lSQL.Append(" where i.agentID=u.userid ");
//			if(strBeginDate!=null && strBeginDate!=string.Empty)
//				lSQL.Append(" and convert( varchar(10),i.interviewTime,120) >='"+strBeginDate+"'");
//
//			if(strEndDate!=null && strEndDate!=string.Empty)
//				lSQL.Append(" and convert( varchar(10),i.interviewTime,120) <='"+strEndDate+"'");
//			lSQL.Append(" group by u.userID,u.userName ");
//			lSQL.Append(" )a ");


			lSQL.Append(" select a.userID,a.userName,a.call_num,a.success_num,a.meet_num,a.arrive_num,a.renewal_num,a.calls_num,a.invalid_num,a.zwyx_num,a.xygj_num,a.khyy_num,a.xbgj_num, '");
			lSQL.Append( strBeginDate +" 'as begin_date,'"+strEndDate+"' as end_date, ");
			lSQL.Append("cast(100*a.meet_num/(case a.call_num when  0 then 1 else a.call_num end) as varchar(4))+'%' as meet_rate, ");
			lSQL.Append("cast(100*(arrive_num+renewal_num)/(case a.call_num when  0 then 1 else a.call_num end) as varchar(4))+'%' as arrive_rate, ");
			lSQL.Append("cast(100*a.arrive_num/(case  a.call_num  when 0 then 1 else a.call_num end) as varchar(4)) +'%' as success_rate ");//成功率
			lSQL.Append(" from ");
			lSQL.Append(" ( ");
			lSQL.Append("  select  u.userID,u.userName, ");
			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0    when '暂无意向' then 1 ");
			lSQL.Append(" when '需要跟进' then 1   when '客户预约' then 1  when '续保跟进' then 1 else 0 end)) as call_num,");//外呼总量
			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '客户到店' then 1 else 0 end)) as arrive_num,");//客户到店
			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '暂无意向' then 1 else 0 end)) as zwyx_num,");//暂无意向
			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '需要跟进' then 1 else 0 end)) as xygj_num,");//需要跟进
			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '客户预约' then 1 else 0 end)) as khyy_num,");//客户预约
			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '续保跟进' then 1 else 0 end)) as xbgj_num,");//续保跟进

			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '续保成功' then 1 else 0 end)) as renewal_num,");
			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '邀约成功' then 1 else 0 end)) as meet_num,");
			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '空号错号' then 1 when '占线关机' then 1 when '无人接听' then 1 else 0 end)) as invalid_num,");//无效电话
			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0    when '邀约跟进' then 1 ");
			lSQL.Append(" when '需要跟进' then 1   when '续保成功' then 1 when '客户到店' then 1  when '暂无意向' then 1 else 0 end)) as calls_num,");
			lSQL.Append(" sum((case isnull(i.contactState,'0') when '0' then 0 when '续保成功' then 1 when '客户到店' then 1 else 0 end)) as success_num");
			lSQL.Append(" from (select convert( varchar(10),interviewTime,120) interviewTime,contactState,agentID,personInfoId  from interview_list where count_type='1' group by convert( varchar(10),interviewTime,120),contactState,agentID,personInfoId) i,hdb_user u ");
			lSQL.Append(" where i.agentID=u.userid ");
			if(strBeginDate!=null && strBeginDate!=string.Empty)
				lSQL.Append(" and i.interviewTime >='"+strBeginDate+"'");

			if(strEndDate!=null && strEndDate!=string.Empty)
				lSQL.Append(" and i.interviewTime <='"+strEndDate+"'");
			lSQL.Append(" group by u.userID,u.userName ");
			lSQL.Append(" )a ");

			IDbConnection Con=DBFunc.getConnection();
			using(Con)
			{
				return DBFunc.executeDataTable(Con,lSQL.ToString());
			}

		}



		/// <summary>
		/// 判断上次客户到店业务是否在15天以内
		/// </summary>
		/// <returns></returns>
		public static DataTable JudgeCountType(string personInfoId,string interviewTime)
		{
			StringBuilder lSQL=new StringBuilder();
			lSQL.Append(" select * ");
			lSQL.Append("   from interview_list ");
			lSQL.Append("  where interviewListId = (select MAX(interviewListId) interviewListId ");
			lSQL.Append("                             from interview_list i ");
			lSQL.Append("                            where i.contactState = '客户到店' ");
			lSQL.Append("                              and i.personInfoId = "+personInfoId+" ");
			lSQL.Append("                              and i.count_type = '1') ");
			lSQL.Append("    and DATEDIFF(DAY, convert(varchar(10), interviewTime, 120), '"+interviewTime+"') <= 15 ");


			IDbConnection Con=DBFunc.getConnection();
			using(Con)
			{
				return DBFunc.executeDataTable(Con,lSQL.ToString());
			}
		}


		/// <summary>
		/// 更新联系情况
		/// </summary>
		/// <param name="strInterViewID"></param>
		/// <param name="htbCaseInfo"></param>
		/// <returns></returns>
		public static int UpdateInterViewByID(string strInterViewID,Hashtable htbInterView)
		{
			int bRet=0;
			StringBuilder lSQL=new StringBuilder();
			//lSQL.Append("update interview_list set  comment=@comment where interviewListId="+strInterViewID);

			lSQL.Append("update interview_list ");
			lSQL.Append("set comment=@comment,");
			lSQL.Append(" profit=@profit,");
			lSQL.Append(" rebate=@rebate,");
			lSQL.Append(" insuranceCompany=@insuranceCompany,");
			lSQL.Append(" returnPoint=@returnPoint,");
			lSQL.Append(" insuranceFees=@insuranceFees,");
			lSQL.Append(" service_type=@service_type,");
			lSQL.Append(" insurancePoint=@insurancePoint,");
			lSQL.Append(" trafficPoint=@trafficPoint,");
			lSQL.Append(" travelTax=@travelTax,");
			lSQL.Append(" failedReason=@failedReason,");
			lSQL.Append(" contactState=@contactState,");
			lSQL.Append(" expire_date=@expire_date,");
			lSQL.Append(" single_date=@single_date,");
			lSQL.Append(" view_time=@view_time,");
			lSQL.Append(" view_date=@view_date,");
			lSQL.Append(" forceInsur=@forceInsur");
			lSQL.Append(" where interviewListId="+strInterViewID);

			StringBuilder lSQLCase=new StringBuilder();
			lSQLCase.Append(" UPDATE hdb_caseInfo ");
			lSQLCase.Append(" SET  [personName]=@personName , ");
			lSQLCase.Append("  [gender]=@gender , ");
			lSQLCase.Append("  [phone]=@phone ,  ");
			lSQLCase.Append(" [area]=@area, ");
			lSQLCase.Append("  [address]=@address, ");
			lSQLCase.Append("  [brand]=@brand, ");
			lSQLCase.Append("  [model]=@model, ");
			lSQLCase.Append("  [vin]=@vin, ");
			lSQLCase.Append("  [licensePlate]=@licensePlate, ");
			lSQLCase.Append("  [contactState]=@contactState, ");
			lSQLCase.Append("  [failedReason]=@failedReason, ");
			lSQLCase.Append("  [introducer]=@introducer, ");
			lSQLCase.Append("   [lastUpdate]=getdate(),  ");
			lSQLCase.Append("   [expire_date]=@expire_date, ");
			lSQLCase.Append("   [salesdate]=@salesdate, ");
			lSQLCase.Append("  [insuranceCompany]=@insuranceCompany, ");
			lSQLCase.Append("  [insuranceFees]=@insuranceFees, ");
			lSQLCase.Append("  [returnPoint]=@returnPoint, ");
			lSQLCase.Append("  [profit]=@profit, ");
			lSQLCase.Append("  [forceInsur]=@forceInsur, ");
			lSQLCase.Append("  [rebate]=@rebate, ");
			lSQLCase.Append("  [companyName]=@companyName, ");
			lSQLCase.Append("  [idcard]=@idcard, ");
			lSQLCase.Append("  [birthday]=@birthday, ");
			lSQLCase.Append("  [customerType]=@customerType ");
			lSQLCase.Append(" WHERE id=@id ");

			StringBuilder lSQLCar=new StringBuilder();

			lSQLCar.Append(" UPDATE Car_info ");
			lSQLCar.Append(" SET  [manufacturers]=@brand, ");
			lSQLCar.Append("  [car_model]=@model, ");
			lSQLCar.Append("  [vin]=@vin, ");
			lSQLCar.Append("  [licensePlate]=@licensePlate, ");
			lSQLCar.Append("  [salesDate]=@salesDate, ");
			lSQLCar.Append("  [expire_date]=@expire_date, ");
			lSQLCar.Append("  [car_type]=@car_type, ");
			lSQLCar.Append("  [engine_no]=@engine_no ");
			lSQLCar.Append(" WHERE id=@carid ");
		

			IDbConnection Con=DBFunc.getConnection();
			IDbTransaction trans=Con.BeginTransaction();
			try
			{
				bRet=DBFunc.executeNonQuery(trans,lSQL.ToString(),htbInterView);
				DBFunc.executeNonQuery(trans,lSQLCase.ToString(),htbInterView);
				DBFunc.executeNonQuery(trans,lSQLCar.ToString(),htbInterView);
				trans.Commit();

			}
			catch(Exception ex)
			{
				trans.Rollback();
				bRet=0;
				throw new Exception("更新客户信息出错："+ex.Message);
			}
			finally
			{
				Con.Close();
				Con.Dispose();
			}
			return bRet;
		}
		public static bool SaveInterView(Hashtable htbCaseInfo)
		{
			bool bRet=true;

			StringBuilder lSQL=new StringBuilder();

			lSQL.Append(" INSERT INTO  interview_list( [personInfoId], [interviewTime], [agentId], [comment],[insurancePoint],[service_type], ");
			lSQL.Append(" [createTime], [insuranceFees], [returnPoint],[count_type], ");
			lSQL.Append("  [insuranceCompany], [profit],[rebate],[forceInsur],  [view_date], ");
		//	lSQL.Append("  [view_time], [single_date],[expire_date],[contactState],[failedReason]) ");
			lSQL.Append("  [view_time], [single_date],[expire_date],[contactState],[failedReason],[carId],[travelTax],[trafficPoint]");
			lSQL.Append(" ,[Fee_CheSun],[Fee_HuaHen],[Fee_SanZhe],[Fee_DaoCheJing],[Fee_RenYuan],[Fee_BoLi],[Fee_DaoQiang],");
			lSQL.Append(" [Fee_SheShui],[Fee_BuJiMianPei],[Fee_ZiRan]) ");
			lSQL.Append(" VALUES( @Id, @interviewTime, @agent_id, @comment,@insurancePoint,@service_type, ");
			lSQL.Append(" getdate(), @insuranceFees, @returnPoint,@count_type, ");
			lSQL.Append("  @insuranceCompany, @profit,@rebate,@forceInsur, @view_date,@view_time, @single_date,@expire_date,@contactState,@failedReason,@carId,@travelTax,@trafficPoint");
			lSQL.Append(" ,@Fee_CheSun,@Fee_HuaHen,@Fee_SanZhe,@Fee_DaoCheJing,@Fee_RenYuan,@Fee_BoLi,@Fee_DaoQiang");
			lSQL.Append(",@Fee_SheShui,@Fee_BuJiMianPei,@Fee_ZiRan) ");


			StringBuilder sbUpdate=new StringBuilder();



			sbUpdate.Append(" UPDATE hdb_caseInfo ");
			sbUpdate.Append(" SET  [personName]=@personName , ");
			sbUpdate.Append("  [gender]=@gender , ");
			sbUpdate.Append("  [phone]=@phone ,  ");
			sbUpdate.Append(" [area]=@area, ");
			sbUpdate.Append("  [address]=@address, ");
			sbUpdate.Append("  [brand]=@brand, ");
			sbUpdate.Append("  [model]=@model, ");
			sbUpdate.Append("  [vin]=@vin, ");
			sbUpdate.Append("  [birthday]=@birthday, ");			
			sbUpdate.Append("  [customer_level]=@customer_level, ");
			sbUpdate.Append("  [licensePlate]=@licensePlate, ");
			sbUpdate.Append("  [contactState]=@contactState, ");
			sbUpdate.Append("  [failedReason]=@failedReason, ");
			sbUpdate.Append("  [introducer]=@introducer, ");
			sbUpdate.Append("   [lastUpdate]=getdate(),  ");
			sbUpdate.Append("   [expire_date]=@expire_date, ");
			sbUpdate.Append("   [salesdate]=@salesdate, ");
			sbUpdate.Append("  [insuranceCompany]=@insuranceCompany, ");
			sbUpdate.Append("  [insuranceFees]=@insuranceFees, ");
			sbUpdate.Append("  [returnPoint]=@returnPoint, ");
			sbUpdate.Append("  [profit]=@profit, ");
			sbUpdate.Append("  [forceInsur]=@forceInsur, ");
			sbUpdate.Append("  [rebate]=@rebate, ");
			sbUpdate.Append("  [companyName]=@companyName, ");
			sbUpdate.Append("  [agent_id]=@agent_ids, ");
			sbUpdate.Append("  [idcard]=@idcard, ");
		//	sbUpdate.Append("  [birthday]=@birthday, ");
			sbUpdate.Append("  [customerType]=@customerType ");
			sbUpdate.Append(" WHERE id=@id ");

			StringBuilder sbCarUpdate=new StringBuilder();

			sbCarUpdate.Append(" UPDATE Car_info ");
			sbCarUpdate.Append(" SET  [manufacturers]=@brand, ");
			sbCarUpdate.Append("  [car_model]=@model, ");
			sbCarUpdate.Append("  [vin]=@vin, ");
			sbCarUpdate.Append("  [shape_colors]=@shape_colors, ");
			sbCarUpdate.Append("  [licensePlate]=@licensePlate, ");
			sbCarUpdate.Append("  [salesDate]=@salesDate, ");
			sbCarUpdate.Append("  [expire_date]=@expire_date, ");
//			if(htbCaseInfo["keep_date"].ToString()!=string.Empty)
//			{
//				sbCarUpdate.Append("  [keep_date]=@keep_date, ");
//			}
			sbCarUpdate.Append("  [engine_no]=@engine_no ");
			sbCarUpdate.Append(" WHERE id=@carId ");

			IDbConnection Con=DBFunc.getConnection();
			IDbTransaction trans=Con.BeginTransaction();

			try
			{
				DBFunc.executeNonQuery(trans,lSQL.ToString(),htbCaseInfo);
				DBFunc.executeNonQuery(trans,sbUpdate.ToString(),htbCaseInfo);
				DBFunc.executeNonQuery(trans,sbCarUpdate.ToString(),htbCaseInfo);
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				bRet=false;
				throw new Exception("更新客户信息出错："+ex.Message);
			}
			finally
			{
				Con.Close();
				Con.Dispose();
			}
			return bRet;

			//lSQL.Append(" insert into interviewList
		}

		public static int existsPhone(string strPhone)
		{

			IDbConnection Con=DBFunc.getConnection();
		 

			try
			{
				string strQuery="select * from hdb_caseInfo where phone='"+strPhone+"'";
				DataTable dt= DBFunc.executeDataTable(Con,strQuery);
				return dt.Rows.Count;
			}
			catch(Exception ex)
			{
				
				throw new Exception("查询客户信息出错："+ex.Message);
			}
			finally
			{
				Con.Close();
				Con.Dispose();
			}
		}
		public static int ImportCustomerInfo(DataTable dt)
		{

			IDbConnection Con=DBFunc.getConnection();
			IDbTransaction trans=Con.BeginTransaction();

			try
			{
				int nRet =BizFileHelper.InsertData((SqlTransaction) trans,"hdb_caseInfo",dt);
				trans.Commit();
				return nRet;
			}
			catch(Exception ex)
			{
				trans.Rollback();
				throw new Exception("导入客户信息出错："+ex.Message);
			}
			finally
			{
				Con.Close();
				Con.Dispose();
			}

			
		}
		public static bool InsertCustomer(Hashtable htbCustomer)
		{
			bool bRet=true;
			StringBuilder lSQL=new StringBuilder();
//			lSQL.Append(" INSERT INTO  [hdb_caseInfo](  [personName], [gender], [phone], [area], [address], ");
//			lSQL.Append("  [brand], [model], [vin], [licensePlate], [salesDate], ");
//			lSQL.Append("  [contactState], [failedReason], [introducer],[createTime], [lastUpdate], ");
//			lSQL.Append("  [expire_Date],  [companyName],[deleteflag],[customerType]) ");
//			lSQL.Append(" VALUES(@personName,@gender,@phone,@area,@address, ");
//			lSQL.Append(" @brand,@model,@vin,@licensePlate,@salesDate, ");
//			lSQL.Append(" @contactState,@failedReason,@introducer,getDate(),getDate(), ");
//			lSQL.Append(" @expire_Date,@companyName,0,@customerType) ");

			lSQL.Append(" INSERT INTO  [hdb_caseInfo](  [personName], [gender], [phone], [area], [address],[idcard],[birthday], ");
			lSQL.Append("  [contactState], [failedReason], [introducer],[createTime], [lastUpdate],[customer_level], ");
			lSQL.Append("  [companyName],[deleteflag],[customerType]) ");
			lSQL.Append(" VALUES(@personName,@gender,@phone,@area,@address,@idcard,@birthday, ");
			lSQL.Append(" @contactState,@failedReason,@introducer,getDate(),getDate(),@customer_level, ");
			lSQL.Append(" @companyName,0,@customerType) ");

			IDbConnection Con=DBFunc.getConnection();
			IDbTransaction trans=Con.BeginTransaction();

			try
			{
				DBFunc.executeNonQuery(trans,lSQL.ToString(),htbCustomer);
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				bRet=false;
				throw new Exception("新建客户信息出错："+ex.Message);
			}
			finally
			{
				Con.Close();
				Con.Dispose();
			}
			return bRet;

		}

		public static bool ModifyCustomer(Hashtable htbCustomer)
		{
			bool bRet=true;
			StringBuilder sbUpdate=new StringBuilder();

//			sbUpdate.Append(" UPDATE hdb_caseInfo ");
//			sbUpdate.Append(" SET  [personName]=@personName , ");
//			sbUpdate.Append("  [gender]=@gender , ");
//			sbUpdate.Append("  [phone]=@phone ,  ");
//			sbUpdate.Append(" [area]=@area, ");
//			sbUpdate.Append("  [address]=@address, ");
//			sbUpdate.Append("  [brand]=@brand, ");
//			sbUpdate.Append("  [model]=@model, ");
//			sbUpdate.Append("  [vin]=@vin, ");
//			sbUpdate.Append("  [licensePlate]=@licensePlate, ");
//			//sbUpdate.Append(" [salesDate]=@salesDate, ");
//			sbUpdate.Append("  [contactState]=@contactState, ");
//			sbUpdate.Append("  [failedReason]=@failedReason, ");
//			sbUpdate.Append("  [introducer]=@introducer, ");
//			sbUpdate.Append("   [lastUpdate]=getdate(),  ");
//			sbUpdate.Append("   [expire_date]=@expire_date, ");
//			sbUpdate.Append("	[salesDate]=@salesDate,");
////			sbUpdate.Append("  [insuranceCompany]=@insuranceCompany, ");
////			sbUpdate.Append("  [insuranceFees]=@insuranceFees, ");
////			sbUpdate.Append("  [returnPoint]=@returnPoint, ");
//			sbUpdate.Append("  [companyName]=@companyName ,");
//			sbUpdate.Append("  [customerType]=@customerType ");
////			sbUpdate.Append("  [agent_id]=@agent_id ");
//			sbUpdate.Append(" WHERE id=@id ");


			sbUpdate.Append(" UPDATE hdb_caseInfo ");
			sbUpdate.Append(" SET  [personName]=@personName , ");
			sbUpdate.Append("  [gender]=@gender , ");
			sbUpdate.Append("  [phone]=@phone ,  ");
			sbUpdate.Append(" [area]=@area, ");
			sbUpdate.Append("  [address]=@address, ");
			sbUpdate.Append("  [idcard]=@idcard, ");
			sbUpdate.Append("  [birthday]=@birthday, ");
			sbUpdate.Append("  [customer_level]=@customer_level, ");
			
			sbUpdate.Append("  [contactState]=@contactState, ");
			sbUpdate.Append("  [failedReason]=@failedReason, ");
			sbUpdate.Append("  [introducer]=@introducer, ");
			sbUpdate.Append("   [lastUpdate]=getdate(),  ");
			sbUpdate.Append("  [companyName]=@companyName ,");
			sbUpdate.Append("  [customerType]=@customerType ");
			sbUpdate.Append(" WHERE id=@id ");

			IDbConnection Con=DBFunc.getConnection();
			IDbTransaction trans=Con.BeginTransaction();

			try
			{
				DBFunc.executeNonQuery(trans,sbUpdate.ToString(),htbCustomer);
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				bRet=false;
				throw new Exception("更新客户信息出错："+ex.Message);
			}
			finally
			{
				Con.Close();
				Con.Dispose();
			}
			return bRet;

			//lSQL.Append(" insert into interviewList
		}



		public static bool InsertCar(Hashtable htbCar)
		{
			bool bRet=true;
			StringBuilder lSQL=new StringBuilder();
		


			lSQL.Append(" INSERT INTO  [Car_info](   [licensePlate], [personId], [Vin], [engine_no],[shape_colors], ");
			lSQL.Append("  [car_model], [salesdate], [manufacturers],[average_mileage], [expire_date],[keep_date],[car_type], ");
			lSQL.Append("  [current_mileage]) ");
			lSQL.Append(" VALUES(@licensePlate,@personId,@VIN,@engine_no,@shape_colors, ");
			lSQL.Append(" @car_model,@salesDate,@manufacturers,@average_mileage,@expire_date,@keep_date,@car_type, ");
			lSQL.Append(" @current_mileage) ");

			IDbConnection Con=DBFunc.getConnection();
			IDbTransaction trans=Con.BeginTransaction();

			try
			{
				DBFunc.executeNonQuery(trans,lSQL.ToString(),htbCar);
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				bRet=false;
				throw new Exception("新建车辆信息出错："+ex.Message);
			}
			finally
			{
				Con.Close();
				Con.Dispose();
			}
			return bRet;

		}

		public static bool ModifyCar(Hashtable htbCar)
		{
			bool bRet=true;

			StringBuilder sbUpdate=new StringBuilder();

			sbUpdate.Append(" UPDATE Car_info ");
			sbUpdate.Append(" SET  [licensePlate]=@licensePlate , ");
			sbUpdate.Append("  [Vin]=@VIN , ");
			sbUpdate.Append("  [engine_no]=@engine_no ,  ");
			sbUpdate.Append("  [shape_colors]=@shape_colors ,  ");	
			sbUpdate.Append(" [car_model]=@car_model, ");
			sbUpdate.Append("  [salesdate]=@salesDate, ");
			sbUpdate.Append("  [manufacturers]=@manufacturers, ");
			sbUpdate.Append("  [average_mileage]=@average_mileage, ");
			sbUpdate.Append("  [expire_date]=@expire_date, ");
			sbUpdate.Append("  [keep_date]=@keep_date, ");
			sbUpdate.Append("  [car_type]=@car_type, ");
			sbUpdate.Append("   [current_mileage]=@current_mileage  ");
		
			sbUpdate.Append(" WHERE id=@id ");

			IDbConnection Con=DBFunc.getConnection();
			IDbTransaction trans=Con.BeginTransaction();

			try
			{
				DBFunc.executeNonQuery(trans,sbUpdate.ToString(),htbCar);
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				bRet=false;
				throw new Exception("更新车辆信息出错："+ex.Message);
			}
			finally
			{
				Con.Close();
				Con.Dispose();
			}
			return bRet;

			
		}
	}
}
