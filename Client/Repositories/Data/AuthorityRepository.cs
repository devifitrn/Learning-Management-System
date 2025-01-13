using API.Models;
using Client.Base;
using Client.Repositories;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Client.Repositories.Data
{
    public class AuthorityRepository : GeneralRepository<Authority, int>
    {
        public AuthorityRepository(Address address) : base(address, "Authorities/") { }
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

    }
}