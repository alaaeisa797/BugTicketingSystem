using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
    public class UserBugRepo : GenericRepo<UserBug>, IUserBugRepo
    {
        private readonly BugTicketingContext _bugTicketingContext;
        public UserBugRepo(BugTicketingContext bugTicketingContext) : base(bugTicketingContext)
        {
            _bugTicketingContext = bugTicketingContext;
        }



        public async Task<UserBug?> IsBugAssignedToUserBefore(Guid bug_Id, string user_Id)
        {
            return await _bugTicketingContext.Set<UserBug>().AsNoTracking()
                .SingleOrDefaultAsync(ub => ub.Bug_Id == bug_Id && ub.User_Id == user_Id);
        }


    }
}
