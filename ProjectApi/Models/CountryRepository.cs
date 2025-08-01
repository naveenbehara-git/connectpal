using Microsoft.EntityFrameworkCore;
using ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Models
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext appDbContext;

        public CountryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;

        }
        public async  Task<IEnumerable<Country>> GetCountries()
        {
            return await appDbContext.Countries.ToListAsync();
        }

        public async Task<Country> GetCountry(int countryid)
        {
            return await appDbContext.Countries
               .FirstOrDefaultAsync(d => d.CountryID == countryid);
        }
    }
}
