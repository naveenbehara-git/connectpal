using ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWeb.Services
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetCities(int countryid);
    }
}
