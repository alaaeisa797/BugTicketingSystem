using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.Enums
{
    public enum BugStatus
    {
        Open,
        InProgress,
        Resolved , 
        New
    }
    public enum BugPriority
    {
        Low, Medium, High
    }

}
