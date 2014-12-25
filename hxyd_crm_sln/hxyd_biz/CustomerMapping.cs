using System;
using System.Data;
using System.Reflection;
using CaseyLib.util;
using System.Text;

namespace hxyd_biz
{





		public class CustomerMapping
		{
			private CustomerMapping()
			{
			}
       
			public static string DayNum = System.Configuration.ConfigurationSettings.AppSettings["DayNum"].ToString().Trim();
			private static void init()
			{
				IDbConnection connOrTrans = null;
				try
				{
			
					StringBuilder lSQL=new StringBuilder();
//					lSQL.Append(" SELECT [id], [customerNum], [personName], [gender], [phone], [area], ");
//					lSQL.Append("  [address], [brand], [model], [vin], [licensePlate],  ");
//					lSQL.Append("  convert( varchar(10),salesDate,120) as salesDate,  ");
//					lSQL.Append(" [contactState], [failedReason], [introducer], [remark], [deleteFlag], ");
//					lSQL.Append("  [createTime], [lastUpdate], [expire_Date], [insuranceCompany], [insuranceFees], [profit], ");
//					lSQL.Append("  [forceInsur], [rebate], [returnPoint], [opreator], [companyName], [agent_id],[customerType],comment ");
//					lSQL.Append(" from hdb_caseInfo c left join ");
//					lSQL.Append(" (select a.personInfoID,a.comment ");
//					lSQL.Append(" from interview_list a,( ");
//					lSQL.Append(" select i.personInfoId, max(interviewListId) as interviewListId from interview_list i group by i.personInfoId) b ");
//					lSQL.Append(" where a.interviewListId=b.interviewListId) b ");
//					lSQL.Append(" on c.id=b.personInfoID ");
//					lSQL.Append(" where  ");
//					lSQL.Append("  (c.contactState ");
//					lSQL.Append(" in ('占线关机','需要跟进','') or c.contactState is null ) ");
//					lSQL.Append(" and  DATEDIFF(day,isnull(c.expire_date,c.salesDate),getDate())%365>240  ");
//					lSQL.Append(" order by");
//					lSQL.Append(" isnull(c.contactState,0) asc, c.lastUpdate  asc,month(isnull(c.expire_date,c.salesDate)) desc,year(isnull(c.expire_date,c.salesDate)) desc ");

					
//					lSQL.Append(" SELECT c.id,c.idcard,c.birthday, [customerNum], [personName], [gender], [phone], [area], ");
//					lSQL.Append("  [address], ca.manufacturers brand, ca.car_model model, ca.vin, ca.licensePlate,ca.id carId,ca.engine_no,  ");
//					lSQL.Append("  convert( varchar(10),ca.salesDate,120) as salesDate,  ");
//					lSQL.Append(" [contactState], [failedReason], [introducer], [remark], [deleteFlag], ");
//					lSQL.Append("  [createTime], [lastUpdate], ca.expire_Date, [insuranceCompany], [insuranceFees], [profit], ");
//					lSQL.Append("  [forceInsur], [rebate], [returnPoint], [opreator], [companyName], [agent_id],[customerType],comment,travelTax,trafficPoint ");
//					lSQL.Append(" from hdb_caseInfo c join Car_info ca on c.id=ca.personId ");
//					lSQL.Append(" left join (select a.carId,a.comment,a.travelTax,a.trafficPoint ");
//					lSQL.Append(" from interview_list a,( ");
//					lSQL.Append(" select i.carId, max(interviewListId) as interviewListId from interview_list i group by i.carId) b ");
//					lSQL.Append(" where a.interviewListId=b.interviewListId) b ");
//					//lSQL.Append(" on c.id=b.personInfoID ");
//					lSQL.Append(" on ca.id=b.carId ");
//					lSQL.Append(" where  ");
//					lSQL.Append("  (c.contactState ");
//					lSQL.Append(" in ('占线关机','需要跟进','无人接听','') or c.contactState is null ) ");
//					//lSQL.Append(" and  DATEDIFF(day,isnull(ca.expire_date,ca.salesDate),getDate())%365>200  ");
//					lSQL.Append(" and  DATEDIFF(day,isnull(ca.expire_date,ca.salesDate),getDate())%365>"+DayNum);
//					lSQL.Append(" order by");
//					lSQL.Append(" isnull(c.contactState,0) asc, c.lastUpdate  asc,month(isnull(ca.expire_date,ca.salesDate)) desc,year(isnull(ca.expire_date,ca.salesDate)) desc ");

					lSQL.Append(" SELECT c.id,c.idcard,c.birthday, [customerNum], [personName], [gender], [phone], [area],isnull(c.customer_level,'') customer_level,ca.shape_colors, ");
					lSQL.Append("  [address], ca.manufacturers brand, ca.car_model model, ca.vin, ca.licensePlate,ca.id carId,ca.engine_no,convert( varchar(10),c.birthday,120) birthday,  ");
					lSQL.Append("  convert( varchar(10),ca.salesDate,120) as salesDate,DATEDIFF(DD, ca.salesDate ,getdate()  )*average_mileage current_mileage_new,ca.average_mileage,  ");
					lSQL.Append(" [contactState], [failedReason], [introducer], [remark], c.deleteFlag, ");
					lSQL.Append("  [createTime], [lastUpdate], ca.expire_Date, [insuranceCompany], [insuranceFees], [profit], ");
					lSQL.Append("  [forceInsur], [rebate], [returnPoint], [opreator], [companyName], [agent_id],[customerType],comment,travelTax,trafficPoint ");
					lSQL.Append(" from hdb_caseInfo c join Car_info ca on c.id=ca.personId ");
					lSQL.Append(" left join (select a.carId,a.comment,a.travelTax,a.trafficPoint,hu.deleteFlag ");
					lSQL.Append(" from interview_list a,hdb_user hu,( ");
					lSQL.Append(" select i.carId, max(interviewListId) as interviewListId from interview_list i group by i.carId) b ");
					lSQL.Append(" where a.interviewListId=b.interviewListId and hu.userId=a.agentId) b ");
					lSQL.Append(" on ca.id=b.carId ");
					lSQL.Append(" where  ");
					lSQL.Append("  (c.contactState ");
					lSQL.Append(" in ('占线关机','无人接听','') or c.contactState is null or (c.contactState='需要跟进' and b.deleteFlag=1)) ");
					lSQL.Append(" and  DATEDIFF(day,isnull(ca.expire_date,ca.salesDate),getDate())%365>"+DayNum);
					lSQL.Append(" order by isNull(c.customerType,'本店客户') desc,");
					lSQL.Append(" isnull(c.contactState,0) asc, c.lastUpdate  asc,month(isnull(ca.expire_date,ca.salesDate)) desc,year(isnull(ca.expire_date,ca.salesDate)) desc ");

					connOrTrans = DBFunc.getConnection();
					
					_dtCustomer = DBFunc.executeDataTable(connOrTrans, lSQL.ToString());
					//_dtCustomer.PrimaryKey = new DataColumn[] { _dtCustomer.Columns["id"] };
					_dtCustomer.PrimaryKey = new DataColumn[] { _dtCustomer.Columns["carId"] };
				}
				catch (Exception exception)
				{
					_dtCustomer = null;
					throw exception;
				}
				finally
				{
					if (connOrTrans != null)
					{
						connOrTrans.Close();
					}
				}
			}
       
			private static DataTable _dtCustomer = null;
			private static int nCount=0;
			private static CustomerMapping _instance = new CustomerMapping();
			private static string _version = "";
       
			public DataRow this[string strID]
			{
				get
				{
					if (_dtCustomer != null)
					{
						return _dtCustomer.Rows.Find(strID);
					}
					return null;
					
				}
			}
			public DataRow getNext(string strUserID)
			{
				//if(_dtCustomer.Rows.Count<nCount ||_dtCustomer.Rows.Count==0)
				if(_dtCustomer.Rows.Count<=nCount-1 ||_dtCustomer.Rows.Count==0)
					throw new Exception("获取客户信息出错：没有客户信息了！");
//				if(_dtCustomer.Rows[nCount]["agent_id"]!=null && _dtCustomer.Rows[nCount]["agent_id"].ToString()!=string.Empty)
//				{
//					if(_dtCustomer.Rows[nCount]["agent_id"].ToString()!=strUserID)
//					{
//						nCount++;
//						this.getNext(strUserID);
//					}
//				}
				if(_dtCustomer.Rows[nCount]["agent_id"]!=null && _dtCustomer.Rows[nCount]["agent_id"].ToString()!=string.Empty&&_dtCustomer.Rows[nCount]["contactState"].ToString()!="需要跟进")
				{
					if(_dtCustomer.Rows[nCount]["agent_id"].ToString()!=strUserID)
					{
						nCount++;
						this.getNext(strUserID);
					}
				}
				DataRow dr=_dtCustomer.Rows[nCount];
				nCount++;
				return dr;
				
			}
       
			public static CustomerMapping getInstance()
			{
				string str ="1";
				if ((_dtCustomer == null) || (_version != str))	
				{
					lock (_instance)
					{
						if ((_dtCustomer == null) || (_version != str))
							if(_dtCustomer==null)
							{
								init();
								_version = str;
							}
					}
				}
				init();
				return _instance;
			}

    
		}





}
