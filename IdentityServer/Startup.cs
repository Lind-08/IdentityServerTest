using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IdentityServer.Models;
using IdentityServer.Data;
using IdentityServer4.Models;

namespace IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore();
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
#if DEBUG
            var ConnectionString = "Data Source=LIND-PC;Initial Catalog=ThinClientApi;Integrated Security=True";
#elif RELEASE
            var ConnectionString = Configuration.GetConnectionString("DefaultConnection");
#endif
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(ConnectionString);
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                //.AddTestUsers(IdentityConfig.GetUsers())
                .AddInMemoryClients(IdentityConfig.GetClients())
                .AddInMemoryApiResources(IdentityConfig.GetApiResources())
                .AddAspNetIdentity<ApplicationUser>()
                .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources());
                /*.AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(ConnectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(ConnectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                    };
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                });*/
                
        }

        async private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var userContext = services.GetRequiredService<ApplicationDbContext>();
                userContext.Database.Migrate();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await RoleInitializer.InitializeAsync(userManager, rolesManager, "WORKGROUP");
                /*
                services.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = services.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in IdentityConfig.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in IdentityConfig.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in IdentityConfig.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }*/
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            /*if (!roleManager.Roles.Any())
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "user"
                };
                var result = roleManager.CreateAsync(role);
            }
            if (!userManager.Users.Any())
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Kate",
                    Email = "kate@123.com",
                };
                var result = userManager.CreateAsync(user, "123Kate_password");
                var result1 = userManager.AddToRoleAsync(user, "user");
            }*/
            InitializeDatabase(app);
            app.UseIdentityServer();
            app.UseMvc();
        }
    }
}
