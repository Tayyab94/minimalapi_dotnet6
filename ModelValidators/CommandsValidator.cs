using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using SixMinAPI.DTOs;
using SixMinAPI.Models;


// https://www.infoworld.com/article/3676077/use-model-validation-in-minimal-apis-in-aspnet-core-6.html
// For ModelState Validatio... please check this link
namespace SixMinAPI.ModelValidators
{
    // for this we need to install Package FluentValidation.AspNetCore
    public class CommandsValidator :AbstractValidator<CommandCreateDTO>
    {
        public CommandsValidator()
        {
            // RuleFor(s=>s.Id)
            RuleFor(s=>s.CommandLine).NotNull();
            RuleFor(s=>s.Howto).NotNull();
            RuleFor(s=>s.Plateform).NotNull().MaximumLength(5);
        }
    }
}