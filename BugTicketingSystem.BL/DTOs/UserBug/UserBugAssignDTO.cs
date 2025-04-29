using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL
{
    public class UserBugAssignDTO
    {
        public string User_Id { get; set; }
        public Guid Bug_Id { get; set; }
    }
}
