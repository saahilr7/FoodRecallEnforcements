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
    }
}
