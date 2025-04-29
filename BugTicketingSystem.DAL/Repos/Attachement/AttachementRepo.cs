using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Repos.Attachement;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
    public class AttachementRepo : GenericRepo<Attachment> , IAttachementRepo
    {
        private readonly BugTicketingContext _bugTicketingContext;
        public AttachementRepo(BugTicketingContext bugTicketingContext) : base(bugTicketingContext)
        {
            _bugTicketingContext = bugTicketingContext;
        }
        //public async Task<Bug?> GetAttachement(Guid id)
        //{
        //    return await _bugTicketingContext.Set<Bug>().AsNoTracking()
        //        .Include(b => b.Attachments).Include(b => b.Project).Include(b => b.UserBugs).ThenInclude(us => us.CustomUser).SingleOrDefaultAsync(b => b.BugId == id);
        //}
    }
}
