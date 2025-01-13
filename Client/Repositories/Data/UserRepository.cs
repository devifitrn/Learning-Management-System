using API.Models;
using Client.Base;
using Client.Repositories;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class UserRepository : GeneralRepository<User, string>
    {
        public UserRepository(Address address) : base(address, "Users/") { }
        public async Task<object> GetMasterData()
        {
            object result;
            using (var response = await httpClient.GetAsync(request + "getmasterdata"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject(apiResponse);
            }
            return result;
        }
        public async Task<object> GetMasterData(string NIK)
        {
            object result;

            using (var response = await httpClient.GetAsync(request + $"getmasterdata/{NIK}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject(apiResponse);
            }
            return result;
        }
        public async Task<object> GetStudentData()
        {
            object result;
            using (var response = await httpClient.GetAsync(request + "getstudentdata"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject(apiResponse);
            }
            return result;
        }

        public Object UpdateNIK(User user)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            Object entities = new Object();
            using (var response = httpClient.PutAsync(request, content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }

            return entities;
        }

        public async Task<object> MasterEnroll()
        {
            object result;
            using (var response = await httpClient.GetAsync(request + "getenrolldata"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject(apiResponse);
            }
            return result;
        }
        public object AssignRequestStatus(User student)
        {
            object results;
            StringContent content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
            using (var response = httpClient.PutAsync(request + "UpdateStatus", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                results = JsonConvert.DeserializeObject(apiResponse);
            }
            return results;
        }
    }
}