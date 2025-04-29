using System.Numerics;
using BugTicketingSystem.DAL.Models;

namespace BugTicketingSystem.DAL
{
    public interface IProjectRepo  : IGenericRepo<Project>
    {
        Task<Project?> GetProjectWithHisBugsAsync(Guid id);
    }
}