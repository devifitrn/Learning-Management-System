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
    public class SubContentRepository : GeneralRepository<SubContent, int>
    {
        public SubContentRepository(Address address, string request = "SubContents/") : base(address, request) { }
    }
    
}