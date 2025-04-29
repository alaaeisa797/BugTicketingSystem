using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Enums;

namespace BugTicketingSystem.BL
{
    public class BugReadDTO
    {
        public Guid BugId { get; set; }
        public string BugTitle { get; set; }
        //public string BugDescription { get; set; }
        public string? Status { get; set; } 
        //public string? Priority { get; set; } 
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }

    }
}
