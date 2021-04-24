using DAL;
using MODEL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class mesEmployeeManager
    {
        mesEmployeeService emps = new mesEmployeeService();
        public bool  isExistsByaccount (string account)
        {
            return emps.isExistsByaccount(account);
             
        }
        public int  updataOrAddUser(string[] userInfo)
        {
            int updataOrAdd = 0;
             emps.insetMesDepts(userInfo);

            if (userInfo[0].Length <= 0)
            {
                updataOrAdd =  addUser(userInfo);
            }
            else
            {
                updataOrAdd = updataUser(userInfo);
            }
            return updataOrAdd;
        }
        public int addUser(string[] userInfo)
        {
            //查询 account 是否已有  有的话 不能新增
           
            string account = userInfo[1];
            if (this.isExistsByaccount(account) )
            {
                return -1;
            }

            return  emps.addUser(userInfo);
             
        }
        public int updataUser(string[] userInfo)
        {
            return emps.updataUser(userInfo);             
        }


        public  async Task<List<mesEmployee>> GetAllProducts(string APIUlr)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(APIUlr);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var dict = JsonConvert.DeserializeObject<Dictionary<object, object>>(responseBody);
                string Code = dict["Code"].ToString();
                JObject json1 = (JObject)JsonConvert.DeserializeObject(responseBody);
                JArray array = (JArray)json1["Data"];
                int i = array.Count;
                List<mesEmployee> emps = new List<mesEmployee>();
                foreach (var jObject in array)
                {
                    mesEmployee emp = new mesEmployee();
                    emp.ID = jObject["ProcessID"].ToString();
                    emp.Name = jObject["ProcessName"].ToString();
                    emps.Add(emp);
                }
                return emps;
            }
            catch (Exception)
            {
                List<mesEmployee> emps = new List<mesEmployee>();
                return emps;
            }
           
          
        }

        public DataTable getUserInfo(string[] searchParameter)
        {
            return emps.getUserInfo(searchParameter);
        }

    }
} 