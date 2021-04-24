using DAL;
using MODEL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class mesOrgManager
    {
        mesOrgService emps = new mesOrgService();
        public bool isExistsByaccount(string account)
        {
            return false;

        }
        public async Task<List<mesOrg>>  getOrgs(string APIUlr)
        {

            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(5);
            try
            {
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
            catch (Exception ex)
            {
                List<mesOrg> orgs = new List<mesOrg>();
                return orgs;
              //  throw;
            } 
        }
    }
}
