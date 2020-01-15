using InventoryDataAccess.Services;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryDataAccess.Models
{
    public  class DataContext: DbContext
    {
        ConnectionString _connectionstring;
        //public DataContext(DbContextOptions<DataContext> options)
        //    : base(options)
        //{

        //}
        public DataContext(ConnectionString connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public DbSet<Category> category { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connectionstring.DefaultConnection);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<long>("category_id")
               .StartsAt(1)
               .IncrementsBy(1);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => new
                {
                    e.category_id,
                    e.category_name,
                    e.category_description,
                    e.created_date,
                    e.user_id
                })
                    .HasName("Ix_Units_OrgId_IsDeleted");
              
                entity.Property(e => e.created_date)
                                .HasColumnType("datetime")
                                .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.category_name)
                                .IsRequired()
                                .HasMaxLength(50)
                                .IsUnicode(false);

                entity.Property(e => e.category_description)
                                .IsRequired()
                                .HasMaxLength(50)
                                .IsUnicode(false);
            });
        }

    }
}
