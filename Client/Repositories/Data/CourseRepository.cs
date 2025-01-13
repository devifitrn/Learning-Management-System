using API.Models;
using Client.Base;
using Client.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Client.Repositories;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using API.Models.Views;

namespace Client.Repositories.Data
{
    public class CourseRepository : GeneralRepository<Course, int>
    {
        public CourseRepository(Address address, string request = "Courses/") : base(address, request) { }
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
        public async Task<List<CourseMasterVM>> GetCourseData()
        {
            List<CourseMasterVM> entities = new List<CourseMasterVM>();

            using (var response = await httpClient.GetAsync(request + "mastercourse"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                entities = obj.result.ToObject<List<CourseMasterVM>>();
                //entities = JsonConvert.DeserializeObject<List<CourseMasterVM>>(apiResponse);
            }
            return entities;
        }
        public HttpResponseMessage UpdateStatus(Course course)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(course), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(address.link + request + "UpdateStatus", content).Result;
            return result;
        }
        public async Task<List<Course>> GetByUser(string Id)
        {
            List<Course> entities = new List<Course>();

            using (var response = await httpClient.GetAsync(request + "getbyuser/" + Id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                entities = obj.result.ToObject<List<Course>>();
                //entities = JsonConvert.DeserializeObject<List<CourseMasterVM>>(apiResponse);
            }
            return entities;
        }
    }
}
