using FlightDocSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlightDocSystem.Data
{
    public class FlightDocsContext : DbContext
    {

        public FlightDocsContext(DbContextOptions<FlightDocsContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<DocType> DocTypes { get; set; }

        public DbSet<DocumentHistory> DocumentHistorys { get; set; }
        
    }
}
