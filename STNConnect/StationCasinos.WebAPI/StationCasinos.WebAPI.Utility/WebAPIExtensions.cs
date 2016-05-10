using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationCasinos.WebAPI.Utility
{
    public static class WebAPIExtensions
    {
        /// <summary>
        /// Run annotation validation on a given object of Type T.
        /// Throws <see cref="WebAPIValidationException"/> when validation error is found.
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="obj"></param>
        public static void Validate<T>(this T obj)
        {
            var context = new ValidationContext(obj, null, null);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(obj, context, results, true);
            
            if(results.Count != 0)
                throw new WebAPIValidationException(results, string.Format("Validation of {0} object failed.", obj.GetType().ToString()));
        }

    }
}
