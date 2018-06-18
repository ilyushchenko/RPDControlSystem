using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RPDControlSystem.Storage;
using RPDControlSystem.Models.RPD;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace RPDControlSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            bool isConnected = false;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<DatabaseContext>();
                var userManager = services.GetRequiredService<UserManager<TeacherProfile>>();
                var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    context.Database.Migrate();

                    Task roleInit = Initializer.InitializeRoleAsync(userManager, rolesManager);
                    roleInit.Wait();
                    isConnected = true;
                }
                catch (Exception)
                {
                    logger.LogError($"Unable to connect to database!");
                }

                if (isConnected)
                {
                    logger.LogInformation($"Succesfully connectet to database");
                    host.Run();
                }
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
