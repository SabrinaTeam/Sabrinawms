using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
   public  class T_employee
    {
       
        public int id { set; get; } //员工表行ID
        public int certifiedID { set; get; } // 证件ID
        public int deptID { set; get; } // 部门ID

        public string subID { set; get; } // 代码
        public string workID { set; get; } // 工号
        public string userName { set; get; } // 姓名
        public string userNameEN { set; get; } // 姓名 英文
        /*
            0 = 未知项，
            1 = 男性，
            2 = 女性，
            3 = 女改男，男性，
            4 = 男改女，女性，
            9 = 未规定项（未指明）
         */
        public int userSex { set; get; } // 性别 
        
        public string birthday { set; get; } // 出生年月日
        public string education { set; get; } // 学历
        public string hometown { set; get; } // 户籍地
        public string phoneNumber { set; get; } // 手机号
        public string position { set; get; } // 职位

        public string entryDate { set; get; } // 入职日期
        public string jobChange { set; get; } // 职务异动
        public string assessDate { set; get; } // 年度考核月  '-1m 提醒
        public string contractFinishDate { set; get; } // 合约到期年月日
        public string tryFinishDate { set; get; } // 试用期到期日  -7d 提醒

        public string planResignDate { set; get; } // 预计离职日
        public string resignDate { set; get; } // 真正离职日
        public string resignNote { set; get; } // 离职原因

        public int msgCheck { set; get; } // 提醒信息确认: 0 = 没有信息 , 1 = 未确认 

    }
   
}
