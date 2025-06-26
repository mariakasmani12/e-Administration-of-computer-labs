using Microsoft.EntityFrameworkCore;

namespace e_Administration_of_computer_labs.Models
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Software> Softwares { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<LabAssignment> LabAssignments { get; set; }
        public DbSet<ExtraLabRequest> ExtraLabRequests { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExtraLabRequest>()
             .HasOne(r => r.HOD)
             .WithMany()
             .HasForeignKey(r => r.HODId)
             .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.User)
                .WithMany(u => u.Complaints)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.Lab)
                .WithMany(l => l.Complaints)
                .HasForeignKey(c => c.LabId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.Equipment)
                .WithMany(e => e.Complaints)
                .HasForeignKey(c => c.EquipmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }








    }
}
