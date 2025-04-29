using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Repos.Attachement;

namespace BugTicketingSystem.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProjectRepo _projectRepo { get; } // 3shan m2darsh a3mlhaha set f ay wa2t mel aw2at 
        public IBugRepo _bugRepo { get; } // 3shan m2darsh a3mlhaha set f ay wa2t mel aw2at 
        public IUserBugRepo _userBugRepo { get; } // 3shan m2darsh a3mlhaha set f ay wa2t mel aw2at 
        public IAttachementRepo _attachementRepo { get; } // 3shan m2darsh a3mlhaha set f ay wa2t mel aw2at 

        private readonly BugTicketingContext _context;
        public UnitOfWork(IProjectRepo projectRepo, BugTicketingContext bugTicketingContext, IBugRepo bugRepo, IUserBugRepo userBugRepo, IAttachementRepo attachementRepo)
        {
            _projectRepo = projectRepo;
            _bugRepo = bugRepo;
            _context = bugTicketingContext;
            _userBugRepo = userBugRepo;
            _attachementRepo = attachementRepo;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
