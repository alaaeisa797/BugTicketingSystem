using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace BugTicketingSystem.DAL
{
    public class CustomUser : IdentityUser
    {
        //--------------------------------------------1:M with UserBug---------------------------------------------------------------------------------//
        public virtual ICollection<UserBug> UserBugs { get; set; } = new HashSet<UserBug>();
    }
}
