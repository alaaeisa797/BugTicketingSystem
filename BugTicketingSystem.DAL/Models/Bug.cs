using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Enums;
using BugTicketingSystem.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BugTicketingSystem.DAL
{
    public class Bug
    {

        public Guid BugId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public BugStatus? Status { get; set; } =  BugStatus.New;
        public BugPriority? Priority { get; set; } = BugPriority.Low;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        //--------------------------------------------------1:M with Project------------------------------------------------------------//
        public Guid Project_Id { get; set; }
        public virtual Project Project { get; set; } = null;
        //--------------------------------------------------1:M with Attachement--------------------------------------------------------//
        public virtual ICollection <Attachment> Attachments { get; set; } = new HashSet<Attachment>();
        //--------------------------------------------1:M with UserBug------------------------------------------------------------------//
        public virtual ICollection<UserBug> UserBugs { get; set; } = new HashSet<UserBug>();
    }
}
