
namespace BugTicketingSystem.DAL
{
    public interface IBugRepo : IGenericRepo<Bug>
    {
        Task<Bug?> GetBugWithHisAttachmentAsync(Guid id);
    }
}