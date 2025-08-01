using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModels
{
    class DomainValidator:ValidationAttribute
    {
        public string AllowedDomain { get; set; }


        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {

            if (value.ToString().Contains("@"))
            {
                string[] strings = value.ToString().Split('@');
                if (strings[1].ToUpper() == AllowedDomain.ToUpper())
                {
                    return null;
                }
                return new ValidationResult($"Domain must be {AllowedDomain}",
                new[] { validationContext.MemberName });
            }
            else
            {
                return new ValidationResult($"Domain must be {AllowedDomain}",
                 new[] { validationContext.MemberName });
            }



        }
    }
}
