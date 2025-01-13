using API.Models;
using Client.Base;
using Client.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Client.Repositories;

namespace Client.Repositories.Data
{
    public class AccountRepository : GeneralRepository<Account, string>
    {
        public AccountRepository(Address address) : base(address, "Accounts/") { }
        public object Register(RegisterVM registerVM)
        {
            object responseJSON;
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request + "register", content).Result)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;
                responseJSON = JsonConvert.DeserializeObject(responseString);
            }
            return responseJSON;
        }
        /*public Object Register(RegistrationVM registrationVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(registrationVM), Encoding.UTF8, "application/json");
            Object entities = new Object();
            using (var response = httpClient.PostAsync(request + "Register", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }

            return entities;
        }*/

        public JWTokenVM login(LoginVM login)
        {

            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            JWTokenVM token;
            using (var response = httpClient.PostAsync(request + "Login", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<JWTokenVM>(apiResponse);
            }
            return token;
        }

    }
}
