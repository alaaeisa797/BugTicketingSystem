using BugTicketingSystem.DAL.Repos.Attachement;

namespace BugTicketingSystem.DAL
{
    public interface IUnitOfWork
    {
        IAttachementRepo _attachementRepo { get; }
        IBugRepo _bugRepo { get; }
        IProjectRepo _projectRepo { get; }
        IUserBugRepo _userBugRepo { get; }

        Task<int> SaveChangesAsync();
    }
}