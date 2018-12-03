using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThinClientApi.Models;

namespace ThinClientApi.Data
{
    public class ClientFileDbContext : DbContext
    {
        public ClientFileDbContext(DbContextOptions<ClientFileDbContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<ClientFile> ClientFiles { get; set; }
    }
}
