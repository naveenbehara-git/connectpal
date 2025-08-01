using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;

namespace ProjectModels
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        
        public string UserEmail { get; set; }

        public string UserName { get; set; }

        [Required]
        [NumberValidator(ErrorMessage = "Enter Correct Mobile Number")]
        public long MobileNumber  { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Country Country { get; set; }

        [Required]
        [Range(1,5,ErrorMessage ="PLease Select Valid Country")]
        public int CountryId { get; set; }
        public City City { get; set; }

        [Required]
        [Range(1, 13, ErrorMessage = "PLease Select Valid City")]
        public int CityID { get; set; }
        public string Friends { get; set; }
        public string Requests { get; set; }

        public byte[] ProfilePic { get; set; }
        public string  ImageUrl { get; set; }

        
    }
}
