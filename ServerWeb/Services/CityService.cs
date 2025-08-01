using Microsoft.AspNetCore.Components;
using ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServerWeb.Services
{
    public class CityService:ICityService
    {
        private readonly HttpClient httpClient;

        public CityService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async  Task<IEnumerable<City>> GetCities(int countryid)
        {
            return await httpClient.GetJsonAsync<City[]>($"api/City/{countryid}");
        }
    }
}
