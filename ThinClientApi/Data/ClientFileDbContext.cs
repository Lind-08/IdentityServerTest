using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
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
            builder.Entity<ClientFile>().HasData(new ClientFile
            {
                Version = "1.2",
                FileName = "some_file.exe",
                Checksum = "CC1AB435A408325E9E08ADA9798C8B1D"
            });
            base.OnModelCreating(builder);
        }

        public DbSet<ClientFile> ClientFiles { get; set; }
    }
}
