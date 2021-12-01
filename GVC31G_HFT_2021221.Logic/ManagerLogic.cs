using GVC31G_HFT_2021221.Models;
using GVC31G_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GVC31G_HFT_2021221.Logic
{
    public class ManagerLogic : IManagerLogic
    {
        IManagerRepository managerRepo;
        public ManagerLogic(IManagerRepository manRepo)
        {
            this.managerRepo = manRepo;
        }
        
        public void Create(Manager manager)
        {
            if(manager.Name != null && manager.DepartmentName != null)
            {
            managerRepo.Create(manager);
            }
            else
            {
                throw new ArgumentNullException("Manager must have a name and a department name");
            }
        }

        public void Delete(int id)
        {
            if (id > 0)
            {
                managerRepo.Delete(id);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Id can not be lower than 0");
            }
        }

        public Manager Read(int id)
        {
            if(id > 0)
            {
            return managerRepo.Read(id);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Id can not be lower than 0");
            }
        }

        public IEnumerable<Manager> ReadAll()
        {
            return managerRepo.ReadAll();
        }

        public void Update(Manager manager)
        {
            managerRepo.Update(manager);
        }
        
    }
}
