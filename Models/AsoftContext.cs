using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TestPrj2.Models;

public partial class AsoftContext : DbContext
{
    public AsoftContext()
    {
    }

    public AsoftContext(DbContextOptions<AsoftContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-8K1UAMI\\SQLEXPRESS;Database=Asoft;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B8258CA9F1");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(10)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(10);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__D796AAD5D27356E9");

            entity.ToTable("Invoice");

            entity.Property(e => e.InvoiceId)
                .HasMaxLength(10)
                .HasColumnName("InvoiceID");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(10)
                .HasColumnName("CustomerID");
            entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Invoice__Custome__4D94879B");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.InvoiceDetailId).HasName("PK__InvoiceD__1F1578F17EB366CB");

            entity.Property(e => e.InvoiceDetailId)
                .HasMaxLength(10)
                .HasColumnName("InvoiceDetailID");
            entity.Property(e => e.InvoiceId)
                .HasMaxLength(10)
                .HasColumnName("InvoiceID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .HasColumnName("ProductID");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__InvoiceDe__Invoi__5070F446");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__InvoiceDe__Produ__5165187F");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6EDEA4F3108");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .HasColumnName("ProductID");
            entity.Property(e => e.ProductName).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
