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
            managerRepo.Create(manager);
        }

        public void Delete(int id)
        {
            managerRepo.Delete(id);
        }

        public Manager Read(int id)
        {
            return managerRepo.Read(id);
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
