using GVC31G_HFT_2021221.Models;
using GVC31G_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GVC31G_HFT_2021221.Logic
{
    public class EmployeeLogic : IEmployeeLogic
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
        public bool DoesEmployeeHasManager(Employee Employee)
        {
            if(Employee.Manager == null)
            {
                throw new ArgumentNullException($"{Employee.Name} does not have a manager");
            }
            else
            {
                return true;
            }
        }
        public string whoHasMaxAssignment()
        {
            var readRepo = repo.ReadAll();
            var max = (from X in readRepo
                       orderby X.CurrentTask.Count() descending
                       select X.Name).FirstOrDefault();
            return max;
        }
        public IEnumerable<SelectAllEmp> ListAllEmployees()
        {
            var allEmp = repo.ReadAll();
            var selectEmp = from x in allEmp
                            where x.ManagerId == x.Manager.Id
                            select new SelectAllEmp
                            {
                                managerName = x.Manager.Name,
                                name = x.Name
                            };
            return selectEmp;
        }
        public IEnumerable<SelectEmpCount> EmployeesbyManagers()
        {
            var allEmp = repo.ReadAll();
            var selectEmp = (from x in allEmp
                            where x.ManagerId == x.Manager.Id
                            select new SelectEmpCount
                            {
                                managerName = x.Manager.Name,
                                count = x.Manager.Employees.Count()
                            }).Distinct();
            return selectEmp;
        }

    }
    public class SelectAllEmp
    {
        public string managerName { get; set; }
        public string name { get;  set; }
    }
public class SelectEmpCount
{
    public string managerName { get; set; }
    public double count { get; set; }
}
}
