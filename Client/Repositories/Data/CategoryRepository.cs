using API.Models;
using Client.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Repositories;
using System.Text;

namespace Client.Repositories.Data
{
    public class CategoryRepository : GeneralRepository<Category, int>
    {
        public CategoryRepository(Address address, string request = "Categories/") : base(address, request) { }
    }

}