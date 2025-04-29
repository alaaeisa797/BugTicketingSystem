using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL;
using BugTicketingSystem.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BugTicketingSystem.BL.Managers.Projects
{
    public class ProjectManager : IProjectManager
    {

        private readonly ProjectAddDTOValidator _projectAddDTOValidator;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectManager(IUnitOfWork unitOfWork, ProjectAddDTOValidator projectAddDTOValidator)
        {

            _unitOfWork = unitOfWork;
            _projectAddDTOValidator = projectAddDTOValidator;

        }

        //----------------------------------------------------AddProject------------------------------------------------------------------------------//
        public async Task<GeneralResult> AddAsync(ProjectAddDTO projectAddDTO)
        {
            var validationResult = await _projectAddDTOValidator.ValidateAsync(projectAddDTO);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(x =>
                     new ResultError { Code = x.ErrorCode, Message = x.ErrorMessage }).ToArray();
                return GeneralResult.Fail(errors);
            }
            Project newProjectToBeAdded = new Project
            {
                ProjectName = projectAddDTO.ProjectName,
                Description = projectAddDTO.ProjectDescription
            };


            _unitOfWork._projectRepo.Add(newProjectToBeAdded);


            var saveResult = await _unitOfWork.SaveChangesAsync();

            if (saveResult > 0)
            {
                return GeneralResult.Ok();
            }
            else
            {
                ResultError[] errors = new ResultError[]
{
    new ResultError
    {
        Code = null ,
        Message = "unable to add this project"
    }
};
                return GeneralResult.Fail(errors);

            }

        }
        //----------------------------------------------------GetAllProjectsWithoutJoin------------------------------------------------------------------------------//
        public async Task<GeneralResult<List<ProjectReadDTO>>> GetAllAsync()
        {
            var projectsFromDb = await _unitOfWork._projectRepo.GetAllAsync();
            if (projectsFromDb != null || projectsFromDb.Count != 0)
            {
                var mappedDbProjectObjects = projectsFromDb.Select(x => new ProjectReadDTO
                {
                    ProjectId = x.ProjectId,
                    ProjectName = x.ProjectName,
                    ProjectDescription = x.Description,
                }).ToList();
                return GeneralResult<List<ProjectReadDTO>>.Ok(mappedDbProjectObjects);
            }
            ResultError[] errors = new ResultError[]
{
    new ResultError
    {
        Code = null ,
        Message = "their is no projects "
    }
};
            return GeneralResult<List<ProjectReadDTO>>.Fail(errors);

        }

        //----------------------------------------------------GetAllProjectsWithBugjoin------------------------------------------------------------------------------//
        public async Task<GeneralResult<ProjectReadWithdetailesDTO>> GetByIdWithJoinAsync(Guid id)
        {
            var project = await _unitOfWork._projectRepo.GetProjectWithHisBugsAsync(id);
            if (project == null)
            {
                ResultError[] errors = new ResultError[]
{
    new ResultError
    {
        Code = null ,
        Message = "their is no project with this id"
    }
};
                return GeneralResult<ProjectReadWithdetailesDTO>.Fail(errors);
            }
            else
            {

                var projectReadWithDetailesDTO = new ProjectReadWithdetailesDTO
                {
                    ProjectName = project.ProjectName,
                    BugsOfProject = project.Bugs.Select(e => new BugReadFromProjectDTO
                    {
                        BugTitle = e.Title,
                        BugDescription = e.Description,
                        BugUpdatedAt = e.UpdatedAt,
                        BugCreatedAt = e.CreatedAt,
                        BugPriority = e.Priority,
                    }).ToList()
                };
                return GeneralResult<ProjectReadWithdetailesDTO>.Ok(projectReadWithDetailesDTO);
            }
        }
    }
}
