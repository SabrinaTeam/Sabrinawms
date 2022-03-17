using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
   public class T_certified
    { 
        public int id { set; get; } //证件表行ID
        public string passportNumber { set; get; } //护照号码
        public string passportIssueDate { set; get; } //护照签发日
        public string passportFinishDate { set; get; } //护照到期日  -18m 提醒
        public string passportSignArea { set; get; } //护照签发地
        public string passportVisaNumber { set; get; } //签证号
        public string passportVisaArea { set; get; } //签证签发地
        public string passportVisaTimeLimit { set; get; } //签证期限
        public string passportVisaFinshDate { set; get; } //签证到期日期  -1m 提醒
        public string entryVisaDate { set; get; } //入职签证日期
        public int workerCard { set; get; } //劳工证
        public string workerCardID { set; get; } //劳工证号
        public int healthCard { set; get; } //健康证
    }
}
