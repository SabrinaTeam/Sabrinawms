using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class T_dept
    {
        public int id { set; get; } //部门表 ID
        public string org { set; get; } // 厂区
        public string deptName { set; get; } // 部门名称
    }

    public class T_Position
    {
        public int id { set; get; } //职位表 ID
        public string positionName { set; get; } // 职称
        public string positionNameEN { set; get; } // 职称英文
        public string Org { set; get; } // 厂区
    }

    public class T_Sex
    {
        public int id { set; get; } //性别表 ID
        public int sexID { set; get; } // 性别ID       
        public string sexName { set; get; } // 性别名称 
        public string sexNote { set; get; } // 备注
    }

    public class T_Education
    {
        public int id { set; get; } //性别表 ID
        public string educationName { set; get; } // 性别ID       
        public string educationNameEN { set; get; } // 性别名称 
        public string educationNote { set; get; } // 备注


       
    }
}
 