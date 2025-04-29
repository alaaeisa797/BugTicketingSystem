using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL;
using BugTicketingSystem.DAL.Models;

namespace BugTicketingSystem.BL
{
    public class UserBugsManager : IUserBugsManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserBugsManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //----------------------------------------------------Assign Bug To A user------------------------------------------------------------------------------//
        public async Task<GeneralResult> AssignBugAsync(UserBugAssignDTO userBugAssignDTO)
        {

            UserBug newUserBugToBeAssigned = new UserBug
            {
                Bug_Id = userBugAssignDTO.Bug_Id,
                User_Id = userBugAssignDTO.User_Id
            };


            var result = await _unitOfWork._userBugRepo.IsBugAssignedToUserBefore(newUserBugToBeAssigned.Bug_Id, newUserBugToBeAssigned.User_Id);
            if (result != null)
            {
                // this means tha the user is already assigned with this bug before 
                ResultError[] errors = new ResultError[]
                                                        {
                                 new ResultError
                                                  {
                                                     Code = null ,
                                                     Message = "user already assigned with this bug before"
                                                   }
                                                        };
                return GeneralResult.Fail(errors);

            }

            _unitOfWork._userBugRepo.Add(newUserBugToBeAssigned);


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
        Message = "unable to assign this bug ro this user"
    }
};
                return GeneralResult.Fail(errors);

            }

        }

        //----------------------------------------------------Unassign a user from a bug-----------------------------------------------------------------------//
        public async Task<GeneralResult> UnAssignUserFromABugAsync(UserBugAssignDTO userBugAssignDTO)
        {
            
            UserBug newUserBugToBeDeleted = new UserBug
            {
                Bug_Id = userBugAssignDTO.Bug_Id,
                User_Id = userBugAssignDTO.User_Id
            };


            var result = await _unitOfWork._userBugRepo.IsBugAssignedToUserBefore(newUserBugToBeDeleted.Bug_Id, newUserBugToBeDeleted.User_Id);
            if (result == null)
            {
                // this means tha the user isn't assigned with this bug before 
                ResultError[] errors = new ResultError[]
                                                        {
                                 new ResultError
                                                  {
                                                     Code = null ,
                                                     Message = "user isnt assigned to to this bug"
                                                   }
                                                        };
                return GeneralResult.Fail(errors);

            }



            _unitOfWork._userBugRepo.Delete(newUserBugToBeDeleted);


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
        Message = "Unable to Unassign a user from a bug"
    }
};
                return GeneralResult.Fail(errors);

            }

        }
    }
}
