using System;
using System.Text;
using System.Collections;
using CaseyLib;
using CaseyLib.util;
using System.Data;
namespace hxyd_biz
{
	/// <summary>
	/// InsurCompany 的摘要说明。
	/// </summary>
	public class InsurCompany
	{
		public InsurCompany()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//

		}

		public static DataTable GetInsurCompanyInfo(string strCompanyID)
		{
			DataTable dt=null;
			StringBuilder lSQL=new StringBuilder();
			lSQL.Append(" SELECT [insurCompanyID], [insurCompanyName], [rebate], [remark]");
			lSQL.Append("  FROM [insurCompany] ");

			lSQL.Append( " where 1=1 ");
			if(strCompanyID!=null && strCompanyID!=string.Empty)
				lSQL.Append(" and insurCompanyID='"+strCompanyID+"'");
			lSQL.Append(" order by  insurCompanyID ");
			using (IDbConnection con=DBFunc.getConnection())
			{
				dt =DBFunc.executeDataTable(con,lSQL.ToString());
			}
			return dt;
		}


		public static bool  InsertCompany(Hashtable htbCompany)
		{
			bool bRet=true;

			StringBuilder lSQL=new StringBuilder();
			lSQL.Append("INSERT INTO  [InsurCompany](  [InsurCompanyName], [rebate], [remark])");
			lSQL.Append("VALUES(@InsurCompanyName,@rebate,@remark)");


			IDbConnection Con=DBFunc.getConnection();
			IDbTransaction trans=Con.BeginTransaction();

			try
			{
				DBFunc.executeNonQuery(trans,lSQL.ToString(),htbCompany);
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				bRet=false;
				throw new Exception("插入保险公司信息出错："+ex.Message);
			}
			finally
			{
				Con.Close();
				Con.Dispose();
			}
			return bRet;
		}
		public static bool UpdateInsurCompany(Hashtable htbCompany)
		{
			bool bRet=true;
			if(htbCompany["insurCompanyID"]==null || htbCompany["insurCompanyID"].ToString()==string.Empty)
				throw new Exception("更新保险信息出错：公司ID不能为空!");


			StringBuilder lSQL=new StringBuilder();
			lSQL.Append(" update insurCompany  set ");
			lSQL.Append(" insurCompanyName=@insurCompanyName ");
			lSQL.Append(" ,rebate=@rebate");
			lSQL.Append(",remark=@remark");
			
			lSQL.Append(" where insurCompanyID=@insurCompanyID ");

			IDbConnection Con=DBFunc.getConnection();
			IDbTransaction trans=Con.BeginTransaction();

			try
			{
				DBFunc.executeNonQuery(trans,lSQL.ToString(),htbCompany);
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
		}

		public static bool UpdateMileage(Hashtable htMileage)
		{
			bool bRet=true;
			if(htMileage["id"]==null || htMileage["id"].ToString()==string.Empty)
				throw new Exception("更新里程信息出错：品牌ID不能为空!");


			StringBuilder lSQL=new StringBuilder();
			lSQL.Append(" update brandinfo  set ");
			lSQL.Append(" brandNameEN=@brandNameEN ");
			lSQL.Append(" ,brandNameCN=@brandNameCN");
			lSQL.Append(",mileage=@mileage");
			
			lSQL.Append(" where id=@id ");

			IDbConnection Con=DBFunc.getConnection();
			IDbTransaction trans=Con.BeginTransaction();

			try
			{
				DBFunc.executeNonQuery(trans,lSQL.ToString(),htMileage);
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				bRet=false;
				throw new Exception("更新里程信息出错："+ex.Message);
			}
			finally
			{
				Con.Close();
				Con.Dispose();
			}
			return bRet;
		}

		public static DataTable GetMileageInfo(string strID)
		{
			DataTable dt=null;
			StringBuilder lSQL=new StringBuilder();
			lSQL.Append(" SELECT [id], [brandNameEN], [brandNameCN], [mileage]");
			lSQL.Append("  FROM [brandinfo] ");

			lSQL.Append( " where 1=1 ");
			if(strID!=null && strID!=string.Empty)
				lSQL.Append(" and id='"+strID+"'");
			lSQL.Append(" order by  id ");
			using (IDbConnection con=DBFunc.getConnection())
			{
				dt =DBFunc.executeDataTable(con,lSQL.ToString());
			}
			return dt;
		}

		


	}
}
