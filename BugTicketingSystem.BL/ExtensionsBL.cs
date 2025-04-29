using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.BL.Managers.Attachements;
using BugTicketingSystem.BL.Managers.Projects;
using BugTicketingSystem.BL.Managers.Users;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BugTicketingSystem.BL
{
    public static class ExtensionsBL
    {
        public static void RegisterBusinessLayerServices(this IServiceCollection services)
        {

            services.AddScoped<IUserManger, UserManger>();
            services.AddScoped<IProjectManager, ProjectManager>();
            services.AddScoped<IBugManager, BugManager>();
            services.AddScoped<IUserBugsManager, UserBugsManager>();
            services.AddScoped<IAttachementManager, AttachementManager>();

            services.AddValidatorsFromAssembly(typeof(ExtensionsBL).Assembly);
          
        }
    }
}
