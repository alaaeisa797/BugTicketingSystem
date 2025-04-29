
namespace BugTicketingSystem.BL
{
    public interface IUserBugsManager
    {
        Task<GeneralResult> AssignBugAsync(UserBugAssignDTO userBugAssignDTO);
        Task<GeneralResult> UnAssignUserFromABugAsync(UserBugAssignDTO userBugAssignDTO);
    }
}