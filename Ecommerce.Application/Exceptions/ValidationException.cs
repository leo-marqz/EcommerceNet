
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures.GroupBy((err) => err.PropertyName, err => err.ErrorMessage)
                .ToDictionary(
                        (failureGroup) =>failureGroup.Key, 
                        (failureGroup) => failureGroup.ToArray()
                    );
        }
    }
}
