
using System.Text;
using BugTicketingSystem.BL;
using BugTicketingSystem.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

namespace BugTicketingSystem.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            #region register DAL Services

            builder.Services.RegisterDataAccessLayerServices(builder.Configuration);
            #endregion 
            #region register BL Services

            builder.Services.RegisterBusinessLayerServices();
            #endregion
            #region register identity 

            builder.Services.AddIdentityCore<CustomUser>(options =>
            {
                //validation from configuration 
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;



            }).AddEntityFrameworkStores<BugTicketingContext>();

            #endregion
            #region Authentication

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var secretKey = builder.Configuration.GetValue<string>("ScretKey")!;

                    var secretKeyInBytes = Encoding.UTF8.GetBytes(secretKey);
                    var key = new SymmetricSecurityKey(secretKeyInBytes);

                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = key,
                    };
                });

            #endregion
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();
            #region handel image
            var imagesFolder = Path.Combine(
       Directory.GetCurrentDirectory(),
       "Images");

            app.UseStaticFiles(new StaticFileOptions
            {

                FileProvider = new PhysicalFileProvider(imagesFolder)
               ,
                RequestPath = "/api/my-static-files"
            }); 
            #endregion
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
