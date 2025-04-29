
namespace BugTicketingSystem.DAL
{
    public interface IGenericRepo<T>
    {
        void Add(T Entity);
        void Delete(T Entity);
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        void Update(T Entity);
    }
}