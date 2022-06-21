using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public  class Machines
    {

        public int id { get; set; } // 机器分类ID
        public string machineClass { get; set; } // 机器分类
        public string machineName { get; set; } // 机器名称
        public string machineTypeShortName { get; set; } // 机器名称英文 / 别称
        public string machinesMarckKhmer { get; set; } // 机器备注
        public string imagestr { get; set; } // 机器图片
        public int isMachinesStatus { get; set; } // -1 报废  ， 0 正常 ， 1 维修
        public DateTime CreateDate { get; set; } // 创建日期
        public string Creator { get; set; } //创建者
        public DateTime modify { get; set; } // 修改日期
        public string modifor { get; set; } // 修改者

    }
}
