using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Xo_Test.Models
{
    public class Xo_TestContext : DbContext
    {
        public Xo_TestContext(DbContextOptions<Xo_TestContext> options) : base(options) { }

        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<BusinessRelation> BusinessRelations { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<SecondaryActivity> SecondaryActivities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=XoTest;User Id=sa;Password=xo_test1234;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Business>(entity =>
            {
                entity.ToTable("Business");
                entity.HasKey(b => b.BusinessId);

                entity.Property(b => b.Name).HasMaxLength(100);
                entity.Property(b => b.BrandName).HasMaxLength(100);
                entity.Property(b => b.Address).HasMaxLength(200);
                entity.Property(b => b.City).HasMaxLength(100);
                entity.Property(b => b.ZipCode).HasMaxLength(10);
                entity.Property(b => b.IsActive);
                entity.Property(b => b.MainActivity).HasMaxLength(100);
            });

            modelBuilder.Entity<BusinessRelation>(entity =>
            {
                entity.ToTable("BusinessRelation");
                entity.HasKey(r => r.RelationId);

                entity.HasOne(r => r.Business1)
                      .WithMany()
                      .HasForeignKey(r => r.BusinessId1).OnDelete(DeleteBehavior.NoAction); ;

                entity.HasOne(r => r.Business2)
                      .WithMany()
                      .HasForeignKey(r => r.BusinessId2).OnDelete(DeleteBehavior.NoAction); ;
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(p => p.ProductId);

                entity.Property(p => p.Code).HasMaxLength(50);
                entity.Property(p => p.Name).HasMaxLength(100);
                entity.Property(p => p.Activity).HasMaxLength(100);
                entity.Property(p => p.Price);
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.ToTable("Phone");
                entity.HasKey(p => p.PhoneId);

                entity.HasOne(p => p.Business)
                    .WithMany(b => b.Phones)
                    .HasForeignKey(p => p.BusinessId);
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.ToTable("Contract"); 
                entity.HasKey(c => c.ContractId);

                entity.Property(e => e.IsActive).HasColumnType("bit");
                entity.HasOne(c => c.Business)
                      .WithMany()
                      .HasForeignKey(c => c.BusinessId);

                entity.HasOne(c => c.Product)
                      .WithMany()
                      .HasForeignKey(c => c.ProductId);

                entity.Property(c => c.StartDate);
                entity.Property(c => c.EndDate);
            });

            modelBuilder.Entity<SecondaryActivity>(entity =>
            {
                entity.ToTable("SecondaryActivity");
                entity.HasKey(s => s.SecondaryActivityId);

                entity.Property(p => p.Name).HasMaxLength(100);

                entity.HasOne(sa => sa.Business)
                    .WithMany(b => b.SecondaryActivities)
                    .HasForeignKey(sa => sa.BusinessId);
            });
        }
    }
}