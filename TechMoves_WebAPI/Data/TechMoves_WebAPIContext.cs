using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechMove_Logistics.Models;
using TechMoves_WebAPI.Models;

namespace TechMoves_WebAPI.Data
{
    public class TechMoves_WebAPIContext : DbContext
    {
        public TechMoves_WebAPIContext (DbContextOptions<TechMoves_WebAPIContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Client> Clients { get; set; } = default!;
        public DbSet<Manager> Managers { get; set; } = default!;
        public DbSet<Contract> Contracts { get; set; } = default!;
        public DbSet<ServiceRequest> ServiceRequests { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<TechMove_Logistics.State.ContractState>();
            modelBuilder.Ignore<TechMove_Logistics.State.DraftState>();
            modelBuilder.Ignore<TechMove_Logistics.State.ActiveState>();
            modelBuilder.Ignore<TechMove_Logistics.State.ExpiredState>();
        }
    }
}
