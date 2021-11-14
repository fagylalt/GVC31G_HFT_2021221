using GVC31G_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GVC31G_HFT_2021221.Data
{
    public class TrainDbContext : DbContext
    {
        public virtual DbSet<Manager> Manager { get; set; }
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
            Manager man1 = new Manager { Id = 1, DepartmentName = "IT" };
            Employee emp1 = new Employee { Id = 1, Name = "Béla", ManagerId = man1.Id };
            Employee emp2 = new Employee { Id = 2, Name = "Ferkó", ManagerId = man1.Id };
            Assignment ass1 = new Assignment { Id = 1, dueDate = DateTime.Now, EmployeeId = emp1.Id, Description = "asd" };

            modelBuilder.Entity<Manager>().HasData(man1);
            modelBuilder.Entity<Employee>().HasData(emp1, emp2);
            modelBuilder.Entity<Assignment>().HasData(ass1);
        }
    }
}
