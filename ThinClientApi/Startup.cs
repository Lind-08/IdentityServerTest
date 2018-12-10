using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using ThinClientApi.Data;
using ThinClientApi.Models;

namespace ThinClientApi
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
#if DEBUG
            var connectionString = "Data Source=LIND-PC;Initial Catalog=ThinClientApi;Integrated Security=True";
#elif RELEASE
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
#endif
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration["Authority"];
                    options.RequireHttpsMetadata = false;
                    options.ApiName = Configuration["AppName"];
                });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
                if (!context.ClientFiles.Any())
                {
                    ClientFile clientFile = new ClientFile
                    {
                        Version = "1.2",
                        FileName = "some_file.exe",
                        Checksum = "CC1AB435A408325E9E08ADA9798C8B1D"
                    };
                    context.SaveChanges();
                }
                if (!context.RdpEndpoints.Any())
                {
                    RdpEndpoint rdpEndpoint = new RdpEndpoint
                    {
                        Address = "1.1.1.1",
                        Port = 123,
                    };
                    context.RdpEndpoints.Add(rdpEndpoint);
                    RdpEndpoint rdpEndpoint1 = new RdpEndpoint
                    {
                        Address = "2.2.2.2",
                        Port = 321,
                    };
                    context.RdpEndpoints.Add(rdpEndpoint1);
                    Domain domain1 = new Domain
                    {
                        Name = "PRO-SAAS",
                        Description = "PRO-SAAS",
                        RdpEndpoints = new[] { rdpEndpoint }
                    };
                    Domain domain2 = new Domain
                    {
                        Name = "SAAS",
                        Description = "SAAS",
                        RdpEndpoints = new[] { rdpEndpoint1 }
                    };
                    context.Domains.Add(domain1);
                    context.Domains.Add(domain2);
                    context.SaveChanges();
                    context.SaveChanges();
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            InitializeDatabase(app);
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
