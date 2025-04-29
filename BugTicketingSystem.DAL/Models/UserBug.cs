using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL.Models
{
    [PrimaryKey("User_Id", "Bug_Id")]
    public class UserBug
    {
        // and the whole table is the o/p of the M:M relationShip between CustomUser and Bug
        //--------------------------------------1:M with CustomUser-------------------------------------------------------//
        [ForeignKey("CustomUser")]
        public string User_Id { get; set; }
        public virtual CustomUser CustomUser { get; set; } = null!;

        //--------------------------------------1:M with Bug-------------------------------------------------------//
        [ForeignKey("Bug")]
        public Guid Bug_Id { get; set; }
        public virtual Bug Bug { get; set; } = null!;


    }
}
