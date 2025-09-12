using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Test_App.Models.Entities;

namespace Test_App.Models;

public partial class TestDbaseContext : DbContext
{
    public TestDbaseContext()
    {
    }

    public TestDbaseContext(DbContextOptions<TestDbaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<User> Users { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        
    //    => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=test_dbase;User Id=franky;Password=password123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__persons__AA2FFB858C61B89A");

            entity.ToTable("persons");

            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C90D0536E");

            entity.Property(e => e.PasswordHash).HasMaxLength(200);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.Person).WithMany(p => p.Users)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Persons");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
