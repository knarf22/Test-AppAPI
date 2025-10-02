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
        public virtual DbSet<ContactMessage> ContactMessages { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Person
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.PersonId).HasName("PK_Persons");

                entity.ToTable("Persons");

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

                entity.ToTable("Users");

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
                      .WithMany(u => u.TodoHistories)
                      .HasForeignKey(h => h.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_TodoHistory_Users");
            });

            //  ContactMessage
            modelBuilder.Entity<ContactMessage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_ContactMessages");

                entity.ToTable("ContactMessages");

                entity.Property(e => e.Name).HasMaxLength(200);
                entity.Property(e => e.Email).HasMaxLength(200);
                entity.Property(e => e.Message).HasMaxLength(2000);
                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(cm => cm.User)
                      .WithMany(u => u.ContactMessages)
                      .HasForeignKey(cm => cm.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_ContactMessages_Users");
            });

            // Post
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.PostId).HasName("PK_Posts");
                entity.ToTable("Posts");
                entity.Property(e => e.Title)
                      .HasMaxLength(300)
                      .IsUnicode(false);
                entity.Property(e => e.Body)
                      .IsUnicode(true);
                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");
                entity.HasOne(p => p.User)
                      .WithMany(u => u.Posts)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Posts_Users");
            });
            // Comment
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.CommentId).HasName("PK_Comments");
                entity.ToTable("Comments");
                entity.Property(e => e.Text)
                      .HasMaxLength(1000)
                      .IsUnicode(true);

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");

                entity.HasOne(c => c.Post)
                      .WithMany(p => p.Comments)
                      .HasForeignKey(c => c.PostId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Comments_Posts");
                entity.HasOne(c => c.User)
                      .WithMany(u => u.Comments)
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Comments_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
