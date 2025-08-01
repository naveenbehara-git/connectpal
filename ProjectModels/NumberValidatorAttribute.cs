using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    class NumberValidatorAttribute : ValidationAttribute
    {
        public string MobileNumber { get; set; }
        protected override ValidationResult IsValid(object value,
           ValidationContext validationContext)
        {
            
            MobileNumber = value.ToString();
            if((MobileNumber[0]=='6' || MobileNumber[0] == '7' || MobileNumber[0] == '8' || MobileNumber[0] == '9') && (MobileNumber.Length==10))
            {
                return null;
            }
            return new ValidationResult(ErrorMessage,
                new[] { validationContext.MemberName });
        }
    }
}
