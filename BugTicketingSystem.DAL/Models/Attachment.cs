using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL
{
    public class Attachment
    {
        public Guid AttachementId { get; set; }
        public string? FilePath { get; set; }
        public string? FileName { get; set; }
        public string FileURL { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        //-----------------------------------------------1:M with bugs------------------------------------------------------------------------------//

        public Guid Bug_Id { get; set; }

        public virtual Bug Bug { get; set; } = null; 
        //-----------------------------------------------------------------------------------------------------------------------------//


    }
}
