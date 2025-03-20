using Core_Layer.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Utilities
{
    internal class ValidationHelper
    {
        public static void ValidateEntity(object entity)
        {
            var validationContext = new ValidationContext(entity, null, null);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity, validationContext, validationResults, true);

            if (!isValid)
            {
                var errors = string.Join(", ", validationResults.Select(v => v.ErrorMessage));
                throw new BadRequestException($"Validation failed: {errors}");
            }
        }
        public static void ValidateEntities(IEnumerable<object> entities)
        {
            foreach(var e in entities)
            {
                ValidateEntity(e);
            }
        }
        public static void ValidatieEmail(string Email)
        {
            if (string.IsNullOrWhiteSpace(Email) || !new EmailAddressAttribute().IsValid(Email))
                throw new BadRequestException("Invalid email format.");
        }
        public static void ValidatieNumber(string PhoneNumber)
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber) || !Regex.IsMatch(PhoneNumber, @"^\+?\d{9,15}$"))
            {
                throw new BadRequestException("Invalid phone number format.");
            }
        }
    }
}
