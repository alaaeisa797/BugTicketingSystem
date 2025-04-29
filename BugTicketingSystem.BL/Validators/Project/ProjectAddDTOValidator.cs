using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BugTicketingSystem.BL
{
    public class ProjectAddDTOValidator : AbstractValidator<ProjectAddDTO>
    {
        public ProjectAddDTOValidator()
        {
            RuleFor(p => p.ProjectName)
                .NotEmpty().WithMessage("Project name is required")
                .MaximumLength(100).WithMessage("Project name must not exceed 100 characters.");

            RuleFor(p => p.ProjectDescription)
                .NotEmpty().WithMessage("Project description is required")
                .MaximumLength(500).WithMessage("Project description must not exceed 500 characters");

        }
    }
}
