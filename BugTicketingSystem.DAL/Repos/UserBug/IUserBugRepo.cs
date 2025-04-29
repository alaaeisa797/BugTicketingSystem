using BugTicketingSystem.DAL.Models;

namespace BugTicketingSystem.DAL
{
    public interface IUserBugRepo : IGenericRepo<UserBug>
    {
        Task<UserBug?> IsBugAssignedToUserBefore(Guid bug_Id, string user_Id);
    }
}