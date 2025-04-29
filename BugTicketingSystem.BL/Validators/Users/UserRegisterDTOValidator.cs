using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace BugTicketingSystem.BL
{
    public class UserRegisterDTOValidator : AbstractValidator<UsersRegisterDto>
    {
        private readonly UserManager<CustomUser> _userManager;
        public UserRegisterDTOValidator(UserManager<CustomUser> userManager)
        {
            _userManager = userManager;

            RuleFor(u => u.UserName)
                    .NotEmpty()
                    .WithMessage("Name Shouldn't ne Empty")
                    .MaximumLength(20)
                    .WithMessage("Name Shouldn't Exede 20 Character");
            RuleFor(u => u.Email)
              .NotEmpty()
              .WithMessage("Email shouldn't be empty")
              .EmailAddress()
              .WithMessage("Invalid email format")
              .MustAsync(CheckUserEmailIsUnique)
              .WithMessage("This Email already Exists");

            RuleFor(u => u.UserRole)
              .IsInEnum()
              .WithMessage("Invalid role selected");

            RuleFor(u => u.Password)
             .NotEmpty()
             .WithMessage("Password shouldn't be empty");


        }

        private async Task<bool> CheckUserEmailIsUnique(string arg, CancellationToken token)
        {
            var user = await _userManager.FindByEmailAsync(arg);
            if (user == null)
            {
                return true;
            }
            return false;

        }
    }
}
