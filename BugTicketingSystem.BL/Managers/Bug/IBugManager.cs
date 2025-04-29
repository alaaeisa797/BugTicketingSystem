
namespace BugTicketingSystem.BL
{
    public interface IBugManager
    {
        Task<GeneralResult> AddAsync(BugAddDTO bugAddDTO);
        Task<GeneralResult<List<BugReadDTO>>> GetAllAsync();
        Task<GeneralResult<BugReadDetailesDTO>> GetByIdWithJoinAsync(Guid id);
    }
}