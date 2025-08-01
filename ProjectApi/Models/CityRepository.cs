using Microsoft.EntityFrameworkCore;
using ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Models
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext appDbContext;

        public CityRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;

        }
        public async Task<IEnumerable<City>> GetCities(int countryid)
        {
            IQueryable<City> cities = appDbContext.Cities;
            cities = cities.Where(e => e.CountryId == countryid);

            return await cities.ToListAsync();
        }
        public async Task<City> GetCity(int cityid)
        {
            return await appDbContext.Cities
               .FirstOrDefaultAsync(d => d.CityId == cityid);
        }
    }
}
