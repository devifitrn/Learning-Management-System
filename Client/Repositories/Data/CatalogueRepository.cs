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
    public class CatalogueRepository : GeneralRepository<Catalogue, int>
    {
        public CatalogueRepository(Address address, string request = "Catalogues/") : base(address, request){ }
    }
    
}