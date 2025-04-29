using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL.DTOs.User
{
    public record LoginCredentials(string UserEmail, string Password);
}
