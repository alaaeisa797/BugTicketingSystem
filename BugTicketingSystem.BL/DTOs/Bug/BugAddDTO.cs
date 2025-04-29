using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Enums;

namespace BugTicketingSystem.BL
{
    public class BugAddDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public BugStatus? Status { get; set; } = BugStatus.New;
        public BugPriority? Priority { get; set; } = BugPriority.Low;
        //public DateTime? CreatedAt { get; set; } = DateTime.Now;
        //---------------------------------------------------------------------------------------//
        public Guid Project_Id { get; set; }  // The ID of the Project to which this Bug is assigned to 

    }
}
