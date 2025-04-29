using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Enums;

namespace BugTicketingSystem.BL
{
    public class ProjectReadWithdetailesDTO
    {
        public string ProjectName { get; set; }
        public List<BugReadFromProjectDTO>? BugsOfProject { get; set; }
    }

    public class BugReadFromProjectDTO
    {
        public string BugTitle { get; set; }
        public string BugDescription { get; set; }
        public BugStatus? BugStatus { get; set; }
        public BugPriority? BugPriority { get; set; }
        public DateTime? BugCreatedAt { get; set; }
        public DateTime? BugUpdatedAt { get; set; }
    }
}
