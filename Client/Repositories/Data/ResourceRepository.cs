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
    public class ResourceRepository : GeneralRepository<Resource, int>
    {
        public ResourceRepository(Address address, string request = "Resources/") : base(address, request) { }
    }
    
}