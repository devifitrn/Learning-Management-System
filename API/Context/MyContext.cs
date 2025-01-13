using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            //modelBuilder.Entity<User>()
            //    .Property(usr => usr.Gender)
            //    .HasConversion<string>();

            modelBuilder.Entity<Course>()
              .HasOne(cse => cse.User)
              .WithMany(usr => usr.Courses)
              .HasForeignKey(cse => cse.UserId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Content>()
               .HasOne(ctt => ctt.Course)
               .WithMany(usr => usr.Contents)
               .HasForeignKey(emt => emt.CourseId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
               .HasOne(emt => emt.User)
               .WithMany(usr => usr.Enrollments)
               .HasForeignKey(emt => emt.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
               .HasOne(emt => emt.Course)
               .WithMany(cse => cse.Enrollments)
               .HasForeignKey(emt => emt.CourseId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
              .HasOne(rvw => rvw.User)
              .WithMany(cse => cse.Reviews)
              .HasForeignKey(rvw => rvw.UserId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
              .HasOne(rvw => rvw.Course)
              .WithMany(cse => cse.Reviews)
              .HasForeignKey(rvw => rvw.CourseId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Catalogue>()
              .HasOne(cge =>  cge.Course)
              .WithMany(cse => cse.Catalogues)
              .HasForeignKey(cge => cge.CourseId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Catalogue>()
              .HasOne(cge => cge.Category)
              .WithMany(cry => cry.Catalogues)
              .HasForeignKey(cge => cge.CategoryId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
              .HasOne(a => a.Account)
              .WithOne(b => b.User)
              .HasForeignKey<Account>(b => b.Id);
            modelBuilder.Entity<Authority>()
               .HasKey(bc => new { bc.AccountId, bc.RoleId });
            modelBuilder.Entity<Authority>()
                .HasOne(bc => bc.Account)
                .WithMany(b => b.Authorities)
                .HasForeignKey(bc => bc.AccountId);
            modelBuilder.Entity<Role>()
                .HasMany(c => c.Authorities)
                .WithOne(bc => bc.Role)
                .HasForeignKey(bc => bc.RoleId);

            modelBuilder.Entity<SubContent>()
                .HasOne(a => a.Content)
                .WithMany(b => b.SubContent)
                .HasForeignKey(a => a.ContentId);
            modelBuilder.Entity<Resource>()
                .HasOne(a => a.SubContent)
                .WithMany(b => b.Resources)
                .HasForeignKey(a => a.SubContentId);
            modelBuilder.Entity<Quiz>()
                .HasOne(a => a.SubContent)
                .WithMany(b => b.Quizzes)
                .HasForeignKey(a => a.SubContentId);
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Quiz)
                .WithMany(b => b.Answers)
                .HasForeignKey(a => a.QuizId);

        }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Authority> Authorities { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<SubContent> SubContents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Catalogue> Catalogues { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Review> Reviews { get; set; }


    }
}
