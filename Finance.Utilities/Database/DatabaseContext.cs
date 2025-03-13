using Finance.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance.Utilities.Database
{
    public class DatabaseContext : DbContext
    {
        private static string connectionString = @"Data Source=PlutoDb.db;";

        public DbSet<AccountModel> AccountModels { get; set; }
        public DbSet<AllocationModel> AllocationModels { get; set; }
        public DbSet<GroupModel> GroupModels { get; set; }
        public DbSet<PersonModel> PersonModels { get; set; }
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
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModel>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId);

                entity.HasMany(a => a.Allocations)
                .WithOne(a => a.Account)
                .HasForeignKey(a => a.AccountId);
            });

            modelBuilder.Entity<AllocationModel>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.HasMany(a => a.Transactions)
                .WithOne(t => t.Allocation)
                .HasForeignKey(t => t.AllocationId);
            });

            modelBuilder.Entity<GroupModel>(entity =>
            {
                entity.HasKey(g => g.Id);

                entity.HasMany(g => g.Persons)
                .WithOne(g => g.Group)
                .HasForeignKey(g => g.GroupId);
            });

            modelBuilder.Entity<PersonModel>(entity =>
            {
                entity.HasKey(a => a.Id);
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

                entity.HasMany(u => u.Groups)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);
            });
        }
    }
}
