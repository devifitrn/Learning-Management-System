﻿using Client.Base;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories
{
    public class GeneralRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class
    {
        protected readonly Address address;
        protected readonly string request;
        protected readonly IHttpContextAccessor _contextAccessor;
        protected readonly HttpClient httpClient;


        public GeneralRepository(Address address, string request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };

            /*httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));*/
        }

        public HttpStatusCode Delete(TId id)
        {
            var result = httpClient.DeleteAsync(request + id).Result;
            return result.StatusCode;
        }

        public async Task<List<TEntity>> Get()
        {
            List<TEntity> entities = new List<TEntity>();


            using (var response = await httpClient.GetAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dynamic obj = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                    entities = obj.result.ToObject<List<TEntity>>();
                    /*entities = JsonConvert.DeserializeObject<List<TEntity>>(apiResponse);*/
                }
            }
            return entities;
        }

        public async Task<TEntity> Get(TId id)
        {
            TEntity entity = null;

            using (var response = await httpClient.GetAsync(request + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dynamic obj = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                    entity = obj.result.ToObject<TEntity>();
                    /*entity = JsonConvert.DeserializeObject<TEntity>(apiResponse);*/
                }
            }
            return entity;
        }

        public HttpStatusCode Put(TEntity entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(request, content).Result;
            return result.StatusCode;
        }

        public HttpStatusCode Post(TEntity entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(request, content).Result;
            return result.StatusCode;
        }
    }
}
