using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StationCasinos.WebAPI.Utility
{
    public class WebAPIValidationException : WebAPIException
    {

        public WebAPIValidationException(List<ValidationResult> errors, string message) : base(HttpStatusCode.PreconditionFailed, message)
        {
            Errors = errors;
        }

        public WebAPIValidationException(List<ValidationResult> errors) : base(HttpStatusCode.PreconditionFailed)
        {
            Errors = errors;
        }

        public List<ValidationResult> Errors { get; }

        public string ToErrorMessage()
        {
            return this.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(this.Message))
                sb.AppendLine(this.Message);

            PrintResults(Errors, 0, sb);

            return sb.ToString();
        }

        private static void PrintResults(IEnumerable<ValidationResult> results, Int32 indentationLevel, StringBuilder sb)
        {
            foreach (var validationResult in results)
            {
                for (int i = 0; i < indentationLevel; i++)
                    sb.Append("  ");

                sb.AppendLine(validationResult.ErrorMessage);
                
                if (validationResult is CompositeValidationResult)
                {
                    PrintResults(((CompositeValidationResult)validationResult).Results, indentationLevel + 1, sb);
                }
            }
        }
    }
}
