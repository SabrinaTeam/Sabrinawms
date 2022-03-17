using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class mesEmployee
    {
        
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class EmployeesParameters
    {
        public string org { get; set; }
        public string dept   { get; set; }
        public string passPortNumber { get; set; }
        public string userName { get; set; }
        public string workNumber { get; set; }
        public bool checkDate { get; set; }
        public int? dateRequire { get; set; }
        public string starDate { get; set; }
        public string stopDate { get; set; }
        public string assessDate { get; set; }
    }

    public class Employees
    {
        public int Eid { get; set; }//用户ID
        public string passportNumber { get; set; } //护照号码ID
        
        public string subID { get; set; } //代码
        public string workID { get; set; } //工号
        public string userName { get; set; } //姓名
        public string userNameEN { get; set; } //姓名英文
        public int userSexID { get; set; } //性别
        public string birthday { get; set; } //生日
        public int educationID { get; set; }//学历
        public string hometown { get; set; }//户籍
        public string phoneNumber { get; set; }//手机号码
        public int positionID { get; set; }//职位
        public string entryDate { get; set; }//入职日期
        public string jobChange { get; set; }//职务变动
        public string assessDate { get; set; }//年度考核月
        public string contractFinishDate { get; set; }//合约到期日
        public string tryFinishDate { get; set; }//试用到期日
        public string planResignDate { get; set; }//计划离职日
        public string resignDate { get; set; }//离职日
        public string resignNote { get; set; }//离职备注
        public int resigned { get; set; }//离职标志

        public int Did { get; set; }//部门ID 
        public string deptName { get; set; }//部门名称   

        public int Cid { get; set; }//证件ID 
        public string passportIssueDate { get; set; }//护照签发日
        public string passportFinishDate { get; set; }//护照到期日
        public string passportSignArea { get; set; }//护照签发地
        public string passportVisaNumber { get; set; }//护照签证号
        public string passportVisaArea { get; set; }//签证签发地
        public string passportVisaTimeLimit { get; set; }//签证有效时长
        public string passportVisaFinshDate { get; set; }//签证到期日
        public string entryVisaDate { get; set; }//入职签证日期
        public int workerCard { get; set; }//是否有劳工证
        public string workerCardID { get; set; }//劳工证号
        public int healthCard { get; set; }//是否有健康证 


       
        public int age { get; set; }//年龄
        public double Seniority { get; set; }//年资 


        public int Mid { get; set; }//信息ID 
        public string msgTxt { get; set; }//信息文本
        public int msgCheck { get; set; }//信息确认


        public int Pid { get; set; }//职务ID 
        public string positionName { get; set; }//职位
        public string positionNameEN { get; set; }//职位英文名
        public string Org { get; set; }//厂区

       
        public int sexID { get; set; }//性别ID
        public string sexName { get; set; }//性别
        public string sexNote { get; set; }//性别备注 



    }


}
