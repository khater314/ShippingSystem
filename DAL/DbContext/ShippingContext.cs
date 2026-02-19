using Bogus;
using DAL.UserModel;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DAL.DbContext;

public partial class ShippingContext : IdentityDbContext<AppUser>
{
    public ShippingContext()
    {
    }

    //public ShippingContext(DbContextOptions options)
    //    : base(options)
    //{
    //}
    public ShippingContext(DbContextOptions<ShippingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCarrier> TbCarriers { get; set; }

    public virtual DbSet<TbCity> TbCities { get; set; }

    public virtual DbSet<TbCountry> TbCountries { get; set; }

    public virtual DbSet<TbPaymentMethod> TbPaymentMethods { get; set; }

    public virtual DbSet<TbSetting> TbSettings { get; set; }

    public virtual DbSet<TbShippingType> TbShippingTypes { get; set; }

    public virtual DbSet<TbShippment> TbShippments { get; set; }

    public virtual DbSet<TbShippmentStatus> TbShippmentStatuses { get; set; }

    public virtual DbSet<TbSubscriptionPackage> TbSubscriptionPackages { get; set; }

    public virtual DbSet<TbUserReceiver> TbUserReceivers { get; set; }

    public virtual DbSet<TbUserSender> TbUserSenders { get; set; }

    public virtual DbSet<TbUserSubscription> TbUserSubscriptions { get; set; }

    public virtual DbSet<TbLog> Logs { get; set; }




    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // بنجيب كل الحاجات اللي حصل فيها تغيير أو إضافة وموروثة من BaseEntity
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity &&
                       (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (BaseEntity)entry.Entity;
            var now = DateTime.UtcNow; 

            if (entry.State == EntityState.Added)
            {
                entity.CreatedDate = now;
                //Identity
                //entity.CreatedBy = "System User"; 
            }
            else 
            {
                entry.Property(nameof(BaseEntity.CreatedDate)).IsModified = false;
                entry.Property(nameof(BaseEntity.CreatedBy)).IsModified = false;

                entity.UpdatedDate = now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Start EF Core Identity Tables Configuration
        base.OnModelCreating(modelBuilder);
        //End EF Core Identity Tables Configuration

        modelBuilder.Entity<TbCarrier>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())", "DF_TbCarriers_Id");
            entity.Property(e => e.CarrierName).HasMaxLength(200);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbCity>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())", "DF_TbCities_Id");
            entity.Property(e => e.CityAname)
                .HasMaxLength(100)
                .HasColumnName("CityAName");
            entity.Property(e => e.CityEname)
                .HasMaxLength(100)
                .HasColumnName("CityEName");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Country).WithMany(p => p.TbCities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbCities_TbCountries");
        });

        modelBuilder.Entity<TbCountry>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())", "DF_TbCountries_Id");
            entity.Property(e => e.CountryAname)
                .HasMaxLength(200)
                .HasColumnName("CountryAName");
            entity.Property(e => e.CountryEname)
                .HasMaxLength(200)
                .HasColumnName("CountryEName");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbPaymentMethod>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())", "DF_TbPaymentMethods_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.MethdAname)
                .HasMaxLength(200)
                .HasColumnName("MethdAName");
            entity.Property(e => e.MethodEname)
                .HasMaxLength(200)
                .HasColumnName("MethodEName");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbSetting>(entity =>
        {
            entity.ToTable("TbSetting");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())", "DF_TbSetting_Id");
        });

        modelBuilder.Entity<TbShippingType>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())", "DF_TbShippingTypes_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ShippingTypeAname)
                .HasMaxLength(200)
                .HasColumnName("ShippingTypeAName");
            entity.Property(e => e.ShippingTypeEname)
                .HasMaxLength(200)
                .HasColumnName("ShippingTypeEName");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbShippment>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())", "DF_TbShippments_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.PackageValue).HasColumnType("decimal(8, 4)");
            entity.Property(e => e.ShippingDate).HasColumnType("datetime");
            entity.Property(e => e.ShippingRate).HasColumnType("decimal(8, 4)");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.TbShippments)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK_TbShippments_TbPaymentMethods");

            entity.HasOne(d => d.Receiver).WithMany(p => p.TbShippments)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbShippments_TbUserReceivers");

            entity.HasOne(d => d.Sender).WithMany(p => p.TbShippments)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbShippments_TbUserSenders");

            entity.HasOne(d => d.ShippingType).WithMany(p => p.TbShippments)
                .HasForeignKey(d => d.ShippingTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbShippments_TbShippingTypes");
        });

        modelBuilder.Entity<TbShippmentStatus>(entity =>
        {
            entity.ToTable("TbShippmentStatus");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())", "DF_TbShippmentStatus_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Carrier).WithMany(p => p.TbShippmentStatuses)
                .HasForeignKey(d => d.CarrierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbShippmentStatus_TbCarriers");

            entity.HasOne(d => d.Shippment).WithMany(p => p.TbShippmentStatuses)
                .HasForeignKey(d => d.ShippmentId)
                .HasConstraintName("FK_TbShippmentStatus_TbShippments");
        });

        modelBuilder.Entity<TbSubscriptionPackage>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())", "DF_TbSubscriptionPackages_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.PackageName).HasMaxLength(200);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbUserReceiver>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())", "DF_TbUserReceivers_Id");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(200);
            entity.Property(e => e.ReceiverName).HasMaxLength(200);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.City).WithMany(p => p.TbUserReceivers)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbUserReceivers_TbCities");
        });

        modelBuilder.Entity<TbUserSender>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())", "DF_TbUserSenders_Id");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(200);
            entity.Property(e => e.SenderName).HasMaxLength(200);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.City).WithMany(p => p.TbUserSenders)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbUserSenders_TbCities");
        });

        modelBuilder.Entity<TbUserSubscription>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())", "DF_TbUserSubscriptions_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.SubscriptionDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Package).WithMany(p => p.TbUserSubscriptions)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbUserSubscriptions_TbSubscriptionPackages");
        });

        modelBuilder.Entity<TbLog>(entity =>
        {
            entity.ToTable("Logs");
            entity.HasKey(e => e.Id);
            entity.ToTable("Logs", t => t.ExcludeFromMigrations());
        });

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
