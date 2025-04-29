using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL
{
    public record TokenDTO(string token, DateTime ExpiryDate);
}
