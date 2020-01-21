using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRInventories.Models
{
    public partial class HRInventoryDBContext : DbContext
    {
        Connectionstrings _connectionstring;
        public HRInventoryDBContext(Connectionstrings connectionstring)
        {
            _connectionstring = connectionstring;
        }

        
        public virtual DbSet<Catagory> Catagory { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Server=localhost;Port=5433;Database=HRInventoryDB;Username=postgres;Password=dell@123;Integrated Security=true;Pooling=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Catagory>(entity =>
            {
                entity.HasKey(e => e.Categoryid)
                    .HasName("catagory_pkey");

                entity.ToTable("catagory");

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Categorydescription)
                    .IsRequired()
                    .HasColumnName("categorydescription")
                    .HasMaxLength(250);

                entity.Property(e => e.Categoryname)
                    .IsRequired()
                    .HasColumnName("categoryname")
                    .HasMaxLength(50);

                entity.Property(e => e.Createddate).HasColumnName("createddate");

                entity.Property(e => e.Isdeleted)
                    .IsRequired()
                    .HasColumnName("isdeleted")
                    .HasMaxLength(50);

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasColumnName("userid")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Categoryid)
                    .IsRequired()
                    .HasColumnName("categoryid")
                    .HasMaxLength(50);

                entity.Property(e => e.Createddate).HasColumnName("createddate");

                entity.Property(e => e.Isdeleted)
                    .IsRequired()
                    .HasColumnName("isdeleted")
                    .HasMaxLength(50);

                entity.Property(e => e.Productdescription)
                    .IsRequired()
                    .HasColumnName("productdescription")
                    .HasMaxLength(250);

                entity.Property(e => e.Productname)
                    .IsRequired()
                    .HasColumnName("productname")
                    .HasMaxLength(50);

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasColumnName("userid")
                    .HasMaxLength(50);
            });
        }
    }
}
