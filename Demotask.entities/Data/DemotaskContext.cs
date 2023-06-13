using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using UserManage.entities.Models;
using UserManage.entities.Viewmodels;

namespace UserManage.entities.Data;

public partial class DemotaskContext : DbContext
{
    //public DemotaskContext()
    //{
    //}

    public DemotaskContext(DbContextOptions<DemotaskContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserAddview> Useradd { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("city");

            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CityName)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("city_name");
            entity.Property(e => e.StateId).HasColumnName("state_id");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_city_state");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.ToTable("state");

            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.StateName)
                .HasMaxLength(50)
                .HasColumnName("state_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.StreetAddress)
                .HasMaxLength(255)
                .HasColumnName("street_address");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.CityNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.City)
                .HasConstraintName("Fk_city_user");

            entity.HasOne(d => d.StateNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.State)
                .HasConstraintName("Fk_state_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
