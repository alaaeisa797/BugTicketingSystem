using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using BugTicketingSystem.BL.DTOs.User;
using BugTicketingSystem.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BugTicketingSystem.BL.Managers.Users
{
    public class UserManger : IUserManger
    {
        private IConfiguration _configuration;
        private readonly UserRegisterDTOValidator _userRegisterDTOValidator;
        private readonly UserManager<CustomUser> _userManager;

        public UserManger(UserManager<CustomUser> userManger, UserRegisterDTOValidator userRegisterDTOValidator, IConfiguration configuration)
        {
            _userManager = userManger;
            _configuration = configuration;
            _userRegisterDTOValidator = userRegisterDTOValidator;

        }


        public async Task<GeneralResult> AddAsync(UsersRegisterDto userRegisterDTO)
        {
            var validationResult = await _userRegisterDTOValidator.ValidateAsync(userRegisterDTO);
            if (!validationResult.IsValid)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = validationResult.Errors.Select(x =>
                    new ResultError { Code = x.ErrorCode, Message = x.ErrorMessage }).ToArray()
                };
            }

            CustomUser newUserToBeAdded = new CustomUser
            {
                UserName = userRegisterDTO.UserName,
                Email = userRegisterDTO.Email,
            };

            var identityResult = await _userManager.CreateAsync(newUserToBeAdded, userRegisterDTO.Password);

            if (!identityResult.Succeeded)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = identityResult.Errors.Select(e =>
                        new ResultError { Message = e.Description }).ToArray()

                };

            }


            var claims = new List<Claim>
        {

            new (ClaimTypes.NameIdentifier, newUserToBeAdded.Id),
            new (ClaimTypes.Email, newUserToBeAdded.Email),
            new (ClaimTypes.Role, userRegisterDTO.UserRole.ToString() )
        };

            await _userManager.AddClaimsAsync(newUserToBeAdded, claims);

            return GeneralResult.Ok();
        }
        //----------------------------------------------Login--------------------------------------------------------------//
        public async Task<GeneralResult<TokenDTO>> LogInAsync(LoginCredentials loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.UserEmail);
            if (user == null)
            {

                ResultError[] errors = new ResultError[]
{
    new ResultError
    {
        Message = "Invalid Email or Password"
    }
};
                return GeneralResult<TokenDTO>.Fail(errors);
            }
            var isPassValid = await _userManager.CheckPasswordAsync(user, loginUser.Password);

            if (!isPassValid)
            {
                ResultError[] errors = new ResultError[]
               {
                    new ResultError
                    {
                         Message = "Invalid Email or Password"
                    }
                };
                return GeneralResult<TokenDTO>.Fail(errors);
            }
            var claims = await _userManager.GetClaimsAsync(user);
            var tokenDto = GenerateToken(claims.ToList());
            return GeneralResult<TokenDTO>.Ok(tokenDto);


        }
        //------------------------------------------------------------------------------------------------------------//
        private TokenDTO GenerateToken(List<Claim> _Claims)
        {
            var secretKey = _configuration.GetSection("ScretKey").Value;
            var secretKeyInBytes = Encoding.UTF8.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(secretKeyInBytes);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(1)
                , claims: _Claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenDTO(tokenString, token.ValidTo);
        }



    }
}
