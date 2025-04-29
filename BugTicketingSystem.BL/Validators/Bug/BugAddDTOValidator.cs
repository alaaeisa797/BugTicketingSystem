using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BugTicketingSystem.BL.Validators.Bug
{
    public class BugAddDTOValidator  : AbstractValidator<BugAddDTO>
    {
        public BugAddDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Bug Title is required")
                .MaximumLength(100).WithMessage("Bug Title must not exceed 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Bug Description is required")
                .MaximumLength(500).WithMessage("Bug Description must not exceed 500 characters");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid status value")
                .When(x => x.Status.HasValue);

            RuleFor(x => x.Priority)
                .IsInEnum().WithMessage("Invalid priority value")
                .When(x => x.Priority.HasValue);

            //RuleFor(x => x.CreatedAt)
            //    .LessThanOrEqualTo(DateTime.Now)
            //    .WithMessage("CreatedAt cannot be in the future");

            RuleFor(b => b.Project_Id)
                .NotEmpty().WithMessage("Project ID is required")
                .NotEqual(Guid.Empty).WithMessage("Project ID must be a valid GUID");

            //RuleFor(x => x.UpdatedAt)
            //    .GreaterThanOrEqualTo(x => x.CreatedAt)
            //    .When(x => x.UpdatedAt.HasValue && x.CreatedAt.HasValue)
            //    .WithMessage("UpdatedAt cannot be earlier than CreatedAt");
        }
    }
}
