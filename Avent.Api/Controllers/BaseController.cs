using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Avent.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected void Validate(dynamic request)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(request, null, null);
            if (!Validator.TryValidateObject(request, context, results, true))
            {
                throw new ArgumentException(string.Join(", ", results.Select(x => x.ErrorMessage)));
            }
        }
    }
}
