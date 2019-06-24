﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Min18YearsIfAMember:ValidationAttribute
    {
        //Custom Validation
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer=(Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId==MembershipType.unknown||customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (customer.BirthDate == null)
                return new ValidationResult("Birthdate is required");

            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;
            return (age >= 18) ? ValidationResult.Success : new ValidationResult("Min 18 years is required to be a membership");
        }
    }
}