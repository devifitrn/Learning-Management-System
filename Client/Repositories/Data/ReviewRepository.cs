using API.Models;
using Client.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace Client.Repositories.Data
{
    public class ReviewRepository : GeneralRepository<Review, int>
    {
        public ReviewRepository(Address address, string request = "Reviews/") : base(address, request){ }
        public async Task<object> GetCourseReviews(int courseId)
        {
            object result;
            using (var response = await httpClient.GetAsync(request + $"GetCourseReviews/{courseId}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject(apiResponse);
            }
            return result;
        }
        
        public async Task<object> AvgRating(int id)
        {
            dynamic obj = null;

            using (var response = await httpClient.GetAsync(request + "AvgRating/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                    /*entity = JsonConvert.DeserializeObject<TEntity>(apiResponse);*/
                }
            }
            return obj;
        }
        public async Task<Review> CheckReview(string userId, int id)
        {
            Review entity = null;

            using (var response = await httpClient.GetAsync(request + "CheckReview/" + userId + "/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dynamic obj = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                    entity = obj.ToObject<Review>();
                    /*entity = JsonConvert.DeserializeObject<TEntity>(apiResponse);*/
                }
            }
            return entity;
        }
    }
    
}