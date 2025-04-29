

using BugTicketingSystem.BL.Validators.Bug;
using BugTicketingSystem.DAL;


namespace BugTicketingSystem.BL
{
    public class BugManager : IBugManager
    {
        private readonly BugAddDTOValidator _BugAddDTOValidator;
        private readonly IUnitOfWork _unitOfWork;

        public BugManager(IUnitOfWork unitOfWork, BugAddDTOValidator BugAddDTOValidator)
        {

            _unitOfWork = unitOfWork;
            _BugAddDTOValidator = BugAddDTOValidator;

        }

        //----------------------------------------------------AddBug----------------------------------------------------------------------------------------------//
        public async Task<GeneralResult> AddAsync(BugAddDTO bugAddDTO)
        {
            var validationResult = await _BugAddDTOValidator.ValidateAsync(bugAddDTO);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(x =>
                     new ResultError { Code = x.ErrorCode, Message = x.ErrorMessage }).ToArray();
                return GeneralResult.Fail(errors);
            }

            var projectToAssignBugTo = await _unitOfWork._projectRepo.GetByIdAsync(bugAddDTO.Project_Id);
            if (projectToAssignBugTo != null)
            {

                Bug newBugToBeAdded = new Bug
                {
                    Title = bugAddDTO.Title,
                    CreatedAt = DateTime.Now,
                    Description = bugAddDTO.Description,
                    Status = bugAddDTO.Status,
                    Priority = bugAddDTO.Priority,
                    Project_Id = bugAddDTO.Project_Id,


                };


                _unitOfWork._bugRepo.Add(newBugToBeAdded);


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
        Message = "unable to add this bug"
    }
    };
                    return GeneralResult.Fail(errors);

                }
            }
            else
            {
                ResultError[] errors = new ResultError[]
{
    new ResultError
    {
        Code = null ,
        Message = "no project with this id"
    }
};
                return GeneralResult.Fail(errors);

            }

        }
        //----------------------------------------------------GetAllBugsWithoutJoin-------------------------------------------------------------------------------//
        public async Task<GeneralResult<List<BugReadDTO>>> GetAllAsync()
        {
            var bugsFromDb = await _unitOfWork._bugRepo.GetAllAsync();
            if (bugsFromDb != null || bugsFromDb.Count != 0)
            {
                var mappedDbProjectObjects = bugsFromDb.Select(x => new BugReadDTO
                {
                    BugTitle = x.Title,
                    Status = x.Status.ToString(),
                    BugId = x.BugId,
                }).ToList();
                return GeneralResult<List<BugReadDTO>>.Ok(mappedDbProjectObjects);
            }
            ResultError[] errors = new ResultError[]
{
    new ResultError
    {
        Code = null ,
        Message = "their is no Bugs "
    }
};
            return GeneralResult<List<BugReadDTO>>.Fail(errors);

        }

        //----------------------------------------------------GetAllBugsWithitsAlInfo-----------------------------------------------------------------------------//
        public async Task<GeneralResult<BugReadDetailesDTO>> GetByIdWithJoinAsync(Guid id)
        {
            var detailedBug = await _unitOfWork._bugRepo.GetBugWithHisAttachmentAsync(id);
            if (detailedBug == null)
            {
                ResultError[] errors = new ResultError[]
{
    new ResultError
    {
        Code = null ,
        Message = "their is no bug with this id"
    }
};
                return GeneralResult<BugReadDetailesDTO>.Fail(errors);
            }
            else
            {

                var bugReadWithDetailesDTO = new BugReadDetailesDTO
                {
                    ProjectName = detailedBug.Project.ProjectName,
                    AssigneesNamesOfBug = detailedBug.UserBugs
                        .Select(us => us.CustomUser != null ? us.CustomUser.UserName : "unknown user").ToList(),
                    BugDescription = detailedBug.Description,
                    Status = detailedBug.Status.ToString(),
                    Priority = detailedBug.Priority.ToString(),
                    CreatedAt = detailedBug.CreatedAt,
                    UpdatedAt = detailedBug.UpdatedAt,
                    BugId = detailedBug.BugId,
                    BugTitle = detailedBug.Title

                };
                return GeneralResult<BugReadDetailesDTO>.Ok(bugReadWithDetailesDTO);
            }
        }
    }
}