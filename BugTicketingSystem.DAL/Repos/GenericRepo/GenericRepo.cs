using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly BugTicketingContext _bugTicketingContext;
        public GenericRepo(BugTicketingContext bugTicketingContext)
        {
            _bugTicketingContext = bugTicketingContext;
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _bugTicketingContext.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _bugTicketingContext.Set<T>().FindAsync(id);
        }
        public void Add(T Entity)
        {
            _bugTicketingContext.Set<T>().Add(Entity);
        }
        public void Delete(T Entity)
        {
            _bugTicketingContext.Set<T>().Remove(Entity);
        }
        public void Update(T Entity)
        {

        }
    }
}
