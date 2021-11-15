using GVC31G_HFT_2021221.Models;
using GVC31G_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVC31G_HFT_2021221.Logic
{
    class EmployeeLogic : IEmployeeLogic
    {
        IEmployeeRepository repo;

        public EmployeeLogic(IEmployeeRepository repo)
        {
            this.repo = repo;
        }
        public void Create(Employee Employee)
        {
            repo.Create(Employee);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public Employee Read(int id)
        {
            return repo.Read(id);
        }

        public IEnumerable<Employee> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Employee Employee)
        {
            repo.Update(Employee);
        }
    }
}
