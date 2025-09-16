using Microsoft.EntityFrameworkCore;
using Test_App.Models.Entities;

namespace Test_App.Models
{
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
        public virtual DbSet<Todo> Todos { get; set; }
        public virtual DbSet<TodoHistory> TodoHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Person
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.PersonId).HasName("PK_Persons");

                entity.ToTable("persons");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");
                entity.Property(e => e.City).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.FirstName).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.LastName).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.MiddleName).HasMaxLength(255).IsUnicode(false);
            });

            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK_Users");

                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.PasswordHash).HasMaxLength(200);
                entity.Property(e => e.PersonId).HasColumnName("PersonID");
                entity.Property(e => e.Username).HasMaxLength(100);

                entity.HasOne(d => d.Person)
                      .WithMany(p => p.Users)
                      .HasForeignKey(d => d.PersonId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Users_Persons");
            });

            // Todo
            modelBuilder.Entity<Todo>(entity =>
            {
                entity.HasKey(e => e.TodoId).HasName("PK_Todos");

                entity.ToTable("Todos");

                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Todos)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Todos_Users");
            });


            // TodoHistory
            modelBuilder.Entity<TodoHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId).HasName("PK_TodoHistory");

                entity.ToTable("TodoHistory");

                entity.HasOne(h => h.Todo)
                      .WithMany(t => t.Histories)
                      .HasForeignKey(h => h.TodoId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_TodoHistory_Todos");

                entity.HasOne(h => h.User)
                      .WithMany(u => u.TodoHistories) // <-- Add this in User.cs
                      .HasForeignKey(h => h.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_TodoHistory_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
