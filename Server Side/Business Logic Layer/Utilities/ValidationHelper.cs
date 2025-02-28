using Core_Layer.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
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
    }
}
