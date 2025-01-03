﻿using Finance.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance.Utilities.Database
{
    public class DatabaseContext : DbContext
    {
        private static string connectionString = @"Data Source=PlutoDb.db;";

        public DbSet<AccountModel> AccountModels { get; set; }
        public DbSet<TransactionModel> TransactionModels { get; set; }
        public DbSet<UserModel> UserModels { get; set; }

        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModel>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.Id);
            });

            modelBuilder.Entity<TransactionModel>(entity =>
            {
                entity.HasKey(t => t.Id);
            });

            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.HasMany(u => u.Accounts)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);
            });
        }
    }
}
