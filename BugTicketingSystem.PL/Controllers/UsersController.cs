using BugTicketingSystem.DAL;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BugTicketingSystem.BL;
using BugTicketingSystem.BL.Managers.Users;
using BugTicketingSystem.BL.DTOs.User;


namespace BugTicketingSystem.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManger _userManager;
        public UsersController(IUserManger userManger)
        {
            _userManager = userManger;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> Register(UsersRegisterDto userRegisterDto)
        {
            var result = await _userManager.AddAsync(userRegisterDto);
            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }
        //----------------------------------------------------Login------------------------------------------------------------------------------//
        [HttpPost]
        [Route("Login")]
        public async Task<Results<Ok<GeneralResult<TokenDTO>>, BadRequest<GeneralResult<TokenDTO>>>> Login(LoginCredentials loginCredentials)
        {
            var result = await _userManager.LogInAsync(loginCredentials);
            if (result.Success)
            { 
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        
        }



    }
}
