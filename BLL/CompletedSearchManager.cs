using DAL;
using MODEL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CompletedSearchManager
    {
        public CompletedSearchService css = new CompletedSearchService();
        public async Task<List<mesOrg>> getOrgs(string APIUlr)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = System.TimeSpan.FromSeconds(5);
                HttpResponseMessage response = await client.GetAsync(APIUlr);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var dict = JsonConvert.DeserializeObject<Dictionary<object, object>>(responseBody);
                string Code = dict["Code"].ToString();
                JObject json1 = (JObject)JsonConvert.DeserializeObject(responseBody);
                JArray array = (JArray)json1["Data"];
                int i = array.Count;
                List<mesOrg> orgs = new List<mesOrg>();
                foreach (var jObject in array)
                {
                    mesOrg org = new mesOrg();
                    org.ReportPlaceId = Convert.ToInt32(jObject["ReportPlaceId"]);
                    org.ReportPlaceName = jObject["ReportPlaceName"].ToString();
                    orgs.Add(org);
                }
                return orgs;
            }
            catch (Exception)
            {
                List<mesOrg> orgs = new List<mesOrg>();
                return orgs;
                //throw;
            }
          
          

        }

        public async Task<List<mesEmployee>> GetAllProducts(string APIUlr)
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
                //throw;
            }
           
        }

        public  List<string> getLocation(string Org)
        {
            List<string> locations = new List<string>();
           DataTable location = css.getLocation(Org);
            if (location.Rows.Count > 0)
            {
                foreach (DataRow row in location.Rows)
                {
                    locations.Add(row["tagLocation"].ToString().Trim().ToUpper());
                }
            }
            return locations;
        }

        public DataTable getMesWorktagScans(List<CompletedSearch> parameters)
        {
            
            DataTable WorktagScans = css.getMesWorktagScans(parameters);
            
            return WorktagScans;
        }
    }
}
