using HealthRecordsPro.Data;
using HealthRecordsPro.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthRecordsPro
{
    public class Program
    {

        public static void Main(string[] args)
        {

            var host = CreateHostBuilder(args).Build();
            try
            {
                var scope = host.Services.CreateScope();

                var _cont = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var userManage = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var rolerManage = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                _cont.Database.EnsureCreated();

                var SuperAdmin = new IdentityRole("SuperAdmin");

                var superAdminRole = _cont.Roles.Any(a => a.Name == "SuperAdmin");
                if ((!superAdminRole))
                {
                    rolerManage.CreateAsync(SuperAdmin).GetAwaiter().GetResult();
                }
                var SuperUserExist = _cont.Users.Any(u => u.UserName == "CodeBrain");
                var superadmin = new ApplicationUser()
                {
                    UserName = "CodeBrain",
                    Email = "codebrain@gmail.com",
                    LoginHint = "P@55w0rd",
                };
                if (!SuperUserExist)
                {
                    userManage.CreateAsync(superadmin, "P@55w0rd").GetAwaiter().GetResult();
                    userManage.AddToRoleAsync(superadmin, SuperAdmin.Name).GetAwaiter().GetResult();
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
