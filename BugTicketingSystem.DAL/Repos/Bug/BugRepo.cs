using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
    public class BugRepo : GenericRepo<Bug>, IBugRepo
    {
        private readonly BugTicketingContext _bugTicketingContext;
        public BugRepo(BugTicketingContext bugTicketingContext) : base(bugTicketingContext)
        {
            _bugTicketingContext = bugTicketingContext;
        }
        public async Task<Bug?> GetBugWithHisAttachmentAsync(Guid id)
        {
            return await _bugTicketingContext.Set<Bug>().AsNoTracking()
                .Include(b => b.Attachments).Include(b=>b.Project).Include(b => b.UserBugs).ThenInclude(us=>us.CustomUser).SingleOrDefaultAsync(b => b.BugId == id);
        }
    }
}
