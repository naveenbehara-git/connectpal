using Microsoft.AspNetCore.Components;
using ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServerWeb.Services
{
    public class CountryService:ICountryService
    {
        private readonly HttpClient httpClient;

        public CountryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async  Task<IEnumerable<Country>> GetCountries()
        {
            return await httpClient.GetJsonAsync<Country[]>("api/Country");
        }
    }
}
