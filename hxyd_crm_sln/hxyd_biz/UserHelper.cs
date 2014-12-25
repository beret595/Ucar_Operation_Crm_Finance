using System;
using System.Text;
using System.Collections;
using CaseyLib;
using CaseyLib.util;
using System.Data;

namespace hxyd_biz
{
	/// <summary>
	/// User ��ժҪ˵����
	/// </summary>
	public class UserHelper
	{
		public UserHelper()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public static DataTable GetUserInfo(string strUserName,string strUserID)
		{
			DataTable dt=null;
			StringBuilder lSQL=new StringBuilder();
			lSQL.Append(" SELECT [userId], [userName], [userPassword], [role], [remark],[email],");
			lSQL.Append(" (case when (deleteFlag=0) then '����' else 'ɾ��' end) as delflag, ");
			lSQL.Append("  [createTime], [lastUpdate], [fullName], [phone] FROM [hdb_user] ");

			lSQL.Append( " where 1=1 ");
			if(strUserName!=null && strUserName!=string.Empty)
				lSQL.Append(" and userName='"+strUserName+"'");
			if(strUserID!=null && strUserID!=string.Empty)
				lSQL.Append(" and UserID='"+strUserID+"'");
			lSQL.Append(" order by createTime desc ");
			using (IDbConnection con=DBFunc.getConnection())
			{
				dt =DBFunc.executeDataTable(con,lSQL.ToString());
			}
			return dt;
		}
		public static DataTable GetUserInfo(string strUserName)
		{
			return GetUserInfo(strUserName,null);
		}
		public static DataTable GetUserInfoByUserID(string strUserID)
		{
			return GetUserInfo(null,strUserID);
		}

		public static bool DeleteUser(string strUserID)
		{
			bool bRet=true;
			if(strUserID==null || strUserID==string.Empty)
				throw new Exception("ɾ���û���Ϣ�����û�ID����Ϊ��!");
			string strDel="update hdb_user set deleteflag=1 where userid="+strUserID;
			
			IDbConnection Con=DBFunc.getConnection();
			IDbTransaction trans=Con.BeginTransaction();

			try
			{
				DBFunc.executeNonQuery(trans,strDel.ToString());
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				bRet=false;
				throw new Exception("ɾ���û���Ϣ����"+ex.Message);
			}
			finally
			{
				Con.Close();
				Con.Dispose();
			}
			return bRet;

		}

		public static bool ModifyPassword(string strNewPass,string strUserID)
		{
			if(strUserID==null || strUserID==string.Empty)
				throw new Exception("�޸�����ʧ�ܣ��û�ID����Ϊ��!");
			string strUpdate=" update hdb_user set userPassword='"+strNewPass+"' where userid="+strUserID;
			bool bRet =true;
			IDbConnection Con=DBFunc.getConnection();
			IDbTransaction trans=Con.BeginTransaction();

			try
			{
				DBFunc.executeNonQuery(trans,strUpdate.ToString());
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				bRet=false;
				throw new Exception("ɾ���û���Ϣ����"+ex.Message);
			}
			finally
			{
				Con.Close();
				Con.Dispose();
			}
			return bRet;
		}

		public static bool  InsertUser(Hashtable htbUser)
		{
			bool bRet=true;

			StringBuilder lSQL=new StringBuilder();
			lSQL.Append(" INSERT INTO [hdb_user]( [userName], [userPassword], [role], [remark], [deleteFlag],[email], ");
			lSQL.Append("  [createTime], [fullName], [phone]) ");
			lSQL.Append(" VALUES(@userName,@userPassword,@role,@remark,0,@email,getdate(),@fullName,@phone) ");


			IDbConnection Con=DBFunc.getConnection();
			IDbTransaction trans=Con.BeginTransaction();

			try
			{
				DBFunc.executeNonQuery(trans,lSQL.ToString(),htbUser);
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				bRet=false;
				throw new Exception("�����û���Ϣ����"+ex.Message);
			}
			finally
			{
				Con.Close();
				Con.Dispose();
			}
			return bRet;
		}
		public static bool UpdateUser(Hashtable htbUser)
		{
			bool bRet=true;
			if(htbUser["userid"]==null || htbUser["userid"].ToString()==string.Empty)
				throw new Exception("�����û���Ϣ�����û�ID����Ϊ��!");


			StringBuilder lSQL=new StringBuilder();
			lSQL.Append(" update hdb_user  set ");
			lSQL.Append(" username=@username ");
			lSQL.Append(" ,userPassword=@userPassword");
			lSQL.Append(",remark=@remark");
			lSQL.Append(",fullname=@fullname");
			lSQL.Append(",email=@email");
			lSQL.Append(",phone=@phone");
			lSQL.Append(",role=@role");
			lSQL.Append(" where userid=@userid ");

			IDbConnection Con=DBFunc.getConnection();
			IDbTransaction trans=Con.BeginTransaction();

			try
			{
				DBFunc.executeNonQuery(trans,lSQL.ToString(),htbUser);
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				bRet=false;
				throw new Exception("���¿ͻ���Ϣ����"+ex.Message);
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
