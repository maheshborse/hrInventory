using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace HRInventories.Models
{
    public partial class HRInventoryDBContext : DbContext
    {
        Connectionstrings _connectionstring;
        public HRInventoryDBContext(Connectionstrings connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public HRInventoryDBContext()
        {
        }

        public HRInventoryDBContext(DbContextOptions<HRInventoryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Catagory> Catagory { get; set; }
        public virtual DbSet<Dispatchdetails> Dispatchdetails { get; set; }
        public virtual DbSet<Dispatchmaster> Dispatchmaster { get; set; }
        public virtual DbSet<Podetail> Podetail { get; set; }
        public virtual DbSet<Pomaster> Pomaster { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Reqestmaster> Reqestmaster { get; set; }
        public virtual DbSet<Requestdetail> Requestdetail { get; set; }
        public DbQuery<PODispatchDetailsGrid> PODispatchDetailsGrids { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseNpgsql("Server=localhost;Port=5433;Database=HRInventoryDB;Username=postgres;Password=dell@123;Integrated Security=true;Pooling=true");
        //            }
        //        }

        private static ILoggerFactory _loggerFactory = new LoggerFactory().AddConsole();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseNpgsql("Server=localhost;Port=5433;Database=HRInventoryDB;Username=postgres;Password=dell@123;Integrated Security=true;Pooling=true");
                optionsBuilder.UseNpgsql(_connectionstring.DatabaseConnection).UseLoggerFactory(_loggerFactory);
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

            modelBuilder.Entity<Dispatchdetails>(entity =>
            {
                entity.HasKey(e => e.Dispatchdetailid)
                    .HasName("dispatchdetails_pkey");

                entity.ToTable("dispatchdetails");

                entity.Property(e => e.Dispatchdetailid).HasColumnName("dispatchdetailid");

                entity.Property(e => e.Createddate).HasColumnName("createddate");

                entity.Property(e => e.Dispatchid).HasColumnName("dispatchid");

                entity.Property(e => e.Isdeleted)
                    .IsRequired()
                    .HasColumnName("isdeleted")
                    .HasMaxLength(50);

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasColumnName("userid")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Dispatch)
                    .WithMany(p => p.Dispatchdetails)
                    .HasForeignKey(d => d.Dispatchid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dispatchdetails_dispatchid_fkey");
            });

            modelBuilder.Entity<Dispatchmaster>(entity =>
            {
                entity.HasKey(e => e.Dispatchid)
                    .HasName("dispatchmaster_pkey");

                entity.ToTable("dispatchmaster");

                entity.Property(e => e.Dispatchid).HasColumnName("dispatchid");

                entity.Property(e => e.Createddate).HasColumnName("createddate");

                entity.Property(e => e.Dispatchdate).HasColumnName("dispatchdate");

                entity.Property(e => e.Employeeid).HasColumnName("employeeid");

                entity.Property(e => e.Employeename)
                    .IsRequired()
                    .HasColumnName("employeename")
                    .HasMaxLength(50);

                entity.Property(e => e.Isdeleted)
                    .IsRequired()
                    .HasColumnName("isdeleted")
                    .HasMaxLength(50);

                entity.Property(e => e.Totalqty).HasColumnName("totalqty");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasColumnName("userid")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Podetail>(entity =>
            {
                entity.ToTable("podetail");

                entity.Property(e => e.Podetailid).HasColumnName("podetailid");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Createddate).HasColumnName("createddate");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Isdeleted)
                    .IsRequired()
                    .HasColumnName("isdeleted")
                    .HasMaxLength(50);

                entity.Property(e => e.Poid).HasColumnName("poid");

                entity.Property(e => e.Porate).HasColumnName("porate");

                entity.Property(e => e.Productid)
                    .HasColumnName("productid")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasColumnName("userid")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Po)
                    .WithMany(p => p.Podetail)
                    .HasForeignKey(d => d.Poid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("podetail_poid_fkey");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Podetail)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("podetail_productid_fkey");
            });

            modelBuilder.Entity<Pomaster>(entity =>
            {
                entity.HasKey(e => e.Poid)
                    .HasName("pomaster_pkey");

                entity.ToTable("pomaster");

                entity.Property(e => e.Poid).HasColumnName("poid");

                entity.Property(e => e.Createddate).HasColumnName("createddate");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Finalamount).HasColumnName("finalamount");

                entity.Property(e => e.Isdeleted)
                    .IsRequired()
                    .HasColumnName("isdeleted")
                    .HasMaxLength(50);

                entity.Property(e => e.Podate).HasColumnName("podate");

                entity.Property(e => e.Totalamount).HasColumnName("totalamount");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasColumnName("userid")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

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

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.Categoryid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_categoryid_fkey");
            });
            modelBuilder.Entity<Reqestmaster>(entity =>
            {
                entity.HasKey(e => e.Requestid)
                    .HasName("reqestmaster_pkey");

                entity.ToTable("reqestmaster");

                entity.Property(e => e.Requestid).HasColumnName("requestid");

                entity.Property(e => e.Createddate)
                    .HasColumnName("createddate")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.Employeeid)
                    .HasColumnName("employeeid")
                    .HasMaxLength(50);

                entity.Property(e => e.Isdeleted)
                    .HasColumnName("isdeleted")
                    .HasMaxLength(50);

                entity.Property(e => e.Isread).HasColumnName("isread");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Requestdetail>(entity =>
            {
                entity.ToTable("requestdetail");

                entity.Property(e => e.Requestdetailid).HasColumnName("requestdetailid");

                entity.Property(e => e.Createddate)
                    .HasColumnName("createddate")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.Isdeleted)
                    .HasColumnName("isdeleted")
                    .HasMaxLength(50);

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Requestid).HasColumnName("requestid");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(50);

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Requestdetail)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("requestdetail_productid_fkey");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.Requestdetail)
                    .HasForeignKey(d => d.Requestid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("requestdetail_requestid_fkey");
            });
            modelBuilder.Query<PODispatchDetailsGrid>().ToView("podispatchdetailsgrid");
        }
       
    }
}
