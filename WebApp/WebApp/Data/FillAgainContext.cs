using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;
namespace WebApp.Data
{
    public class FillAgainContext : DbContext
    {
      
        public FillAgainContext(DbContextOptions<FillAgainContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Cup> Cups { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Friendship> Friendships { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
