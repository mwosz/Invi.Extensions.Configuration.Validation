using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Invi.Extensions.Configuration.Validation
{
    public static class ValidationExtensions
    {
        public static void Validate(this IValidation @this)
        {
            var validation = new List<ValidationResult>();
            if (!Validator.TryValidateObject(@this, new ValidationContext(@this), validation, validateAllProperties: true))
            {
                throw new ValidationException($"{@this} Failed validation.{Environment.NewLine}{validation.Aggregate(new System.Text.StringBuilder(), (sb, vr) => sb.AppendLine(vr.ErrorMessage))}");
            }
        }
    }
}