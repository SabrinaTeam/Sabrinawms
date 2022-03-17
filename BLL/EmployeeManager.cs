using DAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BLL
{
    public class EmployeeManager
    {
        EmployeeService es = new EmployeeService();
        public List<T_dept> getDetp(string org)
        {
            List<T_dept> detps = new List<T_dept>();
            DataTable dt = es.getDetps(org);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    T_dept detp = new T_dept();
                    detp.id = Convert.ToInt32 (dr["id"] );
                    detp.org = dr["org"].ToString();
                    detp.deptName = dr["deptName"].ToString();
                    detps.Add(detp);
                }
            }
            return detps;
        }

        public List<T_Position> getPositions(string org)
        {
            List<T_Position> positions = new List<T_Position>();
            DataTable dt = es.getPositions(org);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    // id,positionName,positionNameEN,Org 
                    T_Position position = new T_Position();
                    position.id = Convert.ToInt32(dr["id"]);
                    position.positionName = dr["positionName"].ToString();
                    position.positionNameEN = dr["positionNameEN"].ToString();
                    position.Org = dr["Org"].ToString();
                    positions.Add(position);
                }
            }

            T_Position p = new T_Position();
            p.id = -1;
            p.positionName = "";
            p.positionNameEN = "";
            p.Org = "";
            positions.Add(p);

            return positions;
        }

        //  T_certified,   T_employee, T_msg 
        public int[] SaveEmployees(DataTable T_certified, DataTable T_employee, DataTable T_msg)
        {
          
            int j = 0;
            int k = 0;
            //新增

            for (int i = 0; i < T_certified.Rows.Count; i++)
            {
                if(T_certified.Rows[i]["id"].ToString() == "-1")
                {
                    j = j + es.insetCertified(T_certified.Rows[i]);
                            es.insetemployee(T_employee.Rows[i]);
                            es.insetemsg(T_msg.Rows[i]);
                }
                else
                {
                    k = k + es.updataCertified(T_certified.Rows[i]);
                            es.updataemployee(T_employee.Rows[i]);
                            es.updatamsg(T_msg.Rows[i]);
                }
            }
            int[] result = { j, k };
            return result;
        }


        public List<Employees> getEmployeesByParameters(EmployeesParameters ep)
        { 
            DataTable employees = es.getEmployeesByParameters(ep);
            if(employees.Rows.Count <= 0)
            {
                return null;
            }
            List<Employees> eps = new List<Employees>();           
            foreach (DataRow item in employees.Rows)
            {
                Employees epl = new Employees();
                epl.Eid =Convert.ToInt32( item["Eid"].ToString() );
                epl.passportNumber = item["passportNumber"].ToString();
                epl.Did = Convert.ToInt32(item["deptID"].ToString());
                epl.subID = item["subID"].ToString();
                epl.workID = item["workID"].ToString();
                epl.userName = item["userName"].ToString();
                epl.userNameEN = item["userNameEN"].ToString();
                epl.userSexID = Convert.ToInt32(item["userSexID"].ToString());
                epl.birthday = item["birthday"].ToString();
                epl.educationID = Convert.ToInt32(item["educationID"].ToString());
                epl.hometown = item["hometown"].ToString();
                epl.phoneNumber = item["phoneNumber"].ToString();
                epl.positionID = Convert.ToInt32(item["positionID"].ToString());
                epl.entryDate = item["entryDate"].ToString();
                epl.jobChange = item["jobChange"].ToString();
                epl.assessDate = item["assessDate"].ToString();
                epl.contractFinishDate = item["contractFinishDate"].ToString();
                epl.tryFinishDate = item["tryFinishDate"].ToString();
                epl.planResignDate = item["planResignDate"].ToString();

                epl.resignDate = item["resignDate"].ToString();
                epl.resignNote = item["resignNote"].ToString();
                string r = item["resigned"].ToString() ;
                epl.resigned = r.Length == 0 ? 0 : Convert.ToInt32( r) ;

                epl.Did = Convert.ToInt32(item["Did"].ToString());
                epl.Org = item["org"].ToString();
                epl.deptName = item["deptName"].ToString();

                epl.Cid = Convert.ToInt32(item["Cid"].ToString());
                epl.passportIssueDate = item["passportIssueDate"].ToString();
                epl.passportFinishDate = item["passportFinishDate"].ToString();
                epl.passportSignArea = item["passportSignArea"].ToString();
                epl.passportVisaNumber = item["passportVisaNumber"].ToString();
                epl.passportVisaArea = item["passportVisaArea"].ToString();
                epl.passportVisaTimeLimit = item["passportVisaTimeLimit"].ToString();
                epl.passportVisaFinshDate = item["passportVisaFinshDate"].ToString();
                epl.entryVisaDate = item["entryVisaDate"].ToString();

                string w = item["workerCard"].ToString();
                epl.workerCard = w.Length == 0 ? 0 : Convert.ToInt32(w);               
                epl.workerCardID = item["workerCardID"].ToString();

                string h = item["healthCard"].ToString();
                epl.healthCard = h.Length == 0 ? 0 : Convert.ToInt32(h);

                string mid = item["Mid"].ToString();
                if(mid == "")
                {
                    epl.Mid = -1;
                }
                else
                {
                    epl.Mid = Convert.ToInt32( mid);
                }
              
                epl.msgTxt = item["msgTxt"].ToString();

                string m = item["msgCheck"].ToString();
                epl.msgCheck = m.Length == 0 ? 0 : Convert.ToInt32(m);

                

                epl.Pid = Convert.ToInt32(item["Pid"].ToString());
                epl.positionName = item["positionName"].ToString();
                epl.positionNameEN = item["positionNameEN"].ToString();

               
                epl.sexID = Convert.ToInt32(item["sexID"].ToString());
                epl.sexName = item["sexName"].ToString();
                epl.sexNote = item["sexNote"].ToString();

                eps.Add(epl);
            }
            return eps;
        }

        public List<T_Sex> getSex()
        {
            List<T_Sex> sexs = new List<T_Sex>();
            DataTable dt = es.getSex();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    T_Sex sex = new T_Sex();
                    sex.id = Convert.ToInt32(dr["id"]);
                    sex.sexID = Convert.ToInt32( dr["sexID"].ToString());
                    sex.sexName = dr["sexName"].ToString();
                    sex.sexNote = dr["sexNote"].ToString();
                    sexs.Add(sex);
                }
            }
            return sexs;
        }

        public List<T_Education> getEducation()
        {
            List<T_Education> educations = new List<T_Education>();
            DataTable dt = es.getEducation();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //id,educationName,educationNameEN,educationNote
                    T_Education education = new T_Education();
                    education.id = Convert.ToInt32(dr["id"]);
                    education.educationName =dr["educationName"].ToString();
                    education.educationNameEN = dr["educationNameEN"].ToString();
                    education.educationNote = dr["educationNote"].ToString();
                    educations.Add(education);
                }
            }
            return educations;
        }


        public List<string> getPassportNumbers()
        {
            DataTable passportNumbers = es.getPassportNumbers();
            if (passportNumbers.Rows.Count <= 0)
            {
                return null;
            }
            List<string> ps = new List<string>();
            foreach (DataRow item in passportNumbers.Rows)
            {
                ps.Add(item["passportNumber"].ToString());
            }
            return ps;
        }


        public List<string> getWorkIDs()
        {
            DataTable workIDs = es.getWorkIDs();
            if (workIDs.Rows.Count <= 0)
            {
                return null;
            }
            List<string> ps = new List<string>();
            foreach (DataRow item in workIDs.Rows)
            {
                ps.Add(item["workID"].ToString());
            }
            return ps;
        }
        public List<string> getJobs(string org)
        {
            DataTable jobs = es.getJobs(org);
            if (jobs.Rows.Count <= 0)
            {
                return null;
            }
            List<string> ps = new List<string>();
            foreach (DataRow item in jobs.Rows)
            {
                ps.Add(item["positionName"].ToString());
            }
            return ps;
        }

        public int addJobs(string jobsName, string jobsNameEN, string org)
        {
            int  jobs = es.addJobs(jobsName, jobsNameEN,org);
            
            return jobs;
        }
    }
}
