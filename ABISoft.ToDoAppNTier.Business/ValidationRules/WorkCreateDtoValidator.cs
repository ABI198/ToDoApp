using ABISoft.ToDoAppNTier.Dtos.WorkDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABISoft.ToDoAppNTier.Business.ValidationRules
{
    public class WorkCreateDtoValidator : AbstractValidator<WorkCreateDto>
    {
        public WorkCreateDtoValidator()
        {
            RuleFor(x => x.Definition).NotEmpty();

            //RuleFor(x => x.Definition).NotEmpty().WithMessage("Bu alan bos gecilemez");
            //RuleFor(x => x.Definition).NotEmpty().WithMessage("Definition is required")
            //    .When(x => x.IsCompleted)
            //        .Must(x => x != "Batu" && x != "batu").WithMessage("Definition shouldn't be \"Batu\" or \"batu\"");
        }
    }
}
