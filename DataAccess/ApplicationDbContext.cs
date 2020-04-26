using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodRecallEnforcements.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodRecallEnforcements.DataAccess
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

       

        public DbSet<Enforcement> Enforcements { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Recall> Recalls { get; set; }

        public DbSet<Classification> Classifications { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Firm> Firms { get; set; }
    }
}
