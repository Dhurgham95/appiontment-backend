using AppiontmentBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace AppiontmentBackEnd.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options)
        {
        }


        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Server=DESKTOP-Q844F82\\SQLEXPRESS;Database=QassamDb;Trusted_Connection=True;");
        //            }
        //        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(ur => ur.UserRoles)
                .HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(ur => ur.UserRoles)
                .HasForeignKey(bc => bc.RoleId);

            //user appiontment 
            modelBuilder.Entity<UserAppionetment>()
              .HasKey(ur => new { ur.UserId, ur.AppionetmentId });
            modelBuilder.Entity<UserAppionetment>()
                .HasOne(ur => ur.User)
                .WithMany(ur => ur.UserAppionetments)
                .HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserAppionetment>()
                .HasOne(ur => ur.Appionetment)
                .WithMany(ur => ur.UserAppionetments)
                .HasForeignKey(bc => bc.AppionetmentId);





        }


        public DbSet<User> Users { get ;  set; }
        public DbSet<Role> Roles { get ;  set; }
        public DbSet<UserRole> UserRoles { get ;  set; }

        public DbSet<UserAppionetment> UserAppionetments { get; set; } 
        public DbSet<Appionetment> Appionetments { get; set; } 

        public DbSet<Status> Statuses { get; set; }

    }
}
