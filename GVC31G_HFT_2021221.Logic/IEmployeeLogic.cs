using GVC31G_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVC31G_HFT_2021221.Logic
{
    public interface IEmployeeLogic
    {
        void Create(Employee Employee);
        void Delete(int id);
        Employee Read(int id);
        IEnumerable<Employee> ReadAll();
        void Update(Employee Employee);
    }
}
