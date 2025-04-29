using BugTicketingSystem.BL;
using BugTicketingSystem.BL.Managers.Projects;
using BugTicketingSystem.BL.Managers.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BugTicketingSystem.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectManager _projectManager;
        public ProjectsController(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }
        //----------------------------------------------------AddProject------------------------------------------------------------------------------//
        [HttpPost]
        [Authorize]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> AddAsync(ProjectAddDTO projectAddDTO)
        {
            var result = await _projectManager.AddAsync(projectAddDTO);
            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }
        //----------------------------------------------------GetAllProjectWithoutJoin------------------------------------------------------------------------------//
        [HttpGet]
        [Authorize]

        public async Task<Results<Ok<GeneralResult<List<ProjectReadDTO>>>, NotFound>> GetAllAsync()
        {
            var Projects = await _projectManager.GetAllAsync();

            if (Projects == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(Projects);

        }
        //----------------------------------------------------GetAllProjectWithJoin------------------------------------------------------------------------------//
        [HttpGet("{id}")]
        [Authorize]
        public async Task<Results<Ok<GeneralResult<ProjectReadWithdetailesDTO>>, NotFound>> GetByIdAsync(Guid id)
        {
            var Projects = await _projectManager.GetByIdWithJoinAsync(id);

            if (Projects == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(Projects);

        }
    }
}
