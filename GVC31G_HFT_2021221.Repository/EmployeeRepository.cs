using GVC31G_HFT_2021221.Data;
using GVC31G_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVC31G_HFT_2021221.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        TrainDbContext db;
        public EmployeeRepository(TrainDbContext db)
        {
            this.db = db;
        }
        public void Create(Employee Employee)
        {
            db.Employees.Add(Employee);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }

        public Employee Read(int id)
        {
            return db.Employees.FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<Employee> ReadAll()
        {
            return db.Employees;
        }

        public void Update(Employee employee)
        {
            var oldEmployee = Read(employee.Id);
            oldEmployee.Name = employee.Name;
            oldEmployee.CurrentTask = employee.CurrentTask;
            oldEmployee.Manager = employee.Manager;
            oldEmployee.ManagerId = employee.ManagerId;
            db.SaveChanges();
        }
    }
}
