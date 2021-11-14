using GVC31G_HFT_2021221.Data;
using GVC31G_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVC31G_HFT_2021221.Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {
        TrainDbContext db;
        public AssignmentRepository(TrainDbContext db)
        {
            this.db = db;
        }
        public void Create(Assignment assignment)
        {
            db.Assignments.Add(assignment);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }

        public Assignment Read(int id)
        {
            return db.Assignments.FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<Assignment> Readall()
        {
            return db.Assignments;
        }

        public void Update(Assignment assignment)
        {
            var oldAssignment = Read(assignment.Id);
            oldAssignment = assignment;
            db.SaveChanges();
        }
    }
}
