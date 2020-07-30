using Routines.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Routines.Api.ValidationAttributes
{
    public class EmploeeNoMustDifferentFromFirstNameAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var adddto = (EmployeeAddDto)value;  //和下面相同 value可能是属
            var adddto = (EmployeeAddOrUpdateDto)validationContext.ObjectInstance;//返回对象类
            if(adddto.EmployeeNo==adddto.FirstName)
            {
                return new ValidationResult(ErrorMessage, new[] { nameof(EmployeeAddOrUpdateDto) });
            }
            return ValidationResult.Success;
        }
    }
}
