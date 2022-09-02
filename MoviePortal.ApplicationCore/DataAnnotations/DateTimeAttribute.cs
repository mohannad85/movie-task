using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviePortal.ApplicationCore.DataAnnotations
{
    public class DateTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (!DateTime.TryParseExact((string)value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return new ValidationResult("Date format should be yyyy-MM-dd HH:mm:ss.");
            }

            if (date < SqlDateTime.MinValue)
            {
                return new ValidationResult($"Date must not be earlier than {SqlDateTime.MinValue.Value:yyyy-MM-dd HH:mm:ss}");
            }

            if (date > SqlDateTime.MaxValue)
            {
                return new ValidationResult($"Date must not be later than {SqlDateTime.MaxValue.Value:yyyy-MM-dd HH:mm:ss}");
            }

            return ValidationResult.Success;
        }
    }
}
