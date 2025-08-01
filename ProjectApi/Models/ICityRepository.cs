using ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Models
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCities(int countryid);
        Task<City> GetCity(int cityid);
    }
}
