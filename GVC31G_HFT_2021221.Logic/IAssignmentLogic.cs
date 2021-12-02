using GVC31G_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVC31G_HFT_2021221.Logic
{
    public interface IAssignmentLogic
    {
        void Create(Assignment Assignment);
        void Delete(int id);
        Assignment Read(int id);
        IEnumerable<Assignment> ReadAll();
        void Update(Assignment Assignment);
        IEnumerable<string> getEmployeeWithLatestAssignment();
        IEnumerable<string> getEmployeeWithLongestAssignmentDescription();
    }
}
