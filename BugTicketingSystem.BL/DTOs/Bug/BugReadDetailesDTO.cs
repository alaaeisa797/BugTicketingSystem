using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL
{
    public class BugReadDetailesDTO
    {
        public Guid BugId { get; set; }
        public string BugTitle { get; set; }
        public string BugDescription { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // now i need to put the three props to get the (users name which handels the bug and the project naem in which the bug is at)
        public string ProjectName { get; set; }
        public List<string>? AssigneesNamesOfBug { get; set; } // lessa ta7t el tagreba 
    }
}
