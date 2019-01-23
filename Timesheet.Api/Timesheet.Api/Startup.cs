using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Timesheet.BLL;
using Timesheet.BLL.Interfaces;
using Timesheet.DAL;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;

namespace Timesheet.Api
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
            services.AddOptions();

            var appSettings = Configuration.GetSection("AppSettings");



            AppSettings config = new AppSettings();
            Configuration.GetSection("AppSettings").Bind(config);

            //string secret = Configuration.GetValue<string>("AppSettings:Secret");
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            var appSettingsObject = appSettings.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(config.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            services.AddSingleton<IUserLoginDP, UserLoginDP>();
            services.AddSingleton<IEmployeeDP, EmployeeDP>();
            services.AddSingleton<ITimesheetDP, TimesheetDP>();
            services.AddSingleton<IProjectDP, ProjectDP>();
            services.AddSingleton<IEmployeeProjectDP, EmployeeProjectDP>();
            services.AddSingleton<ITaskDP, TaskDP>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<IHashService, HashService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ITimesheetService, TimesheetService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IProjectService, ProjectService>();
            services.AddSingleton<IEmployeeProjectService, EmployeeProjectService>();



            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseCors("AllowAll");
            app.UseMiddleware<HeaderMiddleware>();
            app.UseMvc();
        }
    }
}
