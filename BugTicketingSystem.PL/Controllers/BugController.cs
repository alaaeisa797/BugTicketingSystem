using BugTicketingSystem.BL.Managers.Projects;
using BugTicketingSystem.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using BugTicketingSystem.BL.Managers.Attachements;

namespace BugTicketingSystem.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : ControllerBase
    {
        private readonly IBugManager _bugManager;
        private readonly IUserBugsManager _userBugsManager;
        private readonly IAttachementManager _attachementManager;

        public BugController(IBugManager bugManager, IUserBugsManager userBugsManager, IAttachementManager attachementManager)
        {
            _bugManager = bugManager;
            _userBugsManager = userBugsManager;
            _attachementManager = attachementManager;
        }
        //----------------------------------------------------AddBug------------------------------------------------------------------------------//
        [HttpPost]
        [Authorize]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> AddAsync(BugAddDTO bugAddDTO)
        {
            var result = await _bugManager.AddAsync(bugAddDTO);
            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }
        //----------------------------------------------------GetAllBugsWithoutJoin------------------------------------------------------------------------------//
        [HttpGet]
        [Authorize]
        public async Task<Results<Ok<GeneralResult<List<BugReadDTO>>>, NotFound>> GetAllAsync()
        {
            var Projects = await _bugManager.GetAllAsync();

            if (Projects == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(Projects);

        }

        //----------------------------------------------------GetAllBugsWithDetailedInfo------------------------------------------------------------------------------//
        [HttpGet("{id}")]
        [Authorize]
        public async Task<Results<Ok<GeneralResult<BugReadDetailesDTO>>, NotFound>> GetByIdAsync(Guid id)
        {
            var detailedBug = await _bugManager.GetByIdWithJoinAsync(id);

            if (detailedBug == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(detailedBug);

        }
        //----------------------------------------------------Assign Bug To User------------------------------------------------------------------------------//
        [HttpPost("{bug_Id}/assignees")]
        [Authorize]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> AssignUserToBug(Guid bug_Id, [FromBody] AssignBugToUserRequest requestBody)
        {
            UserBugAssignDTO userBugAssignDTO = new UserBugAssignDTO
            {
                Bug_Id = bug_Id,
                User_Id = requestBody.User_Id,
            };
            var result = await _userBugsManager.AssignBugAsync(userBugAssignDTO);
            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }

        //----------------------------------------------------UnAssign Bug To User------------------------------------------------------------------------------//
        [HttpDelete("{bug_Id}/assignees/{user_Id}")]
        [Authorize]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> UnAssignUserFromBug(Guid bug_Id, string user_Id)
        {
            UserBugAssignDTO userBugAssignDTO = new UserBugAssignDTO
            {
                Bug_Id = bug_Id,
                User_Id = user_Id,
            };
            var result = await _userBugsManager.UnAssignUserFromABugAsync(userBugAssignDTO);
            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }
        //----------------------------------------------------add attachement to Bug ------------------------------------------------------------------------------//
        [HttpPost("{bug_Id}/attachments")]

        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> AddAsync(Guid bug_Id, [FromForm] FileUploadRequest fileRequest)
        {
            var result = await _attachementManager.AddAttachementAsync(bug_Id, fileRequest);

            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }

        //----------------------------------------------------GetAllBugsWithDetailedInfo------------------------------------------------------------------------------//
        [HttpGet("{id}/attachments")]

        public async Task<Results<Ok<GeneralResult<BugWithAttachementReadDTO>>, NotFound>> GetBugByIdWithAttachementAsync(Guid id)
        {
            var detailedBug = await _attachementManager.GetBugByIdWithAttachementJoinAsync(id);

            if (detailedBug == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(detailedBug);

        }
        //----------------------------------------------------delete attachement from bug------------------------------------------------------------------------------//
        [HttpDelete("{bug_Id}/attachments/{attachement_Id}")]
       
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> delteAttachementFromBug(Guid bug_Id, Guid attachement_Id)
        {
           
            var result = await _attachementManager.DeleteAttachementFromBug(attachement_Id);
            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }


    }
}
