using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessManagementAPI.Models
{
    public partial class OrdersContext : DbContext
    {
        public OrdersContext()
        {
        }

        public OrdersContext(DbContextOptions<OrdersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=inventory_management;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.FullName)
                    .IsUnicode(false)
                    .HasColumnName("fullName");

                entity.Property(e => e.PhoneNumber).HasColumnName("Phone_number");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.CustomerId, "IX_Orders_CustomerId");

                entity.Property(e => e.ComThread)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Com_thread")
                    .IsFixedLength();

                entity.Property(e => e.OrderCompletionDate).HasColumnName("Order_completion_date");

                entity.Property(e => e.OrderDate).HasColumnName("Order_date");

                entity.Property(e => e.OrderFulfillmentDate).HasColumnName("Order_fulfillment_date");

                entity.Property(e => e.OrderStatus).HasColumnName("Order_status");

                entity.Property(e => e.OutOfTown)
                    .IsRequired()
                    .HasColumnName("Out_Of_Town")
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasIndex(e => e.OrderId, "IX_Payments_OrderId");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.OrderId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryId");

                entity.HasIndex(e => e.OrderId, "IX_Products_OrderId");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.OrderId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
