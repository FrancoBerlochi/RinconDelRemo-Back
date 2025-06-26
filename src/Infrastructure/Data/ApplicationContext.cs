using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        { 
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Attendant> Attendants { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Kayak> Kayaks { get; set; }
        public DbSet<Administrator> Admins { get; set; }
        public DbSet<KayakReservation> KayaksReservations { get; set; }
        public DbSet<Hanger> Hangers { get; set; }
    }
}
