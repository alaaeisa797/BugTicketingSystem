
namespace BugTicketingSystem.BL.Managers.Projects
{
    public interface IProjectManager
    {
        Task<GeneralResult> AddAsync(ProjectAddDTO projectAddDTO);
        Task<GeneralResult<List<ProjectReadDTO>>> GetAllAsync();
        Task<GeneralResult<ProjectReadWithdetailesDTO>> GetByIdWithJoinAsync(Guid id);
    }
}