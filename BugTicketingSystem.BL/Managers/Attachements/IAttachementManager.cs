
namespace BugTicketingSystem.BL.Managers.Attachements
{
    public interface IAttachementManager
    {
        Task<GeneralResult> AddAttachementAsync(Guid bug_id, FileUploadRequest fileRequest);
        Task<GeneralResult> DeleteAttachementFromBug(Guid attachementId);
        Task<GeneralResult<BugWithAttachementReadDTO>> GetBugByIdWithAttachementJoinAsync(Guid Bug_id);
    }
}