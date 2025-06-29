using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_Administration_of_computer_labs.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

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
            base.OnModelCreating(modelBuilder); // required for Identity

            // ExtraLabRequest — linked to HOD
            modelBuilder.Entity<ExtraLabRequest>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.HODId)
                .OnDelete(DeleteBehavior.Restrict);

            // Complaint — linked to ApplicationUser (User), Lab, and Equipment
            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.Lab)
                .WithMany()
                .HasForeignKey(c => c.LabId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.Equipment)
                .WithMany()
                .HasForeignKey(c => c.EquipmentId)
                .OnDelete(DeleteBehavior.Restrict);

            
        }
    }
}
