
using Microsoft.EntityFrameworkCore;
namespace BankingApplication.Data
{
    public partial class BankingApplicationdbContext : DbContext
    {
        public BankingApplicationdbContext()
        {
        }

        public BankingApplicationdbContext(DbContextOptions<BankingApplicationdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<User> Users { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseSqlServer("Server=ASPLAP1320;Database=BankingApplicationdb;Trusted_Connection=True;TrustServerCertificate=True;");

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Account>(entity =>
                {
                    entity.HasKey(e => e.Id).HasName("PK__Accounts__3214EC0720D1C7AC");

                    entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");

                    entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                        .HasForeignKey(d => d.UserId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Accounts__UserId__4D94879B");
                });

                modelBuilder.Entity<User>(entity =>
                {
                    entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0736F9FCE3");

                    entity.HasIndex(e => e.Username, "UQ__Users__536C85E4133D76C2").IsUnique();

                    entity.Property(e => e.PasswordHash).HasMaxLength(255);
                    entity.Property(e => e.PinHash).HasMaxLength(200);
                    entity.Property(e => e.Username).HasMaxLength(100);
                });

                OnModelCreatingPartial(modelBuilder);
            }

            partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        
    }
}
