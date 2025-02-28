using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core_Layer.Exceptions;

namespace BUS_E_TICKET.Utilities
{
    internal class ValidationHelper
    {
        static public void ModelsErrorCollector(ModelStateDictionary modelState)
        {
            if (modelState.IsValid) return;
            throw new BadRequestException(string.Join(", ", modelState.Values.Select(i => i.Errors)));
        }
    }
}
