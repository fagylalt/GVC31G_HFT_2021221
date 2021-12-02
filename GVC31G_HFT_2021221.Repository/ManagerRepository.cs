using GVC31G_HFT_2021221.Data;
using GVC31G_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVC31G_HFT_2021221.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        TrainDbContext db;
        public ManagerRepository(TrainDbContext db)
        {
            this.db = db;
        }
        public void Create(Manager manager)
        {
            db.Managers.Add(manager);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }

        public Manager Read(int id)
        {
            return db.Managers.FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<Manager> ReadAll()
        {
            return db.Managers;
        }

        public void Update(Manager Manager)
        {
            var oldManager = Read(Manager.Id);
            oldManager.Name = Manager.Name;
            oldManager.DepartmentName = Manager.DepartmentName;
            oldManager.Employees = Manager.Employees;
            db.SaveChanges();
        }
    }
}
