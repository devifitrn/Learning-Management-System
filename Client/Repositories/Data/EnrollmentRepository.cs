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
    public class EnrollmentRepository : GeneralRepository<Enrollment, int>
    {
        public EnrollmentRepository(Address address) : base(address, "Enrollments/") { }
        //public object Register(RegisterVM registerVM)
        //{
        //    object responseJSON;
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
        //    using (var response = httpClient.PostAsync(request + "register", content).Result)
        //    {
        //        string responseString = response.Content.ReadAsStringAsync().Result;
        //        responseJSON = JsonConvert.DeserializeObject(responseString);
        //    }
        //    return responseJSON;
        //}
        public Object GetEnrollment(Enrollment enrollment)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(enrollment), Encoding.UTF8, "application/json");
            Object entities = new Object();
            using (var response = httpClient.PutAsync(request, content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }

            return entities;
        }

        public async Task<object> GetEnrollData(string UserId)
        {
            object result;
            using (var response = await httpClient.GetAsync(request + $"getenrollmentdata/{UserId}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject(apiResponse);
            }
            return result;
        }
        public Enrollment PostReturn(Enrollment enrollment)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(enrollment), Encoding.UTF8, "application/json");
            Enrollment entities = null;
            using (var response = httpClient.PostAsync(request + "PostReturn", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                entities = obj.result.ToObject<Enrollment>();
            }

            return entities;
        }
        public async Task<Enrollment> CheckEnrollment(string userId, int id)
        {
            Enrollment entity = null;

            using (var response = await httpClient.GetAsync(request + "CheckEnrollment/" + userId + "/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dynamic obj = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                    entity = obj.ToObject<Enrollment>();
                    /*entity = JsonConvert.DeserializeObject<TEntity>(apiResponse);*/
                }
            }
            return entity;
        }

    }
}