using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RPDControlSystem.Models.RPD;
using RPDControlSystem.Storage;

namespace RPDControlSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string server = Configuration["DB_SERVER"] ?? "localhost";
            string port = Configuration["DB_PORT"] ?? "3306";
            string database = Configuration["DB_NAME"] ?? "rpd";
            string user = Configuration["DB_USER"] ?? "dev_user";
            string password = Configuration["DB_PASSWORD"] ?? "dev_password";

            var connectionString = $"server={server};port={port};database={database};uid={user};password={password};";

            Console.WriteLine(connectionString);

            services.AddDbContext<DatabaseContext>(options => options.UseMySql(connectionString));

            services.AddIdentity<TeacherProfile, IdentityRole>(opt =>
            {
                // Задание минимальной длинны пароля
                opt.Password.RequiredLength = 6;
                // Требование символов
                opt.Password.RequireNonAlphanumeric = true;
                // требование нижнего регистра
                opt.Password.RequireLowercase = true;
                // требование верхнего регистра
                opt.Password.RequireUppercase = true;
                // требование цифр
                opt.Password.RequireDigit = true;
                // Задает требование уникальности почты
                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<DatabaseContext>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "DisciplineInfoes",
                    template: "{controller=DisciplineInfoes}/{action=Index}/{id}/{code?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
