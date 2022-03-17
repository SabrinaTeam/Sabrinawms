using MODEL;
using System;
using System.Data;

namespace DAL
{
    public class EmployeeService
    {
        public DataTable getDetps(string org)
        {
            string sql = " SELECT id,org,deptName FROM dbo.T_dept WHERE org ='" + org + "'";
            DataTable dt = new DataTable();
            dt = SabrinaVisa_SqlHelper.ExcuteTable(sql);
            return dt;
        }

        public DataTable getPositions(string org)
        {
            string sql = "SELECT id,positionName,positionNameEN,Org FROM dbo.T_Position  WHERE org ='" + org + "'";
            DataTable dt = new DataTable();
            dt = SabrinaVisa_SqlHelper.ExcuteTable(sql);
            return dt;
        }

        //   public string insetCertified(DataTable certified)
        //   {
        //   string mesg=  SabrinaVisa_SqlHelper.SqlBulkCopyCertified(certified);

        //    return mesg;
        //  }
        public int getCertifiedMaxID()
        {
            string sql = @"SELECT MAX(id) id FROM T_certified";
            DataTable dt = SabrinaVisa_SqlHelper.ExcuteTable(sql);
            if (dt.Rows.Count <= 0)
            {
                return 0;
            }
            string maxID = dt.Rows[0][0].ToString();
            return Convert.ToInt32(maxID);
        }

        public string insetEmployee(DataTable employee)
        {
            string mesg = SabrinaVisa_SqlHelper.SqlBulkCopyEmployee(employee);
            return mesg;
        }

        public string insetMsg(DataTable msg)
        {
            string mesg = SabrinaVisa_SqlHelper.SqlBulkCopyMsg(msg);
            return mesg;
        }

        public int updataCertified(DataRow CertifiedRow)
        {
            string sql = @" UPDATE [dbo].[T_certified]
                                                   SET
                                                      [passportIssueDate] = '" + CertifiedRow["passportIssueDate"].ToString() + @"'
                                                      ,[passportNumber] = '" + CertifiedRow["passportNumber"].ToString() + @"'
                                                      ,[passportFinishDate] = '" + CertifiedRow["passportFinishDate"].ToString() + @"'
                                                      ,[passportSignArea] = '" + CertifiedRow["passportSignArea"].ToString() + @"'
                                                      ,[passportVisaNumber] = '" + CertifiedRow["passportVisaNumber"].ToString() + @"'
                                                      ,[passportVisaArea] = '" + CertifiedRow["passportVisaArea"].ToString() + @"'
                                                      ,[passportVisaTimeLimit] = '" + CertifiedRow["passportVisaTimeLimit"].ToString() + @"'
                                                      ,[passportVisaFinshDate] = '" + CertifiedRow["passportVisaFinshDate"].ToString() + @"'
                                                      ,[entryVisaDate] = '" + CertifiedRow["entryVisaDate"].ToString() + @"'
                                                      ,[workerCard] = " + CertifiedRow["workerCard"].ToString() + @"
                                                      ,[workerCardID] = '" + CertifiedRow["workerCardID"].ToString() + @"'
                                                      ,[healthCard] = " + CertifiedRow["healthCard"].ToString() + @"
                                                 WHERE passportNumber = '" + CertifiedRow["passportNumber"].ToString() + "'";
            int i = SabrinaVisa_SqlHelper.ExecuteNonQuery(sql);
            return i;
        }

        public void updataEmployee(DataTable employee, string emploreeid)
        {
            string sql = " SELECT id,org,deptName FROM dbo.T_dept ";
            DataTable dt = new DataTable();
            dt = SabrinaVisa_SqlHelper.ExcuteTable(sql);
        }

        public void updataMsg(DataTable msg, string emploreeid)
        {
            string sql = " SELECT id,org,deptName FROM dbo.T_dept ";
            DataTable dt = new DataTable();
            dt = SabrinaVisa_SqlHelper.ExcuteTable(sql);
        }

        public DataTable getEmployeesByParameters(EmployeesParameters ep)
        {
            string sql = "";
            if (ep.checkDate) //考核月份
            {
                if (ep.dateRequire == 0) //考核月份
                {
                    sql = @"
                            SELECT e.id AS Eid,       e.passportNumber,       e.deptID,       e.subID,        e.workID,       e.userName,        e.userNameEN,	          e.userSexID,       e.birthday,       e.educationID,
		e.hometown,         e.phoneNumber,         e.positionID,	        e.entryDate,          e.jobChange,        e.assessDate,		       e.contractFinishDate,         e.tryFinishDate,
       e.planResignDate,	       e.resignDate,         e.resignNote,			        e.resigned,
	   d.id AS Did,	        d.org,	        d.deptName,
	   c.id AS Cid,	        c.passportNumber,	       c.passportIssueDate,	          c.passportFinishDate,	        c.passportSignArea,       c.passportVisaNumber,		        c.passportVisaArea,
       c.passportVisaTimeLimit,	         c.passportVisaFinshDate,	        c.entryVisaDate,			        c.workerCard,		       c.workerCardID,	       c.healthCard,
	   m.id AS Mid,	       m.workID,	         m.msgTxt,				       m.msgCheck,
	   p.id AS Pid,		       p.positionName,       p.positionNameEN,	        p.Org,
                s.sexID,       s.sexName,		          s.sexNote
FROM dbo.T_employee e
    LEFT JOIN dbo.T_dept d	          ON e.deptID = d.id
    LEFT JOIN dbo.T_certified c		        ON e.passportNumber = c.passportNumber
    LEFT JOIN dbo.T_msg m	           ON m.workID = e.workID
    LEFT JOIN dbo.T_education u		         ON u.id = e.educationID
    LEFT JOIN dbo.T_Position p		        ON p.id = e.positionID
    LEFT JOIN dbo.T_Sex s		           ON s.sexID = e.userSexID
                            WHERE 1 = 1
                                  AND d.org LIKE '%" + ep.org + @"%'
                                  AND d.deptName  LIKE '%" + ep.dept + @"%'
                                  AND e.passportNumber LIKE '%" + ep.passPortNumber + @"%'
                                  AND e.userName LIKE '%" + ep.userName + @"%'
                                  AND e.workID  LIKE '%" + ep.workNumber + @"%'
                                  AND e.assessDate ='" + ep.assessDate + "'";
                }
                else if (ep.dateRequire == 1) //合约到期日
                {
                    sql = @"
                            SELECT e.id AS Eid,       e.passportNumber,       e.deptID,       e.subID,        e.workID,       e.userName,        e.userNameEN,	          e.userSexID,       e.birthday,       e.educationID,
		e.hometown,         e.phoneNumber,         e.positionID,	        e.entryDate,          e.jobChange,        e.assessDate,		       e.contractFinishDate,         e.tryFinishDate,
       e.planResignDate,	       e.resignDate,         e.resignNote,			        e.resigned,
	   d.id AS Did,	        d.org,	        d.deptName,
	   c.id AS Cid,	        c.passportNumber,	       c.passportIssueDate,	          c.passportFinishDate,	        c.passportSignArea,       c.passportVisaNumber,		        c.passportVisaArea,
       c.passportVisaTimeLimit,	         c.passportVisaFinshDate,	        c.entryVisaDate,			        c.workerCard,		       c.workerCardID,	       c.healthCard,
	   m.id AS Mid,	       m.workID,	         m.msgTxt,				       m.msgCheck,
	   p.id AS Pid,		       p.positionName,       p.positionNameEN,	        p.Org,
              s.sexID,       s.sexName,		          s.sexNote
FROM dbo.T_employee e
    LEFT JOIN dbo.T_dept d	          ON e.deptID = d.id
    LEFT JOIN dbo.T_certified c		        ON e.passportNumber = c.passportNumber
    LEFT JOIN dbo.T_msg m	           ON m.workID = e.workID
    LEFT JOIN dbo.T_education u		         ON u.id = e.educationID
    LEFT JOIN dbo.T_Position p		        ON p.id = e.positionID
    LEFT JOIN dbo.T_Sex s		           ON s.sexID = e.userSexID
                            WHERE 1 = 1
                                  AND d.org LIKE '%" + ep.org + @"%'
                                  AND d.deptName  LIKE '%" + ep.dept + @"%'
                                  AND e.passportNumber LIKE '%" + ep.passPortNumber + @"%'
                                  AND e.userName LIKE '%" + ep.userName + @"%'
                                  AND e.workID  LIKE '%" + ep.workNumber + @"%'
                                  AND e.contractFinishDate BETWEEN '" + ep.starDate + "' AND '" + ep.stopDate + "'";
                }
                else if (ep.dateRequire == 2) //试用到期日
                {
                    sql = @"
                            SELECT e.id AS Eid,       e.passportNumber,       e.deptID,       e.subID,        e.workID,       e.userName,        e.userNameEN,	          e.userSexID,       e.birthday,       e.educationID,
		e.hometown,         e.phoneNumber,         e.positionID,	        e.entryDate,          e.jobChange,        e.assessDate,		       e.contractFinishDate,         e.tryFinishDate,
       e.planResignDate,	       e.resignDate,         e.resignNote,			        e.resigned,
	   d.id AS Did,	        d.org,	        d.deptName,
	   c.id AS Cid,	        c.passportNumber,	       c.passportIssueDate,	          c.passportFinishDate,	        c.passportSignArea,       c.passportVisaNumber,		        c.passportVisaArea,
       c.passportVisaTimeLimit,	         c.passportVisaFinshDate,	        c.entryVisaDate,			        c.workerCard,		       c.workerCardID,	       c.healthCard,
	   m.id AS Mid,	       m.workID,	         m.msgTxt,				       m.msgCheck,
	   p.id AS Pid,		       p.positionName,       p.positionNameEN,	        p.Org,
                s.sexID,       s.sexName,		          s.sexNote
FROM dbo.T_employee e
    LEFT JOIN dbo.T_dept d	          ON e.deptID = d.id
    LEFT JOIN dbo.T_certified c		        ON e.passportNumber = c.passportNumber
    LEFT JOIN dbo.T_msg m	           ON m.workID = e.workID
    LEFT JOIN dbo.T_education u		         ON u.id = e.educationID
    LEFT JOIN dbo.T_Position p		        ON p.id = e.positionID
    LEFT JOIN dbo.T_Sex s		           ON s.sexID = e.userSexID
                            WHERE 1 = 1
                                  AND d.org LIKE '%" + ep.org + @"%'
                                  AND d.deptName  LIKE '%" + ep.dept + @"%'
                                  AND e.passportNumber LIKE '%" + ep.passPortNumber + @"%'
                                  AND e.userName LIKE '%" + ep.userName + @"%'
                                  AND e.workID  LIKE '%" + ep.workNumber + @"%'
                                  AND e.tryFinishDate BETWEEN '" + ep.starDate + "' AND '" + ep.stopDate + "'";
                }
                else if (ep.dateRequire == 3) //护照到期日
                {
                    sql = @"
                            SELECT e.id AS Eid,       e.passportNumber,       e.deptID,       e.subID,        e.workID,       e.userName,        e.userNameEN,	          e.userSexID,       e.birthday,       e.educationID,
		e.hometown,         e.phoneNumber,         e.positionID,	        e.entryDate,          e.jobChange,        e.assessDate,		       e.contractFinishDate,         e.tryFinishDate,
       e.planResignDate,	       e.resignDate,         e.resignNote,			        e.resigned,
	   d.id AS Did,	        d.org,	        d.deptName,
	   c.id AS Cid,	        c.passportNumber,	       c.passportIssueDate,	          c.passportFinishDate,	        c.passportSignArea,       c.passportVisaNumber,		        c.passportVisaArea,
       c.passportVisaTimeLimit,	         c.passportVisaFinshDate,	        c.entryVisaDate,			        c.workerCard,		       c.workerCardID,	       c.healthCard,
	   m.id AS Mid,	       m.workID,	         m.msgTxt,				       m.msgCheck,
	   p.id AS Pid,		       p.positionName,       p.positionNameEN,	        p.Org,
              s.sexID,       s.sexName,		          s.sexNote
FROM dbo.T_employee e
    LEFT JOIN dbo.T_dept d	          ON e.deptID = d.id
    LEFT JOIN dbo.T_certified c		        ON e.passportNumber = c.passportNumber
    LEFT JOIN dbo.T_msg m	           ON m.workID = e.workID
    LEFT JOIN dbo.T_education u		         ON u.id = e.educationID
    LEFT JOIN dbo.T_Position p		        ON p.id = e.positionID
    LEFT JOIN dbo.T_Sex s		           ON s.sexID = e.userSexID
                            WHERE 1 = 1
                                  AND d.org LIKE '%" + ep.org + @"%'
                                  AND d.deptName  LIKE '%" + ep.dept + @"%'
                                  AND e.passportNumber LIKE '%" + ep.passPortNumber + @"%'
                                  AND e.userName LIKE '%" + ep.userName + @"%'
                                  AND  e.workID  LIKE '%" + ep.workNumber + @"%'
                                  AND c.passportFinishDate BETWEEN '" + ep.starDate + "' AND '" + ep.stopDate + "'";
                }
                else if (ep.dateRequire == 4) //签证到期日
                {
                    sql = @"
                            SELECT e.id AS Eid,       e.passportNumber,       e.deptID,       e.subID,        e.workID,       e.userName,        e.userNameEN,	          e.userSexID,       e.birthday,       e.educationID,
		e.hometown,         e.phoneNumber,         e.positionID,	        e.entryDate,          e.jobChange,        e.assessDate,		       e.contractFinishDate,         e.tryFinishDate,
       e.planResignDate,	       e.resignDate,         e.resignNote,			        e.resigned,
	   d.id AS Did,	        d.org,	        d.deptName,
	   c.id AS Cid,	        c.passportNumber,	       c.passportIssueDate,	          c.passportFinishDate,	        c.passportSignArea,       c.passportVisaNumber,		        c.passportVisaArea,
       c.passportVisaTimeLimit,	         c.passportVisaFinshDate,	        c.entryVisaDate,			        c.workerCard,		       c.workerCardID,	       c.healthCard,
	   m.id AS Mid,	       m.workID,	         m.msgTxt,				       m.msgCheck,
	   p.id AS Pid,		       p.positionName,       p.positionNameEN,	        p.Org,
               s.sexID,       s.sexName,		          s.sexNote
FROM dbo.T_employee e
    LEFT JOIN dbo.T_dept d	          ON e.deptID = d.id
    LEFT JOIN dbo.T_certified c		        ON e.passportNumber = c.passportNumber
    LEFT JOIN dbo.T_msg m	           ON m.workID = e.workID
    LEFT JOIN dbo.T_education u		         ON u.id = e.educationID
    LEFT JOIN dbo.T_Position p		        ON p.id = e.positionID
    LEFT JOIN dbo.T_Sex s		           ON s.sexID = e.userSexID
                            WHERE 1 = 1
                                  AND d.org LIKE '%" + ep.org + @"%'
                                  AND d.deptName  LIKE '%" + ep.dept + @"%'
                                  AND e.passportNumber LIKE '%" + ep.passPortNumber + @"%'
                                  AND e.userName LIKE '%" + ep.userName + @"%'
                                  AND e.workID  LIKE '%" + ep.workNumber + @"%'
                                  AND c.passportVisaFinshDate BETWEEN '" + ep.starDate + "' AND '" + ep.stopDate + "'";
                }
                else if (ep.dateRequire == 5) //入职签证日
                {
                    sql = @"
                            SELECT e.id AS Eid,       e.passportNumber,       e.deptID,       e.subID,        e.workID,       e.userName,        e.userNameEN,	          e.userSexID,       e.birthday,       e.educationID,
		e.hometown,         e.phoneNumber,         e.positionID,	        e.entryDate,          e.jobChange,        e.assessDate,		       e.contractFinishDate,         e.tryFinishDate,
       e.planResignDate,	       e.resignDate,         e.resignNote,			        e.resigned,
	   d.id AS Did,	        d.org,	        d.deptName,
	   c.id AS Cid,	        c.passportNumber,	       c.passportIssueDate,	          c.passportFinishDate,	        c.passportSignArea,       c.passportVisaNumber,		        c.passportVisaArea,
       c.passportVisaTimeLimit,	         c.passportVisaFinshDate,	        c.entryVisaDate,			        c.workerCard,		       c.workerCardID,	       c.healthCard,
	   m.id AS Mid,	       m.workID,	         m.msgTxt,				       m.msgCheck,
	   p.id AS Pid,		       p.positionName,       p.positionNameEN,	        p.Org,
              s.sexID,       s.sexName,		          s.sexNote
FROM dbo.T_employee e
    LEFT JOIN dbo.T_dept d	          ON e.deptID = d.id
    LEFT JOIN dbo.T_certified c		        ON e.passportNumber = c.passportNumber
    LEFT JOIN dbo.T_msg m	           ON m.workID = e.workID
    LEFT JOIN dbo.T_education u		         ON u.id = e.educationID
    LEFT JOIN dbo.T_Position p		        ON p.id = e.positionID
    LEFT JOIN dbo.T_Sex s		           ON s.sexID = e.userSexID
                            WHERE 1 = 1
                                  AND d.org LIKE '%" + ep.org + @"%'
                                  AND d.deptName  LIKE '%" + ep.dept + @"%'
                                  AND e.passportNumber LIKE '%" + ep.passPortNumber + @"%'
                                  AND e.userName LIKE '%" + ep.userName + @"%'
                                  AND e.workID  LIKE '%" + ep.workNumber + @"%'
                                  AND c.entryVisaDate BETWEEN '" + ep.starDate + "' AND '" + ep.stopDate + "'";
                }
                else if (ep.dateRequire == 6) //入职日期
                {
                    sql = @"
                            SELECT e.id AS Eid,       e.passportNumber,       e.deptID,       e.subID,        e.workID,       e.userName,        e.userNameEN,	          e.userSexID,       e.birthday,       e.educationID,
		e.hometown,         e.phoneNumber,         e.positionID,	        e.entryDate,          e.jobChange,        e.assessDate,		       e.contractFinishDate,         e.tryFinishDate,
       e.planResignDate,	       e.resignDate,         e.resignNote,			        e.resigned,
	   d.id AS Did,	        d.org,	        d.deptName,
	   c.id AS Cid,	        c.passportNumber,	       c.passportIssueDate,	          c.passportFinishDate,	        c.passportSignArea,       c.passportVisaNumber,		        c.passportVisaArea,
       c.passportVisaTimeLimit,	         c.passportVisaFinshDate,	        c.entryVisaDate,			        c.workerCard,		       c.workerCardID,	       c.healthCard,
	   m.id AS Mid,	       m.workID,	         m.msgTxt,				       m.msgCheck,
	   p.id AS Pid,		       p.positionName,       p.positionNameEN,	        p.Org,
               s.sexID,       s.sexName,		          s.sexNote
FROM dbo.T_employee e
    LEFT JOIN dbo.T_dept d	          ON e.deptID = d.id
    LEFT JOIN dbo.T_certified c		        ON e.passportNumber = c.passportNumber
    LEFT JOIN dbo.T_msg m	           ON m.workID = e.workID
    LEFT JOIN dbo.T_education u		         ON u.id = e.educationID
    LEFT JOIN dbo.T_Position p		        ON p.id = e.positionID
    LEFT JOIN dbo.T_Sex s		           ON s.sexID = e.userSexID
                            WHERE 1 = 1
                                  AND d.org LIKE '%" + ep.org + @"%'
                                  AND d.deptName  LIKE '%" + ep.dept + @"%'
                                  AND e.passportNumber LIKE '%" + ep.passPortNumber + @"%'
                                  AND e.userName LIKE '%" + ep.userName + @"%'
                                  AND e.workID  LIKE '%" + ep.workNumber + @"%'
                                  AND e.entryDate BETWEEN '" + ep.starDate + "' AND '" + ep.stopDate + "'";
                }
                else if (ep.dateRequire == 7) //预计离职日
                {
                    sql = @"
                            SELECT e.id AS Eid,       e.passportNumber,       e.deptID,       e.subID,        e.workID,       e.userName,        e.userNameEN,	          e.userSexID,       e.birthday,       e.educationID,
		e.hometown,         e.phoneNumber,         e.positionID,	        e.entryDate,          e.jobChange,        e.assessDate,		       e.contractFinishDate,         e.tryFinishDate,
       e.planResignDate,	       e.resignDate,         e.resignNote,			        e.resigned,
	   d.id AS Did,	        d.org,	        d.deptName,
	   c.id AS Cid,	        c.passportNumber,	       c.passportIssueDate,	          c.passportFinishDate,	        c.passportSignArea,       c.passportVisaNumber,		        c.passportVisaArea,
       c.passportVisaTimeLimit,	         c.passportVisaFinshDate,	        c.entryVisaDate,			        c.workerCard,		       c.workerCardID,	       c.healthCard,
	   m.id AS Mid,	       m.workID,	         m.msgTxt,				       m.msgCheck,
	   p.id AS Pid,		       p.positionName,       p.positionNameEN,	        p.Org,
               s.sexID,       s.sexName,		          s.sexNote
FROM dbo.T_employee e
    LEFT JOIN dbo.T_dept d	          ON e.deptID = d.id
    LEFT JOIN dbo.T_certified c		        ON e.passportNumber = c.passportNumber
    LEFT JOIN dbo.T_msg m	           ON m.workID = e.workID
    LEFT JOIN dbo.T_education u		         ON u.id = e.educationID
    LEFT JOIN dbo.T_Position p		        ON p.id = e.positionID
    LEFT JOIN dbo.T_Sex s		           ON s.sexID = e.userSexID
                            WHERE 1 = 1
                                  AND d.org LIKE '%" + ep.org + @"%'
                                  AND d.deptName  LIKE '%" + ep.dept + @"%'
                                  AND e.passportNumber LIKE '%" + ep.passPortNumber + @"%'
                                  AND e.userName LIKE '%" + ep.userName + @"%'
                                  AND e.workID  LIKE '%" + ep.workNumber + @"%'
                                  AND e.planResignDate BETWEEN '" + ep.starDate + "' AND '" + ep.stopDate + "'";
                }
                else if (ep.dateRequire == 8) //离职日期
                {
                    sql = @"
                            SELECT e.id AS Eid,       e.passportNumber,       e.deptID,       e.subID,        e.workID,       e.userName,        e.userNameEN,	          e.userSexID,       e.birthday,       e.educationID,
		e.hometown,         e.phoneNumber,         e.positionID,	        e.entryDate,          e.jobChange,        e.assessDate,		       e.contractFinishDate,         e.tryFinishDate,
       e.planResignDate,	       e.resignDate,         e.resignNote,			        e.resigned,
	   d.id AS Did,	        d.org,	        d.deptName,
	   c.id AS Cid,	        c.passportNumber,	       c.passportIssueDate,	          c.passportFinishDate,	        c.passportSignArea,       c.passportVisaNumber,		        c.passportVisaArea,
       c.passportVisaTimeLimit,	         c.passportVisaFinshDate,	        c.entryVisaDate,			        c.workerCard,		       c.workerCardID,	       c.healthCard,
	   m.id AS Mid,	       m.workID,	         m.msgTxt,				       m.msgCheck,
	   p.id AS Pid,		       p.positionName,       p.positionNameEN,	        p.Org,
               s.sexID,       s.sexName,		          s.sexNote
FROM dbo.T_employee e
    LEFT JOIN dbo.T_dept d	          ON e.deptID = d.id
    LEFT JOIN dbo.T_certified c		        ON e.passportNumber = c.passportNumber
    LEFT JOIN dbo.T_msg m	           ON m.workID = e.workID
    LEFT JOIN dbo.T_education u		         ON u.id = e.educationID
    LEFT JOIN dbo.T_Position p		        ON p.id = e.positionID
    LEFT JOIN dbo.T_Sex s		           ON s.sexID = e.userSexID
                            WHERE 1 = 1
                                  AND d.org LIKE '%" + ep.org + @"%'
                                  AND d.deptName  LIKE '%" + ep.dept + @"%'
                                  AND e.passportNumber LIKE '%" + ep.passPortNumber + @"%'
                                  AND e.userName LIKE '%" + ep.userName + @"%'
                                  AND e.workID  LIKE '%" + ep.workNumber + @"%'
                                  AND e.resignDate BETWEEN '" + ep.starDate + "' AND '" + ep.stopDate + "'";
                }
            }
            else
            {
                sql = @"
                            SELECT e.id AS Eid,       e.passportNumber,       e.deptID,       e.subID,        e.workID,       e.userName,        e.userNameEN,	          e.userSexID,       e.birthday,       e.educationID,
		e.hometown,         e.phoneNumber,         e.positionID,	        e.entryDate,          e.jobChange,        e.assessDate,		       e.contractFinishDate,         e.tryFinishDate,
       e.planResignDate,	       e.resignDate,         e.resignNote,			        e.resigned,
	   d.id AS Did,	        d.org,	        d.deptName,
	   c.id AS Cid,	        c.passportNumber,	       c.passportIssueDate,	          c.passportFinishDate,	        c.passportSignArea,       c.passportVisaNumber,		        c.passportVisaArea,
       c.passportVisaTimeLimit,	         c.passportVisaFinshDate,	        c.entryVisaDate,			        c.workerCard,		       c.workerCardID,	       c.healthCard,
	   m.id AS Mid,	       m.workID,	         m.msgTxt,				       m.msgCheck,
	   p.id AS Pid,		       p.positionName,       p.positionNameEN,	        p.Org,
             s.sexID,       s.sexName,		          s.sexNote
FROM dbo.T_employee e
    LEFT JOIN dbo.T_dept d	          ON e.deptID = d.id
    LEFT JOIN dbo.T_certified c		        ON e.passportNumber = c.passportNumber
    LEFT JOIN dbo.T_msg m	           ON m.workID = e.workID
    LEFT JOIN dbo.T_education u		         ON u.id = e.educationID
    LEFT JOIN dbo.T_Position p		        ON p.id = e.positionID
    LEFT JOIN dbo.T_Sex s		           ON s.sexID = e.userSexID
                            WHERE 1 = 1
                                  AND d.org LIKE '%" + ep.org + @"%'
                                  AND d.deptName  LIKE '%" + ep.dept + @"%'
                                  AND e.passportNumber LIKE '%" + ep.passPortNumber + @"%'
                                  AND e.userName LIKE '%" + ep.userName + @"%'
                                  AND   e.workID  LIKE '%" + ep.workNumber + @"%' ";
            }

            DataTable dt = new DataTable();
            dt = SabrinaVisa_SqlHelper.ExcuteTable(sql);
            return dt;
        }

        public DataTable getSex()
        {
            string sql = " SELECT id,sexID,sexName,sexNote FROM dbo.T_Sex ";
            DataTable dt = new DataTable();
            dt = SabrinaVisa_SqlHelper.ExcuteTable(sql);
            return dt;
        }

        public DataTable getEducation()
        {
            string sql = " SELECT id,educationName,educationNameEN,educationNote FROM dbo.T_education ";
            DataTable dt = new DataTable();
            dt = SabrinaVisa_SqlHelper.ExcuteTable(sql);
            return dt;
        }

        public DataTable getPassportNumbers()
        {
            string sql = "SELECT passportNumber FROM dbo.T_certified";
            DataTable dt = new DataTable();
            dt = SabrinaVisa_SqlHelper.ExcuteTable(sql);
            return dt;
        }

        public DataTable getWorkIDs()
        {
            string sql = "  SELECT workID FROM T_employee";
            DataTable dt = new DataTable();
            dt = SabrinaVisa_SqlHelper.ExcuteTable(sql);
            return dt;
        }

        public DataTable getJobs(string org)
        {
            string sql = " SELECT positionName	FROM T_Position	  WHERE Org = '"+ org + "';";
            DataTable dt = new DataTable();
            dt = SabrinaVisa_SqlHelper.ExcuteTable(sql);
            return dt;
        }


        public int  addJobs(string jobsName, string jobsNameEN, string org)
        {
            string sql = @" INSERT INTO [dbo].[T_Position]
                                                       ([positionName]
                                                       ,[positionNameEN]
                                                       ,[Org])
                                                 VALUES
                                                       ('"+ jobsName + @"'
                                                       ,'" + jobsNameEN + @"'
                                                       ,'" + org + @"')";
          
            int i = SabrinaVisa_SqlHelper.ExecuteNonQuery(sql);
            return i;
        }


        public int insetCertified(DataRow CertifiedRow)
        {
            string VALUES = "";

            VALUES = VALUES + "'" + CertifiedRow["passportNumber"].ToString() + @"','"
                            + CertifiedRow["passportIssueDate"].ToString() + @"','"
                            + CertifiedRow["passportFinishDate"].ToString() + @"','"
                            + CertifiedRow["passportSignArea"].ToString() + @"','"
                            + CertifiedRow["passportVisaNumber"].ToString() + @"','"
                            + CertifiedRow["passportVisaArea"].ToString() + @"','"
                            + CertifiedRow["passportVisaTimeLimit"].ToString() + @"','"
                            + CertifiedRow["passportVisaFinshDate"].ToString() + @"','"
                            + CertifiedRow["entryVisaDate"].ToString() + @"',"
                            + CertifiedRow["workerCard"].ToString() + @",'"
                            + CertifiedRow["workerCardID"].ToString() + @"',"
                            + CertifiedRow["healthCard"].ToString();

            string sql = @"  INSERT INTO [dbo].[T_certified]
                                                    ([passportNumber]
                                                   ,[passportIssueDate]
                                                   ,[passportFinishDate]
                                                   ,[passportSignArea]
                                                   ,[passportVisaNumber]
                                                   ,[passportVisaArea]
                                                   ,[passportVisaTimeLimit]
                                                   ,[passportVisaFinshDate]
                                                   ,[entryVisaDate]
                                                   ,[workerCard]
                                                   ,[workerCardID]
                                                   ,[healthCard])
                                             VALUES (" + VALUES + ")";
            int i = SabrinaVisa_SqlHelper.ExecuteNonQuery(sql);
            return i;
        }

        public int insetemployee(DataRow Employee)
        {
            string VALUES = "";
            VALUES = VALUES + "'" + Employee["passportNumber"].ToString() + @"',"
                            + Employee["deptID"].ToString() + @",'"
                            + Employee["subID"].ToString() + @"','"
                            + Employee["workID"].ToString() + @"','"
                            + Employee["userName"].ToString() + @"','"
                            + Employee["userNameEN"].ToString() + @"',"
                            + Employee["userSexID"].ToString() + @",'"
                            + Employee["birthday"].ToString() + @"',"
                            + Employee["educationID"].ToString() + @",'"
                            + Employee["hometown"].ToString() + @"','"
                            + Employee["phoneNumber"].ToString() + @"',"
                            + Employee["positionID"].ToString() + @",'"
                            + Employee["entryDate"].ToString() + @"','"
                            + Employee["jobChange"].ToString() + @"','"
                            + Employee["assessDate"].ToString() + @"','"
                            + Employee["contractFinishDate"].ToString() + @"','"
                            + Employee["tryFinishDate"].ToString() + @"','"
                            + Employee["planResignDate"].ToString() + @"','"
                            + Employee["resignDate"].ToString() + @"','"
                            + Employee["resignNote"].ToString() + @"',"
                            + Employee["resigned"].ToString();

            string sql = @"  INSERT INTO [dbo].[T_employee]
                                                           ([passportNumber]
                                                           ,[deptID]
                                                           ,[subID]
                                                           ,[workID]
                                                           ,[userName]
                                                           ,[userNameEN]
                                                           ,[userSexID]
                                                           ,[birthday]
                                                           ,[educationID]
                                                           ,[hometown]
                                                           ,[phoneNumber]
                                                           ,[positionID]
                                                           ,[entryDate]
                                                           ,[jobChange]
                                                           ,[assessDate]
                                                           ,[contractFinishDate]
                                                           ,[tryFinishDate]
                                                           ,[planResignDate]
                                                           ,[resignDate]
                                                           ,[resignNote]
                                                           ,[resigned])
                                             VALUES (" + VALUES + ")";
            int i = SabrinaVisa_SqlHelper.ExecuteNonQuery(sql);
            return i;
        }

        public int updataemployee(DataRow Employee)
        {
            string sql = @" UPDATE [dbo].[T_employee]
                                                   SET
                                                       [passportNumber] = '" + Employee["passportNumber"].ToString() + @"'
                                                      ,[deptID] = " + Employee["deptID"].ToString() + @"
                                                      ,[subID] = '" + Employee["subID"].ToString() + @"'
                                                      ,[workID] = '" + Employee["workID"].ToString() + @"'
                                                      ,[userName] = '" + Employee["userName"].ToString() + @"'
                                                      ,[userNameEN] = '" + Employee["userNameEN"].ToString() + @"'
                                                      ,[userSexID] = " + Employee["userSexID"].ToString() + @"
                                                      ,[birthday] = '" + Employee["birthday"].ToString() + @"'
                                                      ,[educationID] = " + Employee["educationID"].ToString() + @"
                                                      ,[hometown] = '" + Employee["hometown"].ToString() + @"'
                                                      ,[phoneNumber] = '" + Employee["phoneNumber"].ToString() + @"'
                                                      ,[positionID] = " + Employee["positionID"].ToString() + @"
                                                      ,[entryDate] = '" + Employee["entryDate"].ToString() + @"'
                                                      ,[jobChange] = '" + Employee["jobChange"].ToString() + @"'
                                                      ,[assessDate] = '" + Employee["assessDate"].ToString() + @"'
                                                      ,[contractFinishDate] = '" + Employee["contractFinishDate"].ToString() + @"'
                                                      ,[tryFinishDate] = '" + Employee["tryFinishDate"].ToString() + @"'
                                                      ,[planResignDate] = '" + Employee["planResignDate"].ToString() + @"'
                                                      ,[resignDate] = '" + Employee["resignDate"].ToString() + @"'
                                                      ,[resignNote] = '" + Employee["resignNote"].ToString() + @"'
                                                      ,[resigned] = " + Employee["resigned"].ToString() + @"
                                                WHERE passportNumber = '" + Employee["passportNumber"].ToString() + "'";
            int i = SabrinaVisa_SqlHelper.ExecuteNonQuery(sql);
            return i;
        }

        public int insetemsg(DataRow mesRow)
        {
            string VALUES = "";
            VALUES = VALUES + "'" + mesRow["workID"].ToString() + @"','"
                            + mesRow["msgTxt"].ToString() + @"',"
                            + mesRow["msgCheck"].ToString();

            string sql = @"  INSERT INTO [dbo].[T_msg]
                                                    ([workID]
                                                   ,[msgTxt]
                                                   ,[msgCheck])
                                             VALUES (" + VALUES + ")";
            int i = SabrinaVisa_SqlHelper.ExecuteNonQuery(sql);
            return i;
        }

        public int updatamsg(DataRow mesRow)
        {
            string sql = @" UPDATE [dbo].[T_msg]
                                                   SET
                                                      [workID] = '" + mesRow["workID"].ToString() + @"'
                                                      ,[msgTxt] = '" + mesRow["msgTxt"].ToString() + @"'
                                                      ,[msgCheck] = " + mesRow["msgCheck"].ToString() + @"
                                                WHERE id = '" + mesRow["id"].ToString() + "'";
            int i = SabrinaVisa_SqlHelper.ExecuteNonQuery(sql);
            return i;
        }
    }
}