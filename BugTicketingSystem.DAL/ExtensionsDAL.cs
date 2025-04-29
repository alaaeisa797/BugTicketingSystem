using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Repos.Attachement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BugTicketingSystem.DAL
{
    public static class ExtensionsDAL
    {
        public static void RegisterDataAccessLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("hamada");
            // configuration["def"];
            services.AddDbContext<BugTicketingContext>(optionsAction => optionsAction.UseSqlServer(connectionString));

           
            services.AddScoped<IProjectRepo, ProjectRepo>();
            services.AddScoped<IBugRepo, BugRepo>();
            services.AddScoped<IUserBugRepo, UserBugRepo>();
            services.AddScoped<IAttachementRepo, AttachementRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

    }
}
