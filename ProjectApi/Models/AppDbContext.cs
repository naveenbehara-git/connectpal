using Microsoft.EntityFrameworkCore;
using ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi
{
    public class AppDbContext : DbContext
    {
        

        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed for country
            modelBuilder.Entity<Country>().HasData(
                new Country { CountryID = 1, CountryName = "India" });
            modelBuilder.Entity<Country>().HasData(
                new Country { CountryID = 2, CountryName = "UK" });
            modelBuilder.Entity<Country>().HasData(
                new Country { CountryID = 3, CountryName = "USA" });

            //seed for City

            modelBuilder.Entity<City>().HasData(
               new City { CityId = 1, CityName = "Hyderabad", CountryId = 1 });
            modelBuilder.Entity<City>().HasData(
              new City { CityId = 2, CityName = "Bangalore", CountryId = 1 });
            modelBuilder.Entity<City>().HasData(
              new City { CityId = 3, CityName = "Mumbai", CountryId = 1 });
            modelBuilder.Entity<City>().HasData(
              new City { CityId = 4, CityName = "London1", CountryId = 2 });
            modelBuilder.Entity<City>().HasData(
              new City { CityId = 5, CityName = "London", CountryId = 2 });
            modelBuilder.Entity<City>().HasData(
              new City { CityId = 6, CityName = "Los Angeles", CountryId = 3 });
            modelBuilder.Entity<City>().HasData(
              new City { CityId = 7, CityName = "Washington", CountryId = 3 });
            modelBuilder.Entity<City>().HasData(
              new City { CityId = 8, CityName = "Malibu", CountryId = 3 });


            //seed for posts
            modelBuilder.Entity<Post>().HasData(
             new Post {PostId=20, Message = "Santga Monica", UserId = 2, Image = null });


            ////seed for Users
            //modelBuilder.Entity<User>().HasData(
            // new User {
            //     UserId = 1,
            //     CityID = 1,
            //     CountryId = 1,
            //     DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            //     Gender = 0,
            //     MobileNumber = 7997404583,
            //     UserEmail = "naveen@gmail.com",
            //     UserName = "Malibu"
            // });
            //modelBuilder.Entity<User>().HasData(
            // new User
            // {
            //     UserId = 2,
            //     CityID = 1,
            //     CountryId = 1,
            //     DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            //     Gender = 0,
            //     MobileNumber = 7674057034,
            //     UserEmail = "nav@gmail.com",
            //     UserName = "Master"
            // });

        }
    }
}
