using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.Models
{
    public class Project
    {
        public Guid ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string Description { get; set; }

        //---------------------------------------------------1:M With Bugs-----------------------------------------------//
        public virtual ICollection<Bug> Bugs { get; set; } = new HashSet<Bug>();

        //--------------------------------------------------------------------------------------------------------------//
    }
}
