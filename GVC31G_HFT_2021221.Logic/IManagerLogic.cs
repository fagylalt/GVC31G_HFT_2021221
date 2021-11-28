using GVC31G_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVC31G_HFT_2021221.Logic
{
    public interface IManagerLogic
    {
        void Create(Manager manager);
        void Delete(int id);
        Manager Read(int id);
        IEnumerable<Manager> ReadAll();
        void Update(Manager manager);
    }
}
