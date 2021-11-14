using GVC31G_HFT_2021221.Models;
using System;
using System.Linq;

namespace GVC31G_HFT_2021221.Repository
{
    public interface IAssignmentRepository
    {
        void Create(Assignment assignment);
        void Delete(int id);
        Assignment Read(int id);

        IQueryable<Assignment> Readall();
        void Update(Assignment assignment);
    }
}
