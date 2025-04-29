using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
    public class ProjectRepo : GenericRepo<Project>, IProjectRepo
    {
        private readonly BugTicketingContext _bugTicketingContext;
        public ProjectRepo(BugTicketingContext bugTicketingContext) : base(bugTicketingContext)
        {
            _bugTicketingContext = bugTicketingContext;
        }

        public async Task<Project?> GetProjectWithHisBugsAsync(Guid id)
        {
            return await _bugTicketingContext.Set<Project>().AsNoTracking()
                .Include(p => p.Bugs).SingleOrDefaultAsync(p => p.ProjectId == id);
        }
    }
}
