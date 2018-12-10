using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ThinClientApi.Models;

namespace ThinClientApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<ClientFile> ClientFiles { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<RdpEndpoint> RdpEndpoints { get; set; }
    }
}
