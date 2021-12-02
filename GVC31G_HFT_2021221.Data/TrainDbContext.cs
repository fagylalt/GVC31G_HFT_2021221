using GVC31G_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GVC31G_HFT_2021221.Data
{
    public class TrainDbContext : DbContext
    {
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public TrainDbContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\TrainDb.mdf;Integrated Security=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne(employee => employee.Manager)
                    .WithMany(manager => manager.Employees)
                    .HasForeignKey(employee => employee.ManagerId)
                    .OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<Manager>(entity =>
            {
                entity.HasMany(manager => manager.Employees)
                .WithOne(employee => employee.Manager)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.HasOne(assignment => assignment.Employee)
                .WithMany(employee => employee.CurrentTask)
                .HasForeignKey(assignment => assignment.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            Manager man1 = new Manager { Name="Józska", Id = 1, DepartmentName = "IT" };
            Manager man2 = new Manager { Name="Erika", Id = 2, DepartmentName = "OT" };
            Employee emp1 = new Employee { Id = 1, Name = "Béla", ManagerId = man1.Id };
            Employee emp2 = new Employee { Id = 2, Name = "Ferenc", ManagerId = man1.Id };
            Employee emp3 = new Employee { Id = 3, Name = "Ica", ManagerId = man2.Id };
            Employee emp4 = new Employee { Id = 4, Name = "Erzsi", ManagerId = man2.Id };
            Assignment assi1 = new Assignment { Id = 1, dueDate = DateTime.Now, EmployeeId = emp1.Id, Description = "Vonat takarítás" };
            Assignment assi2 = new Assignment { Id = 2, dueDate = DateTime.MinValue, EmployeeId = emp2.Id, Description = "Vonat elindítás" };
            Assignment assi3 = new Assignment { Id = 3, dueDate = DateTime.MaxValue, EmployeeId = emp2.Id, Description = "Késés megtervezése" };
            

            modelBuilder.Entity<Manager>().HasData(man1,man2);
            modelBuilder.Entity<Employee>().HasData(emp1, emp2,emp3, emp4);
            modelBuilder.Entity<Assignment>().HasData(assi1,assi2,assi3);
        }
    }
}
