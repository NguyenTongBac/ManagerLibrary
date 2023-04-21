using Microsoft.EntityFrameworkCore;
using Models.Tables;

namespace Models.Entities
{
    public class ManagerLibraryDbContext : DbContext
    {
        public ManagerLibraryDbContext(DbContextOptions<ManagerLibraryDbContext> options) : base(options) {}
        
        public DbSet<Item> Items { get; set; }

        public DbSet<BorrowHistory> BorrowHistories { get; set; }

        public DbSet<BorrowHistoryDetail> BorrowHistoryDetails { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Borrower> Borrowers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}