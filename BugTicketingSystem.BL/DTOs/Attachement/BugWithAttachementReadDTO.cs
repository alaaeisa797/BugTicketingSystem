using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL
{
    public class BugWithAttachementReadDTO
    {
        
            public Guid BugId { get; set; }
            public string BugTitle { get; set; }


            public DateTime? CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }

            // now i need to put the three props to get the (users name which handels the bug and the project naem in which the bug is at)
            public string ProjectName { get; set; }
            public List<AttachementDetailes>? Attachements { get; set; } 
        

    }
        public class AttachementDetailes
        {
        public Guid AttachementId { get; set; }
        public string AttachementName { get; set; }
        public string AttachementURL { get; set; }
        }
}
